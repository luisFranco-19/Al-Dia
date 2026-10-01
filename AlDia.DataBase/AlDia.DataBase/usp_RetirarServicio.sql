-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RetirarServicio
    @IdUsuarioActor INT, @IdDetalleServicio INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede retirar detalles durante la reparacion.', 1;
    DECLARE @IdServicio INT, @Anterior INT;
    SELECT @IdServicio=IdServicio,@Anterior=Cantidad FROM dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio;
    IF @IdServicio IS NULL THROW 51000, 'Detalle inexistente.', 1;
    DELETE dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
