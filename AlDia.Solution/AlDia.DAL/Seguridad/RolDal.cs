using AlDia.DAL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using Microsoft.Data.SqlClient;

namespace AlDia.DAL.Seguridad;

public sealed class RolDal(EjecutorSql sql) : IRolDal
{
    private static RolEntity Mapear(SqlDataReader r) => new(r.Entero("IdRol"), r.Texto("Nombre"), r.TextoOpcional("Descripcion"), r.Bit("Estado"));
    public Task<int> GuardarAsync(int idActor, RolSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarRol", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.EntradaSalida("@IdRol", s.IdRol), ParametrosSql.Texto("@Nombre", s.Nombre, 50),
            ParametrosSql.Texto("@Descripcion", s.Descripcion, 200), ParametrosSql.Bit("@Estado", s.Estado)], "@IdRol", idActor, ct);
    public Task<IReadOnlyList<RolEntity>> ConsultarAsync(int idActor, int? idRol = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarRoles", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Entero("@IdRol", idRol), ParametrosSql.Bit("@SoloActivos", soloActivos)], Mapear, idActor, ct);
    public Task<PaginacionEntity<RolEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default) =>
        sql.ConsultarPaginaAsync("dbo.usp_ConsultarRoles", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Bit("@SoloActivos", soloActivos)], Mapear, idActor, pagina, tamPagina, buscar, ct);
}
