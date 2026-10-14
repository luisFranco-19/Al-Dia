using AlDia.BLL.Common;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Catalogos;

public partial class FrmCatalogos : FrmListadoBase
{
    public FrmCatalogos()
    {
        InitializeComponent();
        VincularColumna("colCatalogo", nameof(CatalogoListado.Catalogo));
        VincularColumna("colNombre", nameof(CatalogoListado.Nombre));
        VincularColumna("colDescripcion", nameof(CatalogoListado.Descripcion));
        VincularColumna("colPrecio", nameof(CatalogoListado.Precio));
        VincularColumna("colEstado", nameof(CatalogoListado.EstadoNombre));
        cboFiltroCatalogo.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
    }
    public FrmCatalogos(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador) && cboFiltroCatalogo?.SelectedIndex > 0;
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        TipoCatalogo? tipo = cboFiltroCatalogo.SelectedIndex > 0 ? (TipoCatalogo)cboFiltroCatalogo.SelectedIndex : null;
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        MostrarPagina(await Aplicacion.Catalogos.MostrarAsync(pagina, tamPagina, buscar, tipo, estado, ct), ct);
    }

    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<CatalogoListado>()?.Id;
    protected override Form? CrearFormularioAccionPrincipal() => CrearRegistro((TipoCatalogo)cboFiltroCatalogo.SelectedIndex, null);
    protected override Form? CrearFormularioAccionSecundaria(int id) =>
        ObtenerSeleccionado<CatalogoListado>() is { } registro ? CrearRegistro(registro.Tipo, id) : null;
    private Form? CrearRegistro(TipoCatalogo tipo, int? id) => tipo switch
    {
        TipoCatalogo.Rol => id.HasValue ? new FrmRegistroRol(Aplicacion, id.Value) : new FrmRegistroRol(Aplicacion),
        TipoCatalogo.TipoEquipo => id.HasValue ? new FrmRegistroTipoEquipo(Aplicacion, id.Value) : new FrmRegistroTipoEquipo(Aplicacion),
        TipoCatalogo.Servicio => id.HasValue ? new FrmRegistroServicio(Aplicacion, id.Value) : new FrmRegistroServicio(Aplicacion),
        _ => null
    };
    protected override async Task DesactivarAsync()
    {
        var registro = ObtenerSeleccionado<CatalogoListado>();
        if (registro is null || !ConfirmarCambioEstado(registro.Estado)) return;
        await Aplicacion.Catalogos.CambiarEstadoAsync(registro, !registro.Estado);
        await RecargarAsync();
    }
}
