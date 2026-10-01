namespace AlDia.UI.Formularios.Seguridad;

partial class FrmAdministradorInicial
{
    private System.ComponentModel.IContainer components;

    protected override void Dispose(bool disposing)
    {
        if (disposing && !_recursosLiberados)
        {
            _recursosLiberados = true;
            _cancelacion.Cancel();
            _cancelacion.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdministradorInicial));
        panelContenido = new Panel();
        panelBotones = new Panel();
        tableLayoutPanelBotones = new TableLayoutPanel();
        btnCrear = new FontAwesome.Sharp.IconButton();
        btnCancelar = new FontAwesome.Sharp.IconButton();
        panelCabecera = new Panel();
        pictureCerrar = new PictureBox();
        lblDescripcion = new Label();
        lblTitulo = new Label();
        iconAdministrador = new FontAwesome.Sharp.IconPictureBox();
        lblNombre = new Label();
        panelNombre = new Panel();
        txtNombre = new TextBox();
        lblApellido = new Label();
        panelApellido = new Panel();
        txtApellido = new TextBox();
        lblCedula = new Label();
        panelCedula = new Panel();
        txtCedula = new TextBox();
        lblUsuario = new Label();
        panelUsuario = new Panel();
        txtUsuario = new TextBox();
        lblContrasena = new Label();
        panelContrasena = new Panel();
        txtContrasena = new TextBox();
        btnVerContrasena = new FontAwesome.Sharp.IconButton();
        lblConfirmacion = new Label();
        panelConfirmacion = new Panel();
        txtConfirmacion = new TextBox();
        btnVerConfirmacion = new FontAwesome.Sharp.IconButton();
        lblMensaje = new Label();
        errorIcon = new ErrorProvider(components);
        ayudaControles = new ToolTip(components);
        panelContenido.SuspendLayout();
        panelBotones.SuspendLayout();
        tableLayoutPanelBotones.SuspendLayout();
        panelCabecera.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureCerrar).BeginInit();
        ((System.ComponentModel.ISupportInitialize)iconAdministrador).BeginInit();
        panelNombre.SuspendLayout();
        panelApellido.SuspendLayout();
        panelCedula.SuspendLayout();
        panelUsuario.SuspendLayout();
        panelContrasena.SuspendLayout();
        panelConfirmacion.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
        SuspendLayout();
        // 
        // panelContenido
        // 
        panelContenido.BackColor = Color.FromArgb(230, 236, 243);
        panelContenido.Controls.Add(panelBotones);
        panelContenido.Controls.Add(panelCabecera);
        panelContenido.Controls.Add(lblNombre);
        panelContenido.Controls.Add(panelNombre);
        panelContenido.Controls.Add(lblApellido);
        panelContenido.Controls.Add(panelApellido);
        panelContenido.Controls.Add(lblCedula);
        panelContenido.Controls.Add(panelCedula);
        panelContenido.Controls.Add(lblUsuario);
        panelContenido.Controls.Add(panelUsuario);
        panelContenido.Controls.Add(lblContrasena);
        panelContenido.Controls.Add(panelContrasena);
        panelContenido.Controls.Add(lblConfirmacion);
        panelContenido.Controls.Add(panelConfirmacion);
        panelContenido.Controls.Add(lblMensaje);
        panelContenido.Dock = DockStyle.Fill;
        panelContenido.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
        panelContenido.Location = new Point(0, 0);
        panelContenido.Margin = new Padding(3, 4, 3, 4);
        panelContenido.Name = "panelContenido";
        panelContenido.Size = new Size(550, 765);
        panelContenido.TabIndex = 0;
        // 
        // panelBotones
        // 
        panelBotones.BackColor = Color.White;
        panelBotones.Controls.Add(tableLayoutPanelBotones);
        panelBotones.Dock = DockStyle.Bottom;
        panelBotones.Location = new Point(0, 672);
        panelBotones.Margin = new Padding(3, 4, 3, 4);
        panelBotones.Name = "panelBotones";
        panelBotones.Size = new Size(550, 93);
        panelBotones.TabIndex = 8;
        // 
        // tableLayoutPanelBotones
        // 
        tableLayoutPanelBotones.ColumnCount = 4;
        tableLayoutPanelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
        tableLayoutPanelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 274F));
        tableLayoutPanelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
        tableLayoutPanelBotones.Controls.Add(btnCrear, 1, 0);
        tableLayoutPanelBotones.Controls.Add(btnCancelar, 2, 0);
        tableLayoutPanelBotones.Dock = DockStyle.Bottom;
        tableLayoutPanelBotones.Location = new Point(0, 0);
        tableLayoutPanelBotones.Margin = new Padding(3, 4, 3, 4);
        tableLayoutPanelBotones.Name = "tableLayoutPanelBotones";
        tableLayoutPanelBotones.RowCount = 1;
        tableLayoutPanelBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanelBotones.Size = new Size(550, 93);
        tableLayoutPanelBotones.TabIndex = 0;
        // 
        // btnCrear
        // 
        btnCrear.Cursor = Cursors.Hand;
        btnCrear.Dock = DockStyle.Top;
        btnCrear.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
        btnCrear.IconColor = Color.FromArgb(37, 99, 235);
        btnCrear.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnCrear.IconSize = 24;
        btnCrear.Location = new Point(46, 4);
        btnCrear.Margin = new Padding(3, 4, 3, 4);
        btnCrear.Name = "btnCrear";
        btnCrear.Size = new Size(204, 65);
        btnCrear.TabIndex = 0;
        btnCrear.Text = "Crear cuenta";
        btnCrear.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnCrear.UseVisualStyleBackColor = true;
        btnCrear.Click += btnCrear_Click;
        // 
        // btnCancelar
        // 
        btnCancelar.Dock = DockStyle.Top;
        btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Close;
        btnCancelar.IconColor = Color.FromArgb(220, 53, 69);
        btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnCancelar.IconSize = 28;
        btnCancelar.Location = new Point(256, 4);
        btnCancelar.Margin = new Padding(3, 4, 3, 4);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(268, 67);
        btnCancelar.TabIndex = 1;
        btnCancelar.Text = "Cancelar";
        btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += btnCancelar_Click;
        // 
        // panelCabecera
        // 
        panelCabecera.BackColor = Color.White;
        panelCabecera.Controls.Add(pictureCerrar);
        panelCabecera.Controls.Add(lblDescripcion);
        panelCabecera.Controls.Add(lblTitulo);
        panelCabecera.Controls.Add(iconAdministrador);
        panelCabecera.Dock = DockStyle.Top;
        panelCabecera.Location = new Point(0, 0);
        panelCabecera.Margin = new Padding(3, 4, 3, 4);
        panelCabecera.Name = "panelCabecera";
        panelCabecera.Size = new Size(550, 115);
        panelCabecera.TabIndex = 0;
        // 
        // pictureCerrar
        // 
        pictureCerrar.Cursor = Cursors.Hand;
        pictureCerrar.Image = (Image)resources.GetObject("pictureCerrar.Image");
        pictureCerrar.Location = new Point(502, 19);
        pictureCerrar.Margin = new Padding(3, 4, 3, 4);
        pictureCerrar.Name = "pictureCerrar";
        pictureCerrar.Size = new Size(24, 24);
        pictureCerrar.SizeMode = PictureBoxSizeMode.AutoSize;
        pictureCerrar.TabIndex = 0;
        pictureCerrar.TabStop = false;
        pictureCerrar.Click += btnCancelar_Click;
        // 
        // lblDescripcion
        // 
        lblDescripcion.AutoSize = true;
        lblDescripcion.ForeColor = Color.DarkGray;
        lblDescripcion.Location = new Point(99, 56);
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Size = new Size(331, 25);
        lblDescripcion.TabIndex = 1;
        lblDescripcion.Text = "Complete la información del registro.";
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTitulo.Location = new Point(99, 9);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(321, 41);
        lblTitulo.TabIndex = 2;
        lblTitulo.Text = "Registrar Credenciales";
        // 
        // iconAdministrador
        // 
        iconAdministrador.BackColor = Color.White;
        iconAdministrador.ForeColor = Color.FromArgb(37, 99, 235);
        iconAdministrador.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
        iconAdministrador.IconColor = Color.FromArgb(37, 99, 235);
        iconAdministrador.IconFont = FontAwesome.Sharp.IconFont.Auto;
        iconAdministrador.IconSize = 55;
        iconAdministrador.Location = new Point(23, 9);
        iconAdministrador.Margin = new Padding(3, 4, 3, 4);
        iconAdministrador.Name = "iconAdministrador";
        iconAdministrador.Size = new Size(55, 55);
        iconAdministrador.SizeMode = PictureBoxSizeMode.AutoSize;
        iconAdministrador.TabIndex = 3;
        iconAdministrador.TabStop = false;
        // 
        // lblNombre
        // 
        lblNombre.AutoSize = true;
        lblNombre.Location = new Point(23, 149);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(83, 25);
        lblNombre.TabIndex = 9;
        lblNombre.Text = "Nombre";
        // 
        // panelNombre
        // 
        panelNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelNombre.BackColor = Color.White;
        panelNombre.Controls.Add(txtNombre);
        panelNombre.Location = new Point(159, 149);
        panelNombre.Margin = new Padding(3, 4, 3, 4);
        panelNombre.Name = "panelNombre";
        panelNombre.Padding = new Padding(5, 0, 11, 0);
        panelNombre.Size = new Size(365, 53);
        panelNombre.TabIndex = 1;
        // 
        // txtNombre
        // 
        txtNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtNombre.BackColor = Color.White;
        txtNombre.BorderStyle = BorderStyle.None;
        txtNombre.Location = new Point(13, 13);
        txtNombre.Margin = new Padding(3, 4, 3, 4);
        txtNombre.MaxLength = 100;
        txtNombre.Name = "txtNombre";
        txtNombre.Size = new Size(328, 25);
        txtNombre.TabIndex = 0;
        // 
        // lblApellido
        // 
        lblApellido.AutoSize = true;
        lblApellido.Location = new Point(23, 215);
        lblApellido.Name = "lblApellido";
        lblApellido.Size = new Size(83, 25);
        lblApellido.TabIndex = 10;
        lblApellido.Text = "Apellido";
        // 
        // panelApellido
        // 
        panelApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelApellido.BackColor = Color.White;
        panelApellido.Controls.Add(txtApellido);
        panelApellido.Location = new Point(159, 215);
        panelApellido.Margin = new Padding(3, 4, 3, 4);
        panelApellido.Name = "panelApellido";
        panelApellido.Padding = new Padding(5, 0, 11, 0);
        panelApellido.Size = new Size(365, 53);
        panelApellido.TabIndex = 2;
        // 
        // txtApellido
        // 
        txtApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtApellido.BackColor = Color.White;
        txtApellido.BorderStyle = BorderStyle.None;
        txtApellido.Location = new Point(13, 13);
        txtApellido.Margin = new Padding(3, 4, 3, 4);
        txtApellido.MaxLength = 100;
        txtApellido.Name = "txtApellido";
        txtApellido.Size = new Size(328, 25);
        txtApellido.TabIndex = 0;
        // 
        // lblCedula
        // 
        lblCedula.AutoSize = true;
        lblCedula.Location = new Point(23, 283);
        lblCedula.Name = "lblCedula";
        lblCedula.Size = new Size(71, 25);
        lblCedula.TabIndex = 11;
        lblCedula.Text = "Cédula";
        // 
        // panelCedula
        // 
        panelCedula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelCedula.BackColor = Color.White;
        panelCedula.Controls.Add(txtCedula);
        panelCedula.Location = new Point(159, 283);
        panelCedula.Margin = new Padding(3, 4, 3, 4);
        panelCedula.Name = "panelCedula";
        panelCedula.Padding = new Padding(5, 0, 11, 0);
        panelCedula.Size = new Size(365, 53);
        panelCedula.TabIndex = 3;
        // 
        // txtCedula
        // 
        txtCedula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtCedula.BackColor = Color.White;
        txtCedula.BorderStyle = BorderStyle.None;
        txtCedula.Location = new Point(13, 13);
        txtCedula.Margin = new Padding(3, 4, 3, 4);
        txtCedula.MaxLength = 20;
        txtCedula.Name = "txtCedula";
        txtCedula.Size = new Size(328, 25);
        txtCedula.TabIndex = 0;
        // 
        // lblUsuario
        // 
        lblUsuario.AutoSize = true;
        lblUsuario.Location = new Point(23, 352);
        lblUsuario.Name = "lblUsuario";
        lblUsuario.Size = new Size(77, 25);
        lblUsuario.TabIndex = 12;
        lblUsuario.Text = "Usuario";
        // 
        // panelUsuario
        // 
        panelUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelUsuario.BackColor = Color.White;
        panelUsuario.Controls.Add(txtUsuario);
        panelUsuario.Location = new Point(159, 352);
        panelUsuario.Margin = new Padding(3, 4, 3, 4);
        panelUsuario.Name = "panelUsuario";
        panelUsuario.Padding = new Padding(5, 0, 11, 0);
        panelUsuario.Size = new Size(365, 53);
        panelUsuario.TabIndex = 4;
        // 
        // txtUsuario
        // 
        txtUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtUsuario.BackColor = Color.White;
        txtUsuario.BorderStyle = BorderStyle.None;
        txtUsuario.Location = new Point(13, 13);
        txtUsuario.Margin = new Padding(3, 4, 3, 4);
        txtUsuario.MaxLength = 50;
        txtUsuario.Name = "txtUsuario";
        txtUsuario.Size = new Size(328, 25);
        txtUsuario.TabIndex = 0;
        // 
        // lblContrasena
        // 
        lblContrasena.AutoSize = true;
        lblContrasena.Location = new Point(23, 421);
        lblContrasena.Name = "lblContrasena";
        lblContrasena.Size = new Size(109, 25);
        lblContrasena.TabIndex = 13;
        lblContrasena.Text = "Contraseña";
        // 
        // panelContrasena
        // 
        panelContrasena.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelContrasena.BackColor = Color.White;
        panelContrasena.Controls.Add(txtContrasena);
        panelContrasena.Controls.Add(btnVerContrasena);
        panelContrasena.Cursor = Cursors.IBeam;
        panelContrasena.Location = new Point(159, 421);
        panelContrasena.Margin = new Padding(3, 4, 3, 4);
        panelContrasena.Name = "panelContrasena";
        panelContrasena.Padding = new Padding(5, 0, 11, 0);
        panelContrasena.Size = new Size(365, 53);
        panelContrasena.TabIndex = 5;
        panelContrasena.MouseClick += panelContrasena_MouseClick;
        // 
        // txtContrasena
        // 
        txtContrasena.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtContrasena.BackColor = Color.White;
        txtContrasena.BorderStyle = BorderStyle.None;
        txtContrasena.Location = new Point(13, 13);
        txtContrasena.Margin = new Padding(3, 4, 3, 4);
        txtContrasena.MaxLength = 128;
        txtContrasena.Name = "txtContrasena";
        txtContrasena.PasswordChar = '*';
        txtContrasena.Size = new Size(275, 25);
        txtContrasena.TabIndex = 0;
        // 
        // btnVerContrasena
        // 
        btnVerContrasena.AccessibleName = "Mostrar contraseña";
        btnVerContrasena.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnVerContrasena.BackColor = Color.White;
        btnVerContrasena.Cursor = Cursors.Hand;
        btnVerContrasena.FlatAppearance.BorderSize = 0;
        btnVerContrasena.FlatAppearance.MouseDownBackColor = Color.FromArgb(219, 234, 254);
        btnVerContrasena.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 236, 243);
        btnVerContrasena.FlatStyle = FlatStyle.Flat;
        btnVerContrasena.IconChar = FontAwesome.Sharp.IconChar.Eye;
        btnVerContrasena.IconColor = Color.FromArgb(84, 94, 110);
        btnVerContrasena.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnVerContrasena.IconSize = 22;
        btnVerContrasena.Location = new Point(317, 7);
        btnVerContrasena.Margin = new Padding(3, 4, 3, 4);
        btnVerContrasena.Name = "btnVerContrasena";
        btnVerContrasena.Size = new Size(37, 40);
        btnVerContrasena.TabIndex = 1;
        ayudaControles.SetToolTip(btnVerContrasena, "Mostrar contraseña");
        btnVerContrasena.UseVisualStyleBackColor = false;
        btnVerContrasena.Click += btnVerContrasena_Click;
        // 
        // lblConfirmacion
        // 
        lblConfirmacion.AutoSize = true;
        lblConfirmacion.Location = new Point(23, 491);
        lblConfirmacion.Name = "lblConfirmacion";
        lblConfirmacion.Size = new Size(99, 25);
        lblConfirmacion.TabIndex = 14;
        lblConfirmacion.Text = "Confirmar";
        // 
        // panelConfirmacion
        // 
        panelConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelConfirmacion.BackColor = Color.White;
        panelConfirmacion.Controls.Add(txtConfirmacion);
        panelConfirmacion.Controls.Add(btnVerConfirmacion);
        panelConfirmacion.Cursor = Cursors.IBeam;
        panelConfirmacion.Location = new Point(159, 491);
        panelConfirmacion.Margin = new Padding(3, 4, 3, 4);
        panelConfirmacion.Name = "panelConfirmacion";
        panelConfirmacion.Padding = new Padding(5, 0, 11, 0);
        panelConfirmacion.Size = new Size(365, 53);
        panelConfirmacion.TabIndex = 6;
        panelConfirmacion.MouseClick += panelConfirmacion_MouseClick;
        // 
        // txtConfirmacion
        // 
        txtConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtConfirmacion.BackColor = Color.White;
        txtConfirmacion.BorderStyle = BorderStyle.None;
        txtConfirmacion.Location = new Point(13, 13);
        txtConfirmacion.Margin = new Padding(3, 4, 3, 4);
        txtConfirmacion.MaxLength = 128;
        txtConfirmacion.Name = "txtConfirmacion";
        txtConfirmacion.PasswordChar = '*';
        txtConfirmacion.Size = new Size(275, 25);
        txtConfirmacion.TabIndex = 0;
        // 
        // btnVerConfirmacion
        // 
        btnVerConfirmacion.AccessibleName = "Mostrar confirmación de contraseña";
        btnVerConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnVerConfirmacion.BackColor = Color.White;
        btnVerConfirmacion.Cursor = Cursors.Hand;
        btnVerConfirmacion.FlatAppearance.BorderSize = 0;
        btnVerConfirmacion.FlatAppearance.MouseDownBackColor = Color.FromArgb(219, 234, 254);
        btnVerConfirmacion.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 236, 243);
        btnVerConfirmacion.FlatStyle = FlatStyle.Flat;
        btnVerConfirmacion.IconChar = FontAwesome.Sharp.IconChar.Eye;
        btnVerConfirmacion.IconColor = Color.FromArgb(84, 94, 110);
        btnVerConfirmacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnVerConfirmacion.IconSize = 22;
        btnVerConfirmacion.Location = new Point(317, 7);
        btnVerConfirmacion.Margin = new Padding(3, 4, 3, 4);
        btnVerConfirmacion.Name = "btnVerConfirmacion";
        btnVerConfirmacion.Size = new Size(37, 40);
        btnVerConfirmacion.TabIndex = 1;
        ayudaControles.SetToolTip(btnVerConfirmacion, "Mostrar confirmación de contraseña");
        btnVerConfirmacion.UseVisualStyleBackColor = false;
        btnVerConfirmacion.Click += btnVerConfirmacion_Click;
        // 
        // lblMensaje
        // 
        lblMensaje.ForeColor = Color.DarkGray;
        lblMensaje.Location = new Point(99, 603);
        lblMensaje.Name = "lblMensaje";
        lblMensaje.Size = new Size(379, 29);
        lblMensaje.TabIndex = 15;
        lblMensaje.Text = "Complete sus datos para crear la cuenta.";
        // 
        // errorIcon
        // 
        errorIcon.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        errorIcon.ContainerControl = this;
        // 
        // FrmAdministradorInicial
        // 
        AcceptButton = btnCrear;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancelar;
        ClientSize = new Size(550, 765);
        Controls.Add(panelContenido);
        FormBorderStyle = FormBorderStyle.None;
        Margin = new Padding(3, 4, 3, 4);
        Name = "FrmAdministradorInicial";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Al Día · Primer administrador";
        panelContenido.ResumeLayout(false);
        panelContenido.PerformLayout();
        panelBotones.ResumeLayout(false);
        tableLayoutPanelBotones.ResumeLayout(false);
        panelCabecera.ResumeLayout(false);
        panelCabecera.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureCerrar).EndInit();
        ((System.ComponentModel.ISupportInitialize)iconAdministrador).EndInit();
        panelNombre.ResumeLayout(false);
        panelNombre.PerformLayout();
        panelApellido.ResumeLayout(false);
        panelApellido.PerformLayout();
        panelCedula.ResumeLayout(false);
        panelCedula.PerformLayout();
        panelUsuario.ResumeLayout(false);
        panelUsuario.PerformLayout();
        panelContrasena.ResumeLayout(false);
        panelContrasena.PerformLayout();
        panelConfirmacion.ResumeLayout(false);
        panelConfirmacion.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelContenido;
    private Panel panelCabecera;
    private Panel panelBotones;
    private TableLayoutPanel tableLayoutPanelBotones;
    private PictureBox pictureCerrar;
    private FontAwesome.Sharp.IconPictureBox iconAdministrador;
    private Label lblTitulo;
    private Label lblDescripcion;
    private Label lblNombre;
    private Panel panelNombre;
    private TextBox txtNombre;
    private Label lblApellido;
    private Panel panelApellido;
    private TextBox txtApellido;
    private Label lblCedula;
    private Panel panelCedula;
    private TextBox txtCedula;
    private Label lblUsuario;
    private Panel panelUsuario;
    private TextBox txtUsuario;
    private Label lblContrasena;
    private Panel panelContrasena;
    private TextBox txtContrasena;
    private FontAwesome.Sharp.IconButton btnVerContrasena;
    private Label lblConfirmacion;
    private Panel panelConfirmacion;
    private TextBox txtConfirmacion;
    private FontAwesome.Sharp.IconButton btnVerConfirmacion;
    private Label lblMensaje;
    private FontAwesome.Sharp.IconButton btnCrear;
    private FontAwesome.Sharp.IconButton btnCancelar;
    private ErrorProvider errorIcon;
    private ToolTip ayudaControles;
}
