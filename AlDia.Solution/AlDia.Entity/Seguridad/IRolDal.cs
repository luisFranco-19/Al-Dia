using AlDia.Entity.Common;
namespace AlDia.Entity.Seguridad;

// Contrato del modulo de roles; su implementacion y los procedimientos se trabajan en DAL.
public interface IRolDal : IConsultaPaginadaDal<RolEntity>
{
    Task<int> GuardarAsync(int idActor, RolSolicitud solicitud, CancellationToken ct = default);
    Task<IReadOnlyList<RolEntity>> ConsultarAsync(int idActor, int? idRol = null, bool soloActivos = true, CancellationToken ct = default);
}
