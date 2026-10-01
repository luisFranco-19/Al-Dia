USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarRepuestos
 @IdUsuarioActor INT, @IdRepuesto INT=NULL, @SoloActivos BIT=1
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT IdRepuesto,Nombre,Descripcion,Marca,NumeroParte,PrecioCompra,PrecioVenta,Estado,Stock FROM dbo.Repuestos WHERE (@IdRepuesto IS NULL OR IdRepuesto=@IdRepuesto) AND (@SoloActivos=0 OR Estado=1) ORDER BY IdRepuesto;
END;
GO
