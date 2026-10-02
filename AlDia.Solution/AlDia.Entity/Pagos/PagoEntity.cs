using AlDia.Entity.Common;

namespace AlDia.Entity.Pagos;

public sealed class PagoEntity
{
    public int IdPago { get; private set; }
    public int IdOrden { get; }
    public int IdUsuario { get; }
    public decimal Monto { get; }
    public MetodoPago MetodoPago { get; }
    public DateTime FechaPago { get; }
    public string? Observaciones { get; }
    public bool Anulado { get; }
    public DateTime? FechaAnulacion { get; }
    public int? IdUsuarioAnulacion { get; }
    public string? MotivoAnulacion { get; }

    public PagoEntity(int idOrden, int idUsuario, decimal monto, MetodoPago metodoPago, string? observaciones = null)
        : this(0, idOrden, idUsuario, monto, metodoPago, DateTime.Now, observaciones, false, null, null, null) { }

    public PagoEntity(int idPago, int idOrden, int idUsuario, decimal monto, MetodoPago metodoPago,
        DateTime fechaPago, string? observaciones, bool anulado, DateTime? fechaAnulacion, int? idUsuarioAnulacion, string? motivoAnulacion)
    {
        IdPago = Validacion.IdNuevoOExistente(idPago, "IdPago");
        IdOrden = Validacion.Id(idOrden, "IdOrden");
        IdUsuario = Validacion.Id(idUsuario, "IdUsuario");
        Monto = Validacion.Dinero(monto, "Monto", true);
        MetodoPago = Validacion.Enumeracion(metodoPago, "MetodoPago");
        FechaPago = fechaPago;
        Observaciones = Validacion.Opcional(observaciones, "Observaciones", 300);
        if (anulado && (!fechaAnulacion.HasValue || !idUsuarioAnulacion.HasValue || string.IsNullOrWhiteSpace(motivoAnulacion)))
            throw new ValidacionException("Un pago anulado requiere fecha, usuario y motivo de anulacion.");
        if (!anulado && (fechaAnulacion.HasValue || idUsuarioAnulacion.HasValue || !string.IsNullOrWhiteSpace(motivoAnulacion)))
            throw new ValidacionException("Un pago vigente no debe contener datos de anulacion.");
        Anulado = anulado;
        FechaAnulacion = fechaAnulacion;
        IdUsuarioAnulacion = idUsuarioAnulacion.HasValue ? Validacion.Id(idUsuarioAnulacion.Value, "IdUsuarioAnulacion") : null;
        MotivoAnulacion = Validacion.Opcional(motivoAnulacion, "MotivoAnulacion", 300);
    }

    public void AsignarId(int id) => IdPago = Validacion.AsignarId(IdPago, id, "IdPago");
    public PagoSolicitud CrearSolicitud() => new(IdOrden, Monto, MetodoPago, Observaciones);

    // SQL anula el pago anterior y crea uno nuevo; no se sobrescribe el importe historico.
    public CorreccionPagoSolicitud CrearCorreccion(decimal monto, MetodoPago metodoPago, string motivo, string? observaciones = null)
    {
        if (Anulado) throw new ValidacionException("No se puede corregir un pago anulado.");
        return new(Validacion.Id(IdPago, "IdPago"), Validacion.Dinero(monto, "Monto", true),
            Validacion.Enumeracion(metodoPago, "MetodoPago"), Validacion.Texto(motivo, "Motivo", 300),
            Validacion.Opcional(observaciones, "Observaciones", 300));
    }

    public AnulacionPagoSolicitud CrearAnulacion(string motivo)
    {
        if (Anulado) throw new ValidacionException("El pago ya esta anulado.");
        return new(Validacion.Id(IdPago, "IdPago"), Validacion.Texto(motivo, "Motivo", 300));
    }
}
