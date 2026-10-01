namespace AlDia.Entity.Reparaciones;
public enum DecisionCliente { Aprobada, Rechazada }
public sealed record RecepcionSolicitud(string NumeroOrden, int IdEquipo, string ProblemaReportado, string? ObservacionesRecepcion = null, string? AccesoriosRecepcion = null);
public sealed record CorreccionRecepcionSolicitud(int IdOrden, string ProblemaReportado, string? ObservacionesRecepcion = null, string? AccesoriosRecepcion = null);
public sealed record DiagnosticoSolicitud(int IdOrden, string ProblemaEncontrado, string ReparacionPropuesta, decimal CostoEstimado, string? Observaciones = null);
public sealed record CorreccionDiagnosticoSolicitud(int IdDiagnostico, string ProblemaEncontrado, string ReparacionPropuesta, decimal CostoEstimado, string? Observaciones = null);
public sealed record DecisionClienteSolicitud(int IdDiagnostico, DecisionCliente Decision, decimal? CostoAprobado, string? Observaciones = null);
public sealed record CorreccionDecisionClienteSolicitud(int IdConfirmacion, DecisionCliente Decision, decimal? CostoAprobado, string? Observaciones = null);
public sealed record ServicioOrdenSolicitud(int IdOrden, int IdServicio, int Cantidad, decimal Precio, string? Observaciones = null);
public sealed record CorreccionServicioOrdenSolicitud(int IdDetalleServicio, int Cantidad, decimal Precio, string? Observaciones = null);
public sealed record FiltroOrdenes(int? IdOrden = null, int? IdEstado = null, int? IdCliente = null, bool SoloDisponibles = false, bool IncluirAnuladas = false);
