using AlDia.BLL.Common;
using AlDia.Entity.Common;
using System.ComponentModel;

namespace AlDia.UI.Formularios.Base;

// El base conoce los eventos comunes. Los hijos deciden qué datos y acciones ejecutar.
public partial class FrmListadoBase : Form, IModuloConServicios
{
    private AplicacionBll? _aplicacion;
    private readonly CancellationTokenSource _vida = new();
    private CancellationTokenSource? _carga;
    private int _versionCarga;
    private bool _ocupado;
    private bool _accionEnCurso;
    protected AplicacionBll Aplicacion => _aplicacion
        ?? throw new InvalidOperationException("El módulo necesita los servicios de la sesión.");
    protected CancellationToken TokenCarga { get; private set; }
    protected int PaginaActual { get; set; } = 1;
    protected int TamPagina { get; set; } = 10;
    protected int TotalRegistros { get; set; }
    protected int TotalPaginas { get; set; }
    protected virtual bool MostrarBotonPrincipal => false;
    protected virtual bool MostrarBotonSecundario => false;
    protected virtual bool MostrarBotonTerciario => false;

    public FrmListadoBase()
    {
        InitializeComponent();
        btnBuscar.Click += BtnBuscar_Click;
        txtBuscar.TextChanged += TxtBuscar_TextChanged;
        txtBuscar.KeyDown += TxtBuscar_KeyDown;
        btnAnterior.Click += BtnAnterior_Click;
        btnSiguiente.Click += BtnSiguiente_Click;
        btnNuevo.Click += BtnNuevo_Click;
        btnEditar.Click += BtnEditar_Click;
        btnCambiarEstado.Click += BtnCambiarEstado_Click;
        dgvListado.SelectionChanged += DgvListado_SelectionChanged;
        dgvListado.CellFormatting += DgvListado_CellFormatting;
        dgvListado.AllowUserToAddRows = false;
        dgvListado.ReadOnly = true;
        dgvListado.AutoGenerateColumns = false;
    }

    private async void BtnBuscar_Click(object? sender, EventArgs e) => await RecargarAsync(1);

    private async void TxtBuscar_TextChanged(object? sender, EventArgs e)
    {
        if (!IsHandleCreated) return;
        await RecargarAsync(1);
    }

