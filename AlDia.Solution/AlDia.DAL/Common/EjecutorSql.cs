using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using System.Data;
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
    public async Task<T> EjecutarAsync<T>(string procedimiento, SqlParameter[] parametros,
        Func<SqlCommand, CancellationToken, Task<T>> operacion, int? idActor, CancellationToken ct)
    {
        try
        {
            using var cn = _conexiones.Crear();
            using var cmd = new SqlCommand(procedimiento, cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddRange(parametros);
            await cn.OpenAsync(ct);
            return await operacion(cmd, ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            // El log usa otra conexion, despues de liberar la conexion de la operacion fallida.
            try { await _errores.RegistrarAsync(ex, idActor, CancellationToken.None); }
            catch (Exception logError) { ex.Data["ErrorRegistro"] = logError.GetType().Name; }
            var mensaje = ex.Number switch
            {
                >= 51000 and <= 51003 => ex.Message,
                2601 or 2627 => "Ya existe un registro con esos datos unicos.",
                547 => "La operacion incumple una relacion o restriccion de la base de datos.",
                _ => "No fue posible completar la operacion en la base de datos."
            };
            throw new DatosException(mensaje, ex.Number, ex.Procedure, ex.LineNumber, ex);
        }
    }
    public Task<int> EscribirAsync(string procedimiento, SqlParameter[] parametros, string? salida, int? idActor, CancellationToken ct) =>
        EjecutarAsync(procedimiento, parametros, async (cmd, token) =>
        {
            await cmd.ExecuteNonQueryAsync(token);
            return salida is null ? 0 : Convert.ToInt32(cmd.Parameters[salida].Value);
        }, idActor, ct);
    public Task<IReadOnlyList<T>> ConsultarAsync<T>(string procedimiento, SqlParameter[] parametros,
        Func<SqlDataReader, T> mapear, int? idActor, CancellationToken ct) =>
        EjecutarAsync<IReadOnlyList<T>>(procedimiento, parametros, async (cmd, token) =>
        {
            using var reader = await cmd.ExecuteReaderAsync(token);
            var lista = new List<T>();
            while (await reader.ReadAsync(token)) lista.Add(mapear(reader));
            return lista.AsReadOnly();
        }, idActor, ct);

    // La consulta SQL obtiene una pagina y devuelve su total con el mismo filtro.
    public Task<PaginacionEntity<T>> ConsultarPaginaAsync<T>(string procedimiento, SqlParameter[] parametros,
        Func<SqlDataReader, T> mapear, int idActor, int pagina, int tamPagina, string? buscar, CancellationToken ct) =>
        EjecutarAsync(procedimiento, [..parametros, ParametrosSql.Entero("@Pagina", pagina),
            ParametrosSql.Entero("@TamPagina", tamPagina), ParametrosSql.Texto("@Buscar", buscar, 100, true),
            ParametrosSql.Salida("@TotalRegistros")], async (cmd, token) =>
        {
            var registros = new List<T>();
            using (var reader = await cmd.ExecuteReaderAsync(token))
                while (await reader.ReadAsync(token)) registros.Add(mapear(reader));
            // Los parametros OUTPUT se leen despues de cerrar el reader.
            return new PaginacionEntity<T>(registros, Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }, idActor, ct);
}
