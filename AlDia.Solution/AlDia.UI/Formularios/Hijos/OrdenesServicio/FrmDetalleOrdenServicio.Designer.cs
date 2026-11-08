namespace AlDia.UI.Formularios.Hijos.OrdenesServicio;

partial class FrmDetalleOrdenServicio
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
        tablaExpediente = new TableLayoutPanel();
        tablaResumen = new TableLayoutPanel();
        paneltxtOrdenResumen = new Panel();
        lbltxtOrdenResumen = new Label();
        txtOrdenResumen = new TextBox();
        paneltxtClienteResumen = new Panel();
        lbltxtClienteResumen = new Label();
        txtClienteResumen = new TextBox();
        paneltxtEquipoResumen = new Panel();
        lbltxtEquipoResumen = new Label();
        txtEquipoResumen = new TextBox();
        paneltxtEstadoResumen = new Panel();
        lbltxtEstadoResumen = new Label();
        txtEstadoResumen = new TextBox();
        paneltxtTecnicoResumen = new Panel();
        lbltxtTecnicoResumen = new Label();
        txtTecnicoResumen = new TextBox();
        paneltxtSaldoResumen = new Panel();
        lbltxtSaldoResumen = new Label();
        txtSaldoResumen = new TextBox();
        tabsExpediente = new TabControl();
        tabRecepcion = new TabPage();
        paneltxtFechaRecepcionDetalle = new Panel();
        lbltxtFechaRecepcionDetalle = new Label();
        txtFechaRecepcionDetalle = new TextBox();
        paneltxtRecepcionistaDetalle = new Panel();
        lbltxtRecepcionistaDetalle = new Label();
        txtRecepcionistaDetalle = new TextBox();
        paneltxtProblemaRecepcionDetalle = new Panel();
        lbltxtProblemaRecepcionDetalle = new Label();
        txtProblemaRecepcionDetalle = new TextBox();
        paneltxtAccesoriosDetalle = new Panel();
        lbltxtAccesoriosDetalle = new Label();
        txtAccesoriosDetalle = new TextBox();
        paneltxtObservacionesDetalle = new Panel();
        lbltxtObservacionesDetalle = new Label();
        txtObservacionesDetalle = new TextBox();
        tabDiagnostico = new TabPage();
        panelcboTecnicoDiagnostico = new Panel();
        lblcboTecnicoDiagnostico = new Label();
        cboTecnicoDiagnostico = new ComboBox();
        paneltxtFechaDiagnostico = new Panel();
        lbltxtFechaDiagnostico = new Label();
        txtFechaDiagnostico = new TextBox();
        paneltxtProblemaEncontrado = new Panel();
        lbltxtProblemaEncontrado = new Label();
        txtProblemaEncontrado = new TextBox();
        paneltxtReparacionPropuesta = new Panel();
        lbltxtReparacionPropuesta = new Label();
        txtReparacionPropuesta = new TextBox();
        panelnumCostoEstimado = new Panel();
        lblnumCostoEstimado = new Label();
        numCostoEstimado = new TextBox();
        panelcboDecisionCliente = new Panel();
        lblcboDecisionCliente = new Label();
        cboDecisionCliente = new ComboBox();
        panelnumCostoAprobado = new Panel();
        lblnumCostoAprobado = new Label();
        numCostoAprobado = new TextBox();
        paneltxtFechaConfirmacion = new Panel();
        lbltxtFechaConfirmacion = new Label();
        txtFechaConfirmacion = new TextBox();
        paneltxtObservacionesDiagnostico = new Panel();
        lbltxtObservacionesDiagnostico = new Label();
        txtObservacionesDiagnostico = new TextBox();
        tabTrabajos = new TabPage();
        lblServiciosOrden = new Label();
        btnAgregarServicio = new FontAwesome.Sharp.IconButton();
        dgvServiciosOrden = new DataGridView();
        dgvServiciosOrdenCol0 = new DataGridViewTextBoxColumn();
        dgvServiciosOrdenCol1 = new DataGridViewTextBoxColumn();
        dgvServiciosOrdenCol2 = new DataGridViewTextBoxColumn();
        dgvServiciosOrdenCol3 = new DataGridViewTextBoxColumn();
        lblServiciosVacios = new Label();
        lblRepuestosOrden = new Label();
        btnAgregarRepuesto = new FontAwesome.Sharp.IconButton();
        dgvRepuestosOrden = new DataGridView();
        dgvRepuestosOrdenCol0 = new DataGridViewTextBoxColumn();
        dgvRepuestosOrdenCol1 = new DataGridViewTextBoxColumn();
        dgvRepuestosOrdenCol2 = new DataGridViewTextBoxColumn();
        dgvRepuestosOrdenCol3 = new DataGridViewTextBoxColumn();
        lblRepuestosVacios = new Label();
        paneltxtTotalTrabajos = new Panel();
        lbltxtTotalTrabajos = new Label();
        txtTotalTrabajos = new TextBox();
        panelcboResultadoReparacion = new Panel();
        lblcboResultadoReparacion = new Label();
        cboResultadoReparacion = new ComboBox();
        paneltxtObservacionesTrabajo = new Panel();
        lbltxtObservacionesTrabajo = new Label();
        txtObservacionesTrabajo = new TextBox();
        tabPagos = new TabPage();
        lblCobrosOrden = new Label();
        btnRegistrarPagoOrden = new FontAwesome.Sharp.IconButton();
        dgvPagosOrden = new DataGridView();
        dgvPagosOrdenCol0 = new DataGridViewTextBoxColumn();
        dgvPagosOrdenCol1 = new DataGridViewTextBoxColumn();
        dgvPagosOrdenCol2 = new DataGridViewTextBoxColumn();
        dgvPagosOrdenCol3 = new DataGridViewTextBoxColumn();
        dgvPagosOrdenCol4 = new DataGridViewTextBoxColumn();
        lblPagosVacios = new Label();
        paneltxtTotalPagos = new Panel();
        lbltxtTotalPagos = new Label();
        txtTotalPagos = new TextBox();
        paneltxtPagadoOrden = new Panel();
        lbltxtPagadoOrden = new Label();
        txtPagadoOrden = new TextBox();
        paneltxtSaldoPagos = new Panel();
        lbltxtSaldoPagos = new Label();
        txtSaldoPagos = new TextBox();
        tabHistorial = new TabPage();
        lblHistorialTitulo = new Label();
        lblHistorialDescripcion = new Label();
        dgvHistorialOrden = new DataGridView();
        dgvHistorialOrdenCol0 = new DataGridViewTextBoxColumn();
        dgvHistorialOrdenCol1 = new DataGridViewTextBoxColumn();
        dgvHistorialOrdenCol2 = new DataGridViewTextBoxColumn();
        dgvHistorialOrdenCol3 = new DataGridViewTextBoxColumn();
        dgvHistorialOrdenCol4 = new DataGridViewTextBoxColumn();
        lblHistorialVacio = new Label();
        ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
        panelContenido.SuspendLayout();
        panelCabecera.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)iconModulo).BeginInit();
        panelCuerpo.SuspendLayout();
        panelPie.SuspendLayout();
        tablaExpediente.SuspendLayout();
        tablaResumen.SuspendLayout();
        paneltxtOrdenResumen.SuspendLayout();
        paneltxtClienteResumen.SuspendLayout();
        paneltxtEquipoResumen.SuspendLayout();
        paneltxtEstadoResumen.SuspendLayout();
        paneltxtTecnicoResumen.SuspendLayout();
        paneltxtSaldoResumen.SuspendLayout();
        tabsExpediente.SuspendLayout();
        tabRecepcion.SuspendLayout();
        paneltxtFechaRecepcionDetalle.SuspendLayout();
        paneltxtRecepcionistaDetalle.SuspendLayout();
        paneltxtProblemaRecepcionDetalle.SuspendLayout();
        paneltxtAccesoriosDetalle.SuspendLayout();
        paneltxtObservacionesDetalle.SuspendLayout();
        tabDiagnostico.SuspendLayout();
        panelcboTecnicoDiagnostico.SuspendLayout();
        paneltxtFechaDiagnostico.SuspendLayout();
        paneltxtProblemaEncontrado.SuspendLayout();
        paneltxtReparacionPropuesta.SuspendLayout();
        panelnumCostoEstimado.SuspendLayout();
        panelcboDecisionCliente.SuspendLayout();
        panelnumCostoAprobado.SuspendLayout();
        paneltxtFechaConfirmacion.SuspendLayout();
        paneltxtObservacionesDiagnostico.SuspendLayout();
        tabTrabajos.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvServiciosOrden).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvRepuestosOrden).BeginInit();
        paneltxtTotalTrabajos.SuspendLayout();
        panelcboResultadoReparacion.SuspendLayout();
        paneltxtObservacionesTrabajo.SuspendLayout();
        tabPagos.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPagosOrden).BeginInit();
        paneltxtTotalPagos.SuspendLayout();
        paneltxtPagadoOrden.SuspendLayout();
        paneltxtSaldoPagos.SuspendLayout();
        tabHistorial.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHistorialOrden).BeginInit();
        SuspendLayout();
        // 
        // panelContenido
        // 
        panelContenido.Size = new Size(1040, 860);
        // 
        // panelCabecera
        // 
        panelCabecera.Size = new Size(1040, 139);
        // 
        // iconModulo
        // 
        iconModulo.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
        // 
        // lblTitulo
        // 
        lblTitulo.Text = "Expediente de la orden";
        // 
        // lblDescripcion
        // 
        lblDescripcion.Text = "Recepción, diagnóstico, trabajos realizados, pagos e historial del equipo.";
        // 
        // btnCerrar
        // 
        btnCerrar.FlatAppearance.BorderSize = 0;
        btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCerrar.Location = new Point(859, 4);
        // 
        // panelCuerpo
        // 
        panelCuerpo.Controls.Add(tablaExpediente);
        panelCuerpo.Size = new Size(1040, 614);
        // 
        // panelPie
        // 
        panelPie.Location = new Point(0, 753);
        panelPie.Size = new Size(1040, 107);
        // 
        // lineaPie
        // 
        lineaPie.Size = new Size(1040, 1);
        // 
        // lblObligatorios
        // 
        lblObligatorios.Size = new Size(262, 32);
        lblObligatorios.Text = "Información de la orden de servicio";
        lblObligatorios.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnCancelar
        // 
        btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnCancelar.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 236, 243);
        btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCancelar.Location = new Point(857, 25);
        // 
        // btnGuardar
        // 
        btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnGuardar.FlatAppearance.BorderSize = 0;
        btnGuardar.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
        btnGuardar.Location = new Point(774, 25);
        btnGuardar.Text = "Guardar cambios";
        // 
        // tablaExpediente
        // 
        tablaExpediente.ColumnCount = 1;
        tablaExpediente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tablaExpediente.Controls.Add(tablaResumen, 0, 0);
        tablaExpediente.Controls.Add(tabsExpediente, 0, 1);
        tablaExpediente.Dock = DockStyle.Top;
        tablaExpediente.Location = new Point(32, 32);
        tablaExpediente.Margin = new Padding(0);
        tablaExpediente.Name = "tablaExpediente";
        tablaExpediente.RowCount = 2;
        tablaExpediente.RowStyles.Add(new RowStyle(SizeType.Absolute, 170F));
        tablaExpediente.RowStyles.Add(new RowStyle(SizeType.Absolute, 502F));
        tablaExpediente.Size = new Size(955, 672);
        tablaExpediente.TabIndex = 0;
        // 
        // tablaResumen
        // 
        tablaResumen.ColumnCount = 3;
        tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        tablaResumen.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tablaResumen.Controls.Add(paneltxtOrdenResumen, 0, 0);
        tablaResumen.Controls.Add(paneltxtClienteResumen, 1, 0);
        tablaResumen.Controls.Add(paneltxtEquipoResumen, 2, 0);
        tablaResumen.Controls.Add(paneltxtEstadoResumen, 0, 1);
        tablaResumen.Controls.Add(paneltxtTecnicoResumen, 1, 1);
        tablaResumen.Controls.Add(paneltxtSaldoResumen, 2, 1);
        tablaResumen.Dock = DockStyle.Fill;
        tablaResumen.Location = new Point(0, 0);
        tablaResumen.Margin = new Padding(0, 0, 0, 10);
        tablaResumen.Name = "tablaResumen";
        tablaResumen.RowCount = 2;
        tablaResumen.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tablaResumen.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tablaResumen.Size = new Size(955, 160);
        tablaResumen.TabIndex = 0;
        // 
        // paneltxtOrdenResumen
        // 
        paneltxtOrdenResumen.Controls.Add(lbltxtOrdenResumen);
        paneltxtOrdenResumen.Controls.Add(txtOrdenResumen);
        paneltxtOrdenResumen.Dock = DockStyle.Fill;
        paneltxtOrdenResumen.Location = new Point(0, 0);
        paneltxtOrdenResumen.Margin = new Padding(0, 0, 16, 0);
        paneltxtOrdenResumen.Name = "paneltxtOrdenResumen";
        paneltxtOrdenResumen.Size = new Size(302, 80);
        paneltxtOrdenResumen.TabIndex = 0;
        // 
        // lbltxtOrdenResumen
        // 
        lbltxtOrdenResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtOrdenResumen.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtOrdenResumen.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtOrdenResumen.Location = new Point(0, 4);
        lbltxtOrdenResumen.Name = "lbltxtOrdenResumen";
        lbltxtOrdenResumen.Size = new Size(302, 22);
        lbltxtOrdenResumen.TabIndex = 0;
        lbltxtOrdenResumen.Text = "N.º de orden";
        // 
        // txtOrdenResumen
        // 
        txtOrdenResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtOrdenResumen.BackColor = Color.FromArgb(246, 248, 251);
        txtOrdenResumen.BorderStyle = BorderStyle.FixedSingle;
        txtOrdenResumen.Font = new Font("Segoe UI", 10F);
        txtOrdenResumen.ForeColor = Color.FromArgb(20, 43, 78);
        txtOrdenResumen.Location = new Point(0, 32);
        txtOrdenResumen.MaxLength = 100;
        txtOrdenResumen.Name = "txtOrdenResumen";
        txtOrdenResumen.PlaceholderText = "—";
        txtOrdenResumen.ReadOnly = true;
        txtOrdenResumen.Size = new Size(302, 30);
        txtOrdenResumen.TabIndex = 0;
        txtOrdenResumen.TabStop = false;
        // 
        // paneltxtClienteResumen
        // 
        paneltxtClienteResumen.Controls.Add(lbltxtClienteResumen);
        paneltxtClienteResumen.Controls.Add(txtClienteResumen);
        paneltxtClienteResumen.Dock = DockStyle.Fill;
        paneltxtClienteResumen.Location = new Point(318, 0);
        paneltxtClienteResumen.Margin = new Padding(0, 0, 16, 0);
        paneltxtClienteResumen.Name = "paneltxtClienteResumen";
        paneltxtClienteResumen.Size = new Size(302, 80);
        paneltxtClienteResumen.TabIndex = 1;
        // 
        // lbltxtClienteResumen
        // 
        lbltxtClienteResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtClienteResumen.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtClienteResumen.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtClienteResumen.Location = new Point(0, 4);
        lbltxtClienteResumen.Name = "lbltxtClienteResumen";
        lbltxtClienteResumen.Size = new Size(302, 22);
        lbltxtClienteResumen.TabIndex = 0;
        lbltxtClienteResumen.Text = "Cliente";
        // 
        // txtClienteResumen
        // 
        txtClienteResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtClienteResumen.BackColor = Color.FromArgb(246, 248, 251);
        txtClienteResumen.BorderStyle = BorderStyle.FixedSingle;
        txtClienteResumen.Font = new Font("Segoe UI", 10F);
        txtClienteResumen.ForeColor = Color.FromArgb(20, 43, 78);
        txtClienteResumen.Location = new Point(0, 32);
        txtClienteResumen.MaxLength = 100;
        txtClienteResumen.Name = "txtClienteResumen";
        txtClienteResumen.PlaceholderText = "—";
        txtClienteResumen.ReadOnly = true;
        txtClienteResumen.Size = new Size(302, 30);
        txtClienteResumen.TabIndex = 0;
        txtClienteResumen.TabStop = false;
        // 
        // paneltxtEquipoResumen
        // 
        paneltxtEquipoResumen.Controls.Add(lbltxtEquipoResumen);
        paneltxtEquipoResumen.Controls.Add(txtEquipoResumen);
        paneltxtEquipoResumen.Dock = DockStyle.Fill;
        paneltxtEquipoResumen.Location = new Point(636, 0);
        paneltxtEquipoResumen.Margin = new Padding(0);
        paneltxtEquipoResumen.Name = "paneltxtEquipoResumen";
        paneltxtEquipoResumen.Size = new Size(319, 80);
        paneltxtEquipoResumen.TabIndex = 2;
        // 
        // lbltxtEquipoResumen
        // 
        lbltxtEquipoResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtEquipoResumen.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtEquipoResumen.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtEquipoResumen.Location = new Point(0, 4);
        lbltxtEquipoResumen.Name = "lbltxtEquipoResumen";
        lbltxtEquipoResumen.Size = new Size(319, 22);
        lbltxtEquipoResumen.TabIndex = 0;
        lbltxtEquipoResumen.Text = "Equipo";
        // 
        // txtEquipoResumen
        // 
        txtEquipoResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtEquipoResumen.BackColor = Color.FromArgb(246, 248, 251);
        txtEquipoResumen.BorderStyle = BorderStyle.FixedSingle;
        txtEquipoResumen.Font = new Font("Segoe UI", 10F);
        txtEquipoResumen.ForeColor = Color.FromArgb(20, 43, 78);
        txtEquipoResumen.Location = new Point(0, 32);
        txtEquipoResumen.MaxLength = 100;
        txtEquipoResumen.Name = "txtEquipoResumen";
        txtEquipoResumen.PlaceholderText = "—";
        txtEquipoResumen.ReadOnly = true;
        txtEquipoResumen.Size = new Size(319, 30);
        txtEquipoResumen.TabIndex = 0;
        txtEquipoResumen.TabStop = false;
        // 
        // paneltxtEstadoResumen
        // 
        paneltxtEstadoResumen.Controls.Add(lbltxtEstadoResumen);
        paneltxtEstadoResumen.Controls.Add(txtEstadoResumen);
        paneltxtEstadoResumen.Dock = DockStyle.Fill;
        paneltxtEstadoResumen.Location = new Point(0, 80);
        paneltxtEstadoResumen.Margin = new Padding(0, 0, 16, 0);
        paneltxtEstadoResumen.Name = "paneltxtEstadoResumen";
        paneltxtEstadoResumen.Size = new Size(302, 80);
        paneltxtEstadoResumen.TabIndex = 3;
        // 
        // lbltxtEstadoResumen
        // 
        lbltxtEstadoResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtEstadoResumen.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtEstadoResumen.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtEstadoResumen.Location = new Point(0, 4);
        lbltxtEstadoResumen.Name = "lbltxtEstadoResumen";
        lbltxtEstadoResumen.Size = new Size(302, 22);
        lbltxtEstadoResumen.TabIndex = 0;
        lbltxtEstadoResumen.Text = "Estado de la orden";
        // 
        // txtEstadoResumen
        // 
        txtEstadoResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtEstadoResumen.BackColor = Color.FromArgb(246, 248, 251);
        txtEstadoResumen.BorderStyle = BorderStyle.FixedSingle;
        txtEstadoResumen.Font = new Font("Segoe UI", 10F);
        txtEstadoResumen.ForeColor = Color.FromArgb(20, 43, 78);
        txtEstadoResumen.Location = new Point(0, 32);
        txtEstadoResumen.MaxLength = 100;
        txtEstadoResumen.Name = "txtEstadoResumen";
        txtEstadoResumen.PlaceholderText = "—";
        txtEstadoResumen.ReadOnly = true;
        txtEstadoResumen.Size = new Size(302, 30);
        txtEstadoResumen.TabIndex = 0;
        txtEstadoResumen.TabStop = false;
        // 
        // paneltxtTecnicoResumen
        // 
        paneltxtTecnicoResumen.Controls.Add(lbltxtTecnicoResumen);
        paneltxtTecnicoResumen.Controls.Add(txtTecnicoResumen);
        paneltxtTecnicoResumen.Dock = DockStyle.Fill;
        paneltxtTecnicoResumen.Location = new Point(318, 80);
        paneltxtTecnicoResumen.Margin = new Padding(0, 0, 16, 0);
        paneltxtTecnicoResumen.Name = "paneltxtTecnicoResumen";
        paneltxtTecnicoResumen.Size = new Size(302, 80);
        paneltxtTecnicoResumen.TabIndex = 4;
        // 
        // lbltxtTecnicoResumen
        // 
        lbltxtTecnicoResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtTecnicoResumen.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtTecnicoResumen.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtTecnicoResumen.Location = new Point(0, 4);
        lbltxtTecnicoResumen.Name = "lbltxtTecnicoResumen";
        lbltxtTecnicoResumen.Size = new Size(302, 22);
        lbltxtTecnicoResumen.TabIndex = 0;
        lbltxtTecnicoResumen.Text = "Técnico responsable";
        // 
        // txtTecnicoResumen
        // 
        txtTecnicoResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTecnicoResumen.BackColor = Color.FromArgb(246, 248, 251);
        txtTecnicoResumen.BorderStyle = BorderStyle.FixedSingle;
        txtTecnicoResumen.Font = new Font("Segoe UI", 10F);
        txtTecnicoResumen.ForeColor = Color.FromArgb(20, 43, 78);
        txtTecnicoResumen.Location = new Point(0, 32);
        txtTecnicoResumen.MaxLength = 100;
        txtTecnicoResumen.Name = "txtTecnicoResumen";
        txtTecnicoResumen.PlaceholderText = "—";
        txtTecnicoResumen.ReadOnly = true;
        txtTecnicoResumen.Size = new Size(302, 30);
        txtTecnicoResumen.TabIndex = 0;
        txtTecnicoResumen.TabStop = false;
        // 
        // paneltxtSaldoResumen
        // 
        paneltxtSaldoResumen.Controls.Add(lbltxtSaldoResumen);
        paneltxtSaldoResumen.Controls.Add(txtSaldoResumen);
        paneltxtSaldoResumen.Dock = DockStyle.Fill;
        paneltxtSaldoResumen.Location = new Point(636, 80);
        paneltxtSaldoResumen.Margin = new Padding(0);
        paneltxtSaldoResumen.Name = "paneltxtSaldoResumen";
        paneltxtSaldoResumen.Size = new Size(319, 80);
        paneltxtSaldoResumen.TabIndex = 5;
        // 
        // lbltxtSaldoResumen
        // 
        lbltxtSaldoResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtSaldoResumen.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtSaldoResumen.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtSaldoResumen.Location = new Point(0, 4);
        lbltxtSaldoResumen.Name = "lbltxtSaldoResumen";
        lbltxtSaldoResumen.Size = new Size(319, 22);
        lbltxtSaldoResumen.TabIndex = 0;
        lbltxtSaldoResumen.Text = "Saldo pendiente (C$)";
        // 
        // txtSaldoResumen
        // 
        txtSaldoResumen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSaldoResumen.BackColor = Color.FromArgb(246, 248, 251);
        txtSaldoResumen.BorderStyle = BorderStyle.FixedSingle;
        txtSaldoResumen.Font = new Font("Segoe UI", 10F);
        txtSaldoResumen.ForeColor = Color.FromArgb(20, 43, 78);
        txtSaldoResumen.Location = new Point(0, 32);
        txtSaldoResumen.MaxLength = 100;
        txtSaldoResumen.Name = "txtSaldoResumen";
        txtSaldoResumen.PlaceholderText = "—";
        txtSaldoResumen.ReadOnly = true;
        txtSaldoResumen.Size = new Size(319, 30);
        txtSaldoResumen.TabIndex = 0;
        txtSaldoResumen.TabStop = false;
        // 
        // tabsExpediente
        // 
        tabsExpediente.Controls.Add(tabRecepcion);
        tabsExpediente.Controls.Add(tabDiagnostico);
        tabsExpediente.Controls.Add(tabTrabajos);
        tabsExpediente.Controls.Add(tabPagos);
        tabsExpediente.Controls.Add(tabHistorial);
        tabsExpediente.Dock = DockStyle.Fill;
        tabsExpediente.Font = new Font("Segoe UI", 10F);
        tabsExpediente.ItemSize = new Size(140, 36);
        tabsExpediente.Location = new Point(0, 170);
        tabsExpediente.Margin = new Padding(0);
        tabsExpediente.Name = "tabsExpediente";
        tabsExpediente.SelectedIndex = 0;
        tabsExpediente.Size = new Size(955, 502);
        tabsExpediente.TabIndex = 1;
        // 
        // tabRecepcion
        // 
        tabRecepcion.AutoScroll = true;
        tabRecepcion.BackColor = Color.White;
        tabRecepcion.Controls.Add(paneltxtFechaRecepcionDetalle);
        tabRecepcion.Controls.Add(paneltxtRecepcionistaDetalle);
        tabRecepcion.Controls.Add(paneltxtProblemaRecepcionDetalle);
        tabRecepcion.Controls.Add(paneltxtAccesoriosDetalle);
        tabRecepcion.Controls.Add(paneltxtObservacionesDetalle);
        tabRecepcion.Location = new Point(4, 40);
        tabRecepcion.Name = "tabRecepcion";
        tabRecepcion.Padding = new Padding(18);
        tabRecepcion.Size = new Size(947, 458);
        tabRecepcion.TabIndex = 0;
        tabRecepcion.Text = "Recepción";
        // 
        // paneltxtFechaRecepcionDetalle
        // 
        paneltxtFechaRecepcionDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtFechaRecepcionDetalle.Controls.Add(lbltxtFechaRecepcionDetalle);
        paneltxtFechaRecepcionDetalle.Controls.Add(txtFechaRecepcionDetalle);
        paneltxtFechaRecepcionDetalle.Location = new Point(18, 18);
        paneltxtFechaRecepcionDetalle.Name = "paneltxtFechaRecepcionDetalle";
        paneltxtFechaRecepcionDetalle.Size = new Size(413, 80);
        paneltxtFechaRecepcionDetalle.TabIndex = 0;
        // 
        // lbltxtFechaRecepcionDetalle
        // 
        lbltxtFechaRecepcionDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtFechaRecepcionDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtFechaRecepcionDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtFechaRecepcionDetalle.Location = new Point(0, 4);
        lbltxtFechaRecepcionDetalle.Name = "lbltxtFechaRecepcionDetalle";
        lbltxtFechaRecepcionDetalle.Size = new Size(413, 22);
        lbltxtFechaRecepcionDetalle.TabIndex = 0;
        lbltxtFechaRecepcionDetalle.Text = "Fecha de recepción";
        // 
        // txtFechaRecepcionDetalle
        // 
        txtFechaRecepcionDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtFechaRecepcionDetalle.BackColor = Color.FromArgb(246, 248, 251);
        txtFechaRecepcionDetalle.BorderStyle = BorderStyle.FixedSingle;
        txtFechaRecepcionDetalle.Font = new Font("Segoe UI", 10F);
        txtFechaRecepcionDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        txtFechaRecepcionDetalle.Location = new Point(0, 32);
        txtFechaRecepcionDetalle.MaxLength = 100;
        txtFechaRecepcionDetalle.Name = "txtFechaRecepcionDetalle";
        txtFechaRecepcionDetalle.PlaceholderText = "—";
        txtFechaRecepcionDetalle.ReadOnly = true;
        txtFechaRecepcionDetalle.Size = new Size(413, 30);
        txtFechaRecepcionDetalle.TabIndex = 0;
        txtFechaRecepcionDetalle.TabStop = false;
        // 
        // paneltxtRecepcionistaDetalle
        // 
        paneltxtRecepcionistaDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtRecepcionistaDetalle.Controls.Add(lbltxtRecepcionistaDetalle);
        paneltxtRecepcionistaDetalle.Controls.Add(txtRecepcionistaDetalle);
        paneltxtRecepcionistaDetalle.Location = new Point(482, 18);
        paneltxtRecepcionistaDetalle.Name = "paneltxtRecepcionistaDetalle";
        paneltxtRecepcionistaDetalle.Size = new Size(413, 80);
        paneltxtRecepcionistaDetalle.TabIndex = 1;
        // 
        // lbltxtRecepcionistaDetalle
        // 
        lbltxtRecepcionistaDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtRecepcionistaDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtRecepcionistaDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtRecepcionistaDetalle.Location = new Point(0, 4);
        lbltxtRecepcionistaDetalle.Name = "lbltxtRecepcionistaDetalle";
        lbltxtRecepcionistaDetalle.Size = new Size(413, 22);
        lbltxtRecepcionistaDetalle.TabIndex = 0;
        lbltxtRecepcionistaDetalle.Text = "Recibido por";
        // 
        // txtRecepcionistaDetalle
        // 
        txtRecepcionistaDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtRecepcionistaDetalle.BackColor = Color.FromArgb(246, 248, 251);
        txtRecepcionistaDetalle.BorderStyle = BorderStyle.FixedSingle;
        txtRecepcionistaDetalle.Font = new Font("Segoe UI", 10F);
        txtRecepcionistaDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        txtRecepcionistaDetalle.Location = new Point(0, 32);
        txtRecepcionistaDetalle.MaxLength = 100;
        txtRecepcionistaDetalle.Name = "txtRecepcionistaDetalle";
        txtRecepcionistaDetalle.PlaceholderText = "—";
        txtRecepcionistaDetalle.ReadOnly = true;
        txtRecepcionistaDetalle.Size = new Size(413, 30);
        txtRecepcionistaDetalle.TabIndex = 0;
        txtRecepcionistaDetalle.TabStop = false;
        // 
        // paneltxtProblemaRecepcionDetalle
        // 
        paneltxtProblemaRecepcionDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtProblemaRecepcionDetalle.Controls.Add(lbltxtProblemaRecepcionDetalle);
        paneltxtProblemaRecepcionDetalle.Controls.Add(txtProblemaRecepcionDetalle);
        paneltxtProblemaRecepcionDetalle.Location = new Point(18, 102);
        paneltxtProblemaRecepcionDetalle.Name = "paneltxtProblemaRecepcionDetalle";
        paneltxtProblemaRecepcionDetalle.Size = new Size(877, 112);
        paneltxtProblemaRecepcionDetalle.TabIndex = 2;
        // 
        // lbltxtProblemaRecepcionDetalle
        // 
        lbltxtProblemaRecepcionDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtProblemaRecepcionDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtProblemaRecepcionDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtProblemaRecepcionDetalle.Location = new Point(0, 4);
        lbltxtProblemaRecepcionDetalle.Name = "lbltxtProblemaRecepcionDetalle";
        lbltxtProblemaRecepcionDetalle.Size = new Size(877, 22);
        lbltxtProblemaRecepcionDetalle.TabIndex = 0;
        lbltxtProblemaRecepcionDetalle.Text = "Problema reportado";
        // 
        // txtProblemaRecepcionDetalle
        // 
        txtProblemaRecepcionDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtProblemaRecepcionDetalle.BackColor = Color.FromArgb(246, 248, 251);
        txtProblemaRecepcionDetalle.BorderStyle = BorderStyle.FixedSingle;
        txtProblemaRecepcionDetalle.Font = new Font("Segoe UI", 10F);
        txtProblemaRecepcionDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        txtProblemaRecepcionDetalle.Location = new Point(0, 32);
        txtProblemaRecepcionDetalle.MaxLength = 500;
        txtProblemaRecepcionDetalle.Multiline = true;
        txtProblemaRecepcionDetalle.Name = "txtProblemaRecepcionDetalle";
        txtProblemaRecepcionDetalle.PlaceholderText = "—";
        txtProblemaRecepcionDetalle.ReadOnly = true;
        txtProblemaRecepcionDetalle.ScrollBars = ScrollBars.Vertical;
        txtProblemaRecepcionDetalle.Size = new Size(877, 72);
        txtProblemaRecepcionDetalle.TabIndex = 0;
        txtProblemaRecepcionDetalle.TabStop = false;
        // 
        // paneltxtAccesoriosDetalle
        // 
        paneltxtAccesoriosDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtAccesoriosDetalle.Controls.Add(lbltxtAccesoriosDetalle);
        paneltxtAccesoriosDetalle.Controls.Add(txtAccesoriosDetalle);
        paneltxtAccesoriosDetalle.Location = new Point(18, 224);
        paneltxtAccesoriosDetalle.Name = "paneltxtAccesoriosDetalle";
        paneltxtAccesoriosDetalle.Size = new Size(877, 100);
        paneltxtAccesoriosDetalle.TabIndex = 3;
        // 
        // lbltxtAccesoriosDetalle
        // 
        lbltxtAccesoriosDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtAccesoriosDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtAccesoriosDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtAccesoriosDetalle.Location = new Point(0, 4);
        lbltxtAccesoriosDetalle.Name = "lbltxtAccesoriosDetalle";
        lbltxtAccesoriosDetalle.Size = new Size(877, 22);
        lbltxtAccesoriosDetalle.TabIndex = 0;
        lbltxtAccesoriosDetalle.Text = "Accesorios recibidos";
        // 
        // txtAccesoriosDetalle
        // 
        txtAccesoriosDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtAccesoriosDetalle.BackColor = Color.FromArgb(246, 248, 251);
        txtAccesoriosDetalle.BorderStyle = BorderStyle.FixedSingle;
        txtAccesoriosDetalle.Font = new Font("Segoe UI", 10F);
        txtAccesoriosDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        txtAccesoriosDetalle.Location = new Point(0, 32);
        txtAccesoriosDetalle.MaxLength = 300;
        txtAccesoriosDetalle.Multiline = true;
        txtAccesoriosDetalle.Name = "txtAccesoriosDetalle";
        txtAccesoriosDetalle.PlaceholderText = "—";
        txtAccesoriosDetalle.ReadOnly = true;
        txtAccesoriosDetalle.ScrollBars = ScrollBars.Vertical;
        txtAccesoriosDetalle.Size = new Size(877, 60);
        txtAccesoriosDetalle.TabIndex = 0;
        txtAccesoriosDetalle.TabStop = false;
        // 
        // paneltxtObservacionesDetalle
        // 
        paneltxtObservacionesDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtObservacionesDetalle.Controls.Add(lbltxtObservacionesDetalle);
        paneltxtObservacionesDetalle.Controls.Add(txtObservacionesDetalle);
        paneltxtObservacionesDetalle.Location = new Point(18, 334);
        paneltxtObservacionesDetalle.Name = "paneltxtObservacionesDetalle";
        paneltxtObservacionesDetalle.Size = new Size(877, 112);
        paneltxtObservacionesDetalle.TabIndex = 4;
        // 
        // lbltxtObservacionesDetalle
        // 
        lbltxtObservacionesDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtObservacionesDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtObservacionesDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtObservacionesDetalle.Location = new Point(0, 4);
        lbltxtObservacionesDetalle.Name = "lbltxtObservacionesDetalle";
        lbltxtObservacionesDetalle.Size = new Size(877, 22);
        lbltxtObservacionesDetalle.TabIndex = 0;
        lbltxtObservacionesDetalle.Text = "Observaciones de recepción";
        // 
        // txtObservacionesDetalle
        // 
        txtObservacionesDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtObservacionesDetalle.BackColor = Color.FromArgb(246, 248, 251);
        txtObservacionesDetalle.BorderStyle = BorderStyle.FixedSingle;
        txtObservacionesDetalle.Font = new Font("Segoe UI", 10F);
        txtObservacionesDetalle.ForeColor = Color.FromArgb(20, 43, 78);
        txtObservacionesDetalle.Location = new Point(0, 32);
        txtObservacionesDetalle.MaxLength = 500;
        txtObservacionesDetalle.Multiline = true;
        txtObservacionesDetalle.Name = "txtObservacionesDetalle";
        txtObservacionesDetalle.PlaceholderText = "—";
        txtObservacionesDetalle.ReadOnly = true;
        txtObservacionesDetalle.ScrollBars = ScrollBars.Vertical;
        txtObservacionesDetalle.Size = new Size(877, 72);
        txtObservacionesDetalle.TabIndex = 0;
        txtObservacionesDetalle.TabStop = false;
        // 
        // tabDiagnostico
        // 
        tabDiagnostico.AutoScroll = true;
        tabDiagnostico.BackColor = Color.White;
        tabDiagnostico.Controls.Add(panelcboTecnicoDiagnostico);
        tabDiagnostico.Controls.Add(paneltxtFechaDiagnostico);
        tabDiagnostico.Controls.Add(paneltxtProblemaEncontrado);
        tabDiagnostico.Controls.Add(paneltxtReparacionPropuesta);
        tabDiagnostico.Controls.Add(panelnumCostoEstimado);
        tabDiagnostico.Controls.Add(panelcboDecisionCliente);
        tabDiagnostico.Controls.Add(panelnumCostoAprobado);
        tabDiagnostico.Controls.Add(paneltxtFechaConfirmacion);
        tabDiagnostico.Controls.Add(paneltxtObservacionesDiagnostico);
        tabDiagnostico.Location = new Point(4, 40);
        tabDiagnostico.Name = "tabDiagnostico";
        tabDiagnostico.Padding = new Padding(18);
        tabDiagnostico.Size = new Size(947, 458);
        tabDiagnostico.TabIndex = 1;
        tabDiagnostico.Text = "Diagnóstico";
        // 
        // panelcboTecnicoDiagnostico
        // 
        panelcboTecnicoDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelcboTecnicoDiagnostico.Controls.Add(lblcboTecnicoDiagnostico);
        panelcboTecnicoDiagnostico.Controls.Add(cboTecnicoDiagnostico);
        panelcboTecnicoDiagnostico.Location = new Point(18, 18);
        panelcboTecnicoDiagnostico.Name = "panelcboTecnicoDiagnostico";
        panelcboTecnicoDiagnostico.Size = new Size(308, 80);
        panelcboTecnicoDiagnostico.TabIndex = 0;
        // 
        // lblcboTecnicoDiagnostico
        // 
        lblcboTecnicoDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblcboTecnicoDiagnostico.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblcboTecnicoDiagnostico.ForeColor = Color.FromArgb(20, 43, 78);
        lblcboTecnicoDiagnostico.Location = new Point(0, 4);
        lblcboTecnicoDiagnostico.Name = "lblcboTecnicoDiagnostico";
        lblcboTecnicoDiagnostico.Size = new Size(308, 22);
        lblcboTecnicoDiagnostico.TabIndex = 0;
        lblcboTecnicoDiagnostico.Text = "Técnico responsable";
        // 
        // cboTecnicoDiagnostico
        // 
        cboTecnicoDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboTecnicoDiagnostico.BackColor = Color.WhiteSmoke;
        cboTecnicoDiagnostico.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTecnicoDiagnostico.FlatStyle = FlatStyle.Flat;
        cboTecnicoDiagnostico.Font = new Font("Segoe UI", 10F);
        cboTecnicoDiagnostico.ForeColor = Color.FromArgb(20, 43, 78);
        cboTecnicoDiagnostico.Items.AddRange(new object[] { "Selecciona un técnico" });
        cboTecnicoDiagnostico.Location = new Point(0, 32);
        cboTecnicoDiagnostico.Name = "cboTecnicoDiagnostico";
        cboTecnicoDiagnostico.Size = new Size(308, 31);
        cboTecnicoDiagnostico.TabIndex = 0;
        // 
        // paneltxtFechaDiagnostico
        // 
        paneltxtFechaDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtFechaDiagnostico.Controls.Add(lbltxtFechaDiagnostico);
        paneltxtFechaDiagnostico.Controls.Add(txtFechaDiagnostico);
        paneltxtFechaDiagnostico.Location = new Point(482, 18);
        paneltxtFechaDiagnostico.Name = "paneltxtFechaDiagnostico";
        paneltxtFechaDiagnostico.Size = new Size(308, 80);
        paneltxtFechaDiagnostico.TabIndex = 1;
        // 
        // lbltxtFechaDiagnostico
        // 
        lbltxtFechaDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtFechaDiagnostico.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtFechaDiagnostico.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtFechaDiagnostico.Location = new Point(0, 4);
        lbltxtFechaDiagnostico.Name = "lbltxtFechaDiagnostico";
        lbltxtFechaDiagnostico.Size = new Size(308, 22);
        lbltxtFechaDiagnostico.TabIndex = 0;
        lbltxtFechaDiagnostico.Text = "Fecha del diagnóstico";
        // 
        // txtFechaDiagnostico
        // 
        txtFechaDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtFechaDiagnostico.BackColor = Color.FromArgb(246, 248, 251);
        txtFechaDiagnostico.BorderStyle = BorderStyle.FixedSingle;
        txtFechaDiagnostico.Font = new Font("Segoe UI", 10F);
        txtFechaDiagnostico.ForeColor = Color.FromArgb(20, 43, 78);
        txtFechaDiagnostico.Location = new Point(0, 32);
        txtFechaDiagnostico.MaxLength = 100;
        txtFechaDiagnostico.Name = "txtFechaDiagnostico";
        txtFechaDiagnostico.PlaceholderText = "—";
        txtFechaDiagnostico.ReadOnly = true;
        txtFechaDiagnostico.Size = new Size(308, 30);
        txtFechaDiagnostico.TabIndex = 0;
        txtFechaDiagnostico.TabStop = false;
        // 
        // paneltxtProblemaEncontrado
        // 
        paneltxtProblemaEncontrado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtProblemaEncontrado.Controls.Add(lbltxtProblemaEncontrado);
        paneltxtProblemaEncontrado.Controls.Add(txtProblemaEncontrado);
        paneltxtProblemaEncontrado.Location = new Point(18, 104);
        paneltxtProblemaEncontrado.Name = "paneltxtProblemaEncontrado";
        paneltxtProblemaEncontrado.Size = new Size(772, 108);
        paneltxtProblemaEncontrado.TabIndex = 2;
        // 
        // lbltxtProblemaEncontrado
        // 
        lbltxtProblemaEncontrado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtProblemaEncontrado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtProblemaEncontrado.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtProblemaEncontrado.Location = new Point(0, 4);
        lbltxtProblemaEncontrado.Name = "lbltxtProblemaEncontrado";
        lbltxtProblemaEncontrado.Size = new Size(772, 22);
        lbltxtProblemaEncontrado.TabIndex = 0;
        lbltxtProblemaEncontrado.Text = "Problema encontrado *";
        // 
        // txtProblemaEncontrado
        // 
        txtProblemaEncontrado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtProblemaEncontrado.BackColor = Color.WhiteSmoke;
        txtProblemaEncontrado.BorderStyle = BorderStyle.FixedSingle;
        txtProblemaEncontrado.Font = new Font("Segoe UI", 10F);
        txtProblemaEncontrado.ForeColor = Color.FromArgb(20, 43, 78);
        txtProblemaEncontrado.Location = new Point(0, 32);
        txtProblemaEncontrado.MaxLength = 1000;
        txtProblemaEncontrado.Multiline = true;
        txtProblemaEncontrado.Name = "txtProblemaEncontrado";
        txtProblemaEncontrado.PlaceholderText = "Describe el resultado de la revisión técnica.";
        txtProblemaEncontrado.ScrollBars = ScrollBars.Vertical;
        txtProblemaEncontrado.Size = new Size(772, 68);
        txtProblemaEncontrado.TabIndex = 0;
        // 
        // paneltxtReparacionPropuesta
        // 
        paneltxtReparacionPropuesta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtReparacionPropuesta.Controls.Add(lbltxtReparacionPropuesta);
        paneltxtReparacionPropuesta.Controls.Add(txtReparacionPropuesta);
        paneltxtReparacionPropuesta.Location = new Point(18, 222);
        paneltxtReparacionPropuesta.Name = "paneltxtReparacionPropuesta";
        paneltxtReparacionPropuesta.Size = new Size(772, 108);
        paneltxtReparacionPropuesta.TabIndex = 3;
        // 
        // lbltxtReparacionPropuesta
        // 
        lbltxtReparacionPropuesta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtReparacionPropuesta.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtReparacionPropuesta.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtReparacionPropuesta.Location = new Point(0, 4);
        lbltxtReparacionPropuesta.Name = "lbltxtReparacionPropuesta";
        lbltxtReparacionPropuesta.Size = new Size(772, 22);
        lbltxtReparacionPropuesta.TabIndex = 0;
        lbltxtReparacionPropuesta.Text = "Reparación propuesta *";
        // 
        // txtReparacionPropuesta
        // 
        txtReparacionPropuesta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtReparacionPropuesta.BackColor = Color.WhiteSmoke;
        txtReparacionPropuesta.BorderStyle = BorderStyle.FixedSingle;
        txtReparacionPropuesta.Font = new Font("Segoe UI", 10F);
        txtReparacionPropuesta.ForeColor = Color.FromArgb(20, 43, 78);
        txtReparacionPropuesta.Location = new Point(0, 32);
        txtReparacionPropuesta.MaxLength = 1000;
        txtReparacionPropuesta.Multiline = true;
        txtReparacionPropuesta.Name = "txtReparacionPropuesta";
        txtReparacionPropuesta.PlaceholderText = "Detalla el trabajo que necesita el equipo.";
        txtReparacionPropuesta.ScrollBars = ScrollBars.Vertical;
        txtReparacionPropuesta.Size = new Size(772, 68);
        txtReparacionPropuesta.TabIndex = 0;
        // 
        // panelnumCostoEstimado
        // 
        panelnumCostoEstimado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelnumCostoEstimado.Controls.Add(lblnumCostoEstimado);
        panelnumCostoEstimado.Controls.Add(numCostoEstimado);
        panelnumCostoEstimado.Location = new Point(18, 340);
        panelnumCostoEstimado.Name = "panelnumCostoEstimado";
        panelnumCostoEstimado.Size = new Size(308, 80);
        panelnumCostoEstimado.TabIndex = 4;
        // 
        // lblnumCostoEstimado
        // 
        lblnumCostoEstimado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblnumCostoEstimado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblnumCostoEstimado.ForeColor = Color.FromArgb(20, 43, 78);
        lblnumCostoEstimado.Location = new Point(0, 4);
        lblnumCostoEstimado.Name = "lblnumCostoEstimado";
        lblnumCostoEstimado.Size = new Size(308, 22);
        lblnumCostoEstimado.TabIndex = 0;
        lblnumCostoEstimado.Text = "Costo estimado (C$) *";
        // 
        // numCostoEstimado
        // 
        numCostoEstimado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numCostoEstimado.BackColor = Color.White;
        numCostoEstimado.BorderStyle = BorderStyle.FixedSingle;
        numCostoEstimado.Font = new Font("Segoe UI", 10F);
        numCostoEstimado.ForeColor = Color.FromArgb(20, 43, 78);
        numCostoEstimado.Location = new Point(0, 32);
        numCostoEstimado.Name = "numCostoEstimado";
        numCostoEstimado.PlaceholderText = "0.00";
        numCostoEstimado.Size = new Size(308, 30);
        numCostoEstimado.TabIndex = 0;
        numCostoEstimado.Text = "0.00";
        numCostoEstimado.TextAlign = HorizontalAlignment.Right;
        // 
        // panelcboDecisionCliente
        // 
        panelcboDecisionCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelcboDecisionCliente.Controls.Add(lblcboDecisionCliente);
        panelcboDecisionCliente.Controls.Add(cboDecisionCliente);
        panelcboDecisionCliente.Location = new Point(482, 340);
        panelcboDecisionCliente.Name = "panelcboDecisionCliente";
        panelcboDecisionCliente.Size = new Size(308, 80);
        panelcboDecisionCliente.TabIndex = 5;
        // 
        // lblcboDecisionCliente
        // 
        lblcboDecisionCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblcboDecisionCliente.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblcboDecisionCliente.ForeColor = Color.FromArgb(20, 43, 78);
        lblcboDecisionCliente.Location = new Point(0, 4);
        lblcboDecisionCliente.Name = "lblcboDecisionCliente";
        lblcboDecisionCliente.Size = new Size(308, 22);
        lblcboDecisionCliente.TabIndex = 0;
        lblcboDecisionCliente.Text = "Decisión del cliente";
        // 
        // cboDecisionCliente
        // 
        cboDecisionCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboDecisionCliente.BackColor = Color.WhiteSmoke;
        cboDecisionCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        cboDecisionCliente.FlatStyle = FlatStyle.Flat;
        cboDecisionCliente.Font = new Font("Segoe UI", 10F);
        cboDecisionCliente.ForeColor = Color.FromArgb(20, 43, 78);
        cboDecisionCliente.Items.AddRange(new object[] { "Pendiente de confirmación", "Aprobada", "Rechazada" });
        cboDecisionCliente.Location = new Point(0, 32);
        cboDecisionCliente.Name = "cboDecisionCliente";
        cboDecisionCliente.Size = new Size(308, 31);
        cboDecisionCliente.TabIndex = 0;
        // 
        // panelnumCostoAprobado
        // 
        panelnumCostoAprobado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelnumCostoAprobado.Controls.Add(lblnumCostoAprobado);
        panelnumCostoAprobado.Controls.Add(numCostoAprobado);
        panelnumCostoAprobado.Location = new Point(18, 426);
        panelnumCostoAprobado.Name = "panelnumCostoAprobado";
        panelnumCostoAprobado.Size = new Size(308, 80);
        panelnumCostoAprobado.TabIndex = 6;
        // 
        // lblnumCostoAprobado
        // 
        lblnumCostoAprobado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblnumCostoAprobado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblnumCostoAprobado.ForeColor = Color.FromArgb(20, 43, 78);
        lblnumCostoAprobado.Location = new Point(0, 4);
        lblnumCostoAprobado.Name = "lblnumCostoAprobado";
        lblnumCostoAprobado.Size = new Size(308, 22);
        lblnumCostoAprobado.TabIndex = 0;
        lblnumCostoAprobado.Text = "Costo aprobado (C$)";
        // 
        // numCostoAprobado
        // 
        numCostoAprobado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numCostoAprobado.BackColor = Color.White;
        numCostoAprobado.BorderStyle = BorderStyle.FixedSingle;
        numCostoAprobado.Font = new Font("Segoe UI", 10F);
        numCostoAprobado.ForeColor = Color.FromArgb(20, 43, 78);
        numCostoAprobado.Location = new Point(0, 32);
        numCostoAprobado.Name = "numCostoAprobado";
        numCostoAprobado.PlaceholderText = "0.00";
        numCostoAprobado.Size = new Size(308, 30);
        numCostoAprobado.TabIndex = 0;
        numCostoAprobado.Text = "0.00";
        numCostoAprobado.TextAlign = HorizontalAlignment.Right;
        // 
        // paneltxtFechaConfirmacion
        // 
        paneltxtFechaConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtFechaConfirmacion.Controls.Add(lbltxtFechaConfirmacion);
        paneltxtFechaConfirmacion.Controls.Add(txtFechaConfirmacion);
        paneltxtFechaConfirmacion.Location = new Point(482, 426);
        paneltxtFechaConfirmacion.Name = "paneltxtFechaConfirmacion";
        paneltxtFechaConfirmacion.Size = new Size(308, 80);
        paneltxtFechaConfirmacion.TabIndex = 7;
        // 
        // lbltxtFechaConfirmacion
        // 
        lbltxtFechaConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtFechaConfirmacion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtFechaConfirmacion.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtFechaConfirmacion.Location = new Point(0, 4);
        lbltxtFechaConfirmacion.Name = "lbltxtFechaConfirmacion";
        lbltxtFechaConfirmacion.Size = new Size(308, 22);
        lbltxtFechaConfirmacion.TabIndex = 0;
        lbltxtFechaConfirmacion.Text = "Fecha de confirmación";
        // 
        // txtFechaConfirmacion
        // 
        txtFechaConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtFechaConfirmacion.BackColor = Color.FromArgb(246, 248, 251);
        txtFechaConfirmacion.BorderStyle = BorderStyle.FixedSingle;
        txtFechaConfirmacion.Font = new Font("Segoe UI", 10F);
        txtFechaConfirmacion.ForeColor = Color.FromArgb(20, 43, 78);
        txtFechaConfirmacion.Location = new Point(0, 32);
        txtFechaConfirmacion.MaxLength = 100;
        txtFechaConfirmacion.Name = "txtFechaConfirmacion";
        txtFechaConfirmacion.PlaceholderText = "—";
        txtFechaConfirmacion.ReadOnly = true;
        txtFechaConfirmacion.Size = new Size(308, 30);
        txtFechaConfirmacion.TabIndex = 0;
        txtFechaConfirmacion.TabStop = false;
        // 
        // paneltxtObservacionesDiagnostico
        // 
        paneltxtObservacionesDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtObservacionesDiagnostico.Controls.Add(lbltxtObservacionesDiagnostico);
        paneltxtObservacionesDiagnostico.Controls.Add(txtObservacionesDiagnostico);
        paneltxtObservacionesDiagnostico.Location = new Point(18, 512);
        paneltxtObservacionesDiagnostico.Name = "paneltxtObservacionesDiagnostico";
        paneltxtObservacionesDiagnostico.Size = new Size(772, 108);
        paneltxtObservacionesDiagnostico.TabIndex = 8;
        // 
        // lbltxtObservacionesDiagnostico
        // 
        lbltxtObservacionesDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtObservacionesDiagnostico.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtObservacionesDiagnostico.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtObservacionesDiagnostico.Location = new Point(0, 4);
        lbltxtObservacionesDiagnostico.Name = "lbltxtObservacionesDiagnostico";
        lbltxtObservacionesDiagnostico.Size = new Size(772, 22);
        lbltxtObservacionesDiagnostico.TabIndex = 0;
        lbltxtObservacionesDiagnostico.Text = "Observaciones del diagnóstico y la confirmación";
        // 
        // txtObservacionesDiagnostico
        // 
        txtObservacionesDiagnostico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtObservacionesDiagnostico.BackColor = Color.WhiteSmoke;
        txtObservacionesDiagnostico.BorderStyle = BorderStyle.FixedSingle;
        txtObservacionesDiagnostico.Font = new Font("Segoe UI", 10F);
        txtObservacionesDiagnostico.ForeColor = Color.FromArgb(20, 43, 78);
        txtObservacionesDiagnostico.Location = new Point(0, 32);
        txtObservacionesDiagnostico.MaxLength = 1000;
        txtObservacionesDiagnostico.Multiline = true;
        txtObservacionesDiagnostico.Name = "txtObservacionesDiagnostico";
        txtObservacionesDiagnostico.PlaceholderText = "Notas de la revisión y respuesta del cliente.";
        txtObservacionesDiagnostico.ScrollBars = ScrollBars.Vertical;
        txtObservacionesDiagnostico.Size = new Size(772, 68);
        txtObservacionesDiagnostico.TabIndex = 0;
        // 
        // tabTrabajos
        // 
        tabTrabajos.AutoScroll = true;
        tabTrabajos.BackColor = Color.White;
        tabTrabajos.Controls.Add(lblServiciosOrden);
        tabTrabajos.Controls.Add(btnAgregarServicio);
        tabTrabajos.Controls.Add(dgvServiciosOrden);
        tabTrabajos.Controls.Add(lblServiciosVacios);
        tabTrabajos.Controls.Add(lblRepuestosOrden);
        tabTrabajos.Controls.Add(btnAgregarRepuesto);
        tabTrabajos.Controls.Add(dgvRepuestosOrden);
        tabTrabajos.Controls.Add(lblRepuestosVacios);
        tabTrabajos.Controls.Add(paneltxtTotalTrabajos);
        tabTrabajos.Controls.Add(panelcboResultadoReparacion);
        tabTrabajos.Controls.Add(paneltxtObservacionesTrabajo);
        tabTrabajos.Location = new Point(4, 40);
        tabTrabajos.Name = "tabTrabajos";
        tabTrabajos.Padding = new Padding(18);
        tabTrabajos.Size = new Size(947, 458);
        tabTrabajos.TabIndex = 2;
        tabTrabajos.Text = "Trabajos";
        // 
        // lblServiciosOrden
        // 
        lblServiciosOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblServiciosOrden.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        lblServiciosOrden.ForeColor = Color.FromArgb(20, 43, 78);
        lblServiciosOrden.Location = new Point(18, 22);
        lblServiciosOrden.Name = "lblServiciosOrden";
        lblServiciosOrden.Size = new Size(566, 26);
        lblServiciosOrden.TabIndex = 0;
        lblServiciosOrden.Text = "Servicios realizados";
        // 
        // btnAgregarServicio
        // 
        btnAgregarServicio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAgregarServicio.BackColor = Color.FromArgb(239, 246, 255);
        btnAgregarServicio.FlatAppearance.BorderSize = 0;
        btnAgregarServicio.FlatStyle = FlatStyle.Flat;
        btnAgregarServicio.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        btnAgregarServicio.ForeColor = Color.FromArgb(37, 99, 235);
        btnAgregarServicio.IconChar = FontAwesome.Sharp.IconChar.Add;
        btnAgregarServicio.IconColor = Color.FromArgb(37, 99, 235);
        btnAgregarServicio.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnAgregarServicio.IconSize = 16;
        btnAgregarServicio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAgregarServicio.Location = new Point(717, 16);
        btnAgregarServicio.Name = "btnAgregarServicio";
        btnAgregarServicio.Size = new Size(178, 34);
        btnAgregarServicio.TabIndex = 0;
        btnAgregarServicio.Text = "Agregar servicio";
        btnAgregarServicio.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnAgregarServicio.UseVisualStyleBackColor = false;
        // 
        // dgvServiciosOrden
        // 
        dgvServiciosOrden.AllowUserToAddRows = false;
        dgvServiciosOrden.AllowUserToDeleteRows = false;
        dgvServiciosOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvServiciosOrden.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvServiciosOrden.BackgroundColor = Color.White;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(246, 248, 251);
        dataGridViewCellStyle1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(103, 116, 137);
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvServiciosOrden.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvServiciosOrden.ColumnHeadersHeight = 40;
        dgvServiciosOrden.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvServiciosOrden.Columns.AddRange(new DataGridViewColumn[] { dgvServiciosOrdenCol0, dgvServiciosOrdenCol1, dgvServiciosOrdenCol2, dgvServiciosOrdenCol3 });
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = SystemColors.Window;
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F);
        dataGridViewCellStyle4.ForeColor = Color.FromArgb(20, 43, 78);
        dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(234, 240, 252);
        dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(20, 43, 78);
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
        dgvServiciosOrden.DefaultCellStyle = dataGridViewCellStyle4;
        dgvServiciosOrden.EnableHeadersVisualStyles = false;
        dgvServiciosOrden.GridColor = Color.FromArgb(235, 240, 246);
        dgvServiciosOrden.Location = new Point(18, 60);
        dgvServiciosOrden.MultiSelect = false;
        dgvServiciosOrden.Name = "dgvServiciosOrden";
        dgvServiciosOrden.ReadOnly = true;
        dgvServiciosOrden.RowHeadersVisible = false;
        dgvServiciosOrden.RowHeadersWidth = 51;
        dgvServiciosOrden.RowTemplate.Height = 40;
        dgvServiciosOrden.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvServiciosOrden.Size = new Size(877, 144);
        dgvServiciosOrden.TabIndex = 1;
        // 
        // dgvServiciosOrdenCol0
        // 
        dgvServiciosOrdenCol0.FillWeight = 42F;
        dgvServiciosOrdenCol0.HeaderText = "Servicio";
        dgvServiciosOrdenCol0.MinimumWidth = 6;
        dgvServiciosOrdenCol0.Name = "dgvServiciosOrdenCol0";
        dgvServiciosOrdenCol0.ReadOnly = true;
        dgvServiciosOrdenCol0.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvServiciosOrdenCol1
        // 
        dgvServiciosOrdenCol1.FillWeight = 14F;
        dgvServiciosOrdenCol1.HeaderText = "Cantidad";
        dgvServiciosOrdenCol1.MinimumWidth = 6;
        dgvServiciosOrdenCol1.Name = "dgvServiciosOrdenCol1";
        dgvServiciosOrdenCol1.ReadOnly = true;
        dgvServiciosOrdenCol1.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvServiciosOrdenCol2
        // 
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle2.Format = "N2";
        dgvServiciosOrdenCol2.DefaultCellStyle = dataGridViewCellStyle2;
        dgvServiciosOrdenCol2.FillWeight = 20F;
        dgvServiciosOrdenCol2.HeaderText = "Precio (C$)";
        dgvServiciosOrdenCol2.MinimumWidth = 6;
        dgvServiciosOrdenCol2.Name = "dgvServiciosOrdenCol2";
        dgvServiciosOrdenCol2.ReadOnly = true;
        dgvServiciosOrdenCol2.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvServiciosOrdenCol3
        // 
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle3.Format = "N2";
        dgvServiciosOrdenCol3.DefaultCellStyle = dataGridViewCellStyle3;
        dgvServiciosOrdenCol3.FillWeight = 24F;
        dgvServiciosOrdenCol3.HeaderText = "Subtotal (C$)";
        dgvServiciosOrdenCol3.MinimumWidth = 6;
        dgvServiciosOrdenCol3.Name = "dgvServiciosOrdenCol3";
        dgvServiciosOrdenCol3.ReadOnly = true;
        dgvServiciosOrdenCol3.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // lblServiciosVacios
        // 
        lblServiciosVacios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblServiciosVacios.Font = new Font("Segoe UI", 9F);
        lblServiciosVacios.ForeColor = Color.FromArgb(103, 116, 137);
        lblServiciosVacios.Location = new Point(18, 208);
        lblServiciosVacios.Name = "lblServiciosVacios";
        lblServiciosVacios.Size = new Size(877, 24);
        lblServiciosVacios.TabIndex = 2;
        lblServiciosVacios.Text = "Aún no hay servicios agregados a la orden.";
        // 
        // lblRepuestosOrden
        // 
        lblRepuestosOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRepuestosOrden.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        lblRepuestosOrden.ForeColor = Color.FromArgb(20, 43, 78);
        lblRepuestosOrden.Location = new Point(18, 256);
        lblRepuestosOrden.Name = "lblRepuestosOrden";
        lblRepuestosOrden.Size = new Size(877, 26);
        lblRepuestosOrden.TabIndex = 3;
        lblRepuestosOrden.Text = "Repuestos utilizados";
        // 
        // btnAgregarRepuesto
        // 
        btnAgregarRepuesto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAgregarRepuesto.BackColor = Color.FromArgb(239, 246, 255);
        btnAgregarRepuesto.FlatAppearance.BorderSize = 0;
        btnAgregarRepuesto.FlatStyle = FlatStyle.Flat;
        btnAgregarRepuesto.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        btnAgregarRepuesto.ForeColor = Color.FromArgb(37, 99, 235);
        btnAgregarRepuesto.IconChar = FontAwesome.Sharp.IconChar.Add;
        btnAgregarRepuesto.IconColor = Color.FromArgb(37, 99, 235);
        btnAgregarRepuesto.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnAgregarRepuesto.IconSize = 16;
        btnAgregarRepuesto.Location = new Point(717, 250);
        btnAgregarRepuesto.Name = "btnAgregarRepuesto";
        btnAgregarRepuesto.Size = new Size(178, 34);
        btnAgregarRepuesto.TabIndex = 2;
        btnAgregarRepuesto.Text = "Agregar repuesto";
        btnAgregarRepuesto.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnAgregarRepuesto.UseVisualStyleBackColor = false;
        // 
        // dgvRepuestosOrden
        // 
        dgvRepuestosOrden.AllowUserToAddRows = false;
        dgvRepuestosOrden.AllowUserToDeleteRows = false;
        dgvRepuestosOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvRepuestosOrden.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvRepuestosOrden.BackgroundColor = Color.White;
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = Color.FromArgb(246, 248, 251);
        dataGridViewCellStyle5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        dataGridViewCellStyle5.ForeColor = Color.FromArgb(103, 116, 137);
        dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
        dgvRepuestosOrden.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
        dgvRepuestosOrden.ColumnHeadersHeight = 40;
        dgvRepuestosOrden.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvRepuestosOrden.Columns.AddRange(new DataGridViewColumn[] { dgvRepuestosOrdenCol0, dgvRepuestosOrdenCol1, dgvRepuestosOrdenCol2, dgvRepuestosOrdenCol3 });
        dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle8.BackColor = SystemColors.Window;
        dataGridViewCellStyle8.Font = new Font("Segoe UI", 10F);
        dataGridViewCellStyle8.ForeColor = Color.FromArgb(20, 43, 78);
        dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(234, 240, 252);
        dataGridViewCellStyle8.SelectionForeColor = Color.FromArgb(20, 43, 78);
        dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
        dgvRepuestosOrden.DefaultCellStyle = dataGridViewCellStyle8;
        dgvRepuestosOrden.EnableHeadersVisualStyles = false;
        dgvRepuestosOrden.GridColor = Color.FromArgb(235, 240, 246);
        dgvRepuestosOrden.Location = new Point(18, 294);
        dgvRepuestosOrden.MultiSelect = false;
        dgvRepuestosOrden.Name = "dgvRepuestosOrden";
        dgvRepuestosOrden.ReadOnly = true;
        dgvRepuestosOrden.RowHeadersVisible = false;
        dgvRepuestosOrden.RowHeadersWidth = 51;
        dgvRepuestosOrden.RowTemplate.Height = 40;
        dgvRepuestosOrden.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvRepuestosOrden.Size = new Size(877, 144);
        dgvRepuestosOrden.TabIndex = 3;
        // 
        // dgvRepuestosOrdenCol0
        // 
        dgvRepuestosOrdenCol0.FillWeight = 42F;
        dgvRepuestosOrdenCol0.HeaderText = "Repuesto";
        dgvRepuestosOrdenCol0.MinimumWidth = 6;
        dgvRepuestosOrdenCol0.Name = "dgvRepuestosOrdenCol0";
        dgvRepuestosOrdenCol0.ReadOnly = true;
        dgvRepuestosOrdenCol0.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvRepuestosOrdenCol1
        // 
        dgvRepuestosOrdenCol1.FillWeight = 14F;
        dgvRepuestosOrdenCol1.HeaderText = "Cantidad";
        dgvRepuestosOrdenCol1.MinimumWidth = 6;
        dgvRepuestosOrdenCol1.Name = "dgvRepuestosOrdenCol1";
        dgvRepuestosOrdenCol1.ReadOnly = true;
        dgvRepuestosOrdenCol1.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvRepuestosOrdenCol2
        // 
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle6.Format = "N2";
        dgvRepuestosOrdenCol2.DefaultCellStyle = dataGridViewCellStyle6;
        dgvRepuestosOrdenCol2.FillWeight = 20F;
        dgvRepuestosOrdenCol2.HeaderText = "Precio (C$)";
        dgvRepuestosOrdenCol2.MinimumWidth = 6;
        dgvRepuestosOrdenCol2.Name = "dgvRepuestosOrdenCol2";
        dgvRepuestosOrdenCol2.ReadOnly = true;
        dgvRepuestosOrdenCol2.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvRepuestosOrdenCol3
        // 
        dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle7.Format = "N2";
        dgvRepuestosOrdenCol3.DefaultCellStyle = dataGridViewCellStyle7;
        dgvRepuestosOrdenCol3.FillWeight = 24F;
        dgvRepuestosOrdenCol3.HeaderText = "Subtotal (C$)";
        dgvRepuestosOrdenCol3.MinimumWidth = 6;
        dgvRepuestosOrdenCol3.Name = "dgvRepuestosOrdenCol3";
        dgvRepuestosOrdenCol3.ReadOnly = true;
        dgvRepuestosOrdenCol3.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // lblRepuestosVacios
        // 
        lblRepuestosVacios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRepuestosVacios.Font = new Font("Segoe UI", 9F);
        lblRepuestosVacios.ForeColor = Color.FromArgb(103, 116, 137);
        lblRepuestosVacios.Location = new Point(18, 442);
        lblRepuestosVacios.Name = "lblRepuestosVacios";
        lblRepuestosVacios.Size = new Size(772, 24);
        lblRepuestosVacios.TabIndex = 4;
        lblRepuestosVacios.Text = "Aún no hay repuestos agregados a la orden.";
        // 
        // paneltxtTotalTrabajos
        // 
        paneltxtTotalTrabajos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtTotalTrabajos.Controls.Add(lbltxtTotalTrabajos);
        paneltxtTotalTrabajos.Controls.Add(txtTotalTrabajos);
        paneltxtTotalTrabajos.Location = new Point(18, 480);
        paneltxtTotalTrabajos.Name = "paneltxtTotalTrabajos";
        paneltxtTotalTrabajos.Size = new Size(308, 80);
        paneltxtTotalTrabajos.TabIndex = 4;
        // 
        // lbltxtTotalTrabajos
        // 
        lbltxtTotalTrabajos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtTotalTrabajos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtTotalTrabajos.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtTotalTrabajos.Location = new Point(0, 4);
        lbltxtTotalTrabajos.Name = "lbltxtTotalTrabajos";
        lbltxtTotalTrabajos.Size = new Size(308, 22);
        lbltxtTotalTrabajos.TabIndex = 0;
        lbltxtTotalTrabajos.Text = "Total de servicios y repuestos (C$)";
        // 
        // txtTotalTrabajos
        // 
        txtTotalTrabajos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTotalTrabajos.BackColor = Color.FromArgb(246, 248, 251);
        txtTotalTrabajos.BorderStyle = BorderStyle.FixedSingle;
        txtTotalTrabajos.Font = new Font("Segoe UI", 10F);
        txtTotalTrabajos.ForeColor = Color.FromArgb(20, 43, 78);
        txtTotalTrabajos.Location = new Point(0, 32);
        txtTotalTrabajos.MaxLength = 100;
        txtTotalTrabajos.Name = "txtTotalTrabajos";
        txtTotalTrabajos.PlaceholderText = "—";
        txtTotalTrabajos.ReadOnly = true;
        txtTotalTrabajos.Size = new Size(308, 30);
        txtTotalTrabajos.TabIndex = 0;
        txtTotalTrabajos.TabStop = false;
        // 
        // panelcboResultadoReparacion
        // 
        panelcboResultadoReparacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelcboResultadoReparacion.Controls.Add(lblcboResultadoReparacion);
        panelcboResultadoReparacion.Controls.Add(cboResultadoReparacion);
        panelcboResultadoReparacion.Location = new Point(482, 480);
        panelcboResultadoReparacion.Name = "panelcboResultadoReparacion";
        panelcboResultadoReparacion.Size = new Size(308, 80);
        panelcboResultadoReparacion.TabIndex = 5;
        // 
        // lblcboResultadoReparacion
        // 
        lblcboResultadoReparacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblcboResultadoReparacion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblcboResultadoReparacion.ForeColor = Color.FromArgb(20, 43, 78);
        lblcboResultadoReparacion.Location = new Point(0, 4);
        lblcboResultadoReparacion.Name = "lblcboResultadoReparacion";
        lblcboResultadoReparacion.Size = new Size(308, 22);
        lblcboResultadoReparacion.TabIndex = 0;
        lblcboResultadoReparacion.Text = "Resultado de la reparación";
        // 
        // cboResultadoReparacion
        // 
        cboResultadoReparacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboResultadoReparacion.BackColor = Color.White;
        cboResultadoReparacion.DropDownStyle = ComboBoxStyle.DropDownList;
        cboResultadoReparacion.FlatStyle = FlatStyle.Flat;
        cboResultadoReparacion.Font = new Font("Segoe UI", 10F);
        cboResultadoReparacion.ForeColor = Color.FromArgb(20, 43, 78);
        cboResultadoReparacion.Items.AddRange(new object[] { "Selecciona el resultado", "Reparada", "No Reparada" });
        cboResultadoReparacion.Location = new Point(0, 32);
        cboResultadoReparacion.Name = "cboResultadoReparacion";
        cboResultadoReparacion.Size = new Size(308, 31);
        cboResultadoReparacion.TabIndex = 0;
        // 
        // paneltxtObservacionesTrabajo
        // 
        paneltxtObservacionesTrabajo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtObservacionesTrabajo.Controls.Add(lbltxtObservacionesTrabajo);
        paneltxtObservacionesTrabajo.Controls.Add(txtObservacionesTrabajo);
        paneltxtObservacionesTrabajo.Location = new Point(18, 566);
        paneltxtObservacionesTrabajo.Name = "paneltxtObservacionesTrabajo";
        paneltxtObservacionesTrabajo.Size = new Size(772, 112);
        paneltxtObservacionesTrabajo.TabIndex = 6;
        // 
        // lbltxtObservacionesTrabajo
        // 
        lbltxtObservacionesTrabajo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtObservacionesTrabajo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtObservacionesTrabajo.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtObservacionesTrabajo.Location = new Point(0, 4);
        lbltxtObservacionesTrabajo.Name = "lbltxtObservacionesTrabajo";
        lbltxtObservacionesTrabajo.Size = new Size(772, 22);
        lbltxtObservacionesTrabajo.TabIndex = 0;
        lbltxtObservacionesTrabajo.Text = "Observaciones del trabajo";
        // 
        // txtObservacionesTrabajo
        // 
        txtObservacionesTrabajo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtObservacionesTrabajo.BackColor = Color.White;
        txtObservacionesTrabajo.BorderStyle = BorderStyle.FixedSingle;
        txtObservacionesTrabajo.Font = new Font("Segoe UI", 10F);
        txtObservacionesTrabajo.ForeColor = Color.FromArgb(20, 43, 78);
        txtObservacionesTrabajo.Location = new Point(0, 32);
        txtObservacionesTrabajo.MaxLength = 500;
        txtObservacionesTrabajo.Multiline = true;
        txtObservacionesTrabajo.Name = "txtObservacionesTrabajo";
        txtObservacionesTrabajo.PlaceholderText = "Detalla el trabajo realizado y el estado final del equipo.";
        txtObservacionesTrabajo.ScrollBars = ScrollBars.Vertical;
        txtObservacionesTrabajo.Size = new Size(772, 72);
        txtObservacionesTrabajo.TabIndex = 0;
        // 
        // tabPagos
        // 
        tabPagos.AutoScroll = true;
        tabPagos.BackColor = Color.White;
        tabPagos.Controls.Add(lblCobrosOrden);
        tabPagos.Controls.Add(btnRegistrarPagoOrden);
        tabPagos.Controls.Add(dgvPagosOrden);
        tabPagos.Controls.Add(lblPagosVacios);
        tabPagos.Controls.Add(paneltxtTotalPagos);
        tabPagos.Controls.Add(paneltxtPagadoOrden);
        tabPagos.Controls.Add(paneltxtSaldoPagos);
        tabPagos.Location = new Point(4, 40);
        tabPagos.Name = "tabPagos";
        tabPagos.Padding = new Padding(18);
        tabPagos.Size = new Size(947, 458);
        tabPagos.TabIndex = 3;
        tabPagos.Text = "Pagos";
        // 
        // lblCobrosOrden
        // 
        lblCobrosOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblCobrosOrden.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        lblCobrosOrden.ForeColor = Color.FromArgb(20, 43, 78);
        lblCobrosOrden.Location = new Point(18, 22);
        lblCobrosOrden.Name = "lblCobrosOrden";
        lblCobrosOrden.Size = new Size(671, 26);
        lblCobrosOrden.TabIndex = 0;
        lblCobrosOrden.Text = "Pagos de la orden";
        // 
        // btnRegistrarPagoOrden
        // 
        btnRegistrarPagoOrden.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRegistrarPagoOrden.BackColor = Color.FromArgb(239, 246, 255);
        btnRegistrarPagoOrden.FlatAppearance.BorderSize = 0;
        btnRegistrarPagoOrden.FlatStyle = FlatStyle.Flat;
        btnRegistrarPagoOrden.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        btnRegistrarPagoOrden.ForeColor = Color.FromArgb(37, 99, 235);
        btnRegistrarPagoOrden.IconChar = FontAwesome.Sharp.IconChar.Add;
        btnRegistrarPagoOrden.IconColor = Color.FromArgb(37, 99, 235);
        btnRegistrarPagoOrden.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnRegistrarPagoOrden.IconSize = 16;
        btnRegistrarPagoOrden.Location = new Point(717, 16);
        btnRegistrarPagoOrden.Name = "btnRegistrarPagoOrden";
        btnRegistrarPagoOrden.Size = new Size(178, 34);
        btnRegistrarPagoOrden.TabIndex = 0;
        btnRegistrarPagoOrden.Text = "Registrar pago";
        btnRegistrarPagoOrden.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnRegistrarPagoOrden.UseVisualStyleBackColor = false;
        // 
        // dgvPagosOrden
        // 
        dgvPagosOrden.AllowUserToAddRows = false;
        dgvPagosOrden.AllowUserToDeleteRows = false;
        dgvPagosOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvPagosOrden.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPagosOrden.BackgroundColor = Color.White;
        dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle9.BackColor = Color.FromArgb(246, 248, 251);
        dataGridViewCellStyle9.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        dataGridViewCellStyle9.ForeColor = Color.FromArgb(103, 116, 137);
        dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
        dgvPagosOrden.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
        dgvPagosOrden.ColumnHeadersHeight = 40;
        dgvPagosOrden.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvPagosOrden.Columns.AddRange(new DataGridViewColumn[] { dgvPagosOrdenCol0, dgvPagosOrdenCol1, dgvPagosOrdenCol2, dgvPagosOrdenCol3, dgvPagosOrdenCol4 });
        dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle11.BackColor = SystemColors.Window;
        dataGridViewCellStyle11.Font = new Font("Segoe UI", 10F);
        dataGridViewCellStyle11.ForeColor = Color.FromArgb(20, 43, 78);
        dataGridViewCellStyle11.SelectionBackColor = Color.FromArgb(234, 240, 252);
        dataGridViewCellStyle11.SelectionForeColor = Color.FromArgb(20, 43, 78);
        dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
        dgvPagosOrden.DefaultCellStyle = dataGridViewCellStyle11;
        dgvPagosOrden.EnableHeadersVisualStyles = false;
        dgvPagosOrden.GridColor = Color.FromArgb(235, 240, 246);
        dgvPagosOrden.Location = new Point(18, 60);
        dgvPagosOrden.MultiSelect = false;
        dgvPagosOrden.Name = "dgvPagosOrden";
        dgvPagosOrden.ReadOnly = true;
        dgvPagosOrden.RowHeadersVisible = false;
        dgvPagosOrden.RowHeadersWidth = 51;
        dgvPagosOrden.RowTemplate.Height = 40;
        dgvPagosOrden.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPagosOrden.Size = new Size(877, 238);
        dgvPagosOrden.TabIndex = 1;
        // 
        // dgvPagosOrdenCol0
        // 
        dgvPagosOrdenCol0.FillWeight = 22F;
        dgvPagosOrdenCol0.HeaderText = "Fecha";
        dgvPagosOrdenCol0.MinimumWidth = 6;
        dgvPagosOrdenCol0.Name = "dgvPagosOrdenCol0";
        dgvPagosOrdenCol0.ReadOnly = true;
        dgvPagosOrdenCol0.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvPagosOrdenCol1
        // 
        dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle10.Format = "N2";
        dgvPagosOrdenCol1.DefaultCellStyle = dataGridViewCellStyle10;
        dgvPagosOrdenCol1.FillWeight = 20F;
        dgvPagosOrdenCol1.HeaderText = "Monto (C$)";
        dgvPagosOrdenCol1.MinimumWidth = 6;
        dgvPagosOrdenCol1.Name = "dgvPagosOrdenCol1";
        dgvPagosOrdenCol1.ReadOnly = true;
        dgvPagosOrdenCol1.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvPagosOrdenCol2
        // 
        dgvPagosOrdenCol2.FillWeight = 18F;
        dgvPagosOrdenCol2.HeaderText = "Método";
        dgvPagosOrdenCol2.MinimumWidth = 6;
        dgvPagosOrdenCol2.Name = "dgvPagosOrdenCol2";
        dgvPagosOrdenCol2.ReadOnly = true;
        dgvPagosOrdenCol2.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvPagosOrdenCol3
        // 
        dgvPagosOrdenCol3.FillWeight = 24F;
        dgvPagosOrdenCol3.HeaderText = "Registrado por";
        dgvPagosOrdenCol3.MinimumWidth = 6;
        dgvPagosOrdenCol3.Name = "dgvPagosOrdenCol3";
        dgvPagosOrdenCol3.ReadOnly = true;
        dgvPagosOrdenCol3.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvPagosOrdenCol4
        // 
        dgvPagosOrdenCol4.FillWeight = 16F;
        dgvPagosOrdenCol4.HeaderText = "Estado";
        dgvPagosOrdenCol4.MinimumWidth = 6;
        dgvPagosOrdenCol4.Name = "dgvPagosOrdenCol4";
        dgvPagosOrdenCol4.ReadOnly = true;
        dgvPagosOrdenCol4.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // lblPagosVacios
        // 
        lblPagosVacios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblPagosVacios.Font = new Font("Segoe UI", 9F);
        lblPagosVacios.ForeColor = Color.FromArgb(103, 116, 137);
        lblPagosVacios.Location = new Point(18, 304);
        lblPagosVacios.Name = "lblPagosVacios";
        lblPagosVacios.Size = new Size(877, 26);
        lblPagosVacios.TabIndex = 2;
        lblPagosVacios.Text = "Aún no hay pagos registrados en esta orden.";
        // 
        // paneltxtTotalPagos
        // 
        paneltxtTotalPagos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtTotalPagos.Controls.Add(lbltxtTotalPagos);
        paneltxtTotalPagos.Controls.Add(txtTotalPagos);
        paneltxtTotalPagos.Location = new Point(18, 350);
        paneltxtTotalPagos.Name = "paneltxtTotalPagos";
        paneltxtTotalPagos.Size = new Size(257, 80);
        paneltxtTotalPagos.TabIndex = 2;
        // 
        // lbltxtTotalPagos
        // 
        lbltxtTotalPagos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtTotalPagos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtTotalPagos.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtTotalPagos.Location = new Point(0, 4);
        lbltxtTotalPagos.Name = "lbltxtTotalPagos";
        lbltxtTotalPagos.Size = new Size(257, 22);
        lbltxtTotalPagos.TabIndex = 0;
        lbltxtTotalPagos.Text = "Total de la orden (C$)";
        // 
        // txtTotalPagos
        // 
        txtTotalPagos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTotalPagos.BackColor = Color.FromArgb(246, 248, 251);
        txtTotalPagos.BorderStyle = BorderStyle.FixedSingle;
        txtTotalPagos.Font = new Font("Segoe UI", 10F);
        txtTotalPagos.ForeColor = Color.FromArgb(20, 43, 78);
        txtTotalPagos.Location = new Point(0, 32);
        txtTotalPagos.MaxLength = 100;
        txtTotalPagos.Name = "txtTotalPagos";
        txtTotalPagos.PlaceholderText = "—";
        txtTotalPagos.ReadOnly = true;
        txtTotalPagos.Size = new Size(257, 30);
        txtTotalPagos.TabIndex = 0;
        txtTotalPagos.TabStop = false;
        // 
        // paneltxtPagadoOrden
        // 
        paneltxtPagadoOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtPagadoOrden.Controls.Add(lbltxtPagadoOrden);
        paneltxtPagadoOrden.Controls.Add(txtPagadoOrden);
        paneltxtPagadoOrden.Location = new Point(328, 350);
        paneltxtPagadoOrden.Name = "paneltxtPagadoOrden";
        paneltxtPagadoOrden.Size = new Size(257, 80);
        paneltxtPagadoOrden.TabIndex = 3;
        // 
        // lbltxtPagadoOrden
        // 
        lbltxtPagadoOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtPagadoOrden.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtPagadoOrden.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtPagadoOrden.Location = new Point(0, 4);
        lbltxtPagadoOrden.Name = "lbltxtPagadoOrden";
        lbltxtPagadoOrden.Size = new Size(257, 22);
        lbltxtPagadoOrden.TabIndex = 0;
        lbltxtPagadoOrden.Text = "Pagado (C$)";
        // 
        // txtPagadoOrden
        // 
        txtPagadoOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPagadoOrden.BackColor = Color.FromArgb(246, 248, 251);
        txtPagadoOrden.BorderStyle = BorderStyle.FixedSingle;
        txtPagadoOrden.Font = new Font("Segoe UI", 10F);
        txtPagadoOrden.ForeColor = Color.FromArgb(20, 43, 78);
        txtPagadoOrden.Location = new Point(0, 32);
        txtPagadoOrden.MaxLength = 100;
        txtPagadoOrden.Name = "txtPagadoOrden";
        txtPagadoOrden.PlaceholderText = "—";
        txtPagadoOrden.ReadOnly = true;
        txtPagadoOrden.Size = new Size(257, 30);
        txtPagadoOrden.TabIndex = 0;
        txtPagadoOrden.TabStop = false;
        // 
        // paneltxtSaldoPagos
        // 
        paneltxtSaldoPagos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        paneltxtSaldoPagos.Controls.Add(lbltxtSaldoPagos);
        paneltxtSaldoPagos.Controls.Add(txtSaldoPagos);
        paneltxtSaldoPagos.Location = new Point(638, 350);
        paneltxtSaldoPagos.Name = "paneltxtSaldoPagos";
        paneltxtSaldoPagos.Size = new Size(257, 80);
        paneltxtSaldoPagos.TabIndex = 4;
        // 
        // lbltxtSaldoPagos
        // 
        lbltxtSaldoPagos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtSaldoPagos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtSaldoPagos.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtSaldoPagos.Location = new Point(0, 4);
        lbltxtSaldoPagos.Name = "lbltxtSaldoPagos";
        lbltxtSaldoPagos.Size = new Size(257, 22);
        lbltxtSaldoPagos.TabIndex = 0;
        lbltxtSaldoPagos.Text = "Saldo pendiente (C$)";
        // 
        // txtSaldoPagos
        // 
        txtSaldoPagos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSaldoPagos.BackColor = Color.FromArgb(246, 248, 251);
        txtSaldoPagos.BorderStyle = BorderStyle.FixedSingle;
        txtSaldoPagos.Font = new Font("Segoe UI", 10F);
        txtSaldoPagos.ForeColor = Color.FromArgb(20, 43, 78);
        txtSaldoPagos.Location = new Point(0, 32);
        txtSaldoPagos.MaxLength = 100;
        txtSaldoPagos.Name = "txtSaldoPagos";
        txtSaldoPagos.PlaceholderText = "—";
        txtSaldoPagos.ReadOnly = true;
        txtSaldoPagos.Size = new Size(257, 30);
        txtSaldoPagos.TabIndex = 0;
        txtSaldoPagos.TabStop = false;
        // 
        // tabHistorial
        // 
        tabHistorial.AutoScroll = true;
        tabHistorial.BackColor = Color.White;
        tabHistorial.Controls.Add(lblHistorialTitulo);
        tabHistorial.Controls.Add(lblHistorialDescripcion);
        tabHistorial.Controls.Add(dgvHistorialOrden);
        tabHistorial.Controls.Add(lblHistorialVacio);
        tabHistorial.Location = new Point(4, 40);
        tabHistorial.Name = "tabHistorial";
        tabHistorial.Padding = new Padding(18);
        tabHistorial.Size = new Size(947, 458);
        tabHistorial.TabIndex = 4;
        tabHistorial.Text = "Historial";
        // 
        // lblHistorialTitulo
        // 
        lblHistorialTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblHistorialTitulo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        lblHistorialTitulo.ForeColor = Color.FromArgb(20, 43, 78);
        lblHistorialTitulo.Location = new Point(18, 22);
        lblHistorialTitulo.Name = "lblHistorialTitulo";
        lblHistorialTitulo.Size = new Size(877, 26);
        lblHistorialTitulo.TabIndex = 0;
        lblHistorialTitulo.Text = "Historial de estados";
        // 
        // lblHistorialDescripcion
        // 
        lblHistorialDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblHistorialDescripcion.Font = new Font("Segoe UI", 9F);
        lblHistorialDescripcion.ForeColor = Color.FromArgb(103, 116, 137);
        lblHistorialDescripcion.Location = new Point(18, 52);
        lblHistorialDescripcion.Name = "lblHistorialDescripcion";
        lblHistorialDescripcion.Size = new Size(877, 30);
        lblHistorialDescripcion.TabIndex = 1;
        lblHistorialDescripcion.Text = "Fecha, responsable y observaciones de cada cambio de estado de la orden.";
        // 
        // dgvHistorialOrden
        // 
        dgvHistorialOrden.AllowUserToAddRows = false;
        dgvHistorialOrden.AllowUserToDeleteRows = false;
        dgvHistorialOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvHistorialOrden.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHistorialOrden.BackgroundColor = Color.White;
        dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle12.BackColor = Color.FromArgb(246, 248, 251);
        dataGridViewCellStyle12.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        dataGridViewCellStyle12.ForeColor = Color.FromArgb(103, 116, 137);
        dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
        dgvHistorialOrden.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
        dgvHistorialOrden.ColumnHeadersHeight = 40;
        dgvHistorialOrden.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvHistorialOrden.Columns.AddRange(new DataGridViewColumn[] { dgvHistorialOrdenCol0, dgvHistorialOrdenCol1, dgvHistorialOrdenCol2, dgvHistorialOrdenCol3, dgvHistorialOrdenCol4 });
        dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle13.BackColor = SystemColors.Window;
        dataGridViewCellStyle13.Font = new Font("Segoe UI", 10F);
        dataGridViewCellStyle13.ForeColor = Color.FromArgb(20, 43, 78);
        dataGridViewCellStyle13.SelectionBackColor = Color.FromArgb(234, 240, 252);
        dataGridViewCellStyle13.SelectionForeColor = Color.FromArgb(20, 43, 78);
        dataGridViewCellStyle13.WrapMode = DataGridViewTriState.False;
        dgvHistorialOrden.DefaultCellStyle = dataGridViewCellStyle13;
        dgvHistorialOrden.EnableHeadersVisualStyles = false;
        dgvHistorialOrden.GridColor = Color.FromArgb(235, 240, 246);
        dgvHistorialOrden.Location = new Point(18, 96);
        dgvHistorialOrden.MultiSelect = false;
        dgvHistorialOrden.Name = "dgvHistorialOrden";
        dgvHistorialOrden.ReadOnly = true;
        dgvHistorialOrden.RowHeadersVisible = false;
        dgvHistorialOrden.RowHeadersWidth = 51;
        dgvHistorialOrden.RowTemplate.Height = 40;
        dgvHistorialOrden.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHistorialOrden.Size = new Size(877, 306);
        dgvHistorialOrden.TabIndex = 0;
        // 
        // dgvHistorialOrdenCol0
        // 
        dgvHistorialOrdenCol0.FillWeight = 20F;
        dgvHistorialOrdenCol0.HeaderText = "Fecha y hora";
        dgvHistorialOrdenCol0.MinimumWidth = 6;
        dgvHistorialOrdenCol0.Name = "dgvHistorialOrdenCol0";
        dgvHistorialOrdenCol0.ReadOnly = true;
        dgvHistorialOrdenCol0.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvHistorialOrdenCol1
        // 
        dgvHistorialOrdenCol1.FillWeight = 18F;
        dgvHistorialOrdenCol1.HeaderText = "Estado anterior";
        dgvHistorialOrdenCol1.MinimumWidth = 6;
        dgvHistorialOrdenCol1.Name = "dgvHistorialOrdenCol1";
        dgvHistorialOrdenCol1.ReadOnly = true;
        dgvHistorialOrdenCol1.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvHistorialOrdenCol2
        // 
        dgvHistorialOrdenCol2.FillWeight = 18F;
        dgvHistorialOrdenCol2.HeaderText = "Nuevo estado";
        dgvHistorialOrdenCol2.MinimumWidth = 6;
        dgvHistorialOrdenCol2.Name = "dgvHistorialOrdenCol2";
        dgvHistorialOrdenCol2.ReadOnly = true;
        dgvHistorialOrdenCol2.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvHistorialOrdenCol3
        // 
        dgvHistorialOrdenCol3.FillWeight = 18F;
        dgvHistorialOrdenCol3.HeaderText = "Usuario";
        dgvHistorialOrdenCol3.MinimumWidth = 6;
        dgvHistorialOrdenCol3.Name = "dgvHistorialOrdenCol3";
        dgvHistorialOrdenCol3.ReadOnly = true;
        dgvHistorialOrdenCol3.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // dgvHistorialOrdenCol4
        // 
        dgvHistorialOrdenCol4.FillWeight = 26F;
        dgvHistorialOrdenCol4.HeaderText = "Observación";
        dgvHistorialOrdenCol4.MinimumWidth = 6;
        dgvHistorialOrdenCol4.Name = "dgvHistorialOrdenCol4";
        dgvHistorialOrdenCol4.ReadOnly = true;
        dgvHistorialOrdenCol4.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // lblHistorialVacio
        // 
        lblHistorialVacio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblHistorialVacio.Font = new Font("Segoe UI", 9F);
        lblHistorialVacio.ForeColor = Color.FromArgb(103, 116, 137);
        lblHistorialVacio.Location = new Point(18, 408);
        lblHistorialVacio.Name = "lblHistorialVacio";
        lblHistorialVacio.Size = new Size(877, 28);
        lblHistorialVacio.TabIndex = 2;
        lblHistorialVacio.Text = "El historial de la orden aparecerá en esta sección.";
        // 
        // FrmDetalleOrdenServicio
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        ClientSize = new Size(1040, 860);
        Name = "FrmDetalleOrdenServicio";
        Text = "Expediente de la orden de servicio";
        ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
        panelContenido.ResumeLayout(false);
        panelCabecera.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)iconModulo).EndInit();
        panelCuerpo.ResumeLayout(false);
        panelPie.ResumeLayout(false);
        tablaExpediente.ResumeLayout(false);
        tablaResumen.ResumeLayout(false);
        paneltxtOrdenResumen.ResumeLayout(false);
        paneltxtOrdenResumen.PerformLayout();
        paneltxtClienteResumen.ResumeLayout(false);
        paneltxtClienteResumen.PerformLayout();
        paneltxtEquipoResumen.ResumeLayout(false);
        paneltxtEquipoResumen.PerformLayout();
        paneltxtEstadoResumen.ResumeLayout(false);
        paneltxtEstadoResumen.PerformLayout();
        paneltxtTecnicoResumen.ResumeLayout(false);
        paneltxtTecnicoResumen.PerformLayout();
        paneltxtSaldoResumen.ResumeLayout(false);
        paneltxtSaldoResumen.PerformLayout();
        tabsExpediente.ResumeLayout(false);
        tabRecepcion.ResumeLayout(false);
        paneltxtFechaRecepcionDetalle.ResumeLayout(false);
        paneltxtFechaRecepcionDetalle.PerformLayout();
        paneltxtRecepcionistaDetalle.ResumeLayout(false);
        paneltxtRecepcionistaDetalle.PerformLayout();
        paneltxtProblemaRecepcionDetalle.ResumeLayout(false);
        paneltxtProblemaRecepcionDetalle.PerformLayout();
        paneltxtAccesoriosDetalle.ResumeLayout(false);
        paneltxtAccesoriosDetalle.PerformLayout();
        paneltxtObservacionesDetalle.ResumeLayout(false);
        paneltxtObservacionesDetalle.PerformLayout();
        tabDiagnostico.ResumeLayout(false);
        panelcboTecnicoDiagnostico.ResumeLayout(false);
        paneltxtFechaDiagnostico.ResumeLayout(false);
        paneltxtFechaDiagnostico.PerformLayout();
        paneltxtProblemaEncontrado.ResumeLayout(false);
        paneltxtProblemaEncontrado.PerformLayout();
        paneltxtReparacionPropuesta.ResumeLayout(false);
        paneltxtReparacionPropuesta.PerformLayout();
        panelnumCostoEstimado.ResumeLayout(false);
        panelnumCostoEstimado.PerformLayout();
        panelcboDecisionCliente.ResumeLayout(false);
        panelnumCostoAprobado.ResumeLayout(false);
        panelnumCostoAprobado.PerformLayout();
        paneltxtFechaConfirmacion.ResumeLayout(false);
        paneltxtFechaConfirmacion.PerformLayout();
        paneltxtObservacionesDiagnostico.ResumeLayout(false);
        paneltxtObservacionesDiagnostico.PerformLayout();
        tabTrabajos.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvServiciosOrden).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvRepuestosOrden).EndInit();
        paneltxtTotalTrabajos.ResumeLayout(false);
        paneltxtTotalTrabajos.PerformLayout();
        panelcboResultadoReparacion.ResumeLayout(false);
        paneltxtObservacionesTrabajo.ResumeLayout(false);
        paneltxtObservacionesTrabajo.PerformLayout();
        tabPagos.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvPagosOrden).EndInit();
        paneltxtTotalPagos.ResumeLayout(false);
        paneltxtTotalPagos.PerformLayout();
        paneltxtPagadoOrden.ResumeLayout(false);
        paneltxtPagadoOrden.PerformLayout();
        paneltxtSaldoPagos.ResumeLayout(false);
        paneltxtSaldoPagos.PerformLayout();
        tabHistorial.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvHistorialOrden).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tablaExpediente = null!;
    private TableLayoutPanel tablaResumen = null!;
    private TabControl tabsExpediente = null!;
    private Panel paneltxtOrdenResumen = null!;
    private Label lbltxtOrdenResumen = null!;
    private TextBox txtOrdenResumen = null!;
    private Panel paneltxtClienteResumen = null!;
    private Label lbltxtClienteResumen = null!;
    private TextBox txtClienteResumen = null!;
    private Panel paneltxtEquipoResumen = null!;
    private Label lbltxtEquipoResumen = null!;
    private TextBox txtEquipoResumen = null!;
    private Panel paneltxtEstadoResumen = null!;
    private Label lbltxtEstadoResumen = null!;
    private TextBox txtEstadoResumen = null!;
    private Panel paneltxtTecnicoResumen = null!;
    private Label lbltxtTecnicoResumen = null!;
    private TextBox txtTecnicoResumen = null!;
    private Panel paneltxtSaldoResumen = null!;
    private Label lbltxtSaldoResumen = null!;
    private TextBox txtSaldoResumen = null!;
    private TabPage tabRecepcion = null!;
    private TabPage tabDiagnostico = null!;
    private TabPage tabTrabajos = null!;
    private TabPage tabPagos = null!;
    private TabPage tabHistorial = null!;
    private Panel paneltxtFechaRecepcionDetalle = null!;
    private Label lbltxtFechaRecepcionDetalle = null!;
    private TextBox txtFechaRecepcionDetalle = null!;
    private Panel paneltxtRecepcionistaDetalle = null!;
    private Label lbltxtRecepcionistaDetalle = null!;
    private TextBox txtRecepcionistaDetalle = null!;
    private Panel paneltxtProblemaRecepcionDetalle = null!;
    private Label lbltxtProblemaRecepcionDetalle = null!;
    private TextBox txtProblemaRecepcionDetalle = null!;
    private Panel paneltxtAccesoriosDetalle = null!;
    private Label lbltxtAccesoriosDetalle = null!;
    private TextBox txtAccesoriosDetalle = null!;
    private Panel paneltxtObservacionesDetalle = null!;
    private Label lbltxtObservacionesDetalle = null!;
    private TextBox txtObservacionesDetalle = null!;
    private Panel panelcboTecnicoDiagnostico = null!;
    private Label lblcboTecnicoDiagnostico = null!;
    private ComboBox cboTecnicoDiagnostico = null!;
    private Panel paneltxtFechaDiagnostico = null!;
    private Label lbltxtFechaDiagnostico = null!;
    private TextBox txtFechaDiagnostico = null!;
    private Panel paneltxtProblemaEncontrado = null!;
    private Label lbltxtProblemaEncontrado = null!;
    private TextBox txtProblemaEncontrado = null!;
    private Panel paneltxtReparacionPropuesta = null!;
    private Label lbltxtReparacionPropuesta = null!;
    private TextBox txtReparacionPropuesta = null!;
    private Panel panelnumCostoEstimado = null!;
    private Label lblnumCostoEstimado = null!;
    private TextBox numCostoEstimado = null!;
    private Panel panelcboDecisionCliente = null!;
    private Label lblcboDecisionCliente = null!;
    private ComboBox cboDecisionCliente = null!;
    private Panel panelnumCostoAprobado = null!;
    private Label lblnumCostoAprobado = null!;
    private TextBox numCostoAprobado = null!;
    private Panel paneltxtFechaConfirmacion = null!;
    private Label lbltxtFechaConfirmacion = null!;
    private TextBox txtFechaConfirmacion = null!;
    private Panel paneltxtObservacionesDiagnostico = null!;
    private Label lbltxtObservacionesDiagnostico = null!;
    private TextBox txtObservacionesDiagnostico = null!;
    private Label lblServiciosOrden = null!;
    private FontAwesome.Sharp.IconButton btnAgregarServicio = null!;
    private DataGridView dgvServiciosOrden = null!;
    private DataGridViewTextBoxColumn dgvServiciosOrdenCol0 = null!;
    private DataGridViewTextBoxColumn dgvServiciosOrdenCol1 = null!;
    private DataGridViewTextBoxColumn dgvServiciosOrdenCol2 = null!;
    private DataGridViewTextBoxColumn dgvServiciosOrdenCol3 = null!;
    private Label lblServiciosVacios = null!;
    private Label lblRepuestosOrden = null!;
    private FontAwesome.Sharp.IconButton btnAgregarRepuesto = null!;
    private DataGridView dgvRepuestosOrden = null!;
    private DataGridViewTextBoxColumn dgvRepuestosOrdenCol0 = null!;
    private DataGridViewTextBoxColumn dgvRepuestosOrdenCol1 = null!;
    private DataGridViewTextBoxColumn dgvRepuestosOrdenCol2 = null!;
    private DataGridViewTextBoxColumn dgvRepuestosOrdenCol3 = null!;
    private Label lblRepuestosVacios = null!;
    private Panel paneltxtTotalTrabajos = null!;
    private Label lbltxtTotalTrabajos = null!;
    private TextBox txtTotalTrabajos = null!;
    private Panel panelcboResultadoReparacion = null!;
    private Label lblcboResultadoReparacion = null!;
    private ComboBox cboResultadoReparacion = null!;
    private Panel paneltxtObservacionesTrabajo = null!;
    private Label lbltxtObservacionesTrabajo = null!;
    private TextBox txtObservacionesTrabajo = null!;
    private Label lblCobrosOrden = null!;
    private FontAwesome.Sharp.IconButton btnRegistrarPagoOrden = null!;
    private DataGridView dgvPagosOrden = null!;
    private DataGridViewTextBoxColumn dgvPagosOrdenCol0 = null!;
    private DataGridViewTextBoxColumn dgvPagosOrdenCol1 = null!;
    private DataGridViewTextBoxColumn dgvPagosOrdenCol2 = null!;
    private DataGridViewTextBoxColumn dgvPagosOrdenCol3 = null!;
    private DataGridViewTextBoxColumn dgvPagosOrdenCol4 = null!;
    private Label lblPagosVacios = null!;
    private Panel paneltxtTotalPagos = null!;
    private Label lbltxtTotalPagos = null!;
    private TextBox txtTotalPagos = null!;
    private Panel paneltxtPagadoOrden = null!;
    private Label lbltxtPagadoOrden = null!;
    private TextBox txtPagadoOrden = null!;
    private Panel paneltxtSaldoPagos = null!;
    private Label lbltxtSaldoPagos = null!;
    private TextBox txtSaldoPagos = null!;
    private Label lblHistorialTitulo = null!;
    private Label lblHistorialDescripcion = null!;
    private DataGridView dgvHistorialOrden = null!;
    private DataGridViewTextBoxColumn dgvHistorialOrdenCol0 = null!;
    private DataGridViewTextBoxColumn dgvHistorialOrdenCol1 = null!;
    private DataGridViewTextBoxColumn dgvHistorialOrdenCol2 = null!;
    private DataGridViewTextBoxColumn dgvHistorialOrdenCol3 = null!;
    private DataGridViewTextBoxColumn dgvHistorialOrdenCol4 = null!;
    private Label lblHistorialVacio = null!;
}
