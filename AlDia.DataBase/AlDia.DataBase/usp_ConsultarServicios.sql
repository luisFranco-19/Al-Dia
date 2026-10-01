USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarServicios
 @IdUsuarioActor INT, @IdServicio INT=NULL, @SoloActivos BIT=1
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT IdServicio,Nombre,Descripcion,PrecioBase,Estado FROM dbo.Servicios WHERE (@IdServicio IS NULL OR IdServicio=@IdServicio) AND (@SoloActivos=0 OR Estado=1) ORDER BY IdServicio;
END;
GO
