namespace AlDia.UI.Formularios.Hijos.Catalogos;

partial class FrmServicios
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
        colDescripcion = new DataGridViewTextBoxColumn();
        colPrecio = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();

        SuspendLayout();
        Name = "FrmServicios";
        Text = "Servicios del taller";
        lblTitulo.Text = "Servicios del taller";
        lblDescripcion.Text = "Catálogo de trabajos y sus precios base.";
        lblListado.Text = "Servicios del taller";
        btnNuevo.Text = "Nuevo servicio";
        txtBuscar.PlaceholderText = "Nombre o descripción";
        lblSinRegistros.Text = "Sin servicios para mostrar";
        lblSinRegistrosDetalle.Text = "Los registros aparecerán en este listado.";
        colNombre.Name = "colNombre";
        colNombre.HeaderText = "Servicio";
        colNombre.FillWeight = 28F;
        colNombre.MinimumWidth = 176;
        colNombre.ReadOnly = true;
        colNombre.SortMode = DataGridViewColumnSortMode.NotSortable;
        colDescripcion.Name = "colDescripcion";
        colDescripcion.HeaderText = "Descripción";
        colDescripcion.FillWeight = 42F;
        colDescripcion.MinimumWidth = 260;
        colDescripcion.ReadOnly = true;
        colDescripcion.SortMode = DataGridViewColumnSortMode.NotSortable;
        colPrecio.Name = "colPrecio";
        colPrecio.HeaderText = "Precio base";
        colPrecio.FillWeight = 16F;
        colPrecio.MinimumWidth = 130;
        colPrecio.ReadOnly = true;
        colPrecio.SortMode = DataGridViewColumnSortMode.NotSortable;
        colPrecio.DefaultCellStyle.Format = "N2";
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEstado.FillWeight = 14F;
        colEstado.MinimumWidth = 100;
        colEstado.ReadOnly = true;
        colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
        dgvListado.Columns.AddRange(new DataGridViewColumn[] { colNombre, colDescripcion, colPrecio, colEstado });
        lblFiltroEstado.Name = "lblFiltroEstado";
        lblFiltroEstado.Text = "Estado";
        lblFiltroEstado.Location = new Point(0, 0);
        lblFiltroEstado.Size = new Size(372, 22);
        lblFiltroEstado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroEstado.TabStop = false;
        cboFiltroEstado.Name = "cboFiltroEstado";
        cboFiltroEstado.Location = new Point(0, 30);
        cboFiltroEstado.Size = new Size(372, 28);
        cboFiltroEstado.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        cboFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroEstado.FlatStyle = FlatStyle.Flat;
        cboFiltroEstado.Items.AddRange(new object[] { "Todos los estados", "Activos", "Inactivos" });
        cboFiltroEstado.SelectedIndex = 0;
        cboFiltroEstado.TabIndex = 0;
        panelFiltrosModulo.Controls.Add(lblFiltroEstado);
        panelFiltrosModulo.Controls.Add(cboFiltroEstado);

        ResumeLayout(false);
    }

    #endregion

    private DataGridViewTextBoxColumn colNombre;
    private DataGridViewTextBoxColumn colDescripcion;
    private DataGridViewTextBoxColumn colPrecio;
    private DataGridViewTextBoxColumn colEstado;
    private Label lblFiltroEstado;
    private ComboBox cboFiltroEstado;
}

