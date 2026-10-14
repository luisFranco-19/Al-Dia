using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Reparaciones;
namespace AlDia.BLL.Reparaciones;
public sealed class OrdenReparacionBll
{
    private readonly IOrdenReparacionDal _datos;
    private readonly SesionUsuario _sesion;

    public OrdenReparacionBll(IOrdenReparacionDal datos, SesionUsuario sesion)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<PaginacionEntity<OrdenReparacionEntity>> MostrarAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, int? idEstado = null, bool? tecnicoAsignado = null, bool soloAnuladas = false,
        CancellationToken ct = default)
    {
        ConsultaListado.Validar(pagina, tamPagina, buscar);
        var filtro = new FiltroOrdenes(IdEstado: idEstado, IncluirAnuladas: soloAnuladas);
        if (!tecnicoAsignado.HasValue && !soloAnuladas)
            return ConsultarPaginaAsync(pagina, tamPagina, buscar, filtro, ct);
        return ConsultaListado.FiltrarPaginasAsync(pagina, tamPagina,
            p => ConsultarPaginaAsync(p, 200, buscar, filtro, ct),
            x => (!tecnicoAsignado.HasValue || x.IdTecnicoResponsable.HasValue == tecnicoAsignado.Value)
                && (!soloAnuladas || x.Anulada), ct);
    }

    public Task<int> RegistrarRecepcionAsync(RecepcionSolicitud s, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { NumeroOrden = Validacion.Texto(s.NumeroOrden, "NumeroOrden", 20), IdEquipo = Validacion.Id(s.IdEquipo, "IdEquipo"), ProblemaReportado = Validacion.Texto(s.ProblemaReportado, "ProblemaReportado", 500), ObservacionesRecepcion = Validacion.Opcional(s.ObservacionesRecepcion, "ObservacionesRecepcion", 500), AccesoriosRecepcion = Validacion.Opcional(s.AccesoriosRecepcion, "AccesoriosRecepcion", 300) };
        return _datos.RegistrarRecepcionAsync(actor, validada, ct);
    }
    public Task CorregirRecepcionAsync(CorreccionRecepcionSolicitud s, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdOrden = Validacion.Id(s.IdOrden, "IdOrden"), ProblemaReportado = Validacion.Texto(s.ProblemaReportado, "ProblemaReportado", 500), ObservacionesRecepcion = Validacion.Opcional(s.ObservacionesRecepcion, "ObservacionesRecepcion", 500), AccesoriosRecepcion = Validacion.Opcional(s.AccesoriosRecepcion, "AccesoriosRecepcion", 300) };
        return _datos.CorregirRecepcionAsync(actor, validada, ct);
    }
    public Task AnularAsync(int idOrden, string motivo, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Recepcionista);
        return _datos.AnularAsync(actor, Validacion.Id(idOrden, "IdOrden"), Validacion.Texto(motivo, "Motivo", 500), ct);
    }
    public Task TomarAsync(int idOrden, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        return _datos.TomarAsync(actor, Validacion.Id(idOrden, "IdOrden"), ct);
    }
    public Task LiberarAsync(int idOrden, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        return _datos.LiberarAsync(actor, Validacion.Id(idOrden, "IdOrden"), ct);
    }
    public Task IniciarReparacionAsync(int idOrden, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        return _datos.IniciarReparacionAsync(actor, Validacion.Id(idOrden, "IdOrden"), ct);
    }
    public Task FinalizarReparacionAsync(int idOrden, bool reparada, string observaciones, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        return _datos.FinalizarReparacionAsync(actor, Validacion.Id(idOrden, "IdOrden"), reparada, Validacion.Texto(observaciones, "Observaciones", 500), ct);
    }
    public Task EntregarAsync(int idOrden, string? observacion, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Recepcionista);
        return _datos.EntregarAsync(actor, Validacion.Id(idOrden, "IdOrden"), Validacion.Opcional(observacion, "Observacion", 500), ct);
    }

    public Task<IReadOnlyList<OrdenReparacionEntity>> ConsultarAsync(FiltroOrdenes? filtro = null, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir();
        filtro ??= new FiltroOrdenes();
        if (filtro.IdOrden.HasValue) Validacion.Id(filtro.IdOrden.Value, "Orden");
        if (filtro.IdEstado.HasValue) Validacion.Id(filtro.IdEstado.Value, "Estado");
        if (filtro.IdCliente.HasValue) Validacion.Id(filtro.IdCliente.Value, "Cliente");
        return _datos.ConsultarAsync(actor, filtro, ct);
    }
    public Task<IReadOnlyList<EstadoReparacionEntity>> ConsultarEstadosAsync(CancellationToken ct = default) =>
        _datos.ConsultarEstadosAsync(_sesion.Exigir(), ct);
    public Task<ExpedienteOrdenEntity?> ConsultarExpedienteAsync(int idOrden, CancellationToken ct = default) =>
        _datos.ConsultarExpedienteAsync(_sesion.Exigir(), Validacion.Id(idOrden, "Orden"), ct);

    public Task<PaginacionEntity<OrdenReparacionEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, FiltroOrdenes? filtro = null, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir();
        string? texto = ConsultaListado.Validar(pagina, tamPagina, buscar);
        filtro ??= new FiltroOrdenes();
        if (filtro.IdOrden.HasValue) Validacion.Id(filtro.IdOrden.Value, "Orden");
        if (filtro.IdEstado.HasValue) Validacion.Id(filtro.IdEstado.Value, "Estado");
        if (filtro.IdCliente.HasValue) Validacion.Id(filtro.IdCliente.Value, "Cliente");
        return _datos.ConsultarPaginaAsync(actor, filtro, pagina, tamPagina, texto, ct);
    }
    public async Task<OrdenReparacionEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(new FiltroOrdenes(IdOrden: Validacion.Id(id, "Orden"), IncluirAnuladas: true), ct)).SingleOrDefault();
    public async Task<int> RegistrarEntidadAsync(OrdenReparacionEntity orden, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(orden);
        if (orden.IdOrden != 0) throw new ValidacionException("La orden ya fue registrada.");
        if (orden.IdRecepcionista != actor) throw new PermisoException("La recepcion debe corresponder al usuario autenticado.");
        int id = await RegistrarRecepcionAsync(orden.CrearSolicitud(), ct);
        orden.AsignarId(id);
        return id;
    }
    public Task CorregirEntidadAsync(OrdenReparacionEntity orden, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(orden);
        return CorregirRecepcionAsync(orden.CrearCorreccion(), ct);
    }

}
