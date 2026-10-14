using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Equipos;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Equipos;

public partial class FrmRegistroEquipo : FrmRegistroBase
{
    private readonly int? _id;
    private bool EsEdicion => _id.HasValue;
    private EquipoEntity? _equipo;
    protected override bool PermitirGuardar => true;
    public FrmRegistroEquipo() => InitializeComponent();
    public FrmRegistroEquipo(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    public FrmRegistroEquipo(AplicacionBll aplicacion, int id) : this(aplicacion)
    {
        _id = Validacion.Id(id, "IdEquipo");
        lblTitulo.Text = "Editar equipo";
        btnGuardar.Text = "Guardar cambios";
    }

    protected override async Task CargarDatosAsync()
    {
        var clientes = await Aplicacion.Clientes.ConsultarAsync(soloActivos: false, ct: TokenFormulario);
        var tipos = await Aplicacion.TiposEquipo.ConsultarAsync(soloActivos: false, ct: TokenFormulario);
        cboCliente.DataSource = clientes.Select(x => new OpcionCombo(x.IdCliente, x.NombreCompleto)).ToList();
        cboTipoEquipo.DataSource = tipos.Select(x => new OpcionCombo(x.IdTipoEquipo, x.Nombre)).ToList();
        cboCliente.SelectedIndex = cboTipoEquipo.SelectedIndex = -1;
        if (!_id.HasValue) return;
        _equipo = await Aplicacion.Equipos.ObtenerPorIdAsync(_id.Value, TokenFormulario)
            ?? throw new ValidacionException("El equipo ya no existe.");
        cboCliente.SelectedItem = ((List<OpcionCombo>)cboCliente.DataSource).FirstOrDefault(x => x.Id == _equipo.IdCliente);
        cboTipoEquipo.SelectedItem = ((List<OpcionCombo>)cboTipoEquipo.DataSource).FirstOrDefault(x => x.Id == _equipo.IdTipoEquipo);
        txtMarca.Text = _equipo.Marca;
        txtModelo.Text = _equipo.Modelo;
        txtNumeroSerie.Text = _equipo.NumeroSerie;
        txtColor.Text = _equipo.Color;
        txtObservaciones.Text = _equipo.Observaciones;
        chkActivo.Checked = _equipo.Estado;
    }

    protected override bool ValidarControles()
    {
        errorIcon.Clear();
        bool valido = Requerido(txtMarca, "Ingrese la marca.");
        if (cboCliente.SelectedItem is not OpcionCombo { Id: not null })
        { errorIcon.SetError(cboCliente, "Seleccione un cliente."); valido = false; }
        if (cboTipoEquipo.SelectedItem is not OpcionCombo { Id: not null })
        { errorIcon.SetError(cboTipoEquipo, "Seleccione un tipo de equipo."); valido = false; }
        return valido;
    }

    protected override async Task GuardarAsync()
    {
        int cliente = ((OpcionCombo)cboCliente.SelectedItem!).Id!.Value;
        int tipo = ((OpcionCombo)cboTipoEquipo.SelectedItem!).Id!.Value;
        _equipo ??= new EquipoEntity();
        _equipo.CambiarCliente(cliente);
        _equipo.CambiarTipoEquipo(tipo);
        _equipo.CambiarMarca(txtMarca.Text);
        _equipo.CambiarModelo(txtModelo.Text);
        _equipo.CambiarNumeroSerie(txtNumeroSerie.Text);
        _equipo.CambiarColor(txtColor.Text);
        _equipo.CambiarObservaciones(txtObservaciones.Text);
        _equipo.CambiarEstado(chkActivo.Checked);
        if (EsEdicion)
            await Aplicacion.Equipos.ActualizarAsync(_equipo, TokenFormulario);
        else
            await Aplicacion.Equipos.AgregarAsync(_equipo, TokenFormulario);
    }
}
