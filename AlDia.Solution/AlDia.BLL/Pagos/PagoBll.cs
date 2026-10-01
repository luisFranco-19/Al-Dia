using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Pagos;
namespace AlDia.BLL.Pagos;
public sealed class PagoBll(IPagoDal datos, SesionUsuario sesion)
{
    public Task<int> RegistrarAsync(PagoSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdOrden = Validacion.Id(s.IdOrden, "IdOrden"), Monto = Validacion.Dinero(s.Monto, "Monto", true), MetodoPago = Validacion.Enumeracion(s.MetodoPago, "MetodoPago"), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 300) };
        return datos.RegistrarAsync(actor, validada, ct);
    }
    public Task<int> CorregirAsync(CorreccionPagoSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdPago = Validacion.Id(s.IdPago, "IdPago"), Monto = Validacion.Dinero(s.Monto, "Monto", true), MetodoPago = Validacion.Enumeracion(s.MetodoPago, "MetodoPago"), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 300), Motivo = Validacion.Texto(s.Motivo, "Motivo", 300) };
        return datos.CorregirAsync(actor, validada, ct);
    }
    public Task AnularAsync(int idPago, string motivo, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        return datos.AnularAsync(actor, Validacion.Id(idPago, "IdPago"), Validacion.Texto(motivo, "Motivo", 300), ct);
    }
}
