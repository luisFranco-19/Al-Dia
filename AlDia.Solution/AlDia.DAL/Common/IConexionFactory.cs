using Microsoft.Data.SqlClient;
namespace AlDia.DAL.Common;

public interface IConexionFactory
{
    SqlConnection Crear();
}
public sealed class ConexionFactory : IConexionFactory
{
    private readonly string _cadena;
    public ConexionFactory(string cadena)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cadena);
        _cadena = new SqlConnectionStringBuilder(cadena).ConnectionString;
    }
    public SqlConnection Crear() => new(_cadena);
}
// Adaptador para la configuracion estatica utilizada por el arranque actual.
public sealed class ConexionActualFactory : IConexionFactory
{
    public SqlConnection Crear() => Conexion.ObtenerConexion();
}
