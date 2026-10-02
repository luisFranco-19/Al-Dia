using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Equipos;
namespace AlDia.DAL.Equipos;
public sealed class TipoEquipoDal(EjecutorSql sql) : ITipoEquipoDal
{
    public Task<int> GuardarAsync(int idActor, TipoEquipoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarTipoEquipo", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdTipoEquipo", s.IdTipoEquipo), ParametrosSql.Texto("@Nombre", s.Nombre, 50), ParametrosSql.Texto("@Descripcion", s.Descripcion, 200), ParametrosSql.Bit("@Estado", s.Estado)], "@IdTipoEquipo", idActor, ct);
    public Task<IReadOnlyList<TipoEquipoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarTiposEquipo", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdTipoEquipo", id), ParametrosSql.Bit("@SoloActivos", soloActivos)],
            Mapear, idActor, ct);

    private static TipoEquipoEntity Mapear(SqlDataReader r) => new(
        r.Entero("IdTipoEquipo"), r.Texto("Nombre"), r.TextoOpcional("Descripcion"), r.Bit("Estado"));
    public Task<PaginacionEntity<TipoEquipoEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default) =>
        sql.ConsultarPaginaAsync("dbo.usp_ConsultarTiposEquipo", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Bit("@SoloActivos", soloActivos)], Mapear, idActor, pagina, tamPagina, buscar, ct);
}
