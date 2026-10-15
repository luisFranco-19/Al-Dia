using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Clientes;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Clientes;

public partial class FrmRegistroCliente : FrmRegistroBase // Formulario para crear o editar clientes.
{
    private readonly int? _id; // Guarda el ID cuando se está editando.
    private bool EsEdicion => _id.HasValue; // Indica si el formulario abrió un cliente existente.
    private ClienteEntity? _entidad; // Guarda los datos del cliente actual.
    protected override bool PermitirGuardar => true; // Este formulario permite guardar.

    public FrmRegistroCliente() // Inicializa los controles del formulario.
    {
        InitializeComponent();
    }

    public FrmRegistroCliente(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion); // Configura los servicios recibidos.
    public FrmRegistroCliente(AplicacionBll aplicacion, int id) : this(aplicacion) // Prepara el formulario para editar el ID indicado.
    {
        _id = Validacion.Id(id, "IdCliente");
        lblTitulo.Text = "Editar registro"; // Indica que se está editando un registro.
        btnGuardar.Text = "Guardar cambios"; // Cambia el texto del botón para editar.
    }

    protected override async Task CargarDatosAsync() // Carga los datos cuando el formulario edita un cliente.
    {
        if (!_id.HasValue) return; // Si no hay ID, se trata de un cliente nuevo.
        _entidad = await Aplicacion.Clientes.ObtenerPorIdAsync(_id.Value, TokenFormulario) // Busca el cliente y permite cancelar la consulta.
            ?? throw new ValidacionException("El registro ya no existe.");
        txtNombre.Text = _entidad.Nombre; // Muestra el nombre recuperado.
        chkActivo.Checked = _entidad.Estado; // Muestra si el cliente está activo.
        txtApellido.Text = _entidad.Apellido; // Muestra el apellido recuperado.
        txtCedula.Text = _entidad.Cedula; // Muestra la cédula recuperada.
        txtTelefono.Text = _entidad.Telefono; // Muestra el teléfono recuperado.
    }

    protected override bool ValidarControles() // Comprueba que se hayan ingresado los datos obligatorios.
    {
        errorIcon.Clear(); // Limpia los mensajes de validación anteriores.
        return Requerido(txtNombre, "Ingrese el nombre.") & Requerido(txtApellido, // Verifica que el nombre no esté vacío.
            "Ingrese el apellido.") & Requerido(txtCedula, "Ingrese la cédula.") & Requerido(txtTelefono, // Verifica que la cédula no esté vacía.
            "Ingrese el teléfono.");
    }

    protected override async Task GuardarAsync() // Copia los campos a la entidad y la guarda.
    {
        _entidad ??= new ClienteEntity(); // Crea la entidad solo si aún no existe.
        _entidad.CambiarNombre(txtNombre.Text); // Muestra el nombre recuperado.
        _entidad.CambiarEstado(chkActivo.Checked); // Muestra si el cliente está activo.
        _entidad.CambiarApellido(txtApellido.Text); // Muestra el apellido recuperado.
        _entidad.CambiarCedula(txtCedula.Text); // Muestra la cédula recuperada.
        _entidad.CambiarTelefono(txtTelefono.Text); // Muestra el teléfono recuperado.
        if (EsEdicion) // Decide si actualiza o agrega el cliente.
            await Aplicacion.Clientes.ActualizarAsync(_entidad, TokenFormulario); // Actualiza el cliente existente.
        else
            await Aplicacion.Clientes.AgregarAsync(_entidad, TokenFormulario); // Guarda el cliente nuevo.
    }
}
