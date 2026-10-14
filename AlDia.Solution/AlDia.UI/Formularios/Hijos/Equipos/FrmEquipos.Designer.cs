#nullable disable

namespace AlDia.UI.Formularios.Hijos.Equipos;

partial class FrmEquipos
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
        lblFiltroTipoEquipo = new Label();
        cboFiltroTipoEquipo = new ComboBox();
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();
        colIdEquipo = new DataGridViewTextBoxColumn();
        colCliente = new DataGridViewTextBoxColumn();
        colTipoEquipo = new DataGridViewTextBoxColumn();
        colMarca = new DataGridViewTextBoxColumn();
        colModelo = new DataGridViewTextBoxColumn();
        colNumeroSerie = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        panelFiltrosModulo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvListado).BeginInit();
        SuspendLayout();
        // Controles heredados del listado base
        lblTitulo.Text = "Equipos";
        iconSinRegistros.IconChar = FontAwesome.Sharp.IconChar.Laptop;
        lblDescripcion.Text = "Equipos de los clientes y sus características de identificación.";
        btnNuevo.Text = "Nuevo equipo";
        txtBuscar.PlaceholderText = "Buscar por cliente, marca, modelo o serie";
        lblListado.Text = "Listado de equipos";
        btnEditar.Text = "Editar equipo";
        lblSinRegistros.Text = "Sin equipos registrados";
        lblSinRegistrosDetalle.Text = "Los equipos aparecerán aquí cuando se registren.";
        lblPie.Text = "0 equipos · Seleccione un registro para editar";
        // lblFiltroTipoEquipo
        lblFiltroTipoEquipo.Name = "lblFiltroTipoEquipo";
        lblFiltroTipoEquipo.Text = "Tipo de equipo";
        lblFiltroTipoEquipo.Location = new Point(0, 0);
        lblFiltroTipoEquipo.Size = new Size(176, 20);
        lblFiltroTipoEquipo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblFiltroTipoEquipo.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroTipoEquipo.TabStop = false;
        // cboFiltroTipoEquipo
        cboFiltroTipoEquipo.Name = "cboFiltroTipoEquipo";
        cboFiltroTipoEquipo.Location = new Point(0, 32);
        cboFiltroTipoEquipo.Size = new Size(176, 28);
        cboFiltroTipoEquipo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        cboFiltroTipoEquipo.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroTipoEquipo.BackColor = Color.White;
        cboFiltroTipoEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroTipoEquipo.FlatStyle = FlatStyle.Flat;
        cboFiltroTipoEquipo.Items.AddRange(new object[] { "Todos los tipos" });
        cboFiltroTipoEquipo.SelectedIndex = 0;
        cboFiltroTipoEquipo.TabIndex = 0;
        // lblFiltroEstado
        lblFiltroEstado.Name = "lblFiltroEstado";
        lblFiltroEstado.Text = "Estado";
        lblFiltroEstado.Location = new Point(188, 0);
        lblFiltroEstado.Size = new Size(176, 20);
        lblFiltroEstado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroEstado.TabStop = false;
        // cboFiltroEstado
        cboFiltroEstado.Name = "cboFiltroEstado";
        cboFiltroEstado.Location = new Point(188, 32);
        cboFiltroEstado.Size = new Size(176, 28);
        cboFiltroEstado.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        cboFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroEstado.BackColor = Color.White;
        cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroEstado.FlatStyle = FlatStyle.Flat;
        cboFiltroEstado.Items.AddRange(new object[] { "Todos los estados", "Activo", "Inactivo" });
        cboFiltroEstado.SelectedIndex = 0;
        cboFiltroEstado.TabIndex = 1;
        // colIdEquipo
        colIdEquipo.Name = "colIdEquipo";
        colIdEquipo.HeaderText = "Código";
        colIdEquipo.DataPropertyName = "IdEquipo";
        colIdEquipo.FillWeight = 55F;
        colIdEquipo.MinimumWidth = 60;
        colIdEquipo.ReadOnly = true;
        // colCliente
        colCliente.Name = "colCliente";
        colCliente.HeaderText = "Cliente";
        colCliente.DataPropertyName = "Cliente";
        colCliente.FillWeight = 150F;
        colCliente.MinimumWidth = 130;
        colCliente.ReadOnly = true;
        // colTipoEquipo
        colTipoEquipo.Name = "colTipoEquipo";
        colTipoEquipo.HeaderText = "Tipo de equipo";
        colTipoEquipo.DataPropertyName = "TipoEquipo";
        colTipoEquipo.FillWeight = 100F;
        colTipoEquipo.MinimumWidth = 100;
        colTipoEquipo.ReadOnly = true;
        // colMarca
        colMarca.Name = "colMarca";
        colMarca.HeaderText = "Marca";
        colMarca.DataPropertyName = "Marca";
        colMarca.FillWeight = 90F;
        colMarca.MinimumWidth = 90;
        colMarca.ReadOnly = true;
        // colModelo
        colModelo.Name = "colModelo";
        colModelo.HeaderText = "Modelo";
        colModelo.DataPropertyName = "Modelo";
        colModelo.FillWeight = 110F;
        colModelo.MinimumWidth = 110;
        colModelo.ReadOnly = true;
        // colNumeroSerie
        colNumeroSerie.Name = "colNumeroSerie";
        colNumeroSerie.HeaderText = "Número de serie";
        colNumeroSerie.DataPropertyName = "NumeroSerie";
        colNumeroSerie.FillWeight = 115F;
        colNumeroSerie.MinimumWidth = 125;
        colNumeroSerie.ReadOnly = true;
        // colEstado
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEstado.DataPropertyName = "EstadoNombre";
        colEstado.FillWeight = 70F;
        colEstado.MinimumWidth = 75;
        colEstado.ReadOnly = true;
        panelFiltrosModulo.Controls.Add(lblFiltroTipoEquipo);
        panelFiltrosModulo.Controls.Add(cboFiltroTipoEquipo);
        panelFiltrosModulo.Controls.Add(lblFiltroEstado);
        panelFiltrosModulo.Controls.Add(cboFiltroEstado);
        dgvListado.Columns.AddRange(new DataGridViewColumn[]
        {
            colIdEquipo, colCliente, colTipoEquipo, colMarca, colModelo, colNumeroSerie, colEstado
        });
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Name = "FrmEquipos";
        Text = "Al Día · Equipos";
        panelFiltrosModulo.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvListado).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label lblFiltroTipoEquipo;
    private ComboBox cboFiltroTipoEquipo;
    private Label lblFiltroEstado;
    private ComboBox cboFiltroEstado;
    private DataGridViewTextBoxColumn colIdEquipo;
    private DataGridViewTextBoxColumn colCliente;
    private DataGridViewTextBoxColumn colTipoEquipo;
    private DataGridViewTextBoxColumn colMarca;
    private DataGridViewTextBoxColumn colModelo;
    private DataGridViewTextBoxColumn colNumeroSerie;
    private DataGridViewTextBoxColumn colEstado;
}
