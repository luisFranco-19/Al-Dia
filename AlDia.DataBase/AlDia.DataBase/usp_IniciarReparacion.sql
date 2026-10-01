-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_IniciarReparacion
    @IdUsuarioActor INT, @IdOrden INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    
    IF @EstadoActual<>'Aprobada' OR NOT EXISTS(SELECT 1 FROM dbo.Diagnosticos d JOIN dbo.ConfirmacionesReparacion c ON c.IdDiagnostico=d.IdDiagnostico WHERE d.IdOrden=@IdOrden AND c.Decision='APROBADA') THROW 51002, 'Hace falta diagnostico y aprobacion del cliente.', 1;
    EXEC dbo.usp_Interno_Estado @IdOrden,'En Reparación',@IdUsuarioActor,'Inicio de reparacion';
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
