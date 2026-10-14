using AlDia.BLL.Equipos;
using AlDia.BLL.Reparaciones;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;

namespace AlDia.BLL.Common;

public enum TipoCatalogo { Rol = 1, TipoEquipo = 2, Servicio = 3 }

public sealed record CatalogoListado(int Id, TipoCatalogo Tipo, string Nombre,
    string? Descripcion, decimal? Precio, bool Estado)
{
    public string Catalogo => Tipo switch
    {
        TipoCatalogo.Rol => "Roles", TipoCatalogo.TipoEquipo => "Tipos de equipo", _ => "Servicios"
    };
    public string EstadoNombre => Estado ? "Activo" : "Inactivo";
}

// Composición: reúne los tres catálogos sin cambiar las entidades ni sus procedimientos.
public sealed class CatalogosBll
{
    private readonly RolBll _roles;
    private readonly TipoEquipoBll _tipos;
    private readonly ServicioBll _servicios;
    public CatalogosBll(RolBll roles, TipoEquipoBll tipos, ServicioBll servicios)
    {
        _roles = roles ?? throw new ArgumentNullException(nameof(roles));
        _tipos = tipos ?? throw new ArgumentNullException(nameof(tipos));
        _servicios = servicios ?? throw new ArgumentNullException(nameof(servicios));
    }

    public async Task<PaginacionEntity<CatalogoListado>> MostrarAsync(int pagina, int tamPagina,
        string? buscar = null, TipoCatalogo? tipo = null, bool? estado = null, CancellationToken ct = default)
    {
        ConsultaListado.Validar(pagina, tamPagina, buscar);
        if (tipo.HasValue) Validacion.Enumeracion(tipo.Value, "Catálogo");
        List<CatalogoListado> registros = [];
        if (tipo is null or TipoCatalogo.Rol)
            registros.AddRange((await _roles.ConsultarAsync(soloActivos: false, ct: ct))
                .Select(x => new CatalogoListado(x.IdRol, TipoCatalogo.Rol, x.Nombre, x.Descripcion, null, x.Estado)));
        if (tipo is null or TipoCatalogo.TipoEquipo)
            registros.AddRange((await _tipos.ConsultarAsync(soloActivos: false, ct: ct))
                .Select(x => new CatalogoListado(x.IdTipoEquipo, TipoCatalogo.TipoEquipo, x.Nombre, x.Descripcion, null, x.Estado)));
        if (tipo is null or TipoCatalogo.Servicio)
            registros.AddRange((await _servicios.ConsultarAsync(soloActivos: false, ct: ct))
                .Select(x => new CatalogoListado(x.IdServicio, TipoCatalogo.Servicio, x.Nombre, x.Descripcion, x.PrecioBase, x.Estado)));
        return ConsultaListado.Paginar(registros.Where(x => !estado.HasValue || x.Estado == estado.Value),
            pagina, tamPagina, buscar, x => [x.Nombre, x.Descripcion]);
    }

    public Task CambiarEstadoAsync(CatalogoListado registro, bool estado, CancellationToken ct = default) => registro.Tipo switch
    {
        TipoCatalogo.Rol => _roles.CambiarEstadoAsync(registro.Id, estado, ct),
        TipoCatalogo.TipoEquipo => _tipos.CambiarEstadoAsync(registro.Id, estado, ct),
        TipoCatalogo.Servicio => _servicios.CambiarEstadoAsync(registro.Id, estado, ct),
        _ => throw new ValidacionException("Catálogo desconocido.")
    };
}
