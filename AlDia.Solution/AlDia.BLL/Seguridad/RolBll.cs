using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;

namespace AlDia.BLL.Seguridad;

public sealed class RolBll
{
    private readonly IRolDal _datos;
    private readonly SesionUsuario _sesion;

    public RolBll(IRolDal datos, SesionUsuario sesion)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<int> GuardarAsync(RolSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        _sesion.Exigir(RolUsuario.Administrador);
        return GuardarEntidadAsync(new RolEntity(s.IdRol, s.Nombre, s.Descripcion, s.Estado), ct);
    }
    public Task<int> AgregarAsync(RolEntity rol, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(rol);
        if (rol.IdRol != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarEntidadAsync(rol, ct);
    }

    public Task<int> ActualizarAsync(RolEntity rol, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(rol);
        Validacion.Id(rol.IdRol, "IdRol");
        return GuardarEntidadAsync(rol, ct);
    }

    public async Task<int> GuardarEntidadAsync(RolEntity rol, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(rol);
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        int id = await _datos.GuardarAsync(actor, rol.CrearSolicitud(), ct);
        rol.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<RolEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        if (id.HasValue) Validacion.Id(id.Value, "IdRol");
        return _datos.ConsultarAsync(actor, id, soloActivos, ct);
    }
    public Task<IReadOnlyList<RolEntity>> MostrarRolesActivosAsync(CancellationToken ct = default) =>
        ConsultarAsync(soloActivos: true, ct: ct);

    public Task<PaginacionEntity<RolEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        _datos.ConsultarPaginaAsync(_sesion.Exigir(RolUsuario.Administrador), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<RolEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdRol"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        var rol = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Rol inexistente.");
        rol.CambiarEstado(estado);
        await GuardarEntidadAsync(rol, ct);
    }

    public async Task<PaginacionEntity<RolEntity>> MostrarAsync(int pagina = 1, int tamPagina = 10,
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
