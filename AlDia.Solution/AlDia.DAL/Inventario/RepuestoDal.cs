using System.Data;
using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Inventario;
namespace AlDia.DAL.Inventario;
public sealed class RepuestoDal : IRepuestoDal
{
    private readonly EjecutorSql _sql;

    public RepuestoDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public Task<int> GuardarAsync(int idActor, RepuestoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        return s.IdRepuesto == 0
            ? AgregarAsync(idActor, s, ct)
            : ActualizarAsync(idActor, s, ct);
    }

    public Task<int> AgregarAsync(int idActor, RepuestoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.IdRepuesto != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    public Task<int> ActualizarAsync(int idActor, RepuestoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        Validacion.Id(s.IdRepuesto, "IdRepuesto");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    private async Task<int> GuardarRegistroAsync(int idActor, RepuestoSolicitud s, CancellationToken ct)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_GuardarRepuesto", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.EntradaSalida("@IdRepuesto", s.IdRepuesto));
            cmd.Parameters.Add(ParametrosSql.Texto("@Nombre", s.Nombre, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Descripcion", s.Descripcion, 300));
            cmd.Parameters.Add(ParametrosSql.Texto("@Marca", s.Marca, 50));
            cmd.Parameters.Add(ParametrosSql.Texto("@NumeroParte", s.NumeroParte, 100));
            cmd.Parameters.Add(ParametrosSql.Dinero("@PrecioCompra", s.PrecioCompra));
            cmd.Parameters.Add(ParametrosSql.Dinero("@PrecioVenta", s.PrecioVenta));
            cmd.Parameters.Add(ParametrosSql.Bit("@Estado", s.Estado));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdRepuesto"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<IReadOnlyList<RepuestoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarRepuestos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdRepuesto", id));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));

            await cn.OpenAsync(ct);
            List<RepuestoEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    public async Task<int> ConsumirAsync(int idActor, ConsumoRepuestoSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsumirRepuesto", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", s.IdOrden));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdRepuesto", s.IdRepuesto));
            cmd.Parameters.Add(ParametrosSql.Entero("@Cantidad", s.Cantidad));
            cmd.Parameters.Add(ParametrosSql.Dinero("@Precio", s.Precio));
            cmd.Parameters.Add(ParametrosSql.Salida("@IdDetalleRepuesto"));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdDetalleRepuesto"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task CorregirConsumoAsync(int idActor, CorreccionConsumoRepuestoSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_CorregirConsumoRepuesto", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdDetalleRepuesto", s.IdDetalleRepuesto));
            cmd.Parameters.Add(ParametrosSql.Entero("@Cantidad", s.Cantidad));
            cmd.Parameters.Add(ParametrosSql.Dinero("@Precio", s.Precio));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task RetirarConsumoAsync(int idActor, int idDetalleRepuesto, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RetirarConsumoRepuesto", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdDetalleRepuesto", idDetalleRepuesto));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task AjustarExistenciasAsync(int idActor, int idRepuesto, int variacion, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_AjustarExistencias", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdRepuesto", idRepuesto));
            cmd.Parameters.Add(ParametrosSql.Entero("@Variacion", variacion));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    private static RepuestoEntity Mapear(SqlDataReader r)
    {
        RepuestoEntity entidad = new();
        entidad.AsignarId(r.Entero("IdRepuesto"));
        entidad.CambiarNombre(r.Texto("Nombre"));
        entidad.CambiarDescripcion(r.TextoOpcional("Descripcion"));
        entidad.CambiarMarca(r.TextoOpcional("Marca"));
        entidad.CambiarNumeroParte(r.TextoOpcional("NumeroParte"));
        entidad.CambiarPrecios(r.Dinero("PrecioCompra"), r.Dinero("PrecioVenta"));
        entidad.CambiarEstado(r.Bit("Estado"));
        entidad.AsignarStockConsultado(r.Entero("Stock"));
        return entidad;
    }
    public async Task<PaginacionEntity<RepuestoEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarRepuestos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            List<RepuestoEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<RepuestoEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
