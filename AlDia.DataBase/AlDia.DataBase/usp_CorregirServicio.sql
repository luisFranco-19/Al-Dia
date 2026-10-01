-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_CorregirServicio
    @IdUsuarioActor INT, @IdDetalleServicio INT, @Cantidad INT, @Precio DECIMAL(10,2), @Observaciones VARCHAR(500)
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
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede modificar detalles durante la reparacion.', 1;
    DECLARE @IdServicio INT, @Anterior INT;
    SELECT @IdServicio=IdServicio,@Anterior=Cantidad FROM dbo.DetalleServicios WHERE IdDetalleServicio=@IdDetalleServicio;
    IF @IdServicio IS NULL THROW 51000, 'Detalle inexistente.', 1;
    IF @Cantidad IS NULL OR @Cantidad<=0 OR @Precio IS NULL OR @Precio<0 THROW 51000, 'Cantidad o precio invalido.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Servicios WHERE IdServicio=@IdServicio AND Estado=1) THROW 51000, 'Servicio o repuesto inexistente o inactivo.', 1;
    UPDATE dbo.DetalleServicios SET Cantidad=@Cantidad,Precio=@Precio,Observaciones=@Observaciones WHERE IdDetalleServicio=@IdDetalleServicio;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
