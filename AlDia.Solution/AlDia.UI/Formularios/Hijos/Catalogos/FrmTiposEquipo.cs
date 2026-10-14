using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Equipos;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Catalogos;

public partial class FrmTiposEquipo : FrmListadoBase
{
    public FrmTiposEquipo()
    {
        InitializeComponent();
        VincularColumna("colNombre", nameof(TipoEquipoEntity.Nombre));
        VincularColumna("colDescripcion", nameof(TipoEquipoEntity.Descripcion));
        VincularColumna("colEstado", nameof(TipoEquipoEntity.EstadoNombre));
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
    }

    public FrmTiposEquipo(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        var resultado = await Aplicacion.TiposEquipo.MostrarAsync(pagina, tamPagina, buscar, estado, ct: ct);
        MostrarPagina(resultado, ct);
    }

    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroTipoEquipo(Aplicacion);
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmRegistroTipoEquipo(Aplicacion, id);
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<TipoEquipoEntity>()?.IdTipoEquipo;

    protected override async Task DesactivarAsync()
    {
        var entidad = ObtenerSeleccionado<TipoEquipoEntity>();
        if (entidad is null || !ConfirmarCambioEstado(entidad.Estado)) return;
        await Aplicacion.TiposEquipo.CambiarEstadoAsync(entidad.IdTipoEquipo, !entidad.Estado);
        await RecargarAsync();
    }

}
