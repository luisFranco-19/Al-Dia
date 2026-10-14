using AlDia.BLL.Seguridad;
using AlDia.DAL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
// Se conserva el namespace usado por Program.cs hasta la integracion de UI.
namespace AlDia.BLL;

public sealed class SistemaBll
{
    private readonly SistemaDal _datos;

    public SistemaBll(SistemaDal datos)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
    }

    public Task<EstadoInicializacion> ConsultarInicializacionAsync(CancellationToken ct = default) => _datos.ConsultarInicializacionAsync(ct);
    public async Task CrearAdministradorInicialAsync()
    {
        var estado = await _datos.ConsultarInicializacionAsync();
        if (!estado.HayAdministradorActivo)
            throw new ValidacionException("Debe registrar un administrador inicial con UsuarioBll antes de utilizar el sistema.");
    }
    public Task<int> CrearAdministradorInicialAsync(UsuarioSolicitud solicitud, string contrasena, CancellationToken ct = default) =>
        new UsuarioBll(_datos.Usuarios, new SesionUsuario()).CrearAdministradorInicialAsync(solicitud, contrasena, ct);
}
