using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Inventario;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Inventario;

public partial class FrmRegistroRepuesto : FrmRegistroBase
{
    private readonly int? _id;
    private bool EsEdicion => _id.HasValue;
    private RepuestoEntity? _entidad;
    protected override bool PermitirGuardar => true;

    public FrmRegistroRepuesto()
    {
        InitializeComponent();
        txtExistencias.ReadOnly = true;
        txtExistencias.Text = "0";
    }

    public FrmRegistroRepuesto(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    public FrmRegistroRepuesto(AplicacionBll aplicacion, int id) : this(aplicacion)
    {
        _id = Validacion.Id(id, "IdRepuesto");
        lblTitulo.Text = "Editar registro";
        btnGuardar.Text = "Guardar cambios";
    }

    protected override async Task CargarDatosAsync()
    {
        if (!_id.HasValue) return;
        _entidad = await Aplicacion.Repuestos.ObtenerPorIdAsync(_id.Value, TokenFormulario)
            ?? throw new ValidacionException("El registro ya no existe.");
        txtNombre.Text = _entidad.Nombre;
        chkActivo.Checked = _entidad.Estado;
        txtDescripcion.Text = _entidad.Descripcion;
        txtMarca.Text = _entidad.Marca;
        txtNumeroParte.Text = _entidad.NumeroParte;
        nudPrecioCompra.Value = _entidad.PrecioCompra;
        nudPrecioVenta.Value = _entidad.PrecioVenta;
        txtExistencias.Text = _entidad.Stock.ToString();
    }

    protected override bool ValidarControles()
    {
        errorIcon.Clear();
        return Requerido(txtNombre, "Ingrese el nombre.");
    }

    protected override async Task GuardarAsync()
    {
        _entidad ??= new RepuestoEntity();
        _entidad.CambiarNombre(txtNombre.Text);
        _entidad.CambiarEstado(chkActivo.Checked);
        _entidad.CambiarDescripcion(txtDescripcion.Text);
        _entidad.CambiarMarca(txtMarca.Text);
        _entidad.CambiarNumeroParte(txtNumeroParte.Text);
        _entidad.CambiarPrecios(nudPrecioCompra.Value, nudPrecioVenta.Value);
        if (EsEdicion)
            await Aplicacion.Repuestos.ActualizarAsync(_entidad, TokenFormulario);
        else
            await Aplicacion.Repuestos.AgregarAsync(_entidad, TokenFormulario);
    }
}
