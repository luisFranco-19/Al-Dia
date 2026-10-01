namespace AlDia.UI.Formularios.Seguridad;

partial class FrmLogin
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
        panelContenido = new Panel();
        panelBotones = new Panel();
        tableLayoutPanelBotones = new TableLayoutPanel();
        btnIngresar = new FontAwesome.Sharp.IconButton();
        btnSalir = new FontAwesome.Sharp.IconButton();
        panelCabecera = new Panel();
        pictureCerrar = new PictureBox();
        lblDescripcion = new Label();
        lblTitulo = new Label();
        iconAcceso = new FontAwesome.Sharp.IconPictureBox();
        panelUsuario = new Panel();
        txtUsuario = new TextBox();
        lblUsuario = new Label();
        panelContrasena = new Panel();
        txtContrasena = new TextBox();
        btnVerContrasena = new FontAwesome.Sharp.IconButton();
        lblContrasena = new Label();
        lblMensaje = new Label();
        btnCrearAdministrador = new FontAwesome.Sharp.IconButton();
        errorIcon = new ErrorProvider(components);
        ayudaControles = new ToolTip(components);
        panelContenido.SuspendLayout();
        panelBotones.SuspendLayout();
        tableLayoutPanelBotones.SuspendLayout();
        panelCabecera.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureCerrar).BeginInit();
        ((System.ComponentModel.ISupportInitialize)iconAcceso).BeginInit();
        panelUsuario.SuspendLayout();
        panelContrasena.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
        SuspendLayout();
        // 
        // panelContenido
        // 
        panelContenido.BackColor = Color.Transparent;
        panelContenido.Controls.Add(panelBotones);
        panelContenido.Controls.Add(panelCabecera);
        panelContenido.Controls.Add(panelUsuario);
        panelContenido.Controls.Add(lblUsuario);
        panelContenido.Controls.Add(panelContrasena);
        panelContenido.Controls.Add(lblContrasena);
        panelContenido.Controls.Add(lblMensaje);
        panelContenido.Controls.Add(btnCrearAdministrador);
        panelContenido.Dock = DockStyle.Fill;
        panelContenido.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
        panelContenido.Location = new Point(0, 0);
        panelContenido.Margin = new Padding(3, 4, 3, 4);
        panelContenido.Name = "panelContenido";
        panelContenido.Size = new Size(550, 499);
        panelContenido.TabIndex = 0;
        panelContenido.Paint += panelContenido_Paint;
        // 
        // panelBotones
        // 
        panelBotones.BackColor = Color.White;
        panelBotones.Controls.Add(tableLayoutPanelBotones);
        panelBotones.Dock = DockStyle.Bottom;
        panelBotones.Location = new Point(0, 406);
        panelBotones.Margin = new Padding(3, 4, 3, 4);
        panelBotones.Name = "panelBotones";
        panelBotones.Size = new Size(550, 93);
        panelBotones.TabIndex = 6;
        // 
        // tableLayoutPanelBotones
        // 
        tableLayoutPanelBotones.ColumnCount = 4;
        tableLayoutPanelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 246F));
        tableLayoutPanelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 255F));
        tableLayoutPanelBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 23F));
        tableLayoutPanelBotones.Controls.Add(btnIngresar, 1, 0);
        tableLayoutPanelBotones.Controls.Add(btnSalir, 2, 0);
        tableLayoutPanelBotones.Dock = DockStyle.Bottom;
        tableLayoutPanelBotones.Location = new Point(0, 0);
        tableLayoutPanelBotones.Margin = new Padding(3, 4, 3, 4);
        tableLayoutPanelBotones.Name = "tableLayoutPanelBotones";
        tableLayoutPanelBotones.RowCount = 1;
        tableLayoutPanelBotones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanelBotones.Size = new Size(550, 93);
        tableLayoutPanelBotones.TabIndex = 0;
        // 
        // btnIngresar
        // 
        btnIngresar.Dock = DockStyle.Top;
        btnIngresar.Enabled = false;
        btnIngresar.IconChar = FontAwesome.Sharp.IconChar.RightToBracket;
        btnIngresar.IconColor = Color.FromArgb(37, 99, 235);
        btnIngresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnIngresar.IconSize = 24;
        btnIngresar.Location = new Point(29, 4);
        btnIngresar.Margin = new Padding(3, 4, 3, 4);
        btnIngresar.Name = "btnIngresar";
        btnIngresar.Size = new Size(240, 67);
        btnIngresar.TabIndex = 0;
        btnIngresar.Text = "Ingresar";
        btnIngresar.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnIngresar.UseVisualStyleBackColor = true;
        btnIngresar.Click += btnIngresar_Click;
        // 
        // btnSalir
        // 
        btnSalir.Dock = DockStyle.Top;
        btnSalir.IconChar = FontAwesome.Sharp.IconChar.Close;
        btnSalir.IconColor = Color.FromArgb(220, 53, 69);
        btnSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnSalir.IconSize = 28;
        btnSalir.Location = new Point(275, 4);
        btnSalir.Margin = new Padding(3, 4, 3, 4);
        btnSalir.Name = "btnSalir";
        btnSalir.Size = new Size(249, 67);
        btnSalir.TabIndex = 1;
        btnSalir.Text = "Salir";
        btnSalir.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnSalir.UseVisualStyleBackColor = true;
        btnSalir.Click += btnSalir_Click;
        // 
        // panelCabecera
        // 
        panelCabecera.BackColor = Color.White;
        panelCabecera.Controls.Add(pictureCerrar);
        panelCabecera.Controls.Add(lblDescripcion);
        panelCabecera.Controls.Add(lblTitulo);
        panelCabecera.Controls.Add(iconAcceso);
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
        pictureCerrar.Click += btnSalir_Click;
        // 
        // lblDescripcion
        // 
        lblDescripcion.AutoSize = true;
        lblDescripcion.ForeColor = Color.DarkGray;
        lblDescripcion.Location = new Point(84, 50);
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Size = new Size(450, 25);
        lblDescripcion.TabIndex = 1;
        lblDescripcion.Text = "Por favor Ingresa tus datos para acceder al sistema.";
        // 
        // lblTitulo
        // 
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
        lblTitulo.Location = new Point(99, 9);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(324, 41);
        lblTitulo.TabIndex = 2;
        lblTitulo.Text = "¡Bienvenido de nuevo!";
        // 
        // iconAcceso
        // 
        iconAcceso.BackColor = Color.White;
        iconAcceso.ForeColor = Color.FromArgb(37, 99, 235);
        iconAcceso.IconChar = FontAwesome.Sharp.IconChar.UserLock;
        iconAcceso.IconColor = Color.FromArgb(37, 99, 235);
        iconAcceso.IconFont = FontAwesome.Sharp.IconFont.Auto;
        iconAcceso.IconSize = 55;
        iconAcceso.Location = new Point(23, 19);
        iconAcceso.Margin = new Padding(3, 4, 3, 4);
        iconAcceso.Name = "iconAcceso";
        iconAcceso.Size = new Size(55, 55);
        iconAcceso.SizeMode = PictureBoxSizeMode.AutoSize;
        iconAcceso.TabIndex = 3;
        iconAcceso.TabStop = false;
        // 
        // panelUsuario
        // 
        panelUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelUsuario.BackColor = Color.White;
        panelUsuario.Controls.Add(txtUsuario);
        panelUsuario.Cursor = Cursors.IBeam;
        panelUsuario.Location = new Point(67, 157);
        panelUsuario.Margin = new Padding(3, 4, 3, 4);
        panelUsuario.Name = "panelUsuario";
        panelUsuario.Padding = new Padding(5, 0, 11, 0);
        panelUsuario.Size = new Size(426, 53);
        panelUsuario.TabIndex = 1;
        panelUsuario.MouseClick += panelUsuario_MouseClick;
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
        txtUsuario.Size = new Size(389, 25);
        txtUsuario.TabIndex = 0;
        // 
        // lblUsuario
        // 
        lblUsuario.AutoSize = true;
        lblUsuario.Location = new Point(80, 128);
        lblUsuario.Name = "lblUsuario";
        lblUsuario.Size = new Size(77, 25);
        lblUsuario.TabIndex = 7;
        lblUsuario.Text = "Usuario";
        // 
        // panelContrasena
        // 
        panelContrasena.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelContrasena.BackColor = Color.White;
        panelContrasena.Controls.Add(txtContrasena);
        panelContrasena.Controls.Add(btnVerContrasena);
        panelContrasena.Cursor = Cursors.IBeam;
        panelContrasena.Location = new Point(67, 253);
        panelContrasena.Margin = new Padding(3, 4, 3, 4);
        panelContrasena.Name = "panelContrasena";
        panelContrasena.Padding = new Padding(5, 0, 11, 0);
        panelContrasena.Size = new Size(426, 53);
        panelContrasena.TabIndex = 2;
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
        txtContrasena.Size = new Size(336, 25);
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
        btnVerContrasena.Location = new Point(378, 7);
        btnVerContrasena.Margin = new Padding(3, 4, 3, 4);
        btnVerContrasena.Name = "btnVerContrasena";
        btnVerContrasena.Size = new Size(37, 40);
        btnVerContrasena.TabIndex = 1;
        ayudaControles.SetToolTip(btnVerContrasena, "Mostrar contraseña");
        btnVerContrasena.UseVisualStyleBackColor = false;
        btnVerContrasena.Click += btnVerContrasena_Click;
        // 
        // lblContrasena
        // 
        lblContrasena.AutoSize = true;
        lblContrasena.Location = new Point(80, 224);
        lblContrasena.Name = "lblContrasena";
        lblContrasena.Size = new Size(109, 25);
        lblContrasena.TabIndex = 8;
        lblContrasena.Text = "Contraseña";
        // 
        // lblMensaje
        // 
        lblMensaje.ForeColor = Color.FromArgb(84, 94, 110);
        lblMensaje.Location = new Point(58, 322);
        lblMensaje.Name = "lblMensaje";
        lblMensaje.Size = new Size(435, 32);
        lblMensaje.TabIndex = 9;
        lblMensaje.Text = "Ingrese su usuario y contraseña.";
        // 
        // btnCrearAdministrador
        // 
        btnCrearAdministrador.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
        btnCrearAdministrador.IconColor = Color.FromArgb(37, 99, 235);
        btnCrearAdministrador.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnCrearAdministrador.IconSize = 24;
        btnCrearAdministrador.Location = new Point(172, 358);
        btnCrearAdministrador.Margin = new Padding(3, 4, 3, 4);
        btnCrearAdministrador.Name = "btnCrearAdministrador";
        btnCrearAdministrador.Size = new Size(175, 40);
        btnCrearAdministrador.TabIndex = 4;
        btnCrearAdministrador.Text = "¿Registrase?";
        btnCrearAdministrador.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnCrearAdministrador.UseVisualStyleBackColor = true;
        btnCrearAdministrador.Visible = false;
        btnCrearAdministrador.Click += btnCrearAdministrador_Click;
        // 
        // errorIcon
        // 
        errorIcon.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        errorIcon.ContainerControl = this;
        // 
        // FrmLogin
        // 
        AcceptButton = btnIngresar;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnSalir;
        ClientSize = new Size(550, 499);
        Controls.Add(panelContenido);
        FormBorderStyle = FormBorderStyle.None;
        Margin = new Padding(3, 4, 3, 4);
        Name = "FrmLogin";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Al Día · Iniciar sesión";
        panelContenido.ResumeLayout(false);
        panelContenido.PerformLayout();
        panelBotones.ResumeLayout(false);
        tableLayoutPanelBotones.ResumeLayout(false);
        panelCabecera.ResumeLayout(false);
        panelCabecera.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureCerrar).EndInit();
        ((System.ComponentModel.ISupportInitialize)iconAcceso).EndInit();
        panelUsuario.ResumeLayout(false);
        panelUsuario.PerformLayout();
        panelContrasena.ResumeLayout(false);
        panelContrasena.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel panelContenido;
    private Panel panelCabecera;
    private Panel panelBotones;
    private TableLayoutPanel tableLayoutPanelBotones;
    private PictureBox pictureCerrar;
    private FontAwesome.Sharp.IconPictureBox iconAcceso;
    private Label lblTitulo;
    private Label lblDescripcion;
    private Label lblUsuario;
    private Panel panelUsuario;
    private TextBox txtUsuario;
    private Label lblContrasena;
    private Panel panelContrasena;
    private TextBox txtContrasena;
    private FontAwesome.Sharp.IconButton btnVerContrasena;
    private Label lblMensaje;
    private FontAwesome.Sharp.IconButton btnIngresar;
    private FontAwesome.Sharp.IconButton btnCrearAdministrador;
    private FontAwesome.Sharp.IconButton btnSalir;
    private ErrorProvider errorIcon;
    private ToolTip ayudaControles;
}