    private async void TxtBuscar_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        e.SuppressKeyPress = true;
        await RecargarAsync(1);
    }

    private async void BtnAnterior_Click(object? sender, EventArgs e)
    {
        if (PaginaActual > 1) await RecargarAsync(PaginaActual - 1);
    }

    private async void BtnSiguiente_Click(object? sender, EventArgs e)
    {
        if (PaginaActual < TotalPaginas) await RecargarAsync(PaginaActual + 1);
    }

    private async void BtnNuevo_Click(object? sender, EventArgs e)
    {
        await EjecutarAccionAsync(async () =>
        {
            using Form? formulario = CrearFormularioAccionPrincipal();
            if (formulario is not null && MostrarModal(formulario) == DialogResult.OK)
                await RecargarAsync();
        });
    }

    private async void BtnEditar_Click(object? sender, EventArgs e)
    {
        await EjecutarAccionAsync(async () =>
        {
            int? id = ObtenerIdSeleccionado();
            if (id is null) return;
            using Form? formulario = CrearFormularioAccionSecundaria(id.Value);
            if (formulario is not null && MostrarModal(formulario) == DialogResult.OK)
                await RecargarAsync();
        });
    }

    private async void BtnCambiarEstado_Click(object? sender, EventArgs e) =>
        await EjecutarAccionAsync(DesactivarAsync);

    private void DgvListado_SelectionChanged(object? sender, EventArgs e) => ActualizarBotones();
    private void DgvListado_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e) => FormatearCelda(e);

    public virtual void ConfigurarServicios(AplicacionBll aplicacion)
    {
        ArgumentNullException.ThrowIfNull(aplicacion);
        if (ReferenceEquals(_aplicacion, aplicacion)) return;
        _aplicacion = aplicacion;
        if (IsHandleCreated && Visible) _ = RecargarAsync(1);
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        ActualizarBotones();
        if (_aplicacion is not null) await RecargarAsync(1);
    }

    protected virtual Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null) => Task.CompletedTask;
    protected virtual Form? CrearFormularioAccionPrincipal() => null;
    protected virtual Form? CrearFormularioAccionSecundaria(int id) => null;
    protected virtual int? ObtenerIdSeleccionado() => null;
    protected virtual Task DesactivarAsync() => Task.CompletedTask;
    protected T? ObtenerSeleccionado<T>() where T : class => dgvListado.CurrentRow?.DataBoundItem as T;
    protected bool PuedeMantener(params AlDia.Entity.Seguridad.RolUsuario[] roles) =>
        _aplicacion?.Sesion.UsuarioActual?.PermisoRol is { } rol && roles.Contains(rol);

    public async Task RecargarAsync(int? pagina = null)
    {
        if (_aplicacion is null || IsDisposed || _vida.IsCancellationRequested) return;
        _carga?.Cancel();
        using var carga = CancellationTokenSource.CreateLinkedTokenSource(_vida.Token);
        _carga = carga;
        TokenCarga = carga.Token;
        int version = ++_versionCarga;
        _ocupado = true;
        ActualizarBotones();
        try
        {
            await MostrarDatosAsync(Math.Max(1, pagina ?? PaginaActual), TamPagina, txtBuscar.Text);
            // Una desactivación puede haber eliminado el único registro de la última página.
            if (!carga.IsCancellationRequested && TotalPaginas > 0 && PaginaActual > TotalPaginas)
                await MostrarDatosAsync(TotalPaginas, TamPagina, txtBuscar.Text);
        }
        catch (OperationCanceledException) when (carga.IsCancellationRequested) { }
        catch (Exception ex)
        {
            if (!carga.IsCancellationRequested && !IsDisposed) MostrarError(ex);
        }
        finally
        {
            if (version == _versionCarga)
            {
                _carga = null;
                _ocupado = false;
                if (!IsDisposed) ActualizarBotones();
            }
        }
    }

    protected void MostrarPagina<T>(PaginacionEntity<T> resultado, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        if (IsDisposed) return;
        dgvListado.DataSource = resultado.Registros.ToList();
        PaginaActual = resultado.PaginaActual;
        TamPagina = resultado.TamPagina;
        TotalRegistros = resultado.TotalRegistros;
        TotalPaginas = resultado.TotalPaginas;
        ActualizarPaginacion();
        MostrarEstadoVacio(resultado.Registros.Count > 0);
    }

    protected void ActualizarPaginacion()
    {
        lblPagina.Text = TotalRegistros == 0 ? "Página 0 de 0" : $"Página {PaginaActual} de {TotalPaginas}";
        lblPie.Text = $"Total de registros: {TotalRegistros}";
        ActualizarBotones();
    }

    protected void MostrarEstadoVacio(bool tieneDatos)
    {
        dgvListado.Dock = tieneDatos ? DockStyle.Fill : DockStyle.Top;
        if (!tieneDatos) dgvListado.Height = dgvListado.ColumnHeadersHeight;
        panelVacio.Visible = !tieneDatos;
        if (!tieneDatos) panelVacio.BringToFront();
    }

    protected void VincularColumna(string columna, string propiedad)
    {
        if (dgvListado.Columns.Contains(columna)) dgvListado.Columns[columna]!.DataPropertyName = propiedad;
    }

    protected void AjustarColumnas(params string[] columnas)
    {
        foreach (string columna in columnas)
            if (dgvListado.Columns.Contains(columna))
                dgvListado.Columns[columna]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
    }

    protected virtual void FormatearCelda(DataGridViewCellFormattingEventArgs e)
    {
        if (e.ColumnIndex < 0) return;
        if (dgvListado.Columns[e.ColumnIndex].DataPropertyName == "Estado" && e.Value is bool activo)
        {
            e.Value = activo ? "Activo" : "Inactivo";
            e.FormattingApplied = true;
        }
    }

    protected bool ConfirmarCambioEstado(bool activo) => MessageBox.Show(TopLevelControl as Form ?? this,
        $"¿Desea {(activo ? "desactivar" : "activar")} el registro seleccionado?", "Confirmación",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

    protected DialogResult MostrarModal(Form formulario)
    {
        Form propietario = TopLevelControl as Form ?? this;
        using var overlay = new FrmOverlay(propietario);
        overlay.Show(propietario);
        overlay.BringToFront();
        formulario.StartPosition = FormStartPosition.CenterParent;
        return formulario.ShowDialog(overlay);
    }

    private async Task EjecutarAccionAsync(Func<Task> accion)
    {
        if (_accionEnCurso || _ocupado) return;
        _accionEnCurso = true;
        ActualizarBotones();
        try { await accion(); }
        catch (OperationCanceledException) when (_vida.IsCancellationRequested) { }
        catch (Exception ex) { if (!IsDisposed) MostrarError(ex); }
        finally { _accionEnCurso = false; if (!IsDisposed) ActualizarBotones(); }
    }

    private void ActualizarBotones()
    {
        bool listo = !_ocupado && !_accionEnCurso && _aplicacion is not null;
        bool seleccionado = ObtenerIdSeleccionado().HasValue;
        btnNuevo.Visible = MostrarBotonPrincipal;
        btnEditar.Visible = MostrarBotonSecundario;
        btnCambiarEstado.Visible = MostrarBotonTerciario;
        btnNuevo.Enabled = listo && MostrarBotonPrincipal;
        btnEditar.Enabled = listo && seleccionado && MostrarBotonSecundario;
        btnCambiarEstado.Enabled = listo && seleccionado && MostrarBotonTerciario;
        btnAnterior.Enabled = listo && PaginaActual > 1 && TotalRegistros > 0;
        btnSiguiente.Enabled = listo && PaginaActual < TotalPaginas;
        UseWaitCursor = _ocupado;
        lblSeleccion.Text = seleccionado ? "Registro seleccionado" : "Seleccione un registro para ver las acciones";
    }

    protected void MostrarError(Exception ex) => MessageBox.Show(TopLevelControl as Form ?? this,
        ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        CancelarOperaciones();
        base.OnFormClosed(e);
    }
    private void CancelarOperaciones()
    {
        _vida.Cancel();
        _carga?.Cancel();
    }
}
