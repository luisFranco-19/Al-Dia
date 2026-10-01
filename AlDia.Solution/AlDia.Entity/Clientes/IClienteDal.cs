namespace AlDia.Entity.Clientes;
public interface IClienteDal
{
    Task<int> GuardarAsync(int idActor, ClienteSolicitud solicitud, CancellationToken ct = default);
    Task<IReadOnlyList<ClienteEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default);
}
