USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GuardarUsuario
 @IdUsuarioActor INT, @IdUsuario INT OUTPUT, @Nombre VARCHAR(100), @Apellido VARCHAR(100), @Cedula VARCHAR(20), @Telefono VARCHAR(20), @Correo VARCHAR(100), @Usuario VARCHAR(50), @ContrasenaHash VARCHAR(255), @Rol VARCHAR(20), @Estado BIT
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
IF @Rol IS NULL OR @Rol NOT IN ('Administrador','Recepcionista','Tecnico') THROW 51000,'Rol invalido.',1;
IF @IdUsuario=0
BEGIN
 IF NULLIF(@ContrasenaHash,'') IS NULL THROW 51000,'Una nueva cuenta requiere contrasena.',1;
 INSERT dbo.Usuarios(Nombre,Apellido,Cedula,Telefono,Correo,Usuario,Contrasena,Rol,Estado) VALUES(@Nombre,@Apellido,@Cedula,@Telefono,@Correo,@Usuario,@ContrasenaHash,@Rol,@Estado);
 SET @IdUsuario=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 DECLARE @Administradores INT;
 SELECT @Administradores=COUNT(*) FROM dbo.Usuarios WITH(UPDLOCK,HOLDLOCK) WHERE Rol='Administrador' AND Estado=1;
 IF NOT EXISTS(SELECT 1 FROM dbo.Usuarios WHERE IdUsuario=@IdUsuario) THROW 51000,'Usuario inexistente.',1;
 IF EXISTS(SELECT 1 FROM dbo.Usuarios WHERE IdUsuario=@IdUsuario AND Rol='Administrador' AND Estado=1)
    AND (@Rol<>'Administrador' OR @Estado=0) AND @Administradores<=1 THROW 51000,'Debe conservar un administrador activo.',1;
 IF @IdUsuario=@IdUsuarioActor AND (@Estado=0 OR @Rol<>'Administrador') THROW 51000,'No puede desactivar o cambiar el rol de su propia sesion.',1;
 UPDATE dbo.Usuarios SET Nombre=@Nombre,Apellido=@Apellido,Cedula=@Cedula,Telefono=@Telefono,Correo=@Correo,Usuario=@Usuario,
    Contrasena=COALESCE(@ContrasenaHash,Contrasena),Rol=@Rol,Estado=@Estado WHERE IdUsuario=@IdUsuario;
END

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO
