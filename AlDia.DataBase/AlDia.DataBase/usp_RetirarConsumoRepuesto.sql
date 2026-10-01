-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RetirarConsumoRepuesto
    @IdUsuarioActor INT, @IdDetalleRepuesto INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede retirar detalles durante la reparacion.', 1;
    DECLARE @IdRepuesto INT, @Anterior INT;
    SELECT @IdRepuesto=IdRepuesto,@Anterior=Cantidad FROM dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto;
    IF @IdRepuesto IS NULL THROW 51000, 'Detalle inexistente.', 1;
    UPDATE dbo.Repuestos SET Stock=Stock+@Anterior WHERE IdRepuesto=@IdRepuesto;
    DELETE dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
