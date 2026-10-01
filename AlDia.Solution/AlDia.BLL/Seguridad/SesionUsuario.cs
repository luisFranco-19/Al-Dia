using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
namespace AlDia.BLL.Seguridad;

public sealed class SesionUsuario
{
    private UsuarioEntity? _usuario;
    public UsuarioEntity? UsuarioActual => _usuario?.Copiar();
    public bool EstaAutenticada => _usuario is not null;
    internal void Iniciar(UsuarioEntity usuario) => _usuario = usuario.Copiar();
    public void Cerrar() => _usuario = null;
    internal int Exigir(params RolUsuario[] roles)
    {
        var usuario = _usuario ?? throw new PermisoException("Debe iniciar sesion en el sistema.");
        if (!usuario.Estado || (roles.Length > 0 && !roles.Contains(usuario.Rol)))
            throw new PermisoException("Su usuario no tiene permiso para esta operacion.");
        return usuario.IdUsuario;
    }
}
