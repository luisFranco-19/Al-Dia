-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_AnularPago
    @IdUsuarioActor INT, @IdPago INT, @Motivo VARCHAR(300)
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
    IF NULLIF(LTRIM(RTRIM(@Motivo)), '') IS NULL THROW 51000, 'El campo Motivo es obligatorio.', 1;
    SET @Motivo=LTRIM(RTRIM(@Motivo));
    UPDATE dbo.Pagos SET Anulado=1,FechaAnulacion=GETDATE(),IdUsuarioAnulacion=@IdUsuarioActor,MotivoAnulacion=@Motivo WHERE IdPago=@IdPago;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
