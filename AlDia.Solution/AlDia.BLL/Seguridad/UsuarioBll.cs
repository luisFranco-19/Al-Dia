using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
namespace AlDia.BLL.Seguridad;

public sealed class UsuarioBll
{
    private readonly IUsuarioDal _datos;
    private readonly SesionUsuario _sesion;
    private readonly IHashContrasena _hash;
    private static readonly string HashFicticio = new HashContrasena().Crear("CuentaNoExistente");
    public UsuarioBll(IUsuarioDal datos, SesionUsuario sesion, IHashContrasena? hash = null)
    {
        _datos = datos ?? throw new ArgumentNullException(nameof(datos));
        _sesion = sesion ?? throw new ArgumentNullException(nameof(sesion));
        _hash = hash ?? new HashContrasena();
    }
    public Task<EstadoInicializacion> ConsultarInicializacionAsync(CancellationToken ct = default) => _datos.ConsultarInicializacionAsync(ct);
    public async Task<UsuarioEntity> AutenticarAsync(string usuario, string contrasena, CancellationToken ct = default)
    {
        _sesion.Cerrar();
        if (string.IsNullOrWhiteSpace(usuario) || usuario.Trim().Length > 50 || string.IsNullOrEmpty(contrasena) || contrasena.Length > 128)
            throw new ValidacionException("Usuario o contrasena incorrectos.");
        var credencial = await _datos.BuscarCredencialAsync(usuario.Trim(), ct);
        bool valida = _hash.Verificar(contrasena, credencial?.Hash ?? HashFicticio);
        if (!valida || credencial is null || !credencial.Usuario.Estado) throw new ValidacionException("Usuario o contrasena incorrectos.");
        ct.ThrowIfCancellationRequested();
        _sesion.Iniciar(credencial.Usuario);
        return credencial.Usuario;
    }
    public Task<int> CrearAdministradorInicialAsync(UsuarioSolicitud solicitud, string contrasena, CancellationToken ct = default)
    {
        var s = Validar(solicitud);
        if (s.IdUsuario != 0 || s.Rol != RolUsuario.Administrador || !s.Estado)
            throw new ValidacionException("El primer usuario debe ser un administrador activo nuevo.");
        ValidarContrasena(contrasena);
        return _datos.CrearAdministradorInicialAsync(s, _hash.Crear(contrasena), ct);
    }
    public Task<int> GuardarAsync(UsuarioSolicitud solicitud, string? contrasena = null, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        var s = Validar(solicitud);
        if (s.IdUsuario == 0 || contrasena is not null) ValidarContrasena(contrasena);
        string? hash = contrasena is null ? null : _hash.Crear(contrasena);
        return _datos.GuardarAsync(actor, s, hash, ct);
    }
    public async Task<int> GuardarEntidadAsync(UsuarioEntity usuario, string? contrasena = null, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(usuario);
        int id = await GuardarAsync(usuario.CrearSolicitud(), contrasena, ct);
        usuario.AsignarId(id);
        return id;
    }
    public Task<IReadOnlyList<UsuarioEntity>> ConsultarAsync(int? idUsuario = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = _sesion.Exigir(RolUsuario.Administrador);
        if (idUsuario.HasValue) Validacion.Id(idUsuario.Value, "Usuario");
        return _datos.ConsultarAsync(actor, idUsuario, soloActivos, ct);
    }
    public void CerrarSesion() => _sesion.Cerrar();
    private static void ValidarContrasena(string? valor)
    {
        if (valor is null || valor.Length < 8 || valor.Length > 128 || string.IsNullOrWhiteSpace(valor))
            throw new ValidacionException("La contrasena debe tener entre 8 y 128 caracteres.");
    }
    private static UsuarioSolicitud Validar(UsuarioSolicitud s)
    {
        ArgumentNullException.ThrowIfNull(s);
        return new UsuarioEntity(s.IdUsuario, s.Nombre, s.Apellido, s.Cedula, s.Telefono,
            s.Correo, s.Usuario, s.Rol, s.Estado, DateTime.Now).CrearSolicitud();
    }
}
