using AlDia.Entity.Common;
namespace AlDia.Entity.Reparaciones;
public interface IOrdenReparacionDal
{
    Task<int> RegistrarRecepcionAsync(int idActor, RecepcionSolicitud s, CancellationToken ct = default);
    Task CorregirRecepcionAsync(int idActor, CorreccionRecepcionSolicitud s, CancellationToken ct = default);
    Task AnularAsync(int idActor, int idOrden, string motivo, CancellationToken ct = default);
    Task TomarAsync(int idActor, int idOrden, CancellationToken ct = default);
    Task LiberarAsync(int idActor, int idOrden, CancellationToken ct = default);
    Task IniciarReparacionAsync(int idActor, int idOrden, CancellationToken ct = default);
    Task FinalizarReparacionAsync(int idActor, int idOrden, bool reparada, string observaciones, CancellationToken ct = default);
    Task EntregarAsync(int idActor, int idOrden, string? observacion, CancellationToken ct = default);

    Task<PaginacionEntity<OrdenReparacionEntity>> ConsultarPaginaAsync(int idActor, FiltroOrdenes filtro,
        int pagina, int tamPagina, string? buscar, CancellationToken ct = default);
    Task<IReadOnlyList<OrdenReparacionEntity>> ConsultarAsync(int idActor, FiltroOrdenes filtro, CancellationToken ct = default);
    Task<IReadOnlyList<EstadoReparacionEntity>> ConsultarEstadosAsync(int idActor, CancellationToken ct = default);
    Task<ExpedienteOrdenEntity?> ConsultarExpedienteAsync(int idActor, int idOrden, CancellationToken ct = default);

}
