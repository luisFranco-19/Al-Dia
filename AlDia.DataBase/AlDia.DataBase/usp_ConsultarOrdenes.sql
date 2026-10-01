USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarOrdenes
 @IdUsuarioActor INT, @IdOrden INT=NULL, @IdEstado INT=NULL, @IdCliente INT=NULL, @SoloDisponibles BIT=0, @IncluirAnuladas BIT=0
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT o.*, e.Nombre AS EstadoNombre, c.IdCliente, c.Nombre AS NombreCliente, c.Apellido AS ApellidoCliente,
 COALESCE(p.Pagado,0) AS Pagado, o.Total-COALESCE(p.Pagado,0) AS Saldo
FROM dbo.OrdenesReparacion o JOIN dbo.EstadosReparacion e ON e.IdEstado=o.IdEstado
JOIN dbo.Equipos eq ON eq.IdEquipo=o.IdEquipo JOIN dbo.Clientes c ON c.IdCliente=eq.IdCliente
OUTER APPLY(SELECT SUM(Monto) AS Pagado FROM dbo.Pagos WHERE IdOrden=o.IdOrden AND Anulado=0) p
WHERE (@IdOrden IS NULL OR o.IdOrden=@IdOrden) AND (@IdEstado IS NULL OR o.IdEstado=@IdEstado)
 AND (@IdCliente IS NULL OR c.IdCliente=@IdCliente) AND (@IncluirAnuladas=1 OR o.Anulada=0)
 AND (@SoloDisponibles=0 OR (e.Nombre='En Revisión' AND o.IdTecnicoResponsable IS NULL AND o.Anulada=0)) ORDER BY o.IdOrden DESC;
END;
GO
