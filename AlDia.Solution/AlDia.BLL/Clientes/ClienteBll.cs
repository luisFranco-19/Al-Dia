using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Clientes;
namespace AlDia.BLL.Clientes;
public sealed class ClienteBll
{
    private readonly IClienteDal _datos;
    private readonly SesionUsuario _sesion;

    public ClienteBll(IClienteDal datos, SesionUsuario sesion)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<int> GuardarAsync(ClienteSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        _sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        return GuardarEntidadAsync(new ClienteEntity(s.IdCliente, s.Nombre, s.Apellido,
            s.Telefono, s.Cedula, s.Estado), ct);
    }
    public Task<int> AgregarAsync(ClienteEntity cliente, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        if (cliente.IdCliente != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarEntidadAsync(cliente, ct);
    }

    public Task<int> ActualizarAsync(ClienteEntity cliente, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        Validacion.Id(cliente.IdCliente, "IdCliente");
        return GuardarEntidadAsync(cliente, ct);
    }

    public async Task<int> GuardarEntidadAsync(ClienteEntity cliente, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(cliente);
        int actor = _sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        int id = await _datos.GuardarAsync(actor, cliente.CrearSolicitud(), ct);
        cliente.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<ClienteEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, 
        CancellationToken ct = default)
    {
        int actor = _sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return _datos.ConsultarAsync(actor, id, soloActivos, ct);
    }

    public Task<PaginacionEntity<ClienteEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        _datos.ConsultarPaginaAsync(_sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct);
    public async Task<ClienteEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) =>
        (await ConsultarAsync(Validacion.Id(id, "IdCliente"), false, ct)).SingleOrDefault();
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default)
    {
        _sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente.");
        entidad.CambiarEstado(estado);
        await GuardarEntidadAsync(entidad, ct);
    }

    public async Task<PaginacionEntity<ClienteEntity>> MostrarAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, bool? estado = null, CancellationToken ct = default)
    {
        ConsultaListado.Validar(pagina, tamPagina, buscar);
        if (estado != false)
            return await ConsultarPaginaAsync(pagina, tamPagina, buscar, estado == true, ct);
        var registros = (await ConsultarAsync(soloActivos: false, ct: ct))
            .Where(x => !estado.HasValue || x.Estado == estado.Value);
        return ConsultaListado.Paginar(registros, pagina, tamPagina, buscar, x => [x.Nombre, x.Apellido, x.Cedula, x.Telefono]);
    }
}
