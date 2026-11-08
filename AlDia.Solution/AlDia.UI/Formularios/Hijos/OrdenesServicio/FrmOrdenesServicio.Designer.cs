namespace AlDia.UI.Formularios.Hijos.OrdenesServicio;

partial class FrmOrdenesServicio
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
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        tablaFiltrosModulo = new TableLayoutPanel();
        panelFiltroEstado = new Panel();
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();
        panelFiltroAsignacion = new Panel();
        lblFiltroAsignacion = new Label();
        cboFiltroAsignacion = new ComboBox();
        colNumeroOrden = new DataGridViewTextBoxColumn();
        colCliente = new DataGridViewTextBoxColumn();
        colEquipo = new DataGridViewTextBoxColumn();
        colFechaRecepcion = new DataGridViewTextBoxColumn();
        colTecnico = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        colTotal = new DataGridViewTextBoxColumn();
        colSaldo = new DataGridViewTextBoxColumn();
        bordeBuscar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)iconBuscar).BeginInit();
        tablaFiltrosModulo.SuspendLayout();
        panelFiltroEstado.SuspendLayout();
        panelFiltroAsignacion.SuspendLayout();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.Text = "Órdenes de servicio";
        // 
        // lblDescripcion
        // 
        lblDescripcion.Text = "Recepción, seguimiento y expediente de los equipos del taller.";
        // 
        // btnNuevo
        // 
        btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnNuevo.FlatAppearance.BorderSize = 0;
        btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
        btnNuevo.Text = "Nueva orden";
        // 
        // txtBuscar
        // 
        txtBuscar.PlaceholderText = "N.º de orden, cliente o equipo";
        // 
        // btnBuscar
        // 
        btnBuscar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnBuscar.Location = new Point(0, 12);
        // 
        // lblListado
        // 
        lblListado.Text = "Órdenes del taller";
        // 
        // btnEditar
        // 
        btnEditar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnEditar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnEditar.Location = new Point(844, 19);
        btnEditar.Size = new Size(155, 53);
        btnEditar.Text = "Ver expediente";
        // 
        // btnCambiarEstado
        // 
        btnCambiarEstado.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnCambiarEstado.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCambiarEstado.IconChar = FontAwesome.Sharp.IconChar.Cancel;
        btnCambiarEstado.Text = "Anular orden";
        // 
        // btnAnterior
        // 
        btnAnterior.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnAnterior.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        // 
        // btnSiguiente
        // 
        btnSiguiente.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnSiguiente.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        // 
        // tablaFiltrosModulo
        // 
        tablaFiltrosModulo.ColumnCount = 2;
        tablaFiltrosModulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaFiltrosModulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaFiltrosModulo.Controls.Add(panelFiltroEstado, 0, 0);
        tablaFiltrosModulo.Controls.Add(panelFiltroAsignacion, 1, 0);
        tablaFiltrosModulo.Dock = DockStyle.Fill;
        tablaFiltrosModulo.Location = new Point(0, 0);
        tablaFiltrosModulo.Margin = new Padding(0);
        tablaFiltrosModulo.Name = "tablaFiltrosModulo";
        tablaFiltrosModulo.RowCount = 1;
        tablaFiltrosModulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablaFiltrosModulo.Size = new Size(425, 95);
        tablaFiltrosModulo.TabIndex = 0;
        // 
        // panelFiltroEstado
        // 
        panelFiltroEstado.Controls.Add(lblFiltroEstado);
        panelFiltroEstado.Controls.Add(cboFiltroEstado);
        panelFiltroEstado.Dock = DockStyle.Fill;
        panelFiltroEstado.Location = new Point(0, 0);
        panelFiltroEstado.Margin = new Padding(0, 0, 12, 0);
        panelFiltroEstado.Name = "panelFiltroEstado";
        panelFiltroEstado.Size = new Size(200, 95);
        panelFiltroEstado.TabIndex = 0;
        // 
        // lblFiltroEstado
        // 
        lblFiltroEstado.AutoSize = true;
        lblFiltroEstado.Font = new Font("Segoe UI", 9F);
        lblFiltroEstado.ForeColor = Color.FromArgb(103, 116, 137);
        lblFiltroEstado.Location = new Point(0, 0);
        lblFiltroEstado.Name = "lblFiltroEstado";
        lblFiltroEstado.Size = new Size(134, 20);
        lblFiltroEstado.TabIndex = 0;
        lblFiltroEstado.Text = "Estado de la orden";
        // 
        // cboFiltroEstado
        // 
        cboFiltroEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboFiltroEstado.BackColor = Color.White;
        cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroEstado.DropDownWidth = 250;
        cboFiltroEstado.FlatStyle = FlatStyle.Flat;
        cboFiltroEstado.Font = new Font("Segoe UI", 9.5F);
        cboFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroEstado.Items.AddRange(new object[] { "Todos los estados", "En Revisión", "Pendiente de Confirmación", "Aprobada", "En Reparación", "Reparada", "No Reparada", "Rechazada", "Entregada" });
        cboFiltroEstado.Location = new Point(0, 25);
        cboFiltroEstado.Name = "cboFiltroEstado";
        cboFiltroEstado.Size = new Size(200, 29);
        cboFiltroEstado.TabIndex = 0;
        // 
        // panelFiltroAsignacion
        // 
        panelFiltroAsignacion.Controls.Add(lblFiltroAsignacion);
        panelFiltroAsignacion.Controls.Add(cboFiltroAsignacion);
        panelFiltroAsignacion.Dock = DockStyle.Fill;
        panelFiltroAsignacion.Location = new Point(212, 0);
        panelFiltroAsignacion.Margin = new Padding(0);
        panelFiltroAsignacion.Name = "panelFiltroAsignacion";
        panelFiltroAsignacion.Size = new Size(213, 95);
        panelFiltroAsignacion.TabIndex = 1;
        // 
        // lblFiltroAsignacion
        // 
        lblFiltroAsignacion.AutoSize = true;
        lblFiltroAsignacion.Font = new Font("Segoe UI", 9F);
        lblFiltroAsignacion.ForeColor = Color.FromArgb(103, 116, 137);
        lblFiltroAsignacion.Location = new Point(0, 0);
        lblFiltroAsignacion.Name = "lblFiltroAsignacion";
        lblFiltroAsignacion.Size = new Size(82, 20);
        lblFiltroAsignacion.TabIndex = 0;
        lblFiltroAsignacion.Text = "Asignación";
        // 
        // cboFiltroAsignacion
        // 
        cboFiltroAsignacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboFiltroAsignacion.BackColor = Color.White;
        cboFiltroAsignacion.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroAsignacion.FlatStyle = FlatStyle.Flat;
        cboFiltroAsignacion.Font = new Font("Segoe UI", 9.5F);
        cboFiltroAsignacion.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroAsignacion.Items.AddRange(new object[] { "Todas las órdenes", "Sin técnico asignado", "Con técnico asignado", "Anuladas" });
        cboFiltroAsignacion.Location = new Point(0, 25);
        cboFiltroAsignacion.Name = "cboFiltroAsignacion";
        cboFiltroAsignacion.Size = new Size(213, 29);
        cboFiltroAsignacion.TabIndex = 0;
        // 
        // colNumeroOrden
        // 
        colNumeroOrden.FillWeight = 12F;
        colNumeroOrden.HeaderText = "N.º de orden";
        colNumeroOrden.MinimumWidth = 115;
        colNumeroOrden.Name = "colNumeroOrden";
        colNumeroOrden.ReadOnly = true;
        colNumeroOrden.SortMode = DataGridViewColumnSortMode.NotSortable;
        colNumeroOrden.Width = 125;
        // 
        // colCliente
        // 
        colCliente.FillWeight = 18F;
        colCliente.HeaderText = "Cliente";
        colCliente.MinimumWidth = 160;
        colCliente.Name = "colCliente";
        colCliente.ReadOnly = true;
        colCliente.SortMode = DataGridViewColumnSortMode.NotSortable;
        colCliente.Width = 160;
        // 
        // colEquipo
        // 
        colEquipo.FillWeight = 18F;
        colEquipo.HeaderText = "Equipo";
        colEquipo.MinimumWidth = 150;
        colEquipo.Name = "colEquipo";
        colEquipo.ReadOnly = true;
        colEquipo.SortMode = DataGridViewColumnSortMode.NotSortable;
        colEquipo.Width = 150;
        // 
        // colFechaRecepcion
        // 
        dataGridViewCellStyle1.Format = "dd/MM/yyyy";
        colFechaRecepcion.DefaultCellStyle = dataGridViewCellStyle1;
        colFechaRecepcion.FillWeight = 12F;
        colFechaRecepcion.HeaderText = "Recepción";
        colFechaRecepcion.MinimumWidth = 110;
        colFechaRecepcion.Name = "colFechaRecepcion";
        colFechaRecepcion.ReadOnly = true;
        colFechaRecepcion.SortMode = DataGridViewColumnSortMode.NotSortable;
        colFechaRecepcion.Width = 125;
        // 
        // colTecnico
        // 
        colTecnico.FillWeight = 15F;
        colTecnico.HeaderText = "Técnico responsable";
        colTecnico.MinimumWidth = 145;
        colTecnico.Name = "colTecnico";
        colTecnico.ReadOnly = true;
        colTecnico.SortMode = DataGridViewColumnSortMode.NotSortable;
        colTecnico.Width = 145;
        // 
        // colEstado
        // 
        colEstado.FillWeight = 13F;
        colEstado.HeaderText = "Estado";
        colEstado.MinimumWidth = 125;
        colEstado.Name = "colEstado";
        colEstado.ReadOnly = true;
        colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
        colEstado.Width = 125;
        // 
        // colTotal
        // 
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle2.Format = "N2";
        colTotal.DefaultCellStyle = dataGridViewCellStyle2;
        colTotal.FillWeight = 10F;
        colTotal.HeaderText = "Total (C$)";
        colTotal.MinimumWidth = 100;
        colTotal.Name = "colTotal";
        colTotal.ReadOnly = true;
        colTotal.SortMode = DataGridViewColumnSortMode.NotSortable;
        colTotal.Width = 125;
        // 
        // colSaldo
        // 
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
        dataGridViewCellStyle3.Format = "N2";
        colSaldo.DefaultCellStyle = dataGridViewCellStyle3;
        colSaldo.FillWeight = 10F;
        colSaldo.HeaderText = "Saldo (C$)";
        colSaldo.MinimumWidth = 100;
        colSaldo.Name = "colSaldo";
        colSaldo.ReadOnly = true;
        colSaldo.SortMode = DataGridViewColumnSortMode.NotSortable;
        colSaldo.Width = 125;
        dgvListado.Columns.AddRange(new DataGridViewColumn[] { colNumeroOrden, colCliente, colEquipo, colFechaRecepcion, colTecnico, colEstado, colTotal, colSaldo });
        panelFiltrosModulo.Controls.Add(tablaFiltrosModulo);
        // 
        // FrmOrdenesServicio
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        ClientSize = new Size(1221, 933);
        Name = "FrmOrdenesServicio";
        Text = "Órdenes de servicio";
        bordeBuscar.ResumeLayout(false);
        bordeBuscar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)iconBuscar).EndInit();
        tablaFiltrosModulo.ResumeLayout(false);
        panelFiltroEstado.ResumeLayout(false);
        panelFiltroEstado.PerformLayout();
        panelFiltroAsignacion.ResumeLayout(false);
        panelFiltroAsignacion.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tablaFiltrosModulo = null!;
    private Panel panelFiltroEstado = null!;
    private Label lblFiltroEstado = null!;
    private ComboBox cboFiltroEstado = null!;
    private Panel panelFiltroAsignacion = null!;
    private Label lblFiltroAsignacion = null!;
    private ComboBox cboFiltroAsignacion = null!;
    private DataGridViewTextBoxColumn colNumeroOrden = null!;
    private DataGridViewTextBoxColumn colCliente = null!;
    private DataGridViewTextBoxColumn colEquipo = null!;
    private DataGridViewTextBoxColumn colFechaRecepcion = null!;
    private DataGridViewTextBoxColumn colTecnico = null!;
    private DataGridViewTextBoxColumn colEstado = null!;
    private DataGridViewTextBoxColumn colTotal = null!;
    private DataGridViewTextBoxColumn colSaldo = null!;
}
