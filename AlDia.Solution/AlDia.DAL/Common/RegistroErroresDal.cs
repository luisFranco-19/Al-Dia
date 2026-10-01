using Microsoft.Data.SqlClient;
namespace AlDia.DAL.Common;

public interface IRegistroErroresDal
{
    Task RegistrarAsync(SqlException error, int? idActor, CancellationToken ct);
}
public sealed class RegistroErroresDal(IConexionFactory conexiones) : IRegistroErroresDal
{
    public async Task RegistrarAsync(SqlException error, int? idActor, CancellationToken ct)
    {
        using var cn = conexiones.Crear();
        using var cmd = cn.CreateCommand();
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.CommandText = "dbo.usp_RegistrarError";
        cmd.Parameters.AddRange([
            ParametrosSql.Texto("@MensajeError", error.Message, -1, true),
            ParametrosSql.Entero("@NumeroError", error.Number),
            ParametrosSql.Texto("@Procedimiento", string.IsNullOrEmpty(error.Procedure) ? null : error.Procedure[..Math.Min(100, error.Procedure.Length)], 100, true),
            ParametrosSql.Entero("@LineaError", error.LineNumber),
            ParametrosSql.Texto("@UsuarioApp", idActor?.ToString() ?? "SinSesion", 25, true)
        ]);
        await cn.OpenAsync(ct);
        await cmd.ExecuteNonQueryAsync(ct);
    }
}
