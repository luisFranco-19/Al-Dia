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

    public Task<PaginacionEntity<ClienteEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        datos.ConsultarPaginaAsync(sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<ClienteEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdCliente"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }
}
