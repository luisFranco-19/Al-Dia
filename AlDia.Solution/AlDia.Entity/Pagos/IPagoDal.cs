using AlDia.Entity.Common;
namespace AlDia.Entity.Pagos;
public interface IPagoDal
{
    Task<int> RegistrarAsync(int idActor, PagoSolicitud s, CancellationToken ct = default);
    Task<int> CorregirAsync(int idActor, CorreccionPagoSolicitud s, CancellationToken ct = default);
    Task AnularAsync(int idActor, int idPago, string motivo, CancellationToken ct = default);
    Task<IReadOnlyList<PagoEntity>> ConsultarAsync(int idActor, int? idOrden, bool incluirAnulados, CancellationToken ct = default);
    Task<PaginacionEntity<PagoEntity>> ConsultarPaginaAsync(int idActor, int? idOrden, bool incluirAnulados,
        int pagina, int tamPagina, string? buscar, CancellationToken ct = default);

    Task<PagoEntity?> ObtenerPorIdAsync(int idActor, int idPago, CancellationToken ct = default);
}
