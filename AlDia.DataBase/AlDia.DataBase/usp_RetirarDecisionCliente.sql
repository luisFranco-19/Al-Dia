-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RetirarDecisionCliente
    @IdUsuarioActor INT, @IdConfirmacion INT
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
    DELETE dbo.ConfirmacionesReparacion WHERE IdConfirmacion=@IdConfirmacion;
    EXEC dbo.usp_Interno_Estado @IdOrden,'Pendiente de Confirmación',@IdUsuarioActor,'Decision retirada para correccion';
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
