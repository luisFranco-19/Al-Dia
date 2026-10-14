using AlDia.BLL.Common;

namespace AlDia.UI.Formularios;

/// <summary>
/// Permite a los formularios secundarios alojados en el Dashboard recibir los servicios de la aplicación.
/// </summary>
public interface IModuloConServicios
{
    void ConfigurarServicios(AplicacionBll aplicacion);
}
