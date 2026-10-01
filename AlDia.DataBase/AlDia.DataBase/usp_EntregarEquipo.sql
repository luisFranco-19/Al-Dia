-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_EntregarEquipo
    @IdUsuarioActor INT, @IdOrden INT, @Observacion VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    
    IF @EstadoActual NOT IN ('Reparada','No Reparada','Rechazada') THROW 51002, 'La orden no esta lista para entrega.', 1;
    IF EXISTS(SELECT 1 FROM dbo.OrdenesReparacion WHERE IdOrden=@IdOrden AND Total<>COALESCE((SELECT SUM(Monto) FROM dbo.Pagos WHERE IdOrden=@IdOrden AND Anulado=0),0)) THROW 51003, 'La orden tiene saldo pendiente.', 1;
    UPDATE dbo.OrdenesReparacion SET FechaEntrega=GETDATE() WHERE IdOrden=@IdOrden;
    EXEC dbo.usp_Interno_Estado @IdOrden,'Entregada',@IdUsuarioActor,@Observacion;
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
