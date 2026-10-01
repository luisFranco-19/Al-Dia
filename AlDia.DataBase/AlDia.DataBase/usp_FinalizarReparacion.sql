-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FinalizarReparacion
    @IdUsuarioActor INT, @IdOrden INT, @Reparada BIT, @Observaciones VARCHAR(500)
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
    IF NULLIF(LTRIM(RTRIM(@Observaciones)), '') IS NULL THROW 51000, 'El campo Observaciones es obligatorio.', 1;
    SET @Observaciones=LTRIM(RTRIM(@Observaciones));
    
    IF @EstadoActual<>'En Reparación' OR @Reparada IS NULL THROW 51002, 'La orden debe estar en reparacion y debe indicar el resultado.', 1;
    EXEC dbo.usp_Interno_RecalcularTotal @IdOrden;
    DECLARE @Nuevo VARCHAR(50)=CASE WHEN @Reparada=1 THEN 'Reparada' ELSE 'No Reparada' END;
    EXEC dbo.usp_Interno_Estado @IdOrden,@Nuevo,@IdUsuarioActor,@Observaciones;
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
