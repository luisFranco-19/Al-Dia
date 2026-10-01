-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_CorregirPago
    @IdUsuarioActor INT, @IdPago INT, @Monto DECIMAL(10,2), @MetodoPago VARCHAR(30), @Observaciones VARCHAR(300), @Motivo VARCHAR(300), @IdPagoNuevo INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.Pagos WHERE IdPago=@IdPago);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @EstadoActual NOT IN ('Reparada','No Reparada') THROW 51002, 'Los pagos se registran al finalizar el trabajo, antes de entregar.', 1;
    IF NOT EXISTS(SELECT 1 FROM dbo.Pagos WHERE IdPago=@IdPago AND Anulado=0) THROW 51000, 'Pago inexistente o anulado.', 1;
    IF @Monto IS NULL OR @Monto<=0 THROW 51000, 'Monto invalido.', 1;
    IF @MetodoPago IS NULL OR @MetodoPago NOT IN ('Efectivo','Tarjeta','Transferencia') THROW 51000, 'Metodo de pago invalido.', 1;
    IF NULLIF(LTRIM(RTRIM(@Motivo)), '') IS NULL THROW 51000, 'El campo Motivo es obligatorio.', 1;
    SET @Motivo=LTRIM(RTRIM(@Motivo));
    
    IF @Monto+COALESCE((SELECT SUM(Monto) FROM dbo.Pagos WHERE IdOrden=@IdOrden AND Anulado=0 AND IdPago<>@IdPago),0)>(SELECT Total FROM dbo.OrdenesReparacion WHERE IdOrden=@IdOrden) THROW 51003, 'El pago corregido excede el saldo.', 1;
    UPDATE dbo.Pagos SET Anulado=1,FechaAnulacion=GETDATE(),IdUsuarioAnulacion=@IdUsuarioActor,MotivoAnulacion=@Motivo WHERE IdPago=@IdPago;
    INSERT dbo.Pagos(IdOrden,IdUsuario,Monto,MetodoPago,Observaciones) VALUES(@IdOrden,@IdUsuarioActor,@Monto,@MetodoPago,@Observaciones);
    SET @IdPagoNuevo=CONVERT(INT,SCOPE_IDENTITY());
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
