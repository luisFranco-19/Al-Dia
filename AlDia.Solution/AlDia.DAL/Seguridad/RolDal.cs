using System.Data;
using AlDia.DAL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using Microsoft.Data.SqlClient;

namespace AlDia.DAL.Seguridad;

public sealed class RolDal : IRolDal
{
    private readonly EjecutorSql _sql; 

    public RolDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql)); // Inyección de dependencia para la ejecución de SQL
    }

    // Convierte una fila de SQL en un rol.
    private static RolEntity Mapear(SqlDataReader r)
    {
        //Mapeamos los datos del lector de SQL a una entidad de rol
        RolEntity entidad = new();
        entidad.AsignarId(r.Entero("IdRol"));
        entidad.CambiarNombre(r.Texto("Nombre"));
        entidad.CambiarDescripcion(r.TextoOpcional("Descripcion"));
        entidad.CambiarEstado(r.Bit("Estado"));
        return entidad;
    }
    public Task<int> GuardarAsync(int idActor, RolSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s); // Validamos que el objeto de solicitud no sea nulo
        return s.IdRol == 0
            ? AgregarAsync(idActor, s, ct)// Agregar un nuevo rol
            : ActualizarAsync(idActor, s, ct);// Actualizar un rol existente
    }

    public Task<int> AgregarAsync(int idActor, RolSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.IdRol != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    public Task<int> ActualizarAsync(int idActor, RolSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        Validacion.Id(s.IdRol, "IdRol");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    // Ejecuta el procedimiento que guarda el rol.
    private async Task<int> GuardarRegistroAsync(int idActor, RolSolicitud s, CancellationToken ct)
    {
        try
        {
            // Se abre la conexión y se prepara el comando para ejecutar el procedimiento almacenado
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_GuardarRol", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.EntradaSalida("@IdRol", s.IdRol));
            cmd.Parameters.Add(ParametrosSql.Texto("@Nombre", s.Nombre, 50));
            cmd.Parameters.Add(ParametrosSql.Texto("@Descripcion", s.Descripcion, 200));
            cmd.Parameters.Add(ParametrosSql.Bit("@Estado", s.Estado));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdRol"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    // Lee los roles disponibles desde SQL.
    public async Task<IReadOnlyList<RolEntity>> ConsultarAsync(int idActor, int? idRol = null, bool soloActivos = true, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarRoles", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdRol", idRol));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));

            await cn.OpenAsync(ct);
            List<RolEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    // Lee los roles disponibles desde SQL con paginación.
    public async Task<PaginacionEntity<RolEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarRoles", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            List<RolEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<RolEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
