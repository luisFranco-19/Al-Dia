using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Reparaciones;
namespace AlDia.DAL.Reparaciones;
public sealed class ServicioDal(EjecutorSql sql) : IServicioDal
{
    public Task<int> GuardarAsync(int idActor, ServicioSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarServicio", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdServicio", s.IdServicio), ParametrosSql.Texto("@Nombre", s.Nombre, 100), ParametrosSql.Texto("@Descripcion", s.Descripcion, 300), ParametrosSql.Dinero("@PrecioBase", s.PrecioBase), ParametrosSql.Bit("@Estado", s.Estado)], "@IdServicio", idActor, ct);
    public Task<IReadOnlyList<ServicioEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarServicios", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdServicio", id), ParametrosSql.Bit("@SoloActivos", soloActivos)],
            Mapear, idActor, ct);

    public Task<int> RegistrarEnOrdenAsync(int idActor, ServicioOrdenSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RegistrarServicio", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", s.IdOrden), ParametrosSql.Entero("@IdServicio", s.IdServicio), ParametrosSql.Entero("@Cantidad", s.Cantidad), ParametrosSql.Dinero("@Precio", s.Precio), ParametrosSql.Texto("@Observaciones", s.Observaciones, 500), ParametrosSql.Salida("@IdDetalleServicio")], "@IdDetalleServicio", idActor, ct);
    public Task CorregirEnOrdenAsync(int idActor, CorreccionServicioOrdenSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CorregirServicio", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDetalleServicio", s.IdDetalleServicio), ParametrosSql.Entero("@Cantidad", s.Cantidad), ParametrosSql.Dinero("@Precio", s.Precio), ParametrosSql.Texto("@Observaciones", s.Observaciones, 500)], null, idActor, ct);
    public Task RetirarDeOrdenAsync(int idActor, int idDetalleServicio, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RetirarServicio", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDetalleServicio", idDetalleServicio)], null, idActor, ct);

    private static ServicioEntity Mapear(SqlDataReader r) => new(
        r.Entero("IdServicio"), r.Texto("Nombre"), r.TextoOpcional("Descripcion"), r.Dinero("PrecioBase"),
        r.Bit("Estado"));
    public Task<PaginacionEntity<ServicioEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default) =>
        sql.ConsultarPaginaAsync("dbo.usp_ConsultarServicios", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Bit("@SoloActivos", soloActivos)], Mapear, idActor, pagina, tamPagina, buscar, ct);
}
