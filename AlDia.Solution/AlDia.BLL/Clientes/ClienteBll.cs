using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Clientes;
namespace AlDia.BLL.Clientes;
public sealed class ClienteBll(IClienteDal datos, SesionUsuario sesion)
{
    public Task<int> GuardarAsync(ClienteSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        return GuardarEntidadAsync(new ClienteEntity(s.IdCliente, s.Nombre, s.Apellido,
            s.Telefono, s.Cedula, s.Estado), ct);
    }
    public async Task<int> GuardarEntidadAsync(ClienteEntity cliente, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        int actor = sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        int id = await datos.GuardarAsync(actor, cliente.CrearSolicitud(), ct);
        cliente.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<ClienteEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return datos.ConsultarAsync(actor, id, soloActivos, ct);
    }
}
