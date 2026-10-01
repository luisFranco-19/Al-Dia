using AlDia.DAL.Common;
using AlDia.Entity.Reparaciones;
namespace AlDia.DAL.Reparaciones;
public sealed class DiagnosticoDal(EjecutorSql sql) : IDiagnosticoDal
{
    public Task<int> RegistrarAsync(int idActor, DiagnosticoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RegistrarDiagnostico", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", s.IdOrden), ParametrosSql.Texto("@ProblemaEncontrado", s.ProblemaEncontrado, 1000), ParametrosSql.Texto("@ReparacionPropuesta", s.ReparacionPropuesta, 1000), ParametrosSql.Dinero("@CostoEstimado", s.CostoEstimado), ParametrosSql.Texto("@Observaciones", s.Observaciones, 1000), ParametrosSql.Salida("@IdDiagnostico")], "@IdDiagnostico", idActor, ct);
    public Task CorregirAsync(int idActor, CorreccionDiagnosticoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CorregirDiagnostico", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDiagnostico", s.IdDiagnostico), ParametrosSql.Texto("@ProblemaEncontrado", s.ProblemaEncontrado, 1000), ParametrosSql.Texto("@ReparacionPropuesta", s.ReparacionPropuesta, 1000), ParametrosSql.Dinero("@CostoEstimado", s.CostoEstimado), ParametrosSql.Texto("@Observaciones", s.Observaciones, 1000)], null, idActor, ct);
    public Task RetirarAsync(int idActor, int idDiagnostico, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RetirarDiagnostico", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDiagnostico", idDiagnostico)], null, idActor, ct);
    public Task<int> RegistrarDecisionAsync(int idActor, DecisionClienteSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RegistrarDecisionCliente", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDiagnostico", s.IdDiagnostico), ParametrosSql.Texto("@Decision", s.Decision.ToString().ToUpperInvariant(), 20), ParametrosSql.Dinero("@CostoAprobado", s.CostoAprobado), ParametrosSql.Texto("@Observaciones", s.Observaciones, 500), ParametrosSql.Salida("@IdConfirmacion")], "@IdConfirmacion", idActor, ct);
    public Task CorregirDecisionAsync(int idActor, CorreccionDecisionClienteSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CorregirDecisionCliente", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdConfirmacion", s.IdConfirmacion), ParametrosSql.Texto("@Decision", s.Decision.ToString().ToUpperInvariant(), 20), ParametrosSql.Dinero("@CostoAprobado", s.CostoAprobado), ParametrosSql.Texto("@Observaciones", s.Observaciones, 500)], null, idActor, ct);
    public Task RetirarDecisionAsync(int idActor, int idConfirmacion, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RetirarDecisionCliente", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdConfirmacion", idConfirmacion)], null, idActor, ct);
}
