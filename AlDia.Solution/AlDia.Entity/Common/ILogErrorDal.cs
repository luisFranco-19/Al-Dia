namespace AlDia.Entity.Common;

public interface ILogErrorDal
{
    Task<PaginacionEntity<LogErrorEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, CancellationToken ct = default);
}
