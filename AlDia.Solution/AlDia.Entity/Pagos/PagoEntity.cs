namespace AlDia.Entity.Pagos;
public enum MetodoPago { Efectivo, Tarjeta, Transferencia }
public sealed record PagoSolicitud(int IdOrden, decimal Monto, MetodoPago MetodoPago, string? Observaciones = null);
public sealed record CorreccionPagoSolicitud(int IdPago, decimal Monto, MetodoPago MetodoPago, string Motivo, string? Observaciones = null);
public sealed record PagoEntity(int IdPago, int IdOrden, int IdUsuario, decimal Monto, MetodoPago MetodoPago,
    DateTime FechaPago, string? Observaciones, bool Anulado, DateTime? FechaAnulacion, int? IdUsuarioAnulacion, string? MotivoAnulacion);
