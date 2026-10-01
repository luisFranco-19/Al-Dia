namespace AlDia.Entity.Reparaciones;
public interface IDiagnosticoDal
{
    Task<int> RegistrarAsync(int idActor, DiagnosticoSolicitud s, CancellationToken ct = default);
    Task CorregirAsync(int idActor, CorreccionDiagnosticoSolicitud s, CancellationToken ct = default);
    Task RetirarAsync(int idActor, int idDiagnostico, CancellationToken ct = default);
    Task<int> RegistrarDecisionAsync(int idActor, DecisionClienteSolicitud s, CancellationToken ct = default);
    Task CorregirDecisionAsync(int idActor, CorreccionDecisionClienteSolicitud s, CancellationToken ct = default);
    Task RetirarDecisionAsync(int idActor, int idConfirmacion, CancellationToken ct = default);
}
