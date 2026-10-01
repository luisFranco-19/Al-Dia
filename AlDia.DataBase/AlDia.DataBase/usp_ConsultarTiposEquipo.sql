USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarTiposEquipo
 @IdUsuarioActor INT, @IdTipoEquipo INT=NULL, @SoloActivos BIT=1
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT IdTipoEquipo,Nombre,Descripcion,Estado FROM dbo.TiposEquipo WHERE (@IdTipoEquipo IS NULL OR IdTipoEquipo=@IdTipoEquipo) AND (@SoloActivos=0 OR Estado=1) ORDER BY IdTipoEquipo;
END;
GO
