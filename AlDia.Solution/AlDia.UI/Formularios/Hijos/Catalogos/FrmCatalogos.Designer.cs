namespace AlDia.UI.Formularios.Hijos.Catalogos;

partial class FrmCatalogos
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
        colCatalogo = new DataGridViewTextBoxColumn();
        colNombre = new DataGridViewTextBoxColumn();
        colDescripcion = new DataGridViewTextBoxColumn();
        colPrecio = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        lblFiltroCatalogo = new Label();
        cboFiltroCatalogo = new ComboBox();
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();
        bordeBuscar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)iconBuscar).BeginInit();
        SuspendLayout();
        // 
        // lblTitulo
        // 
        lblTitulo.Text = "Catálogos del taller";
        // 
        // lblDescripcion
        // 
        lblDescripcion.Text = "Roles, tipos de equipo y servicios del taller.";
        // 
        // btnNuevo
        // 
        btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnNuevo.FlatAppearance.BorderSize = 0;
        btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
        // 
        // txtBuscar
        // 
        txtBuscar.PlaceholderText = "Nombre o descripción";
        // 
        // btnBuscar
        // 
        btnBuscar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        // 
        // lblListado
        // 
        lblListado.Text = "Catálogos del taller";
        // 
        // btnEditar
        // 
        btnEditar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnEditar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        // 
        // btnCambiarEstado
        // 
        btnCambiarEstado.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnCambiarEstado.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
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
        // colCatalogo
        // 
        colCatalogo.FillWeight = 17F;
        colCatalogo.HeaderText = "Catálogo";
        colCatalogo.MinimumWidth = 134;
        colCatalogo.Name = "colCatalogo";
        colCatalogo.ReadOnly = true;
        colCatalogo.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colNombre
        // 
        colNombre.FillWeight = 25F;
        colNombre.HeaderText = "Nombre";
        colNombre.MinimumWidth = 186;
        colNombre.Name = "colNombre";
        colNombre.ReadOnly = true;
        colNombre.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colDescripcion
        // 
        colDescripcion.FillWeight = 38F;
        colDescripcion.HeaderText = "Descripción";
        colDescripcion.MinimumWidth = 240;
        colDescripcion.Name = "colDescripcion";
        colDescripcion.ReadOnly = true;
        colDescripcion.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colPrecio
        // 
        dataGridViewCellStyle1.Format = "N2";
        colPrecio.DefaultCellStyle = dataGridViewCellStyle1;
        colPrecio.FillWeight = 12F;
        colPrecio.HeaderText = "Precio base";
        colPrecio.MinimumWidth = 104;
        colPrecio.Name = "colPrecio";
        colPrecio.ReadOnly = true;
        colPrecio.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // colEstado
        // 
        colEstado.FillWeight = 10F;
        colEstado.HeaderText = "Estado";
        colEstado.MinimumWidth = 90;
        colEstado.Name = "colEstado";
        colEstado.ReadOnly = true;
        colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // lblFiltroCatalogo
        // 
        lblFiltroCatalogo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblFiltroCatalogo.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroCatalogo.Location = new Point(0, 0);
        lblFiltroCatalogo.Name = "lblFiltroCatalogo";
        lblFiltroCatalogo.Size = new Size(180, 22);
        lblFiltroCatalogo.TabIndex = 0;
        lblFiltroCatalogo.Text = "Catálogo";
        // 
        // cboFiltroCatalogo
        // 
        cboFiltroCatalogo.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroCatalogo.FlatStyle = FlatStyle.Flat;
        cboFiltroCatalogo.Font = new Font("Segoe UI", 9.5F);
        cboFiltroCatalogo.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroCatalogo.Items.AddRange(new object[] { "Todos los catálogos", "Roles", "Tipos de equipo", "Servicios" });
        cboFiltroCatalogo.Location = new Point(0, 30);
        cboFiltroCatalogo.Name = "cboFiltroCatalogo";
        cboFiltroCatalogo.Size = new Size(180, 29);
        cboFiltroCatalogo.TabIndex = 0;
        // 
        // lblFiltroEstado
        // 
        lblFiltroEstado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        lblFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroEstado.Location = new Point(192, 0);
        lblFiltroEstado.Name = "lblFiltroEstado";
        lblFiltroEstado.Size = new Size(180, 22);
        lblFiltroEstado.TabIndex = 1;
        lblFiltroEstado.Text = "Estado";
        // 
        // cboFiltroEstado
        // 
        cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroEstado.FlatStyle = FlatStyle.Flat;
        cboFiltroEstado.Font = new Font("Segoe UI", 9.5F);
        cboFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroEstado.Items.AddRange(new object[] { "Todos los estados", "Activos", "Inactivos" });
        cboFiltroEstado.Location = new Point(192, 30);
        cboFiltroEstado.Name = "cboFiltroEstado";
        cboFiltroEstado.Size = new Size(180, 29);
        cboFiltroEstado.TabIndex = 1;
        dgvListado.Columns.AddRange(new DataGridViewColumn[] { colCatalogo, colNombre, colDescripcion, colPrecio, colEstado });
        panelFiltrosModulo.Controls.AddRange(new Control[] { lblFiltroCatalogo, cboFiltroCatalogo, lblFiltroEstado, cboFiltroEstado });
        cboFiltroCatalogo.SelectedIndex = 0;
        cboFiltroEstado.SelectedIndex = 0;
        // 
        // FrmCatalogos
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        ClientSize = new Size(1221, 933);
        Name = "FrmCatalogos";
        Text = "Catálogos del taller";
        bordeBuscar.ResumeLayout(false);
        bordeBuscar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)iconBuscar).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private DataGridViewTextBoxColumn colCatalogo;
    private DataGridViewTextBoxColumn colNombre;
    private DataGridViewTextBoxColumn colDescripcion;
    private DataGridViewTextBoxColumn colPrecio;
    private DataGridViewTextBoxColumn colEstado;
    private Label lblFiltroCatalogo;
    private ComboBox cboFiltroCatalogo;
    private Label lblFiltroEstado;
    private ComboBox cboFiltroEstado;
}
