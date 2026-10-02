using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Equipos;
namespace AlDia.DAL.Equipos;
public sealed class EquipoDal(EjecutorSql sql) : IEquipoDal
{
    public Task<int> GuardarAsync(int idActor, EquipoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarEquipo", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdEquipo", s.IdEquipo), ParametrosSql.Entero("@IdCliente", s.IdCliente), ParametrosSql.Entero("@IdTipoEquipo", s.IdTipoEquipo), ParametrosSql.Texto("@Marca", s.Marca, 50), ParametrosSql.Texto("@Modelo", s.Modelo, 100), ParametrosSql.Texto("@NumeroSerie", s.NumeroSerie, 100), ParametrosSql.Texto("@Color", s.Color, 50), ParametrosSql.Texto("@Observaciones", s.Observaciones, 500), ParametrosSql.Bit("@Estado", s.Estado)], "@IdEquipo", idActor, ct);
    public Task<IReadOnlyList<EquipoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarEquipos", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdEquipo", id), ParametrosSql.Bit("@SoloActivos", soloActivos)],
            Mapear, idActor, ct);

    private static EquipoEntity Mapear(SqlDataReader r) => new(
        r.Entero("IdEquipo"), r.Entero("IdCliente"), r.Entero("IdTipoEquipo"), r.Texto("Marca"),
        r.TextoOpcional("Modelo"), r.TextoOpcional("NumeroSerie"), r.TextoOpcional("Color"), r.TextoOpcional("Observaciones"),
        r.Bit("Estado"), r.Fecha("FechaRegistro"));
    public Task<PaginacionEntity<EquipoEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default) =>
        sql.ConsultarPaginaAsync("dbo.usp_ConsultarEquipos", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Bit("@SoloActivos", soloActivos)], Mapear, idActor, pagina, tamPagina, buscar, ct);
}
