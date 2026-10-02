
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO


/* =========================================================================
   [01/51] PROCEDIMIENTO: usp_RegistrarError
   MÓDULO: SISTEMA Y AUDITORÍA DE ERRORES
   AUTORIZACIÓN / ROLES: Cualquier usuario o llamado interno desde bloques CATCH
   DESCRIPCIÓN:
   Registra excepciones no controladas en la tabla tblLogErrores con numero de error, procedimiento, linea, usuario y mensaje para auditoria técnica.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RegistrarError
 @MensajeError NVARCHAR(MAX), @NumeroError INT, @Procedimiento NVARCHAR(100), @LineaError INT, @UsuarioApp NVARCHAR(25)
AS
BEGIN
 SET NOCOUNT ON;
INSERT dbo.tblLogErrores(mensajeError,numeroError,procedimiento,lineaError,usuarioApp) VALUES(@MensajeError,@NumeroError,@Procedimiento,@LineaError,@UsuarioApp);
END;
GO


/* =========================================================================
   [02/51] PROCEDIMIENTO: usp_Interno_ValidarUsuario
   MÓDULO: SEGURIDAD / AUXILIAR INTERNO
   AUTORIZACIÓN / ROLES: Uso interno por los procedimientos del sistema
   DESCRIPCIÓN:
   Valida que el usuario actor exista, se encuentre activo y posea uno de los roles autorizados consultando la tabla dbo.Roles. Lanza error 51001 si no cumple.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_Interno_ValidarUsuario
    @IdUsuarioActor INT, @Roles VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios u
        INNER JOIN dbo.Roles r ON u.IdRol = r.IdRol
        WHERE u.IdUsuario=@IdUsuarioActor AND u.Estado=1 AND r.Estado=1
        AND CHARINDEX(','+r.Nombre+',', ','+@Roles+',') > 0)
        THROW 51001, 'Usuario inexistente, inactivo o sin el rol requerido.', 1;
END;
GO


/* =========================================================================
   [03/51] PROCEDIMIENTO: usp_Interno_BloquearOrden
   MÓDULO: FLUJO DE TRABAJO / AUXILIAR INTERNO
   AUTORIZACIÓN / ROLES: Uso interno en operaciones transaccionales de órdenes
   DESCRIPCIÓN:
   Bloquea con UPDLOCK y HOLDLOCK el registro de una orden de reparacion para garantizar concurrencia segura ante accesos simultáneos, retornando estado y técnico.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_Interno_BloquearOrden
    @IdOrden INT, @Estado VARCHAR(50) OUTPUT, @Tecnico INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SET @Estado=NULL; SET @Tecnico=NULL;
    SELECT @Estado=e.Nombre, @Tecnico=o.IdTecnicoResponsable
    FROM dbo.OrdenesReparacion o WITH (UPDLOCK,HOLDLOCK)
    JOIN dbo.EstadosReparacion e ON e.IdEstado=o.IdEstado
    WHERE o.IdOrden=@IdOrden AND o.Anulada=0;
    IF @Estado IS NULL THROW 51002, 'La orden no existe o esta anulada.', 1;
END;
GO


/* =========================================================================
   [04/51] PROCEDIMIENTO: usp_Interno_Estado
   MÓDULO: FLUJO DE TRABAJO / AUXILIAR INTERNO
   AUTORIZACIÓN / ROLES: Uso interno en transiciones de flujo
   DESCRIPCIÓN:
   Registra formalmente una transicion de estado en dbo.HistorialEstados y actualiza la columna IdEstado en dbo.OrdenesReparacion.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_Interno_Estado
    @IdOrden INT, @Nombre VARCHAR(50), @IdUsuarioActor INT, @Observacion VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Anterior INT, @Nuevo INT;
    SELECT @Anterior=IdEstado FROM dbo.OrdenesReparacion WITH (UPDLOCK,HOLDLOCK) WHERE IdOrden=@IdOrden;
    SELECT @Nuevo=IdEstado FROM dbo.EstadosReparacion WHERE Nombre=@Nombre;
    IF @Anterior IS NULL OR @Nuevo IS NULL THROW 51002, 'Orden o estado inexistente.', 1;
    UPDATE dbo.OrdenesReparacion SET IdEstado=@Nuevo WHERE IdOrden=@IdOrden;
    INSERT dbo.HistorialEstados(IdOrden,IdEstadoAnterior,IdEstadoNuevo,IdUsuario,Observacion)
    VALUES(@IdOrden,@Anterior,@Nuevo,@IdUsuarioActor,@Observacion);
END;
GO


/* =========================================================================
   [05/51] PROCEDIMIENTO: usp_Interno_RecalcularTotal
   MÓDULO: FINANZAS / AUXILIAR INTERNO
   AUTORIZACIÓN / ROLES: Uso interno tras agregar/modificar/retirar servicios o repuestos
   DESCRIPCIÓN:
   Recalcula automáticamente el importe Total de la orden sumando los servicios registrados y los repuestos consumidos, validando no superar el costo aprobado.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_Interno_RecalcularTotal
    @IdOrden INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Total DECIMAL(10,2), @Aprobado DECIMAL(10,2);
    SELECT @Total=COALESCE((SELECT SUM(CONVERT(DECIMAL(19,2),Cantidad)*Precio) FROM dbo.DetalleServicios WHERE IdOrden=@IdOrden),0)
        + COALESCE((SELECT SUM(CONVERT(DECIMAL(19,2),Cantidad)*Precio) FROM dbo.DetalleRepuestos WHERE IdOrden=@IdOrden),0);
    SELECT @Aprobado=c.CostoAprobado FROM dbo.Diagnosticos d JOIN dbo.ConfirmacionesReparacion c
    ON c.IdDiagnostico=d.IdDiagnostico WHERE d.IdOrden=@IdOrden AND c.Decision='APROBADA';
    IF @Aprobado IS NULL OR @Total>@Aprobado THROW 51003, 'El total excede el costo autorizado o falta aprobacion.', 1;
    IF @Total < COALESCE((SELECT SUM(Monto) FROM dbo.Pagos WHERE IdOrden=@IdOrden AND Anulado=0),0)
        THROW 51003, 'El total no puede quedar por debajo de los pagos vigentes.', 1;
    UPDATE dbo.OrdenesReparacion SET Total=@Total WHERE IdOrden=@IdOrden;
END;
GO


/* =========================================================================
   [06/51] PROCEDIMIENTO: usp_ConsultarInicializacion
   MÓDULO: SEGURIDAD / ARRANQUE
   AUTORIZACIÓN / ROLES: Público / Llamado por UI al iniciar la aplicación
   DESCRIPCIÓN:
   Comprueba si la base de datos ya cuenta con usuarios y si existe al menos un Administrador activo en dbo.Roles. Permite al login saber si mostrar el asistente inicial.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarInicializacion
 @HayUsuarios BIT OUTPUT, @HayAdministradorActivo BIT OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
SELECT @HayUsuarios=CASE WHEN EXISTS(SELECT 1 FROM dbo.Usuarios) THEN 1 ELSE 0 END,
       @HayAdministradorActivo=CASE WHEN EXISTS(
           SELECT 1 FROM dbo.Usuarios u
           INNER JOIN dbo.Roles r ON u.IdRol = r.IdRol
           WHERE r.Nombre='Administrador' AND u.Estado=1 AND r.Estado=1
       ) THEN 1 ELSE 0 END;
END;
GO


/* =========================================================================
   [07/51] PROCEDIMIENTO: usp_CrearAdministradorInicial
   MÓDULO: SEGURIDAD / CONFIGURACIÓN INICIAL
   AUTORIZACIÓN / ROLES: Público (exclusivo para la creación del primer usuario)
   DESCRIPCIÓN:
   Registra al primer usuario Administrador con su hash criptografico PBKDF2 (ContrasenaHash). Solo se permite si no existe ningun usuario en la base de datos.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_CrearAdministradorInicial
 @Nombre VARCHAR(100), @Apellido VARCHAR(100), @Cedula VARCHAR(20), @Telefono VARCHAR(20), @Correo VARCHAR(100), @Usuario VARCHAR(50), @ContrasenaHash VARCHAR(255), @IdUsuario INT OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@Apellido)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@Cedula)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@Usuario)),'') IS NULL THROW 51000,'Complete los datos obligatorios del usuario.',1;
SET @Cedula=LTRIM(RTRIM(@Cedula)); SET @Usuario=LTRIM(RTRIM(@Usuario));
IF NULLIF(@ContrasenaHash,'') IS NULL THROW 51000,'Debe indicar el hash de contrasena.',1;
IF EXISTS(SELECT 1 FROM dbo.Usuarios WITH (UPDLOCK,HOLDLOCK)) THROW 51000,'El administrador inicial solo se crea cuando no hay usuarios.',1;
DECLARE @IdRolAdmin INT = (SELECT IdRol FROM dbo.Roles WHERE Nombre='Administrador' AND Estado=1);
IF @IdRolAdmin IS NULL THROW 51000,'Rol Administrador no configurado en la base de datos.',1;
INSERT dbo.Usuarios(IdRol,Nombre,Apellido,Cedula,Telefono,Correo,Usuario,ContrasenaHash) VALUES(@IdRolAdmin,@Nombre,@Apellido,@Cedula,@Telefono,@Correo,@Usuario,@ContrasenaHash);
SET @IdUsuario=CONVERT(INT,SCOPE_IDENTITY());
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO


/* =========================================================================
   [08/51] PROCEDIMIENTO: usp_BuscarCredencialUsuario
   MÓDULO: SEGURIDAD / AUTENTICACIÓN
   AUTORIZACIÓN / ROLES: Público (proceso de login)
   DESCRIPCIÓN:
   Busca la credencial de un usuario por su nombre de usuario (Usuario) y estado activo, obteniendo su Rol desde dbo.Roles y su ContrasenaHash para el login.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_BuscarCredencialUsuario
 @Usuario VARCHAR(50)
AS
BEGIN
 SET NOCOUNT ON;
SELECT u.IdUsuario, u.IdRol, u.Nombre, u.Apellido, u.Cedula, u.Telefono, u.Correo, u.Usuario, r.Nombre AS Rol, u.Estado, u.FechaRegistro, u.ContrasenaHash, u.ContrasenaHash AS Contrasena
FROM dbo.Usuarios u
INNER JOIN dbo.Roles r ON u.IdRol = r.IdRol
WHERE u.Usuario=@Usuario AND u.Estado=1 AND r.Estado=1;
END;
GO


/* =========================================================================
   [09/51] PROCEDIMIENTO: usp_ConsultarUsuarios
   MÓDULO: SEGURIDAD / GESTIÓN DE USUARIOS
   AUTORIZACIÓN / ROLES: Administrador
   DESCRIPCIÓN:
   Lista los usuarios registrados junto con el nombre de su rol (dbo.Roles), con filtros opcionales por identificador y estado activo.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarUsuarios
 @IdUsuarioActor INT, @IdUsuario INT=NULL, @SoloActivos BIT=1, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.Usuarios u JOIN dbo.Roles r ON r.IdRol=u.IdRol WHERE (@IdUsuario IS NULL OR u.IdUsuario=@IdUsuario) AND (@SoloActivos=0 OR u.Estado=1) AND (@Buscar IS NULL OR u.Nombre LIKE N'%'+@Buscar+N'%' OR u.Apellido LIKE N'%'+@Buscar+N'%' OR u.Usuario LIKE N'%'+@Buscar+N'%' OR u.Cedula LIKE N'%'+@Buscar+N'%' OR u.Correo LIKE N'%'+@Buscar+N'%' OR u.Telefono LIKE N'%'+@Buscar+N'%' OR r.Nombre LIKE N'%'+@Buscar+N'%');
 SELECT u.IdUsuario,u.IdRol,u.Nombre,u.Apellido,u.Cedula,u.Telefono,u.Correo,u.Usuario,r.Nombre AS Rol,u.Estado,u.FechaRegistro FROM dbo.Usuarios u JOIN dbo.Roles r ON r.IdRol=u.IdRol WHERE (@IdUsuario IS NULL OR u.IdUsuario=@IdUsuario) AND (@SoloActivos=0 OR u.Estado=1) AND (@Buscar IS NULL OR u.Nombre LIKE N'%'+@Buscar+N'%' OR u.Apellido LIKE N'%'+@Buscar+N'%' OR u.Usuario LIKE N'%'+@Buscar+N'%' OR u.Cedula LIKE N'%'+@Buscar+N'%' OR u.Correo LIKE N'%'+@Buscar+N'%' OR u.Telefono LIKE N'%'+@Buscar+N'%' OR r.Nombre LIKE N'%'+@Buscar+N'%')
 ORDER BY u.Nombre,u.Apellido,u.IdUsuario OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* =========================================================================
   [10/51] PROCEDIMIENTO: usp_GuardarUsuario
   MÓDULO: SEGURIDAD / GESTIÓN DE USUARIOS
   AUTORIZACIÓN / ROLES: Administrador
   DESCRIPCIÓN:
   Crea o actualiza usuarios del sistema. Mapea el rol con dbo.Roles, actualiza ContrasenaHash y protege que siempre se conserve al menos un Administrador activo.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_GuardarUsuario
 @IdUsuarioActor INT, @IdUsuario INT OUTPUT, @Nombre VARCHAR(100), @Apellido VARCHAR(100), @Cedula VARCHAR(20), @Telefono VARCHAR(20), @Correo VARCHAR(100), @Usuario VARCHAR(50), @ContrasenaHash VARCHAR(255), @Rol VARCHAR(50)=NULL, @Estado BIT=1, @IdRol INT=NULL
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@Apellido)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@Cedula)),'') IS NULL OR NULLIF(LTRIM(RTRIM(@Usuario)),'') IS NULL THROW 51000,'Complete los datos obligatorios del usuario.',1;
SET @Cedula=LTRIM(RTRIM(@Cedula)); SET @Usuario=LTRIM(RTRIM(@Usuario));

IF @IdUsuario IS NULL OR @IdUsuario<0 THROW 51000,'Identificador invalido.',1;
DECLARE @IdRolParam INT;
SELECT @IdRolParam=IdRol FROM dbo.Roles WITH(UPDLOCK,HOLDLOCK)
 WHERE Estado=1 AND ((@IdRol IS NOT NULL AND IdRol=@IdRol AND (@Rol IS NULL OR Nombre=@Rol))
 OR (@IdRol IS NULL AND Nombre=@Rol));
IF @IdRolParam IS NULL THROW 51000,'Rol invalido.',1;
DECLARE @IdRolAdmin INT = (SELECT IdRol FROM dbo.Roles WHERE Nombre='Administrador' AND Estado=1);

IF @IdUsuario=0
BEGIN
 IF NULLIF(@ContrasenaHash,'') IS NULL THROW 51000,'Una nueva cuenta requiere contrasena.',1;
 INSERT dbo.Usuarios(IdRol,Nombre,Apellido,Cedula,Telefono,Correo,Usuario,ContrasenaHash,Estado) VALUES(@IdRolParam,@Nombre,@Apellido,@Cedula,@Telefono,@Correo,@Usuario,@ContrasenaHash,@Estado);
 SET @IdUsuario=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 DECLARE @Administradores INT;
 SELECT @Administradores=COUNT(*) FROM dbo.Usuarios WITH(UPDLOCK,HOLDLOCK) WHERE IdRol=@IdRolAdmin AND Estado=1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Usuarios WHERE IdUsuario=@IdUsuario) THROW 51000,'Usuario inexistente.',1;
 IF EXISTS(SELECT 1 FROM dbo.Usuarios WHERE IdUsuario=@IdUsuario AND IdRol=@IdRolAdmin AND Estado=1)
    AND (@IdRolParam<>@IdRolAdmin OR @Estado=0) AND @Administradores<=1 THROW 51000,'Debe conservar un administrador activo.',1;
 IF @IdUsuario=@IdUsuarioActor AND (@Estado=0 OR @IdRolParam<>@IdRolAdmin) THROW 51000,'No puede desactivar o cambiar el rol de su propia sesion.',1;
 UPDATE dbo.Usuarios SET IdRol=@IdRolParam,Nombre=@Nombre,Apellido=@Apellido,Cedula=@Cedula,Telefono=@Telefono,Correo=@Correo,Usuario=@Usuario,
    ContrasenaHash=COALESCE(@ContrasenaHash,ContrasenaHash),Estado=@Estado WHERE IdUsuario=@IdUsuario;
END

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO


/* =========================================================================
   [11/51] PROCEDIMIENTO: usp_GuardarCliente
   MÓDULO: CATÁLOGOS / CLIENTES
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista
   DESCRIPCIÓN:
   Crea o actualiza un cliente. Valida que la cédula sea obligatoria, no vacía y única en el sistema.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_GuardarCliente
 @IdUsuarioActor INT, @IdCliente INT OUTPUT, @Nombre VARCHAR(100), @Apellido VARCHAR(100), @Telefono VARCHAR(20), @Cedula VARCHAR(20), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista';
IF @IdCliente IS NULL OR @IdCliente<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL THROW 51000,'El campo Nombre es obligatorio.',1;
SET @Nombre=LTRIM(RTRIM(@Nombre));
IF NULLIF(LTRIM(RTRIM(@Apellido)),'') IS NULL THROW 51000,'El campo Apellido es obligatorio.',1;
SET @Apellido=LTRIM(RTRIM(@Apellido));
IF NULLIF(LTRIM(RTRIM(@Telefono)),'') IS NULL THROW 51000,'El campo Telefono es obligatorio.',1;
SET @Telefono=LTRIM(RTRIM(@Telefono));
IF NULLIF(LTRIM(RTRIM(@Cedula)),'') IS NULL THROW 51000,'El campo Cedula es obligatorio.',1;
SET @Cedula=LTRIM(RTRIM(@Cedula));
IF @IdCliente=0
BEGIN
 INSERT dbo.Clientes(Nombre,Apellido,Telefono,Cedula,Estado) VALUES(@Nombre,@Apellido,@Telefono,@Cedula,@Estado);
 SET @IdCliente=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.Clientes WITH(UPDLOCK,HOLDLOCK) WHERE IdCliente=@IdCliente) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.Clientes SET Nombre=@Nombre,Apellido=@Apellido,Telefono=@Telefono,Cedula=@Cedula,Estado=@Estado WHERE IdCliente=@IdCliente;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO


/* =========================================================================
   [12/51] PROCEDIMIENTO: usp_ConsultarClientes
   MÓDULO: CATÁLOGOS / CLIENTES
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista, Tecnico
   DESCRIPCIÓN:
   Consulta el catálogo de clientes activos o busca un cliente específico por su IdCliente.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarClientes
 @IdUsuarioActor INT, @IdCliente INT=NULL, @SoloActivos BIT=1, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.Clientes WHERE (@IdCliente IS NULL OR IdCliente=@IdCliente) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Apellido LIKE N'%'+@Buscar+N'%' OR Cedula LIKE N'%'+@Buscar+N'%' OR Telefono LIKE N'%'+@Buscar+N'%');
 SELECT IdCliente,Nombre,Apellido,Telefono,Cedula,Estado FROM dbo.Clientes WHERE (@IdCliente IS NULL OR IdCliente=@IdCliente) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Apellido LIKE N'%'+@Buscar+N'%' OR Cedula LIKE N'%'+@Buscar+N'%' OR Telefono LIKE N'%'+@Buscar+N'%')
 ORDER BY IdCliente OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* =========================================================================
   [13/51] PROCEDIMIENTO: usp_GuardarTipoEquipo
   MÓDULO: CATÁLOGOS / TIPOS DE EQUIPO
   AUTORIZACIÓN / ROLES: Administrador
   DESCRIPCIÓN:
   Registra o actualiza categorias de equipos (ej. Laptops, Smartphones, Tablets).
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_GuardarTipoEquipo
 @IdUsuarioActor INT, @IdTipoEquipo INT OUTPUT, @Nombre VARCHAR(50), @Descripcion VARCHAR(200), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
IF @IdTipoEquipo IS NULL OR @IdTipoEquipo<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL THROW 51000,'El campo Nombre es obligatorio.',1;
SET @Nombre=LTRIM(RTRIM(@Nombre));
IF @IdTipoEquipo=0
BEGIN
 INSERT dbo.TiposEquipo(Nombre,Descripcion,Estado) VALUES(@Nombre,@Descripcion,@Estado);
 SET @IdTipoEquipo=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.TiposEquipo WITH(UPDLOCK,HOLDLOCK) WHERE IdTipoEquipo=@IdTipoEquipo) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.TiposEquipo SET Nombre=@Nombre,Descripcion=@Descripcion,Estado=@Estado WHERE IdTipoEquipo=@IdTipoEquipo;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO


/* =========================================================================
   [14/51] PROCEDIMIENTO: usp_ConsultarTiposEquipo
   MÓDULO: CATÁLOGOS / TIPOS DE EQUIPO
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista, Tecnico
   DESCRIPCIÓN:
   Consulta las categorias de equipos disponibles en el taller.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarTiposEquipo
 @IdUsuarioActor INT, @IdTipoEquipo INT=NULL, @SoloActivos BIT=1, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.TiposEquipo WHERE (@IdTipoEquipo IS NULL OR IdTipoEquipo=@IdTipoEquipo) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Descripcion LIKE N'%'+@Buscar+N'%');
 SELECT IdTipoEquipo,Nombre,Descripcion,Estado FROM dbo.TiposEquipo WHERE (@IdTipoEquipo IS NULL OR IdTipoEquipo=@IdTipoEquipo) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Descripcion LIKE N'%'+@Buscar+N'%')
 ORDER BY IdTipoEquipo OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* =========================================================================
   [15/51] PROCEDIMIENTO: usp_GuardarEquipo
   MÓDULO: CATÁLOGOS / EQUIPOS
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista
   DESCRIPCIÓN:
   Crea o edita la ficha de un equipo vinculado a un cliente y a un tipo de equipo (marca, modelo, serie, color).
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_GuardarEquipo
 @IdUsuarioActor INT, @IdEquipo INT OUTPUT, @IdCliente INT, @IdTipoEquipo INT, @Marca VARCHAR(50), @Modelo VARCHAR(100), @NumeroSerie VARCHAR(100), @Color VARCHAR(50), @Observaciones VARCHAR(500), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista';
IF @IdEquipo IS NULL OR @IdEquipo<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Marca)),'') IS NULL THROW 51000,'El campo Marca es obligatorio.',1;
SET @Marca=LTRIM(RTRIM(@Marca));
IF NOT EXISTS(SELECT 1 FROM dbo.Clientes WHERE IdCliente=@IdCliente AND Estado=1) OR NOT EXISTS(SELECT 1 FROM dbo.TiposEquipo WHERE IdTipoEquipo=@IdTipoEquipo AND Estado=1) THROW 51000,'Cliente o tipo de equipo inactivo o inexistente.',1;
IF EXISTS(SELECT 1 FROM dbo.Equipos e WHERE e.IdEquipo=@IdEquipo AND e.IdCliente<>@IdCliente) AND EXISTS(SELECT 1 FROM dbo.OrdenesReparacion WHERE IdEquipo=@IdEquipo) THROW 51000,'No puede cambiar el propietario de un equipo con ordenes registradas.',1;
IF @IdEquipo=0
BEGIN
 INSERT dbo.Equipos(IdCliente,IdTipoEquipo,Marca,Modelo,NumeroSerie,Color,Observaciones,Estado) VALUES(@IdCliente,@IdTipoEquipo,@Marca,@Modelo,@NumeroSerie,@Color,@Observaciones,@Estado);
 SET @IdEquipo=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.Equipos WITH(UPDLOCK,HOLDLOCK) WHERE IdEquipo=@IdEquipo) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.Equipos SET IdCliente=@IdCliente,IdTipoEquipo=@IdTipoEquipo,Marca=@Marca,Modelo=@Modelo,NumeroSerie=@NumeroSerie,Color=@Color,Observaciones=@Observaciones,Estado=@Estado WHERE IdEquipo=@IdEquipo;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO


/* =========================================================================
   [16/51] PROCEDIMIENTO: usp_ConsultarEquipos
   MÓDULO: CATÁLOGOS / EQUIPOS
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista, Tecnico
   DESCRIPCIÓN:
   Consulta equipos registrados por cliente o por identificador de equipo.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarEquipos
 @IdUsuarioActor INT, @IdEquipo INT=NULL, @SoloActivos BIT=1, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.Equipos WHERE (@IdEquipo IS NULL OR IdEquipo=@IdEquipo) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Marca LIKE N'%'+@Buscar+N'%' OR Modelo LIKE N'%'+@Buscar+N'%' OR NumeroSerie LIKE N'%'+@Buscar+N'%' OR Color LIKE N'%'+@Buscar+N'%');
 SELECT IdEquipo,IdCliente,IdTipoEquipo,Marca,Modelo,NumeroSerie,Color,Observaciones,Estado,FechaRegistro FROM dbo.Equipos WHERE (@IdEquipo IS NULL OR IdEquipo=@IdEquipo) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Marca LIKE N'%'+@Buscar+N'%' OR Modelo LIKE N'%'+@Buscar+N'%' OR NumeroSerie LIKE N'%'+@Buscar+N'%' OR Color LIKE N'%'+@Buscar+N'%')
 ORDER BY IdEquipo OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* =========================================================================
   [17/51] PROCEDIMIENTO: usp_ConsultarEstadosReparacion
   MÓDULO: CATÁLOGOS / ESTADOS DE REPARACIÓN
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista, Tecnico
   DESCRIPCIÓN:
   Lista el catálogo de los 8 estados oficiales del flujo de trabajo en el taller.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarEstadosReparacion
 @IdUsuarioActor INT
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT IdEstado,Nombre,Descripcion FROM dbo.EstadosReparacion ORDER BY IdEstado;
END;
GO


/* =========================================================================
   [18/51] PROCEDIMIENTO: usp_GuardarServicio
   MÓDULO: CATÁLOGOS / SERVICIOS DE MANO DE OBRA
   AUTORIZACIÓN / ROLES: Administrador
   DESCRIPCIÓN:
   Crea o actualiza servicios ofrecidos por el taller con su descripción y precio base sugerido.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_GuardarServicio
 @IdUsuarioActor INT, @IdServicio INT OUTPUT, @Nombre VARCHAR(100), @Descripcion VARCHAR(300), @PrecioBase DECIMAL(10,2), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
IF @IdServicio IS NULL OR @IdServicio<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL THROW 51000,'El campo Nombre es obligatorio.',1;
SET @Nombre=LTRIM(RTRIM(@Nombre));
IF @IdServicio=0
BEGIN
 INSERT dbo.Servicios(Nombre,Descripcion,PrecioBase,Estado) VALUES(@Nombre,@Descripcion,@PrecioBase,@Estado);
 SET @IdServicio=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.Servicios WITH(UPDLOCK,HOLDLOCK) WHERE IdServicio=@IdServicio) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.Servicios SET Nombre=@Nombre,Descripcion=@Descripcion,PrecioBase=@PrecioBase,Estado=@Estado WHERE IdServicio=@IdServicio;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO


/* =========================================================================
   [19/51] PROCEDIMIENTO: usp_ConsultarServicios
   MÓDULO: CATÁLOGOS / SERVICIOS DE MANO DE OBRA
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista, Tecnico
   DESCRIPCIÓN:
   Consulta el catálogo de servicios de mano de obra disponibles.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarServicios
 @IdUsuarioActor INT, @IdServicio INT=NULL, @SoloActivos BIT=1, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.Servicios WHERE (@IdServicio IS NULL OR IdServicio=@IdServicio) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Descripcion LIKE N'%'+@Buscar+N'%');
 SELECT IdServicio,Nombre,Descripcion,PrecioBase,Estado FROM dbo.Servicios WHERE (@IdServicio IS NULL OR IdServicio=@IdServicio) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Descripcion LIKE N'%'+@Buscar+N'%')
 ORDER BY IdServicio OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* =========================================================================
   [20/51] PROCEDIMIENTO: usp_GuardarRepuesto
   MÓDULO: INVENTARIO / CATÁLOGO DE REPUESTOS
   AUTORIZACIÓN / ROLES: Administrador
   DESCRIPCIÓN:
   Crea o edita repuestos en el catálogo maestro con precio de compra y precio de venta. No modifica stock directamente.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_GuardarRepuesto
 @IdUsuarioActor INT, @IdRepuesto INT OUTPUT, @Nombre VARCHAR(100), @Descripcion VARCHAR(300), @Marca VARCHAR(50), @NumeroParte VARCHAR(100), @PrecioCompra DECIMAL(10,2), @PrecioVenta DECIMAL(10,2), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
IF @IdRepuesto IS NULL OR @IdRepuesto<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL THROW 51000,'El campo Nombre es obligatorio.',1;
SET @Nombre=LTRIM(RTRIM(@Nombre));
IF @IdRepuesto=0
BEGIN
 INSERT dbo.Repuestos(Nombre,Descripcion,Marca,NumeroParte,PrecioCompra,PrecioVenta,Estado) VALUES(@Nombre,@Descripcion,@Marca,@NumeroParte,@PrecioCompra,@PrecioVenta,@Estado);
 SET @IdRepuesto=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.Repuestos WITH(UPDLOCK,HOLDLOCK) WHERE IdRepuesto=@IdRepuesto) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.Repuestos SET Nombre=@Nombre,Descripcion=@Descripcion,Marca=@Marca,NumeroParte=@NumeroParte,PrecioCompra=@PrecioCompra,PrecioVenta=@PrecioVenta,Estado=@Estado WHERE IdRepuesto=@IdRepuesto;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO


/* =========================================================================
   [21/51] PROCEDIMIENTO: usp_ConsultarRepuestos
   MÓDULO: INVENTARIO / CATÁLOGO DE REPUESTOS
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista, Tecnico
   DESCRIPCIÓN:
   Consulta repuestos disponibles en almacén, sus niveles de existencias (Stock) y precios.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarRepuestos
 @IdUsuarioActor INT, @IdRepuesto INT=NULL, @SoloActivos BIT=1, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.Repuestos WHERE (@IdRepuesto IS NULL OR IdRepuesto=@IdRepuesto) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Marca LIKE N'%'+@Buscar+N'%' OR NumeroParte LIKE N'%'+@Buscar+N'%' OR Descripcion LIKE N'%'+@Buscar+N'%');
 SELECT IdRepuesto,Nombre,Descripcion,Marca,NumeroParte,PrecioCompra,PrecioVenta,Estado,Stock FROM dbo.Repuestos WHERE (@IdRepuesto IS NULL OR IdRepuesto=@IdRepuesto) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Marca LIKE N'%'+@Buscar+N'%' OR NumeroParte LIKE N'%'+@Buscar+N'%' OR Descripcion LIKE N'%'+@Buscar+N'%')
 ORDER BY IdRepuesto OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* =========================================================================
   [22/51] PROCEDIMIENTO: usp_AjustarExistencias
   MÓDULO: INVENTARIO / AJUSTES DE STOCK
   AUTORIZACIÓN / ROLES: Administrador
   DESCRIPCIÓN:
   Permite al administrador ajustar manualmente el stock de un repuesto (por compras, mermas o inventario físico), impidiendo valores negativos.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_AjustarExistencias
    @IdUsuarioActor INT, @IdRepuesto INT, @Variacion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Administrador';

    IF @Variacion IS NULL OR @Variacion=0 THROW 51000, 'Indique una variacion distinta de cero.', 1;
    UPDATE dbo.Repuestos WITH (UPDLOCK) SET Stock=Stock+@Variacion WHERE IdRepuesto=@IdRepuesto AND Estado=1 AND CONVERT(BIGINT,Stock)+@Variacion BETWEEN 0 AND 2147483647;
    IF @@ROWCOUNT=0 THROW 51000, 'Repuesto inactivo, inexistente o ajuste fuera del rango de stock.', 1;

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [23/51] PROCEDIMIENTO: usp_RegistrarRecepcion
   MÓDULO: FLUJO / RECEPCIÓN
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Registra el ingreso de un equipo al taller, genera la orden con código único en estado 'En Revisión' y crea el historial inicial.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RegistrarRecepcion
    @IdUsuarioActor INT, @NumeroOrden VARCHAR(20), @IdEquipo INT, @ProblemaReportado VARCHAR(500), @ObservacionesRecepcion VARCHAR(500), @AccesoriosRecepcion VARCHAR(300), @IdOrden INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    IF NULLIF(LTRIM(RTRIM(@NumeroOrden)), '') IS NULL THROW 51000, 'El campo NumeroOrden es obligatorio.', 1;
    SET @NumeroOrden=LTRIM(RTRIM(@NumeroOrden));
    IF NULLIF(LTRIM(RTRIM(@ProblemaReportado)), '') IS NULL THROW 51000, 'El campo ProblemaReportado es obligatorio.', 1;
    SET @ProblemaReportado=LTRIM(RTRIM(@ProblemaReportado));

    IF NOT EXISTS(SELECT 1 FROM dbo.Equipos e JOIN dbo.Clientes c ON c.IdCliente=e.IdCliente WHERE e.IdEquipo=@IdEquipo AND e.Estado=1 AND c.Estado=1) THROW 51000, 'Equipo o cliente inexistente o inactivo.', 1;
    DECLARE @Estado INT=(SELECT IdEstado FROM dbo.EstadosReparacion WHERE Nombre='En Revisión');
    INSERT dbo.OrdenesReparacion(NumeroOrden,IdEquipo,IdRecepcionista,IdEstado,ProblemaReportado,ObservacionesRecepcion,AccesoriosRecepcion)
    VALUES(@NumeroOrden,@IdEquipo,@IdUsuarioActor,@Estado,@ProblemaReportado,@ObservacionesRecepcion,@AccesoriosRecepcion);
    SET @IdOrden=CONVERT(INT,SCOPE_IDENTITY());
    INSERT dbo.HistorialEstados(IdOrden,IdEstadoAnterior,IdEstadoNuevo,IdUsuario,Observacion)
    VALUES(@IdOrden,NULL,@Estado,@IdUsuarioActor,'Recepcion del equipo');

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [24/51] PROCEDIMIENTO: usp_CorregirRecepcion
   MÓDULO: FLUJO / RECEPCIÓN
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Permite corregir problema reportado, accesorios u observaciones de recepción antes de que un técnico tome la orden.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_CorregirRecepcion
    @IdUsuarioActor INT, @IdOrden INT, @ProblemaReportado VARCHAR(500), @ObservacionesRecepcion VARCHAR(500), @AccesoriosRecepcion VARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF NULLIF(LTRIM(RTRIM(@ProblemaReportado)), '') IS NULL THROW 51000, 'El campo ProblemaReportado es obligatorio.', 1;
    SET @ProblemaReportado=LTRIM(RTRIM(@ProblemaReportado));

    IF @EstadoActual<>'En Revisión' OR @Tecnico IS NOT NULL THROW 51002, 'Solo puede corregir la recepcion antes de que un tecnico tome la orden.', 1;
    UPDATE dbo.OrdenesReparacion SET ProblemaReportado=@ProblemaReportado,ObservacionesRecepcion=@ObservacionesRecepcion,AccesoriosRecepcion=@AccesoriosRecepcion WHERE IdOrden=@IdOrden;

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [25/51] PROCEDIMIENTO: usp_AnularOrden
   MÓDULO: FLUJO / RECEPCIÓN
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Anula una orden recibida por equivocación, siempre que esté 'En Revisión' y no haya sido tomada por ningún técnico.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_AnularOrden
    @IdUsuarioActor INT, @IdOrden INT, @Motivo VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF NULLIF(LTRIM(RTRIM(@Motivo)), '') IS NULL THROW 51000, 'El campo Motivo es obligatorio.', 1;
    SET @Motivo=LTRIM(RTRIM(@Motivo));

    IF @EstadoActual<>'En Revisión' OR @Tecnico IS NOT NULL OR EXISTS(SELECT 1 FROM dbo.Diagnosticos WHERE IdOrden=@IdOrden) THROW 51002, 'Solo se anulan ordenes sin trabajo iniciado.', 1;
    UPDATE dbo.OrdenesReparacion SET Anulada=1 WHERE IdOrden=@IdOrden;
    DECLARE @Nota VARCHAR(500)=LEFT('Anulacion: '+@Motivo,500);
    EXEC dbo.usp_Interno_Estado @IdOrden,@EstadoActual,@IdUsuarioActor,@Nota;

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [26/51] PROCEDIMIENTO: usp_TomarOrden
   MÓDULO: FLUJO / ASIGNACIÓN TÉCNICA
   AUTORIZACIÓN / ROLES: Tecnico
   DESCRIPCIÓN:
   El técnico toma una orden disponible 'En Revisión' y queda asignado como técnico responsable único (sin colaboración múltiple).
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_TomarOrden
    @IdUsuarioActor INT, @IdOrden INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;

    IF @EstadoActual<>'En Revisión' OR @Tecnico IS NOT NULL THROW 51002, 'La orden ya fue tomada o no esta en revision.', 1;
    UPDATE dbo.OrdenesReparacion SET IdTecnicoResponsable=@IdUsuarioActor WHERE IdOrden=@IdOrden;
    EXEC dbo.usp_Interno_Estado @IdOrden,@EstadoActual,@IdUsuarioActor,'El tecnico toma la orden';

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [27/51] PROCEDIMIENTO: usp_LiberarOrden
   MÓDULO: FLUJO / ASIGNACIÓN TÉCNICA
   AUTORIZACIÓN / ROLES: Tecnico
   DESCRIPCIÓN:
   Permite al técnico responsable liberar la orden si no puede atenderla, devolviéndola al estado 'En Revisión' sin técnico asignado.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_LiberarOrden
    @IdUsuarioActor INT, @IdOrden INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;

    IF @EstadoActual<>'En Revisión' OR EXISTS(SELECT 1 FROM dbo.Diagnosticos WHERE IdOrden=@IdOrden) THROW 51002, 'Solo puede liberar una orden antes de registrar el diagnostico.', 1;
    UPDATE dbo.OrdenesReparacion SET IdTecnicoResponsable=NULL WHERE IdOrden=@IdOrden;
    EXEC dbo.usp_Interno_Estado @IdOrden,@EstadoActual,@IdUsuarioActor,'El tecnico libera la orden';

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [28/51] PROCEDIMIENTO: usp_RegistrarDiagnostico
   MÓDULO: FLUJO / DIAGNÓSTICO
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   El técnico responsable registra el diagnóstico técnico, solución propuesta y costo estimado, pasando la orden a 'Pendiente de Confirmación'.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RegistrarDiagnostico
    @IdUsuarioActor INT, @IdOrden INT, @ProblemaEncontrado VARCHAR(1000), @ReparacionPropuesta VARCHAR(1000), @CostoEstimado DECIMAL(10,2), @Observaciones VARCHAR(1000), @IdDiagnostico INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF NULLIF(LTRIM(RTRIM(@ProblemaEncontrado)), '') IS NULL THROW 51000, 'El campo ProblemaEncontrado es obligatorio.', 1;
    SET @ProblemaEncontrado=LTRIM(RTRIM(@ProblemaEncontrado));
    IF NULLIF(LTRIM(RTRIM(@ReparacionPropuesta)), '') IS NULL THROW 51000, 'El campo ReparacionPropuesta es obligatorio.', 1;
    SET @ReparacionPropuesta=LTRIM(RTRIM(@ReparacionPropuesta));

    IF @EstadoActual<>'En Revisión' THROW 51002, 'La orden debe estar en revision.', 1;
    INSERT dbo.Diagnosticos(IdOrden,IdTecnico,ProblemaEncontrado,ReparacionPropuesta,CostoEstimado,Observaciones)
    VALUES(@IdOrden,@IdUsuarioActor,@ProblemaEncontrado,@ReparacionPropuesta,@CostoEstimado,@Observaciones);
    SET @IdDiagnostico=CONVERT(INT,SCOPE_IDENTITY());
    EXEC dbo.usp_Interno_Estado @IdOrden,'Pendiente de Confirmación',@IdUsuarioActor,'Diagnostico registrado';

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [29/51] PROCEDIMIENTO: usp_CorregirDiagnostico
   MÓDULO: FLUJO / DIAGNÓSTICO
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Permite corregir el diagnóstico y presupuesto técnico antes de que el cliente registre su decisión.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_CorregirDiagnostico
    @IdUsuarioActor INT, @IdDiagnostico INT, @ProblemaEncontrado VARCHAR(1000), @ReparacionPropuesta VARCHAR(1000), @CostoEstimado DECIMAL(10,2), @Observaciones VARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.Diagnosticos WHERE IdDiagnostico=@IdDiagnostico);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.Diagnosticos WITH (UPDLOCK,HOLDLOCK) WHERE IdDiagnostico=@IdDiagnostico) THROW 51000, 'No existe el registro de Diagnosticos.', 1;
    IF @EstadoActual<>'Pendiente de Confirmación' OR EXISTS(SELECT 1 FROM dbo.ConfirmacionesReparacion WHERE IdDiagnostico=@IdDiagnostico) THROW 51002, 'El diagnostico ya tiene decision del cliente.', 1;
    IF NULLIF(LTRIM(RTRIM(@ProblemaEncontrado)), '') IS NULL THROW 51000, 'El campo ProblemaEncontrado es obligatorio.', 1;
    SET @ProblemaEncontrado=LTRIM(RTRIM(@ProblemaEncontrado));
    IF NULLIF(LTRIM(RTRIM(@ReparacionPropuesta)), '') IS NULL THROW 51000, 'El campo ReparacionPropuesta es obligatorio.', 1;
    SET @ReparacionPropuesta=LTRIM(RTRIM(@ReparacionPropuesta));
    UPDATE dbo.Diagnosticos SET ProblemaEncontrado=@ProblemaEncontrado,ReparacionPropuesta=@ReparacionPropuesta,CostoEstimado=@CostoEstimado,Observaciones=@Observaciones WHERE IdDiagnostico=@IdDiagnostico;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [30/51] PROCEDIMIENTO: usp_RetirarDiagnostico
   MÓDULO: FLUJO / DIAGNÓSTICO
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Retira el diagnóstico en caso de error técnico antes de la decisión del cliente, regresando la orden a 'En Revisión'.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RetirarDiagnostico
    @IdUsuarioActor INT, @IdDiagnostico INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.Diagnosticos WHERE IdDiagnostico=@IdDiagnostico);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.Diagnosticos WITH (UPDLOCK,HOLDLOCK) WHERE IdDiagnostico=@IdDiagnostico) THROW 51000, 'No existe el registro de Diagnosticos.', 1;
    IF @EstadoActual<>'Pendiente de Confirmación' OR EXISTS(SELECT 1 FROM dbo.ConfirmacionesReparacion WHERE IdDiagnostico=@IdDiagnostico) THROW 51002, 'El diagnostico ya tiene decision del cliente.', 1;
    DELETE dbo.Diagnosticos WHERE IdDiagnostico=@IdDiagnostico;
    EXEC dbo.usp_Interno_Estado @IdOrden,'En Revisión',@IdUsuarioActor,'Diagnostico retirado para correccion';
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [31/51] PROCEDIMIENTO: usp_RegistrarDecisionCliente
   MÓDULO: FLUJO / DECISIÓN DEL CLIENTE
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Registra la decisión del cliente: si APRUEBA (con costo autorizado), pasa a 'Aprobada'; si RECHAZA, pasa a 'Rechazada'.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RegistrarDecisionCliente
    @IdUsuarioActor INT, @IdDiagnostico INT, @Decision VARCHAR(20), @CostoAprobado DECIMAL(10,2), @Observaciones VARCHAR(500), @IdConfirmacion INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.Diagnosticos WHERE IdDiagnostico=@IdDiagnostico);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;

    IF @Decision IS NULL OR @Decision NOT IN ('APROBADA','RECHAZADA') THROW 51000, 'Decision invalida.', 1;
    IF @Decision='APROBADA' AND (@CostoAprobado IS NULL OR @CostoAprobado<0) THROW 51000, 'Indique el costo autorizado por el cliente.', 1;
    IF @Decision='RECHAZADA' AND @CostoAprobado IS NOT NULL THROW 51000, 'Una decision rechazada no tiene costo aprobado.', 1;

    IF @EstadoActual<>'Pendiente de Confirmación' THROW 51002, 'La orden no espera confirmacion.', 1;
    INSERT dbo.ConfirmacionesReparacion(IdDiagnostico,IdUsuario,Decision,CostoAprobado,Observaciones)
    VALUES(@IdDiagnostico,@IdUsuarioActor,@Decision,@CostoAprobado,@Observaciones);
    SET @IdConfirmacion=CONVERT(INT,SCOPE_IDENTITY());
    DECLARE @Nuevo VARCHAR(50)=CASE WHEN @Decision='APROBADA' THEN 'Aprobada' ELSE 'Rechazada' END;
    EXEC dbo.usp_Interno_Estado @IdOrden,@Nuevo,@IdUsuarioActor,'Decision del cliente registrada';
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [32/51] PROCEDIMIENTO: usp_CorregirDecisionCliente
   MÓDULO: FLUJO / DECISIÓN DEL CLIENTE
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Permite corregir la decisión del cliente si hubo un error de captura antes de iniciar la reparación o entregar el equipo.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_CorregirDecisionCliente
    @IdUsuarioActor INT, @IdConfirmacion INT, @Decision VARCHAR(20), @CostoAprobado DECIMAL(10,2), @Observaciones VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @IdOrden INT=(SELECT d.IdOrden FROM dbo.ConfirmacionesReparacion c JOIN dbo.Diagnosticos d ON d.IdDiagnostico=c.IdDiagnostico WHERE c.IdConfirmacion=@IdConfirmacion);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF NOT EXISTS (SELECT 1 FROM dbo.ConfirmacionesReparacion WITH (UPDLOCK,HOLDLOCK) WHERE IdConfirmacion=@IdConfirmacion) THROW 51000, 'No existe el registro de ConfirmacionesReparacion.', 1;
    IF @EstadoActual NOT IN ('Aprobada','Rechazada') THROW 51002, 'La decision solo puede corregirse antes de iniciar la reparacion o entregar.', 1;

    IF @Decision IS NULL OR @Decision NOT IN ('APROBADA','RECHAZADA') THROW 51000, 'Decision invalida.', 1;
    IF @Decision='APROBADA' AND (@CostoAprobado IS NULL OR @CostoAprobado<0) THROW 51000, 'Indique el costo autorizado por el cliente.', 1;
    IF @Decision='RECHAZADA' AND @CostoAprobado IS NOT NULL THROW 51000, 'Una decision rechazada no tiene costo aprobado.', 1;
    UPDATE dbo.ConfirmacionesReparacion SET Decision=@Decision,CostoAprobado=@CostoAprobado,Observaciones=@Observaciones,IdUsuario=@IdUsuarioActor,FechaConfirmacion=GETDATE() WHERE IdConfirmacion=@IdConfirmacion;
    DECLARE @Nuevo VARCHAR(50)=CASE WHEN @Decision='APROBADA' THEN 'Aprobada' ELSE 'Rechazada' END;
    EXEC dbo.usp_Interno_Estado @IdOrden,@Nuevo,@IdUsuarioActor,'Decision del cliente registrada';
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [33/51] PROCEDIMIENTO: usp_RetirarDecisionCliente
   MÓDULO: FLUJO / DECISIÓN DEL CLIENTE
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Revierte la decisión del cliente regresando la orden al estado 'Pendiente de Confirmación'.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RetirarDecisionCliente
    @IdUsuarioActor INT, @IdConfirmacion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @IdOrden INT=(SELECT d.IdOrden FROM dbo.ConfirmacionesReparacion c JOIN dbo.Diagnosticos d ON d.IdDiagnostico=c.IdDiagnostico WHERE c.IdConfirmacion=@IdConfirmacion);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF NOT EXISTS (SELECT 1 FROM dbo.ConfirmacionesReparacion WITH (UPDLOCK,HOLDLOCK) WHERE IdConfirmacion=@IdConfirmacion) THROW 51000, 'No existe el registro de ConfirmacionesReparacion.', 1;
    IF @EstadoActual NOT IN ('Aprobada','Rechazada') THROW 51002, 'La decision solo puede corregirse antes de iniciar la reparacion o entregar.', 1;
    DELETE dbo.ConfirmacionesReparacion WHERE IdConfirmacion=@IdConfirmacion;
    EXEC dbo.usp_Interno_Estado @IdOrden,'Pendiente de Confirmación',@IdUsuarioActor,'Decision retirada para correccion';
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [34/51] PROCEDIMIENTO: usp_IniciarReparacion
   MÓDULO: FLUJO / REPARACIÓN
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Inicia los trabajos técnicos sobre una orden 'Aprobada', colocándola en estado 'En Reparación'.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_IniciarReparacion
    @IdUsuarioActor INT, @IdOrden INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;

    IF @EstadoActual<>'Aprobada' OR NOT EXISTS(SELECT 1 FROM dbo.Diagnosticos d JOIN dbo.ConfirmacionesReparacion c ON c.IdDiagnostico=d.IdDiagnostico WHERE d.IdOrden=@IdOrden AND c.Decision='APROBADA') THROW 51002, 'Hace falta diagnostico y aprobacion del cliente.', 1;
    EXEC dbo.usp_Interno_Estado @IdOrden,'En Reparación',@IdUsuarioActor,'Inicio de reparacion';

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [35/51] PROCEDIMIENTO: usp_RegistrarServicio
   MÓDULO: FLUJO / REPARACIÓN (SERVICIOS)
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Registra un servicio de mano de obra en la orden (normalizado en 3FN sin IdTecnico redundante) y recalcula el total.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RegistrarServicio
    @IdUsuarioActor INT, @IdOrden INT, @IdServicio INT, @Cantidad INT, @Precio DECIMAL(10,2), @Observaciones VARCHAR(500), @IdDetalleServicio INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede modificar detalles durante la reparacion.', 1;
    IF @Cantidad IS NULL OR @Cantidad<=0 OR @Precio IS NULL OR @Precio<0 THROW 51000, 'Cantidad o precio invalido.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Servicios WHERE IdServicio=@IdServicio AND Estado=1) THROW 51000, 'Servicio o repuesto inexistente o inactivo.', 1;
    INSERT dbo.DetalleServicios(IdOrden,IdServicio,Cantidad,Precio,Observaciones) VALUES(@IdOrden,@IdServicio,@Cantidad,@Precio,@Observaciones);
    SET @IdDetalleServicio=CONVERT(INT,SCOPE_IDENTITY());
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [36/51] PROCEDIMIENTO: usp_CorregirServicio
   MÓDULO: FLUJO / REPARACIÓN (SERVICIOS)
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Modifica cantidad, precio u observaciones de un servicio aplicado y recalcula el total acumulado de la orden.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_CorregirServicio
    @IdUsuarioActor INT, @IdDetalleServicio INT, @Cantidad INT, @Precio DECIMAL(10,2), @Observaciones VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede modificar detalles durante la reparacion.', 1;
    DECLARE @IdServicio INT, @Anterior INT;
    SELECT @IdServicio=IdServicio,@Anterior=Cantidad FROM dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio;
    IF @IdServicio IS NULL THROW 51000, 'Detalle inexistente.', 1;
    IF @Cantidad IS NULL OR @Cantidad<=0 OR @Precio IS NULL OR @Precio<0 THROW 51000, 'Cantidad o precio invalido.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Servicios WHERE IdServicio=@IdServicio AND Estado=1) THROW 51000, 'Servicio o repuesto inexistente o inactivo.', 1;
    UPDATE dbo.DetalleServicios SET Cantidad=@Cantidad,Precio=@Precio,Observaciones=@Observaciones WHERE IdDetalleServicio=@IdDetalleServicio;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [37/51] PROCEDIMIENTO: usp_RetirarServicio
   MÓDULO: FLUJO / REPARACIÓN (SERVICIOS)
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Elimina un servicio registrado por error en la orden y recalcula el total.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RetirarServicio
    @IdUsuarioActor INT, @IdDetalleServicio INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede retirar detalles durante la reparacion.', 1;
    DECLARE @IdServicio INT, @Anterior INT;
    SELECT @IdServicio=IdServicio,@Anterior=Cantidad FROM dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio;
    IF @IdServicio IS NULL THROW 51000, 'Detalle inexistente.', 1;
    DELETE dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [38/51] PROCEDIMIENTO: usp_ConsumirRepuesto
   MÓDULO: FLUJO / REPARACIÓN (INVENTARIO)
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Descuenta unidades del stock de almacén en dbo.Repuestos, registra el consumo en la orden (en 3FN) y recalcula el total.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsumirRepuesto
    @IdUsuarioActor INT, @IdOrden INT, @IdRepuesto INT, @Cantidad INT, @Precio DECIMAL(10,2), @IdDetalleRepuesto INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede modificar detalles durante la reparacion.', 1;
    IF @Cantidad IS NULL OR @Cantidad<=0 OR @Precio IS NULL OR @Precio<0 THROW 51000, 'Cantidad o precio invalido.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Repuestos WHERE IdRepuesto=@IdRepuesto AND Estado=1) THROW 51000, 'Servicio o repuesto inexistente o inactivo.', 1;
    UPDATE dbo.Repuestos WITH (UPDLOCK) SET Stock=Stock-@Cantidad WHERE IdRepuesto=@IdRepuesto AND Estado=1 AND Stock>=@Cantidad;
    IF @@ROWCOUNT=0 THROW 51003, 'Stock insuficiente.', 1;
    INSERT dbo.DetalleRepuestos(IdOrden,IdRepuesto,Cantidad,Precio) VALUES(@IdOrden,@IdRepuesto,@Cantidad,@Precio);
    SET @IdDetalleRepuesto=CONVERT(INT,SCOPE_IDENTITY());
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [39/51] PROCEDIMIENTO: usp_CorregirConsumoRepuesto
   MÓDULO: FLUJO / REPARACIÓN (INVENTARIO)
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Corrige la cantidad de repuestos usados, ajustando la diferencia en el stock de inventario y recalculando el total de la orden.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_CorregirConsumoRepuesto
    @IdUsuarioActor INT, @IdDetalleRepuesto INT, @Cantidad INT, @Precio DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede modificar detalles durante la reparacion.', 1;
    DECLARE @IdRepuesto INT, @Anterior INT;
    SELECT @IdRepuesto=IdRepuesto,@Anterior=Cantidad FROM dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto;
    IF @IdRepuesto IS NULL THROW 51000, 'Detalle inexistente.', 1;
    IF @Cantidad IS NULL OR @Cantidad<=0 OR @Precio IS NULL OR @Precio<0 THROW 51000, 'Cantidad o precio invalido.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Repuestos WHERE IdRepuesto=@IdRepuesto AND Estado=1) THROW 51000, 'Servicio o repuesto inexistente o inactivo.', 1;
    UPDATE dbo.Repuestos WITH (UPDLOCK) SET Stock=Stock+@Anterior-@Cantidad WHERE IdRepuesto=@IdRepuesto AND CONVERT(BIGINT,Stock)+@Anterior-@Cantidad BETWEEN 0 AND 2147483647;
    IF @@ROWCOUNT=0 THROW 51003, 'Stock insuficiente o fuera de rango.', 1;
    UPDATE dbo.DetalleRepuestos SET Cantidad=@Cantidad,Precio=@Precio WHERE IdDetalleRepuesto=@IdDetalleRepuesto;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [40/51] PROCEDIMIENTO: usp_RetirarConsumoRepuesto
   MÓDULO: FLUJO / REPARACIÓN (INVENTARIO)
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Retira un repuesto de la orden, devolviendo el 100% de las unidades al inventario y recalculando el total.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RetirarConsumoRepuesto
    @IdUsuarioActor INT, @IdDetalleRepuesto INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede retirar detalles durante la reparacion.', 1;
    DECLARE @IdRepuesto INT, @Anterior INT;
    SELECT @IdRepuesto=IdRepuesto,@Anterior=Cantidad FROM dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto;
    IF @IdRepuesto IS NULL THROW 51000, 'Detalle inexistente.', 1;
    UPDATE dbo.Repuestos SET Stock=Stock+@Anterior WHERE IdRepuesto=@IdRepuesto;
    DELETE dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [41/51] PROCEDIMIENTO: usp_FinalizarReparacion
   MÓDULO: FLUJO / REPARACIÓN
   AUTORIZACIÓN / ROLES: Tecnico responsable
   DESCRIPCIÓN:
   Finaliza el trabajo técnico marcando la orden como 'Reparada' o 'No Reparada' con las notas finales del técnico.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_FinalizarReparacion
    @IdUsuarioActor INT, @IdOrden INT, @Reparada BIT, @Observaciones VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF NULLIF(LTRIM(RTRIM(@Observaciones)), '') IS NULL THROW 51000, 'El campo Observaciones es obligatorio.', 1;
    SET @Observaciones=LTRIM(RTRIM(@Observaciones));

    IF @EstadoActual<>'En Reparación' OR @Reparada IS NULL THROW 51002, 'La orden debe estar en reparacion y debe indicar el resultado.', 1;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    DECLARE @Nuevo VARCHAR(50)=CASE WHEN @Reparada=1 THEN 'Reparada' ELSE 'No Reparada' END;
    EXEC dbo.usp_Interno_Estado @IdOrden,@Nuevo,@IdUsuarioActor,@Observaciones;

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [42/51] PROCEDIMIENTO: usp_RegistrarPago
   MÓDULO: FACTURACIÓN / COBROS
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Registra un pago (Efectivo, Tarjeta, Transferencia) validando que el importe no supere el saldo pendiente de la orden.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_RegistrarPago
    @IdUsuarioActor INT, @IdOrden INT, @Monto DECIMAL(10,2), @MetodoPago VARCHAR(30), @Observaciones VARCHAR(300), @IdPago INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @EstadoActual NOT IN ('Reparada','No Reparada') THROW 51002, 'Los pagos se registran al finalizar el trabajo, antes de entregar.', 1;
    IF @Monto IS NULL OR @Monto<=0 THROW 51000, 'Monto invalido.', 1;
    IF @MetodoPago IS NULL OR @MetodoPago NOT IN ('Efectivo','Tarjeta','Transferencia') THROW 51000, 'Metodo de pago invalido.', 1;

    IF @Monto+COALESCE((SELECT SUM(Monto) FROM dbo.Pagos WHERE IdOrden=@IdOrden AND Anulado=0),0)>(SELECT Total FROM dbo.OrdenesReparacion WHERE IdOrden=@IdOrden) THROW 51003, 'El pago excede el saldo pendiente.', 1;
    INSERT dbo.Pagos(IdOrden,IdUsuario,Monto,MetodoPago,Observaciones) VALUES(@IdOrden,@IdUsuarioActor,@Monto,@MetodoPago,@Observaciones);
    SET @IdPago=CONVERT(INT,SCOPE_IDENTITY());

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [43/51] PROCEDIMIENTO: usp_CorregirPago
   MÓDULO: FACTURACIÓN / COBROS
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Anula el pago anterior por auditoria y registra uno nuevo corregido en una sola transacción.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_CorregirPago
    @IdUsuarioActor INT, @IdPago INT, @Monto DECIMAL(10,2), @MetodoPago VARCHAR(30), @Observaciones VARCHAR(300), @Motivo VARCHAR(300), @IdPagoNuevo INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.Pagos WHERE IdPago=@IdPago);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @EstadoActual NOT IN ('Reparada','No Reparada') THROW 51002, 'Los pagos se registran al finalizar el trabajo, antes de entregar.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Pagos WHERE IdPago=@IdPago AND Anulado=0) THROW 51000, 'Pago inexistente o anulado.', 1;
    IF @Monto IS NULL OR @Monto<=0 THROW 51000, 'Monto invalido.', 1;
    IF @MetodoPago IS NULL OR @MetodoPago NOT IN ('Efectivo','Tarjeta','Transferencia') THROW 51000, 'Metodo de pago invalido.', 1;
    IF NULLIF(LTRIM(RTRIM(@Motivo)), '') IS NULL THROW 51000, 'El campo Motivo es obligatorio.', 1;
    SET @Motivo=LTRIM(RTRIM(@Motivo));

    IF @Monto+COALESCE((SELECT SUM(Monto) FROM dbo.Pagos WHERE IdOrden=@IdOrden AND Anulado=0 AND IdPago<>@IdPago),0)>(SELECT Total FROM dbo.OrdenesReparacion WHERE IdOrden=@IdOrden) THROW 51003, 'El pago corregido excede el saldo.', 1;
    UPDATE dbo.Pagos SET Anulado=1,FechaAnulacion=GETDATE(),IdUsuarioAnulacion=@IdUsuarioActor,MotivoAnulacion=@Motivo WHERE IdPago=@IdPago;
    INSERT dbo.Pagos(IdOrden,IdUsuario,Monto,MetodoPago,Observaciones) VALUES(@IdOrden,@IdUsuarioActor,@Monto,@MetodoPago,@Observaciones);
    SET @IdPagoNuevo=CONVERT(INT,SCOPE_IDENTITY());

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [44/51] PROCEDIMIENTO: usp_AnularPago
   MÓDULO: FACTURACIÓN / COBROS
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Anula un pago con motivo explícito y usuario de anulación, recalculando el saldo pendiente de la orden.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_AnularPago
    @IdUsuarioActor INT, @IdPago INT, @Motivo VARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.Pagos WHERE IdPago=@IdPago);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @EstadoActual NOT IN ('Reparada','No Reparada') THROW 51002, 'Los pagos se registran al finalizar el trabajo, antes de entregar.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Pagos WHERE IdPago=@IdPago AND Anulado=0) THROW 51000, 'Pago inexistente o anulado.', 1;
    IF NULLIF(LTRIM(RTRIM(@Motivo)), '') IS NULL THROW 51000, 'El campo Motivo es obligatorio.', 1;
    SET @Motivo=LTRIM(RTRIM(@Motivo));
    UPDATE dbo.Pagos SET Anulado=1,FechaAnulacion=GETDATE(),IdUsuarioAnulacion=@IdUsuarioActor,MotivoAnulacion=@Motivo WHERE IdPago=@IdPago;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [45/51] PROCEDIMIENTO: usp_EntregarEquipo
   MÓDULO: FLUJO / ENTREGA FINAL
   AUTORIZACIÓN / ROLES: Recepcionista
   DESCRIPCIÓN:
   Registra la entrega del equipo al cliente. Exige que el trabajo esté finalizado (o rechazado) y que el saldo pendiente sea $0.00.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_EntregarEquipo
    @IdUsuarioActor INT, @IdOrden INT, @Observacion VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;

    IF @EstadoActual NOT IN ('Reparada','No Reparada','Rechazada') THROW 51002, 'La orden no esta lista para entrega.', 1;
    IF EXISTS(SELECT 1 FROM dbo.OrdenesReparacion WHERE IdOrden=@IdOrden AND Total<>COALESCE((SELECT SUM(Monto) FROM dbo.Pagos WHERE IdOrden=@IdOrden AND Anulado=0),0)) THROW 51003, 'La orden tiene saldo pendiente.', 1;
    UPDATE dbo.OrdenesReparacion SET FechaEntrega=GETDATE() WHERE IdOrden=@IdOrden;
    EXEC dbo.usp_Interno_Estado @IdOrden,'Entregada',@IdUsuarioActor,@Observacion;

    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO


/* =========================================================================
   [46/51] PROCEDIMIENTO: usp_ConsultarOrdenes
   MÓDULO: REPORTES Y CONSULTAS
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista, Tecnico
   DESCRIPCIÓN:
   Consulta listados de órdenes de servicio con múltiples criterios de filtrado (estado, cliente, fechas) para grillas de control.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarOrdenes
 @IdUsuarioActor INT, @IdOrden INT=NULL, @IdEstado INT=NULL, @IdCliente INT=NULL, @SoloDisponibles BIT=0, @IncluirAnuladas BIT=0, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.OrdenesReparacion o JOIN dbo.EstadosReparacion e ON e.IdEstado=o.IdEstado
 JOIN dbo.Equipos eq ON eq.IdEquipo=o.IdEquipo JOIN dbo.Clientes c ON c.IdCliente=eq.IdCliente
 OUTER APPLY(SELECT SUM(Monto) AS Pagado FROM dbo.Pagos WHERE IdOrden=o.IdOrden AND Anulado=0) p WHERE (@IdOrden IS NULL OR o.IdOrden=@IdOrden) AND (@IdEstado IS NULL OR o.IdEstado=@IdEstado)
 AND (@IdCliente IS NULL OR c.IdCliente=@IdCliente) AND (@IncluirAnuladas=1 OR o.Anulada=0)
 AND (@SoloDisponibles=0 OR (e.Nombre='En Revisión' AND o.IdTecnicoResponsable IS NULL AND o.Anulada=0)) AND (@Buscar IS NULL OR o.NumeroOrden LIKE N'%'+@Buscar+N'%' OR c.Nombre LIKE N'%'+@Buscar+N'%' OR c.Apellido LIKE N'%'+@Buscar+N'%' OR e.Nombre LIKE N'%'+@Buscar+N'%');
 SELECT o.*,e.Nombre AS EstadoNombre,c.IdCliente,c.Nombre AS NombreCliente,c.Apellido AS ApellidoCliente,COALESCE(p.Pagado,0) AS Pagado,o.Total-COALESCE(p.Pagado,0) AS Saldo FROM dbo.OrdenesReparacion o JOIN dbo.EstadosReparacion e ON e.IdEstado=o.IdEstado
 JOIN dbo.Equipos eq ON eq.IdEquipo=o.IdEquipo JOIN dbo.Clientes c ON c.IdCliente=eq.IdCliente
 OUTER APPLY(SELECT SUM(Monto) AS Pagado FROM dbo.Pagos WHERE IdOrden=o.IdOrden AND Anulado=0) p WHERE (@IdOrden IS NULL OR o.IdOrden=@IdOrden) AND (@IdEstado IS NULL OR o.IdEstado=@IdEstado)
 AND (@IdCliente IS NULL OR c.IdCliente=@IdCliente) AND (@IncluirAnuladas=1 OR o.Anulada=0)
 AND (@SoloDisponibles=0 OR (e.Nombre='En Revisión' AND o.IdTecnicoResponsable IS NULL AND o.Anulada=0)) AND (@Buscar IS NULL OR o.NumeroOrden LIKE N'%'+@Buscar+N'%' OR c.Nombre LIKE N'%'+@Buscar+N'%' OR c.Apellido LIKE N'%'+@Buscar+N'%' OR e.Nombre LIKE N'%'+@Buscar+N'%')
 ORDER BY o.IdOrden DESC OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* =========================================================================
   [47/51] PROCEDIMIENTO: usp_ConsultarExpedienteOrden
   MÓDULO: REPORTES Y CONSULTAS
   AUTORIZACIÓN / ROLES: Administrador, Recepcionista, Tecnico
   DESCRIPCIÓN:
   Consulta el expediente integral de una orden: cabecera con saldos, cliente, equipo, diagnóstico, confirmación, servicios, repuestos, pagos e historial.
   ========================================================================= */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarExpedienteOrden
 @IdUsuarioActor INT, @IdOrden INT
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT o.*, e.Nombre AS EstadoNombre, c.IdCliente, c.Nombre AS NombreCliente, c.Apellido AS ApellidoCliente,
 COALESCE(p.Pagado,0) AS Pagado, o.Total-COALESCE(p.Pagado,0) AS Saldo
