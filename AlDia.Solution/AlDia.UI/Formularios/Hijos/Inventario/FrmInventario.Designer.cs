namespace AlDia.UI.Formularios.Hijos.Inventario;

partial class FrmInventario
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
        colNombre = new DataGridViewTextBoxColumn();
        colMarca = new DataGridViewTextBoxColumn();
        colNumeroParte = new DataGridViewTextBoxColumn();
        colCompra = new DataGridViewTextBoxColumn();
        colVenta = new DataGridViewTextBoxColumn();
        colStock = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        lblFiltroExistencias = new Label();
        cboFiltroExistencias = new ComboBox();
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();

        SuspendLayout();
        Name = "FrmInventario";
        Text = "Inventario de repuestos";
        lblTitulo.Text = "Inventario de repuestos";
        lblDescripcion.Text = "Piezas, precios y existencias del taller.";
        lblListado.Text = "Inventario de repuestos";
        btnNuevo.Text = "Nuevo repuesto";
        txtBuscar.PlaceholderText = "Nombre o descripción";
        lblSinRegistros.Text = "Sin repuestos para mostrar";
        lblSinRegistrosDetalle.Text = "Los registros aparecerán en este listado.";
        colNombre.Name = "colNombre";
        colNombre.HeaderText = "Repuesto";
        colNombre.FillWeight = 19F;
        colNombre.MinimumWidth = 144;
        colNombre.ReadOnly = true;
        colNombre.SortMode = DataGridViewColumnSortMode.NotSortable;
        colMarca.Name = "colMarca";
        colMarca.HeaderText = "Marca";
        colMarca.FillWeight = 11F;
        colMarca.MinimumWidth = 100;
        colMarca.ReadOnly = true;
        colMarca.SortMode = DataGridViewColumnSortMode.NotSortable;
        colNumeroParte.Name = "colNumeroParte";
        colNumeroParte.HeaderText = "Número de parte";
        colNumeroParte.FillWeight = 16F;
        colNumeroParte.MinimumWidth = 130;
        colNumeroParte.ReadOnly = true;
        colNumeroParte.SortMode = DataGridViewColumnSortMode.NotSortable;
        colCompra.Name = "colCompra";
        colCompra.HeaderText = "Precio compra";
        colCompra.FillWeight = 13F;
        colCompra.MinimumWidth = 112;
        colCompra.ReadOnly = true;
        colCompra.SortMode = DataGridViewColumnSortMode.NotSortable;
        colCompra.DefaultCellStyle.Format = "N2";
        colVenta.Name = "colVenta";
        colVenta.HeaderText = "Precio venta";
        colVenta.FillWeight = 13F;
        colVenta.MinimumWidth = 112;
        colVenta.ReadOnly = true;
        colVenta.SortMode = DataGridViewColumnSortMode.NotSortable;
        colVenta.DefaultCellStyle.Format = "N2";
        colStock.Name = "colStock";
        colStock.HeaderText = "Existencias";
        colStock.FillWeight = 12F;
        colStock.MinimumWidth = 100;
        colStock.ReadOnly = true;
        colStock.SortMode = DataGridViewColumnSortMode.NotSortable;
        colStock.DefaultCellStyle.Format = "N0";
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEstado.FillWeight = 10F;
        colEstado.MinimumWidth = 90;
        colEstado.ReadOnly = true;
        colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
        dgvListado.Columns.AddRange(new DataGridViewColumn[] { colNombre, colMarca, colNumeroParte, colCompra, colVenta, colStock, colEstado });
        lblFiltroExistencias.Name = "lblFiltroExistencias";
        lblFiltroExistencias.Text = "Existencias";
        lblFiltroExistencias.Location = new Point(0, 0);
        lblFiltroExistencias.Size = new Size(180, 22);
        lblFiltroExistencias.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblFiltroExistencias.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroExistencias.TabStop = false;
        cboFiltroExistencias.Name = "cboFiltroExistencias";
        cboFiltroExistencias.Location = new Point(0, 30);
        cboFiltroExistencias.Size = new Size(180, 28);
        cboFiltroExistencias.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        cboFiltroExistencias.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroExistencias.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroExistencias.FlatStyle = FlatStyle.Flat;
        cboFiltroExistencias.Items.AddRange(new object[] { "Todas las existencias", "Con existencias", "Sin existencias" });
        cboFiltroExistencias.SelectedIndex = 0;
        cboFiltroExistencias.TabIndex = 0;
        panelFiltrosModulo.Controls.Add(lblFiltroExistencias);
        panelFiltrosModulo.Controls.Add(cboFiltroExistencias);
        lblFiltroEstado.Name = "lblFiltroEstado";
        lblFiltroEstado.Text = "Estado";
        lblFiltroEstado.Location = new Point(192, 0);
        lblFiltroEstado.Size = new Size(180, 22);
        lblFiltroEstado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroEstado.TabStop = false;
        cboFiltroEstado.Name = "cboFiltroEstado";
        cboFiltroEstado.Location = new Point(192, 30);
        cboFiltroEstado.Size = new Size(180, 28);
        cboFiltroEstado.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        cboFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroEstado.FlatStyle = FlatStyle.Flat;
        cboFiltroEstado.Items.AddRange(new object[] { "Todos los estados", "Activos", "Inactivos" });
        cboFiltroEstado.SelectedIndex = 0;
        cboFiltroEstado.TabIndex = 1;
        panelFiltrosModulo.Controls.Add(lblFiltroEstado);
        panelFiltrosModulo.Controls.Add(cboFiltroEstado);

        ResumeLayout(false);
    }

    #endregion

    private DataGridViewTextBoxColumn colNombre;
    private DataGridViewTextBoxColumn colMarca;
    private DataGridViewTextBoxColumn colNumeroParte;
    private DataGridViewTextBoxColumn colCompra;
    private DataGridViewTextBoxColumn colVenta;
    private DataGridViewTextBoxColumn colStock;
    private DataGridViewTextBoxColumn colEstado;
    private Label lblFiltroExistencias;
    private ComboBox cboFiltroExistencias;
    private Label lblFiltroEstado;
    private ComboBox cboFiltroEstado;
}

