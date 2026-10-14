using System.Data;
using Microsoft.Data.SqlClient;
using AlDia.Entity.Common;

namespace AlDia.DAL.Common;

public sealed class LogErrorDal : ILogErrorDal
{
    private readonly EjecutorSql _sql;

    public LogErrorDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public async Task<PaginacionEntity<LogErrorEntity>> ConsultarPaginaAsync(int idActor, int pagina,
        int tamPagina, string? buscar, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarLogErrores", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            Func<SqlDataReader, LogErrorEntity> mapear = r => new LogErrorEntity(r.Entero("idLog"), r.Texto("mensajeError"), r.EnteroOpcional("numeroError"),
                r.TextoOpcional("procedimiento"), r.EnteroOpcional("lineaError"), r.Texto("usuarioApp"), r.FechaOpcional("fechaError"));
            List<LogErrorEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(mapear(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<LogErrorEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
