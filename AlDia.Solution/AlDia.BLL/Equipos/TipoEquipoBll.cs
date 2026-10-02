using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Equipos;
namespace AlDia.BLL.Equipos;
public sealed class TipoEquipoBll(ITipoEquipoDal datos, SesionUsuario sesion)
{
    public Task<int> GuardarAsync(TipoEquipoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        sesion.Exigir(RolUsuario.Administrador);
        return GuardarEntidadAsync(new TipoEquipoEntity(s.IdTipoEquipo, s.Nombre, s.Descripcion, s.Estado), ct);
    }
    public async Task<int> GuardarEntidadAsync(TipoEquipoEntity tipo, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(tipo);
        int actor = sesion.Exigir(RolUsuario.Administrador);
        int id = await datos.GuardarAsync(actor, tipo.CrearSolicitud(), ct);
        tipo.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<TipoEquipoEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<PaginacionEntity<TipoEquipoEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        datos.ConsultarPaginaAsync(sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<TipoEquipoEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdTipoEquipo"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        sesion.Exigir(RolUsuario.Administrador);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }
}