FROM dbo.OrdenesReparacion o JOIN dbo.EstadosReparacion e ON e.IdEstado=o.IdEstado
JOIN dbo.Equipos eq ON eq.IdEquipo=o.IdEquipo JOIN dbo.Clientes c ON c.IdCliente=eq.IdCliente
OUTER APPLY(SELECT SUM(Monto) AS Pagado FROM dbo.Pagos WHERE IdOrden=o.IdOrden AND Anulado=0) p
WHERE o.IdOrden=@IdOrden;
SELECT * FROM dbo.Diagnosticos WHERE IdOrden=@IdOrden;
SELECT c.* FROM dbo.ConfirmacionesReparacion c JOIN dbo.Diagnosticos d ON d.IdDiagnostico=c.IdDiagnostico WHERE d.IdOrden=@IdOrden;
SELECT ds.IdDetalleServicio, ds.IdOrden, ds.IdServicio, o.IdTecnicoResponsable AS IdTecnico, ds.Cantidad, ds.Precio, ds.Observaciones
FROM dbo.DetalleServicios ds
JOIN dbo.OrdenesReparacion o ON o.IdOrden = ds.IdOrden
WHERE ds.IdOrden=@IdOrden
ORDER BY ds.IdDetalleServicio;

SELECT dr.IdDetalleRepuesto, dr.IdOrden, dr.IdRepuesto, o.IdTecnicoResponsable AS IdTecnico, dr.Cantidad, dr.Precio
FROM dbo.DetalleRepuestos dr
JOIN dbo.OrdenesReparacion o ON o.IdOrden = dr.IdOrden
WHERE dr.IdOrden=@IdOrden
ORDER BY dr.IdDetalleRepuesto;
SELECT * FROM dbo.Pagos WHERE IdOrden=@IdOrden ORDER BY IdPago;
SELECT * FROM dbo.HistorialEstados WHERE IdOrden=@IdOrden ORDER BY IdHistorial;
END;
GO


