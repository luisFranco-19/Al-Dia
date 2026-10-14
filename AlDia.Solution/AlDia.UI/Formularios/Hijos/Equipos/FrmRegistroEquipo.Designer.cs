#nullable disable

namespace AlDia.UI.Formularios.Hijos.Equipos;

partial class FrmRegistroEquipo
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
        panelPropietario = new Panel();
        lblPropietario = new Label();
        lineaPropietario = new Panel();
        lblCliente = new Label();
        panelCliente = new AlDia.UI.Controles.PanelTarjeta();
        cboCliente = new ComboBox();
        lblTipoEquipo = new Label();
        panelTipoEquipo = new AlDia.UI.Controles.PanelTarjeta();
        cboTipoEquipo = new ComboBox();
        lblMarca = new Label();
        panelMarca = new AlDia.UI.Controles.PanelTarjeta();
        txtMarca = new TextBox();
        lblModelo = new Label();
        panelModelo = new AlDia.UI.Controles.PanelTarjeta();
        txtModelo = new TextBox();
        panelDetalles = new Panel();
        lblDetalles = new Label();
        lineaDetalles = new Panel();
        lblNumeroSerie = new Label();
        panelNumeroSerie = new AlDia.UI.Controles.PanelTarjeta();
        txtNumeroSerie = new TextBox();
        lblColor = new Label();
        panelColor = new AlDia.UI.Controles.PanelTarjeta();
        txtColor = new TextBox();
        lblObservaciones = new Label();
        panelObservaciones = new AlDia.UI.Controles.PanelTarjeta();
        txtObservaciones = new TextBox();
        lblActivo = new Label();
        panelActivo = new AlDia.UI.Controles.PanelTarjeta();
        chkActivo = new CheckBox();
        lblActivoDetalle = new Label();
        panelCuerpo.SuspendLayout();
        tablaSecciones.SuspendLayout();
        panelPropietario.SuspendLayout();
        panelCliente.SuspendLayout();
        panelTipoEquipo.SuspendLayout();
        panelMarca.SuspendLayout();
        panelModelo.SuspendLayout();
        panelDetalles.SuspendLayout();
        panelNumeroSerie.SuspendLayout();
        panelColor.SuspendLayout();
        panelObservaciones.SuspendLayout();
        panelActivo.SuspendLayout();
        SuspendLayout();
        // Controles heredados del formulario de registro base
        lblTitulo.Text = "Nuevo equipo";
        lblDescripcion.Text = "Complete los datos del propietario y las características del equipo.";
        iconModulo.IconChar = FontAwesome.Sharp.IconChar.Laptop;
        btnGuardar.Text = "Guardar equipo";
        // tablaSecciones
        tablaSecciones.Name = "tablaSecciones";
        tablaSecciones.Dock = DockStyle.Top;
        tablaSecciones.Size = new Size(764, 490);
        tablaSecciones.ColumnCount = 2;
        tablaSecciones.RowCount = 1;
        tablaSecciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaSecciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaSecciones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablaSecciones.Margin = new Padding(0);
        tablaSecciones.TabIndex = 0;
        // panelPropietario
        panelPropietario.Name = "panelPropietario";
        panelPropietario.Dock = DockStyle.Fill;
        panelPropietario.Margin = new Padding(0, 0, 12, 0);
        panelPropietario.Size = new Size(370, 490);
        panelPropietario.TabIndex = 0;
        // lblPropietario
        lblPropietario.Name = "lblPropietario";
        lblPropietario.Text = "01   Propietario y equipo";
        lblPropietario.Location = new Point(0, 0);
        lblPropietario.Size = new Size(370, 28);
        lblPropietario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblPropietario.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
        lblPropietario.ForeColor = Color.FromArgb(20, 43, 78);
        lblPropietario.TabStop = false;
        lblPropietario.AutoEllipsis = true;
        // lineaPropietario
        lineaPropietario.Name = "lineaPropietario";
        lineaPropietario.Location = new Point(0, 37);
        lineaPropietario.Size = new Size(370, 1);
        lineaPropietario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lineaPropietario.BackColor = Color.FromArgb(211, 221, 232);
        // lblCliente
        lblCliente.Name = "lblCliente";
        lblCliente.Text = "Cliente *";
        lblCliente.Location = new Point(0, 54);
        lblCliente.Size = new Size(370, 23);
        lblCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblCliente.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblCliente.ForeColor = Color.FromArgb(20, 43, 78);
        lblCliente.TabStop = false;
        // panelCliente
        panelCliente.Name = "panelCliente";
        panelCliente.Location = new Point(0, 82);
        panelCliente.Size = new Size(370, 42);
        panelCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelCliente.BackColor = Color.White;
        panelCliente.RadioBorde = 8;
        panelCliente.ColorBorde = Color.FromArgb(211, 221, 232);
        panelCliente.TabIndex = 0;
        // cboCliente
        cboCliente.Name = "cboCliente";
        cboCliente.Location = new Point(10, 7);
        cboCliente.Size = new Size(350, 28);
        cboCliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboCliente.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        cboCliente.ForeColor = Color.FromArgb(20, 43, 78);
        cboCliente.TabIndex = 0;
        cboCliente.BackColor = Color.White;
        cboCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCliente.FlatStyle = FlatStyle.Flat;
        cboCliente.Items.AddRange(new object[] { "Seleccione un cliente" });
        cboCliente.SelectedIndex = 0;
        // lblTipoEquipo
        lblTipoEquipo.Name = "lblTipoEquipo";
        lblTipoEquipo.Text = "Tipo de equipo *";
        lblTipoEquipo.Location = new Point(0, 136);
        lblTipoEquipo.Size = new Size(370, 23);
        lblTipoEquipo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblTipoEquipo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblTipoEquipo.ForeColor = Color.FromArgb(20, 43, 78);
        lblTipoEquipo.TabStop = false;
        // panelTipoEquipo
        panelTipoEquipo.Name = "panelTipoEquipo";
        panelTipoEquipo.Location = new Point(0, 164);
        panelTipoEquipo.Size = new Size(370, 42);
        panelTipoEquipo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelTipoEquipo.BackColor = Color.White;
        panelTipoEquipo.RadioBorde = 8;
        panelTipoEquipo.ColorBorde = Color.FromArgb(211, 221, 232);
        panelTipoEquipo.TabIndex = 1;
        // cboTipoEquipo
        cboTipoEquipo.Name = "cboTipoEquipo";
        cboTipoEquipo.Location = new Point(10, 7);
        cboTipoEquipo.Size = new Size(350, 28);
        cboTipoEquipo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboTipoEquipo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        cboTipoEquipo.ForeColor = Color.FromArgb(20, 43, 78);
        cboTipoEquipo.TabIndex = 0;
        cboTipoEquipo.BackColor = Color.White;
        cboTipoEquipo.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTipoEquipo.FlatStyle = FlatStyle.Flat;
        cboTipoEquipo.Items.AddRange(new object[] { "Seleccione un tipo de equipo" });
        cboTipoEquipo.SelectedIndex = 0;
        // lblMarca
        lblMarca.Name = "lblMarca";
        lblMarca.Text = "Marca *";
        lblMarca.Location = new Point(0, 218);
        lblMarca.Size = new Size(370, 23);
        lblMarca.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblMarca.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblMarca.ForeColor = Color.FromArgb(20, 43, 78);
        lblMarca.TabStop = false;
        // panelMarca
        panelMarca.Name = "panelMarca";
        panelMarca.Location = new Point(0, 246);
        panelMarca.Size = new Size(370, 42);
        panelMarca.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelMarca.BackColor = Color.White;
        panelMarca.RadioBorde = 8;
        panelMarca.ColorBorde = Color.FromArgb(211, 221, 232);
        panelMarca.TabIndex = 2;
        // txtMarca
        txtMarca.Name = "txtMarca";
        txtMarca.Location = new Point(13, 11);
        txtMarca.Size = new Size(344, 24);
        txtMarca.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtMarca.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtMarca.ForeColor = Color.FromArgb(20, 43, 78);
        txtMarca.TabIndex = 0;
        txtMarca.BorderStyle = BorderStyle.None;
        txtMarca.PlaceholderText = "Ingrese la marca";
        txtMarca.MaxLength = 50;
        // lblModelo
        lblModelo.Name = "lblModelo";
        lblModelo.Text = "Modelo";
        lblModelo.Location = new Point(0, 300);
        lblModelo.Size = new Size(370, 23);
        lblModelo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblModelo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblModelo.ForeColor = Color.FromArgb(20, 43, 78);
        lblModelo.TabStop = false;
        // panelModelo
        panelModelo.Name = "panelModelo";
        panelModelo.Location = new Point(0, 328);
        panelModelo.Size = new Size(370, 42);
        panelModelo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelModelo.BackColor = Color.White;
        panelModelo.RadioBorde = 8;
        panelModelo.ColorBorde = Color.FromArgb(211, 221, 232);
        panelModelo.TabIndex = 3;
        // txtModelo
        txtModelo.Name = "txtModelo";
        txtModelo.Location = new Point(13, 11);
        txtModelo.Size = new Size(344, 24);
        txtModelo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtModelo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtModelo.ForeColor = Color.FromArgb(20, 43, 78);
        txtModelo.TabIndex = 0;
        txtModelo.BorderStyle = BorderStyle.None;
        txtModelo.PlaceholderText = "Ingrese el modelo";
        txtModelo.MaxLength = 100;
        // panelDetalles
        panelDetalles.Name = "panelDetalles";
        panelDetalles.Dock = DockStyle.Fill;
        panelDetalles.Margin = new Padding(12, 0, 0, 0);
        panelDetalles.Size = new Size(370, 490);
        panelDetalles.TabIndex = 1;
        // lblDetalles
        lblDetalles.Name = "lblDetalles";
        lblDetalles.Text = "02   Identificación y detalles";
        lblDetalles.Location = new Point(0, 0);
        lblDetalles.Size = new Size(370, 28);
        lblDetalles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblDetalles.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
        lblDetalles.ForeColor = Color.FromArgb(20, 43, 78);
        lblDetalles.TabStop = false;
        lblDetalles.AutoEllipsis = true;
        // lineaDetalles
        lineaDetalles.Name = "lineaDetalles";
        lineaDetalles.Location = new Point(0, 37);
        lineaDetalles.Size = new Size(370, 1);
        lineaDetalles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lineaDetalles.BackColor = Color.FromArgb(211, 221, 232);
        // lblNumeroSerie
        lblNumeroSerie.Name = "lblNumeroSerie";
        lblNumeroSerie.Text = "Número de serie";
        lblNumeroSerie.Location = new Point(0, 54);
        lblNumeroSerie.Size = new Size(370, 23);
        lblNumeroSerie.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblNumeroSerie.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblNumeroSerie.ForeColor = Color.FromArgb(20, 43, 78);
        lblNumeroSerie.TabStop = false;
        // panelNumeroSerie
        panelNumeroSerie.Name = "panelNumeroSerie";
        panelNumeroSerie.Location = new Point(0, 82);
        panelNumeroSerie.Size = new Size(370, 42);
        panelNumeroSerie.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelNumeroSerie.BackColor = Color.White;
        panelNumeroSerie.RadioBorde = 8;
        panelNumeroSerie.ColorBorde = Color.FromArgb(211, 221, 232);
        panelNumeroSerie.TabIndex = 0;
        // txtNumeroSerie
        txtNumeroSerie.Name = "txtNumeroSerie";
        txtNumeroSerie.Location = new Point(13, 11);
        txtNumeroSerie.Size = new Size(344, 24);
        txtNumeroSerie.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtNumeroSerie.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtNumeroSerie.ForeColor = Color.FromArgb(20, 43, 78);
        txtNumeroSerie.TabIndex = 0;
        txtNumeroSerie.BorderStyle = BorderStyle.None;
        txtNumeroSerie.PlaceholderText = "Ingrese el número de serie";
        txtNumeroSerie.MaxLength = 100;
        // lblColor
        lblColor.Name = "lblColor";
        lblColor.Text = "Color";
        lblColor.Location = new Point(0, 136);
        lblColor.Size = new Size(370, 23);
        lblColor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblColor.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblColor.ForeColor = Color.FromArgb(20, 43, 78);
        lblColor.TabStop = false;
        // panelColor
        panelColor.Name = "panelColor";
        panelColor.Location = new Point(0, 164);
        panelColor.Size = new Size(370, 42);
        panelColor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelColor.BackColor = Color.White;
        panelColor.RadioBorde = 8;
        panelColor.ColorBorde = Color.FromArgb(211, 221, 232);
        panelColor.TabIndex = 1;
        // txtColor
        txtColor.Name = "txtColor";
        txtColor.Location = new Point(13, 11);
        txtColor.Size = new Size(344, 24);
        txtColor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtColor.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtColor.ForeColor = Color.FromArgb(20, 43, 78);
        txtColor.TabIndex = 0;
        txtColor.BorderStyle = BorderStyle.None;
        txtColor.PlaceholderText = "Ingrese el color";
        txtColor.MaxLength = 50;
        // lblObservaciones
        lblObservaciones.Name = "lblObservaciones";
        lblObservaciones.Text = "Observaciones";
        lblObservaciones.Location = new Point(0, 218);
        lblObservaciones.Size = new Size(370, 23);
        lblObservaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblObservaciones.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblObservaciones.ForeColor = Color.FromArgb(20, 43, 78);
        lblObservaciones.TabStop = false;
        // panelObservaciones
        panelObservaciones.Name = "panelObservaciones";
        panelObservaciones.Location = new Point(0, 246);
        panelObservaciones.Size = new Size(370, 112);
        panelObservaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelObservaciones.BackColor = Color.White;
        panelObservaciones.RadioBorde = 8;
        panelObservaciones.ColorBorde = Color.FromArgb(211, 221, 232);
        panelObservaciones.TabIndex = 2;
        // txtObservaciones
        txtObservaciones.Name = "txtObservaciones";
        txtObservaciones.Location = new Point(13, 11);
        txtObservaciones.Size = new Size(344, 90);
        txtObservaciones.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtObservaciones.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtObservaciones.ForeColor = Color.FromArgb(20, 43, 78);
        txtObservaciones.TabIndex = 0;
        txtObservaciones.BorderStyle = BorderStyle.None;
        txtObservaciones.PlaceholderText = "Detalles adicionales del equipo";
        txtObservaciones.MaxLength = 500;
        txtObservaciones.Multiline = true;
        txtObservaciones.ScrollBars = ScrollBars.Vertical;
        txtObservaciones.AcceptsReturn = true;
        // lblActivo
        lblActivo.Name = "lblActivo";
        lblActivo.Text = "Estado del equipo";
        lblActivo.Location = new Point(0, 374);
        lblActivo.Size = new Size(370, 23);
        lblActivo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblActivo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblActivo.ForeColor = Color.FromArgb(20, 43, 78);
        lblActivo.TabStop = false;
        // panelActivo
        panelActivo.Name = "panelActivo";
        panelActivo.Location = new Point(0, 402);
        panelActivo.Size = new Size(370, 64);
        panelActivo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelActivo.BackColor = Color.White;
        panelActivo.RadioBorde = 8;
        panelActivo.ColorBorde = Color.FromArgb(211, 221, 232);
        panelActivo.TabIndex = 3;
        // chkActivo
        chkActivo.Name = "chkActivo";
        chkActivo.Location = new Point(13, 9);
        chkActivo.Size = new Size(344, 24);
        chkActivo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        chkActivo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        chkActivo.ForeColor = Color.FromArgb(20, 43, 78);
        chkActivo.TabIndex = 0;
        chkActivo.Text = "Equipo activo";
        chkActivo.Checked = true;
        chkActivo.CheckState = CheckState.Checked;
        chkActivo.UseVisualStyleBackColor = true;
        // lblActivoDetalle
        lblActivoDetalle.Name = "lblActivoDetalle";
        lblActivoDetalle.Text = "Indica si el equipo está activo.";
        lblActivoDetalle.Location = new Point(30, 35);
        lblActivoDetalle.Size = new Size(322, 20);
        lblActivoDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblActivoDetalle.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point);
        lblActivoDetalle.ForeColor = Color.FromArgb(103, 116, 137);
        lblActivoDetalle.TabStop = false;
        panelCuerpo.Controls.Add(tablaSecciones);
        tablaSecciones.Controls.Add(panelPropietario, 0, 0);
        panelPropietario.Controls.Add(lblPropietario);
        panelPropietario.Controls.Add(lineaPropietario);
        panelPropietario.Controls.Add(lblCliente);
        panelPropietario.Controls.Add(panelCliente);
        panelCliente.Controls.Add(cboCliente);
        panelPropietario.Controls.Add(lblTipoEquipo);
        panelPropietario.Controls.Add(panelTipoEquipo);
        panelTipoEquipo.Controls.Add(cboTipoEquipo);
        panelPropietario.Controls.Add(lblMarca);
        panelPropietario.Controls.Add(panelMarca);
        panelMarca.Controls.Add(txtMarca);
        panelPropietario.Controls.Add(lblModelo);
        panelPropietario.Controls.Add(panelModelo);
        panelModelo.Controls.Add(txtModelo);
        tablaSecciones.Controls.Add(panelDetalles, 1, 0);
        panelDetalles.Controls.Add(lblDetalles);
        panelDetalles.Controls.Add(lineaDetalles);
        panelDetalles.Controls.Add(lblNumeroSerie);
        panelDetalles.Controls.Add(panelNumeroSerie);
        panelNumeroSerie.Controls.Add(txtNumeroSerie);
        panelDetalles.Controls.Add(lblColor);
        panelDetalles.Controls.Add(panelColor);
        panelColor.Controls.Add(txtColor);
        panelDetalles.Controls.Add(lblObservaciones);
        panelDetalles.Controls.Add(panelObservaciones);
        panelObservaciones.Controls.Add(txtObservaciones);
        panelActivo.Controls.Add(lblActivoDetalle);
        panelDetalles.Controls.Add(lblActivo);
        panelDetalles.Controls.Add(panelActivo);
        panelActivo.Controls.Add(chkActivo);
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Name = "FrmRegistroEquipo";
        Text = "Al Día · Equipos";
        panelActivo.ResumeLayout(false);
        panelActivo.PerformLayout();
        panelObservaciones.ResumeLayout(false);
        panelObservaciones.PerformLayout();
        panelColor.ResumeLayout(false);
        panelColor.PerformLayout();
        panelNumeroSerie.ResumeLayout(false);
        panelNumeroSerie.PerformLayout();
        panelDetalles.ResumeLayout(false);
        panelDetalles.PerformLayout();
        panelModelo.ResumeLayout(false);
        panelModelo.PerformLayout();
        panelMarca.ResumeLayout(false);
        panelMarca.PerformLayout();
        panelTipoEquipo.ResumeLayout(false);
        panelTipoEquipo.PerformLayout();
        panelCliente.ResumeLayout(false);
        panelCliente.PerformLayout();
        panelPropietario.ResumeLayout(false);
        panelPropietario.PerformLayout();
        tablaSecciones.ResumeLayout(false);
        panelCuerpo.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tablaSecciones;
    private Panel panelPropietario;
    private Label lblPropietario;
    private Panel lineaPropietario;
    private Label lblCliente;
    private AlDia.UI.Controles.PanelTarjeta panelCliente;
    private ComboBox cboCliente;
    private Label lblTipoEquipo;
    private AlDia.UI.Controles.PanelTarjeta panelTipoEquipo;
    private ComboBox cboTipoEquipo;
    private Label lblMarca;
    private AlDia.UI.Controles.PanelTarjeta panelMarca;
    private TextBox txtMarca;
    private Label lblModelo;
    private AlDia.UI.Controles.PanelTarjeta panelModelo;
    private TextBox txtModelo;
    private Panel panelDetalles;
    private Label lblDetalles;
    private Panel lineaDetalles;
    private Label lblNumeroSerie;
    private AlDia.UI.Controles.PanelTarjeta panelNumeroSerie;
    private TextBox txtNumeroSerie;
    private Label lblColor;
    private AlDia.UI.Controles.PanelTarjeta panelColor;
    private TextBox txtColor;
    private Label lblObservaciones;
    private AlDia.UI.Controles.PanelTarjeta panelObservaciones;
    private TextBox txtObservaciones;
    private Label lblActivo;
    private AlDia.UI.Controles.PanelTarjeta panelActivo;
    private CheckBox chkActivo;
    private Label lblActivoDetalle;
}
