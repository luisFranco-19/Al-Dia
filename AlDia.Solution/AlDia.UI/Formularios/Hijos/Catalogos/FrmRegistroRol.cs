using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Catalogos;

public partial class FrmRegistroRol : FrmRegistroBase
{
    private readonly int? _id;
    private bool EsEdicion => _id.HasValue;
    private RolEntity? _entidad;
    protected override bool PermitirGuardar => true;

    public FrmRegistroRol()
    {
        InitializeComponent();
    }

    public FrmRegistroRol(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    public FrmRegistroRol(AplicacionBll aplicacion, int id) : this(aplicacion)
    {
        _id = Validacion.Id(id, "IdRol");
        lblTitulo.Text = "Editar registro";
        btnGuardar.Text = "Guardar cambios";
    }

    protected override async Task CargarDatosAsync()
    {
        if (!_id.HasValue) return;
        _entidad = await Aplicacion.Roles.ObtenerPorIdAsync(_id.Value, TokenFormulario)
            ?? throw new ValidacionException("El registro ya no existe.");
        txtNombre.Text = _entidad.Nombre;
        chkActivo.Checked = _entidad.Estado;
        txtDescripcion.Text = _entidad.Descripcion;
    }

    protected override bool ValidarControles()
    {
        errorIcon.Clear();
        return Requerido(txtNombre, "Ingrese el nombre.");
    }

    protected override async Task GuardarAsync()
    {
        _entidad ??= new RolEntity();
        _entidad.CambiarNombre(txtNombre.Text);
        _entidad.CambiarEstado(chkActivo.Checked);
        _entidad.CambiarDescripcion(txtDescripcion.Text);
        if (EsEdicion)
            await Aplicacion.Roles.ActualizarAsync(_entidad, TokenFormulario);
        else
            await Aplicacion.Roles.AgregarAsync(_entidad, TokenFormulario);
    }
}
