using AlDia.Entity.Common;
namespace AlDia.Entity.Reparaciones;
public interface IServicioDal : IConsultaPaginadaDal<ServicioEntity>
{
    Task<int> GuardarAsync(int idActor, ServicioSolicitud solicitud, CancellationToken ct = default);
    Task<IReadOnlyList<ServicioEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default);

    Task<int> RegistrarEnOrdenAsync(int idActor, ServicioOrdenSolicitud s, CancellationToken ct = default);
    Task CorregirEnOrdenAsync(int idActor, CorreccionServicioOrdenSolicitud s, CancellationToken ct = default);
    Task RetirarDeOrdenAsync(int idActor, int idDetalleServicio, CancellationToken ct = default);
}
