using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Equipos;
using AlDia.Entity.Reparaciones;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.OrdenesServicio;

public partial class FrmRegistroOrdenServicio : FrmRegistroBase
{
    private List<EquipoEntity> _equipos = [];
    private EstadoReparacionEntity? _estado;
    protected override bool PermitirGuardar => true;
    public FrmRegistroOrdenServicio()
    {
        InitializeComponent();
        cboCliente.SelectedIndexChanged += (_, _) => CargarEquipos();
        dtpFechaRecepcion.Enabled = false;
        cboRecepcionista.Enabled = false;
        txtEstadoInicial.ReadOnly = true;
    }
    public FrmRegistroOrdenServicio(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override async Task CargarDatosAsync()
    {
        var clientes = await Aplicacion.Clientes.ConsultarAsync(ct: TokenFormulario);
        _equipos = (await Aplicacion.Equipos.ConsultarAsync(ct: TokenFormulario)).ToList();
        _estado = (await Aplicacion.Ordenes.ConsultarEstadosAsync(TokenFormulario)).Single(x => x.Nombre == "En Revisión");
        cboCliente.DataSource = clientes.Select(x => new OpcionCombo(x.IdCliente, x.NombreCompleto)).ToList();
        cboCliente.SelectedIndex = -1;
        var actor = Aplicacion.Sesion.UsuarioActual!;
        cboRecepcionista.DataSource = new[] { new OpcionCombo(actor.IdUsuario, actor.NombreCompleto) };
        txtEstadoInicial.Text = _estado.Nombre;
        dtpFechaRecepcion.Value = DateTime.Now;
    }
    private void CargarEquipos()
    {
        int? cliente = (cboCliente.SelectedItem as OpcionCombo)?.Id;
        cboEquipo.DataSource = _equipos.Where(x => x.IdCliente == cliente)
            .Select(x => new OpcionCombo(x.IdEquipo, $"{x.Marca} {x.Modelo} · {x.NumeroSerie}".Trim())).ToList();
        cboEquipo.SelectedIndex = -1;
    }
    protected override bool ValidarControles()
    {
        errorIcon.Clear();
        bool valido = Requerido(txtNumeroOrden, "Ingrese el número de orden.") & Requerido(txtProblemaReportado, "Describa el problema.");
        if (cboEquipo.SelectedItem is not OpcionCombo { Id: not null })
        { errorIcon.SetError(cboEquipo, "Seleccione el equipo del cliente."); valido = false; }
        return valido;
    }
    protected override async Task GuardarAsync()
    {
        OrdenReparacionEntity orden = new();
        orden.CambiarNumeroOrden(txtNumeroOrden.Text);
        orden.CambiarEquipo(((OpcionCombo)cboEquipo.SelectedItem!).Id!.Value);
        orden.CambiarRecepcionista(Aplicacion.Sesion.UsuarioActual!.IdUsuario);
        orden.CambiarEstadoInicial(_estado!);
        orden.CambiarRecepcion(txtProblemaReportado.Text, txtObservacionesRecepcion.Text, txtAccesoriosRecepcion.Text);
        await Aplicacion.Ordenes.RegistrarEntidadAsync(orden, TokenFormulario);
    }
}
