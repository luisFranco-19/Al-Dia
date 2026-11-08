using AlDia.UI.Formularios.Base;
using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Inventario;
using AlDia.Entity.Reparaciones;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Hijos.Pagos;
using AlDia.UI.Helpers;
using FontAwesome.Sharp;

namespace AlDia.UI.Formularios.Hijos.OrdenesServicio;

public partial class FrmDetalleOrdenServicio : FrmRegistroBase
{
    private readonly int? _idOrden;
    private readonly IconButton _btnAccionEstado = new();
    private readonly IconButton _btnEntregaRechazada = new();
    private readonly Label _lblTransicionPermitida = new();
    private readonly Label _lblObservacionesConfirmacion = new();
    private readonly TextBox _txtObservacionesConfirmacion = new();
    private ExpedienteOrdenEntity? _expediente;
    public FrmDetalleOrdenServicio()
    {
        InitializeComponent();
        PrepararGrillaLectura(dgvServiciosOrden);
        PrepararGrillaLectura(dgvRepuestosOrden);
        PrepararGrillaLectura(dgvPagosOrden);
        PrepararGrillaLectura(dgvHistorialOrden);
        dgvRepuestosOrden.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colExistenciaActual",
            HeaderText = "Existencia actual",
            DataPropertyName = "ExistenciaActual",
            FillWeight = 18F,
            ReadOnly = true,
            SortMode = DataGridViewColumnSortMode.NotSortable
        });
        btnGuardar.Visible = false;
        btnCancelar.Text = "Cerrar";
        // Esta vista consulta el expediente; las operaciones técnicas tienen sus propias reglas.
        PrepararConsulta(panelCuerpo);
        _btnAccionEstado.Visible = false;
        _btnAccionEstado.Size = new Size(185, 56);
        _btnAccionEstado.Location = new Point(365, 27);
        _btnAccionEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnAccionEstado.TabIndex = 0;
        _btnAccionEstado.BackColor = Color.FromArgb(37, 99, 235);
        _btnAccionEstado.ForeColor = Color.White;
        _btnAccionEstado.FlatStyle = FlatStyle.Flat;
        _btnAccionEstado.FlatAppearance.BorderSize = 0;
        _btnAccionEstado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _btnAccionEstado.IconColor = Color.White;
        _btnAccionEstado.IconSize = 18;
        _btnAccionEstado.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnAccionEstado.Click += async (_, _) => await EjecutarAccionEstadoAsync();
        panelPie.Controls.Add(_btnAccionEstado);
        _btnEntregaRechazada.Visible = false;
        _btnEntregaRechazada.Size = new Size(185, 56);
        _btnEntregaRechazada.Location = new Point(165, 27);
        _btnEntregaRechazada.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _btnEntregaRechazada.TabIndex = 1;
        _btnEntregaRechazada.BackColor = Color.White;
        _btnEntregaRechazada.ForeColor = Color.FromArgb(37, 99, 235);
        _btnEntregaRechazada.FlatStyle = FlatStyle.Flat;
        _btnEntregaRechazada.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        _btnEntregaRechazada.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _btnEntregaRechazada.IconColor = Color.FromArgb(37, 99, 235);
        _btnEntregaRechazada.IconSize = 18;
        _btnEntregaRechazada.TextImageRelation = TextImageRelation.ImageBeforeText;
        _btnEntregaRechazada.Click += async (_, _) => await RegistrarEntregaAsync();
        panelPie.Controls.Add(_btnEntregaRechazada);
        _lblTransicionPermitida.AutoEllipsis = true;
        _lblTransicionPermitida.Location = new Point(18, 86);
        _lblTransicionPermitida.Size = new Size(906, 52);
        _lblTransicionPermitida.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _lblTransicionPermitida.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _lblTransicionPermitida.ForeColor = Color.FromArgb(37, 99, 235);
        tabHistorial.Controls.Add(_lblTransicionPermitida);
        dgvHistorialOrden.Location = new Point(18, 146);
        dgvHistorialOrden.Height = 256;
        _lblObservacionesConfirmacion.Text = "Observaciones de la respuesta del cliente";
        _lblObservacionesConfirmacion.Location = new Point(18, 632);
        _lblObservacionesConfirmacion.Size = new Size(906, 22);
        _lblObservacionesConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _lblObservacionesConfirmacion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        _lblObservacionesConfirmacion.ForeColor = Color.FromArgb(20, 43, 78);
        _txtObservacionesConfirmacion.Location = new Point(18, 660);
        _txtObservacionesConfirmacion.Size = new Size(906, 64);
        _txtObservacionesConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        _txtObservacionesConfirmacion.Font = new Font("Segoe UI", 10F);
        _txtObservacionesConfirmacion.ForeColor = Color.FromArgb(20, 43, 78);
        _txtObservacionesConfirmacion.BackColor = Color.White;
        _txtObservacionesConfirmacion.BorderStyle = BorderStyle.FixedSingle;
        _txtObservacionesConfirmacion.Multiline = true;
        _txtObservacionesConfirmacion.MaxLength = 500;
        _txtObservacionesConfirmacion.ScrollBars = ScrollBars.Vertical;
        _txtObservacionesConfirmacion.ReadOnly = true;
        tabDiagnostico.Controls.Add(_lblObservacionesConfirmacion);
        tabDiagnostico.Controls.Add(_txtObservacionesConfirmacion);
        btnAgregarServicio.Visible = false;
        btnAgregarRepuesto.Visible = false;
        btnRegistrarPagoOrden.Visible = false;
        btnAgregarServicio.Click += async (_, _) => await AgregarServicioAsync();
        btnAgregarRepuesto.Click += async (_, _) => await AgregarRepuestoAsync();
        btnRegistrarPagoOrden.Click += async (_, _) => await RegistrarPagoAsync();
        cboDecisionCliente.SelectedIndexChanged += (_, _) => ActualizarCostoAprobado();
    }

    private static void PrepararGrillaLectura(DataGridView grilla)
    {
        grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = grilla.ColumnHeadersDefaultCellStyle.BackColor;
        grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
        grilla.DefaultCellStyle.ForeColor = Color.Black;
        grilla.DefaultCellStyle.SelectionForeColor = Color.Black;
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
        _expediente = expediente;
        var orden = expediente.Orden;
        var equipo = await Aplicacion.Equipos.ObtenerPorIdAsync(orden.IdEquipo, TokenFormulario);
        var estados = await Aplicacion.Ordenes.ConsultarEstadosAsync(TokenFormulario);
        var serviciosCatalogo = await Aplicacion.Servicios.ConsultarAsync(soloActivos: false, ct: TokenFormulario);
        var repuestosCatalogo = await Aplicacion.Repuestos.ConsultarAsync(soloActivos: false, ct: TokenFormulario);
        var nombresServicios = serviciosCatalogo.ToDictionary(x => x.IdServicio, x => x.Nombre);
        var nombresRepuestos = repuestosCatalogo.ToDictionary(x => x.IdRepuesto, x => x.Nombre);
        var existenciasRepuestos = repuestosCatalogo.ToDictionary(x => x.IdRepuesto, x => x.Stock);
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
        _txtObservacionesConfirmacion.Text = expediente.Confirmacion?.Observaciones;
        lbltxtObservacionesDiagnostico.Text = "Observaciones del diagnóstico";
        cboTecnicoDiagnostico.Items.Clear();
        cboTecnicoDiagnostico.Items.Add(diagnostico is null ? "Sin diagnóstico" : $"Técnico #{diagnostico.IdTecnico}");
        cboTecnicoDiagnostico.SelectedIndex = 0;
        cboDecisionCliente.Items.Clear();
        cboDecisionCliente.Items.Add(expediente.Confirmacion?.Decision.ToString() ?? "Sin confirmar");
        cboDecisionCliente.SelectedIndex = 0;
        cboResultadoReparacion.Items.Clear();
        cboResultadoReparacion.Items.Add(orden.EstadoNombre);
        cboResultadoReparacion.SelectedIndex = 0;
        numCostoEstimado.Text = DineroTextBoxHelper.Formatear(diagnostico?.CostoEstimado ?? 0);
        numCostoAprobado.Text = DineroTextBoxHelper.Formatear(expediente.Confirmacion?.CostoAprobado ?? 0);
        txtFechaConfirmacion.Text = expediente.Confirmacion?.FechaConfirmacion.ToString("g") ?? "";
        txtTotalTrabajos.Text = txtTotalPagos.Text = orden.Total.ToString("N2");
        txtPagadoOrden.Text = orden.Pagado.ToString("N2");
        txtSaldoPagos.Text = orden.Saldo.ToString("N2");
        VincularGrid(dgvServiciosOrden, "Servicio", "Cantidad", "Precio", "Subtotal");
        dgvServiciosOrden.DataSource = expediente.Servicios.Select(x => new
        { Servicio = nombresServicios.GetValueOrDefault(x.IdServicio, $"Servicio #{x.IdServicio}"), x.Cantidad, x.Precio, x.Subtotal }).ToList();
        VincularGrid(dgvRepuestosOrden, "Repuesto", "Cantidad", "Precio", "Subtotal", "ExistenciaActual");
        dgvRepuestosOrden.DataSource = expediente.Repuestos.Select(x => new
        {
            Repuesto = nombresRepuestos.GetValueOrDefault(x.IdRepuesto, $"Repuesto #{x.IdRepuesto}"),
            x.Cantidad, x.Precio, x.Subtotal,
            ExistenciaActual = existenciasRepuestos.GetValueOrDefault(x.IdRepuesto)
        }).ToList();
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
        ConfigurarAcciones(expediente);
    }

    private void ConfigurarAcciones(ExpedienteOrdenEntity expediente)
    {
        var actor = Aplicacion.Sesion.UsuarioActual!;
        bool tecnicoPropietario = actor.PermisoRol == RolUsuario.Tecnico
            && expediente.Orden.IdTecnicoResponsable == actor.IdUsuario;
        bool recepcionista = actor.PermisoRol == RolUsuario.Recepcionista;
        string estado = expediente.Orden.EstadoNombre;

        _btnAccionEstado.Visible = false;
        btnAgregarServicio.Visible = btnAgregarRepuesto.Visible = btnRegistrarPagoOrden.Visible = false;
        _btnEntregaRechazada.Visible = false;
        btnAgregarServicio.Enabled = btnAgregarRepuesto.Enabled = btnRegistrarPagoOrden.Enabled = true;
        ConfigurarEntrada(txtProblemaEncontrado, false);
        ConfigurarEntrada(txtReparacionPropuesta, false);
        ConfigurarEntrada(txtObservacionesDiagnostico, false);
        ConfigurarEntrada(numCostoEstimado, false);
        cboDecisionCliente.Enabled = false;
        ConfigurarEntrada(numCostoAprobado, false);
        ConfigurarEntrada(_txtObservacionesConfirmacion, false);
        cboResultadoReparacion.Enabled = false;
        txtObservacionesTrabajo.ReadOnly = true;

        if (tecnicoPropietario && estado == "En Revisión" && expediente.Diagnostico is null)
        {
            ConfigurarEntrada(txtProblemaEncontrado, true);
            ConfigurarEntrada(txtReparacionPropuesta, true);
            ConfigurarEntrada(txtObservacionesDiagnostico, true);
            ConfigurarEntrada(numCostoEstimado, true);
            ConfigurarAccion("Guardar diagnóstico", IconChar.ClipboardCheck);
        }
        else if (tecnicoPropietario && estado == "Pendiente de Confirmación"
            && expediente.Diagnostico is not null && expediente.Confirmacion is null)
        {
            ConfigurarEntrada(txtProblemaEncontrado, true);
            ConfigurarEntrada(txtReparacionPropuesta, true);
            ConfigurarEntrada(txtObservacionesDiagnostico, true);
            ConfigurarEntrada(numCostoEstimado, true);
            ConfigurarAccion("Corregir diagnóstico", IconChar.Edit);
        }
        else if (recepcionista && estado == "Pendiente de Confirmación" && expediente.Diagnostico is not null && expediente.Confirmacion is null)
        {
            cboDecisionCliente.Items.Clear();
            cboDecisionCliente.Items.AddRange(new object[] { "Aprobada", "Rechazada" });
            cboDecisionCliente.SelectedIndex = 0;
            cboDecisionCliente.Enabled = true;
            ConfigurarEntrada(_txtObservacionesConfirmacion, true);
            ActualizarCostoAprobado();
            ConfigurarAccion("Registrar respuesta", IconChar.Check);
        }
        else if (recepcionista && (estado is "Aprobada" or "Rechazada") && expediente.Confirmacion is not null)
        {
            cboDecisionCliente.Items.Clear();
            cboDecisionCliente.Items.AddRange(new object[] { "Aprobada", "Rechazada" });
            cboDecisionCliente.SelectedItem = expediente.Confirmacion.Decision == DecisionCliente.Aprobada ? "Aprobada" : "Rechazada";
            cboDecisionCliente.Enabled = true;
            ConfigurarEntrada(_txtObservacionesConfirmacion, true);
            ActualizarCostoAprobado();
            ConfigurarAccion("Corregir respuesta", IconChar.Edit);
            if (estado == "Rechazada" && expediente.Orden.Saldo <= 0)
                ConfigurarEntregaRechazada();
        }
        else if (tecnicoPropietario && estado == "Aprobada")
        {
            ConfigurarAccion("Iniciar reparación", IconChar.Play);
        }
        else if (tecnicoPropietario && estado == "En Reparación")
        {
            btnAgregarServicio.Visible = btnAgregarRepuesto.Visible = true;
            cboResultadoReparacion.Items.Clear();
            cboResultadoReparacion.Items.AddRange(new object[] { "Reparada", "No Reparada" });
            cboResultadoReparacion.SelectedIndex = 0;
            cboResultadoReparacion.Enabled = true;
            txtObservacionesTrabajo.ReadOnly = false;
            ConfigurarAccion("Finalizar reparación", IconChar.CheckDouble);
        }
        else if (recepcionista && estado is "Reparada" or "No Reparada" && expediente.Orden.Saldo > 0)
        {
            btnRegistrarPagoOrden.Visible = true;
        }
        else if (recepcionista && (estado is "Reparada" or "No Reparada" or "Rechazada")
            && expediente.Orden.Saldo <= 0)
        {
            ConfigurarAccion("Registrar entrega", IconChar.Handshake);
        }
        _lblTransicionPermitida.Text = DescribirTransiciones(expediente, actor.PermisoRol, tecnicoPropietario);
        lblHistorialDescripcion.Text = "Transiciones permitidas y registro histórico de la orden.";
    }

    private void ConfigurarEntregaRechazada()
    {
        _btnEntregaRechazada.Text = "Registrar entrega";
        _btnEntregaRechazada.IconChar = IconChar.Handshake;
        _btnEntregaRechazada.Visible = true;
    }

    private static string DescribirTransiciones(ExpedienteOrdenEntity expediente, RolUsuario? rol, bool tecnicoPropietario)
    {
        string estado = expediente.Orden.EstadoNombre;
        if (tecnicoPropietario && estado == "En Revisión" && expediente.Diagnostico is null)
            return "Siguiente transición: En Revisión → Pendiente de Confirmación, al guardar el diagnóstico.";
        if (tecnicoPropietario && estado == "Pendiente de Confirmación" && expediente.Confirmacion is null)
            return "Estado actual: Pendiente de Confirmación. El técnico responsable puede corregir el diagnóstico; recepción puede pasar a Aprobada o Rechazada.";
        if (rol == RolUsuario.Recepcionista && estado == "Pendiente de Confirmación")
            return "Transiciones permitidas: Pendiente de Confirmación → Aprobada o Rechazada. La aprobación requiere el costo autorizado.";
        if (rol == RolUsuario.Administrador && estado == "Pendiente de Confirmación")
            return "Siguientes transiciones: Pendiente de Confirmación → Aprobada o Rechazada (recepción). La aprobación requiere el costo autorizado.";
        if (estado == "Aprobada")
            return tecnicoPropietario
                ? "Siguiente transición: Aprobada → En Reparación. Recepción puede corregir la decisión antes de iniciar el trabajo."
                : "Siguiente transición: Aprobada → En Reparación (técnico responsable). Recepción puede corregir la decisión antes de iniciar.";
        if (estado == "Rechazada")
            return expediente.Orden.Saldo <= 0
                ? "Transición permitida: Rechazada → Entregada (recepción). Recepción también puede corregir la decisión antes de entregar."
                : $"La entrega requiere saldo C$ 0.00; falta C$ {expediente.Orden.Saldo:N2}. Recepción puede corregir la decisión antes de entregar.";
        if (estado == "En Reparación")
            return "Transiciones al finalizar: En Reparación → Reparada o No Reparada (técnico responsable).";
        if (rol == RolUsuario.Recepcionista && (estado is "Reparada" or "No Reparada"))
            return expediente.Orden.Saldo > 0
                ? $"Estado actual: {estado}. Para pasar a Entregada, falta pagar C$ {expediente.Orden.Saldo:N2}."
                : $"Siguiente transición: {estado} → Entregada.";
        if (rol == RolUsuario.Administrador && (estado is "Reparada" or "No Reparada"))
            return expediente.Orden.Saldo > 0
                ? $"Siguiente paso: recepción registra C$ {expediente.Orden.Saldo:N2}; luego puede pasar a Entregada."
                : $"Siguiente transición: {estado} → Entregada (recepción).";
        if (estado == "Entregada") return "La orden ya está en su estado final: Entregada.";
        if (rol == RolUsuario.Tecnico && !tecnicoPropietario)
            return "No hay transiciones disponibles: esta orden no está asignada a tu usuario.";
        if (rol == RolUsuario.Administrador && estado == "En Revisión")
            return "Siguiente transición: En Revisión → Pendiente de Confirmación (requiere diagnóstico del técnico asignado).";
        return $"Estado actual: {estado}. No hay transiciones disponibles para tu rol ahora.";
    }

    private void ConfigurarAccion(string texto, IconChar icono)
    {
        _btnAccionEstado.Text = texto;
        _btnAccionEstado.IconChar = icono;
        _btnAccionEstado.Visible = true;
    }

    private void ActualizarCostoAprobado()
    {
        bool editable = cboDecisionCliente.Enabled
            && cboDecisionCliente.SelectedItem?.ToString() == "Aprobada";
        ConfigurarEntrada(numCostoAprobado, editable);
    }

    private static void ConfigurarEntrada(TextBox control, bool editable)
    {
        control.Enabled = true;
        control.ReadOnly = !editable;
        control.TabStop = editable;
        control.BackColor = Color.WhiteSmoke;
    }

    private async Task EjecutarAccionEstadoAsync()
    {
        if (_expediente is null || !_idOrden.HasValue) return;
        try
        {
            string estado = _expediente.Orden.EstadoNombre;
            var orden = _expediente.Orden;
            var actor = Aplicacion.Sesion.UsuarioActual!;
            if (actor.PermisoRol == RolUsuario.Tecnico && estado == "En Revisión" && _expediente.Diagnostico is null)
            {
                if (!DineroTextBoxHelper.TryLeer(numCostoEstimado, out decimal costoEstimado))
                    throw new ValidacionException("Ingrese un costo estimado válido, con máximo dos decimales.");
                await Aplicacion.Diagnosticos.RegistrarAsync(new DiagnosticoSolicitud(
                    orden.IdOrden, txtProblemaEncontrado.Text, txtReparacionPropuesta.Text,
                    costoEstimado, txtObservacionesDiagnostico.Text), TokenFormulario);
            }
            else if (actor.PermisoRol == RolUsuario.Tecnico && estado == "Pendiente de Confirmación"
                && _expediente.Diagnostico is { } diagnosticoExistente && _expediente.Confirmacion is null)
            {
                if (!DineroTextBoxHelper.TryLeer(numCostoEstimado, out decimal costoEstimado))
                    throw new ValidacionException("Ingrese un costo estimado válido, con máximo dos decimales.");
                await Aplicacion.Diagnosticos.CorregirAsync(new CorreccionDiagnosticoSolicitud(
                    diagnosticoExistente.IdDiagnostico, txtProblemaEncontrado.Text, txtReparacionPropuesta.Text,
                    costoEstimado, txtObservacionesDiagnostico.Text), TokenFormulario);
            }
            else if (actor.PermisoRol == RolUsuario.Recepcionista && estado == "Pendiente de Confirmación" && _expediente.Diagnostico is { } diagnostico)
            {
                bool aprobada = cboDecisionCliente.SelectedItem?.ToString() == "Aprobada";
                decimal? costoAprobado = null;
                if (aprobada)
                {
                    if (!DineroTextBoxHelper.TryLeer(numCostoAprobado, out decimal importe))
                        throw new ValidacionException("Ingrese un costo aprobado válido, con máximo dos decimales.");
                    costoAprobado = importe;
                }
                await Aplicacion.Diagnosticos.RegistrarDecisionAsync(new DecisionClienteSolicitud(
                    diagnostico.IdDiagnostico, aprobada ? DecisionCliente.Aprobada : DecisionCliente.Rechazada,
                    costoAprobado, _txtObservacionesConfirmacion.Text), TokenFormulario);
            }
            else if (actor.PermisoRol == RolUsuario.Recepcionista && (estado is "Aprobada" or "Rechazada")
                && _expediente.Confirmacion is { } confirmacion)
            {
                bool aprobada = cboDecisionCliente.SelectedItem?.ToString() == "Aprobada";
                decimal? costoAprobado = null;
                if (aprobada)
                {
                    if (!DineroTextBoxHelper.TryLeer(numCostoAprobado, out decimal importe))
                        throw new ValidacionException("Ingrese un costo aprobado válido, con máximo dos decimales.");
                    costoAprobado = importe;
                }
                await Aplicacion.Diagnosticos.CorregirDecisionAsync(new CorreccionDecisionClienteSolicitud(
                    confirmacion.IdConfirmacion, aprobada ? DecisionCliente.Aprobada : DecisionCliente.Rechazada,
                    costoAprobado, _txtObservacionesConfirmacion.Text), TokenFormulario);
            }
            else if (actor.PermisoRol == RolUsuario.Tecnico && estado == "Aprobada")
            {
                await Aplicacion.Ordenes.IniciarReparacionAsync(orden.IdOrden, TokenFormulario);
            }
            else if (actor.PermisoRol == RolUsuario.Tecnico && estado == "En Reparación")
            {
                bool reparada = cboResultadoReparacion.SelectedItem?.ToString() == "Reparada";
                await Aplicacion.Ordenes.FinalizarReparacionAsync(orden.IdOrden, reparada,
                    txtObservacionesTrabajo.Text, TokenFormulario);
            }
            else if (actor.PermisoRol == RolUsuario.Recepcionista && estado is "Reparada" or "No Reparada" or "Rechazada")
            {
                await RegistrarEntregaAsync();
                return;
            }
            await CargarDatosAsync();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            MessageBox.Show(this, ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async Task RegistrarEntregaAsync()
    {
        if (_expediente is null) return;
        try
        {
            string? observacion = SolicitarTexto("Entrega del equipo", "Observación de entrega (opcional):", false);
            if (observacion is null) return;
            await Aplicacion.Ordenes.EntregarAsync(_expediente.Orden.IdOrden, observacion, TokenFormulario);
            await CargarDatosAsync();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            MessageBox.Show(this, ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private async Task AgregarServicioAsync()
    {
        if (_expediente is null) return;
        try
        {
            var servicios = await Aplicacion.Servicios.ConsultarAsync(soloActivos: true, ct: TokenFormulario);
            var seleccionado = ElegirCantidad("Agregar servicio", servicios,
                x => $"{x.Nombre} · precio catálogo C$ {x.PrecioBase:N2}", x => x.PrecioBase);
            if (seleccionado is null) return;
            ValidarPresupuesto(seleccionado.Value.Cantidad, seleccionado.Value.Precio);
            await Aplicacion.Servicios.RegistrarEnOrdenAsync(new ServicioOrdenSolicitud(
                _expediente.Orden.IdOrden, seleccionado.Value.Item.IdServicio, seleccionado.Value.Cantidad,
                seleccionado.Value.Precio), TokenFormulario);
            await CargarDatosAsync();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        { MessageBox.Show(this, ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private async Task AgregarRepuestoAsync()
    {
        if (_expediente is null) return;
        try
        {
            var repuestos = (await Aplicacion.Repuestos.ConsultarAsync(soloActivos: true, ct: TokenFormulario))
                .Where(x => x.Stock > 0).ToList();
            var seleccionado = ElegirCantidad("Agregar repuesto", repuestos,
                x => $"{x.Nombre} · disponible {x.Stock} · catálogo C$ {x.PrecioVenta:N2}", x => x.PrecioVenta);
            if (seleccionado is null) return;
            if (seleccionado.Value.Cantidad > seleccionado.Value.Item.Stock)
                throw new ValidacionException("La cantidad supera las existencias disponibles.");
            ValidarPresupuesto(seleccionado.Value.Cantidad, seleccionado.Value.Precio);
            await Aplicacion.Repuestos.ConsumirAsync(new ConsumoRepuestoSolicitud(
                _expediente.Orden.IdOrden, seleccionado.Value.Item.IdRepuesto, seleccionado.Value.Cantidad,
                seleccionado.Value.Precio), TokenFormulario);
            await CargarDatosAsync();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        { MessageBox.Show(this, ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void ValidarPresupuesto(int cantidad, decimal precioUnitario)
    {
        decimal autorizado = _expediente?.Confirmacion?.CostoAprobado
            ?? throw new ValidacionException("La orden todavía no tiene un costo autorizado por recepción.");
        decimal totalActual = _expediente.Orden.Total;
        decimal nuevoTotal = totalActual + cantidad * precioUnitario;
        if (nuevoTotal > autorizado)
        {
            decimal disponible = Math.Max(0, autorizado - totalActual);
            throw new ValidacionException(
                $"El concepto supera el presupuesto autorizado. Total actual: C$ {totalActual:N2}; " +
                $"disponible: C$ {disponible:N2}; total con este concepto: C$ {nuevoTotal:N2}; " +
                $"autorizado: C$ {autorizado:N2}. Solicite a recepción corregir la aprobación si el cliente autorizó un monto mayor.");
        }
    }

    private async Task RegistrarPagoAsync()
    {
        if (_expediente is null) return;
        using var formulario = new FrmRegistroPago(Aplicacion, _expediente.Orden.IdOrden);
        if (formulario.ShowDialog(this) == DialogResult.OK) await CargarDatosAsync();
    }

    private static (T Item, int Cantidad, decimal Precio)? ElegirCantidad<T>(
        string titulo, IReadOnlyList<T> registros, Func<T, string> texto, Func<T, decimal> precioPredeterminado)
        where T : class
    {
        if (registros.Count == 0)
        {
            MessageBox.Show("No hay registros activos disponibles.", titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }
        using var dialogo = new Form { Text = titulo, StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog, MinimizeBox = false, MaximizeBox = false,
            ClientSize = new Size(480, 220) };
        var etiqueta = new Label { Text = "Seleccione el concepto:", AutoSize = true, Location = new Point(20, 18) };
        var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Location = new Point(20, 44), Width = 440 };
        combo.Items.AddRange(registros.Select(texto).Cast<object>().ToArray());
        combo.SelectedIndex = 0;
        var etiquetaCantidad = new Label { Text = "Cantidad", AutoSize = true, Location = new Point(20, 83) };
        var cantidad = new NumericUpDown { Minimum = 1, Maximum = 999, Value = 1, Location = new Point(20, 105), Width = 120 };
        var etiquetaPrecio = new Label { Text = "Precio unitario (C$)", AutoSize = true, Location = new Point(160, 83) };
        var precio = new TextBox { TextAlign = HorizontalAlignment.Right, Text = DineroTextBoxHelper.Formatear(precioPredeterminado(registros[0])), Location = new Point(160, 105), Width = 300 };
        void ActualizarPrecio() => precio.Text = DineroTextBoxHelper.Formatear(precioPredeterminado(registros[combo.SelectedIndex]));
        combo.SelectedIndexChanged += (_, _) => { if (combo.SelectedIndex >= 0) ActualizarPrecio(); };
        var aceptar = new Button { Text = "Agregar", Location = new Point(290, 164), Width = 80 };
        aceptar.Click += (_, _) =>
        {
            if (!DineroTextBoxHelper.TryLeer(precio, out _))
            {
                MessageBox.Show(dialogo, "Ingrese un precio válido, con máximo dos decimales.", titulo,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                precio.Focus();
                return;
            }
            dialogo.DialogResult = DialogResult.OK;
            dialogo.Close();
        };
        var cancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Location = new Point(380, 164), Width = 80 };
        dialogo.Controls.AddRange(new Control[] { etiqueta, combo, etiquetaCantidad, cantidad, etiquetaPrecio, precio, aceptar, cancelar });
        dialogo.AcceptButton = aceptar; dialogo.CancelButton = cancelar;
        if (dialogo.ShowDialog() != DialogResult.OK || !DineroTextBoxHelper.TryLeer(precio, out decimal precioUnitario)) return null;
        return ((T)registros[combo.SelectedIndex], (int)cantidad.Value, precioUnitario);
    }

    private string? SolicitarTexto(string titulo, string etiquetaTexto, bool requerido)
    {
        using var dialogo = new Form { Text = titulo, StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog, MinimizeBox = false, MaximizeBox = false,
            ClientSize = new Size(500, 190) };
        var etiqueta = new Label { Text = etiquetaTexto, AutoSize = true, Location = new Point(18, 16) };
        var texto = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical,
            MaxLength = 500, Location = new Point(18, 42), Size = new Size(464, 84) };
        var aceptar = new Button { Text = "Aceptar", DialogResult = DialogResult.OK, Location = new Point(305, 145), Width = 82 };
        var cancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Location = new Point(396, 145), Width = 86 };
        dialogo.Controls.AddRange(new Control[] { etiqueta, texto, aceptar, cancelar });
        dialogo.AcceptButton = aceptar; dialogo.CancelButton = cancelar;
        if (dialogo.ShowDialog(this) != DialogResult.OK) return null;
        if (requerido && string.IsNullOrWhiteSpace(texto.Text))
        {
            MessageBox.Show(this, "Este campo es obligatorio.", titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
        return texto.Text;
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
