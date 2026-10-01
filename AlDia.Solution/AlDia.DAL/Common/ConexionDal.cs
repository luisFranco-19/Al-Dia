using AlDia.Entity.Entity;
namespace AlDia.DAL.Common;

public interface IConexionDal
{
    Task<bool> ProbarAsync(ConexionEntity datos, CancellationToken ct = default);
}
public sealed class ConexionDal : IConexionDal
{
    public async Task<bool> ProbarAsync(ConexionEntity datos, CancellationToken ct = default)
    {
        try
        {
            using var cn = new Microsoft.Data.SqlClient.SqlConnection(Conexion.ConstruirCadena(datos));
            await cn.OpenAsync(ct);
            return true;
        }
        catch (Microsoft.Data.SqlClient.SqlException) when (!ct.IsCancellationRequested) { return false; }
    }
}
