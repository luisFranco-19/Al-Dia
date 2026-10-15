using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Clientes;
namespace AlDia.BLL.Clientes;
public sealed class ClienteBll // Aplica las reglas de negocio para clientes.
{
    private readonly IClienteDal _datos; // Guarda el acceso a la base de datos.
    private readonly SesionUsuario _sesion; // Guarda la sesión para comprobar permisos.

    public ClienteBll(IClienteDal datos, SesionUsuario sesion) // Recibe acceso a datos y sesión de usuario.
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<int> GuardarAsync(ClienteSolicitud s, CancellationToken ct = default) // Valida permisos y prepara el guardado.
    {
        ArgumentNullException.ThrowIfNull(s);
        _sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista); // Solo permite guardar a los roles autorizados.
        return GuardarEntidadAsync(new ClienteEntity(s.IdCliente, s.Nombre, s.Apellido, // Convierte la solicitud en una entidad cliente.
            s.Telefono, s.Cedula, s.Estado), ct);
    }
    public Task<int> AgregarAsync(ClienteEntity cliente, CancellationToken ct = default) // Agrega un cliente nuevo.
    {
        ArgumentNullException.ThrowIfNull(cliente);
        if (cliente.IdCliente != 0) // Rechaza agregar un cliente que ya tiene ID.
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarEntidadAsync(cliente, ct);
    }

    public Task<int> ActualizarAsync(ClienteEntity cliente, CancellationToken ct = default) // Actualiza un cliente existente.
    {
        ArgumentNullException.ThrowIfNull(cliente);
        Validacion.Id(cliente.IdCliente, "IdCliente"); // Comprueba que el ID sea válido.
        return GuardarEntidadAsync(cliente, ct);
    }

    public async Task<int> GuardarEntidadAsync(ClienteEntity cliente, CancellationToken ct = default) // Guarda una entidad y devuelve su ID.
    {
        ArgumentNullException.ThrowIfNull(cliente);
        int actor = _sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista); // Solo permite guardar a los roles autorizados.
        int id = await _datos.GuardarAsync(actor, cliente.CrearSolicitud(), ct); // Envía la solicitud a la capa de datos.
        cliente.AsignarId(id); // Guarda en la entidad el ID devuelto por SQL.
        return id;
    }
    public Task<IReadOnlyList<ClienteEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, // Consulta clientes por ID y estado.
        CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(); // Obtiene el ID del usuario autorizado.
        if (id.HasValue) Validacion.Id(id.Value, "Identificador"); // Valida el ID solo si se proporcionó.
        return _datos.ConsultarAsync(actor, id, soloActivos, ct); // Delega la consulta a la capa de datos.
    }

    public Task<PaginacionEntity<ClienteEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10, // Obtiene una página y valida los filtros.
        string? buscar = null, bool soloActivos = false, CancellationToken ct = default) =>
        _datos.ConsultarPaginaAsync(_sesion.Exigir(), pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), soloActivos, ct); // Revisa los parámetros de búsqueda y paginación.
    public async Task<ClienteEntity?> ObtenerPorIdAsync(int id, CancellationToken ct = default) => // Devuelve el cliente encontrado o null.
        (await ConsultarAsync(Validacion.Id(id, "IdCliente"), false, ct)).SingleOrDefault(); // Devuelve un resultado como máximo.
    public async Task CambiarEstadoAsync(int id, bool estado, CancellationToken ct = default) // Cambia el estado activo del cliente.
    {
        _sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista); // Solo permite guardar a los roles autorizados.
        var entidad = await ObtenerPorIdAsync(id, ct) ?? throw new ValidacionException("Registro inexistente."); // Devuelve el cliente encontrado o null.
        entidad.CambiarEstado(estado); // Cambia el estado del cliente.
        await GuardarEntidadAsync(entidad, ct); // Guarda el cambio en la base de datos.
    }

    public async Task<PaginacionEntity<ClienteEntity>> MostrarAsync(int pagina = 1, int tamPagina = 10, // Prepara la lista que se mostrará.
        string? buscar = null, bool? estado = null, CancellationToken ct = default)
    {
        ConsultaListado.Validar(pagina, tamPagina, buscar); // Revisa los parámetros de búsqueda y paginación.
        if (estado != false) // Para otros filtros, consulta la página directamente.
            return await ConsultarPaginaAsync(pagina, tamPagina, buscar, estado == true, ct);
        var registros = (await ConsultarAsync(soloActivos: false, ct: ct)) // Obtiene todos los estados para filtrar inactivos.
            .Where(x => !estado.HasValue || x.Estado == estado.Value); // Conserva los clientes que coinciden con el estado.
        return ConsultaListado.Paginar(registros, pagina, tamPagina, buscar, x => [x.Nombre, x.Apellido, x.Cedula, x.Telefono]); // Filtra por texto y devuelve la página solicitada.
    }
}
