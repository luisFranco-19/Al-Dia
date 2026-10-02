namespace AlDia.Entity.Common;

// Resultado comun para los listados; la DAL realizara la paginacion en SQL.
public sealed class PaginacionEntity<T>
{
    public IReadOnlyList<T> Registros { get; }
    public int TotalRegistros { get; }
    public int PaginaActual { get; }
    public int TamPagina { get; }
    public int TotalPaginas => TotalRegistros == 0 ? 0 : (int)(((long)TotalRegistros + TamPagina - 1) / TamPagina);
    public bool TienePaginaAnterior => TotalPaginas > 0 && PaginaActual > 1;
    public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;

    public PaginacionEntity(IEnumerable<T> registros, int totalRegistros, int paginaActual, int tamPagina)
    {
        ArgumentNullException.ThrowIfNull(registros);
        if (totalRegistros < 0) throw new ValidacionException("TotalRegistros no puede ser negativo.");
        Validacion.Id(paginaActual, "PaginaActual");
        Validacion.Id(tamPagina, "TamPagina");

        var copia = registros.ToList();
        if (copia.Count > tamPagina || copia.Count > totalRegistros)
            throw new ValidacionException("La cantidad de registros no corresponde a la paginacion.");

        // Se copia la coleccion para que el llamador no pueda agregar o quitar filas del resultado.
        Registros = copia.AsReadOnly();
        TotalRegistros = totalRegistros;
        PaginaActual = paginaActual;
        TamPagina = tamPagina;
    }
}
