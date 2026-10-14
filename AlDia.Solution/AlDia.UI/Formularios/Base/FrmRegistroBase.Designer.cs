namespace AlDia.UI.Formularios.Base;

partial class FrmRegistroBase
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _vida.Cancel();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        panelContenido = new Panel();
        panelCuerpo = new Panel();
        panelPie = new Panel();
        lineaPie = new Panel();
        lblObligatorios = new Label();
        btnCancelar = new FontAwesome.Sharp.IconButton();
        btnGuardar = new FontAwesome.Sharp.IconButton();
        panelCabecera = new Panel();
        iconModulo = new FontAwesome.Sharp.IconPictureBox();
        lblTitulo = new Label();
        lblDescripcion = new Label();
        btnCerrar = new FontAwesome.Sharp.IconButton();
        panelContenido.SuspendLayout();
        panelPie.SuspendLayout();
        panelCabecera.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)iconModulo).BeginInit();
        SuspendLayout();
        // 
        // panelContenido
        // 
        panelContenido.BackColor = Color.FromArgb(230, 236, 243);
        panelContenido.Controls.Add(panelCuerpo);
        panelContenido.Controls.Add(panelPie);
        panelContenido.Controls.Add(panelCabecera);
        panelContenido.Dock = DockStyle.Fill;
        panelContenido.Location = new Point(0, 0);
        panelContenido.Margin = new Padding(3, 4, 3, 4);
        panelContenido.Name = "panelContenido";
        panelContenido.Size = new Size(937, 971);
        panelContenido.TabIndex = 0;
        // 
        // panelCuerpo
        // 
        panelCuerpo.AutoScroll = true;
        panelCuerpo.Dock = DockStyle.Fill;
        panelCuerpo.Location = new Point(0, 139);
        panelCuerpo.Margin = new Padding(3, 4, 3, 4);
        panelCuerpo.Name = "panelCuerpo";
        panelCuerpo.Padding = new Padding(32, 32, 32, 27);
        panelCuerpo.Size = new Size(937, 725);
        panelCuerpo.TabIndex = 1;
        // 
        // panelPie
        // 
        panelPie.BackColor = Color.White;
        panelPie.Controls.Add(lineaPie);
        panelPie.Controls.Add(lblObligatorios);
        panelPie.Controls.Add(btnCancelar);
        panelPie.Controls.Add(btnGuardar);
        panelPie.Dock = DockStyle.Bottom;
        panelPie.Location = new Point(0, 864);
        panelPie.Margin = new Padding(3, 4, 3, 4);
        panelPie.Name = "panelPie";
        panelPie.Size = new Size(937, 107);
        panelPie.TabIndex = 2;
        // 
        // lineaPie
        // 
        lineaPie.BackColor = Color.FromArgb(211, 221, 232);
        lineaPie.Dock = DockStyle.Top;
        lineaPie.Location = new Point(0, 0);
        lineaPie.Margin = new Padding(3, 4, 3, 4);
        lineaPie.Name = "lineaPie";
        lineaPie.Size = new Size(937, 1);
        lineaPie.TabIndex = 0;
        // 
        // lblObligatorios
        // 
        lblObligatorios.AutoEllipsis = true;
        lblObligatorios.BackColor = Color.Transparent;
        lblObligatorios.Font = new Font("Segoe UI", 9F);
        lblObligatorios.ForeColor = Color.FromArgb(103, 116, 137);
        lblObligatorios.Location = new Point(32, 37);
        lblObligatorios.Name = "lblObligatorios";
        lblObligatorios.Size = new Size(343, 32);
        lblObligatorios.TabIndex = 1;
        lblObligatorios.Text = "* Campos obligatorios";
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCancelar.BackColor = Color.White;
        btnCancelar.Cursor = Cursors.Hand;
        btnCancelar.DialogResult = DialogResult.Cancel;
        btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnCancelar.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 236, 243);
        btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCancelar.FlatStyle = FlatStyle.Flat;
        btnCancelar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        btnCancelar.ForeColor = Color.FromArgb(20, 43, 78);
        btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Close;
        btnCancelar.IconColor = Color.FromArgb(103, 116, 137);
        btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnCancelar.IconSize = 18;
        btnCancelar.Location = new Point(567, 27);
        btnCancelar.Margin = new Padding(3, 4, 3, 4);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(151, 56);
        btnCancelar.TabIndex = 0;
        btnCancelar.Text = "Cancelar";
        btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnCancelar.UseVisualStyleBackColor = false;
        // 
        // btnGuardar
        // 
        btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnGuardar.BackColor = Color.FromArgb(37, 99, 235);
        btnGuardar.Cursor = Cursors.Hand;
        btnGuardar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnGuardar.FlatAppearance.BorderSize = 0;
        btnGuardar.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);
        btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        btnGuardar.ForeColor = Color.White;
        btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
        btnGuardar.IconColor = Color.White;
        btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnGuardar.IconSize = 18;
        btnGuardar.Location = new Point(731, 27);
        btnGuardar.Margin = new Padding(3, 4, 3, 4);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(174, 56);
        btnGuardar.TabIndex = 1;
        btnGuardar.Text = "Guardar registro";
        btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnGuardar.UseVisualStyleBackColor = false;
        // 
        // panelCabecera
        // 
        panelCabecera.BackColor = Color.White;
        panelCabecera.Controls.Add(iconModulo);
        panelCabecera.Controls.Add(lblTitulo);
        panelCabecera.Controls.Add(lblDescripcion);
        panelCabecera.Controls.Add(btnCerrar);
        panelCabecera.Dock = DockStyle.Top;
        panelCabecera.Location = new Point(0, 0);
        panelCabecera.Margin = new Padding(3, 4, 3, 4);
        panelCabecera.Name = "panelCabecera";
        panelCabecera.Size = new Size(937, 139);
        panelCabecera.TabIndex = 0;
        // 
        // iconModulo
        // 
        iconModulo.BackColor = Color.White;
        iconModulo.ForeColor = Color.FromArgb(37, 99, 235);
        iconModulo.IconChar = FontAwesome.Sharp.IconChar.Add;
        iconModulo.IconColor = Color.FromArgb(37, 99, 235);
        iconModulo.IconFont = FontAwesome.Sharp.IconFont.Auto;
        iconModulo.IconSize = 63;
        iconModulo.Location = new Point(32, 32);
        iconModulo.Margin = new Padding(3, 4, 3, 4);
        iconModulo.Name = "iconModulo";
        iconModulo.Size = new Size(63, 65);
        iconModulo.TabIndex = 0;
        iconModulo.TabStop = false;
        // 
        // lblTitulo
        // 
        lblTitulo.AutoEllipsis = true;
        lblTitulo.BackColor = Color.Transparent;
        lblTitulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
        lblTitulo.ForeColor = Color.FromArgb(20, 43, 78);
        lblTitulo.Location = new Point(118, 24);
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Size = new Size(651, 53);
        lblTitulo.TabIndex = 1;
        lblTitulo.Text = "Nuevo registro";
        // 
        // lblDescripcion
        // 
        lblDescripcion.AutoEllipsis = true;
        lblDescripcion.BackColor = Color.Transparent;
        lblDescripcion.Font = new Font("Segoe UI", 10F);
        lblDescripcion.ForeColor = Color.FromArgb(103, 116, 137);
        lblDescripcion.Location = new Point(120, 81);
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Size = new Size(709, 35);
        lblDescripcion.TabIndex = 2;
        lblDescripcion.Text = "Complete la información del registro.";
        // 
        // btnCerrar
        // 
        btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCerrar.BackColor = Color.White;
        btnCerrar.Cursor = Cursors.Hand;
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.FlatAppearance.BorderSize = 0;
        btnCerrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCerrar.FlatStyle = FlatStyle.Flat;
        btnCerrar.IconChar = FontAwesome.Sharp.IconChar.Close;
        btnCerrar.IconColor = Color.FromArgb(103, 116, 137);
        btnCerrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnCerrar.IconSize = 20;
        btnCerrar.Location = new Point(859, 29);
        btnCerrar.Margin = new Padding(3, 4, 3, 4);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(41, 48);
        btnCerrar.TabIndex = 3;
        btnCerrar.TabStop = false;
        btnCerrar.UseVisualStyleBackColor = false;
        // 
        // FrmRegistroBase
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(230, 236, 243);
        CancelButton = btnCancelar;
        ClientSize = new Size(937, 971);
        Controls.Add(panelContenido);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.None;
        Margin = new Padding(3, 4, 3, 4);
        Name = "FrmRegistroBase";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Al Día · Registro";
        panelContenido.ResumeLayout(false);
        panelPie.ResumeLayout(false);
        panelCabecera.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)iconModulo).EndInit();
        ResumeLayout(false);
    }

    #endregion

    protected Panel panelContenido;
    protected Panel panelCabecera;
    protected FontAwesome.Sharp.IconPictureBox iconModulo;
    protected Label lblTitulo;
    protected Label lblDescripcion;
    protected FontAwesome.Sharp.IconButton btnCerrar;
    protected Panel panelCuerpo;
    protected Panel panelPie;
    protected Panel lineaPie;
    protected Label lblObligatorios;
    protected FontAwesome.Sharp.IconButton btnCancelar;
    protected FontAwesome.Sharp.IconButton btnGuardar;
}
