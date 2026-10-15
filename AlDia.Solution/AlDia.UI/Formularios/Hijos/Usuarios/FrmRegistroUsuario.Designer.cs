namespace AlDia.UI.Formularios.Hijos.Usuarios;

partial class FrmRegistroUsuario
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
        panelDatos = new Panel();
        lblDatos = new Label();
        lineaDatos = new Panel();
        lblNombre = new Label();
        panelNombre = new AlDia.UI.Controles.PanelTarjeta();
        txtNombre = new TextBox();
        lblApellido = new Label();
        panelApellido = new AlDia.UI.Controles.PanelTarjeta();
        txtApellido = new TextBox();
        lblCedula = new Label();
        panelCedula = new AlDia.UI.Controles.PanelTarjeta();
        txtCedula = new TextBox();
        lblTelefono = new Label();
        panelTelefono = new AlDia.UI.Controles.PanelTarjeta();
        txtTelefono = new TextBox();
        lblCorreo = new Label();
        panelCorreo = new AlDia.UI.Controles.PanelTarjeta();
        txtCorreo = new TextBox();
        panelAcceso = new Panel();
        lblAcceso = new Label();
        lineaAcceso = new Panel();
        lblUsuario = new Label();
        panelUsuario = new AlDia.UI.Controles.PanelTarjeta();
        txtUsuario = new TextBox();
        lblRol = new Label();
        panelRol = new AlDia.UI.Controles.PanelTarjeta();
        cboRol = new ComboBox();
        lblContrasena = new Label();
        panelContrasena = new AlDia.UI.Controles.PanelTarjeta();
        txtContrasena = new TextBox();
        lblConfirmacion = new Label();
        panelConfirmacion = new AlDia.UI.Controles.PanelTarjeta();
        txtConfirmacion = new TextBox();
        lblEstado = new Label();
        panelEstado = new AlDia.UI.Controles.PanelTarjeta();
        chkActivo = new CheckBox();
        lblEstadoDetalle = new Label();
        ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
        panelContenido.SuspendLayout();
        panelCabecera.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)iconModulo).BeginInit();
        panelCuerpo.SuspendLayout();
        panelPie.SuspendLayout();
        tablaSecciones.SuspendLayout();
        panelDatos.SuspendLayout();
        panelNombre.SuspendLayout();
        panelApellido.SuspendLayout();
        panelCedula.SuspendLayout();
        panelTelefono.SuspendLayout();
        panelCorreo.SuspendLayout();
        panelAcceso.SuspendLayout();
        panelUsuario.SuspendLayout();
        panelRol.SuspendLayout();
        panelContrasena.SuspendLayout();
        panelConfirmacion.SuspendLayout();
        panelEstado.SuspendLayout();
        SuspendLayout();
        // 
        // panelContenido
        // 
        panelContenido.Margin = new Padding(3, 5, 3, 5);
        panelContenido.Size = new Size(937, 660);
        // 
        // panelCabecera
        // 
        panelCabecera.Margin = new Padding(3, 5, 3, 5);
        panelCabecera.Size = new Size(937, 185);
        // 
        // iconModulo
        // 
        iconModulo.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
        iconModulo.IconSize = 72;
        iconModulo.Location = new Point(37, 43);
        iconModulo.Margin = new Padding(3, 5, 3, 5);
        iconModulo.Size = new Size(72, 87);
        // 
        // lblTitulo
        // 
        lblTitulo.Location = new Point(135, 32);
        lblTitulo.Size = new Size(744, 71);
        lblTitulo.Text = "Nuevo usuario";
        // 
        // lblDescripcion
        // 
        lblDescripcion.Location = new Point(137, 108);
        lblDescripcion.Size = new Size(810, 47);
        lblDescripcion.Text = "Complete la información para registrar una cuenta de usuario.";
        // 
        // btnCerrar
        // 
        btnCerrar.FlatAppearance.BorderSize = 0;
        btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCerrar.Location = new Point(848, 39);
        btnCerrar.Margin = new Padding(3, 5, 3, 5);
        btnCerrar.Size = new Size(47, 64);
        // 
        // panelCuerpo
        // 
        panelCuerpo.Controls.Add(tablaSecciones);
        panelCuerpo.Location = new Point(0, 185);
        panelCuerpo.Margin = new Padding(3, 5, 3, 5);
        panelCuerpo.Padding = new Padding(37, 43, 37, 36);
        panelCuerpo.Size = new Size(937, 332);
        // 
        // panelPie
        // 
        panelPie.Location = new Point(0, 517);
        panelPie.Margin = new Padding(3, 5, 3, 5);
        panelPie.Size = new Size(937, 143);
        // 
        // lineaPie
        // 
        lineaPie.Margin = new Padding(3, 5, 3, 5);
        // 
        // lblObligatorios
        // 
        lblObligatorios.Location = new Point(37, 49);
        lblObligatorios.Size = new Size(392, 43);
        // 
        // btnCancelar
        // 
        btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnCancelar.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 236, 243);
        btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCancelar.Location = new Point(514, 36);
        btnCancelar.Margin = new Padding(3, 5, 3, 5);
        btnCancelar.Size = new Size(173, 75);
        // 
        // btnGuardar
        // 
        btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnGuardar.FlatAppearance.BorderSize = 0;
        btnGuardar.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
        btnGuardar.Location = new Point(701, 36);
        btnGuardar.Margin = new Padding(3, 5, 3, 5);
        btnGuardar.Size = new Size(199, 75);
        btnGuardar.Text = "Guardar usuario";
        // 
        // tablaSecciones
        // 
        tablaSecciones.ColumnCount = 2;
        tablaSecciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaSecciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tablaSecciones.Controls.Add(panelDatos, 0, 0);
        tablaSecciones.Controls.Add(panelAcceso, 1, 0);
        tablaSecciones.Dock = DockStyle.Top;
        tablaSecciones.Location = new Point(37, 43);
        tablaSecciones.Margin = new Padding(0);
        tablaSecciones.Name = "tablaSecciones";
        tablaSecciones.RowCount = 1;
        tablaSecciones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablaSecciones.Size = new Size(842, 653);
        tablaSecciones.TabIndex = 0;
        // 
        // panelDatos
        // 
        panelDatos.Controls.Add(lblDatos);
        panelDatos.Controls.Add(lineaDatos);
        panelDatos.Controls.Add(lblNombre);
        panelDatos.Controls.Add(panelNombre);
        panelDatos.Controls.Add(lblApellido);
        panelDatos.Controls.Add(panelApellido);
        panelDatos.Controls.Add(lblCedula);
        panelDatos.Controls.Add(panelCedula);
        panelDatos.Controls.Add(lblTelefono);
        panelDatos.Controls.Add(panelTelefono);
        panelDatos.Controls.Add(lblCorreo);
        panelDatos.Controls.Add(panelCorreo);
        panelDatos.Dock = DockStyle.Fill;
        panelDatos.Location = new Point(0, 0);
        panelDatos.Margin = new Padding(0, 0, 14, 0);
        panelDatos.Name = "panelDatos";
        panelDatos.Size = new Size(407, 653);
        panelDatos.TabIndex = 0;
        // 
        // lblDatos
        // 
        lblDatos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblDatos.AutoEllipsis = true;
        lblDatos.BackColor = Color.Transparent;
        lblDatos.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblDatos.ForeColor = Color.FromArgb(20, 43, 78);
        lblDatos.Location = new Point(0, 0);
        lblDatos.Name = "lblDatos";
        lblDatos.Size = new Size(407, 37);
        lblDatos.TabIndex = 0;
        lblDatos.Text = "01   Datos personales";
        // 
        // lineaDatos
        // 
        lineaDatos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lineaDatos.BackColor = Color.FromArgb(205, 216, 230);
        lineaDatos.Location = new Point(0, 49);
        lineaDatos.Margin = new Padding(3, 4, 3, 4);
        lineaDatos.Name = "lineaDatos";
        lineaDatos.Size = new Size(407, 1);
        lineaDatos.TabIndex = 1;
        // 
        // lblNombre
        // 
        lblNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblNombre.AutoEllipsis = true;
        lblNombre.BackColor = Color.Transparent;
        lblNombre.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblNombre.ForeColor = Color.FromArgb(20, 43, 78);
        lblNombre.Location = new Point(0, 72);
        lblNombre.Name = "lblNombre";
        lblNombre.Size = new Size(407, 31);
        lblNombre.TabIndex = 2;
        lblNombre.Text = "Nombre *";
        // 
        // panelNombre
        // 
        panelNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelNombre.BackColor = Color.White;
        panelNombre.ColorBorde = Color.FromArgb(211, 221, 232);
        panelNombre.Controls.Add(txtNombre);
        panelNombre.Location = new Point(0, 109);
        panelNombre.Margin = new Padding(3, 4, 3, 4);
        panelNombre.Name = "panelNombre";
        panelNombre.RadioBorde = 8;
        panelNombre.Size = new Size(407, 56);
        panelNombre.TabIndex = 0;
        // 
        // txtNombre
        // 
        txtNombre.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtNombre.BorderStyle = BorderStyle.None;
        txtNombre.Font = new Font("Segoe UI", 10.5F);
        txtNombre.ForeColor = Color.FromArgb(20, 43, 78);
        txtNombre.Location = new Point(15, 15);
        txtNombre.Margin = new Padding(3, 4, 3, 4);
        txtNombre.Name = "txtNombre";
        txtNombre.PlaceholderText = "Ingrese el nombre";
        txtNombre.Size = new Size(377, 24);
        txtNombre.TabIndex = 0;
        // 
        // lblApellido
        // 
        lblApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblApellido.AutoEllipsis = true;
        lblApellido.BackColor = Color.Transparent;
        lblApellido.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblApellido.ForeColor = Color.FromArgb(20, 43, 78);
        lblApellido.Location = new Point(0, 181);
        lblApellido.Name = "lblApellido";
        lblApellido.Size = new Size(407, 31);
        lblApellido.TabIndex = 3;
        lblApellido.Text = "Apellido *";
        // 
        // panelApellido
        // 
        panelApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelApellido.BackColor = Color.White;
        panelApellido.ColorBorde = Color.FromArgb(211, 221, 232);
        panelApellido.Controls.Add(txtApellido);
        panelApellido.Location = new Point(0, 219);
        panelApellido.Margin = new Padding(3, 4, 3, 4);
        panelApellido.Name = "panelApellido";
        panelApellido.RadioBorde = 8;
        panelApellido.Size = new Size(407, 56);
        panelApellido.TabIndex = 1;
        // 
        // txtApellido
        // 
        txtApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtApellido.BorderStyle = BorderStyle.None;
        txtApellido.Font = new Font("Segoe UI", 10.5F);
        txtApellido.ForeColor = Color.FromArgb(20, 43, 78);
        txtApellido.Location = new Point(15, 15);
        txtApellido.Margin = new Padding(3, 4, 3, 4);
        txtApellido.Name = "txtApellido";
        txtApellido.PlaceholderText = "Ingrese el apellido";
        txtApellido.Size = new Size(377, 24);
        txtApellido.TabIndex = 0;
        // 
        // lblCedula
        // 
        lblCedula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblCedula.AutoEllipsis = true;
        lblCedula.BackColor = Color.Transparent;
        lblCedula.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblCedula.ForeColor = Color.FromArgb(20, 43, 78);
        lblCedula.Location = new Point(0, 291);
        lblCedula.Name = "lblCedula";
        lblCedula.Size = new Size(407, 31);
        lblCedula.TabIndex = 4;
        lblCedula.Text = "Cédula *";
        // 
        // panelCedula
        // 
        panelCedula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelCedula.BackColor = Color.White;
        panelCedula.ColorBorde = Color.FromArgb(211, 221, 232);
        panelCedula.Controls.Add(txtCedula);
        panelCedula.Location = new Point(0, 328);
        panelCedula.Margin = new Padding(3, 4, 3, 4);
        panelCedula.Name = "panelCedula";
        panelCedula.RadioBorde = 8;
        panelCedula.Size = new Size(407, 56);
        panelCedula.TabIndex = 2;
        // 
        // txtCedula
        // 
        txtCedula.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtCedula.BorderStyle = BorderStyle.None;
        txtCedula.Font = new Font("Segoe UI", 10.5F);
        txtCedula.ForeColor = Color.FromArgb(20, 43, 78);
        txtCedula.Location = new Point(15, 15);
        txtCedula.Margin = new Padding(3, 4, 3, 4);
        txtCedula.Name = "txtCedula";
        txtCedula.PlaceholderText = "Número de identificación";
        txtCedula.Size = new Size(377, 24);
        txtCedula.TabIndex = 0;
        // 
        // lblTelefono
        // 
        lblTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblTelefono.AutoEllipsis = true;
        lblTelefono.BackColor = Color.Transparent;
        lblTelefono.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblTelefono.ForeColor = Color.FromArgb(20, 43, 78);
        lblTelefono.Location = new Point(0, 400);
        lblTelefono.Name = "lblTelefono";
        lblTelefono.Size = new Size(407, 31);
        lblTelefono.TabIndex = 5;
        lblTelefono.Text = "Teléfono (opcional)";
        // 
        // panelTelefono
        // 
        panelTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelTelefono.BackColor = Color.White;
        panelTelefono.ColorBorde = Color.FromArgb(211, 221, 232);
        panelTelefono.Controls.Add(txtTelefono);
        panelTelefono.Location = new Point(0, 437);
        panelTelefono.Margin = new Padding(3, 4, 3, 4);
        panelTelefono.Name = "panelTelefono";
        panelTelefono.RadioBorde = 8;
        panelTelefono.Size = new Size(407, 56);
        panelTelefono.TabIndex = 3;
        // 
        // txtTelefono
        // 
        txtTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTelefono.BorderStyle = BorderStyle.None;
        txtTelefono.Font = new Font("Segoe UI", 10.5F);
        txtTelefono.ForeColor = Color.FromArgb(20, 43, 78);
        txtTelefono.Location = new Point(15, 15);
        txtTelefono.Margin = new Padding(3, 4, 3, 4);
        txtTelefono.Name = "txtTelefono";
        txtTelefono.PlaceholderText = "Número de contacto";
        txtTelefono.Size = new Size(377, 24);
        txtTelefono.TabIndex = 0;
        // 
        // lblCorreo
        // 
        lblCorreo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblCorreo.AutoEllipsis = true;
        lblCorreo.BackColor = Color.Transparent;
        lblCorreo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblCorreo.ForeColor = Color.FromArgb(20, 43, 78);
        lblCorreo.Location = new Point(0, 509);
        lblCorreo.Name = "lblCorreo";
        lblCorreo.Size = new Size(407, 31);
        lblCorreo.TabIndex = 6;
        lblCorreo.Text = "Correo electrónico (opcional)";
        // 
        // panelCorreo
        // 
        panelCorreo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelCorreo.BackColor = Color.White;
        panelCorreo.ColorBorde = Color.FromArgb(211, 221, 232);
        panelCorreo.Controls.Add(txtCorreo);
        panelCorreo.Location = new Point(0, 547);
        panelCorreo.Margin = new Padding(3, 4, 3, 4);
        panelCorreo.Name = "panelCorreo";
        panelCorreo.RadioBorde = 8;
        panelCorreo.Size = new Size(407, 56);
        panelCorreo.TabIndex = 4;
        // 
        // txtCorreo
        // 
        txtCorreo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtCorreo.BorderStyle = BorderStyle.None;
        txtCorreo.Font = new Font("Segoe UI", 10.5F);
        txtCorreo.ForeColor = Color.FromArgb(20, 43, 78);
        txtCorreo.Location = new Point(15, 15);
        txtCorreo.Margin = new Padding(3, 4, 3, 4);
        txtCorreo.Name = "txtCorreo";
        txtCorreo.PlaceholderText = "nombre@correo.com";
        txtCorreo.Size = new Size(377, 24);
        txtCorreo.TabIndex = 0;
        // 
        // panelAcceso
        // 
        panelAcceso.Controls.Add(lblAcceso);
        panelAcceso.Controls.Add(lineaAcceso);
        panelAcceso.Controls.Add(lblUsuario);
        panelAcceso.Controls.Add(panelUsuario);
        panelAcceso.Controls.Add(lblRol);
        panelAcceso.Controls.Add(panelRol);
        panelAcceso.Controls.Add(lblContrasena);
        panelAcceso.Controls.Add(panelContrasena);
        panelAcceso.Controls.Add(lblConfirmacion);
        panelAcceso.Controls.Add(panelConfirmacion);
        panelAcceso.Controls.Add(lblEstado);
        panelAcceso.Controls.Add(panelEstado);
        panelAcceso.Dock = DockStyle.Fill;
        panelAcceso.Location = new Point(435, 0);
        panelAcceso.Margin = new Padding(14, 0, 0, 0);
        panelAcceso.Name = "panelAcceso";
        panelAcceso.Size = new Size(407, 653);
        panelAcceso.TabIndex = 1;
        // 
        // lblAcceso
        // 
        lblAcceso.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblAcceso.AutoEllipsis = true;
        lblAcceso.BackColor = Color.Transparent;
        lblAcceso.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        lblAcceso.ForeColor = Color.FromArgb(20, 43, 78);
        lblAcceso.Location = new Point(0, 0);
        lblAcceso.Name = "lblAcceso";
        lblAcceso.Size = new Size(407, 37);
        lblAcceso.TabIndex = 0;
        lblAcceso.Text = "02   Acceso al sistema";
        // 
        // lineaAcceso
        // 
        lineaAcceso.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lineaAcceso.BackColor = Color.FromArgb(205, 216, 230);
        lineaAcceso.Location = new Point(0, 49);
        lineaAcceso.Margin = new Padding(3, 4, 3, 4);
        lineaAcceso.Name = "lineaAcceso";
        lineaAcceso.Size = new Size(407, 1);
        lineaAcceso.TabIndex = 1;
        // 
        // lblUsuario
        // 
        lblUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblUsuario.AutoEllipsis = true;
        lblUsuario.BackColor = Color.Transparent;
        lblUsuario.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblUsuario.ForeColor = Color.FromArgb(20, 43, 78);
        lblUsuario.Location = new Point(0, 72);
        lblUsuario.Name = "lblUsuario";
        lblUsuario.Size = new Size(407, 31);
        lblUsuario.TabIndex = 2;
        lblUsuario.Text = "Nombre de usuario *";
        // 
        // panelUsuario
        // 
        panelUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelUsuario.BackColor = Color.White;
        panelUsuario.ColorBorde = Color.FromArgb(211, 221, 232);
        panelUsuario.Controls.Add(txtUsuario);
        panelUsuario.Location = new Point(0, 109);
        panelUsuario.Margin = new Padding(3, 4, 3, 4);
        panelUsuario.Name = "panelUsuario";
        panelUsuario.RadioBorde = 8;
        panelUsuario.Size = new Size(407, 56);
        panelUsuario.TabIndex = 0;
        // 
        // txtUsuario
        // 
        txtUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtUsuario.BorderStyle = BorderStyle.None;
        txtUsuario.Font = new Font("Segoe UI", 10.5F);
        txtUsuario.ForeColor = Color.FromArgb(20, 43, 78);
        txtUsuario.Location = new Point(15, 15);
        txtUsuario.Margin = new Padding(3, 4, 3, 4);
        txtUsuario.Name = "txtUsuario";
        txtUsuario.PlaceholderText = "Ingrese el usuario de acceso";
        txtUsuario.Size = new Size(377, 24);
        txtUsuario.TabIndex = 0;
        // 
        // lblRol
        // 
        lblRol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblRol.AutoEllipsis = true;
        lblRol.BackColor = Color.Transparent;
        lblRol.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblRol.ForeColor = Color.FromArgb(20, 43, 78);
        lblRol.Location = new Point(0, 181);
        lblRol.Name = "lblRol";
        lblRol.Size = new Size(407, 31);
        lblRol.TabIndex = 3;
        lblRol.Text = "Rol *";
        // 
        // panelRol
        // 
        panelRol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelRol.BackColor = Color.White;
        panelRol.ColorBorde = Color.FromArgb(211, 221, 232);
        panelRol.Controls.Add(cboRol);
        panelRol.Location = new Point(0, 219);
        panelRol.Margin = new Padding(3, 4, 3, 4);
        panelRol.Name = "panelRol";
        panelRol.RadioBorde = 8;
        panelRol.Size = new Size(407, 56);
        panelRol.TabIndex = 1;
        // 
        // cboRol
        // 
        cboRol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        cboRol.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRol.FlatStyle = FlatStyle.Flat;
        cboRol.Font = new Font("Segoe UI", 10F);
        cboRol.ForeColor = Color.FromArgb(20, 43, 78);
        cboRol.Items.AddRange(new object[] { "Seleccione un rol", "Administrador", "Recepcionista", "Técnico" });
        cboRol.Location = new Point(11, 9);
        cboRol.Margin = new Padding(3, 4, 3, 4);
        cboRol.Name = "cboRol";
        cboRol.Size = new Size(383, 31);
        cboRol.TabIndex = 0;
        // 
        // lblContrasena
        // 
        lblContrasena.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblContrasena.AutoEllipsis = true;
        lblContrasena.BackColor = Color.Transparent;
        lblContrasena.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblContrasena.ForeColor = Color.FromArgb(20, 43, 78);
        lblContrasena.Location = new Point(0, 291);
        lblContrasena.Name = "lblContrasena";
        lblContrasena.Size = new Size(407, 31);
        lblContrasena.TabIndex = 4;
        lblContrasena.Text = "Contraseña *";
        // 
        // panelContrasena
        // 
        panelContrasena.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelContrasena.BackColor = Color.White;
        panelContrasena.ColorBorde = Color.FromArgb(211, 221, 232);
        panelContrasena.Controls.Add(txtContrasena);
        panelContrasena.Location = new Point(0, 328);
        panelContrasena.Margin = new Padding(3, 4, 3, 4);
        panelContrasena.Name = "panelContrasena";
        panelContrasena.RadioBorde = 8;
        panelContrasena.Size = new Size(407, 56);
        panelContrasena.TabIndex = 2;
        // 
        // txtContrasena
        // 
        txtContrasena.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtContrasena.BorderStyle = BorderStyle.None;
        txtContrasena.Font = new Font("Segoe UI", 10.5F);
        txtContrasena.ForeColor = Color.FromArgb(20, 43, 78);
        txtContrasena.Location = new Point(15, 15);
        txtContrasena.Margin = new Padding(3, 4, 3, 4);
        txtContrasena.Name = "txtContrasena";
        txtContrasena.PlaceholderText = "Ingrese una contraseña";
        txtContrasena.Size = new Size(377, 24);
        txtContrasena.TabIndex = 0;
        txtContrasena.UseSystemPasswordChar = true;
        // 
        // lblConfirmacion
        // 
        lblConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblConfirmacion.AutoEllipsis = true;
        lblConfirmacion.BackColor = Color.Transparent;
        lblConfirmacion.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblConfirmacion.ForeColor = Color.FromArgb(20, 43, 78);
        lblConfirmacion.Location = new Point(0, 400);
        lblConfirmacion.Name = "lblConfirmacion";
        lblConfirmacion.Size = new Size(407, 31);
        lblConfirmacion.TabIndex = 5;
        lblConfirmacion.Text = "Confirmar contraseña *";
        // 
        // panelConfirmacion
        // 
        panelConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelConfirmacion.BackColor = Color.White;
        panelConfirmacion.ColorBorde = Color.FromArgb(211, 221, 232);
        panelConfirmacion.Controls.Add(txtConfirmacion);
        panelConfirmacion.Location = new Point(0, 437);
        panelConfirmacion.Margin = new Padding(3, 4, 3, 4);
        panelConfirmacion.Name = "panelConfirmacion";
        panelConfirmacion.RadioBorde = 8;
        panelConfirmacion.Size = new Size(407, 56);
        panelConfirmacion.TabIndex = 3;
        // 
        // txtConfirmacion
        // 
        txtConfirmacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtConfirmacion.BorderStyle = BorderStyle.None;
        txtConfirmacion.Font = new Font("Segoe UI", 10.5F);
        txtConfirmacion.ForeColor = Color.FromArgb(20, 43, 78);
        txtConfirmacion.Location = new Point(15, 15);
        txtConfirmacion.Margin = new Padding(3, 4, 3, 4);
        txtConfirmacion.Name = "txtConfirmacion";
        txtConfirmacion.PlaceholderText = "Repita la contraseña";
        txtConfirmacion.Size = new Size(377, 24);
        txtConfirmacion.TabIndex = 0;
        txtConfirmacion.UseSystemPasswordChar = true;
        // 
        // lblEstado
        // 
        lblEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblEstado.AutoEllipsis = true;
        lblEstado.BackColor = Color.Transparent;
        lblEstado.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        lblEstado.ForeColor = Color.FromArgb(20, 43, 78);
        lblEstado.Location = new Point(0, 509);
        lblEstado.Name = "lblEstado";
        lblEstado.Size = new Size(407, 31);
        lblEstado.TabIndex = 6;
        lblEstado.Text = "Estado de la cuenta";
        // 
        // panelEstado
        // 
        panelEstado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelEstado.BackColor = Color.White;
        panelEstado.ColorBorde = Color.FromArgb(211, 221, 232);
        panelEstado.Controls.Add(chkActivo);
        panelEstado.Controls.Add(lblEstadoDetalle);
        panelEstado.Location = new Point(0, 547);
        panelEstado.Margin = new Padding(3, 4, 3, 4);
        panelEstado.Name = "panelEstado";
        panelEstado.RadioBorde = 8;
        panelEstado.Size = new Size(407, 85);
        panelEstado.TabIndex = 4;
        // 
        // chkActivo
        // 
        chkActivo.Checked = true;
        chkActivo.CheckState = CheckState.Checked;
        chkActivo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        chkActivo.ForeColor = Color.FromArgb(20, 43, 78);
        chkActivo.Location = new Point(15, 12);
        chkActivo.Margin = new Padding(3, 4, 3, 4);
        chkActivo.Name = "chkActivo";
        chkActivo.Size = new Size(377, 32);
        chkActivo.TabIndex = 0;
        chkActivo.Text = "Usuario activo";
        chkActivo.UseVisualStyleBackColor = true;
        // 
        // lblEstadoDetalle
        // 
        lblEstadoDetalle.AutoEllipsis = true;
        lblEstadoDetalle.BackColor = Color.Transparent;
        lblEstadoDetalle.Font = new Font("Segoe UI", 8.5F);
        lblEstadoDetalle.ForeColor = Color.FromArgb(103, 116, 137);
        lblEstadoDetalle.Location = new Point(34, 47);
        lblEstadoDetalle.Name = "lblEstadoDetalle";
        lblEstadoDetalle.Size = new Size(366, 27);
        lblEstadoDetalle.TabIndex = 1;
        lblEstadoDetalle.Text = "Habilita el acceso al sistema.";
        // 
        // FrmRegistroUsuario
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(230, 236, 243);
        ClientSize = new Size(937, 660);
        Margin = new Padding(3, 5, 3, 5);
        Name = "FrmRegistroUsuario";
        Text = "Al Día · Usuarios";
        ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
        panelContenido.ResumeLayout(false);
        panelCabecera.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)iconModulo).EndInit();
        panelCuerpo.ResumeLayout(false);
        panelPie.ResumeLayout(false);
        tablaSecciones.ResumeLayout(false);
        panelDatos.ResumeLayout(false);
        panelNombre.ResumeLayout(false);
        panelNombre.PerformLayout();
        panelApellido.ResumeLayout(false);
        panelApellido.PerformLayout();
        panelCedula.ResumeLayout(false);
        panelCedula.PerformLayout();
        panelTelefono.ResumeLayout(false);
        panelTelefono.PerformLayout();
        panelCorreo.ResumeLayout(false);
        panelCorreo.PerformLayout();
        panelAcceso.ResumeLayout(false);
        panelUsuario.ResumeLayout(false);
        panelUsuario.PerformLayout();
        panelRol.ResumeLayout(false);
        panelContrasena.ResumeLayout(false);
        panelContrasena.PerformLayout();
        panelConfirmacion.ResumeLayout(false);
        panelConfirmacion.PerformLayout();
        panelEstado.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tablaSecciones;
    private Panel panelDatos;
    private Label lblDatos;
    private Panel lineaDatos;
    private Panel panelAcceso;
    private Label lblAcceso;
    private Panel lineaAcceso;
    private Label lblNombre;
    private AlDia.UI.Controles.PanelTarjeta panelNombre;
    private TextBox txtNombre;
    private Label lblApellido;
    private AlDia.UI.Controles.PanelTarjeta panelApellido;
    private TextBox txtApellido;
    private Label lblCedula;
    private AlDia.UI.Controles.PanelTarjeta panelCedula;
    private TextBox txtCedula;
    private Label lblTelefono;
    private AlDia.UI.Controles.PanelTarjeta panelTelefono;
    private TextBox txtTelefono;
    private Label lblCorreo;
    private AlDia.UI.Controles.PanelTarjeta panelCorreo;
    private TextBox txtCorreo;
    private Label lblUsuario;
    private AlDia.UI.Controles.PanelTarjeta panelUsuario;
    private TextBox txtUsuario;
    private Label lblRol;
    private AlDia.UI.Controles.PanelTarjeta panelRol;
    private ComboBox cboRol;
    private Label lblContrasena;
    private AlDia.UI.Controles.PanelTarjeta panelContrasena;
    private TextBox txtContrasena;
    private Label lblConfirmacion;
    private AlDia.UI.Controles.PanelTarjeta panelConfirmacion;
    private TextBox txtConfirmacion;
    private Label lblEstado;
    private AlDia.UI.Controles.PanelTarjeta panelEstado;
    private CheckBox chkActivo;
    private Label lblEstadoDetalle;
}
