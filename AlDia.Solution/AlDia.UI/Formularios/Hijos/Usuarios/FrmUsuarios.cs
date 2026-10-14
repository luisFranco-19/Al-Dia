using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Usuarios;

public partial class FrmUsuarios : FrmListadoBase
{
    private bool _preparandoFiltros;
    public FrmUsuarios()
    {
        InitializeComponent();
        VincularColumna("colNombre", nameof(UsuarioEntity.NombreCompleto));
        VincularColumna("colUsuario", nameof(UsuarioEntity.Usuario));
        VincularColumna("colCorreo", nameof(UsuarioEntity.Correo));
        VincularColumna("colCedula", nameof(UsuarioEntity.Cedula));
        VincularColumna("colTelefono", nameof(UsuarioEntity.Telefono));
        VincularColumna("colRol", nameof(UsuarioEntity.NombreRol));
        VincularColumna("colEstado", nameof(UsuarioEntity.EstadoNombre));
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
        cboFiltroRol.SelectedIndexChanged += async (_, _) => { if (!_preparandoFiltros) await RecargarAsync(1); };
    }

    public FrmUsuarios(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        if (cboFiltroRol.SelectedItem is not OpcionCombo)
        {
            var roles = await Aplicacion.Roles.ConsultarAsync(soloActivos: false, ct: ct);
            ct.ThrowIfCancellationRequested();
            _preparandoFiltros = true;
            cboFiltroRol.Items.Clear();
            cboFiltroRol.Items.Add(new OpcionCombo(null, "Todos los roles"));
            foreach (var rol in roles) cboFiltroRol.Items.Add(new OpcionCombo(rol.IdRol, rol.Nombre));
            cboFiltroRol.SelectedIndex = 0;
            _preparandoFiltros = false;
        }
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        var resultado = await Aplicacion.Usuarios.MostrarAsync(pagina, tamPagina, buscar, estado,
            nombreRol: (cboFiltroRol.SelectedItem as OpcionCombo) is { Id: not null } opcion ? opcion.Nombre : null, ct: ct);
        MostrarPagina(resultado, ct);
    }

    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroUsuario(Aplicacion);
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmRegistroUsuario(Aplicacion, id);
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<UsuarioEntity>()?.IdUsuario;

    protected override async Task DesactivarAsync()
    {
        var entidad = ObtenerSeleccionado<UsuarioEntity>();
        if (entidad is null || !ConfirmarCambioEstado(entidad.Estado)) return;
        await Aplicacion.Usuarios.CambiarEstadoAsync(entidad.IdUsuario, !entidad.Estado);
        await RecargarAsync();
    }

}
