using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;

namespace AlDia.DAL.Common;

public static class SqlErrorHelper
{
    public static DatosException TraducirError(SqlException ex)
    {
        ArgumentNullException.ThrowIfNull(ex);
        // Los códigos pertenecen a los procedimientos de Al Día.
        string mensaje = ex.Number switch
        {
            >= 51000 and <= 51003 => ex.Message,
            2601 or 2627 => "Ya existe un registro con esos datos únicos.",
            547 => "La operación incumple una relación o restricción de la base de datos.",
            _ => "No fue posible completar la operación en la base de datos."
        };
        return new DatosException(mensaje, ex.Number, ex.Procedure, ex.LineNumber, ex);
    }
}
