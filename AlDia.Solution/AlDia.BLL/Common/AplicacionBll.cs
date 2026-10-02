using AlDia.BLL.Clientes;
using AlDia.BLL.Equipos;
using AlDia.BLL.Inventario;
using AlDia.BLL.Pagos;
using AlDia.BLL.Reparaciones;
using AlDia.BLL.Seguridad;
using AlDia.DAL.Common;
namespace AlDia.BLL.Common;

// Composicion de una sesion y sus servicios. Crear una instancia por sesion de la aplicacion.
public sealed class AplicacionBll
{
    public SesionUsuario Sesion { get; } = new();
    public RolBll Roles { get; }
    public LogErrorBll Errores { get; }
    public UsuarioBll Usuarios { get; }
    public ClienteBll Clientes { get; }
    public EquipoBll Equipos { get; }
    public TipoEquipoBll TiposEquipo { get; }
    public OrdenReparacionBll Ordenes { get; }
    public DiagnosticoBll Diagnosticos { get; }
    public ServicioBll Servicios { get; }
    public RepuestoBll Repuestos { get; }
    public PagoBll Pagos { get; }
    public AplicacionBll(string cadena) : this(new ConexionFactory(cadena)) { }
    public AplicacionBll(IConexionFactory conexiones)
    {
        var sql = new EjecutorSql(conexiones);
        Roles = new(new AlDia.DAL.Seguridad.RolDal(sql), Sesion);
        Errores = new(new LogErrorDal(sql), Sesion);
        Usuarios = new(new AlDia.DAL.Seguridad.UsuarioDal(sql), Sesion);
        Clientes = new(new AlDia.DAL.Clientes.ClienteDal(sql), Sesion);
        Equipos = new(new AlDia.DAL.Equipos.EquipoDal(sql), Sesion);
        TiposEquipo = new(new AlDia.DAL.Equipos.TipoEquipoDal(sql), Sesion);
        Ordenes = new(new AlDia.DAL.Reparaciones.OrdenReparacionDal(sql), Sesion);
        Diagnosticos = new(new AlDia.DAL.Reparaciones.DiagnosticoDal(sql), Sesion);
        Servicios = new(new AlDia.DAL.Reparaciones.ServicioDal(sql), Sesion);
        Repuestos = new(new AlDia.DAL.Inventario.RepuestoDal(sql), Sesion);
        Pagos = new(new AlDia.DAL.Pagos.PagoDal(sql), Sesion);
    }
    public static AplicacionBll DesdeConexionActual() => new(new ConexionActualFactory());
}
