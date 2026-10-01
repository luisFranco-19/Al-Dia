namespace AlDia.Entity.Inventario;
public sealed record ConsumoRepuestoSolicitud(int IdOrden, int IdRepuesto, int Cantidad, decimal Precio);
public sealed record CorreccionConsumoRepuestoSolicitud(int IdDetalleRepuesto, int Cantidad, decimal Precio);
