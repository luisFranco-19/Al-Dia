using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;

namespace AlDia.BLL.Common;

public sealed class LogErrorBll
{
    private readonly ILogErrorDal _datos;
    private readonly SesionUsuario _sesion;

    public LogErrorBll(ILogErrorDal datos, SesionUsuario sesion)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
    }

    public Task<PaginacionEntity<LogErrorEntity>> ConsultarPaginaAsync(int pagina = 1, int tamPagina = 10,
        string? buscar = null, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        return _datos.ConsultarPaginaAsync(actor, pagina, tamPagina,
            ConsultaListado.Validar(pagina, tamPagina, buscar), ct);
    }
}
