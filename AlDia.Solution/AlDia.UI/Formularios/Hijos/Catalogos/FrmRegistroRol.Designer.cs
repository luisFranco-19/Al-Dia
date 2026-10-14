namespace AlDia.UI.Formularios.Hijos.Catalogos;

partial class FrmRegistroRol
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
        tablaCampos = new TableLayoutPanel();
        campoNombre = new Panel();
        lblNombre = new Label();
        bordeNombre = new AlDia.UI.Controles.PanelTarjeta();
        txtNombre = new TextBox();
        campoActivo = new Panel();
        lblActivo = new Label();
        chkActivo = new CheckBox();
        campoDescripcion = new Panel();
        lblCampoDescripcion = new Label();
        bordeDescripcion = new AlDia.UI.Controles.PanelTarjeta();
        txtDescripcion = new TextBox();
        tablaCampos.SuspendLayout();
        campoNombre.SuspendLayout();
        bordeNombre.SuspendLayout();
        campoActivo.SuspendLayout();
        campoDescripcion.SuspendLayout();
        bordeDescripcion.SuspendLayout();
        SuspendLayout();
        Name = "FrmRegistroRol";
        Text = "Nuevo rol";
        ClientSize = new Size(820, 610);
        lblTitulo.Text = "Nuevo rol";
        lblDescripcion.Text = "Complete el nombre y la descripción del perfil.";
        iconModulo.IconChar = FontAwesome.Sharp.IconChar.UserShield;
        btnGuardar.Text = "Guardar rol";
        tablaCampos.Name = "tablaCampos";
        tablaCampos.Dock = DockStyle.Top;
        tablaCampos.AutoSize = true;
        tablaCampos.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tablaCampos.ColumnCount = 2;
        tablaCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaCampos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaCampos.Margin = new Padding(0);
        tablaCampos.Size = new Size(764, 1);
        tablaCampos.RowCount = 2;
        tablaCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
        tablaCampos.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
        campoNombre.Name = "campoNombre";
        campoNombre.Dock = DockStyle.Fill;
        campoNombre.Margin = new Padding(0, 0, 12, 8);
        campoNombre.Size = new Size(370, 84);
        campoNombre.TabIndex = 0;
        lblNombre.Name = "lblNombre";
        lblNombre.Text = "Nombre del rol *";
        lblNombre.Location = new Point(0, 0);
        lblNombre.Size = new Size(370, 24);
        lblNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblNombre.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblNombre.ForeColor = Color.FromArgb(20, 43, 78);
        lblNombre.TabStop = false;
        campoNombre.Controls.Add(lblNombre);
        bordeNombre.Name = "bordeNombre";
        bordeNombre.Location = new Point(0, 30);
        bordeNombre.Size = new Size(370, 44);
        bordeNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        bordeNombre.BackColor = Color.White;
        bordeNombre.RadioBorde = 8;
        bordeNombre.TabIndex = 0;
        campoNombre.Controls.Add(bordeNombre);
        txtNombre.BorderStyle = BorderStyle.None;
        txtNombre.PlaceholderText = "Ingrese el nombre del rol";
        txtNombre.Name = "txtNombre";
        txtNombre.Location = new Point(12, 11);
        txtNombre.Size = new Size(344, 24);
        txtNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtNombre.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
        txtNombre.ForeColor = Color.FromArgb(20, 43, 78);
        txtNombre.TabIndex = 0;
        bordeNombre.Controls.Add(txtNombre);
        tablaCampos.Controls.Add(campoNombre, 0, 0);
        campoActivo.Name = "campoActivo";
        campoActivo.Dock = DockStyle.Fill;
        campoActivo.Margin = new Padding(12, 0, 0, 8);
        campoActivo.Size = new Size(370, 84);
        campoActivo.TabIndex = 1;
        lblActivo.Name = "lblActivo";
        lblActivo.Text = "Estado";
        lblActivo.Location = new Point(0, 0);
        lblActivo.Size = new Size(370, 24);
        lblActivo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblActivo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblActivo.ForeColor = Color.FromArgb(20, 43, 78);
        lblActivo.TabStop = false;
        campoActivo.Controls.Add(lblActivo);
        chkActivo.Name = "chkActivo";
        chkActivo.Text = "Rol activo";
        chkActivo.Location = new Point(12, 36);
        chkActivo.Size = new Size(340, 30);
        chkActivo.Checked = true;
        chkActivo.CheckState = CheckState.Checked;
        chkActivo.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        chkActivo.ForeColor = Color.FromArgb(20, 43, 78);
        chkActivo.TabIndex = 0;
        campoActivo.Controls.Add(chkActivo);
        tablaCampos.Controls.Add(campoActivo, 1, 0);
        campoDescripcion.Name = "campoDescripcion";
        campoDescripcion.Dock = DockStyle.Fill;
        campoDescripcion.Margin = new Padding(0, 0, 12, 8);
        campoDescripcion.Size = new Size(370, 142);
        campoDescripcion.TabIndex = 2;
        lblCampoDescripcion.Name = "lblCampoDescripcion";
        lblCampoDescripcion.Text = "Descripción (opcional)";
        lblCampoDescripcion.Location = new Point(0, 0);
        lblCampoDescripcion.Size = new Size(370, 24);
        lblCampoDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblCampoDescripcion.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        lblCampoDescripcion.ForeColor = Color.FromArgb(20, 43, 78);
        lblCampoDescripcion.TabStop = false;
        campoDescripcion.Controls.Add(lblCampoDescripcion);
        bordeDescripcion.Name = "bordeDescripcion";
        bordeDescripcion.Location = new Point(0, 30);
        bordeDescripcion.Size = new Size(370, 108);
        bordeDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        bordeDescripcion.BackColor = Color.White;
        bordeDescripcion.RadioBorde = 8;
        bordeDescripcion.TabIndex = 0;
        campoDescripcion.Controls.Add(bordeDescripcion);
        txtDescripcion.BorderStyle = BorderStyle.None;
        txtDescripcion.PlaceholderText = "Descripción del perfil de acceso";
        txtDescripcion.Multiline = true;
        txtDescripcion.ScrollBars = ScrollBars.Vertical;
        txtDescripcion.Name = "txtDescripcion";
        txtDescripcion.Location = new Point(12, 11);
        txtDescripcion.Size = new Size(344, 84);
        txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtDescripcion.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
        txtDescripcion.ForeColor = Color.FromArgb(20, 43, 78);
        txtDescripcion.TabIndex = 0;
        bordeDescripcion.Controls.Add(txtDescripcion);
        tablaCampos.Controls.Add(campoDescripcion, 0, 1);
        panelCuerpo.Controls.Add(tablaCampos);
        bordeDescripcion.ResumeLayout(false);
        bordeDescripcion.PerformLayout();
        campoDescripcion.ResumeLayout(false);
        campoDescripcion.PerformLayout();
        campoActivo.ResumeLayout(false);
        campoActivo.PerformLayout();
        bordeNombre.ResumeLayout(false);
        bordeNombre.PerformLayout();
        campoNombre.ResumeLayout(false);
        campoNombre.PerformLayout();
        tablaCampos.ResumeLayout(false);
        tablaCampos.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tablaCampos;
    private Panel campoNombre;
    private Label lblNombre;
    private AlDia.UI.Controles.PanelTarjeta bordeNombre;
    private TextBox txtNombre;
    private Panel campoActivo;
    private Label lblActivo;
    private CheckBox chkActivo;
    private Panel campoDescripcion;
    private Label lblCampoDescripcion;
    private AlDia.UI.Controles.PanelTarjeta bordeDescripcion;
    private TextBox txtDescripcion;
}

