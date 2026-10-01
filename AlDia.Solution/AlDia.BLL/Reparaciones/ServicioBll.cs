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
}
