using AlDia.Entity.Common;

namespace AlDia.DAL.Common;

public sealed class LogErrorDal(EjecutorSql sql) : ILogErrorDal
{
    public Task<PaginacionEntity<LogErrorEntity>> ConsultarPaginaAsync(int idActor, int pagina,
        int tamPagina, string? buscar, CancellationToken ct = default) =>
        sql.ConsultarPaginaAsync("dbo.usp_ConsultarLogErrores", [ParametrosSql.Entero("@IdUsuarioActor", idActor)],
            r => new LogErrorEntity(r.Entero("idLog"), r.Texto("mensajeError"), r.EnteroOpcional("numeroError"),
                r.TextoOpcional("procedimiento"), r.EnteroOpcional("lineaError"), r.Texto("usuarioApp"), r.FechaOpcional("fechaError")),
            idActor, pagina, tamPagina, buscar, ct);
}
