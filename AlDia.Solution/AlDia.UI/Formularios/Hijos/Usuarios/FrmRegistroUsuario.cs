using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;
using System.ComponentModel;

namespace AlDia.UI.Formularios.Hijos.Usuarios;

public partial class FrmRegistroUsuario : FrmRegistroBase
{
    private readonly int? _idUsuario;
    private bool EsEdicion => _idUsuario.HasValue;
    private UsuarioEntity? _usuario;
    private List<RolEntity> _roles = [];
    protected override bool PermitirGuardar => true;
    public UsuarioEntity? UsuarioGuardado { get; private set; }

    public FrmRegistroUsuario()
    {
        InitializeComponent();
        CrearBotonOjo(panelContrasena, txtContrasena);
        CrearBotonOjo(panelConfirmacion, txtConfirmacion);
    }

    public FrmRegistroUsuario(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    public FrmRegistroUsuario(AplicacionBll aplicacion, int idUsuario) : this(aplicacion)
    {
        _idUsuario = Validacion.Id(idUsuario, "IdUsuario");
        ModoEdicion = true;
    }

    [Category("Apariencia"), DefaultValue(false)]
    public bool ModoEdicion
    {
        get => _idUsuario.HasValue;
        set
        {
            lblTitulo.Text = value ? "Editar usuario" : "Nuevo usuario";
            lblContrasena.Text = value ? "Nueva contraseña (opcional)" : "Contraseña *";
            lblConfirmacion.Text = value ? "Confirmar nueva contraseña" : "Confirmar contraseña *";
            btnGuardar.Text = value ? "Guardar cambios" : "Guardar usuario";
        }
    }

    // Carga los roles activos para poder asignarlos al usuario.
    protected override async Task CargarDatosAsync()
    {
        _roles = (await Aplicacion.Roles.MostrarRolesActivosAsync(TokenFormulario)).ToList();
        cboRol.DataSource = _roles;
        cboRol.DisplayMember = nameof(RolEntity.Nombre);
        cboRol.SelectedIndex = -1;
        if (!_idUsuario.HasValue) return;
        _usuario = await Aplicacion.Usuarios.ObtenerPorIdAsync(_idUsuario.Value, TokenFormulario)
            ?? throw new ValidacionException("El usuario ya no existe.");
        txtNombre.Text = _usuario.Nombre;
        txtApellido.Text = _usuario.Apellido;
        txtCedula.Text = _usuario.Cedula;
        txtTelefono.Text = _usuario.Telefono;
        txtCorreo.Text = _usuario.Correo;
        txtUsuario.Text = _usuario.Usuario;
        chkActivo.Checked = _usuario.Estado;
        cboRol.SelectedItem = _roles.FirstOrDefault(x => x.IdRol == _usuario.IdRol);
    }

    protected override bool ValidarControles()
    {
        errorIcon.Clear();
        bool valido = Requerido(txtNombre, "Ingrese el nombre.")
            & Requerido(txtApellido, "Ingrese el apellido.")
            & Requerido(txtCedula, "Ingrese la cédula.")
            & Requerido(txtUsuario, "Ingrese el usuario.");
        if (cboRol.SelectedItem is not RolEntity)
        { errorIcon.SetError(cboRol, "Seleccione un rol activo."); valido = false; }
        if (!string.IsNullOrWhiteSpace(txtCorreo.Text)) valido &= ValidationHelper.EmailValido(txtCorreo, errorIcon);
        string clave = txtContrasena.Text;
        if (!_idUsuario.HasValue || clave.Length > 0)
        {
            if (string.IsNullOrWhiteSpace(clave) || clave.Length < 8 || clave.Length > 128)
            { errorIcon.SetError(txtContrasena, "Use entre 8 y 128 caracteres."); valido = false; }
        }
        if (txtConfirmacion.Text != clave)
        { errorIcon.SetError(txtConfirmacion, "Las contraseñas no coinciden."); valido = false; }
        return valido;
    }

    // Guarda el usuario junto con el rol elegido.
    protected override async Task GuardarAsync()
    {
        var rol = (RolEntity)cboRol.SelectedItem!;
        _usuario ??= new UsuarioEntity();
        _usuario.CambiarNombre(txtNombre.Text);
        _usuario.CambiarApellido(txtApellido.Text);
        _usuario.CambiarCedula(txtCedula.Text);
        _usuario.CambiarTelefono(txtTelefono.Text);
        _usuario.CambiarCorreo(txtCorreo.Text);
        _usuario.CambiarUsuario(txtUsuario.Text);
        // Guarda en el usuario el rol seleccionado.
        _usuario.CambiarRol(rol);
        _usuario.CambiarEstado(chkActivo.Checked);
        string? clave = txtContrasena.Text.Length == 0 ? null : txtContrasena.Text;
        if (EsEdicion)
            await Aplicacion.Usuarios.ActualizarAsync(_usuario, clave, TokenFormulario);
        else
            await Aplicacion.Usuarios.AgregarAsync(_usuario, clave!, TokenFormulario);
        UsuarioGuardado = _usuario;
    }

    private void CrearBotonOjo(Panel panel, TextBox texto)
    {
        texto.Width = Math.Max(100, panel.Width - 56);
        var boton = new FontAwesome.Sharp.IconButton
        {
            Anchor = AnchorStyles.Top | AnchorStyles.Right, FlatStyle = FlatStyle.Flat,
            BackColor = Color.White, IconChar = FontAwesome.Sharp.IconChar.Eye,
            IconColor = Color.FromArgb(103, 116, 137), IconSize = 18,
            Location = new Point(panel.Width - 40, 6), Size = new Size(32, 30),
            TabStop = false, AccessibleName = "Mostrar contraseña"
        };
        boton.FlatAppearance.BorderSize = 0;
        boton.Click += (_, _) =>
        {
            texto.UseSystemPasswordChar = !texto.UseSystemPasswordChar;
            boton.IconChar = texto.UseSystemPasswordChar ? FontAwesome.Sharp.IconChar.Eye : FontAwesome.Sharp.IconChar.EyeSlash;
            boton.AccessibleName = texto.UseSystemPasswordChar ? "Mostrar contraseña" : "Ocultar contraseña";
        };
        panel.Controls.Add(boton);
        boton.BringToFront();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        txtContrasena.Clear();
        txtConfirmacion.Clear();
        base.OnFormClosed(e);
    }
}
