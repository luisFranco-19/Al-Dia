-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Interno_BloquearOrden
    @IdOrden INT, @Estado VARCHAR(50) OUTPUT, @Tecnico INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    SET @Estado=NULL; SET @Tecnico=NULL;
    SELECT @Estado=e.Nombre, @Tecnico=o.IdTecnicoResponsable
    FROM dbo.OrdenesReparacion o WITH (UPDLOCK,HOLDLOCK)
    JOIN dbo.EstadosReparacion e ON e.IdEstado=o.IdEstado
    WHERE o.IdOrden=@IdOrden AND o.Anulada=0;
    IF @Estado IS NULL THROW 51002, 'La orden no existe o esta anulada.', 1;
END;
GO
