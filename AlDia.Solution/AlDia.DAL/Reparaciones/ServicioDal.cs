using System.Data;
using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Reparaciones;
namespace AlDia.DAL.Reparaciones;
public sealed class ServicioDal : IServicioDal
{
    private readonly EjecutorSql _sql;

    public ServicioDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public Task<int> GuardarAsync(int idActor, ServicioSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        return s.IdServicio == 0
            ? AgregarAsync(idActor, s, ct)
            : ActualizarAsync(idActor, s, ct);
    }

    public Task<int> AgregarAsync(int idActor, ServicioSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.IdServicio != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    public Task<int> ActualizarAsync(int idActor, ServicioSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        Validacion.Id(s.IdServicio, "IdServicio");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    private async Task<int> GuardarRegistroAsync(int idActor, ServicioSolicitud s, CancellationToken ct)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_GuardarServicio", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.EntradaSalida("@IdServicio", s.IdServicio));
            cmd.Parameters.Add(ParametrosSql.Texto("@Nombre", s.Nombre, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Descripcion", s.Descripcion, 300));
            cmd.Parameters.Add(ParametrosSql.Dinero("@PrecioBase", s.PrecioBase));
            cmd.Parameters.Add(ParametrosSql.Bit("@Estado", s.Estado));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdServicio"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<IReadOnlyList<ServicioEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarServicios", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdServicio", id));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));

            await cn.OpenAsync(ct);
            List<ServicioEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    public async Task<int> RegistrarEnOrdenAsync(int idActor, ServicioOrdenSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RegistrarServicio", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", s.IdOrden));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdServicio", s.IdServicio));
            cmd.Parameters.Add(ParametrosSql.Entero("@Cantidad", s.Cantidad));
            cmd.Parameters.Add(ParametrosSql.Dinero("@Precio", s.Precio));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", s.Observaciones, 500));
            cmd.Parameters.Add(ParametrosSql.Salida("@IdDetalleServicio"));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdDetalleServicio"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task CorregirEnOrdenAsync(int idActor, CorreccionServicioOrdenSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_CorregirServicio", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdDetalleServicio", s.IdDetalleServicio));
            cmd.Parameters.Add(ParametrosSql.Entero("@Cantidad", s.Cantidad));
            cmd.Parameters.Add(ParametrosSql.Dinero("@Precio", s.Precio));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", s.Observaciones, 500));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task RetirarDeOrdenAsync(int idActor, int idDetalleServicio, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RetirarServicio", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdDetalleServicio", idDetalleServicio));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    private static ServicioEntity Mapear(SqlDataReader r)
    {
        ServicioEntity entidad = new();
        entidad.AsignarId(r.Entero("IdServicio"));
        entidad.CambiarNombre(r.Texto("Nombre"));
        entidad.CambiarDescripcion(r.TextoOpcional("Descripcion"));
        entidad.CambiarPrecioBase(r.Dinero("PrecioBase"));
        entidad.CambiarEstado(r.Bit("Estado"));
        return entidad;
    }
    public async Task<PaginacionEntity<ServicioEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarServicios", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            List<ServicioEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<ServicioEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