/* [48/51] usp_GuardarRol: soporte DAL/BLL, roles y listados con paginacion SQL. */
CREATE OR ALTER PROCEDURE dbo.usp_GuardarRol
 @IdUsuarioActor INT, @IdRol INT OUTPUT, @Nombre VARCHAR(50), @Descripcion VARCHAR(200), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON; SET XACT_ABORT ON;
 BEGIN TRY
  BEGIN TRANSACTION;
  EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
  SET @Nombre=NULLIF(LTRIM(RTRIM(@Nombre)),'');
  IF @Nombre IS NULL OR @IdRol IS NULL OR @IdRol<0 OR @Estado IS NULL
     THROW 51000,'Datos del rol invalidos.',1;
  IF @IdRol=0
  BEGIN
   INSERT dbo.Roles(Nombre,Descripcion,Estado) VALUES(@Nombre,@Descripcion,@Estado);
   SET @IdRol=CONVERT(INT,SCOPE_IDENTITY());
  END
  ELSE
  BEGIN
   DECLARE @NombreAnterior VARCHAR(50);
   SELECT @NombreAnterior=Nombre FROM dbo.Roles WITH(UPDLOCK,HOLDLOCK) WHERE IdRol=@IdRol;
   IF @NombreAnterior IS NULL THROW 51000,'Rol inexistente.',1;
   IF @NombreAnterior IN ('Administrador','Recepcionista','Tecnico') AND (CONVERT(VARBINARY(100),@Nombre)<>CONVERT(VARBINARY(100),@NombreAnterior) OR @Estado=0)
      THROW 51000,'Los roles del sistema conservan su nombre y estado activo.',1;
   IF @Estado=0 AND EXISTS(SELECT 1 FROM dbo.Usuarios WITH(UPDLOCK,HOLDLOCK) WHERE IdRol=@IdRol)
      THROW 51000,'No puede desactivar un rol con usuarios asignados.',1;
   UPDATE dbo.Roles SET Nombre=@Nombre,Descripcion=@Descripcion,Estado=@Estado WHERE IdRol=@IdRol;
  END
  COMMIT TRANSACTION;
 END TRY
 BEGIN CATCH
  IF @@TRANCOUNT>0 ROLLBACK TRANSACTION;
  THROW;
 END CATCH;
