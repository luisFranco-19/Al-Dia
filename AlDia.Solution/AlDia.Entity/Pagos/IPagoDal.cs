namespace AlDia.Entity.Pagos;
public interface IPagoDal
{
    Task<int> RegistrarAsync(int idActor, PagoSolicitud s, CancellationToken ct = default);
    Task<int> CorregirAsync(int idActor, CorreccionPagoSolicitud s, CancellationToken ct = default);
    Task AnularAsync(int idActor, int idPago, string motivo, CancellationToken ct = default);
}
