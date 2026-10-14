using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Clientes;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Clientes;

public partial class FrmRegistroCliente : FrmRegistroBase
{
    private readonly int? _id;
    private bool EsEdicion => _id.HasValue;
    private ClienteEntity? _entidad;
    protected override bool PermitirGuardar => true;

    public FrmRegistroCliente()
    {
        InitializeComponent();
    }

    public FrmRegistroCliente(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    public FrmRegistroCliente(AplicacionBll aplicacion, int id) : this(aplicacion)
    {
        _id = Validacion.Id(id, "IdCliente");
        lblTitulo.Text = "Editar registro";
        btnGuardar.Text = "Guardar cambios";
    }

    protected override async Task CargarDatosAsync()
    {
        if (!_id.HasValue) return;
        _entidad = await Aplicacion.Clientes.ObtenerPorIdAsync(_id.Value, TokenFormulario)
            ?? throw new ValidacionException("El registro ya no existe.");
        txtNombre.Text = _entidad.Nombre;
        chkActivo.Checked = _entidad.Estado;
        txtApellido.Text = _entidad.Apellido;
        txtCedula.Text = _entidad.Cedula;
        txtTelefono.Text = _entidad.Telefono;
    }

    protected override bool ValidarControles()
    {
        errorIcon.Clear();
        return Requerido(txtNombre, "Ingrese el nombre.") & Requerido(txtApellido,
            "Ingrese el apellido.") & Requerido(txtCedula, "Ingrese la cédula.") & Requerido(txtTelefono,
            "Ingrese el teléfono.");
    }

    protected override async Task GuardarAsync()
    {
        _entidad ??= new ClienteEntity();
        _entidad.CambiarNombre(txtNombre.Text);
        _entidad.CambiarEstado(chkActivo.Checked);
        _entidad.CambiarApellido(txtApellido.Text);
        _entidad.CambiarCedula(txtCedula.Text);
        _entidad.CambiarTelefono(txtTelefono.Text);
        if (EsEdicion)
            await Aplicacion.Clientes.ActualizarAsync(_entidad, TokenFormulario);
        else
            await Aplicacion.Clientes.AgregarAsync(_entidad, TokenFormulario);
    }
}