END;
GO


/* [49/51] usp_ConsultarRoles: soporte DAL/BLL, roles y listados con paginacion SQL. */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarRoles
 @IdUsuarioActor INT, @IdRol INT=NULL, @SoloActivos BIT=1, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.Roles WHERE (@IdRol IS NULL OR IdRol=@IdRol) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Descripcion LIKE N'%'+@Buscar+N'%');
 SELECT IdRol,Nombre,Descripcion,Estado FROM dbo.Roles WHERE (@IdRol IS NULL OR IdRol=@IdRol) AND (@SoloActivos=0 OR Estado=1) AND (@Buscar IS NULL OR Nombre LIKE N'%'+@Buscar+N'%' OR Descripcion LIKE N'%'+@Buscar+N'%')
 ORDER BY Nombre,IdRol OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* [50/51] usp_ConsultarPagos: soporte DAL/BLL, roles y listados con paginacion SQL. */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarPagos
 @IdUsuarioActor INT, @IdOrden INT=NULL, @IncluirAnulados BIT=0, @IdPago INT=NULL, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.Pagos WHERE (@IdPago IS NULL OR IdPago=@IdPago) AND (@IdOrden IS NULL OR IdOrden=@IdOrden) AND (@IncluirAnulados=1 OR Anulado=0) AND (@Buscar IS NULL OR MetodoPago LIKE N'%'+@Buscar+N'%' OR Observaciones LIKE N'%'+@Buscar+N'%' OR MotivoAnulacion LIKE N'%'+@Buscar+N'%');
 SELECT IdPago,IdOrden,IdUsuario,Monto,MetodoPago,FechaPago,Observaciones,Anulado,FechaAnulacion,IdUsuarioAnulacion,MotivoAnulacion FROM dbo.Pagos WHERE (@IdPago IS NULL OR IdPago=@IdPago) AND (@IdOrden IS NULL OR IdOrden=@IdOrden) AND (@IncluirAnulados=1 OR Anulado=0) AND (@Buscar IS NULL OR MetodoPago LIKE N'%'+@Buscar+N'%' OR Observaciones LIKE N'%'+@Buscar+N'%' OR MotivoAnulacion LIKE N'%'+@Buscar+N'%')
 ORDER BY IdPago DESC OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO


