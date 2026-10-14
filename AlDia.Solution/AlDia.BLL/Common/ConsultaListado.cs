using AlDia.Entity.Common;

namespace AlDia.BLL.Common;

internal static class ConsultaListado
{
    public static async Task<PaginacionEntity<T>> FiltrarPaginasAsync<T>(int pagina, int tamPagina,
        Func<int, Task<PaginacionEntity<T>>> consultar, Func<T, bool> filtro, CancellationToken ct)
    {
        List<T> registros = [];
        int numero = 1;
        PaginacionEntity<T> lote;
        do
        {
            ct.ThrowIfCancellationRequested();
            lote = await consultar(numero++);
            registros.AddRange(lote.Registros.Where(filtro));
        } while (numero <= lote.TotalPaginas);
        return Paginar(registros, pagina, tamPagina, null, _ => []);
    }
    // Para filtros que los procedimientos actuales no reciben, filtrar antes de paginar.
    // Las consultas sin esos filtros siguen utilizando la paginación de SQL.
    public static PaginacionEntity<T> Paginar<T>(IEnumerable<T> registros, int pagina, int tamPagina,
        string? buscar, Func<T, string?[]> campos)
    {
        buscar = Validar(pagina, tamPagina, buscar);
        var coincidencias = registros.Where(x => buscar is null || campos(x).Any(campo => campo is not null
            && System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(campo, buscar,
                System.Globalization.CompareOptions.IgnoreCase | System.Globalization.CompareOptions.IgnoreNonSpace) >= 0)).ToList();
        long inicio = (long)(pagina - 1) * tamPagina;
        var lote = inicio >= coincidencias.Count ? [] : coincidencias.Skip((int)inicio).Take(tamPagina).ToList();
        return new PaginacionEntity<T>(lote, coincidencias.Count, pagina, tamPagina);
    }
    public static string? Validar(int pagina, int tamPagina, string? buscar)
    {
        Validacion.Id(pagina, "Pagina");
        if (tamPagina < 1 || tamPagina > 200)
            throw new ValidacionException("El tamano de pagina debe estar entre 1 y 200.");
        return Validacion.Opcional(buscar, "Buscar", 100);
    }
}
