using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Reparaciones;
namespace AlDia.BLL.Reparaciones;
public sealed class OrdenReparacionBll(IOrdenReparacionDal datos, SesionUsuario sesion)
{
    public Task<int> RegistrarRecepcionAsync(RecepcionSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { NumeroOrden = Validacion.Texto(s.NumeroOrden, "NumeroOrden", 20), IdEquipo = Validacion.Id(s.IdEquipo, "IdEquipo"), ProblemaReportado = Validacion.Texto(s.ProblemaReportado, "ProblemaReportado", 500), ObservacionesRecepcion = Validacion.Opcional(s.ObservacionesRecepcion, "ObservacionesRecepcion", 500), AccesoriosRecepcion = Validacion.Opcional(s.AccesoriosRecepcion, "AccesoriosRecepcion", 300) };
        return datos.RegistrarRecepcionAsync(actor, validada, ct);
    }
    public Task CorregirRecepcionAsync(CorreccionRecepcionSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdOrden = Validacion.Id(s.IdOrden, "IdOrden"), ProblemaReportado = Validacion.Texto(s.ProblemaReportado, "ProblemaReportado", 500), ObservacionesRecepcion = Validacion.Opcional(s.ObservacionesRecepcion, "ObservacionesRecepcion", 500), AccesoriosRecepcion = Validacion.Opcional(s.AccesoriosRecepcion, "AccesoriosRecepcion", 300) };
        return datos.CorregirRecepcionAsync(actor, validada, ct);
    }
    public Task AnularAsync(int idOrden, string motivo, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        return datos.AnularAsync(actor, Validacion.Id(idOrden, "IdOrden"), Validacion.Texto(motivo, "Motivo", 500), ct);
    }
    public Task TomarAsync(int idOrden, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        return datos.TomarAsync(actor, Validacion.Id(idOrden, "IdOrden"), ct);
    }
    public Task LiberarAsync(int idOrden, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        return datos.LiberarAsync(actor, Validacion.Id(idOrden, "IdOrden"), ct);
    }
    public Task IniciarReparacionAsync(int idOrden, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        return datos.IniciarReparacionAsync(actor, Validacion.Id(idOrden, "IdOrden"), ct);
    }
    public Task FinalizarReparacionAsync(int idOrden, bool reparada, string observaciones, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        return datos.FinalizarReparacionAsync(actor, Validacion.Id(idOrden, "IdOrden"), reparada, Validacion.Texto(observaciones, "Observaciones", 500), ct);
    }
    public Task EntregarAsync(int idOrden, string? observacion, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        return datos.EntregarAsync(actor, Validacion.Id(idOrden, "IdOrden"), Validacion.Opcional(observacion, "Observacion", 500), ct);
    }

    public Task<IReadOnlyList<OrdenReparacionEntity>> ConsultarAsync(FiltroOrdenes? filtro = null, CancellationToken ct = default)
    {
        int actor = sesion.Exigir();
        filtro ??= new FiltroOrdenes();
        if (filtro.IdOrden.HasValue) Validacion.Id(filtro.IdOrden.Value, "Orden");
        if (filtro.IdEstado.HasValue) Validacion.Id(filtro.IdEstado.Value, "Estado");
        if (filtro.IdCliente.HasValue) Validacion.Id(filtro.IdCliente.Value, "Cliente");
        return datos.ConsultarAsync(actor, filtro, ct);
    }
    public Task<IReadOnlyList<EstadoReparacionEntity>> ConsultarEstadosAsync(CancellationToken ct = default) =>
        datos.ConsultarEstadosAsync(sesion.Exigir(), ct);
    public Task<ExpedienteOrdenEntity?> ConsultarExpedienteAsync(int idOrden, CancellationToken ct = default) =>
        datos.ConsultarExpedienteAsync(sesion.Exigir(), Validacion.Id(idOrden, "Orden"), ct);

}
