using AlDia.BLL.Common;
using System.ComponentModel;

namespace AlDia.UI.Formularios.Base;

public partial class FrmRegistroBase : Form // Base común para los formularios de registro.
{
    private readonly CancellationTokenSource _vida = new(); // Permite cancelar tareas al cerrar la ventana.
    private AplicacionBll? _aplicacion; // Guarda los servicios de la aplicación.
    private bool _ocupado; // Evita acciones mientras se carga o guarda.
    protected CancellationToken TokenFormulario => _vida.Token; // Expone el token de cancelación a los formularios hijos.
    protected AplicacionBll Aplicacion => _aplicacion // Devuelve los servicios o avisa si no se configuraron.
        ?? throw new InvalidOperationException("El formulario necesita los servicios de la sesión.");
    protected ErrorProvider errorIcon; // Muestra errores junto a los campos.
    protected virtual bool PermitirGuardar => false; // Los formularios hijos pueden habilitar Guardar.

    public FrmRegistroBase() // Prepara controles, botones y eventos.
    {
        InitializeComponent();
        components ??= new Container();
        errorIcon = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink }; // Configura el indicador de errores sin parpadeo.
        btnGuardar.Click += async (_, _) => await GuardarFormularioAsync(); // Guardar inicia la operación asíncrona.
        btnCancelar.Click += (_, _) => Cancelar(); // Cancelar cierra el formulario.
        btnCerrar.Click += (_, _) => Cancelar(); // Cerrar usa la misma acción que Cancelar.
        panelPie.SizeChanged += (_, _) => AlinearAccionesPie();
        AcceptButton = btnGuardar; // Enter activa Guardar.
        CancelButton = btnCancelar; // Escape activa Cancelar.
    }

    protected void ConfigurarServicios(AplicacionBll aplicacion) => // Guarda los servicios necesarios para trabajar.
        _aplicacion = aplicacion ?? throw new ArgumentNullException(nameof(aplicacion));
    protected virtual Task CargarDatosAsync() => Task.CompletedTask; // Los formularios hijos pueden implementar la carga.
    protected virtual bool ValidarControles() => true; // Cada formulario hijo define su validación.
    protected virtual Task GuardarAsync() => Task.CompletedTask; // Cada formulario hijo define cómo guardar.

    protected override async void OnLoad(EventArgs e) // Prepara el formulario cuando se abre.
    {
        base.OnLoad(e);
        if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return; // Evita ejecutar servicios en el diseñador de Visual Studio.
        btnGuardar.Enabled = PermitirGuardar;
        if (_aplicacion is null) return; // Sin servicios configurados no puede cargar datos.
        _ocupado = true;
        panelCuerpo.Enabled = false; // Bloquea los campos durante la carga.
        btnGuardar.Enabled = false;
        try
        {
            await CargarDatosAsync(); // Espera a que el formulario hijo cargue los datos.
            AplicarEstiloCampos(panelCuerpo);
            AlinearAccionesPie();
        }
        catch (OperationCanceledException) when (_vida.IsCancellationRequested) { } // Ignora la cancelación solicitada al cerrar.
        catch (Exception ex) // Muestra otros errores ocurridos durante la operación.
        {
            if (!IsDisposed)
            {
                MostrarError(ex); // Informa al usuario del problema.
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

    private async Task GuardarFormularioAsync() // Coordina la validación y el guardado.
    {
        if (_ocupado || !PermitirGuardar || _aplicacion is null || !ValidarControles()) return; // No guarda si falta algún requisito o ya hay otra tarea.
        _ocupado = true;
        panelCuerpo.Enabled = btnGuardar.Enabled = btnCancelar.Enabled = btnCerrar.Enabled = false;
        UseWaitCursor = true; // Muestra el cursor de espera mientras guarda.
        try
        {
            await GuardarAsync(); // Ejecuta el guardado definido por el formulario hijo.
            _ocupado = false;
            DialogResult = DialogResult.OK; // Indica que la operación terminó correctamente.
            Close();
        }
        catch (OperationCanceledException) when (_vida.IsCancellationRequested) { } // Ignora la cancelación solicitada al cerrar.
        catch (Exception ex) { if (!IsDisposed) MostrarError(ex); } // Muestra otros errores ocurridos durante la operación.
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

    protected bool Requerido(TextBox control, string mensaje) => Helpers.ValidationHelper.Requerido(control, errorIcon, mensaje); // Comprueba que un campo obligatorio tenga contenido.
    protected void MostrarError(Exception ex) => MessageBox.Show(this, ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Presenta el mensaje de error en una ventana.
    private void Cancelar() // Cierra la ventana con resultado Cancelar.
    {
        if (_ocupado) return;
        DialogResult = DialogResult.Cancel;
        Close();
    }

    protected override void OnShown(EventArgs e) // Ajusta la apariencia después de mostrar la ventana.
    {
        base.OnShown(e);
        AjustarAlAreaDisponible();
        Helpers.RedondearControlHelper.RedondearControl(this, 25);
        Helpers.RedondearControlHelper.RedondearControl(panelContenido, 25);
    }

    private void AjustarAlAreaDisponible()
    {
        var area = Screen.FromControl(this).WorkingArea;
        int anchoMaximo = Math.Max(320, area.Width - 24);
        int altoMaximo = Math.Max(320, area.Height - 24);
        if (Width > anchoMaximo) Width = anchoMaximo;
        if (Height > altoMaximo) Height = altoMaximo;

        // El contenido permanece accesible si la ventana debe reducirse por la resolución.
        panelCuerpo.AutoScroll = true;
        panelCuerpo.PerformLayout();

        Rectangle referencia = Owner?.Bounds ?? area;
        int x = referencia.Left + (referencia.Width - Width) / 2;
        int y = referencia.Top + (referencia.Height - Height) / 2;
        x = Math.Clamp(x, area.Left, area.Right - Width);
        y = Math.Clamp(y, area.Top, area.Bottom - Height);
        Location = new Point(x, y);
    }

    private void AplicarEstiloCampos(Control contenedor)
    {
        foreach (Control control in contenedor.Controls)
        {
            if (control is TextBoxBase or ComboBox or DateTimePicker or NumericUpDown or MaskedTextBox)
                control.BackColor = Color.White;
            AplicarEstiloCampos(control);
        }
    }

    private void AlinearAccionesPie()
    {
        if (panelPie.IsDisposed || panelPie.ClientSize.Width <= 0) return;

        var acciones = panelPie.Controls.OfType<Button>()
            .Where(b => b.Visible && b != btnGuardar && b != btnCancelar)
            .OrderBy(b => b.TabIndex)
            .ToList();
        if (btnGuardar.Visible) acciones.Insert(0, btnGuardar);
        if (btnCancelar.Visible) acciones.Add(btnCancelar);

        const int margen = 32;
        const int espacio = 12;
        int anchoAcciones = acciones.Sum(a => a.Width) + Math.Max(0, acciones.Count - 1) * espacio;
        int x = Math.Max(margen, panelPie.ClientSize.Width - margen - anchoAcciones);
        foreach (Button accion in acciones)
        {
            accion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            accion.Location = new Point(x, Math.Max(8, (panelPie.ClientSize.Height - accion.Height) / 2));
            x += accion.Width + espacio;
        }

        int inicioEtiqueta = margen;
        int anchoEtiqueta = Math.Max(0, panelPie.ClientSize.Width - anchoAcciones - margen * 2 - espacio);
        if (anchoEtiqueta >= 80)
        {
            lblObligatorios.Visible = true;
            lblObligatorios.TextAlign = ContentAlignment.MiddleLeft;
            lblObligatorios.Location = new Point(inicioEtiqueta, Math.Max(8, (panelPie.ClientSize.Height - lblObligatorios.Height) / 2));
            lblObligatorios.Size = new Size(anchoEtiqueta, lblObligatorios.Height);
        }
        else
        {
            lblObligatorios.Visible = false;
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e) // Revisa si se puede cerrar la ventana.
    {
        if (_ocupado && e.CloseReason == CloseReason.UserClosing) e.Cancel = true; // Impide cerrar mientras hay una operación activa.
        base.OnFormClosing(e);
    }

    protected override void OnFormClosed(FormClosedEventArgs e) // Limpia operaciones pendientes al cerrar.
    {
        _vida.Cancel(); // Solicita cancelar las tareas asociadas al formulario.
        base.OnFormClosed(e);
    }
}
