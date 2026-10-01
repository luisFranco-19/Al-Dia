using AlDia.DAL.Seguridad;
using AlDia.Entity.Seguridad;
namespace AlDia.DAL.Common;

public sealed class SistemaDal
{
    public IUsuarioDal Usuarios { get; }
    public SistemaDal() : this(new EjecutorSql(new ConexionActualFactory())) { }
    public SistemaDal(EjecutorSql sql) => Usuarios = new UsuarioDal(sql);
    public Task<EstadoInicializacion> ConsultarInicializacionAsync(CancellationToken ct = default) => Usuarios.ConsultarInicializacionAsync(ct);
}
