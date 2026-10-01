namespace AlDia.Entity.Equipos;
public interface ITipoEquipoDal
{
    Task<int> GuardarAsync(int idActor, TipoEquipoSolicitud solicitud, CancellationToken ct = default);
    Task<IReadOnlyList<TipoEquipoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default);
}
