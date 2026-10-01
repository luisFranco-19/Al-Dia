using AlDia.DAL.Common;
using AlDia.Entity.Entity;
namespace AlDia.BLL.Common;

public sealed class ConexionBll
{
    private readonly IConexionDal _datos;
    public ConexionBll() : this(new ConexionDal()) { }
    public ConexionBll(IConexionDal datos) => _datos = datos ?? throw new ArgumentNullException(nameof(datos));
    public Task<bool> ProbarConexion(string servidor, string baseDatos, string usuario, string password,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(servidor) || string.IsNullOrWhiteSpace(baseDatos) || string.IsNullOrWhiteSpace(usuario))
            return Task.FromResult(false);
        return _datos.ProbarAsync(new ConexionEntity
        { Servidor = servidor.Trim(), BaseDatos = baseDatos.Trim(), UsuarioSql = usuario.Trim(), Password = password }, ct);
    }
}
