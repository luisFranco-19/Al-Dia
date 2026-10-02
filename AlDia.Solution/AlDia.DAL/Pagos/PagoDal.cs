using AlDia.DAL.Common;
using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.Entity.Pagos;
namespace AlDia.DAL.Pagos;
public sealed class PagoDal(EjecutorSql sql) : IPagoDal
{
    public Task<int> RegistrarAsync(int idActor, PagoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RegistrarPago", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", s.IdOrden), ParametrosSql.Dinero("@Monto", s.Monto), ParametrosSql.Texto("@MetodoPago", s.MetodoPago.ToString(), 30), ParametrosSql.Texto("@Observaciones", s.Observaciones, 300), ParametrosSql.Salida("@IdPago")], "@IdPago", idActor, ct);
    public Task<int> CorregirAsync(int idActor, CorreccionPagoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CorregirPago", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdPago", s.IdPago), ParametrosSql.Dinero("@Monto", s.Monto), ParametrosSql.Texto("@MetodoPago", s.MetodoPago.ToString(), 30), ParametrosSql.Texto("@Observaciones", s.Observaciones, 300), ParametrosSql.Texto("@Motivo", s.Motivo, 300), ParametrosSql.Salida("@IdPagoNuevo")], "@IdPagoNuevo", idActor, ct);
    public Task AnularAsync(int idActor, int idPago, string motivo, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_AnularPago", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdPago", idPago), ParametrosSql.Texto("@Motivo", motivo, 300)], null, idActor, ct);
    internal static PagoEntity Mapear(SqlDataReader r) => new(r.Entero("IdPago"), r.Entero("IdOrden"),
        r.Entero("IdUsuario"), r.Dinero("Monto"), Enum.Parse<MetodoPago>(r.Texto("MetodoPago")),
        r.Fecha("FechaPago"), r.TextoOpcional("Observaciones"), r.Bit("Anulado"), r.FechaOpcional("FechaAnulacion"),
        r.EnteroOpcional("IdUsuarioAnulacion"), r.TextoOpcional("MotivoAnulacion"));
    public Task<IReadOnlyList<PagoEntity>> ConsultarAsync(int idActor, int? idOrden, bool incluirAnulados, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarPagos", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Entero("@IdOrden", idOrden), ParametrosSql.Bit("@IncluirAnulados", incluirAnulados)], Mapear, idActor, ct);
    public Task<PaginacionEntity<PagoEntity>> ConsultarPaginaAsync(int idActor, int? idOrden, bool incluirAnulados,
        int pagina, int tamPagina, string? buscar, CancellationToken ct = default) =>
        sql.ConsultarPaginaAsync("dbo.usp_ConsultarPagos", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Entero("@IdOrden", idOrden), ParametrosSql.Bit("@IncluirAnulados", incluirAnulados)],
            Mapear, idActor, pagina, tamPagina, buscar, ct);

    public async Task<PagoEntity?> ObtenerPorIdAsync(int idActor, int idPago, CancellationToken ct = default) =>
        (await sql.ConsultarAsync("dbo.usp_ConsultarPagos", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Entero("@IdPago", idPago), ParametrosSql.Bit("@IncluirAnulados", true)], Mapear, idActor, ct)).SingleOrDefault();
}
