USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
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
