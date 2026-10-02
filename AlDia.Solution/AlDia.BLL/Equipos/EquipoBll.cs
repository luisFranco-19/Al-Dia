using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Equipos;
namespace AlDia.BLL.Equipos;
public sealed class EquipoBll(IEquipoDal datos, SesionUsuario sesion)
{
    public Task<int> GuardarAsync(EquipoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        int actor = sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        var validada = new EquipoEntity(s.IdEquipo, s.IdCliente, s.IdTipoEquipo, s.Marca, s.Modelo,
            s.NumeroSerie, s.Color, s.Observaciones, s.Estado, DateTime.Now).CrearSolicitud();
        return datos.GuardarAsync(actor, validada, ct);
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
        int actor = sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<PaginacionEntity<EquipoEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        datos.ConsultarPaginaAsync(sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<EquipoEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdEquipo"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }
}
