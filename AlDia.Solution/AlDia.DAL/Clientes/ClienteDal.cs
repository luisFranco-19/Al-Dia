using System.Data;
using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Clientes;
namespace AlDia.DAL.Clientes;
public sealed class ClienteDal : IClienteDal
{
    private readonly EjecutorSql _sql;

    public ClienteDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public Task<int> GuardarAsync(int idActor, ClienteSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        return s.IdCliente == 0
            ? AgregarAsync(idActor, s, ct)
            : ActualizarAsync(idActor, s, ct);
    }

    public Task<int> AgregarAsync(int idActor, ClienteSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.IdCliente != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    public Task<int> ActualizarAsync(int idActor, ClienteSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        Validacion.Id(s.IdCliente, "IdCliente");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    private async Task<int> GuardarRegistroAsync(int idActor, ClienteSolicitud s, CancellationToken ct)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_GuardarCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.EntradaSalida("@IdCliente", s.IdCliente));
            cmd.Parameters.Add(ParametrosSql.Texto("@Nombre", s.Nombre, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Apellido",
                s.Apellido, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Telefono", s.Telefono, 20));
            cmd.Parameters.Add(ParametrosSql.Texto("@Cedula", s.Cedula, 20));
            cmd.Parameters.Add(ParametrosSql.Bit("@Estado", s.Estado));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdCliente"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<IReadOnlyList<ClienteEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true,
        CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarClientes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdCliente", id));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));

            await cn.OpenAsync(ct);
            List<ClienteEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    private static ClienteEntity Mapear(SqlDataReader r)
    {
        ClienteEntity entidad = new();
        entidad.AsignarId(r.Entero("IdCliente"));
        entidad.CambiarNombre(r.Texto("Nombre"));
        entidad.CambiarApellido(r.Texto("Apellido"));
        entidad.CambiarTelefono(r.Texto("Telefono"));
        entidad.CambiarCedula(r.Texto("Cedula"));
        entidad.CambiarEstado(r.Bit("Estado"));
        return entidad;
    }
    public async Task<PaginacionEntity<ClienteEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarClientes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            List<ClienteEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<ClienteEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
