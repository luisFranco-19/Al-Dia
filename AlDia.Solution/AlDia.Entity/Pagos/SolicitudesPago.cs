namespace AlDia.Entity.Pagos;

public enum MetodoPago { Efectivo, Tarjeta, Transferencia }
public sealed record PagoSolicitud(int IdOrden, decimal Monto, MetodoPago MetodoPago, string? Observaciones = null);
public sealed record CorreccionPagoSolicitud(int IdPago, decimal Monto, MetodoPago MetodoPago, string Motivo, string? Observaciones = null);
public sealed record AnulacionPagoSolicitud(int IdPago, string Motivo);
