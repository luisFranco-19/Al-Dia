using AlDia.Entity.Entity;
using Microsoft.Data.SqlClient;
namespace AlDia.DAL.Common;

public static class Conexion
{
    public static string CadenaConexion { get; set; } = string.Empty;
    public static SqlConnection ObtenerConexion() => new(CadenaConexion);
    public static string ConstruirCadena(ConexionEntity datos) => new SqlConnectionStringBuilder
    {
        DataSource = datos.Servidor, InitialCatalog = datos.BaseDatos,
        UserID = datos.UsuarioSql, Password = datos.Password,
        TrustServerCertificate = true, ConnectTimeout = 15
    }.ConnectionString;
}
