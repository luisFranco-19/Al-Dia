using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;

namespace AlDia.BLL.Common;

public sealed class LogErrorBll(ILogErrorDal datos, SesionUsuario sesion)
{
    public Task<PaginacionEntity<LogErrorEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Administrador);
        return datos.ConsultarPaginaAsync(actor, pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), ct);
    }
}
