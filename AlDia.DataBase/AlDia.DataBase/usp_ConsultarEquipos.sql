USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarEquipos
 @IdUsuarioActor INT, @IdEquipo INT=NULL, @SoloActivos BIT=1
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT IdEquipo,IdCliente,IdTipoEquipo,Marca,Modelo,NumeroSerie,Color,Observaciones,Estado,FechaRegistro FROM dbo.Equipos WHERE (@IdEquipo IS NULL OR IdEquipo=@IdEquipo) AND (@SoloActivos=0 OR Estado=1) ORDER BY IdEquipo;
END;
GO
