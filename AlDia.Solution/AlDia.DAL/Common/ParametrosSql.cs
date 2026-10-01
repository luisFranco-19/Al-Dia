using Microsoft.Data.SqlClient;
using System.Data;
namespace AlDia.DAL.Common;

public static class ParametrosSql
{
    public static SqlParameter Entero(string nombre, int? valor) => new(nombre, SqlDbType.Int) { Value = (object?)valor ?? DBNull.Value };
    public static SqlParameter Bit(string nombre, bool valor) => new(nombre, SqlDbType.Bit) { Value = valor };
    public static SqlParameter Texto(string nombre, string? valor, int longitud, bool unicode = false) =>
        new(nombre, unicode ? SqlDbType.NVarChar : SqlDbType.VarChar, longitud) { Value = (object?)valor ?? DBNull.Value };
    public static SqlParameter Dinero(string nombre, decimal? valor) => new(nombre, SqlDbType.Decimal)
        { Precision = 10, Scale = 2, Value = (object?)valor ?? DBNull.Value };
    public static SqlParameter Salida(string nombre, SqlDbType tipo = SqlDbType.Int) => new(nombre, tipo) { Direction = ParameterDirection.Output };
    public static SqlParameter EntradaSalida(string nombre, int valor) => new(nombre, SqlDbType.Int)
        { Value = valor, Direction = ParameterDirection.InputOutput };
}
