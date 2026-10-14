using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
namespace AlDia.DAL.Common;

public sealed class EjecutorSql
{
    private readonly IConexionFactory _conexiones;
    private readonly IRegistroErroresDal _errores;
    public EjecutorSql(IConexionFactory conexiones, IRegistroErroresDal? errores = null)
    {
        _conexiones = conexiones ?? throw new ArgumentNullException(nameof(conexiones));
        _errores = errores ?? new RegistroErroresDal(conexiones);
    }
    // Cada DAL crea y ejecuta su SqlCommand; este contexto comparte conexión y auditoría.
    public SqlConnection ObtenerConexion() => _conexiones.Crear();

    public async Task<DatosException> TraducirErrorAsync(SqlException ex, int? idActor)
    {
        try { await _errores.RegistrarAsync(ex, idActor, CancellationToken.None); }
        catch (Exception logError) { ex.Data["ErrorRegistro"] = logError.GetType().Name; }
        return SqlErrorHelper.TraducirError(ex);
    }
}
