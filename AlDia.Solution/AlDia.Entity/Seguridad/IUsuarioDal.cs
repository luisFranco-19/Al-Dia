namespace AlDia.Entity.Seguridad;

public interface IUsuarioDal
{
    Task<EstadoInicializacion> ConsultarInicializacionAsync(CancellationToken ct = default);
    Task<CredencialUsuario?> BuscarCredencialAsync(string usuario, CancellationToken ct = default);
    Task<int> CrearAdministradorInicialAsync(UsuarioSolicitud solicitud, string hash, CancellationToken ct = default);
    Task<int> GuardarAsync(int idActor, UsuarioSolicitud solicitud, string? hash, CancellationToken ct = default);
    Task<IReadOnlyList<UsuarioEntity>> ConsultarAsync(int idActor, int? idUsuario = null, bool soloActivos = true, CancellationToken ct = default);
}
