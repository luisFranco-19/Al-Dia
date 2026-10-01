-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RegistrarPago
    @IdUsuarioActor INT, @IdOrden INT, @Monto DECIMAL(10,2), @MetodoPago VARCHAR(30), @Observaciones VARCHAR(300), @IdPago INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @EstadoActual NOT IN ('Reparada','No Reparada') THROW 51002, 'Los pagos se registran al finalizar el trabajo, antes de entregar.', 1;
    IF @Monto IS NULL OR @Monto<=0 THROW 51000, 'Monto invalido.', 1;
    IF @MetodoPago IS NULL OR @MetodoPago NOT IN ('Efectivo','Tarjeta','Transferencia') THROW 51000, 'Metodo de pago invalido.', 1;
    
    IF @Monto+COALESCE((SELECT SUM(Monto) FROM dbo.Pagos WHERE IdOrden=@IdOrden AND Anulado=0),0)>(SELECT Total FROM dbo.OrdenesReparacion WHERE IdOrden=@IdOrden) THROW 51003, 'El pago excede el saldo pendiente.', 1;
    INSERT dbo.Pagos(IdOrden,IdUsuario,Monto,MetodoPago,Observaciones) VALUES(@IdOrden,@IdUsuarioActor,@Monto,@MetodoPago,@Observaciones);
    SET @IdPago=CONVERT(INT,SCOPE_IDENTITY());
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
