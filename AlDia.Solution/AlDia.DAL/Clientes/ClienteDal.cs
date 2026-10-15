using System.Data;
using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Clientes;
namespace AlDia.DAL.Clientes;
public sealed class ClienteDal : IClienteDal // Implementa el acceso a datos de clientes.
{
    private readonly EjecutorSql _sql; // Guarda el servicio que administra las conexiones SQL.

    public ClienteDal(EjecutorSql sql) // Recibe el servicio necesario para conectarse a SQL Server.
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public Task<int> GuardarAsync(int idActor, ClienteSolicitud s, CancellationToken ct = default) // Elige entre agregar o actualizar según el ID.
    {
        ArgumentNullException.ThrowIfNull(s);
        return s.IdCliente == 0 // ID cero significa que el cliente es nuevo.
            ? AgregarAsync(idActor, s, ct)
            : ActualizarAsync(idActor, s, ct);
    }

    public Task<int> AgregarAsync(int idActor, ClienteSolicitud s, CancellationToken ct = default) // Valida y prepara el alta de un cliente.
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.IdCliente != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    public Task<int> ActualizarAsync(int idActor, ClienteSolicitud s, CancellationToken ct = default) // Valida y prepara la actualización de un cliente.
    {
        ArgumentNullException.ThrowIfNull(s);
        Validacion.Id(s.IdCliente, "IdCliente");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    private async Task<int> GuardarRegistroAsync(int idActor, ClienteSolicitud s, CancellationToken ct) // Ejecuta el procedimiento que guarda el cliente.
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_GuardarCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure; // Indica que se ejecutará un procedimiento almacenado.
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor)); // Identifica al usuario que realiza el cambio.
            cmd.Parameters.Add(ParametrosSql.EntradaSalida("@IdCliente", s.IdCliente)); // SQL recibe y devuelve el ID del cliente.
            cmd.Parameters.Add(ParametrosSql.Texto("@Nombre", s.Nombre, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Apellido",
                s.Apellido, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Telefono", s.Telefono, 20));
            cmd.Parameters.Add(ParametrosSql.Texto("@Cedula", s.Cedula, 20));
            cmd.Parameters.Add(ParametrosSql.Bit("@Estado", s.Estado)); // Envía el estado activo o inactivo.

            await cn.OpenAsync(ct); // Abre la conexión con la base de datos.
            await cmd.ExecuteNonQueryAsync(ct); // Ejecuta el procedimiento de guardado.
            return Convert.ToInt32(cmd.Parameters["@IdCliente"].Value); // Devuelve el ID asignado por SQL.
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested) // Traduce errores SQL; respeta las cancelaciones.
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    // Consulta clientes y permite filtrar por ID o estado.
    public async Task<IReadOnlyList<ClienteEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true,
        CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarClientes", cn); // Prepara el procedimiento que consulta clientes.
            cmd.CommandType = CommandType.StoredProcedure; // Indica que se ejecutará un procedimiento almacenado.
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor)); // Identifica al usuario que realiza el cambio.
            cmd.Parameters.Add(ParametrosSql.Entero("@IdCliente", id));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos)); // Envía el filtro para consultar activos o todos.

            await cn.OpenAsync(ct); // Abre la conexión con la base de datos.
            List<ClienteEntity> registros = []; // Crea la lista que recibirá los resultados.
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr)); // Lee las filas y convierte cada una en cliente.
            return registros.AsReadOnly(); // Devuelve los resultados como una lista de solo lectura.
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested) // Traduce errores SQL; respeta las cancelaciones.
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    private static ClienteEntity Mapear(SqlDataReader r) // Convierte una fila de SQL en una entidad ClienteEntity.
    {
        ClienteEntity entidad = new(); // Crea el objeto que se llenará con los datos de la fila.
        entidad.AsignarId(r.Entero("IdCliente")); // Copia el ID leído desde SQL.
        entidad.CambiarNombre(r.Texto("Nombre"));
        entidad.CambiarApellido(r.Texto("Apellido"));
        entidad.CambiarTelefono(r.Texto("Telefono"));
        entidad.CambiarCedula(r.Texto("Cedula"));
        entidad.CambiarEstado(r.Bit("Estado")); // Copia el estado del cliente.
        return entidad; // Devuelve el cliente con sus datos cargados.
    }
    // Consulta clientes por páginas y devuelve el total encontrado.
    public async Task<PaginacionEntity<ClienteEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarClientes", cn); // Prepara el procedimiento que consulta clientes.
            cmd.CommandType = CommandType.StoredProcedure; // Indica que se ejecutará un procedimiento almacenado.
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor)); // Identifica al usuario que realiza el cambio.
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos)); // Envía el filtro para consultar activos o todos.
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina)); // Envía el número de página solicitado.
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina)); // Envía cuántos registros tendrá cada página.
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true)); // Envía el texto para filtrar la búsqueda.
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros")); // Solicita el total de resultados mediante OUTPUT.

            await cn.OpenAsync(ct); // Abre la conexión con la base de datos.
            List<ClienteEntity> registros = []; // Crea la lista que recibirá los resultados.
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct)) // Lee los resultados y libera el lector al terminar.
            {
                while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr)); // Lee las filas y convierte cada una en cliente.
            }
            return new PaginacionEntity<ClienteEntity>(registros, // Devuelve los resultados junto con la información de paginación.
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina); // Convierte el total devuelto por SQL a número.
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested) // Traduce errores SQL; respeta las cancelaciones.
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
