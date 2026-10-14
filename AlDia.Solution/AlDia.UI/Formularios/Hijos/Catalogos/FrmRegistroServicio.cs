using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Reparaciones;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Catalogos;

public partial class FrmRegistroServicio : FrmRegistroBase
{
    private readonly int? _id;
    private bool EsEdicion => _id.HasValue;
    private ServicioEntity? _entidad;
    protected override bool PermitirGuardar => true;

    public FrmRegistroServicio()
    {
        InitializeComponent();
    }

    public FrmRegistroServicio(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    public FrmRegistroServicio(AplicacionBll aplicacion, int id) : this(aplicacion)
    {
        _id = Validacion.Id(id, "IdServicio");
        lblTitulo.Text = "Editar registro";
        btnGuardar.Text = "Guardar cambios";
    }

    protected override async Task CargarDatosAsync()
    {
        if (!_id.HasValue) return;
        _entidad = await Aplicacion.Servicios.ObtenerPorIdAsync(_id.Value, TokenFormulario)
            ?? throw new ValidacionException("El registro ya no existe.");
        txtNombre.Text = _entidad.Nombre;
        chkActivo.Checked = _entidad.Estado;
        txtDescripcion.Text = _entidad.Descripcion;
        nudPrecioBase.Value = _entidad.PrecioBase;
    }

    protected override bool ValidarControles()
    {
        errorIcon.Clear();
        return Requerido(txtNombre, "Ingrese el nombre.");
    }

    protected override async Task GuardarAsync()
    {
        _entidad ??= new ServicioEntity();
        _entidad.CambiarNombre(txtNombre.Text);
        _entidad.CambiarEstado(chkActivo.Checked);
        _entidad.CambiarDescripcion(txtDescripcion.Text);
        _entidad.CambiarPrecioBase(nudPrecioBase.Value);
        if (EsEdicion)
            await Aplicacion.Servicios.ActualizarAsync(_entidad, TokenFormulario);
        else
            await Aplicacion.Servicios.AgregarAsync(_entidad, TokenFormulario);
    }
}
