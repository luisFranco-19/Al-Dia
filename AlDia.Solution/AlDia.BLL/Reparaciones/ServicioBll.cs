using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Reparaciones;
namespace AlDia.BLL.Reparaciones;
public sealed class ServicioBll(IServicioDal datos, SesionUsuario sesion)
{
    public Task<int> GuardarAsync(ServicioSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        sesion.Exigir(RolUsuario.Administrador);
        return GuardarEntidadAsync(new ServicioEntity(s.IdServicio, s.Nombre, s.Descripcion, s.PrecioBase, s.Estado), ct);
    }
    public async Task<int> GuardarEntidadAsync(ServicioEntity servicio, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(servicio);
        int actor = sesion.Exigir(RolUsuario.Administrador);
        int id = await datos.GuardarAsync(actor, servicio.CrearSolicitud(), ct);
        servicio.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<ServicioEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<int> RegistrarEnOrdenAsync(ServicioOrdenSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdOrden = Validacion.Id(s.IdOrden, "IdOrden"), IdServicio = Validacion.Id(s.IdServicio, "IdServicio"), Cantidad = Validacion.Id(s.Cantidad, "Cantidad"), Precio = Validacion.Dinero(s.Precio, "Precio"), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 500) };
        return datos.RegistrarEnOrdenAsync(actor, validada, ct);
    }
    public Task CorregirEnOrdenAsync(CorreccionServicioOrdenSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdDetalleServicio = Validacion.Id(s.IdDetalleServicio, "IdDetalleServicio"), Cantidad = Validacion.Id(s.Cantidad, "Cantidad"), Precio = Validacion.Dinero(s.Precio, "Precio"), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 500) };
        return datos.CorregirEnOrdenAsync(actor, validada, ct);
    }
    public Task RetirarDeOrdenAsync(int idDetalleServicio, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        return datos.RetirarDeOrdenAsync(actor, Validacion.Id(idDetalleServicio, "IdDetalleServicio"), ct);
    }

    public Task<PaginacionEntity<ServicioEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        datos.ConsultarPaginaAsync(sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<ServicioEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdServicio"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        sesion.Exigir(RolUsuario.Administrador);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }
    public async Task<int> RegistrarEntidadEnOrdenAsync(DetalleServicioEntity detalle, CancellationToken ct = default)
    {
        sesion.Exigir(RolUsuario.Tecnico);
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

}