/* [51/51] usp_ConsultarLogErrores: soporte DAL/BLL, roles y listados con paginacion SQL. */
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarLogErrores
 @IdUsuarioActor INT, @Pagina INT=NULL, @TamPagina INT=10, @Buscar NVARCHAR(100)=NULL, @TotalRegistros INT=NULL OUTPUT
AS
BEGIN
 SET NOCOUNT ON;
 EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
 IF (@Pagina IS NOT NULL AND @Pagina<1) OR (@TamPagina IS NULL OR @TamPagina NOT BETWEEN 1 AND 200)
    THROW 51000,'Pagina o tamano de pagina invalido.',1;
 SET @Buscar=NULLIF(LTRIM(RTRIM(@Buscar)),N'');
 DECLARE @Inicio BIGINT=CASE WHEN @Pagina IS NULL THEN 0 ELSE (CONVERT(BIGINT,@Pagina)-1)*@TamPagina END;
 DECLARE @Limite INT=CASE WHEN @Pagina IS NULL THEN 2147483647 ELSE @TamPagina END;
 SELECT @TotalRegistros=COUNT(*) FROM dbo.tblLogErrores WHERE 1=1 AND (@Buscar IS NULL OR mensajeError LIKE N'%'+@Buscar+N'%' OR procedimiento LIKE N'%'+@Buscar+N'%' OR usuarioApp LIKE N'%'+@Buscar+N'%');
 SELECT idLog,mensajeError,numeroError,procedimiento,lineaError,usuarioApp,fechaError FROM dbo.tblLogErrores WHERE 1=1 AND (@Buscar IS NULL OR mensajeError LIKE N'%'+@Buscar+N'%' OR procedimiento LIKE N'%'+@Buscar+N'%' OR usuarioApp LIKE N'%'+@Buscar+N'%')
 ORDER BY idLog DESC OFFSET @Inicio ROWS FETCH NEXT @Limite ROWS ONLY;
END;
GO
