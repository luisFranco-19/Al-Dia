using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Reparaciones;
namespace AlDia.BLL.Reparaciones;
public sealed class ServicioBll
{
    private readonly IServicioDal _datos;
    private readonly SesionUsuario _sesion;

    public ServicioBll(IServicioDal datos, SesionUsuario sesion)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<int> GuardarAsync(ServicioSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        _sesion.Exigir(RolUsuario.Administrador);
        return GuardarEntidadAsync(new ServicioEntity(s.IdServicio, s.Nombre, s.Descripcion, s.PrecioBase, s.Estado), ct);
    }
    public Task<int> AgregarAsync(ServicioEntity servicio, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(servicio);
        if (servicio.IdServicio != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarEntidadAsync(servicio, ct);
    }

    public Task<int> ActualizarAsync(ServicioEntity servicio, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(servicio);
        Validacion.Id(servicio.IdServicio, "IdServicio");
        return GuardarEntidadAsync(servicio, ct);
    }

    public async Task<int> GuardarEntidadAsync(ServicioEntity servicio, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(servicio);
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        int id = await _datos.GuardarAsync(actor, servicio.CrearSolicitud(), ct);
        servicio.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<ServicioEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return _datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<int> RegistrarEnOrdenAsync(ServicioOrdenSolicitud s, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdOrden = Validacion.Id(s.IdOrden, "IdOrden"), IdServicio = Validacion.Id(s.IdServicio, "IdServicio"), Cantidad = Validacion.Id(s.Cantidad, "Cantidad"), Precio = Validacion.Dinero(s.Precio, "Precio"), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 500) };
        return _datos.RegistrarEnOrdenAsync(actor, validada, ct);
    }
    public Task CorregirEnOrdenAsync(CorreccionServicioOrdenSolicitud s, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdDetalleServicio = Validacion.Id(s.IdDetalleServicio, "IdDetalleServicio"), Cantidad = Validacion.Id(s.Cantidad, "Cantidad"), Precio = Validacion.Dinero(s.Precio, "Precio"), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 500) };
        return _datos.CorregirEnOrdenAsync(actor, validada, ct);
    }
    public Task RetirarDeOrdenAsync(int idDetalleServicio, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        return _datos.RetirarDeOrdenAsync(actor, Validacion.Id(idDetalleServicio, "IdDetalleServicio"), ct);
    }

    public Task<PaginacionEntity<ServicioEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        _datos.ConsultarPaginaAsync(_sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<ServicioEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdServicio"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        _sesion.Exigir(RolUsuario.Administrador);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }
    public async Task<int> RegistrarEntidadEnOrdenAsync(DetalleServicioEntity detalle, CancellationToken ct = default)
    {
        _sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(detalle);
        if (detalle.IdDetalleServicio != 0) throw new ValidacionException("El servicio ya fue registrado en la orden.");
        int id = await RegistrarEnOrdenAsync(detalle.CrearSolicitud(), ct);
        detalle.AsignarId(id);
        return id;
    }
    public Task CorregirEntidadEnOrdenAsync(DetalleServicioEntity detalle, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(detalle);
        return CorregirEnOrdenAsync(detalle.CrearCorreccion(), ct);
    }


    public async Task<PaginacionEntity<ServicioEntity>> MostrarAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool? estado = null, CancellationToken ct = default)
    {
        ConsultaListado.Validar(pagina, tamPagina, buscar);
        if (estado != false)
            return await ConsultarPaginaAsync(pagina, tamPagina, buscar, estado == true, ct);
        var registros = (await ConsultarAsync(soloActivos: false, ct: ct))
            .Where(x => !estado.HasValue || x.Estado == estado.Value);
        return ConsultaListado.Paginar(registros, pagina, tamPagina, buscar, x => [x.Nombre, x.Descripcion]);
    }
}
