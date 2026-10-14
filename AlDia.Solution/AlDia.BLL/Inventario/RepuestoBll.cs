using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Inventario;
using AlDia.Entity.Reparaciones;
namespace AlDia.BLL.Inventario;
public sealed class RepuestoBll
{
    private readonly IRepuestoDal _datos;
    private readonly SesionUsuario _sesion;

    public RepuestoBll(IRepuestoDal datos, SesionUsuario sesion)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<int> GuardarAsync(RepuestoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        _sesion.Exigir(RolUsuario.Administrador);
        return GuardarEntidadAsync(new RepuestoEntity(s.IdRepuesto, s.Nombre, s.Descripcion, s.Marca,
            s.NumeroParte, s.PrecioCompra, s.PrecioVenta, s.Estado, 0), ct);
    }
    public Task<int> AgregarAsync(RepuestoEntity repuesto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(repuesto);
        if (repuesto.IdRepuesto != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarEntidadAsync(repuesto, ct);
    }

    public Task<int> ActualizarAsync(RepuestoEntity repuesto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(repuesto);
        Validacion.Id(repuesto.IdRepuesto, "IdRepuesto");
        return GuardarEntidadAsync(repuesto, ct);
    }

    public async Task<int> GuardarEntidadAsync(RepuestoEntity repuesto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(repuesto);
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        int id = await _datos.GuardarAsync(actor, repuesto.CrearSolicitud(), ct);
        repuesto.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<RepuestoEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return _datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<int> ConsumirAsync(ConsumoRepuestoSolicitud s, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdOrden = Validacion.Id(s.IdOrden, "IdOrden"), IdRepuesto = Validacion.Id(s.IdRepuesto, "IdRepuesto"), Cantidad = Validacion.Id(s.Cantidad, "Cantidad"), Precio = Validacion.Dinero(s.Precio, "Precio") };
        return _datos.ConsumirAsync(actor, validada, ct);
    }
    public Task CorregirConsumoAsync(CorreccionConsumoRepuestoSolicitud s, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdDetalleRepuesto = Validacion.Id(s.IdDetalleRepuesto, "IdDetalleRepuesto"), Cantidad = Validacion.Id(s.Cantidad, "Cantidad"), Precio = Validacion.Dinero(s.Precio, "Precio") };
        return _datos.CorregirConsumoAsync(actor, validada, ct);
    }
    public Task RetirarConsumoAsync(int idDetalleRepuesto, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Tecnico);
        return _datos.RetirarConsumoAsync(actor, Validacion.Id(idDetalleRepuesto, "IdDetalleRepuesto"), ct);
    }
    public Task AjustarExistenciasAsync(int idRepuesto, int variacion, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        if (variacion == 0) throw new ValidacionException("La variacion de existencias debe ser distinta de cero.");
        return _datos.AjustarExistenciasAsync(actor, Validacion.Id(idRepuesto, "IdRepuesto"), variacion, ct);
    }

    public Task<PaginacionEntity<RepuestoEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        _datos.ConsultarPaginaAsync(_sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<RepuestoEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdRepuesto"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        _sesion.Exigir(RolUsuario.Administrador);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }
    public async Task<int> ConsumirEntidadAsync(DetalleRepuestoEntity detalle, CancellationToken ct = default)
    {
        _sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(detalle);
        if (detalle.IdDetalleRepuesto != 0) throw new ValidacionException("El consumo ya fue registrado.");
        int id = await ConsumirAsync(detalle.CrearSolicitud(), ct);
        detalle.AsignarId(id);
        return id;
    }
    public Task CorregirConsumoEntidadAsync(DetalleRepuestoEntity detalle, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(detalle);
        return CorregirConsumoAsync(detalle.CrearCorreccion(), ct);
    }


    public async Task<PaginacionEntity<RepuestoEntity>> MostrarAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool? estado = null, bool? conExistencias = null, CancellationToken ct = default)
    {
        ConsultaListado.Validar(pagina, tamPagina, buscar);
        if (estado != false && !conExistencias.HasValue)
            return await ConsultarPaginaAsync(pagina, tamPagina, buscar, estado == true, ct);
        var registros = (await ConsultarAsync(soloActivos: false, ct: ct))
            .Where(x => !estado.HasValue || x.Estado == estado.Value)
            .Where(x => !conExistencias.HasValue || (x.Stock > 0) == conExistencias.Value);
        return ConsultaListado.Paginar(registros, pagina, tamPagina, buscar, x => [x.Nombre, x.Marca, x.NumeroParte, x.Descripcion]);
    }
}
