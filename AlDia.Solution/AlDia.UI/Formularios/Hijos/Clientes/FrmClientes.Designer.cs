#nullable disable

namespace AlDia.UI.Formularios.Hijos.Clientes;

partial class FrmClientes
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
        lblFiltroEstado = new Label();
        cboFiltroEstado = new ComboBox();
        colIdCliente = new DataGridViewTextBoxColumn();
        colNombre = new DataGridViewTextBoxColumn();
        colApellido = new DataGridViewTextBoxColumn();
        colCedula = new DataGridViewTextBoxColumn();
        colTelefono = new DataGridViewTextBoxColumn();
        colEstado = new DataGridViewTextBoxColumn();
        panelFiltrosModulo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvListado).BeginInit();
        SuspendLayout();
        // Controles heredados del listado base
        lblTitulo.Text = "Clientes";
        iconSinRegistros.IconChar = FontAwesome.Sharp.IconChar.AddressBook;
        lblDescripcion.Text = "Información de identificación, contacto y estado de los clientes.";
        btnNuevo.Text = "Nuevo cliente";
        txtBuscar.PlaceholderText = "Buscar por nombre, cédula o teléfono";
        lblListado.Text = "Listado de clientes";
        btnEditar.Text = "Editar cliente";
        lblSinRegistros.Text = "Sin clientes registrados";
        lblSinRegistrosDetalle.Text = "Los clientes aparecerán aquí cuando se registren.";
        lblPie.Text = "0 clientes · Seleccione un registro para editar";
        // lblFiltroEstado
        lblFiltroEstado.Name = "lblFiltroEstado";
        lblFiltroEstado.Text = "Estado";
        lblFiltroEstado.Location = new Point(0, 0);
        lblFiltroEstado.Size = new Size(184, 20);
        lblFiltroEstado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        lblFiltroEstado.TabStop = false;
        // cboFiltroEstado
        cboFiltroEstado.Name = "cboFiltroEstado";
        cboFiltroEstado.Location = new Point(0, 32);
        cboFiltroEstado.Size = new Size(184, 28);
        cboFiltroEstado.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        cboFiltroEstado.ForeColor = Color.FromArgb(20, 43, 78);
        cboFiltroEstado.BackColor = Color.White;
        cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFiltroEstado.FlatStyle = FlatStyle.Flat;
        cboFiltroEstado.Items.AddRange(new object[] { "Todos los estados", "Activo", "Inactivo" });
        cboFiltroEstado.SelectedIndex = 0;
        cboFiltroEstado.TabIndex = 0;
        // colIdCliente
        colIdCliente.Name = "colIdCliente";
        colIdCliente.HeaderText = "Código";
        colIdCliente.DataPropertyName = "IdCliente";
        colIdCliente.FillWeight = 55F;
        colIdCliente.MinimumWidth = 64;
        colIdCliente.ReadOnly = true;
        // colNombre
        colNombre.Name = "colNombre";
        colNombre.HeaderText = "Nombre";
        colNombre.DataPropertyName = "Nombre";
        colNombre.FillWeight = 120F;
        colNombre.MinimumWidth = 100;
        colNombre.ReadOnly = true;
        // colApellido
        colApellido.Name = "colApellido";
        colApellido.HeaderText = "Apellido";
        colApellido.DataPropertyName = "Apellido";
        colApellido.FillWeight = 120F;
        colApellido.MinimumWidth = 100;
        colApellido.ReadOnly = true;
        // colCedula
        colCedula.Name = "colCedula";
        colCedula.HeaderText = "Cédula";
        colCedula.DataPropertyName = "Cedula";
        colCedula.FillWeight = 100F;
        colCedula.MinimumWidth = 120;
        colCedula.ReadOnly = true;
        // colTelefono
        colTelefono.Name = "colTelefono";
        colTelefono.HeaderText = "Teléfono";
        colTelefono.DataPropertyName = "Telefono";
        colTelefono.FillWeight = 95F;
        colTelefono.MinimumWidth = 100;
        colTelefono.ReadOnly = true;
        // colEstado
        colEstado.Name = "colEstado";
        colEstado.HeaderText = "Estado";
        colEstado.DataPropertyName = "EstadoNombre";
        colEstado.FillWeight = 80F;
        colEstado.MinimumWidth = 80;
        colEstado.ReadOnly = true;
        panelFiltrosModulo.Controls.Add(lblFiltroEstado);
        panelFiltrosModulo.Controls.Add(cboFiltroEstado);
        dgvListado.Columns.AddRange(new DataGridViewColumn[]
        {
            colIdCliente, colNombre, colApellido, colCedula, colTelefono, colEstado
        });
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Name = "FrmClientes";
        Text = "Al Día · Clientes";
        panelFiltrosModulo.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvListado).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label lblFiltroEstado;
    private ComboBox cboFiltroEstado;
    private DataGridViewTextBoxColumn colIdCliente;
    private DataGridViewTextBoxColumn colNombre;
    private DataGridViewTextBoxColumn colApellido;
    private DataGridViewTextBoxColumn colCedula;
    private DataGridViewTextBoxColumn colTelefono;
    private DataGridViewTextBoxColumn colEstado;
}
