-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_TomarOrden
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
    
    IF @EstadoActual<>'En Revisión' OR @Tecnico IS NOT NULL THROW 51002, 'La orden ya fue tomada o no esta en revision.', 1;
    UPDATE dbo.OrdenesReparacion SET IdTecnicoResponsable=@IdUsuarioActor WHERE IdOrden=@IdOrden;
    EXEC dbo.usp_Interno_Estado @IdOrden,@EstadoActual,@IdUsuarioActor,'El tecnico toma la orden';
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
