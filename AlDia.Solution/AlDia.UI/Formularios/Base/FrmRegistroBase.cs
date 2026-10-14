using AlDia.BLL.Common;
using System.ComponentModel;

namespace AlDia.UI.Formularios.Base;

public partial class FrmRegistroBase : Form
{
    private readonly CancellationTokenSource _vida = new();
    private AplicacionBll? _aplicacion;
    private bool _ocupado;
    protected CancellationToken TokenFormulario => _vida.Token;
    protected AplicacionBll Aplicacion => _aplicacion
        ?? throw new InvalidOperationException("El formulario necesita los servicios de la sesión.");
    protected ErrorProvider errorIcon;
    protected virtual bool PermitirGuardar => false;

    public FrmRegistroBase()
    {
        InitializeComponent();
        components ??= new Container();
        errorIcon = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
        btnGuardar.Click += async (_, _) => await GuardarFormularioAsync();
        btnCancelar.Click += (_, _) => Cancelar();
        btnCerrar.Click += (_, _) => Cancelar();
        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
    }

    protected void ConfigurarServicios(AplicacionBll aplicacion) =>
        _aplicacion = aplicacion ?? throw new ArgumentNullException(nameof(aplicacion));
    protected virtual Task CargarDatosAsync() => Task.CompletedTask;
    protected virtual bool ValidarControles() => true;
    protected virtual Task GuardarAsync() => Task.CompletedTask;

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        btnGuardar.Enabled = PermitirGuardar;
        if (_aplicacion is null) return;
        _ocupado = true;
        panelCuerpo.Enabled = false;
        btnGuardar.Enabled = false;
        try { await CargarDatosAsync(); }
        catch (OperationCanceledException) when (_vida.IsCancellationRequested) { }
        catch (Exception ex)
        {
            if (!IsDisposed)
            {
                MostrarError(ex);
                _ocupado = false;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
        finally
        {
            _ocupado = false;
            if (!IsDisposed) { panelCuerpo.Enabled = true; btnGuardar.Enabled = PermitirGuardar; }
        }
    }

    private async Task GuardarFormularioAsync()
    {
        if (_ocupado || !PermitirGuardar || _aplicacion is null || !ValidarControles()) return;
        _ocupado = true;
        panelCuerpo.Enabled = btnGuardar.Enabled = btnCancelar.Enabled = btnCerrar.Enabled = false;
        UseWaitCursor = true;
        try
        {
            await GuardarAsync();
            _ocupado = false;
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (OperationCanceledException) when (_vida.IsCancellationRequested) { }
        catch (Exception ex) { if (!IsDisposed) MostrarError(ex); }
        finally
        {
            _ocupado = false;
            if (!IsDisposed)
            {
                panelCuerpo.Enabled = btnGuardar.Enabled = btnCancelar.Enabled = btnCerrar.Enabled = true;
                UseWaitCursor = false;
            }
        }
    }

    protected bool Requerido(TextBox control, string mensaje) => Helpers.ValidationHelper.Requerido(control, errorIcon, mensaje);
    protected void MostrarError(Exception ex) => MessageBox.Show(this, ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    private void Cancelar()
    {
        if (_ocupado) return;
        DialogResult = DialogResult.Cancel;
        Close();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Helpers.RedondearControlHelper.RedondearControl(this, 25);
        Helpers.RedondearControlHelper.RedondearControl(panelContenido, 25);
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_ocupado && e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
        base.OnFormClosing(e);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _vida.Cancel();
        base.OnFormClosed(e);
    }
}
