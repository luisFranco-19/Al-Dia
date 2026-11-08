namespace AlDia.UI.Formularios.Hijos.OrdenesServicio;

partial class FrmRegistroOrdenServicio
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
        tablaFormulario = new TableLayoutPanel();
        lblSeccionEquipo = new Label();
        panelcboCliente = new Panel();
        lblcboCliente = new Label();
        cboCliente = new ComboBox();
        panelcboEquipo = new Panel();
        lblcboEquipo = new Label();
        cboEquipo = new ComboBox();
        lblReferenciaEquipo = new Label();
        lblSeccionRecepcion = new Label();
        paneltxtNumeroOrden = new Panel();
        lbltxtNumeroOrden = new Label();
        txtNumeroOrden = new TextBox();
        paneldtpFechaRecepcion = new Panel();
        lbldtpFechaRecepcion = new Label();
        dtpFechaRecepcion = new DateTimePicker();
        panelcboRecepcionista = new Panel();
        lblcboRecepcionista = new Label();
        cboRecepcionista = new ComboBox();
        paneltxtEstadoInicial = new Panel();
        lbltxtEstadoInicial = new Label();
        txtEstadoInicial = new TextBox();
        paneltxtProblemaReportado = new Panel();
        lbltxtProblemaReportado = new Label();
        txtProblemaReportado = new TextBox();
        paneltxtAccesoriosRecepcion = new Panel();
        lbltxtAccesoriosRecepcion = new Label();
        txtAccesoriosRecepcion = new TextBox();
        paneltxtObservacionesRecepcion = new Panel();
        lbltxtObservacionesRecepcion = new Label();
        txtObservacionesRecepcion = new TextBox();
        lblcboTecnicoAsignado = new Label();
        cboTecnicoAsignado = new ComboBox();
        lblNotaRecepcion = new Label();
        ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
        panelContenido.SuspendLayout();
        panelCabecera.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)iconModulo).BeginInit();
        panelCuerpo.SuspendLayout();
        panelPie.SuspendLayout();
        tablaFormulario.SuspendLayout();
        panelcboCliente.SuspendLayout();
        panelcboEquipo.SuspendLayout();
        paneltxtNumeroOrden.SuspendLayout();
        paneldtpFechaRecepcion.SuspendLayout();
        panelcboRecepcionista.SuspendLayout();
        paneltxtEstadoInicial.SuspendLayout();
        paneltxtProblemaReportado.SuspendLayout();
        paneltxtAccesoriosRecepcion.SuspendLayout();
        paneltxtObservacionesRecepcion.SuspendLayout();
        SuspendLayout();
        // 
        // panelContenido
        // 
        panelContenido.Size = new Size(940, 850);
        // 
        // panelCabecera
        // 
        panelCabecera.Size = new Size(940, 139);
        // 
        // iconModulo
        // 
        iconModulo.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
        // 
        // lblTitulo
        // 
        lblTitulo.Text = "Nueva orden de servicio";
        // 
        // lblDescripcion
        // 
        lblDescripcion.Text = "Registre la recepción del equipo y los datos entregados por el cliente.";
        // 
        // btnCerrar
        // 
        btnCerrar.FlatAppearance.BorderSize = 0;
        btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCerrar.Location = new Point(890, 4);
        // 
        // panelCuerpo
        // 
        panelCuerpo.Controls.Add(tablaFormulario);
        panelCuerpo.Size = new Size(940, 604);
        // 
        // panelPie
        // 
        panelPie.Location = new Point(0, 743);
        panelPie.Size = new Size(940, 107);
        // 
        // lineaPie
        // 
        lineaPie.Size = new Size(940, 1);
        // 
        // btnCancelar
        // 
        btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnCancelar.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 236, 243);
        btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCancelar.Location = new Point(573, 27);
        // 
        // btnGuardar
        // 
        btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnGuardar.FlatAppearance.BorderSize = 0;
        btnGuardar.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
        btnGuardar.Location = new Point(737, 27);
        btnGuardar.Text = "Guardar orden";
        // 
        // tablaFormulario
        // 
        tablaFormulario.ColumnCount = 2;
        tablaFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaFormulario.Controls.Add(lblSeccionEquipo, 0, 0);
        tablaFormulario.Controls.Add(panelcboCliente, 0, 1);
        tablaFormulario.Controls.Add(panelcboEquipo, 1, 1);
        tablaFormulario.Controls.Add(lblReferenciaEquipo, 0, 2);
        tablaFormulario.Controls.Add(lblSeccionRecepcion, 0, 3);
        tablaFormulario.Controls.Add(paneltxtNumeroOrden, 0, 4);
        tablaFormulario.Controls.Add(paneldtpFechaRecepcion, 1, 4);
        tablaFormulario.Controls.Add(panelcboRecepcionista, 0, 5);
        tablaFormulario.Controls.Add(paneltxtEstadoInicial, 1, 5);
        tablaFormulario.Controls.Add(paneltxtProblemaReportado, 0, 6);
        tablaFormulario.Controls.Add(paneltxtAccesoriosRecepcion, 0, 7);
        tablaFormulario.Controls.Add(paneltxtObservacionesRecepcion, 0, 8);
        tablaFormulario.Controls.Add(lblNotaRecepcion, 0, 9);
        tablaFormulario.Dock = DockStyle.Top;
        tablaFormulario.Location = new Point(32, 32);
        tablaFormulario.Margin = new Padding(0);
        tablaFormulario.Name = "tablaFormulario";
        tablaFormulario.RowCount = 11;
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 124F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 124F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        tablaFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 82F));
        tablaFormulario.Size = new Size(855, 862);
        tablaFormulario.TabIndex = 0;
        // 
        // lblSeccionEquipo
        // 
        tablaFormulario.SetColumnSpan(lblSeccionEquipo, 2);
        lblSeccionEquipo.Dock = DockStyle.Fill;
        lblSeccionEquipo.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblSeccionEquipo.ForeColor = Color.FromArgb(20, 43, 78);
        lblSeccionEquipo.Location = new Point(0, 12);
        lblSeccionEquipo.Margin = new Padding(0, 12, 0, 0);
        lblSeccionEquipo.Name = "lblSeccionEquipo";
        lblSeccionEquipo.Size = new Size(855, 36);
        lblSeccionEquipo.TabIndex = 0;
        lblSeccionEquipo.Text = "01  Cliente y equipo";
        lblSeccionEquipo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // panelcboCliente
        // 
        panelcboCliente.Controls.Add(lblcboCliente);
        panelcboCliente.Controls.Add(cboCliente);
        panelcboCliente.Dock = DockStyle.Fill;
        panelcboCliente.Location = new Point(0, 48);
        panelcboCliente.Margin = new Padding(0, 0, 16, 0);
        panelcboCliente.Name = "panelcboCliente";
        panelcboCliente.Size = new Size(411, 82);
        panelcboCliente.TabIndex = 2;
        // 
        // lblcboCliente
        // 
        lblcboCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblcboCliente.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblcboCliente.ForeColor = Color.FromArgb(20, 43, 78);
        lblcboCliente.Location = new Point(0, 6);
        lblcboCliente.Name = "lblcboCliente";
        lblcboCliente.Size = new Size(411, 22);
        lblcboCliente.TabIndex = 0;
        lblcboCliente.Text = "Cliente *";
        // 
        // cboCliente
        // 
        cboCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboCliente.BackColor = Color.White;
        cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCliente.FlatStyle = FlatStyle.Flat;
        cboCliente.Font = new Font("Segoe UI", 10F);
        cboCliente.ForeColor = Color.FromArgb(20, 43, 78);
        cboCliente.Items.AddRange(new object[] { "Selecciona un cliente" });
        cboCliente.Location = new Point(0, 34);
        cboCliente.Name = "cboCliente";
        cboCliente.Size = new Size(411, 31);
        cboCliente.TabIndex = 0;
        // 
        // panelcboEquipo
        // 
        panelcboEquipo.Controls.Add(lblcboEquipo);
        panelcboEquipo.Controls.Add(cboEquipo);
        panelcboEquipo.Dock = DockStyle.Fill;
        panelcboEquipo.Location = new Point(427, 48);
        panelcboEquipo.Margin = new Padding(0);
        panelcboEquipo.Name = "panelcboEquipo";
        panelcboEquipo.Size = new Size(428, 82);
        panelcboEquipo.TabIndex = 3;
        // 
        // lblcboEquipo
        // 
        lblcboEquipo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblcboEquipo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblcboEquipo.ForeColor = Color.FromArgb(20, 43, 78);
        lblcboEquipo.Location = new Point(0, 6);
        lblcboEquipo.Name = "lblcboEquipo";
        lblcboEquipo.Size = new Size(428, 22);
        lblcboEquipo.TabIndex = 0;
        lblcboEquipo.Text = "Equipo del cliente *";
        // 
        // cboEquipo
        // 
        cboEquipo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboEquipo.BackColor = Color.White;
        cboEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEquipo.FlatStyle = FlatStyle.Flat;
        cboEquipo.Font = new Font("Segoe UI", 10F);
        cboEquipo.ForeColor = Color.FromArgb(20, 43, 78);
        cboEquipo.Items.AddRange(new object[] { "Selecciona un equipo" });
        cboEquipo.Location = new Point(0, 34);
        cboEquipo.Name = "cboEquipo";
        cboEquipo.Size = new Size(428, 31);
        cboEquipo.TabIndex = 0;
        // 
        // lblReferenciaEquipo
        // 
        tablaFormulario.SetColumnSpan(lblReferenciaEquipo, 2);
        lblReferenciaEquipo.Dock = DockStyle.Fill;
        lblReferenciaEquipo.Font = new Font("Segoe UI", 9F);
        lblReferenciaEquipo.ForeColor = Color.FromArgb(103, 116, 137);
        lblReferenciaEquipo.Location = new Point(0, 134);
        lblReferenciaEquipo.Margin = new Padding(0, 4, 0, 0);
        lblReferenciaEquipo.Name = "lblReferenciaEquipo";
        lblReferenciaEquipo.Size = new Size(855, 38);
        lblReferenciaEquipo.TabIndex = 4;
        lblReferenciaEquipo.Text = "Equipo seleccionado: marca, modelo y número de serie aparecerán aquí.";
        lblReferenciaEquipo.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // lblSeccionRecepcion
        // 
        tablaFormulario.SetColumnSpan(lblSeccionRecepcion, 2);
        lblSeccionRecepcion.Dock = DockStyle.Fill;
        lblSeccionRecepcion.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblSeccionRecepcion.ForeColor = Color.FromArgb(20, 43, 78);
        lblSeccionRecepcion.Location = new Point(0, 184);
        lblSeccionRecepcion.Margin = new Padding(0, 12, 0, 0);
        lblSeccionRecepcion.Name = "lblSeccionRecepcion";
        lblSeccionRecepcion.Size = new Size(855, 36);
        lblSeccionRecepcion.TabIndex = 5;
        lblSeccionRecepcion.Text = "02  Datos de recepción";
        lblSeccionRecepcion.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // paneltxtNumeroOrden
        // 
        paneltxtNumeroOrden.Controls.Add(lbltxtNumeroOrden);
        paneltxtNumeroOrden.Controls.Add(txtNumeroOrden);
        paneltxtNumeroOrden.Dock = DockStyle.Fill;
        paneltxtNumeroOrden.Location = new Point(0, 220);
        paneltxtNumeroOrden.Margin = new Padding(0, 0, 16, 0);
        paneltxtNumeroOrden.Name = "paneltxtNumeroOrden";
        paneltxtNumeroOrden.Size = new Size(411, 82);
        paneltxtNumeroOrden.TabIndex = 8;
        // 
        // lbltxtNumeroOrden
        // 
        lbltxtNumeroOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtNumeroOrden.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtNumeroOrden.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtNumeroOrden.Location = new Point(0, 6);
        lbltxtNumeroOrden.Name = "lbltxtNumeroOrden";
        lbltxtNumeroOrden.Size = new Size(411, 22);
        lbltxtNumeroOrden.TabIndex = 0;
        lbltxtNumeroOrden.Text = "N.º de orden *";
        // 
        // txtNumeroOrden
        // 
        txtNumeroOrden.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtNumeroOrden.BackColor = Color.White;
        txtNumeroOrden.BorderStyle = BorderStyle.FixedSingle;
        txtNumeroOrden.Font = new Font("Segoe UI", 10F);
        txtNumeroOrden.ForeColor = Color.FromArgb(20, 43, 78);
        txtNumeroOrden.Location = new Point(0, 34);
        txtNumeroOrden.MaxLength = 20;
        txtNumeroOrden.Name = "txtNumeroOrden";
        txtNumeroOrden.PlaceholderText = "Ej. OS-000001";
        txtNumeroOrden.Size = new Size(411, 30);
        txtNumeroOrden.TabIndex = 0;
        // 
        // paneldtpFechaRecepcion
        // 
        paneldtpFechaRecepcion.Controls.Add(lbldtpFechaRecepcion);
        paneldtpFechaRecepcion.Controls.Add(dtpFechaRecepcion);
        paneldtpFechaRecepcion.Dock = DockStyle.Fill;
        paneldtpFechaRecepcion.Location = new Point(427, 220);
        paneldtpFechaRecepcion.Margin = new Padding(0);
        paneldtpFechaRecepcion.Name = "paneldtpFechaRecepcion";
        paneldtpFechaRecepcion.Size = new Size(428, 82);
        paneldtpFechaRecepcion.TabIndex = 9;
        // 
        // lbldtpFechaRecepcion
        // 
        lbldtpFechaRecepcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbldtpFechaRecepcion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbldtpFechaRecepcion.ForeColor = Color.FromArgb(20, 43, 78);
        lbldtpFechaRecepcion.Location = new Point(0, 6);
        lbldtpFechaRecepcion.Name = "lbldtpFechaRecepcion";
        lbldtpFechaRecepcion.Size = new Size(428, 22);
        lbldtpFechaRecepcion.TabIndex = 0;
        lbldtpFechaRecepcion.Text = "Fecha y hora de recepción";
        // 
        // dtpFechaRecepcion
        // 
        dtpFechaRecepcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dtpFechaRecepcion.BackColor = Color.White;
        dtpFechaRecepcion.CustomFormat = "dd/MM/yyyy HH:mm";
        dtpFechaRecepcion.Font = new Font("Segoe UI", 10F);
        dtpFechaRecepcion.ForeColor = Color.FromArgb(20, 43, 78);
        dtpFechaRecepcion.Format = DateTimePickerFormat.Custom;
        dtpFechaRecepcion.Location = new Point(0, 34);
        dtpFechaRecepcion.Name = "dtpFechaRecepcion";
        dtpFechaRecepcion.Size = new Size(428, 30);
        dtpFechaRecepcion.TabIndex = 0;
        // 
        // panelcboRecepcionista
        // 
        panelcboRecepcionista.Controls.Add(lblcboRecepcionista);
        panelcboRecepcionista.Controls.Add(cboRecepcionista);
        panelcboRecepcionista.Dock = DockStyle.Fill;
        panelcboRecepcionista.Location = new Point(0, 302);
        panelcboRecepcionista.Margin = new Padding(0, 0, 16, 0);
        panelcboRecepcionista.Name = "panelcboRecepcionista";
        panelcboRecepcionista.Size = new Size(411, 82);
        panelcboRecepcionista.TabIndex = 10;
        // 
        // lblcboRecepcionista
        // 
        lblcboRecepcionista.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblcboRecepcionista.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblcboRecepcionista.ForeColor = Color.FromArgb(20, 43, 78);
        lblcboRecepcionista.Location = new Point(0, 6);
        lblcboRecepcionista.Name = "lblcboRecepcionista";
        lblcboRecepcionista.Size = new Size(411, 22);
        lblcboRecepcionista.TabIndex = 0;
        lblcboRecepcionista.Text = "Recibido por *";
        // 
        // cboRecepcionista
        // 
        cboRecepcionista.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboRecepcionista.BackColor = Color.White;
        cboRecepcionista.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRecepcionista.FlatStyle = FlatStyle.Flat;
        cboRecepcionista.Font = new Font("Segoe UI", 10F);
        cboRecepcionista.ForeColor = Color.FromArgb(20, 43, 78);
        cboRecepcionista.Items.AddRange(new object[] { "Selecciona un recepcionista" });
        cboRecepcionista.Location = new Point(0, 34);
        cboRecepcionista.Name = "cboRecepcionista";
        cboRecepcionista.Size = new Size(411, 31);
        cboRecepcionista.TabIndex = 0;
        // 
        // paneltxtEstadoInicial
        // 
        paneltxtEstadoInicial.Controls.Add(lbltxtEstadoInicial);
        paneltxtEstadoInicial.Controls.Add(txtEstadoInicial);
        paneltxtEstadoInicial.Dock = DockStyle.Fill;
        paneltxtEstadoInicial.Location = new Point(427, 302);
        paneltxtEstadoInicial.Margin = new Padding(0);
        paneltxtEstadoInicial.Name = "paneltxtEstadoInicial";
        paneltxtEstadoInicial.Size = new Size(428, 82);
        paneltxtEstadoInicial.TabIndex = 11;
        // 
        // lbltxtEstadoInicial
        // 
        lbltxtEstadoInicial.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtEstadoInicial.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtEstadoInicial.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtEstadoInicial.Location = new Point(0, 6);
        lbltxtEstadoInicial.Name = "lbltxtEstadoInicial";
        lbltxtEstadoInicial.Size = new Size(428, 22);
        lbltxtEstadoInicial.TabIndex = 0;
        lbltxtEstadoInicial.Text = "Estado inicial";
        // 
        // txtEstadoInicial
        // 
        txtEstadoInicial.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtEstadoInicial.BackColor = Color.FromArgb(246, 248, 251);
        txtEstadoInicial.BorderStyle = BorderStyle.FixedSingle;
        txtEstadoInicial.Font = new Font("Segoe UI", 10F);
        txtEstadoInicial.ForeColor = Color.FromArgb(20, 43, 78);
        txtEstadoInicial.Location = new Point(0, 34);
        txtEstadoInicial.MaxLength = 100;
        txtEstadoInicial.Name = "txtEstadoInicial";
        txtEstadoInicial.ReadOnly = true;
        txtEstadoInicial.Size = new Size(428, 30);
        txtEstadoInicial.TabIndex = 0;
        txtEstadoInicial.TabStop = false;
        txtEstadoInicial.Text = "En Revisión";
        // 
        // paneltxtProblemaReportado
        // 
        tablaFormulario.SetColumnSpan(paneltxtProblemaReportado, 2);
        paneltxtProblemaReportado.Controls.Add(cboTecnicoAsignado);
        paneltxtProblemaReportado.Controls.Add(lblcboTecnicoAsignado);
        paneltxtProblemaReportado.Controls.Add(lbltxtProblemaReportado);
        paneltxtProblemaReportado.Controls.Add(txtProblemaReportado);
        paneltxtProblemaReportado.Dock = DockStyle.Fill;
        paneltxtProblemaReportado.Location = new Point(0, 384);
        paneltxtProblemaReportado.Margin = new Padding(0);
        paneltxtProblemaReportado.Name = "paneltxtProblemaReportado";
        paneltxtProblemaReportado.Size = new Size(855, 124);
        paneltxtProblemaReportado.TabIndex = 12;
        // 
        // lbltxtProblemaReportado
        // 
        lbltxtProblemaReportado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtProblemaReportado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtProblemaReportado.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtProblemaReportado.Location = new Point(0, 6);
        lbltxtProblemaReportado.Name = "lbltxtProblemaReportado";
        lbltxtProblemaReportado.Size = new Size(855, 22);
        lbltxtProblemaReportado.TabIndex = 0;
        lbltxtProblemaReportado.Text = "Problema reportado por el cliente *";
        // 
        // txtProblemaReportado
        // 
        txtProblemaReportado.AcceptsReturn = true;
        txtProblemaReportado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtProblemaReportado.BackColor = Color.White;
        txtProblemaReportado.BorderStyle = BorderStyle.FixedSingle;
        txtProblemaReportado.Font = new Font("Segoe UI", 10F);
        txtProblemaReportado.ForeColor = Color.FromArgb(20, 43, 78);
        txtProblemaReportado.Location = new Point(0, 34);
        txtProblemaReportado.MaxLength = 500;
        txtProblemaReportado.Multiline = true;
        txtProblemaReportado.Name = "txtProblemaReportado";
        txtProblemaReportado.PlaceholderText = "Describe la falla o el motivo de ingreso del equipo.";
        txtProblemaReportado.ScrollBars = ScrollBars.Vertical;
        txtProblemaReportado.Size = new Size(458, 77);
        txtProblemaReportado.TabIndex = 0;
        // 
        // paneltxtAccesoriosRecepcion
        // 
        tablaFormulario.SetColumnSpan(paneltxtAccesoriosRecepcion, 2);
        paneltxtAccesoriosRecepcion.Controls.Add(lbltxtAccesoriosRecepcion);
        paneltxtAccesoriosRecepcion.Controls.Add(txtAccesoriosRecepcion);
        paneltxtAccesoriosRecepcion.Dock = DockStyle.Fill;
        paneltxtAccesoriosRecepcion.Location = new Point(0, 508);
        paneltxtAccesoriosRecepcion.Margin = new Padding(0);
        paneltxtAccesoriosRecepcion.Name = "paneltxtAccesoriosRecepcion";
        paneltxtAccesoriosRecepcion.Size = new Size(855, 104);
        paneltxtAccesoriosRecepcion.TabIndex = 14;
        // 
        // lbltxtAccesoriosRecepcion
        // 
        lbltxtAccesoriosRecepcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtAccesoriosRecepcion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtAccesoriosRecepcion.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtAccesoriosRecepcion.Location = new Point(0, 6);
        lbltxtAccesoriosRecepcion.Name = "lbltxtAccesoriosRecepcion";
        lbltxtAccesoriosRecepcion.Size = new Size(855, 22);
        lbltxtAccesoriosRecepcion.TabIndex = 0;
        lbltxtAccesoriosRecepcion.Text = "Accesorios recibidos";
        // 
        // txtAccesoriosRecepcion
        // 
        txtAccesoriosRecepcion.AcceptsReturn = true;
        txtAccesoriosRecepcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtAccesoriosRecepcion.BackColor = Color.White;
        txtAccesoriosRecepcion.BorderStyle = BorderStyle.FixedSingle;
        txtAccesoriosRecepcion.Font = new Font("Segoe UI", 10F);
        txtAccesoriosRecepcion.ForeColor = Color.FromArgb(20, 43, 78);
        txtAccesoriosRecepcion.Location = new Point(0, 34);
        txtAccesoriosRecepcion.MaxLength = 300;
        txtAccesoriosRecepcion.Multiline = true;
        txtAccesoriosRecepcion.Name = "txtAccesoriosRecepcion";
        txtAccesoriosRecepcion.PlaceholderText = "Cargador, cables, funda u otros accesorios entregados.";
        txtAccesoriosRecepcion.ScrollBars = ScrollBars.Vertical;
        txtAccesoriosRecepcion.Size = new Size(458, 57);
        txtAccesoriosRecepcion.TabIndex = 0;
        // 
        // paneltxtObservacionesRecepcion
        // 
        tablaFormulario.SetColumnSpan(paneltxtObservacionesRecepcion, 2);
        paneltxtObservacionesRecepcion.Controls.Add(lbltxtObservacionesRecepcion);
        paneltxtObservacionesRecepcion.Controls.Add(txtObservacionesRecepcion);
        paneltxtObservacionesRecepcion.Dock = DockStyle.Fill;
        paneltxtObservacionesRecepcion.Location = new Point(0, 612);
        paneltxtObservacionesRecepcion.Margin = new Padding(0);
        paneltxtObservacionesRecepcion.Name = "paneltxtObservacionesRecepcion";
        paneltxtObservacionesRecepcion.Size = new Size(855, 124);
        paneltxtObservacionesRecepcion.TabIndex = 16;
        // 
        // lbltxtObservacionesRecepcion
        // 
        lbltxtObservacionesRecepcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbltxtObservacionesRecepcion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lbltxtObservacionesRecepcion.ForeColor = Color.FromArgb(20, 43, 78);
        lbltxtObservacionesRecepcion.Location = new Point(0, 6);
        lbltxtObservacionesRecepcion.Name = "lbltxtObservacionesRecepcion";
        lbltxtObservacionesRecepcion.Size = new Size(855, 22);
        lbltxtObservacionesRecepcion.TabIndex = 0;
        lbltxtObservacionesRecepcion.Text = "Observaciones de recepción";
        // 
        // txtObservacionesRecepcion
        // 
        txtObservacionesRecepcion.AcceptsReturn = true;
        txtObservacionesRecepcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtObservacionesRecepcion.BackColor = Color.White;
        txtObservacionesRecepcion.BorderStyle = BorderStyle.FixedSingle;
        txtObservacionesRecepcion.Font = new Font("Segoe UI", 10F);
        txtObservacionesRecepcion.ForeColor = Color.FromArgb(20, 43, 78);
        txtObservacionesRecepcion.Location = new Point(0, 34);
        txtObservacionesRecepcion.MaxLength = 500;
        txtObservacionesRecepcion.Multiline = true;
        txtObservacionesRecepcion.Name = "txtObservacionesRecepcion";
        txtObservacionesRecepcion.PlaceholderText = "Estado físico, daños visibles y detalles adicionales.";
        txtObservacionesRecepcion.ScrollBars = ScrollBars.Vertical;
        txtObservacionesRecepcion.Size = new Size(462, 77);
        txtObservacionesRecepcion.TabIndex = 0;
        // 
        // lblcboTecnicoAsignado
        // 
        lblcboTecnicoAsignado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblcboTecnicoAsignado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblcboTecnicoAsignado.ForeColor = Color.FromArgb(20, 43, 78);
        lblcboTecnicoAsignado.Location = new Point(473, 6);
        lblcboTecnicoAsignado.Name = "lblcboTecnicoAsignado";
        lblcboTecnicoAsignado.Size = new Size(140, 22);
        lblcboTecnicoAsignado.TabIndex = 0;
        lblcboTecnicoAsignado.Text = "Técnico asignado *";
        // 
        // cboTecnicoAsignado
        // 
        cboTecnicoAsignado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboTecnicoAsignado.BackColor = Color.White;
        cboTecnicoAsignado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTecnicoAsignado.FlatStyle = FlatStyle.Flat;
        cboTecnicoAsignado.Font = new Font("Segoe UI", 10F);
        cboTecnicoAsignado.ForeColor = Color.FromArgb(20, 43, 78);
        cboTecnicoAsignado.Location = new Point(473, 34);
        cboTecnicoAsignado.Name = "cboTecnicoAsignado";
        cboTecnicoAsignado.Size = new Size(270, 31);
        cboTecnicoAsignado.TabIndex = 0;
        // 
        // lblNotaRecepcion
        // 
        tablaFormulario.SetColumnSpan(lblNotaRecepcion, 2);
        lblNotaRecepcion.Dock = DockStyle.Fill;
        lblNotaRecepcion.Font = new Font("Segoe UI", 9F);
        lblNotaRecepcion.ForeColor = Color.FromArgb(103, 116, 137);
        lblNotaRecepcion.Location = new Point(0, 740);
        lblNotaRecepcion.Margin = new Padding(0, 4, 0, 0);
        lblNotaRecepcion.Name = "lblNotaRecepcion";
        lblNotaRecepcion.Size = new Size(855, 40);
        lblNotaRecepcion.TabIndex = 17;
        lblNotaRecepcion.Text = "Revisa la información del cliente, el equipo y los accesorios recibidos.";
        lblNotaRecepcion.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // FrmRegistroOrdenServicio
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        ClientSize = new Size(940, 850);
        Name = "FrmRegistroOrdenServicio";
        Text = "Nueva orden de servicio";
        ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
        panelContenido.ResumeLayout(false);
        panelCabecera.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)iconModulo).EndInit();
        panelCuerpo.ResumeLayout(false);
        panelPie.ResumeLayout(false);
        tablaFormulario.ResumeLayout(false);
        panelcboCliente.ResumeLayout(false);
        panelcboEquipo.ResumeLayout(false);
        paneltxtNumeroOrden.ResumeLayout(false);
        paneltxtNumeroOrden.PerformLayout();
        paneldtpFechaRecepcion.ResumeLayout(false);
        panelcboRecepcionista.ResumeLayout(false);
        paneltxtEstadoInicial.ResumeLayout(false);
        paneltxtEstadoInicial.PerformLayout();
        paneltxtProblemaReportado.ResumeLayout(false);
        paneltxtProblemaReportado.PerformLayout();
        paneltxtAccesoriosRecepcion.ResumeLayout(false);
        paneltxtAccesoriosRecepcion.PerformLayout();
        paneltxtObservacionesRecepcion.ResumeLayout(false);
        paneltxtObservacionesRecepcion.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tablaFormulario = null!;
    private Label lblSeccionEquipo = null!;
    private Panel panelcboCliente = null!;
    private Label lblcboCliente = null!;
    private ComboBox cboCliente = null!;
    private Panel panelcboEquipo = null!;
    private Label lblcboEquipo = null!;
    private ComboBox cboEquipo = null!;
    private Label lblReferenciaEquipo = null!;
    private Label lblSeccionRecepcion = null!;
    private Panel paneltxtNumeroOrden = null!;
    private Label lbltxtNumeroOrden = null!;
    private TextBox txtNumeroOrden = null!;
    private Panel paneldtpFechaRecepcion = null!;
    private Label lbldtpFechaRecepcion = null!;
    private DateTimePicker dtpFechaRecepcion = null!;
    private Panel panelcboRecepcionista = null!;
    private Label lblcboRecepcionista = null!;
    private ComboBox cboRecepcionista = null!;
    private Panel paneltxtEstadoInicial = null!;
    private Label lbltxtEstadoInicial = null!;
    private TextBox txtEstadoInicial = null!;
    private Panel paneltxtProblemaReportado = null!;
    private Label lbltxtProblemaReportado = null!;
    private TextBox txtProblemaReportado = null!;
    private Panel paneltxtAccesoriosRecepcion = null!;
    private Label lbltxtAccesoriosRecepcion = null!;
    private TextBox txtAccesoriosRecepcion = null!;
    private Panel paneltxtObservacionesRecepcion = null!;
    private Label lbltxtObservacionesRecepcion = null!;
    private TextBox txtObservacionesRecepcion = null!;
    private Label lblcboTecnicoAsignado = null!;
    private ComboBox cboTecnicoAsignado = null!;
    private Label lblNotaRecepcion;
}
