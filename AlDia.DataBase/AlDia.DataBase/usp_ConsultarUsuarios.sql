USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarUsuarios
 @IdUsuarioActor INT, @IdUsuario INT=NULL, @SoloActivos BIT=1
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
SELECT IdUsuario,Nombre,Apellido,Cedula,Telefono,Correo,Usuario,Rol,Estado,FechaRegistro FROM dbo.Usuarios WHERE (@IdUsuario IS NULL OR IdUsuario=@IdUsuario) AND (@SoloActivos=0 OR Estado=1) ORDER BY Nombre,Apellido;
END;
GO
