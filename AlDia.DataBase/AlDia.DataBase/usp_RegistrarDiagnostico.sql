-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RegistrarDiagnostico
    @IdUsuarioActor INT, @IdOrden INT, @ProblemaEncontrado VARCHAR(1000), @ReparacionPropuesta VARCHAR(1000), @CostoEstimado DECIMAL(10,2), @Observaciones VARCHAR(1000), @IdDiagnostico INT OUTPUT
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
    IF NULLIF(LTRIM(RTRIM(@ProblemaEncontrado)), '') IS NULL THROW 51000, 'El campo ProblemaEncontrado es obligatorio.', 1;
    SET @ProblemaEncontrado=LTRIM(RTRIM(@ProblemaEncontrado));
    IF NULLIF(LTRIM(RTRIM(@ReparacionPropuesta)), '') IS NULL THROW 51000, 'El campo ReparacionPropuesta es obligatorio.', 1;
    SET @ReparacionPropuesta=LTRIM(RTRIM(@ReparacionPropuesta));
    
    IF @EstadoActual<>'En Revisión' THROW 51002, 'La orden debe estar en revision.', 1;
    INSERT dbo.Diagnosticos(IdOrden,IdTecnico,ProblemaEncontrado,ReparacionPropuesta,CostoEstimado,Observaciones)
    VALUES(@IdOrden,@IdUsuarioActor,@ProblemaEncontrado,@ReparacionPropuesta,@CostoEstimado,@Observaciones);
    SET @IdDiagnostico=CONVERT(INT,SCOPE_IDENTITY());
    EXEC dbo.usp_Interno_Estado @IdOrden,'Pendiente de Confirmación',@IdUsuarioActor,'Diagnostico registrado';
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
