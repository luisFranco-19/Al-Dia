-- Ejecutar despues de Base de datos.sql. CREATE OR ALTER permite repetir.
USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Interno_RecalcularTotal
    @IdOrden INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Total DECIMAL(10,2), @Aprobado DECIMAL(10,2);
    SELECT @Total=COALESCE((SELECT SUM(CONVERT(DECIMAL(19,2),Cantidad)*Precio) FROM dbo.DetalleServicios WHERE IdOrden=@IdOrden),0)
        + COALESCE((SELECT SUM(CONVERT(DECIMAL(19,2),Cantidad)*Precio) FROM dbo.DetalleRepuestos WHERE IdOrden=@IdOrden),0);
    SELECT @Aprobado=c.CostoAprobado FROM dbo.Diagnosticos d JOIN dbo.ConfirmacionesReparacion c
    ON c.IdDiagnostico=d.IdDiagnostico WHERE d.IdOrden=@IdOrden AND c.Decision='APROBADA';
    IF @Aprobado IS NULL OR @Total>@Aprobado THROW 51003, 'El total excede el costo autorizado o falta aprobacion.', 1;
    IF @Total < COALESCE((SELECT SUM(Monto) FROM dbo.Pagos WHERE IdOrden=@IdOrden AND Anulado=0),0)
        THROW 51003, 'El total no puede quedar por debajo de los pagos vigentes.', 1;
    UPDATE dbo.OrdenesReparacion SET Total=@Total WHERE IdOrden=@IdOrden;
END;
GO
