using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Seguridad;

public partial class FrmAdministradorInicial : Form
{
    private readonly UsuarioBll? _usuarios;
    private readonly CancellationTokenSource _cancelacion = new();
    private bool _ocupado;
    private bool _recursosLiberados;
    public string UsuarioCreado { get; private set; } = string.Empty;

    public FrmAdministradorInicial() => InitializeComponent();
    public FrmAdministradorInicial(UsuarioBll usuarios) : this() =>
        _usuarios = usuarios ?? throw new ArgumentNullException(nameof(usuarios));

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        RedondearControlHelper.RedondearControl(this, 25);
        RedondearControlHelper.RedondearControl(panelContenido, 25);
        foreach (var panel in new[] { panelNombre, panelApellido, panelCedula, panelUsuario, panelContrasena, panelConfirmacion })
            RedondearControlHelper.RedondearControl(panel, 15);
    }

    private bool ValidarCampos()
    {
        errorIcon.Clear();
        bool valido = true;
        foreach (var campo in new[] { txtNombre, txtApellido, txtCedula, txtUsuario })
        {
            if (!ValidationHelper.Requerido(campo, errorIcon, "Completa este campo.")) valido = false;
        }
        if (string.IsNullOrWhiteSpace(txtContrasena.Text) || txtContrasena.Text.Length < 8)
        {
            errorIcon.SetError(txtContrasena, "Usa una contraseña de al menos 8 caracteres.");
            valido = false;
        }
        if (txtConfirmacion.Text != txtContrasena.Text)
        {
            errorIcon.SetError(txtConfirmacion, "Las contraseñas deben coincidir.");
            valido = false;
        }
        return valido;
    }

    private async Task CrearAdministradorAsync()
    {
        if (_ocupado || _usuarios is null || !ValidarCampos()) return;
        CambiarOcupado(true);
        lblMensaje.ForeColor = Color.DarkGray;
        lblMensaje.Text = "Creando tu cuenta…";
        try
        {
            var solicitud = new UsuarioSolicitud(txtNombre.Text, txtApellido.Text, txtCedula.Text,
                txtUsuario.Text, RolUsuario.Administrador);
            await _usuarios.CrearAdministradorInicialAsync(solicitud, txtContrasena.Text, _cancelacion.Token);
            if (_cancelacion.IsCancellationRequested) return;
            UsuarioCreado = txtUsuario.Text.Trim();
            LimpiarContrasenas();
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (OperationCanceledException) { }
        catch (ValidacionException ex)
        {
            if (!_cancelacion.IsCancellationRequested) MostrarError(ex.Message);
        }
        catch (DatosException ex) when (ex.EsErrorDeNegocio)
        {
            if (!_cancelacion.IsCancellationRequested) MostrarError(ex.Message);
        }
        catch (Exception)
        {
            if (!_cancelacion.IsCancellationRequested)
                MostrarError("No fue posible crear la cuenta. Revisa la conexión e inténtalo nuevamente.");
        }
        finally
        {
            if (!_cancelacion.IsCancellationRequested) CambiarOcupado(false);
        }
    }

    private void CambiarOcupado(bool ocupado)
    {
        _ocupado = ocupado;
        UseWaitCursor = ocupado;
        foreach (var campo in new[] { txtNombre, txtApellido, txtCedula, txtUsuario, txtContrasena, txtConfirmacion })
            campo.Enabled = !ocupado;
        btnCrear.Enabled = btnVerContrasena.Enabled = btnVerConfirmacion.Enabled = !ocupado;
        btnCrear.Text = ocupado ? "Creando…" : "Crear cuenta";
    }
    private void MostrarError(string mensaje)
    {
        lblMensaje.Text = mensaje;
        lblMensaje.ForeColor = Color.FromArgb(220, 53, 69);
    }
    private void LimpiarContrasenas()
    {
        txtContrasena.Clear();
        txtConfirmacion.Clear();
    }
    private async void btnCrear_Click(object? sender, EventArgs e) => await CrearAdministradorAsync();
    private void btnVerContrasena_Click(object? sender, EventArgs e) =>
        AlternarContrasena(txtContrasena, btnVerContrasena, "contraseña");

    private void btnVerConfirmacion_Click(object? sender, EventArgs e) =>
        AlternarContrasena(txtConfirmacion, btnVerConfirmacion, "confirmación de contraseña");

    private void AlternarContrasena(TextBox campo, FontAwesome.Sharp.IconButton boton, string nombreCampo)
    {
        int inicio = campo.SelectionStart;
        int longitud = campo.SelectionLength;
        bool mostrar = campo.PasswordChar != '\0';
        campo.PasswordChar = mostrar ? '\0' : '*';
        boton.IconChar = mostrar
            ? FontAwesome.Sharp.IconChar.EyeSlash : FontAwesome.Sharp.IconChar.Eye;
        string accion = $"{(mostrar ? "Ocultar" : "Mostrar")} {nombreCampo}";
        boton.AccessibleName = accion;
        ayudaControles.SetToolTip(boton, accion);
        EnfocarCampo(campo);
        campo.Select(inicio, longitud);
    }

    private void EnfocarCampo(TextBox campo)
    {
        if (_recursosLiberados || _cancelacion.IsCancellationRequested || !campo.Enabled) return;
        ActiveControl = campo;
        campo.Focus();
    }

    private void panelContrasena_MouseClick(object? sender, MouseEventArgs e) => EnfocarCampo(txtContrasena);
    private void panelConfirmacion_MouseClick(object? sender, MouseEventArgs e) => EnfocarCampo(txtConfirmacion);
    private void btnCancelar_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (e.Cancel) return;
        _cancelacion.Cancel();
        LimpiarContrasenas();
    }
}
