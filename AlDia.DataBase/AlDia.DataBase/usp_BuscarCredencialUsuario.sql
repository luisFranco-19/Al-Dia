USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_BuscarCredencialUsuario
 @Usuario VARCHAR(50)
AS
BEGIN
 SET NOCOUNT ON;
SELECT IdUsuario,Nombre,Apellido,Cedula,Telefono,Correo,Usuario,Rol,Estado,FechaRegistro,Contrasena FROM dbo.Usuarios WHERE Usuario=@Usuario AND Estado=1;
END;
GO
