using AlDia.DAL.Common;
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
}
