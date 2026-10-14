namespace AlDia.UI.Formularios.Hijos.Catalogos;

partial class FrmTiposEquipo
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
        colEstado = new DataGridViewTextBoxColumn();
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();

        SuspendLayout();
        Name = "FrmTiposEquipo";
        Text = "Tipos de equipo";
        lblTitulo.Text = "Tipos de equipo";
        lblDescripcion.Text = "Clasificación de los equipos recibidos en el taller.";
        lblListado.Text = "Tipos de equipo";
        btnNuevo.Text = "Nuevo tipo";
        txtBuscar.PlaceholderText = "Nombre o descripción";
        lblSinRegistros.Text = "Sin tipos de equipo para mostrar";
        lblSinRegistrosDetalle.Text = "Los registros aparecerán en este listado.";
        colNombre.Name = "colNombre";
        colNombre.HeaderText = "Tipo de equipo";
        colNombre.FillWeight = 28F;
        colNombre.MinimumWidth = 176;
        colNombre.ReadOnly = true;
        colNombre.SortMode = DataGridViewColumnSortMode.NotSortable;
        colDescripcion.Name = "colDescripcion";
        colDescripcion.HeaderText = "Descripción";
        colDescripcion.FillWeight = 58F;
        colDescripcion.MinimumWidth = 300;
        colDescripcion.ReadOnly = true;
        colDescripcion.SortMode = DataGridViewColumnSortMode.NotSortable;
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEstado.FillWeight = 14F;
        colEstado.MinimumWidth = 100;
        colEstado.ReadOnly = true;
        colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
        dgvListado.Columns.AddRange(new DataGridViewColumn[] { colNombre, colDescripcion, colEstado });
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
    private DataGridViewTextBoxColumn colEstado;
    private Label lblFiltroEstado;
    private ComboBox cboFiltroEstado;
}

