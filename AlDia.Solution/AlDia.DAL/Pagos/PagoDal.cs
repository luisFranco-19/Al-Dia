using System.Data;
using AlDia.DAL.Common;
using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.Entity.Pagos;
namespace AlDia.DAL.Pagos;
public sealed class PagoDal : IPagoDal
{
    private readonly EjecutorSql _sql;

    public PagoDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public async Task<int> RegistrarAsync(int idActor, PagoSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RegistrarPago", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", s.IdOrden));
            cmd.Parameters.Add(ParametrosSql.Dinero("@Monto", s.Monto));
            cmd.Parameters.Add(ParametrosSql.Texto("@MetodoPago", s.MetodoPago.ToString(), 30));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", s.Observaciones, 300));
            cmd.Parameters.Add(ParametrosSql.Salida("@IdPago"));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdPago"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<int> CorregirAsync(int idActor, CorreccionPagoSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_CorregirPago", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdPago", s.IdPago));
            cmd.Parameters.Add(ParametrosSql.Dinero("@Monto", s.Monto));
            cmd.Parameters.Add(ParametrosSql.Texto("@MetodoPago", s.MetodoPago.ToString(), 30));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", s.Observaciones, 300));
            cmd.Parameters.Add(ParametrosSql.Texto("@Motivo", s.Motivo, 300));
            cmd.Parameters.Add(ParametrosSql.Salida("@IdPagoNuevo"));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdPagoNuevo"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task AnularAsync(int idActor, int idPago, string motivo, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_AnularPago", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdPago", idPago));
            cmd.Parameters.Add(ParametrosSql.Texto("@Motivo", motivo, 300));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    internal static PagoEntity Mapear(SqlDataReader r) => new(r.Entero("IdPago"), r.Entero("IdOrden"),
        r.Entero("IdUsuario"), r.Dinero("Monto"), Enum.Parse<MetodoPago>(r.Texto("MetodoPago")),
        r.Fecha("FechaPago"), r.TextoOpcional("Observaciones"), r.Bit("Anulado"), r.FechaOpcional("FechaAnulacion"),
        r.EnteroOpcional("IdUsuarioAnulacion"), r.TextoOpcional("MotivoAnulacion"));
    public async Task<IReadOnlyList<PagoEntity>> ConsultarAsync(int idActor, int? idOrden, bool incluirAnulados, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarPagos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));
            cmd.Parameters.Add(ParametrosSql.Bit("@IncluirAnulados", incluirAnulados));

            await cn.OpenAsync(ct);
            List<PagoEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<PaginacionEntity<PagoEntity>> ConsultarPaginaAsync(int idActor, int? idOrden, bool incluirAnulados,
        int pagina, int tamPagina, string? buscar, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarPagos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));
            cmd.Parameters.Add(ParametrosSql.Bit("@IncluirAnulados", incluirAnulados));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            List<PagoEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<PagoEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    public async Task<PagoEntity?> ObtenerPorIdAsync(int idActor, int idPago, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarPagos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdPago", idPago));
            cmd.Parameters.Add(ParametrosSql.Bit("@IncluirAnulados", true));
            await cn.OpenAsync(ct);
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            return await dr.ReadAsync(ct) ? Mapear(dr) : null;
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
