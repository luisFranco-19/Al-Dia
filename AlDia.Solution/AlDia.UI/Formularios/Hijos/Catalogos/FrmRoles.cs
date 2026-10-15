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
    // Constructor que recibe la instancia de AplicacionBll para configurar los servicios.
    public FrmRoles(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    // Propiedades que determinan la visibilidad de los botones según el rol del usuario.
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador);
    // Propiedades que determinan la visibilidad de los botones según el rol del usuario.
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador);

    // Carga y muestra los roles según los filtros.
    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        var resultado = await Aplicacion.Roles.MostrarAsync(pagina, tamPagina, buscar, estado, ct: ct);
        MostrarPagina(resultado, ct);
    }

    //
    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroRol(Aplicacion);
    //
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmRegistroRol(Aplicacion, id);
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<RolEntity>()?.IdRol;

    // Cambia el estado del rol seleccionado.
    protected override async Task DesactivarAsync()
    {
        var entidad = ObtenerSeleccionado<RolEntity>();
        if (entidad is null || !ConfirmarCambioEstado(entidad.Estado)) return;
        await Aplicacion.Roles.CambiarEstadoAsync(entidad.IdRol, !entidad.Estado);
        await RecargarAsync();
    }

}
