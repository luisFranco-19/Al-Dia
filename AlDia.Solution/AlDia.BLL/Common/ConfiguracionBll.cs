using AlDia.DAL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Entity;

namespace AlDia.BLL.Common;

public sealed class ConfiguracionBll(IConfiguracionDal datos)
{
    public bool ExisteConfiguracion() => datos.ExisteConfiguracion();
    public ConexionEntity? Obtener() => datos.Obtener();
    public void Guardar(ConexionEntity config)
    {
        ArgumentNullException.ThrowIfNull(config);
        var validada = new ConexionEntity
        {
            Servidor = Validacion.Texto(config.Servidor, "Servidor", 128),
            BaseDatos = Validacion.Texto(config.BaseDatos, "Base de datos", 128),
            UsuarioSql = Validacion.Texto(config.UsuarioSql, "Usuario SQL", 128),
            Password = config.Password
        };
        if (string.IsNullOrEmpty(validada.Password)) throw new ValidacionException("La contrasena SQL es obligatoria.");
        if (!datos.Guardar(validada)) throw new ValidacionException("No fue posible guardar la configuracion de conexion.");
    }
}
