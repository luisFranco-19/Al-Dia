namespace AlDia.UI.Formularios.Seguridad
{
    partial class FrmConfiguracionConexion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguracionConexion));
            panelContenido = new Panel();
            panel3 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnProbar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            panel2 = new Panel();
            pictureCerrar = new PictureBox();
            label5 = new Label();
            lblTitulo = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            panelContra = new Panel();
            txtClave = new TextBox();
            label4 = new Label();
            panelUser = new Panel();
            txtUsuarioSql = new TextBox();
            label3 = new Label();
            panelBD = new Panel();
            txtBaseDatos = new TextBox();
            label2 = new Label();
            panelServidor = new Panel();
            txtServidor = new TextBox();
            label1 = new Label();
            errorIcon = new ErrorProvider(components);
            panelContenido.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureCerrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            panelContra.SuspendLayout();
            panelUser.SuspendLayout();
            panelBD.SuspendLayout();
            panelServidor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
            SuspendLayout();
            // 
            // panelContenido
            // 
            panelContenido.BackColor = Color.FromArgb(230, 236, 243);
            panelContenido.Controls.Add(panel3);
            panelContenido.Controls.Add(panel2);
            panelContenido.Controls.Add(panelContra);
            panelContenido.Controls.Add(label4);
            panelContenido.Controls.Add(panelUser);
            panelContenido.Controls.Add(label3);
            panelContenido.Controls.Add(panelBD);
            panelContenido.Controls.Add(label2);
            panelContenido.Controls.Add(panelServidor);
            panelContenido.Controls.Add(label1);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panelContenido.Location = new Point(0, 0);
            panelContenido.Margin = new Padding(3, 4, 3, 4);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(550, 525);
            panelContenido.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.BackColor = Color.White;
            panel3.Controls.Add(tableLayoutPanel1);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 432);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(550, 93);
            panel3.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 154F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
            tableLayoutPanel1.Controls.Add(btnCancelar, 3, 0);
            tableLayoutPanel1.Controls.Add(btnProbar, 1, 0);
            tableLayoutPanel1.Controls.Add(btnGuardar, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(550, 93);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCancelar
            // 
            btnCancelar.Dock = DockStyle.Top;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnCancelar.IconColor = Color.FromArgb(220, 53, 69);
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 28;
            btnCancelar.Location = new Point(391, 4);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(148, 67);
            btnCancelar.TabIndex = 3;
            btnCancelar.Text = "Canelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnProbar
            // 
            btnProbar.Cursor = Cursors.Hand;
            btnProbar.Dock = DockStyle.Top;
            btnProbar.IconChar = FontAwesome.Sharp.IconChar.Server;
            btnProbar.IconColor = Color.FromArgb(37, 99, 235);
            btnProbar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnProbar.IconSize = 24;
            btnProbar.Location = new Point(11, 4);
            btnProbar.Margin = new Padding(3, 4, 3, 4);
            btnProbar.Name = "btnProbar";
            btnProbar.Size = new Size(204, 65);
            btnProbar.TabIndex = 0;
            btnProbar.Text = "Probar conexión";
            btnProbar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnProbar.UseVisualStyleBackColor = true;
            btnProbar.Click += btnProbar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Dock = DockStyle.Top;
            btnGuardar.Enabled = false;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.FromArgb(37, 99, 235);
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 24;
            btnGuardar.Location = new Point(221, 4);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(164, 67);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(pictureCerrar);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(lblTitulo);
            panel2.Controls.Add(iconPictureBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(550, 115);
            panel2.TabIndex = 8;
            // 
            // pictureCerrar
            // 
            pictureCerrar.Image = (Image)resources.GetObject("pictureCerrar.Image");
            pictureCerrar.Location = new Point(502, 19);
            pictureCerrar.Margin = new Padding(3, 4, 3, 4);
            pictureCerrar.Name = "pictureCerrar";
            pictureCerrar.Size = new Size(24, 24);
            pictureCerrar.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureCerrar.TabIndex = 11;
            pictureCerrar.TabStop = false;
            pictureCerrar.Click += pictureCerrar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.DarkGray;
            label5.Location = new Point(99, 56);
            label5.Name = "label5";
            label5.Size = new Size(331, 25);
            label5.TabIndex = 10;
            label5.Text = "Complete la información del registro.";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(99, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(302, 41);
            lblTitulo.TabIndex = 9;
            lblTitulo.Text = "Conexión al Servidor";
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.White;
            iconPictureBox1.ForeColor = Color.FromArgb(37, 99, 235);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Server;
            iconPictureBox1.IconColor = Color.FromArgb(37, 99, 235);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 55;
            iconPictureBox1.Location = new Point(12, 19);
            iconPictureBox1.Margin = new Padding(3, 4, 3, 4);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(55, 55);
            iconPictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            iconPictureBox1.TabIndex = 0;
            iconPictureBox1.TabStop = false;
            // 
            // panelContra
            // 
            panelContra.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelContra.BackColor = Color.White;
            panelContra.Controls.Add(txtClave);
            panelContra.Location = new Point(159, 352);
            panelContra.Margin = new Padding(3, 4, 3, 4);
            panelContra.Name = "panelContra";
            panelContra.Padding = new Padding(5, 0, 11, 0);
            panelContra.Size = new Size(365, 53);
            panelContra.TabIndex = 7;
            // 
            // txtClave
            // 
            txtClave.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtClave.BorderStyle = BorderStyle.None;
            txtClave.Location = new Point(13, 13);
            txtClave.Margin = new Padding(3, 4, 3, 4);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '*';
            txtClave.Size = new Size(328, 25);
            txtClave.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 352);
            label4.Name = "label4";
            label4.Size = new Size(109, 25);
            label4.TabIndex = 6;
            label4.Text = "Contraseña";
            // 
            // panelUser
            // 
            panelUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelUser.BackColor = Color.White;
            panelUser.Controls.Add(txtUsuarioSql);
            panelUser.Location = new Point(159, 283);
            panelUser.Margin = new Padding(3, 4, 3, 4);
            panelUser.Name = "panelUser";
            panelUser.Padding = new Padding(5, 0, 11, 0);
            panelUser.Size = new Size(365, 53);
            panelUser.TabIndex = 5;
            // 
            // txtUsuarioSql
            // 
            txtUsuarioSql.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsuarioSql.BorderStyle = BorderStyle.None;
            txtUsuarioSql.Location = new Point(13, 13);
            txtUsuarioSql.Margin = new Padding(3, 4, 3, 4);
            txtUsuarioSql.Name = "txtUsuarioSql";
            txtUsuarioSql.Size = new Size(328, 25);
            txtUsuarioSql.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 283);
            label3.Name = "label3";
            label3.Size = new Size(108, 25);
            label3.TabIndex = 4;
            label3.Text = "Usuario Sql";
            // 
            // panelBD
            // 
            panelBD.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelBD.BackColor = Color.White;
            panelBD.Controls.Add(txtBaseDatos);
            panelBD.Location = new Point(159, 215);
            panelBD.Margin = new Padding(3, 4, 3, 4);
            panelBD.Name = "panelBD";
            panelBD.Padding = new Padding(5, 0, 11, 0);
            panelBD.Size = new Size(365, 53);
            panelBD.TabIndex = 3;
            // 
            // txtBaseDatos
            // 
            txtBaseDatos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBaseDatos.BorderStyle = BorderStyle.None;
            txtBaseDatos.Location = new Point(13, 13);
            txtBaseDatos.Margin = new Padding(3, 4, 3, 4);
            txtBaseDatos.Name = "txtBaseDatos";
            txtBaseDatos.Size = new Size(328, 25);
            txtBaseDatos.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 215);
            label2.Name = "label2";
            label2.Size = new Size(132, 25);
            label2.TabIndex = 2;
            label2.Text = "Base de Datos";
            // 
            // panelServidor
            // 
            panelServidor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelServidor.BackColor = Color.White;
            panelServidor.Controls.Add(txtServidor);
            panelServidor.Location = new Point(159, 149);
            panelServidor.Margin = new Padding(3, 4, 3, 4);
            panelServidor.Name = "panelServidor";
            panelServidor.Padding = new Padding(5, 0, 11, 0);
            panelServidor.Size = new Size(365, 53);
            panelServidor.TabIndex = 1;
            // 
            // txtServidor
            // 
            txtServidor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtServidor.BorderStyle = BorderStyle.None;
            txtServidor.Location = new Point(8, 13);
            txtServidor.Margin = new Padding(3, 4, 3, 4);
            txtServidor.Name = "txtServidor";
            txtServidor.Size = new Size(333, 25);
            txtServidor.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 149);
            label1.Name = "label1";
            label1.Size = new Size(84, 25);
            label1.TabIndex = 0;
            label1.Text = "Servidor";
            // 
            // errorIcon
            // 
            errorIcon.ContainerControl = this;
            // 
            // FrmConfiguracionConexion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(550, 525);
            Controls.Add(panelContenido);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmConfiguracionConexion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmConfiguracionConexion";
            Shown += FrmConfiguracionConexion_Shown;
            panelContenido.ResumeLayout(false);
            panelContenido.PerformLayout();
            panel3.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureCerrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            panelContra.ResumeLayout(false);
            panelContra.PerformLayout();
            panelUser.ResumeLayout(false);
            panelUser.PerformLayout();
            panelBD.ResumeLayout(false);
            panelBD.PerformLayout();
            panelServidor.ResumeLayout(false);
            panelServidor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelContenido;
        private Panel panelServidor;
        private TextBox txtServidor;
        private Label label1;
        private Panel panelContra;
        private TextBox txtClave;
        private Label label4;
        private Panel panelUser;
        private TextBox txtUsuarioSql;
        private Label label3;
        private Panel panelBD;
        private TextBox txtBaseDatos;
        private Label label2;
        private Panel panel2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Label lblTitulo;
        private PictureBox pictureCerrar;
        private Label label5;
        private Panel panel3;
        private ErrorProvider errorIcon;
        private TableLayoutPanel tableLayoutPanel1;
        private FontAwesome.Sharp.IconButton btnProbar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
    }
}