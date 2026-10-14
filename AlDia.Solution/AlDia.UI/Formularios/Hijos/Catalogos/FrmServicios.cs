using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Reparaciones;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Catalogos;

public partial class FrmServicios : FrmListadoBase
{
    public FrmServicios()
    {
        InitializeComponent();
        VincularColumna("colNombre", nameof(ServicioEntity.Nombre));
        VincularColumna("colDescripcion", nameof(ServicioEntity.Descripcion));
        VincularColumna("colPrecio", nameof(ServicioEntity.PrecioBase));
        VincularColumna("colEstado", nameof(ServicioEntity.EstadoNombre));
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
    }

    public FrmServicios(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        var resultado = await Aplicacion.Servicios.MostrarAsync(pagina, tamPagina, buscar, estado, ct: ct);
        MostrarPagina(resultado, ct);
    }

    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroServicio(Aplicacion);
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmRegistroServicio(Aplicacion, id);
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<ServicioEntity>()?.IdServicio;

    protected override async Task DesactivarAsync()
    {
        var entidad = ObtenerSeleccionado<ServicioEntity>();
        if (entidad is null || !ConfirmarCambioEstado(entidad.Estado)) return;
        await Aplicacion.Servicios.CambiarEstadoAsync(entidad.IdServicio, !entidad.Estado);
        await RecargarAsync();
    }

}
