-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_AjustarExistencias
    @IdUsuarioActor INT, @IdRepuesto INT, @Variacion INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
    BEGIN TRANSACTION;
    EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor, 'Administrador';
    
    IF @Variacion IS NULL OR @Variacion=0 THROW 51000, 'Indique una variacion distinta de cero.', 1;
    UPDATE dbo.Repuestos WITH (UPDLOCK) SET Stock=Stock+@Variacion WHERE IdRepuesto=@IdRepuesto AND Estado=1 AND CONVERT(BIGINT,Stock)+@Variacion BETWEEN 0 AND 2147483647;
    IF @@ROWCOUNT=0 THROW 51000, 'Repuesto inactivo, inexistente o ajuste fuera del rango de stock.', 1;
    
    COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
