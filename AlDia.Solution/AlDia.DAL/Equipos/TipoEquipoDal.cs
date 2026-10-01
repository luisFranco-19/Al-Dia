using AlDia.DAL.Common;
using AlDia.Entity.Equipos;
namespace AlDia.DAL.Equipos;
public sealed class TipoEquipoDal(EjecutorSql sql) : ITipoEquipoDal
{
    public Task<int> GuardarAsync(int idActor, TipoEquipoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarTipoEquipo", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdTipoEquipo", s.IdTipoEquipo), ParametrosSql.Texto("@Nombre", s.Nombre, 50), ParametrosSql.Texto("@Descripcion", s.Descripcion, 200), ParametrosSql.Bit("@Estado", s.Estado)], "@IdTipoEquipo", idActor, ct);
    public Task<IReadOnlyList<TipoEquipoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarTiposEquipo", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdTipoEquipo", id), ParametrosSql.Bit("@SoloActivos", soloActivos)],
            reader => new TipoEquipoEntity(reader.GetInt32(reader.GetOrdinal("IdTipoEquipo")), reader.GetString(reader.GetOrdinal("Nombre")), reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")), reader.GetBoolean(reader.GetOrdinal("Estado"))), idActor, ct);
}
