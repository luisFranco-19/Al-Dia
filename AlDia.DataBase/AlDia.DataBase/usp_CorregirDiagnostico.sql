-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_CorregirDiagnostico
    @IdUsuarioActor INT, @IdDiagnostico INT, @ProblemaEncontrado VARCHAR(1000), @ReparacionPropuesta VARCHAR(1000), @CostoEstimado DECIMAL(10,2), @Observaciones VARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Tecnico';
    DECLARE @IdOrden INT=(SELECT IdOrden FROM dbo.Diagnosticos WHERE IdDiagnostico=@IdDiagnostico);
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF @Tecnico IS NULL OR @Tecnico<>@IdUsuarioActor THROW 51001, 'Debe ser el tecnico responsable de la orden.', 1;
    IF NOT EXISTS (SELECT 1 FROM dbo.Diagnosticos WITH (UPDLOCK,HOLDLOCK) WHERE IdDiagnostico=@IdDiagnostico) THROW 51000, 'No existe el registro de Diagnosticos.', 1;
    IF @EstadoActual<>'Pendiente de Confirmación' OR EXISTS(SELECT 1 FROM dbo.ConfirmacionesReparacion WHERE IdDiagnostico=@IdDiagnostico) THROW 51002, 'El diagnostico ya tiene decision del cliente.', 1;
    IF NULLIF(LTRIM(RTRIM(@ProblemaEncontrado)), '') IS NULL THROW 51000, 'El campo ProblemaEncontrado es obligatorio.', 1;
    SET @ProblemaEncontrado=LTRIM(RTRIM(@ProblemaEncontrado));
    IF NULLIF(LTRIM(RTRIM(@ReparacionPropuesta)), '') IS NULL THROW 51000, 'El campo ReparacionPropuesta es obligatorio.', 1;
    SET @ReparacionPropuesta=LTRIM(RTRIM(@ReparacionPropuesta));
    UPDATE dbo.Diagnosticos SET ProblemaEncontrado=@ProblemaEncontrado,ReparacionPropuesta=@ReparacionPropuesta,CostoEstimado=@CostoEstimado,Observaciones=@Observaciones WHERE IdDiagnostico=@IdDiagnostico;
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
