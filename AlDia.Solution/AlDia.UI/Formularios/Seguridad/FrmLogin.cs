using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Seguridad;

public partial class FrmLogin : Form
{
    private readonly UsuarioBll? _usuarios;
    private readonly CancellationTokenSource _cancelacion = new();
    private bool _ocupado;
    private bool _puedeIngresar;
    private bool _reintentarConexion;
    private bool _recursosLiberados;

    // El constructor sin argumentos permite abrir el disenador de Visual Studio.
    public FrmLogin() => InitializeComponent();
    public FrmLogin(UsuarioBll usuarios) : this()
    {
        _usuarios = usuarios ?? throw new ArgumentNullException(nameof(usuarios));
    }

    protected override async void OnShown(EventArgs e)
    {
        base.OnShown(e);
        RedondearControlHelper.RedondearControl(this, 25);
        RedondearControlHelper.RedondearControl(panelContenido, 25);
        RedondearControlHelper.RedondearControl(panelUsuario, 15);
        RedondearControlHelper.RedondearControl(panelContrasena, 15);
        if (_usuarios is not null) await CargarEstadoAsync();
        if (_usuarios is null) EnfocarCampo(txtUsuario);
    }

    private async Task CargarEstadoAsync()
    {
        if (_ocupado || _usuarios is null) return;
        _puedeIngresar = false;
        _reintentarConexion = false;
        btnCrearAdministrador.Visible = false;
        CambiarOcupado(true);
        MostrarMensaje("Comprobando acceso al sistema…", false);
        try
        {
            var estado = await _usuarios.ConsultarInicializacionAsync(_cancelacion.Token);
            if (_cancelacion.IsCancellationRequested) return;
            _puedeIngresar = estado.HayUsuarios;
            btnCrearAdministrador.Visible = !estado.HayUsuarios;
            if (!estado.HayUsuarios)
                MostrarMensaje("Crea el primer administrador para comenzar.", false);
            else
                MostrarMensaje("Ingresa con tu cuenta de usuario del sistema.", false);
        }
        catch (OperationCanceledException) { }
        catch (Exception)
        {
            if (_cancelacion.IsCancellationRequested) return;
            MostrarMensaje("No se pudo conectar al sistema. Pulsa Reintentar para volver a comprobarlo.", true);
            _reintentarConexion = true;
        }
        finally
        {
            if (!_cancelacion.IsCancellationRequested)
            {
                CambiarOcupado(false);
                EnfocarCampo(txtUsuario);
            }
        }
    }

    private bool ValidarCampos()
    {
        errorIcon.Clear();
        bool usuarioValido = ValidationHelper.Requerido(txtUsuario, errorIcon, "Ingresa tu usuario.");
        // Se conserva la contrasena exacta, incluidos sus espacios.
        bool claveValida = !string.IsNullOrEmpty(txtContrasena.Text);
        if (!claveValida) errorIcon.SetError(txtContrasena, "Ingresa tu contraseña.");
        if (!usuarioValido) EnfocarCampo(txtUsuario);
        else if (!claveValida) EnfocarCampo(txtContrasena);
        return usuarioValido && claveValida;
    }

    private async Task IngresarAsync()
    {
        if (_ocupado || !_puedeIngresar || _usuarios is null || !ValidarCampos()) return;
        CambiarOcupado(true);
        MostrarMensaje("Validando tus credenciales…", false);
        try
        {
            await _usuarios.AutenticarAsync(txtUsuario.Text.Trim(), txtContrasena.Text, _cancelacion.Token);
            if (_cancelacion.IsCancellationRequested) return;
            txtContrasena.Clear();
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (OperationCanceledException) { }
        catch (ValidacionException)
        {
            if (_cancelacion.IsCancellationRequested) return;
            MostrarMensaje("Usuario o contraseña incorrectos.", true);
            txtContrasena.Clear();
        }
        catch (Exception)
        {
            if (_cancelacion.IsCancellationRequested) return;
            MostrarMensaje("No fue posible iniciar sesión. Revisa la conexión e inténtalo nuevamente.", true);
            txtContrasena.Clear();
        }
        finally
        {
            if (!_cancelacion.IsCancellationRequested)
            {
                CambiarOcupado(false);
                EnfocarCampo(txtContrasena);
            }
        }
    }

    private void CambiarOcupado(bool ocupado)
    {
        _ocupado = ocupado;
        UseWaitCursor = ocupado;
        // Los campos siguen editables aunque falte crear el primer usuario.
        // La disponibilidad del acceso se controla en el boton y en la BLL.
        txtUsuario.Enabled = txtContrasena.Enabled = btnVerContrasena.Enabled = !ocupado;
        btnIngresar.Enabled = !ocupado && (_puedeIngresar || _reintentarConexion);
        btnCrearAdministrador.Enabled = !ocupado;
        btnIngresar.Text = ocupado ? "Espera…" : _reintentarConexion ? "Reintentar" : "Ingresar";
    }

    private void MostrarMensaje(string texto, bool esError)
    {
        lblMensaje.Text = texto;
        lblMensaje.ForeColor = esError ? Color.FromArgb(220, 53, 69) : Color.FromArgb(84, 94, 110);
    }

    private async void btnIngresar_Click(object? sender, EventArgs e)
    {
        if (_reintentarConexion) await CargarEstadoAsync();
        else await IngresarAsync();
    }
    private void btnVerContrasena_Click(object? sender, EventArgs e)
    {
        int inicio = txtContrasena.SelectionStart;
        int longitud = txtContrasena.SelectionLength;
        bool mostrar = txtContrasena.PasswordChar != '\0';
        txtContrasena.PasswordChar = mostrar ? '\0' : '*';
        btnVerContrasena.IconChar = mostrar
            ? FontAwesome.Sharp.IconChar.EyeSlash : FontAwesome.Sharp.IconChar.Eye;
        string accion = mostrar ? "Ocultar contraseña" : "Mostrar contraseña";
        btnVerContrasena.AccessibleName = accion;
        ayudaControles.SetToolTip(btnVerContrasena, accion);
        EnfocarCampo(txtContrasena);
        txtContrasena.Select(inicio, longitud);
    }

    private void EnfocarCampo(TextBox campo)
    {
        if (_recursosLiberados || _cancelacion.IsCancellationRequested || !campo.Enabled) return;
        ActiveControl = campo;
        campo.Focus();
    }

    private void panelUsuario_MouseClick(object? sender, MouseEventArgs e) => EnfocarCampo(txtUsuario);
    private void panelContrasena_MouseClick(object? sender, MouseEventArgs e) => EnfocarCampo(txtContrasena);

    private async void btnCrearAdministrador_Click(object? sender, EventArgs e)
    {
        if (_ocupado || _usuarios is null) return;
        using var formulario = new FrmAdministradorInicial(_usuarios);
        bool creado = formulario.ShowDialog(this) == DialogResult.OK;
        await CargarEstadoAsync();
        if (creado && !_cancelacion.IsCancellationRequested)
        {
            txtUsuario.Text = formulario.UsuarioCreado;
            MostrarMensaje("Administrador creado. Inicia sesión con tu nueva cuenta.", false);
            EnfocarCampo(txtContrasena);
        }
    }

    private void btnSalir_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (e.Cancel) return;
        _cancelacion.Cancel();
        if (DialogResult != DialogResult.OK) _usuarios?.CerrarSesion();
        txtContrasena.Clear();
    }

    private void panelContenido_Paint(object sender, PaintEventArgs e)
    {

    }
}
