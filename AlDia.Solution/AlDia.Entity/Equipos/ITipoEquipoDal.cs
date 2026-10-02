using AlDia.Entity.Common;
namespace AlDia.Entity.Equipos;
public interface ITipoEquipoDal : IConsultaPaginadaDal<TipoEquipoEntity>
{
    Task<int> GuardarAsync(int idActor, TipoEquipoSolicitud solicitud, CancellationToken ct = default);
    Task<IReadOnlyList<TipoEquipoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default);
}
