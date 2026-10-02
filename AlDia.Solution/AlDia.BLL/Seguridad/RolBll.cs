using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;

namespace AlDia.BLL.Seguridad;

public sealed class RolBll(IRolDal datos, SesionUsuario sesion)
{
    public Task<int> GuardarAsync(RolSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        sesion.Exigir(RolUsuario.Administrador);
        return GuardarEntidadAsync(new RolEntity(s.IdRol, s.Nombre, s.Descripcion, s.Estado), ct);
    }
    public async Task<int> GuardarEntidadAsync(RolEntity rol, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(rol);
        int actor = sesion.Exigir(RolUsuario.Administrador);
        int id = await datos.GuardarAsync(actor, rol.CrearSolicitud(), ct);
        rol.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<RolEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Administrador);
        if (id.HasValue) Validacion.Id(id.Value, "IdRol");
        return datos.ConsultarAsync(actor, id, soloActivos, ct);
    }
    public Task<PaginacionEntity<RolEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        datos.ConsultarPaginaAsync(sesion.Exigir(RolUsuario.Administrador), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<RolEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdRol"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        var rol = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Rol inexistente.");
        rol.CambiarEstado(estado);
        await GuardarEntidadAsync(rol, ct);
    }
}
