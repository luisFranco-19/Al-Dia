using AlDia.Entity.Common;

namespace AlDia.BLL.Common;

internal static class ConsultaListado
{
    public static string? Validar(int pagina, int tamPagina, string? buscar)
    {
        Validacion.Id(pagina, "Pagina");
        if (tamPagina < 1 || tamPagina > 200)
            throw new ValidacionException("El tamano de pagina debe estar entre 1 y 200.");
        return Validacion.Opcional(buscar, "Buscar", 100);
    }
}
