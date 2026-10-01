USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
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
INSERT dbo.Usuarios(Nombre,Apellido,Cedula,Telefono,Correo,Usuario,Contrasena,Rol) VALUES(@Nombre,@Apellido,@Cedula,@Telefono,@Correo,@Usuario,@ContrasenaHash,'Administrador');
SET @IdUsuario=CONVERT(INT,SCOPE_IDENTITY());
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO
