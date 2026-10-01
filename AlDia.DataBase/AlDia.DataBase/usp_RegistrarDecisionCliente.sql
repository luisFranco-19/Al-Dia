-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RegistrarDecisionCliente
    @IdUsuarioActor INT, @IdDiagnostico INT, @Decision VARCHAR(20), @CostoAprobado DECIMAL(10,2), @Observaciones VARCHAR(500), @IdConfirmacion INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.Diagnosticos WHERE IdDiagnostico=@IdDiagnostico);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    
    IF @Decision IS NULL OR @Decision NOT IN ('APROBADA','RECHAZADA') THROW 51000, 'Decision invalida.', 1;
    IF @Decision='APROBADA' AND (@CostoAprobado IS NULL OR @CostoAprobado<0) THROW 51000, 'Indique el costo autorizado por el cliente.', 1;
    IF @Decision='RECHAZADA' AND @CostoAprobado IS NOT NULL THROW 51000, 'Una decision rechazada no tiene costo aprobado.', 1;
    
    IF @EstadoActual<>'Pendiente de Confirmación' THROW 51002, 'La orden no espera confirmacion.', 1;
    INSERT dbo.ConfirmacionesReparacion(IdDiagnostico,IdUsuario,Decision,CostoAprobado,Observaciones)
    VALUES(@IdDiagnostico,@IdUsuarioActor,@Decision,@CostoAprobado,@Observaciones);
    SET @IdConfirmacion=CONVERT(INT,SCOPE_IDENTITY());
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
