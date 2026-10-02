using AlDia.Entity.Common;
namespace AlDia.Entity.Inventario;
public interface IRepuestoDal : IConsultaPaginadaDal<RepuestoEntity>
{
    Task<int> GuardarAsync(int idActor, RepuestoSolicitud solicitud, CancellationToken ct = default);
    Task<IReadOnlyList<RepuestoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default);

    Task<int> ConsumirAsync(int idActor, ConsumoRepuestoSolicitud s, CancellationToken ct = default);
    Task CorregirConsumoAsync(int idActor, CorreccionConsumoRepuestoSolicitud s, CancellationToken ct = default);
    Task RetirarConsumoAsync(int idActor, int idDetalleRepuesto, CancellationToken ct = default);
    Task AjustarExistenciasAsync(int idActor, int idRepuesto, int variacion, CancellationToken ct = default);
}
