USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarExpedienteOrden
 @IdUsuarioActor INT, @IdOrden INT
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT o.*, e.Nombre AS EstadoNombre, c.IdCliente, c.Nombre AS NombreCliente, c.Apellido AS ApellidoCliente,
 COALESCE(p.Pagado,0) AS Pagado, o.Total-COALESCE(p.Pagado,0) AS Saldo
FROM dbo.OrdenesReparacion o JOIN dbo.EstadosReparacion e ON e.IdEstado=o.IdEstado
JOIN dbo.Equipos eq ON eq.IdEquipo=o.IdEquipo JOIN dbo.Clientes c ON c.IdCliente=eq.IdCliente
OUTER APPLY(SELECT SUM(Monto) AS Pagado FROM dbo.Pagos WHERE IdOrden=o.IdOrden AND Anulado=0) p
WHERE o.IdOrden=@IdOrden;
SELECT * FROM dbo.Diagnosticos WHERE IdOrden=@IdOrden;
SELECT c.* FROM dbo.ConfirmacionesReparacion c JOIN dbo.Diagnosticos d ON d.IdDiagnostico=c.IdDiagnostico WHERE d.IdOrden=@IdOrden;
SELECT * FROM dbo.DetalleServicios WHERE IdOrden=@IdOrden ORDER BY IdDetalleServicio;
SELECT * FROM dbo.DetalleRepuestos WHERE IdOrden=@IdOrden ORDER BY IdDetalleRepuesto;
SELECT * FROM dbo.Pagos WHERE IdOrden=@IdOrden ORDER BY IdPago;
SELECT * FROM dbo.HistorialEstados WHERE IdOrden=@IdOrden ORDER BY IdHistorial;
END;
GO
