using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Equipos;
namespace AlDia.BLL.Equipos;
public sealed class TipoEquipoBll
{
    private readonly ITipoEquipoDal _datos;
    private readonly SesionUsuario _sesion;

    public TipoEquipoBll(ITipoEquipoDal datos, SesionUsuario sesion)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<int> GuardarAsync(TipoEquipoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        _sesion.Exigir(RolUsuario.Administrador);
        return GuardarEntidadAsync(new TipoEquipoEntity(s.IdTipoEquipo, s.Nombre, s.Descripcion, s.Estado), ct);
    }
    public Task<int> AgregarAsync(TipoEquipoEntity tipo, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(tipo);
        if (tipo.IdTipoEquipo != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarEntidadAsync(tipo, ct);
    }

    public Task<int> ActualizarAsync(TipoEquipoEntity tipo, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(tipo);
        Validacion.Id(tipo.IdTipoEquipo, "IdTipoEquipo");
        return GuardarEntidadAsync(tipo, ct);
    }

    public async Task<int> GuardarEntidadAsync(TipoEquipoEntity tipo, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(tipo);
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        int id = await _datos.GuardarAsync(actor, tipo.CrearSolicitud(), ct);
        tipo.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<TipoEquipoEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return _datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<PaginacionEntity<TipoEquipoEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        _datos.ConsultarPaginaAsync(_sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<TipoEquipoEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdTipoEquipo"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        _sesion.Exigir(RolUsuario.Administrador);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }

    public async Task<PaginacionEntity<TipoEquipoEntity>> MostrarAsync(int pagina = 1, int tamPagina = 10,
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
