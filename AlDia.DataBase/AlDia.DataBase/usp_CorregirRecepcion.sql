-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_CorregirRecepcion
    @IdUsuarioActor INT, @IdOrden INT, @ProblemaReportado VARCHAR(500), @ObservacionesRecepcion VARCHAR(500), @AccesoriosRecepcion VARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF NULLIF(LTRIM(RTRIM(@ProblemaReportado)), '') IS NULL THROW 51000, 'El campo ProblemaReportado es obligatorio.', 1;
    SET @ProblemaReportado=LTRIM(RTRIM(@ProblemaReportado));
    
    IF @EstadoActual<>'En Revisión' OR @Tecnico IS NOT NULL THROW 51002, 'Solo puede corregir la recepcion antes de que un tecnico tome la orden.', 1;
    UPDATE dbo.OrdenesReparacion SET ProblemaReportado=@ProblemaReportado,ObservacionesRecepcion=@ObservacionesRecepcion,AccesoriosRecepcion=@AccesoriosRecepcion WHERE IdOrden=@IdOrden;
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
