using AlDia.DAL.Common;
using AlDia.Entity.Equipos;
namespace AlDia.DAL.Equipos;
public sealed class EquipoDal(EjecutorSql sql) : IEquipoDal
{
    public Task<int> GuardarAsync(int idActor, EquipoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarEquipo", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdEquipo", s.IdEquipo), ParametrosSql.Entero("@IdCliente", s.IdCliente), ParametrosSql.Entero("@IdTipoEquipo", s.IdTipoEquipo), ParametrosSql.Texto("@Marca", s.Marca, 50), ParametrosSql.Texto("@Modelo", s.Modelo, 100), ParametrosSql.Texto("@NumeroSerie", s.NumeroSerie, 100), ParametrosSql.Texto("@Color", s.Color, 50), ParametrosSql.Texto("@Observaciones", s.Observaciones, 500), ParametrosSql.Bit("@Estado", s.Estado)], "@IdEquipo", idActor, ct);
    public Task<IReadOnlyList<EquipoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarEquipos", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdEquipo", id), ParametrosSql.Bit("@SoloActivos", soloActivos)],
            reader => new EquipoEntity(reader.GetInt32(reader.GetOrdinal("IdEquipo")), reader.GetInt32(reader.GetOrdinal("IdCliente")), reader.GetInt32(reader.GetOrdinal("IdTipoEquipo")), reader.GetString(reader.GetOrdinal("Marca")), reader.IsDBNull(reader.GetOrdinal("Modelo")) ? null : reader.GetString(reader.GetOrdinal("Modelo")), reader.IsDBNull(reader.GetOrdinal("NumeroSerie")) ? null : reader.GetString(reader.GetOrdinal("NumeroSerie")), reader.IsDBNull(reader.GetOrdinal("Color")) ? null : reader.GetString(reader.GetOrdinal("Color")), reader.IsDBNull(reader.GetOrdinal("Observaciones")) ? null : reader.GetString(reader.GetOrdinal("Observaciones")), reader.GetBoolean(reader.GetOrdinal("Estado")), reader.GetDateTime(reader.GetOrdinal("FechaRegistro"))), idActor, ct);
}
