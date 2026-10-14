namespace AlDia.UI.Formularios.Hijos.Usuarios;

partial class FrmUsuarios
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
        colUsuario = new DataGridViewTextBoxColumn();
        colNombre = new DataGridViewTextBoxColumn();
        colCedula = new DataGridViewTextBoxColumn();
        colRol = new DataGridViewTextBoxColumn();
        colTelefono = new DataGridViewTextBoxColumn();
        colCorreo = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        lblFiltroRol = new Label();
        cboFiltroRol = new ComboBox();
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();

        SuspendLayout();
        Name = "FrmUsuarios";
        Text = "Usuarios";
        lblTitulo.Text = "Cuentas del equipo";
        lblDescripcion.Text = "Accesos y perfiles del personal del taller.";
        lblListado.Text = "Directorio de usuarios";
        btnNuevo.Text = "Nuevo usuario";
        txtBuscar.PlaceholderText = "Nombre, usuario o cédula";
        lblSinRegistros.Text = "Sin usuarios para mostrar";
        lblSinRegistrosDetalle.Text = "Las cuentas del equipo aparecerán en este listado.";
        lblFiltroRol.Name = "lblFiltroRol";
        lblFiltroRol.Text = "Rol";
        lblFiltroRol.Location = new Point(0, 0);
        lblFiltroRol.Size = new Size(180, 22);
        lblFiltroRol.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblFiltroRol.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroRol.TabStop = false;
        cboFiltroRol.Name = "cboFiltroRol";
        cboFiltroRol.Location = new Point(0, 30);
        cboFiltroRol.Size = new Size(180, 28);
        cboFiltroRol.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        cboFiltroRol.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroRol.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroRol.FlatStyle = FlatStyle.Flat;
        cboFiltroRol.Items.AddRange(new object[] { "Todos los roles", "Administrador", "Recepcionista", "Técnico" });
        cboFiltroRol.SelectedIndex = 0;
        cboFiltroRol.TabIndex = 0;
        panelFiltrosModulo.Controls.Add(lblFiltroRol);
        panelFiltrosModulo.Controls.Add(cboFiltroRol);
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

        colUsuario.Name = "colUsuario";
        colUsuario.HeaderText = "Usuario";
        colUsuario.FillWeight = 13F;
        colUsuario.MinimumWidth = 108;
        colUsuario.ReadOnly = true;
        colUsuario.SortMode = DataGridViewColumnSortMode.NotSortable;
        colNombre.Name = "colNombre";
        colNombre.HeaderText = "Nombre completo";
        colNombre.FillWeight = 19F;
        colNombre.MinimumWidth = 176;
        colNombre.ReadOnly = true;
        colNombre.SortMode = DataGridViewColumnSortMode.NotSortable;
        colCedula.Name = "colCedula";
        colCedula.HeaderText = "Cédula";
        colCedula.FillWeight = 12F;
        colCedula.MinimumWidth = 118;
        colCedula.ReadOnly = true;
        colCedula.SortMode = DataGridViewColumnSortMode.NotSortable;
        colRol.Name = "colRol";
        colRol.HeaderText = "Rol";
        colRol.FillWeight = 13F;
        colRol.MinimumWidth = 120;
        colRol.ReadOnly = true;
        colRol.SortMode = DataGridViewColumnSortMode.NotSortable;
        colTelefono.Name = "colTelefono";
        colTelefono.HeaderText = "Teléfono";
        colTelefono.FillWeight = 11F;
        colTelefono.MinimumWidth = 106;
        colTelefono.ReadOnly = true;
        colTelefono.SortMode = DataGridViewColumnSortMode.NotSortable;
        colCorreo.Name = "colCorreo";
        colCorreo.HeaderText = "Correo electrónico";
        colCorreo.FillWeight = 21F;
        colCorreo.MinimumWidth = 164;
        colCorreo.ReadOnly = true;
        colCorreo.SortMode = DataGridViewColumnSortMode.NotSortable;
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEstado.FillWeight = 11F;
        colEstado.MinimumWidth = 90;
        colEstado.ReadOnly = true;
        colEstado.SortMode = DataGridViewColumnSortMode.NotSortable;
        dgvListado.Columns.AddRange(new DataGridViewColumn[] { colUsuario, colNombre, colCedula, colRol, colTelefono, colCorreo, colEstado });
        ResumeLayout(false);
    }

    #endregion

    private DataGridViewTextBoxColumn colUsuario;
    private DataGridViewTextBoxColumn colNombre;
    private DataGridViewTextBoxColumn colCedula;
    private DataGridViewTextBoxColumn colRol;
    private DataGridViewTextBoxColumn colTelefono;
    private DataGridViewTextBoxColumn colCorreo;
    private DataGridViewTextBoxColumn colEstado;

    private Label lblFiltroRol;
    private ComboBox cboFiltroRol;
    private Label lblFiltroEstado;
    private ComboBox cboFiltroEstado;
}
