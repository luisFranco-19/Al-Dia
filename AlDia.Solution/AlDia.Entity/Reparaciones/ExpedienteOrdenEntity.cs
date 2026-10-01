using AlDia.Entity.Pagos;
namespace AlDia.Entity.Reparaciones;
public sealed record EstadoReparacionEntity(int IdEstado, string Nombre, string? Descripcion);
public sealed record OrdenReparacionEntity(int IdOrden, string NumeroOrden, int IdEquipo, int IdRecepcionista, int IdEstado,
    string EstadoNombre, int? IdTecnicoResponsable, DateTime FechaRecepcion, string ProblemaReportado, string? ObservacionesRecepcion,
    string? AccesoriosRecepcion, DateTime? FechaEntrega, bool Anulada, decimal Total, decimal Pagado, decimal Saldo,
    int IdCliente, string NombreCliente, string ApellidoCliente);
public sealed record DiagnosticoEntity(int IdDiagnostico, int IdOrden, int IdTecnico, DateTime FechaDiagnostico,
    string ProblemaEncontrado, string ReparacionPropuesta, decimal CostoEstimado, string? Observaciones);
public sealed record ConfirmacionEntity(int IdConfirmacion, int IdDiagnostico, int IdUsuario, DecisionCliente Decision,
    decimal? CostoAprobado, DateTime FechaConfirmacion, string? Observaciones);
public sealed record DetalleServicioEntity(int IdDetalleServicio, int IdOrden, int IdServicio, int IdTecnico,
    int Cantidad, decimal Precio, string? Observaciones);
public sealed record DetalleRepuestoEntity(int IdDetalleRepuesto, int IdOrden, int IdRepuesto, int IdTecnico, int Cantidad, decimal Precio);
public sealed record HistorialEstadoEntity(int IdHistorial, int IdOrden, int? IdEstadoAnterior, int IdEstadoNuevo,
    int IdUsuario, DateTime FechaCambio, string? Observacion);
public sealed record ExpedienteOrdenEntity(OrdenReparacionEntity Orden, DiagnosticoEntity? Diagnostico,
    ConfirmacionEntity? Confirmacion, IReadOnlyList<DetalleServicioEntity> Servicios,
    IReadOnlyList<DetalleRepuestoEntity> Repuestos, IReadOnlyList<PagoEntity> Pagos,
    IReadOnlyList<HistorialEstadoEntity> Historial);
