namespace AlDia.UI.Formularios.Hijos.Pagos;

partial class FrmPagos
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
        tablaFiltrosModulo = new TableLayoutPanel();
        panelFiltroMetodo = new Panel();
        lblFiltroMetodo = new Label();
        cboFiltroMetodo = new ComboBox();
        panelFiltroEstado = new Panel();
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();
        colNumeroPago = new DataGridViewTextBoxColumn();
        colNumeroOrden = new DataGridViewTextBoxColumn();
        colCliente = new DataGridViewTextBoxColumn();
        colFechaPago = new DataGridViewTextBoxColumn();
        colMonto = new DataGridViewTextBoxColumn();
        colMetodo = new DataGridViewTextBoxColumn();
        colUsuario = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        panelFiltrosModulo.SuspendLayout();
        tablaFiltrosModulo.SuspendLayout();
        SuspendLayout();
        lblTitulo.Text = "Pagos";
        lblDescripcion.Text = "Cobros asociados a las órdenes de servicio del taller.";
        btnNuevo.Text = "Registrar pago";
        txtBuscar.PlaceholderText = "N.º de pago, orden o cliente";
        lblListado.Text = "Pagos registrados";
        btnEditar.Text = "Ver pago";
        btnCambiarEstado.Text = "Anular pago";
        btnCambiarEstado.IconChar = FontAwesome.Sharp.IconChar.Ban;
        lblSinRegistros.Text = "Aún no hay pagos";
        lblSinRegistrosDetalle.Text = "Los registros aparecerán aquí con su información y estado.";
        lblPie.Text = "AL DÍA  /  PAGOS";
        tablaFiltrosModulo.ColumnCount = 2;
        tablaFiltrosModulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaFiltrosModulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaFiltrosModulo.Dock = DockStyle.Fill;
        tablaFiltrosModulo.Margin = new Padding(0);
        tablaFiltrosModulo.Name = "tablaFiltrosModulo";
        tablaFiltrosModulo.RowCount = 1;
        tablaFiltrosModulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablaFiltrosModulo.Size = new Size(384, 70);
        tablaFiltrosModulo.TabIndex = 0;
        panelFiltroMetodo.Dock = DockStyle.Fill;
        panelFiltroMetodo.Margin = new Padding(0, 0, 12, 0);
        panelFiltroMetodo.Name = "panelFiltroMetodo";
        panelFiltroMetodo.Size = new Size(180, 70);
        panelFiltroMetodo.TabIndex = 0;
        lblFiltroMetodo.Text = "Método de pago";
        lblFiltroMetodo.Name = "lblFiltroMetodo";
        lblFiltroMetodo.AutoSize = true;
        lblFiltroMetodo.Location = new Point(0, 0);
        lblFiltroMetodo.ForeColor = Color.FromArgb(103, 116, 137);
        lblFiltroMetodo.Font = new Font("Segoe UI", 9F);
        cboFiltroMetodo.Name = "cboFiltroMetodo";
        cboFiltroMetodo.Location = new Point(0, 25);
        cboFiltroMetodo.Size = new Size(180, 27);
        cboFiltroMetodo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboFiltroMetodo.BackColor = Color.White;
        cboFiltroMetodo.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroMetodo.Font = new Font("Segoe UI", 9.5F);
        cboFiltroMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroMetodo.FlatStyle = FlatStyle.Flat;
        cboFiltroMetodo.Items.AddRange(new object[] { "Todos los métodos", "Efectivo", "Tarjeta", "Transferencia" });
        cboFiltroMetodo.SelectedIndex = 0;
        cboFiltroMetodo.TabIndex = 0;
        panelFiltroMetodo.Controls.Add(lblFiltroMetodo);
        panelFiltroMetodo.Controls.Add(cboFiltroMetodo);
        tablaFiltrosModulo.Controls.Add(panelFiltroMetodo, 0, 0);
        panelFiltroEstado.Dock = DockStyle.Fill;
        panelFiltroEstado.Margin = new Padding(0, 0, 0, 0);
        panelFiltroEstado.Name = "panelFiltroEstado";
        panelFiltroEstado.Size = new Size(180, 70);
        panelFiltroEstado.TabIndex = 1;
        lblFiltroEstado.Text = "Estado del pago";
        lblFiltroEstado.Name = "lblFiltroEstado";
        lblFiltroEstado.AutoSize = true;
        lblFiltroEstado.Location = new Point(0, 0);
        lblFiltroEstado.ForeColor = Color.FromArgb(103, 116, 137);
        lblFiltroEstado.Font = new Font("Segoe UI", 9F);
        cboFiltroEstado.Name = "cboFiltroEstado";
        cboFiltroEstado.Location = new Point(0, 25);
        cboFiltroEstado.Size = new Size(180, 27);
        cboFiltroEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboFiltroEstado.BackColor = Color.White;
        cboFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroEstado.Font = new Font("Segoe UI", 9.5F);
        cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroEstado.FlatStyle = FlatStyle.Flat;
        cboFiltroEstado.Items.AddRange(new object[] { "Todos los estados", "Vigentes", "Anulados" });
        cboFiltroEstado.SelectedIndex = 0;
        cboFiltroEstado.TabIndex = 0;
        panelFiltroEstado.Controls.Add(lblFiltroEstado);
        panelFiltroEstado.Controls.Add(cboFiltroEstado);
        tablaFiltrosModulo.Controls.Add(panelFiltroEstado, 1, 0);
        colNumeroPago.Name = "colNumeroPago";
        colNumeroPago.HeaderText = "N.º de pago";
        colNumeroPago.FillWeight = 10F;
        colNumeroPago.MinimumWidth = 105;
        colNumeroPago.ReadOnly = true;
        colNumeroPago.SortMode = DataGridViewColumnSortMode.NotSortable;
        colNumeroOrden.Name = "colNumeroOrden";
        colNumeroOrden.HeaderText = "N.º de orden";
        colNumeroOrden.FillWeight = 12F;
        colNumeroOrden.MinimumWidth = 115;
        colNumeroOrden.ReadOnly = true;
        colNumeroOrden.SortMode = DataGridViewColumnSortMode.NotSortable;
        colCliente.Name = "colCliente";
        colCliente.HeaderText = "Cliente";
        colCliente.FillWeight = 20F;
        colCliente.MinimumWidth = 170;
        colCliente.ReadOnly = true;
        colCliente.SortMode = DataGridViewColumnSortMode.NotSortable;
        colFechaPago.Name = "colFechaPago";
        colFechaPago.HeaderText = "Fecha";
        colFechaPago.FillWeight = 12F;
        colFechaPago.MinimumWidth = 110;
        colFechaPago.ReadOnly = true;
        colFechaPago.SortMode = DataGridViewColumnSortMode.NotSortable;
        colFechaPago.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        colMonto.Name = "colMonto";
        colMonto.HeaderText = "Monto (C$)";
        colMonto.FillWeight = 12F;
        colMonto.MinimumWidth = 110;
        colMonto.ReadOnly = true;
        colMonto.SortMode = DataGridViewColumnSortMode.NotSortable;
        colMonto.DefaultCellStyle.Format = "N2";
        colMonto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colMetodo.Name = "colMetodo";
        colMetodo.HeaderText = "Método";
        colMetodo.FillWeight = 12F;
        colMetodo.MinimumWidth = 110;
        colMetodo.ReadOnly = true;
        colMetodo.SortMode = DataGridViewColumnSortMode.NotSortable;
        colUsuario.Name = "colUsuario";
        colUsuario.HeaderText = "Registrado por";
        colUsuario.FillWeight = 16F;
        colUsuario.MinimumWidth = 135;
        colUsuario.ReadOnly = true;
        colUsuario.SortMode = DataGridViewColumnSortMode.NotSortable;
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEstado.FillWeight = 10F;
        colEstado.MinimumWidth = 90;
        colEstado.ReadOnly = true;
        colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
        dgvListado.Columns.AddRange(new DataGridViewColumn[] { colNumeroPago, colNumeroOrden, colCliente, colFechaPago, colMonto, colMetodo, colUsuario, colEstado });
        panelFiltrosModulo.Controls.Add(tablaFiltrosModulo);
        Name = "FrmPagos";
        Text = "Pagos";

        tablaFiltrosModulo.ResumeLayout(false);
        panelFiltrosModulo.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tablaFiltrosModulo = null!;
    private Panel panelFiltroMetodo = null!;
    private Label lblFiltroMetodo = null!;
    private ComboBox cboFiltroMetodo = null!;
    private Panel panelFiltroEstado = null!;
    private Label lblFiltroEstado = null!;
    private ComboBox cboFiltroEstado = null!;
    private DataGridViewTextBoxColumn colNumeroPago = null!;
    private DataGridViewTextBoxColumn colNumeroOrden = null!;
    private DataGridViewTextBoxColumn colCliente = null!;
    private DataGridViewTextBoxColumn colFechaPago = null!;
    private DataGridViewTextBoxColumn colMonto = null!;
    private DataGridViewTextBoxColumn colMetodo = null!;
    private DataGridViewTextBoxColumn colUsuario = null!;
    private DataGridViewTextBoxColumn colEstado = null!;
}
