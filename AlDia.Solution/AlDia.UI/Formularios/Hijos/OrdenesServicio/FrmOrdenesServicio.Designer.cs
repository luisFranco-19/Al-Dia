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
        panelFiltrosModulo.SuspendLayout();
        tablaFiltrosModulo.SuspendLayout();
        SuspendLayout();
        lblTitulo.Text = "Órdenes de servicio";
        lblDescripcion.Text = "Recepción, seguimiento y expediente de los equipos del taller.";
        btnNuevo.Text = "Nueva orden";
        txtBuscar.PlaceholderText = "N.º de orden, cliente o equipo";
        lblListado.Text = "Órdenes del taller";
        btnEditar.Text = "Ver expediente";
        btnCambiarEstado.Text = "Anular orden";
        btnCambiarEstado.IconChar = FontAwesome.Sharp.IconChar.Ban;
        lblSinRegistros.Text = "Aún no hay órdenes de servicio";
        lblSinRegistrosDetalle.Text = "Los registros aparecerán aquí con su información y estado.";
        lblPie.Text = "AL DÍA  /  ÓRDENES DE SERVICIO";
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
        panelFiltroEstado.Dock = DockStyle.Fill;
        panelFiltroEstado.Margin = new Padding(0, 0, 12, 0);
        panelFiltroEstado.Name = "panelFiltroEstado";
        panelFiltroEstado.Size = new Size(180, 70);
        panelFiltroEstado.TabIndex = 0;
        lblFiltroEstado.Text = "Estado de la orden";
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
        cboFiltroEstado.DropDownWidth = 250;
        cboFiltroEstado.Items.AddRange(new object[] { "Todos los estados", "En Revisión", "Pendiente de Confirmación", "Aprobada", "En Reparación", "Reparada", "No Reparada", "Rechazada", "Entregada" });
        cboFiltroEstado.SelectedIndex = 0;
        cboFiltroEstado.TabIndex = 0;
        panelFiltroEstado.Controls.Add(lblFiltroEstado);
        panelFiltroEstado.Controls.Add(cboFiltroEstado);
        tablaFiltrosModulo.Controls.Add(panelFiltroEstado, 0, 0);
        panelFiltroAsignacion.Dock = DockStyle.Fill;
        panelFiltroAsignacion.Margin = new Padding(0, 0, 0, 0);
        panelFiltroAsignacion.Name = "panelFiltroAsignacion";
        panelFiltroAsignacion.Size = new Size(180, 70);
        panelFiltroAsignacion.TabIndex = 1;
        lblFiltroAsignacion.Text = "Asignación";
        lblFiltroAsignacion.Name = "lblFiltroAsignacion";
        lblFiltroAsignacion.AutoSize = true;
        lblFiltroAsignacion.Location = new Point(0, 0);
        lblFiltroAsignacion.ForeColor = Color.FromArgb(103, 116, 137);
        lblFiltroAsignacion.Font = new Font("Segoe UI", 9F);
        cboFiltroAsignacion.Name = "cboFiltroAsignacion";
        cboFiltroAsignacion.Location = new Point(0, 25);
        cboFiltroAsignacion.Size = new Size(180, 27);
        cboFiltroAsignacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboFiltroAsignacion.BackColor = Color.White;
        cboFiltroAsignacion.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroAsignacion.Font = new Font("Segoe UI", 9.5F);
        cboFiltroAsignacion.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroAsignacion.FlatStyle = FlatStyle.Flat;
        cboFiltroAsignacion.Items.AddRange(new object[] { "Todas las órdenes", "Sin técnico asignado", "Con técnico asignado", "Anuladas" });
        cboFiltroAsignacion.SelectedIndex = 0;
        cboFiltroAsignacion.TabIndex = 0;
        panelFiltroAsignacion.Controls.Add(lblFiltroAsignacion);
        panelFiltroAsignacion.Controls.Add(cboFiltroAsignacion);
        tablaFiltrosModulo.Controls.Add(panelFiltroAsignacion, 1, 0);
        colNumeroOrden.Name = "colNumeroOrden";
        colNumeroOrden.HeaderText = "N.º de orden";
        colNumeroOrden.FillWeight = 12F;
        colNumeroOrden.MinimumWidth = 115;
        colNumeroOrden.ReadOnly = true;
        colNumeroOrden.SortMode = DataGridViewColumnSortMode.NotSortable;
        colCliente.Name = "colCliente";
        colCliente.HeaderText = "Cliente";
        colCliente.FillWeight = 18F;
        colCliente.MinimumWidth = 160;
        colCliente.ReadOnly = true;
        colCliente.SortMode = DataGridViewColumnSortMode.NotSortable;
        colEquipo.Name = "colEquipo";
        colEquipo.HeaderText = "Equipo";
        colEquipo.FillWeight = 18F;
        colEquipo.MinimumWidth = 150;
        colEquipo.ReadOnly = true;
        colEquipo.SortMode = DataGridViewColumnSortMode.NotSortable;
        colFechaRecepcion.Name = "colFechaRecepcion";
        colFechaRecepcion.HeaderText = "Recepción";
        colFechaRecepcion.FillWeight = 12F;
        colFechaRecepcion.MinimumWidth = 110;
        colFechaRecepcion.ReadOnly = true;
        colFechaRecepcion.SortMode = DataGridViewColumnSortMode.NotSortable;
        colFechaRecepcion.DefaultCellStyle.Format = "dd/MM/yyyy";
        colTecnico.Name = "colTecnico";
        colTecnico.HeaderText = "Técnico responsable";
        colTecnico.FillWeight = 15F;
        colTecnico.MinimumWidth = 145;
        colTecnico.ReadOnly = true;
        colTecnico.SortMode = DataGridViewColumnSortMode.NotSortable;
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEstado.FillWeight = 13F;
        colEstado.MinimumWidth = 125;
        colEstado.ReadOnly = true;
        colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
        colTotal.Name = "colTotal";
        colTotal.HeaderText = "Total (C$)";
        colTotal.FillWeight = 10F;
        colTotal.MinimumWidth = 100;
        colTotal.ReadOnly = true;
        colTotal.SortMode = DataGridViewColumnSortMode.NotSortable;
        colTotal.DefaultCellStyle.Format = "N2";
        colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        colSaldo.Name = "colSaldo";
        colSaldo.HeaderText = "Saldo (C$)";
        colSaldo.FillWeight = 10F;
        colSaldo.MinimumWidth = 100;
        colSaldo.ReadOnly = true;
        colSaldo.SortMode = DataGridViewColumnSortMode.NotSortable;
        colSaldo.DefaultCellStyle.Format = "N2";
        colSaldo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvListado.Columns.AddRange(new DataGridViewColumn[] { colNumeroOrden, colCliente, colEquipo, colFechaRecepcion, colTecnico, colEstado, colTotal, colSaldo });
        panelFiltrosModulo.Controls.Add(tablaFiltrosModulo);
        Name = "FrmOrdenesServicio";
        Text = "Órdenes de servicio";

        tablaFiltrosModulo.ResumeLayout(false);
        panelFiltrosModulo.ResumeLayout(false);
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
