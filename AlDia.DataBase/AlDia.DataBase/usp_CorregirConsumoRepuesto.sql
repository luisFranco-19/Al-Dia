-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_CorregirConsumoRepuesto
    @IdUsuarioActor INT, @IdDetalleRepuesto INT, @Cantidad INT, @Precio DECIMAL(10,2)
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
    IF @EstadoActual<>'En Reparación' THROW 51002, 'Solo puede modificar detalles durante la reparacion.', 1;
    DECLARE @IdRepuesto INT, @Anterior INT;
    SELECT @IdRepuesto=IdRepuesto,@Anterior=Cantidad FROM dbo.DetalleRepuestos WHERE IdDetalleRepuesto=@IdDetalleRepuesto;
    IF @IdRepuesto IS NULL THROW 51000, 'Detalle inexistente.', 1;
    IF @Cantidad IS NULL OR @Cantidad<=0 OR @Precio IS NULL OR @Precio<0 THROW 51000, 'Cantidad o precio invalido.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Repuestos WHERE IdRepuesto=@IdRepuesto AND Estado=1) THROW 51000, 'Servicio o repuesto inexistente o inactivo.', 1;
    UPDATE dbo.Repuestos WITH (UPDLOCK) SET Stock=Stock+@Anterior-@Cantidad WHERE IdRepuesto=@IdRepuesto AND CONVERT(BIGINT,Stock)+@Anterior-@Cantidad BETWEEN 0 AND 2147483647;
    IF @@ROWCOUNT=0 THROW 51003, 'Stock insuficiente o fuera de rango.', 1;
    UPDATE dbo.DetalleRepuestos SET Cantidad=@Cantidad,Precio=@Precio WHERE IdDetalleRepuesto=@IdDetalleRepuesto;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
