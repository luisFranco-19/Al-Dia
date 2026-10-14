using AlDia.Entity.Common;

namespace AlDia.Entity.Pagos;

public sealed class PagoEntity
{
    public int IdPago { get; private set; }
    public int IdOrden { get; private set; }
    public int IdUsuario { get; private set; }
    public decimal Monto { get; private set; }
    public MetodoPago MetodoPago { get; private set; }
    public DateTime FechaPago { get; } = DateTime.Now;
    public string? Observaciones { get; private set; }
    public bool Anulado { get; }
    public DateTime? FechaAnulacion { get; }
    public int? IdUsuarioAnulacion { get; }
    public string? MotivoAnulacion { get; }

    public PagoEntity() { }

    public PagoEntity(int idOrden, int idUsuario, decimal monto, MetodoPago metodoPago, string? observaciones = null)
        : this(0, idOrden, idUsuario, monto, metodoPago, DateTime.Now, observaciones, false, null, null, null) { }

    public PagoEntity(int idPago, int idOrden, int idUsuario, decimal monto, MetodoPago metodoPago,
        DateTime fechaPago, string? observaciones, bool anulado, DateTime? fechaAnulacion, int? idUsuarioAnulacion, string? motivoAnulacion)
    {
        Validacion.IdNuevoOExistente(idPago, "IdPago");
        CambiarOrden(idOrden);
        CambiarUsuario(idUsuario);
        CambiarMonto(monto);
        CambiarMetodoPago(metodoPago);
        FechaPago = fechaPago;
        CambiarObservaciones(observaciones);
        if (anulado && (!fechaAnulacion.HasValue || !idUsuarioAnulacion.HasValue || string.IsNullOrWhiteSpace(motivoAnulacion)))
            throw new ValidacionException("Un pago anulado requiere fecha, usuario y motivo de anulacion.");
        if (!anulado && (fechaAnulacion.HasValue || idUsuarioAnulacion.HasValue || !string.IsNullOrWhiteSpace(motivoAnulacion)))
            throw new ValidacionException("Un pago vigente no debe contener datos de anulacion.");
        Anulado = anulado;
        FechaAnulacion = fechaAnulacion;
        IdUsuarioAnulacion = idUsuarioAnulacion.HasValue ? Validacion.Id(idUsuarioAnulacion.Value, "IdUsuarioAnulacion") : null;
        MotivoAnulacion = string.IsNullOrWhiteSpace(motivoAnulacion) ? null : motivoAnulacion.Trim();
        if (idPago > 0) AsignarId(idPago);
    }

    public void AsignarId(int id) => IdPago = Validacion.AsignarId(IdPago, id, "IdPago");

    private void ExigirPagoNuevo()
    {
        if (IdPago != 0 || Anulado)
            throw new ValidacionException("Los pagos guardados se corrigen mediante un reemplazo; no se modifican en memoria.");
    }

    public void CambiarOrden(int idOrden) { ExigirPagoNuevo(); IdOrden = Validacion.Id(idOrden, "IdOrden"); }
    public void CambiarUsuario(int idUsuario) { ExigirPagoNuevo(); IdUsuario = Validacion.Id(idUsuario, "IdUsuario"); }
    public void CambiarMonto(decimal monto) { ExigirPagoNuevo(); Monto = Validacion.Dinero(monto, "Monto", true); }
    public void CambiarMetodoPago(MetodoPago metodoPago) { ExigirPagoNuevo(); MetodoPago = Validacion.Enumeracion(metodoPago, "MetodoPago"); }
    public void CambiarObservaciones(string? observaciones)
    {
        ExigirPagoNuevo();
        Observaciones = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim();
    }

    public PagoSolicitud CrearSolicitud()
    {
        Validacion.Id(IdOrden, "IdOrden");
        Validacion.Id(IdUsuario, "IdUsuario");
        Validacion.Dinero(Monto, "Monto", true);
        Validacion.Enumeracion(MetodoPago, "MetodoPago");
        return new(IdOrden, Monto, MetodoPago, Observaciones);
    }

    // SQL anula el pago anterior y crea uno nuevo; no se sobrescribe el importe historico.
    public CorreccionPagoSolicitud CrearCorreccion(decimal monto, MetodoPago metodoPago, string motivo, string? observaciones = null)
    {
        if (Anulado) throw new ValidacionException("No se puede corregir un pago anulado.");
        if (string.IsNullOrWhiteSpace(motivo))
            throw new ValidacionException("Debe ingresar el motivo de la correccion.");
        return new(Validacion.Id(IdPago, "IdPago"), Validacion.Dinero(monto, "Monto", true),
            Validacion.Enumeracion(metodoPago, "MetodoPago"), motivo.Trim(),
            string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim());
    }

    public AnulacionPagoSolicitud CrearAnulacion(string motivo)
    {
        if (Anulado) throw new ValidacionException("El pago ya esta anulado.");
        if (string.IsNullOrWhiteSpace(motivo))
            throw new ValidacionException("Debe ingresar el motivo de la anulacion.");
        return new(Validacion.Id(IdPago, "IdPago"), motivo.Trim());
    }
}
