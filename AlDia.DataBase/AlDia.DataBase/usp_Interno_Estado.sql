-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Interno_Estado
    @IdOrden INT, @Nombre VARCHAR(50), @IdUsuarioActor INT, @Observacion VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Anterior INT, @Nuevo INT;
    SELECT @Anterior=IdEstado FROM dbo.OrdenesReparacion WITH (UPDLOCK,HOLDLOCK) WHERE IdOrden=@IdOrden;
    SELECT @Nuevo=IdEstado FROM dbo.EstadosReparacion WHERE Nombre=@Nombre;
    IF @Anterior IS NULL OR @Nuevo IS NULL THROW 51002, 'Orden o estado inexistente.', 1;
    UPDATE dbo.OrdenesReparacion SET IdEstado=@Nuevo WHERE IdOrden=@IdOrden;
    INSERT dbo.HistorialEstados(IdOrden,IdEstadoAnterior,IdEstadoNuevo,IdUsuario,Observacion)
    VALUES(@IdOrden,@Anterior,@Nuevo,@IdUsuarioActor,@Observacion);
END;
GO
