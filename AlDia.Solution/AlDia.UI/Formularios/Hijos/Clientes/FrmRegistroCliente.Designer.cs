#nullable disable

namespace AlDia.UI.Formularios.Hijos.Clientes;

partial class FrmRegistroCliente
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
        tablaSecciones = new TableLayoutPanel();
        panelDatosCliente = new Panel();
        lblDatosCliente = new Label();
        lineaDatosCliente = new Panel();
        lblNombre = new Label();
        panelNombre = new AlDia.UI.Controles.PanelTarjeta();
        txtNombre = new TextBox();
        lblApellido = new Label();
        panelApellido = new AlDia.UI.Controles.PanelTarjeta();
        txtApellido = new TextBox();
        lblCedula = new Label();
        panelCedula = new AlDia.UI.Controles.PanelTarjeta();
        txtCedula = new TextBox();
        panelContacto = new Panel();
        lblContacto = new Label();
        lineaContacto = new Panel();
        lblTelefono = new Label();
        panelTelefono = new AlDia.UI.Controles.PanelTarjeta();
        txtTelefono = new TextBox();
        lblActivo = new Label();
        panelActivo = new AlDia.UI.Controles.PanelTarjeta();
        chkActivo = new CheckBox();
        lblActivoDetalle = new Label();
        lblContactoAyuda = new Label();
        panelCuerpo.SuspendLayout();
        tablaSecciones.SuspendLayout();
        panelDatosCliente.SuspendLayout();
        panelNombre.SuspendLayout();
        panelApellido.SuspendLayout();
        panelCedula.SuspendLayout();
        panelContacto.SuspendLayout();
        panelTelefono.SuspendLayout();
        panelActivo.SuspendLayout();
        SuspendLayout();
        // Controles heredados del formulario de registro base
        lblTitulo.Text = "Nuevo cliente";
        lblDescripcion.Text = "Complete los datos de identificación y contacto del cliente.";
        iconModulo.IconChar = FontAwesome.Sharp.IconChar.AddressBook;
        btnGuardar.Text = "Guardar cliente";
        // tablaSecciones
        tablaSecciones.Name = "tablaSecciones";
        tablaSecciones.Dock = DockStyle.Top;
        tablaSecciones.Size = new Size(764, 350);
        tablaSecciones.ColumnCount = 2;
        tablaSecciones.RowCount = 1;
        tablaSecciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaSecciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaSecciones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablaSecciones.Margin = new Padding(0);
        tablaSecciones.TabIndex = 0;
        // panelDatosCliente
        panelDatosCliente.Name = "panelDatosCliente";
        panelDatosCliente.Dock = DockStyle.Fill;
        panelDatosCliente.Margin = new Padding(0, 0, 12, 0);
        panelDatosCliente.Size = new Size(370, 350);
        panelDatosCliente.TabIndex = 0;
        // lblDatosCliente
        lblDatosCliente.Name = "lblDatosCliente";
        lblDatosCliente.Text = "01   Datos del cliente";
        lblDatosCliente.Location = new Point(0, 0);
        lblDatosCliente.Size = new Size(370, 28);
        lblDatosCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblDatosCliente.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
        lblDatosCliente.ForeColor = Color.FromArgb(20, 43, 78);
        lblDatosCliente.TabStop = false;
        lblDatosCliente.AutoEllipsis = true;
        // lineaDatosCliente
        lineaDatosCliente.Name = "lineaDatosCliente";
        lineaDatosCliente.Location = new Point(0, 37);
        lineaDatosCliente.Size = new Size(370, 1);
        lineaDatosCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lineaDatosCliente.BackColor = Color.FromArgb(211, 221, 232);
        // lblNombre
        lblNombre.Name = "lblNombre";
        lblNombre.Text = "Nombre *";
        lblNombre.Location = new Point(0, 54);
        lblNombre.Size = new Size(370, 23);
        lblNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblNombre.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblNombre.ForeColor = Color.FromArgb(20, 43, 78);
        lblNombre.TabStop = false;
        // panelNombre
        panelNombre.Name = "panelNombre";
        panelNombre.Location = new Point(0, 82);
        panelNombre.Size = new Size(370, 42);
        panelNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelNombre.BackColor = Color.White;
        panelNombre.RadioBorde = 8;
        panelNombre.ColorBorde = Color.FromArgb(211, 221, 232);
        panelNombre.TabIndex = 0;
        // txtNombre
        txtNombre.Name = "txtNombre";
        txtNombre.Location = new Point(13, 11);
        txtNombre.Size = new Size(344, 24);
        txtNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtNombre.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtNombre.ForeColor = Color.FromArgb(20, 43, 78);
        txtNombre.TabIndex = 0;
        txtNombre.BorderStyle = BorderStyle.None;
        txtNombre.PlaceholderText = "Ingrese el nombre";
        txtNombre.MaxLength = 100;
        // lblApellido
        lblApellido.Name = "lblApellido";
        lblApellido.Text = "Apellido *";
        lblApellido.Location = new Point(0, 136);
        lblApellido.Size = new Size(370, 23);
        lblApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblApellido.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblApellido.ForeColor = Color.FromArgb(20, 43, 78);
        lblApellido.TabStop = false;
        // panelApellido
        panelApellido.Name = "panelApellido";
        panelApellido.Location = new Point(0, 164);
        panelApellido.Size = new Size(370, 42);
        panelApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelApellido.BackColor = Color.White;
        panelApellido.RadioBorde = 8;
        panelApellido.ColorBorde = Color.FromArgb(211, 221, 232);
        panelApellido.TabIndex = 1;
        // txtApellido
        txtApellido.Name = "txtApellido";
        txtApellido.Location = new Point(13, 11);
        txtApellido.Size = new Size(344, 24);
        txtApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtApellido.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtApellido.ForeColor = Color.FromArgb(20, 43, 78);
        txtApellido.TabIndex = 0;
        txtApellido.BorderStyle = BorderStyle.None;
        txtApellido.PlaceholderText = "Ingrese el apellido";
        txtApellido.MaxLength = 100;
        // lblCedula
        lblCedula.Name = "lblCedula";
        lblCedula.Text = "Cédula *";
        lblCedula.Location = new Point(0, 218);
        lblCedula.Size = new Size(370, 23);
        lblCedula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblCedula.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblCedula.ForeColor = Color.FromArgb(20, 43, 78);
        lblCedula.TabStop = false;
        // panelCedula
        panelCedula.Name = "panelCedula";
        panelCedula.Location = new Point(0, 246);
        panelCedula.Size = new Size(370, 42);
        panelCedula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelCedula.BackColor = Color.White;
        panelCedula.RadioBorde = 8;
        panelCedula.ColorBorde = Color.FromArgb(211, 221, 232);
        panelCedula.TabIndex = 2;
        // txtCedula
        txtCedula.Name = "txtCedula";
        txtCedula.Location = new Point(13, 11);
        txtCedula.Size = new Size(344, 24);
        txtCedula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtCedula.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtCedula.ForeColor = Color.FromArgb(20, 43, 78);
        txtCedula.TabIndex = 0;
        txtCedula.BorderStyle = BorderStyle.None;
        txtCedula.PlaceholderText = "Ingrese el número de cédula";
        txtCedula.MaxLength = 20;
        // panelContacto
        panelContacto.Name = "panelContacto";
        panelContacto.Dock = DockStyle.Fill;
        panelContacto.Margin = new Padding(12, 0, 0, 0);
        panelContacto.Size = new Size(370, 350);
        panelContacto.TabIndex = 1;
        // lblContacto
        lblContacto.Name = "lblContacto";
        lblContacto.Text = "02   Contacto y estado";
        lblContacto.Location = new Point(0, 0);
        lblContacto.Size = new Size(370, 28);
        lblContacto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblContacto.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
        lblContacto.ForeColor = Color.FromArgb(20, 43, 78);
        lblContacto.TabStop = false;
        lblContacto.AutoEllipsis = true;
        // lineaContacto
        lineaContacto.Name = "lineaContacto";
        lineaContacto.Location = new Point(0, 37);
        lineaContacto.Size = new Size(370, 1);
        lineaContacto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lineaContacto.BackColor = Color.FromArgb(211, 221, 232);
        // lblTelefono
        lblTelefono.Name = "lblTelefono";
        lblTelefono.Text = "Teléfono *";
        lblTelefono.Location = new Point(0, 54);
        lblTelefono.Size = new Size(370, 23);
        lblTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblTelefono.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblTelefono.ForeColor = Color.FromArgb(20, 43, 78);
        lblTelefono.TabStop = false;
        // panelTelefono
        panelTelefono.Name = "panelTelefono";
        panelTelefono.Location = new Point(0, 82);
        panelTelefono.Size = new Size(370, 42);
        panelTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelTelefono.BackColor = Color.White;
        panelTelefono.RadioBorde = 8;
        panelTelefono.ColorBorde = Color.FromArgb(211, 221, 232);
        panelTelefono.TabIndex = 0;
        // txtTelefono
        txtTelefono.Name = "txtTelefono";
        txtTelefono.Location = new Point(13, 11);
        txtTelefono.Size = new Size(344, 24);
        txtTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTelefono.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtTelefono.ForeColor = Color.FromArgb(20, 43, 78);
        txtTelefono.TabIndex = 0;
        txtTelefono.BorderStyle = BorderStyle.None;
        txtTelefono.PlaceholderText = "Ingrese el número de teléfono";
        txtTelefono.MaxLength = 20;
        // lblActivo
        lblActivo.Name = "lblActivo";
        lblActivo.Text = "Estado del cliente";
        lblActivo.Location = new Point(0, 136);
        lblActivo.Size = new Size(370, 23);
        lblActivo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblActivo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblActivo.ForeColor = Color.FromArgb(20, 43, 78);
        lblActivo.TabStop = false;
        // panelActivo
        panelActivo.Name = "panelActivo";
        panelActivo.Location = new Point(0, 164);
        panelActivo.Size = new Size(370, 64);
        panelActivo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelActivo.BackColor = Color.White;
        panelActivo.RadioBorde = 8;
        panelActivo.ColorBorde = Color.FromArgb(211, 221, 232);
        panelActivo.TabIndex = 1;
        // chkActivo
        chkActivo.Name = "chkActivo";
        chkActivo.Location = new Point(13, 9);
        chkActivo.Size = new Size(344, 24);
        chkActivo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        chkActivo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        chkActivo.ForeColor = Color.FromArgb(20, 43, 78);
        chkActivo.TabIndex = 0;
        chkActivo.Text = "Cliente activo";
        chkActivo.Checked = true;
        chkActivo.CheckState = CheckState.Checked;
        chkActivo.UseVisualStyleBackColor = true;
        // lblActivoDetalle
        lblActivoDetalle.Name = "lblActivoDetalle";
        lblActivoDetalle.Text = "Indica si el cliente está activo.";
        lblActivoDetalle.Location = new Point(30, 35);
        lblActivoDetalle.Size = new Size(322, 20);
        lblActivoDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblActivoDetalle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point);
        lblActivoDetalle.ForeColor = Color.FromArgb(103, 116, 137);
        lblActivoDetalle.TabStop = false;
        // lblContactoAyuda
        lblContactoAyuda.Name = "lblContactoAyuda";
        lblContactoAyuda.Text = "El teléfono permite mantener el contacto con el cliente.";
        lblContactoAyuda.Location = new Point(0, 290);
        lblContactoAyuda.Size = new Size(370, 54);
        lblContactoAyuda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblContactoAyuda.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        lblContactoAyuda.ForeColor = Color.FromArgb(103, 116, 137);
        lblContactoAyuda.TabStop = false;
        panelCuerpo.Controls.Add(tablaSecciones);
        tablaSecciones.Controls.Add(panelDatosCliente, 0, 0);
        panelDatosCliente.Controls.Add(lblDatosCliente);
        panelDatosCliente.Controls.Add(lineaDatosCliente);
        panelDatosCliente.Controls.Add(lblNombre);
        panelDatosCliente.Controls.Add(panelNombre);
        panelNombre.Controls.Add(txtNombre);
        panelDatosCliente.Controls.Add(lblApellido);
        panelDatosCliente.Controls.Add(panelApellido);
        panelApellido.Controls.Add(txtApellido);
        panelDatosCliente.Controls.Add(lblCedula);
        panelDatosCliente.Controls.Add(panelCedula);
        panelCedula.Controls.Add(txtCedula);
        tablaSecciones.Controls.Add(panelContacto, 1, 0);
        panelContacto.Controls.Add(lblContacto);
        panelContacto.Controls.Add(lineaContacto);
        panelContacto.Controls.Add(lblTelefono);
        panelContacto.Controls.Add(panelTelefono);
        panelTelefono.Controls.Add(txtTelefono);
        panelActivo.Controls.Add(lblActivoDetalle);
        panelContacto.Controls.Add(lblActivo);
        panelContacto.Controls.Add(panelActivo);
        panelActivo.Controls.Add(chkActivo);
        panelContacto.Controls.Add(lblContactoAyuda);
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Name = "FrmRegistroCliente";
        Text = "Al Día · Clientes";
        panelActivo.ResumeLayout(false);
        panelActivo.PerformLayout();
        panelTelefono.ResumeLayout(false);
        panelTelefono.PerformLayout();
        panelContacto.ResumeLayout(false);
        panelContacto.PerformLayout();
        panelCedula.ResumeLayout(false);
        panelCedula.PerformLayout();
        panelApellido.ResumeLayout(false);
        panelApellido.PerformLayout();
        panelNombre.ResumeLayout(false);
        panelNombre.PerformLayout();
        panelDatosCliente.ResumeLayout(false);
        panelDatosCliente.PerformLayout();
        tablaSecciones.ResumeLayout(false);
        panelCuerpo.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tablaSecciones;
    private Panel panelDatosCliente;
    private Label lblDatosCliente;
    private Panel lineaDatosCliente;
    private Label lblNombre;
    private AlDia.UI.Controles.PanelTarjeta panelNombre;
    private TextBox txtNombre;
    private Label lblApellido;
    private AlDia.UI.Controles.PanelTarjeta panelApellido;
    private TextBox txtApellido;
    private Label lblCedula;
    private AlDia.UI.Controles.PanelTarjeta panelCedula;
    private TextBox txtCedula;
    private Panel panelContacto;
    private Label lblContacto;
    private Panel lineaContacto;
    private Label lblTelefono;
    private AlDia.UI.Controles.PanelTarjeta panelTelefono;
    private TextBox txtTelefono;
    private Label lblActivo;
    private AlDia.UI.Controles.PanelTarjeta panelActivo;
    private CheckBox chkActivo;
    private Label lblActivoDetalle;
    private Label lblContactoAyuda;
}
