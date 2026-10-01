namespace AlDia.Entity.Equipos;
public interface IEquipoDal
{
    Task<int> GuardarAsync(int idActor, EquipoSolicitud solicitud, CancellationToken ct = default);
    Task<IReadOnlyList<EquipoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default);
}
