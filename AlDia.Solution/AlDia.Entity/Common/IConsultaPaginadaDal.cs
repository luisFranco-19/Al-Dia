namespace AlDia.Entity.Common;

public interface IConsultaPaginadaDal<T>
{
    Task<PaginacionEntity<T>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default);
}
