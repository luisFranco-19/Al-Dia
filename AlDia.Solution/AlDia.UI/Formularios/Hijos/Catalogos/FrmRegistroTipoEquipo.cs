using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Equipos;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Catalogos;

public partial class FrmRegistroTipoEquipo : FrmRegistroBase
{
    private readonly int? _id;
    private bool EsEdicion => _id.HasValue;
    private TipoEquipoEntity? _entidad;
    protected override bool PermitirGuardar => true;

    public FrmRegistroTipoEquipo()
    {
        InitializeComponent();
    }

    public FrmRegistroTipoEquipo(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    public FrmRegistroTipoEquipo(AplicacionBll aplicacion, int id) : this(aplicacion)
    {
        _id = Validacion.Id(id, "IdTipoEquipo");
        lblTitulo.Text = "Editar registro";
        btnGuardar.Text = "Guardar cambios";
    }

    protected override async Task CargarDatosAsync()
    {
        if (!_id.HasValue) return;
        _entidad = await Aplicacion.TiposEquipo.ObtenerPorIdAsync(_id.Value, TokenFormulario)
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
        _entidad ??= new TipoEquipoEntity();
        _entidad.CambiarNombre(txtNombre.Text);
        _entidad.CambiarEstado(chkActivo.Checked);
        _entidad.CambiarDescripcion(txtDescripcion.Text);
        if (EsEdicion)
            await Aplicacion.TiposEquipo.ActualizarAsync(_entidad, TokenFormulario);
        else
            await Aplicacion.TiposEquipo.AgregarAsync(_entidad, TokenFormulario);
    }
}
