using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Inventario;
namespace AlDia.BLL.Inventario;
public sealed class RepuestoBll(IRepuestoDal datos, SesionUsuario sesion)
{
    public Task<int> GuardarAsync(RepuestoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        sesion.Exigir(RolUsuario.Administrador);
        return GuardarEntidadAsync(new RepuestoEntity(s.IdRepuesto, s.Nombre, s.Descripcion, s.Marca,
            s.NumeroParte, s.PrecioCompra, s.PrecioVenta, s.Estado, 0), ct);
    }
    public async Task<int> GuardarEntidadAsync(RepuestoEntity repuesto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(repuesto);
        int actor = sesion.Exigir(RolUsuario.Administrador);
        int id = await datos.GuardarAsync(actor, repuesto.CrearSolicitud(), ct);
        repuesto.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<RepuestoEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<int> ConsumirAsync(ConsumoRepuestoSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdOrden = Validacion.Id(s.IdOrden, "IdOrden"), IdRepuesto = Validacion.Id(s.IdRepuesto, "IdRepuesto"), Cantidad = Validacion.Id(s.Cantidad, "Cantidad"), Precio = Validacion.Dinero(s.Precio, "Precio") };
        return datos.ConsumirAsync(actor, validada, ct);
    }
    public Task CorregirConsumoAsync(CorreccionConsumoRepuestoSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdDetalleRepuesto = Validacion.Id(s.IdDetalleRepuesto, "IdDetalleRepuesto"), Cantidad = Validacion.Id(s.Cantidad, "Cantidad"), Precio = Validacion.Dinero(s.Precio, "Precio") };
        return datos.CorregirConsumoAsync(actor, validada, ct);
    }
    public Task RetirarConsumoAsync(int idDetalleRepuesto, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        return datos.RetirarConsumoAsync(actor, Validacion.Id(idDetalleRepuesto, "IdDetalleRepuesto"), ct);
    }
    public Task AjustarExistenciasAsync(int idRepuesto, int variacion, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Administrador);
        if (variacion == 0) throw new ValidacionException("La variacion de existencias debe ser distinta de cero.");
        return datos.AjustarExistenciasAsync(actor, Validacion.Id(idRepuesto, "IdRepuesto"), variacion, ct);
    }
}
