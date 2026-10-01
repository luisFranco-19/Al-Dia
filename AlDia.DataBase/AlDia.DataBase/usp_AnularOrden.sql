-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_AnularOrden
    @IdUsuarioActor INT, @IdOrden INT, @Motivo VARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Recepcionista';
    DECLARE @EstadoActual VARCHAR(50), @Tecnico INT;
    EXEC dbo.usp_Interno_BloquearOrden @IdOrden, @EstadoActual OUTPUT, @Tecnico OUTPUT;
    IF NULLIF(LTRIM(RTRIM(@Motivo)), '') IS NULL THROW 51000, 'El campo Motivo es obligatorio.', 1;
    SET @Motivo=LTRIM(RTRIM(@Motivo));
    
    IF @EstadoActual<>'En Revisión' OR @Tecnico IS NOT NULL OR EXISTS(SELECT 1 FROM dbo.Diagnosticos WHERE IdOrden=@IdOrden) THROW 51002, 'Solo se anulan ordenes sin trabajo iniciado.', 1;
    UPDATE dbo.OrdenesReparacion SET Anulada=1 WHERE IdOrden=@IdOrden;
    DECLARE @Nota VARCHAR(500)=LEFT('Anulacion: '+@Motivo,500);
    EXEC dbo.usp_Interno_Estado @IdOrden,@EstadoActual,@IdUsuarioActor,@Nota;
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
