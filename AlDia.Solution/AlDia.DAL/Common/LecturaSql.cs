using Microsoft.Data.SqlClient;
namespace AlDia.DAL.Common;
internal static class LecturaSql
{
    public static int Entero(this SqlDataReader r, string c) => r.GetInt32(r.GetOrdinal(c));
    public static int? EnteroOpcional(this SqlDataReader r, string c) => r.IsDBNull(r.GetOrdinal(c)) ? null : r.Entero(c);
    public static string Texto(this SqlDataReader r, string c) => r.GetString(r.GetOrdinal(c));
    public static string? TextoOpcional(this SqlDataReader r, string c) => r.IsDBNull(r.GetOrdinal(c)) ? null : r.Texto(c);
    public static decimal Dinero(this SqlDataReader r, string c) => r.GetDecimal(r.GetOrdinal(c));
    public static decimal? DineroOpcional(this SqlDataReader r, string c) => r.IsDBNull(r.GetOrdinal(c)) ? null : r.Dinero(c);
    public static bool Bit(this SqlDataReader r, string c) => r.GetBoolean(r.GetOrdinal(c));
    public static DateTime Fecha(this SqlDataReader r, string c) => r.GetDateTime(r.GetOrdinal(c));
    public static DateTime? FechaOpcional(this SqlDataReader r, string c) => r.IsDBNull(r.GetOrdinal(c)) ? null : r.Fecha(c);
}
