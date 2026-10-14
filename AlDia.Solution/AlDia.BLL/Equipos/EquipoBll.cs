using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Equipos;
namespace AlDia.BLL.Equipos;
public sealed class EquipoBll
{
    private readonly IEquipoDal _datos;
    private readonly SesionUsuario _sesion;

    public EquipoBll(IEquipoDal datos, SesionUsuario sesion)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<int> GuardarAsync(EquipoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        int actor = _sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        var validada = new EquipoEntity(s.IdEquipo, s.IdCliente, s.IdTipoEquipo, s.Marca, s.Modelo,
            s.NumeroSerie, s.Color, s.Observaciones, s.Estado, DateTime.Now).CrearSolicitud();
        return _datos.GuardarAsync(actor, validada, ct);
    }
    public Task<int> AgregarAsync(EquipoEntity equipo, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(equipo);
        if (equipo.IdEquipo != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarEntidadAsync(equipo, ct);
    }

    public Task<int> ActualizarAsync(EquipoEntity equipo, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(equipo);
        Validacion.Id(equipo.IdEquipo, "IdEquipo");
        return GuardarEntidadAsync(equipo, ct);
    }

    public async Task<int> GuardarEntidadAsync(EquipoEntity equipo, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(equipo);
        int id = await GuardarAsync(equipo.CrearSolicitud(), ct);
        equipo.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<EquipoEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return _datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<PaginacionEntity<EquipoEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        _datos.ConsultarPaginaAsync(_sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<EquipoEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdEquipo"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        _sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }

    public async Task<PaginacionEntity<EquipoEntity>> MostrarAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool? estado = null, int? idTipoEquipo = null, CancellationToken ct = default)
    {
        ConsultaListado.Validar(pagina, tamPagina, buscar);
        if (estado != false && !idTipoEquipo.HasValue)
            return await ConsultarPaginaAsync(pagina, tamPagina, buscar, estado == true, ct);
        var registros = (await ConsultarAsync(soloActivos: false, ct: ct))
            .Where(x => !estado.HasValue || x.Estado == estado.Value)
            .Where(x => !idTipoEquipo.HasValue || x.IdTipoEquipo == idTipoEquipo.Value);
        return ConsultaListado.Paginar(registros, pagina, tamPagina, buscar, x => [x.Marca, x.Modelo, x.NumeroSerie, x.Color]);
    }
}
