using AlDia.UI.Formularios.Base;
using AlDia.BLL.Common;
using AlDia.Entity.Common;

namespace AlDia.UI.Formularios.Hijos.OrdenesServicio;

public partial class FrmDetalleOrdenServicio : FrmRegistroBase
{
    private readonly int? _idOrden;
    public FrmDetalleOrdenServicio()
    {
        InitializeComponent();
        btnGuardar.Visible = false;
        btnCancelar.Text = "Cerrar";
        // Esta vista consulta el expediente; las operaciones técnicas tienen sus propias reglas.
        PrepararConsulta(panelCuerpo);
    }

    public FrmDetalleOrdenServicio(AplicacionBll aplicacion, int idOrden) : this()
    {
        ConfigurarServicios(aplicacion);
        _idOrden = Validacion.Id(idOrden, "IdOrden");
    }

    protected override async Task CargarDatosAsync()
    {
        if (!_idOrden.HasValue) return;
        var expediente = await Aplicacion.Ordenes.ConsultarExpedienteAsync(_idOrden.Value, TokenFormulario)
            ?? throw new ValidacionException("La orden ya no existe.");
        var orden = expediente.Orden;
        var equipo = await Aplicacion.Equipos.ObtenerPorIdAsync(orden.IdEquipo, TokenFormulario);
        var estados = await Aplicacion.Ordenes.ConsultarEstadosAsync(TokenFormulario);
        txtOrdenResumen.Text = orden.NumeroOrden;
        txtClienteResumen.Text = orden.NombreCompletoCliente;
        txtEquipoResumen.Text = equipo is null ? $"Equipo #{orden.IdEquipo}" : $"{equipo.Marca} {equipo.Modelo}".Trim();
        txtEstadoResumen.Text = orden.EstadoNombre;
        txtTecnicoResumen.Text = orden.IdTecnicoResponsable.HasValue ? $"Técnico #{orden.IdTecnicoResponsable}" : "Sin asignar";
        txtSaldoResumen.Text = orden.Saldo.ToString("N2");
        txtFechaRecepcionDetalle.Text = orden.FechaRecepcion.ToString("g");
        txtRecepcionistaDetalle.Text = $"Usuario #{orden.IdRecepcionista}";
        txtProblemaRecepcionDetalle.Text = orden.ProblemaReportado;
        txtAccesoriosDetalle.Text = orden.AccesoriosRecepcion;
        txtObservacionesDetalle.Text = orden.ObservacionesRecepcion;
        var diagnostico = expediente.Diagnostico;
        txtFechaDiagnostico.Text = diagnostico?.FechaDiagnostico.ToString("g") ?? "";
        txtProblemaEncontrado.Text = diagnostico?.ProblemaEncontrado;
        txtReparacionPropuesta.Text = diagnostico?.ReparacionPropuesta;
        txtObservacionesDiagnostico.Text = diagnostico?.Observaciones;
        cboTecnicoDiagnostico.Items.Clear();
        cboTecnicoDiagnostico.Items.Add(diagnostico is null ? "Sin diagnóstico" : $"Técnico #{diagnostico.IdTecnico}");
        cboTecnicoDiagnostico.SelectedIndex = 0;
        cboDecisionCliente.Items.Clear();
        cboDecisionCliente.Items.Add(expediente.Confirmacion?.Decision.ToString() ?? "Sin confirmar");
        cboDecisionCliente.SelectedIndex = 0;
        cboResultadoReparacion.Items.Clear();
        cboResultadoReparacion.Items.Add(orden.EstadoNombre);
        cboResultadoReparacion.SelectedIndex = 0;
        numCostoEstimado.Maximum = numCostoAprobado.Maximum = 99999999.99m;
        numCostoEstimado.Value = diagnostico?.CostoEstimado ?? 0;
        numCostoAprobado.Value = expediente.Confirmacion?.CostoAprobado ?? 0;
        txtFechaConfirmacion.Text = expediente.Confirmacion?.FechaConfirmacion.ToString("g") ?? "";
        txtTotalTrabajos.Text = txtTotalPagos.Text = orden.Total.ToString("N2");
        txtPagadoOrden.Text = orden.Pagado.ToString("N2");
        txtSaldoPagos.Text = orden.Saldo.ToString("N2");
        VincularGrid(dgvServiciosOrden, "Servicio", "Cantidad", "Precio", "Subtotal");
        dgvServiciosOrden.DataSource = expediente.Servicios.Select(x => new
        { Servicio = $"Servicio #{x.IdServicio}", x.Cantidad, x.Precio, x.Subtotal }).ToList();
        VincularGrid(dgvRepuestosOrden, "Repuesto", "Cantidad", "Precio", "Subtotal");
        dgvRepuestosOrden.DataSource = expediente.Repuestos.Select(x => new
        { Repuesto = $"Repuesto #{x.IdRepuesto}", x.Cantidad, x.Precio, x.Subtotal }).ToList();
        VincularGrid(dgvPagosOrden, "FechaPago", "Monto", "MetodoPago", "Usuario", "Estado");
        dgvPagosOrden.DataSource = expediente.Pagos.Select(x => new
        { x.FechaPago, x.Monto, x.MetodoPago, Usuario = $"Usuario #{x.IdUsuario}", Estado = x.Anulado ? "Anulado" : "Vigente" }).ToList();
        VincularGrid(dgvHistorialOrden, "FechaCambio", "Anterior", "Nuevo", "Usuario", "Observacion");
        dgvHistorialOrden.DataSource = expediente.Historial.Select(x => new
        {
            x.FechaCambio, Anterior = estados.FirstOrDefault(s => s.IdEstado == x.IdEstadoAnterior)?.Nombre ?? "—",
            Nuevo = estados.FirstOrDefault(s => s.IdEstado == x.IdEstadoNuevo)?.Nombre ?? $"Estado #{x.IdEstadoNuevo}",
            Usuario = $"Usuario #{x.IdUsuario}", x.Observacion
        }).ToList();
    }

    private static void VincularGrid(DataGridView grid, params string[] propiedades)
    {
        grid.AutoGenerateColumns = false;
        for (int i = 0; i < Math.Min(propiedades.Length, grid.Columns.Count); i++)
            grid.Columns[i].DataPropertyName = propiedades[i];
    }

    private static void PrepararConsulta(Control contenedor)
    {
        foreach (Control control in contenedor.Controls)
        {
            if (control is TextBox texto) texto.ReadOnly = true;
            if (control is Button or ComboBox or NumericUpDown) control.Enabled = false;
            if (control is DataGridView grid) { grid.ReadOnly = true; grid.AllowUserToAddRows = false; }
            PrepararConsulta(control);
        }
    }
}
