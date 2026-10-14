using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Clientes;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Clientes;

public partial class FrmClientes : FrmListadoBase
{
    public FrmClientes()
    {
        InitializeComponent();
        VincularColumna("colIdCliente", nameof(ClienteEntity.IdCliente));
        VincularColumna("colNombre", nameof(ClienteEntity.Nombre));
        VincularColumna("colApellido", nameof(ClienteEntity.Apellido));
        VincularColumna("colCedula", nameof(ClienteEntity.Cedula));
        VincularColumna("colTelefono", nameof(ClienteEntity.Telefono));
        VincularColumna("colEstado", nameof(ClienteEntity.EstadoNombre));
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
    }

    public FrmClientes(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador, RolUsuario.Recepcionista);
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador, RolUsuario.Recepcionista);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador, RolUsuario.Recepcionista);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        var resultado = await Aplicacion.Clientes.MostrarAsync(pagina, tamPagina, buscar, estado, ct: ct);
        MostrarPagina(resultado, ct);
    }

    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroCliente(Aplicacion);
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmRegistroCliente(Aplicacion, id);
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<ClienteEntity>()?.IdCliente;

    protected override async Task DesactivarAsync()
    {
        var entidad = ObtenerSeleccionado<ClienteEntity>();
        if (entidad is null || !ConfirmarCambioEstado(entidad.Estado)) return;
        await Aplicacion.Clientes.CambiarEstadoAsync(entidad.IdCliente, !entidad.Estado);
        await RecargarAsync();
    }

}
