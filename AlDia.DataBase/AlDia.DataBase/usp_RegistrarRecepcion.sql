-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_RegistrarRecepcion
    @IdUsuarioActor INT, @NumeroOrden VARCHAR(20), @IdEquipo INT, @ProblemaReportado VARCHAR(500), @ObservacionesRecepcion VARCHAR(500), @AccesoriosRecepcion VARCHAR(300), @IdOrden INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    IF NULLIF(LTRIM(RTRIM(@NumeroOrden)), '') IS NULL THROW 51000, 'El campo NumeroOrden es obligatorio.', 1;
    SET @NumeroOrden=LTRIM(RTRIM(@NumeroOrden));
    IF NULLIF(LTRIM(RTRIM(@ProblemaReportado)), '') IS NULL THROW 51000, 'El campo ProblemaReportado es obligatorio.', 1;
    SET @ProblemaReportado=LTRIM(RTRIM(@ProblemaReportado));
    
    IF NOT EXISTS(SELECT 1 FROM dbo.Equipos e JOIN dbo.Clientes c ON c.IdCliente=e.IdCliente WHERE e.IdEquipo=@IdEquipo AND e.Estado=1 AND c.Estado=1) THROW 51000, 'Equipo o cliente inexistente o inactivo.', 1;
    DECLARE @Estado INT=(SELECT IdEstado FROM dbo.EstadosReparacion WHERE Nombre='En Revisión');
    INSERT dbo.OrdenesReparacion(NumeroOrden,IdEquipo,IdRecepcionista,IdEstado,ProblemaReportado,ObservacionesRecepcion,AccesoriosRecepcion)
    VALUES(@NumeroOrden,@IdEquipo,@IdUsuarioActor,@Estado,@ProblemaReportado,@ObservacionesRecepcion,@AccesoriosRecepcion);
    SET @IdOrden=CONVERT(INT,SCOPE_IDENTITY());
    INSERT dbo.HistorialEstados(IdOrden,IdEstadoAnterior,IdEstadoNuevo,IdUsuario,Observacion)
    VALUES(@IdOrden,NULL,@Estado,@IdUsuarioActor,'Recepcion del equipo');
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
