using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Catalogos;

public partial class FrmRoles : FrmListadoBase
{
    public FrmRoles()
    {
        InitializeComponent();
        VincularColumna("colNombre", nameof(RolEntity.Nombre));
        VincularColumna("colDescripcion", nameof(RolEntity.Descripcion));
        VincularColumna("colEstado", nameof(RolEntity.EstadoNombre));
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
    }

    public FrmRoles(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        var resultado = await Aplicacion.Roles.MostrarAsync(pagina, tamPagina, buscar, estado, ct: ct);
        MostrarPagina(resultado, ct);
    }

    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroRol(Aplicacion);
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmRegistroRol(Aplicacion, id);
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<RolEntity>()?.IdRol;

    protected override async Task DesactivarAsync()
    {
        var entidad = ObtenerSeleccionado<RolEntity>();
        if (entidad is null || !ConfirmarCambioEstado(entidad.Estado)) return;
        await Aplicacion.Roles.CambiarEstadoAsync(entidad.IdRol, !entidad.Estado);
        await RecargarAsync();
    }

}
