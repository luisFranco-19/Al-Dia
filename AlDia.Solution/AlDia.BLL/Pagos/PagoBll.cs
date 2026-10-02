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
    public Task<IReadOnlyList<PagoEntity>> ConsultarAsync(int? idOrden = null, bool incluirAnulados = false, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        if (idOrden.HasValue) Validacion.Id(idOrden.Value, "Orden");
        return datos.ConsultarAsync(actor, idOrden, incluirAnulados, ct);
    }
    public Task<PaginacionEntity<PagoEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, int? idOrden = null, bool incluirAnulados = false, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        if (idOrden.HasValue) Validacion.Id(idOrden.Value, "Orden");
        return datos.ConsultarPaginaAsync(actor, idOrden, incluirAnulados, pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), ct);
    }
    public async Task<int> RegistrarEntidadAsync(PagoEntity pago, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(pago);
        if (pago.IdPago != 0 || pago.Anulado) throw new ValidacionException("El pago ya fue registrado o anulado.");
        if (pago.IdUsuario != actor) throw new PermisoException("El pago debe corresponder al usuario autenticado.");
        int id = await RegistrarAsync(pago.CrearSolicitud(), ct);
        pago.AsignarId(id);
        return id;
    }
    // SQL anula el original y crea otro pago; se conserva la entidad historica.
    public Task<int> CorregirEntidadAsync(PagoEntity pago, decimal monto, MetodoPago metodo, string motivo,
        string? observaciones = null, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(pago);
        return CorregirAsync(pago.CrearCorreccion(monto, metodo, motivo, observaciones), ct);
    }
    public Task AnularEntidadAsync(PagoEntity pago, string motivo, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(pago);
        var solicitud = pago.CrearAnulacion(motivo);
        return AnularAsync(solicitud.IdPago, solicitud.Motivo, ct);
    }

    public Task<PagoEntity?> ObtenerPorIdAsync(int idPago, CancellationToken ct = default) =>
        datos.ObtenerPorIdAsync(sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista),
            Validacion.Id(idPago, "Pago"), ct);
}
