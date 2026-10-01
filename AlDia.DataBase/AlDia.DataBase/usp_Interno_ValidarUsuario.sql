-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Interno_ValidarUsuario
    @IdUsuarioActor INT, @Roles VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM dbo.Usuarios WHERE IdUsuario=@IdUsuarioActor AND Estado=1
        AND CHARINDEX(','+Rol+',', ','+@Roles+',') > 0)
        THROW 51001, 'Usuario inexistente, inactivo o sin el rol requerido.', 1;
END;
GO
