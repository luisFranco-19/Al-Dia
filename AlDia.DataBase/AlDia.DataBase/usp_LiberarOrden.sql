-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_LiberarOrden
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
    
    IF @EstadoActual<>'En Revisión' OR EXISTS(SELECT 1 FROM dbo.Diagnosticos WHERE IdOrden=@IdOrden) THROW 51002, 'Solo puede liberar una orden antes de registrar el diagnostico.', 1;
    UPDATE dbo.OrdenesReparacion SET IdTecnicoResponsable=NULL WHERE IdOrden=@IdOrden;
    EXEC dbo.usp_Interno_Estado @IdOrden,@EstadoActual,@IdUsuarioActor,'El tecnico libera la orden';
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
