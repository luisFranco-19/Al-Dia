-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_CorregirDecisionCliente
    @IdUsuarioActor INT, @IdConfirmacion INT, @Decision VARCHAR(20), @CostoAprobado DECIMAL(10,2), @Observaciones VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @IdOrden INT=(SELECT d.IdOrden FROM dbo.ConfirmacionesReparacion c JOIN dbo.Diagnosticos d ON d.IdDiagnostico=c.IdDiagnostico WHERE c.IdConfirmacion=@IdConfirmacion);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF NOT EXISTS (SELECT 1 FROM dbo.ConfirmacionesReparacion WITH (UPDLOCK,HOLDLOCK) WHERE IdConfirmacion=@IdConfirmacion) THROW 51000, 'No existe el registro de ConfirmacionesReparacion.', 1;
    IF @EstadoActual NOT IN ('Aprobada','Rechazada') THROW 51002, 'La decision solo puede corregirse antes de iniciar la reparacion o entregar.', 1;
    
    IF @Decision IS NULL OR @Decision NOT IN ('APROBADA','RECHAZADA') THROW 51000, 'Decision invalida.', 1;
    IF @Decision='APROBADA' AND (@CostoAprobado IS NULL OR @CostoAprobado<0) THROW 51000, 'Indique el costo autorizado por el cliente.', 1;
    IF @Decision='RECHAZADA' AND @CostoAprobado IS NOT NULL THROW 51000, 'Una decision rechazada no tiene costo aprobado.', 1;
    UPDATE dbo.ConfirmacionesReparacion SET Decision=@Decision,CostoAprobado=@CostoAprobado,Observaciones=@Observaciones,IdUsuario=@IdUsuarioActor,FechaConfirmacion=GETDATE() WHERE IdConfirmacion=@IdConfirmacion;
    DECLARE @Nuevo VARCHAR(50)=CASE WHEN @Decision='APROBADA' THEN 'Aprobada' ELSE 'Rechazada' END;
    EXEC dbo.usp_Interno_Estado @IdOrden,@Nuevo,@IdUsuarioActor,'Decision del cliente registrada';
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
