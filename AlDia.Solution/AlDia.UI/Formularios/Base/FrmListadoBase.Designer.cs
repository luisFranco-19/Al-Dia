namespace AlDia.UI.Formularios.Base;

partial class FrmListadoBase
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            CancelarOperaciones();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        panelContenido = new Panel();
        tablaModulo = new TableLayoutPanel();
        panelIntroduccion = new Panel();
        lblTitulo = new Label();
        lblDescripcion = new Label();
        btnNuevo = new FontAwesome.Sharp.IconButton();
        tarjetaFiltros = new AlDia.UI.Controles.PanelTarjeta();
        tablaFiltros = new TableLayoutPanel();
        panelBuscar = new Panel();
        panelFiltrosModulo = new Panel();
        panelAccionBuscar = new Panel();
        lblBuscar = new Label();
        bordeBuscar = new AlDia.UI.Controles.PanelTarjeta();
        iconBuscar = new FontAwesome.Sharp.IconPictureBox();
        txtBuscar = new TextBox();
        btnBuscar = new FontAwesome.Sharp.IconButton();
        tarjetaListado = new AlDia.UI.Controles.PanelTarjeta();
        tablaListado = new TableLayoutPanel();
        panelTituloListado = new Panel();
        lblListado = new Label();
        btnEditar = new FontAwesome.Sharp.IconButton();
        btnCambiarEstado = new FontAwesome.Sharp.IconButton();
        panelGrilla = new Panel();
        panelVacio = new Panel();
        tablaVacio = new TableLayoutPanel();
        iconSinRegistros = new FontAwesome.Sharp.IconPictureBox();
        lblSinRegistros = new Label();
        lblSinRegistrosDetalle = new Label();
        dgvListado = new DataGridView();
        DataGridViewCellStyle estiloCabecera = new DataGridViewCellStyle();
        DataGridViewCellStyle estiloFila = new DataGridViewCellStyle();
        DataGridViewCellStyle estiloAlterno = new DataGridViewCellStyle();
        panelPaginacion = new Panel();
        lineaPaginacion = new Panel();
        lblSeleccion = new Label();
        btnAnterior = new FontAwesome.Sharp.IconButton();
        lblPagina = new Label();
        btnSiguiente = new FontAwesome.Sharp.IconButton();
        lblPie = new Label();
        panelContenido.SuspendLayout();
        tablaModulo.SuspendLayout();
        panelIntroduccion.SuspendLayout();
        tarjetaFiltros.SuspendLayout();
        tablaFiltros.SuspendLayout();
        panelBuscar.SuspendLayout();
        panelFiltrosModulo.SuspendLayout();
        panelAccionBuscar.SuspendLayout();
        bordeBuscar.SuspendLayout();
        tarjetaListado.SuspendLayout();
        tablaListado.SuspendLayout();
        panelTituloListado.SuspendLayout();
        panelGrilla.SuspendLayout();
        panelVacio.SuspendLayout();
        tablaVacio.SuspendLayout();
        panelPaginacion.SuspendLayout();
        lineaPaginacion.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)iconBuscar).BeginInit();
        ((System.ComponentModel.ISupportInitialize)iconSinRegistros).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvListado).BeginInit();
        SuspendLayout();
        // panelContenido
        panelContenido.Name = "panelContenido";
        panelContenido.Dock = DockStyle.Fill;
        panelContenido.BackColor = Color.FromArgb(244, 246, 250);
        panelContenido.Location = new Point(0, 0);
        panelContenido.Size = new Size(1068, 700);
        panelContenido.TabIndex = 0;
        // tablaModulo
        tablaModulo.Name = "tablaModulo";
        tablaModulo.Dock = DockStyle.Fill;
        tablaModulo.Size = new Size(1068, 700);
        tablaModulo.Margin = new Padding(0);
        tablaModulo.ColumnCount = 1;
        tablaModulo.RowCount = 4;
        tablaModulo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tablaModulo.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
        tablaModulo.RowStyles.Add(new RowStyle(SizeType.Absolute, 118F));
        tablaModulo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablaModulo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
        tablaModulo.TabIndex = 0;
        // panelIntroduccion
        panelIntroduccion.Name = "panelIntroduccion";
        panelIntroduccion.Dock = DockStyle.Fill;
        panelIntroduccion.Margin = new Padding(0);
        panelIntroduccion.Size = new Size(1068, 78);
        panelIntroduccion.TabIndex = 0;
        // lblTitulo
        lblTitulo.Name = "lblTitulo";
        lblTitulo.Text = "Listado del módulo";
        lblTitulo.Location = new Point(0, 0);
        lblTitulo.Size = new Size(710, 32);
        lblTitulo.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitulo.ForeColor = Color.FromArgb(20, 43, 78);
        lblTitulo.BackColor = Color.Transparent;
        lblTitulo.AutoEllipsis = true;
        lblTitulo.TabStop = false;
        // lblTitulo
        lblTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        // lblDescripcion
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Text = "Consulte y organice la información del módulo.";
        lblDescripcion.Location = new Point(1, 37);
        lblDescripcion.Size = new Size(710, 25);
        lblDescripcion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        lblDescripcion.ForeColor = Color.FromArgb(103, 116, 137);
        lblDescripcion.BackColor = Color.Transparent;
        lblDescripcion.AutoEllipsis = true;
        lblDescripcion.TabStop = false;
        // lblDescripcion
        lblDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        // btnNuevo
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Text = "Nuevo registro";
        btnNuevo.Location = new Point(888, 9);
        btnNuevo.Size = new Size(180, 40);
        btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNuevo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        btnNuevo.BackColor = Color.FromArgb(37, 99, 235);
        btnNuevo.ForeColor = Color.White;
        btnNuevo.IconChar = FontAwesome.Sharp.IconChar.Plus;
        btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnNuevo.IconSize = 18;
        btnNuevo.IconColor = Color.White;
        btnNuevo.FlatStyle = FlatStyle.Flat;
        btnNuevo.FlatAppearance.BorderSize = 0;
        btnNuevo.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
        btnNuevo.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnNuevo.Cursor = Cursors.Hand;
        btnNuevo.UseVisualStyleBackColor = false;
        btnNuevo.Enabled = true;
        // btnNuevo
        btnNuevo.TabIndex = 0;
        // tarjetaFiltros
        tarjetaFiltros.Name = "tarjetaFiltros";
        tarjetaFiltros.Dock = DockStyle.Fill;
        tarjetaFiltros.Margin = new Padding(0, 0, 0, 18);
        tarjetaFiltros.Padding = new Padding(18, 14, 18, 14);
        tarjetaFiltros.Size = new Size(1068, 100);
        tarjetaFiltros.BackColor = Color.White;
        tarjetaFiltros.TabIndex = 1;
        // tablaFiltros
        tablaFiltros.Name = "tablaFiltros";
        tablaFiltros.Dock = DockStyle.Fill;
        tablaFiltros.ColumnCount = 3;
        tablaFiltros.RowCount = 1;
        tablaFiltros.Size = new Size(1032, 72);
        tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 384F));
        tablaFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 108F));
        tablaFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablaFiltros.Margin = new Padding(0);
        tablaFiltros.TabIndex = 0;
        // panelBuscar
        panelBuscar.Name = "panelBuscar";
        panelBuscar.Dock = DockStyle.Fill;
        panelBuscar.Margin = new Padding(0, 0, 12, 0);
        panelBuscar.Size = new Size(528, 72);
        panelBuscar.TabIndex = 0;
        // panelFiltrosModulo
        panelFiltrosModulo.Name = "panelFiltrosModulo";
        panelFiltrosModulo.Dock = DockStyle.Fill;
        panelFiltrosModulo.Margin = new Padding(0, 0, 12, 0);
        panelFiltrosModulo.Size = new Size(372, 72);
        panelFiltrosModulo.TabIndex = 1;
        // panelAccionBuscar
        panelAccionBuscar.Name = "panelAccionBuscar";
        panelAccionBuscar.Dock = DockStyle.Fill;
        panelAccionBuscar.Margin = new Padding(0, 0, 0, 0);
        panelAccionBuscar.Size = new Size(108, 72);
        panelAccionBuscar.TabIndex = 2;
        // lblBuscar
        lblBuscar.Name = "lblBuscar";
        lblBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblBuscar.Text = "Buscar";
        lblBuscar.Location = new Point(0, 0);
        lblBuscar.Size = new Size(528, 22);
        lblBuscar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblBuscar.ForeColor = Color.FromArgb(20, 43, 78);
        lblBuscar.BackColor = Color.Transparent;
        lblBuscar.AutoEllipsis = true;
        lblBuscar.TabStop = false;
        // bordeBuscar
        bordeBuscar.Name = "bordeBuscar";
        bordeBuscar.Location = new Point(0, 26);
        bordeBuscar.Size = new Size(528, 40);
        bordeBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        bordeBuscar.BackColor = Color.FromArgb(248, 250, 253);
        bordeBuscar.RadioBorde = 8;
        bordeBuscar.TabIndex = 0;
        // iconBuscar
        iconBuscar.Name = "iconBuscar";
        iconBuscar.Location = new Point(12, 10);
        iconBuscar.Size = new Size(20, 20);
        iconBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
        iconBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        iconBuscar.IconSize = 20;
        iconBuscar.IconColor = Color.FromArgb(103, 116, 137);
        iconBuscar.BackColor = Color.FromArgb(248, 250, 253);
        iconBuscar.TabStop = false;
        // txtBuscar
        txtBuscar.Name = "txtBuscar";
        txtBuscar.Location = new Point(42, 10);
        txtBuscar.Size = new Size(470, 24);
        txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtBuscar.BorderStyle = BorderStyle.None;
        txtBuscar.BackColor = Color.FromArgb(248, 250, 253);
        txtBuscar.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
        txtBuscar.ForeColor = Color.FromArgb(20, 43, 78);
        txtBuscar.PlaceholderText = "Nombre, código o referencia";
        txtBuscar.TabIndex = 0;
        // btnBuscar
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Text = "Buscar";
        btnBuscar.Location = new Point(0, 26);
        btnBuscar.Size = new Size(108, 40);
        btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBuscar.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        btnBuscar.BackColor = Color.White;
        btnBuscar.ForeColor = Color.FromArgb(20, 43, 78);
        btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
        btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnBuscar.IconSize = 18;
        btnBuscar.IconColor = Color.FromArgb(103, 116, 137);
        btnBuscar.FlatStyle = FlatStyle.Flat;
        btnBuscar.FlatAppearance.BorderSize = 1;
        btnBuscar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnBuscar.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnBuscar.Cursor = Cursors.Hand;
        btnBuscar.UseVisualStyleBackColor = false;
        btnBuscar.Enabled = true;
        // btnBuscar
        btnBuscar.TabIndex = 0;
        // tarjetaListado
        tarjetaListado.Name = "tarjetaListado";
        tarjetaListado.Dock = DockStyle.Fill;
        tarjetaListado.Size = new Size(1068, 474);
        tarjetaListado.Margin = new Padding(0);
        tarjetaListado.Padding = new Padding(1);
        tarjetaListado.BackColor = Color.White;
        tarjetaListado.TabIndex = 2;
        // tablaListado
        tablaListado.Name = "tablaListado";
        tablaListado.Dock = DockStyle.Fill;
        tablaListado.Size = new Size(1066, 472);
        tablaListado.Margin = new Padding(0);
        tablaListado.ColumnCount = 1;
        tablaListado.RowCount = 3;
        tablaListado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tablaListado.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
        tablaListado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tablaListado.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
        tablaListado.TabIndex = 0;
        // panelTituloListado
        panelTituloListado.Name = "panelTituloListado";
        panelTituloListado.Dock = DockStyle.Fill;
        panelTituloListado.Margin = new Padding(0);
        panelTituloListado.Size = new Size(1066, 68);
        panelTituloListado.TabIndex = 0;
        // lblListado
        lblListado.Name = "lblListado";
        lblListado.Text = "Registros";
        lblListado.Location = new Point(20, 21);
        lblListado.Size = new Size(600, 28);
        lblListado.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
        lblListado.ForeColor = Color.FromArgb(20, 43, 78);
        lblListado.BackColor = Color.Transparent;
        lblListado.AutoEllipsis = true;
        lblListado.TabStop = false;
        // lblListado
        lblListado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        // btnEditar
        btnEditar.Name = "btnEditar";
        btnEditar.Text = "Editar";
        btnEditar.Location = new Point(764, 14);
        btnEditar.Size = new Size(114, 40);
        btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnEditar.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        btnEditar.BackColor = Color.White;
        btnEditar.ForeColor = Color.FromArgb(20, 43, 78);
        btnEditar.IconChar = FontAwesome.Sharp.IconChar.PenToSquare;
        btnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnEditar.IconSize = 18;
        btnEditar.IconColor = Color.FromArgb(103, 116, 137);
        btnEditar.FlatStyle = FlatStyle.Flat;
        btnEditar.FlatAppearance.BorderSize = 1;
        btnEditar.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnEditar.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnEditar.Cursor = Cursors.Hand;
        btnEditar.UseVisualStyleBackColor = false;
        btnEditar.Enabled = false;
        // btnEditar
        btnEditar.TabIndex = 0;
        // btnCambiarEstado
        btnCambiarEstado.Name = "btnCambiarEstado";
        btnCambiarEstado.Text = "Cambiar estado";
        btnCambiarEstado.Location = new Point(888, 14);
        btnCambiarEstado.Size = new Size(158, 40);
        btnCambiarEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCambiarEstado.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        btnCambiarEstado.BackColor = Color.White;
        btnCambiarEstado.ForeColor = Color.FromArgb(20, 43, 78);
        btnCambiarEstado.IconChar = FontAwesome.Sharp.IconChar.ToggleOff;
        btnCambiarEstado.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnCambiarEstado.IconSize = 18;
        btnCambiarEstado.IconColor = Color.FromArgb(103, 116, 137);
        btnCambiarEstado.FlatStyle = FlatStyle.Flat;
        btnCambiarEstado.FlatAppearance.BorderSize = 1;
        btnCambiarEstado.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnCambiarEstado.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnCambiarEstado.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnCambiarEstado.Cursor = Cursors.Hand;
        btnCambiarEstado.UseVisualStyleBackColor = false;
        btnCambiarEstado.Enabled = false;
        // btnCambiarEstado
        btnCambiarEstado.TabIndex = 1;
        // panelGrilla
        panelGrilla.Name = "panelGrilla";
        panelGrilla.Dock = DockStyle.Fill;
        panelGrilla.Margin = new Padding(0);
        panelGrilla.Size = new Size(1066, 340);
        panelGrilla.TabIndex = 1;
        // panelVacio
        panelVacio.Name = "panelVacio";
        panelVacio.Location = new Point(0, 44);
        panelVacio.Size = new Size(1066, 296);
        panelVacio.Dock = DockStyle.Fill;
        panelVacio.BackColor = Color.White;
        panelVacio.TabIndex = 1;
        // tablaVacio
        tablaVacio.Name = "tablaVacio";
        tablaVacio.Dock = DockStyle.Fill;
        tablaVacio.Margin = new Padding(0);
        tablaVacio.ColumnCount = 1;
        tablaVacio.RowCount = 5;
        tablaVacio.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tablaVacio.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tablaVacio.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
        tablaVacio.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
        tablaVacio.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        tablaVacio.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        // iconSinRegistros
        iconSinRegistros.Name = "iconSinRegistros";
        iconSinRegistros.Size = new Size(46, 46);
        iconSinRegistros.Anchor = AnchorStyles.None;
        iconSinRegistros.IconChar = FontAwesome.Sharp.IconChar.Users;
        iconSinRegistros.IconFont = FontAwesome.Sharp.IconFont.Auto;
        iconSinRegistros.IconSize = 46;
        iconSinRegistros.IconColor = Color.FromArgb(167, 189, 222);
        iconSinRegistros.BackColor = Color.White;
        iconSinRegistros.TabStop = false;
        // lblSinRegistros
        lblSinRegistros.Name = "lblSinRegistros";
        lblSinRegistros.Text = "Sin registros para mostrar";
        lblSinRegistros.Location = new Point(0, 0);
        lblSinRegistros.Size = new Size(900, 32);
        lblSinRegistros.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
        lblSinRegistros.ForeColor = Color.FromArgb(20, 43, 78);
        lblSinRegistros.BackColor = Color.Transparent;
        lblSinRegistros.AutoEllipsis = true;
        lblSinRegistros.TabStop = false;
        // lblSinRegistros
        lblSinRegistros.Dock = DockStyle.Fill;
        lblSinRegistros.TextAlign = ContentAlignment.MiddleCenter;
        lblSinRegistros.Margin = new Padding(12, 0, 12, 0);
        // lblSinRegistrosDetalle
        lblSinRegistrosDetalle.Name = "lblSinRegistrosDetalle";
        lblSinRegistrosDetalle.Text = "Los registros del módulo aparecerán en este listado.";
        lblSinRegistrosDetalle.Location = new Point(0, 0);
        lblSinRegistrosDetalle.Size = new Size(900, 28);
        lblSinRegistrosDetalle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        lblSinRegistrosDetalle.ForeColor = Color.FromArgb(103, 116, 137);
        lblSinRegistrosDetalle.BackColor = Color.Transparent;
        lblSinRegistrosDetalle.AutoEllipsis = true;
        lblSinRegistrosDetalle.TabStop = false;
        // lblSinRegistrosDetalle
        lblSinRegistrosDetalle.Dock = DockStyle.Fill;
        lblSinRegistrosDetalle.TextAlign = ContentAlignment.MiddleCenter;
        lblSinRegistrosDetalle.Margin = new Padding(12, 0, 12, 0);
        // dgvListado
        dgvListado.Name = "dgvListado";
        dgvListado.Dock = DockStyle.Top;
        dgvListado.Size = new Size(1066, 44);
        dgvListado.ReadOnly = true;
        dgvListado.AllowUserToAddRows = false;
        dgvListado.AllowUserToDeleteRows = false;
        dgvListado.AllowUserToResizeRows = false;
        dgvListado.MultiSelect = false;
        dgvListado.RowHeadersVisible = false;
        dgvListado.BorderStyle = BorderStyle.None;
        dgvListado.BackgroundColor = Color.White;
        dgvListado.GridColor = Color.FromArgb(235, 240, 246);
        dgvListado.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvListado.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dgvListado.ColumnHeadersHeight = 44;
        dgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvListado.EnableHeadersVisualStyles = false;
        dgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvListado.RowTemplate.Height = 44;
        dgvListado.TabIndex = 0;
        estiloCabecera.BackColor = Color.FromArgb(246, 248, 251);
        estiloCabecera.ForeColor = Color.FromArgb(103, 116, 137);
        estiloCabecera.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
        estiloCabecera.Padding = new Padding(12, 0, 6, 0);
        estiloCabecera.SelectionBackColor = Color.FromArgb(246, 248, 251);
        estiloCabecera.SelectionForeColor = Color.FromArgb(103, 116, 137);
        dgvListado.ColumnHeadersDefaultCellStyle = estiloCabecera;
        estiloFila.BackColor = Color.White;
        estiloFila.ForeColor = Color.FromArgb(20, 43, 78);
        estiloFila.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        estiloFila.Padding = new Padding(12, 0, 6, 0);
        estiloFila.SelectionBackColor = Color.FromArgb(234, 240, 252);
        estiloFila.SelectionForeColor = Color.FromArgb(20, 43, 78);
        dgvListado.DefaultCellStyle = estiloFila;
        estiloAlterno.BackColor = Color.FromArgb(250, 252, 254);
        dgvListado.AlternatingRowsDefaultCellStyle = estiloAlterno;
        // dgvListado
        // panelPaginacion
        panelPaginacion.Name = "panelPaginacion";
        panelPaginacion.Dock = DockStyle.Fill;
        panelPaginacion.Margin = new Padding(0);
        panelPaginacion.Size = new Size(1066, 64);
        panelPaginacion.TabIndex = 2;
        // lineaPaginacion
        lineaPaginacion.Name = "lineaPaginacion";
        lineaPaginacion.Dock = DockStyle.Top;
        lineaPaginacion.Height = 1;
        lineaPaginacion.BackColor = Color.FromArgb(235, 240, 246);
        // lblSeleccion
        lblSeleccion.Name = "lblSeleccion";
        lblSeleccion.Text = "Seleccione un registro para ver sus acciones.";
        lblSeleccion.Location = new Point(20, 23);
        lblSeleccion.Size = new Size(640, 24);
        lblSeleccion.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        lblSeleccion.ForeColor = Color.FromArgb(103, 116, 137);
        lblSeleccion.BackColor = Color.Transparent;
        lblSeleccion.AutoEllipsis = true;
        lblSeleccion.TabStop = false;
        // lblSeleccion
        lblSeleccion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        // btnAnterior
        btnAnterior.Name = "btnAnterior";
        btnAnterior.Text = "";
        btnAnterior.Location = new Point(796, 12);
        btnAnterior.Size = new Size(40, 40);
        btnAnterior.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAnterior.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        btnAnterior.BackColor = Color.White;
        btnAnterior.ForeColor = Color.FromArgb(20, 43, 78);
        btnAnterior.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
        btnAnterior.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnAnterior.IconSize = 18;
        btnAnterior.IconColor = Color.FromArgb(103, 116, 137);
        btnAnterior.FlatStyle = FlatStyle.Flat;
        btnAnterior.FlatAppearance.BorderSize = 1;
        btnAnterior.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnAnterior.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnAnterior.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnAnterior.Cursor = Cursors.Hand;
        btnAnterior.UseVisualStyleBackColor = false;
        btnAnterior.Enabled = false;
        // btnAnterior
        btnAnterior.TabIndex = 0;
        // lblPagina
        lblPagina.Name = "lblPagina";
        lblPagina.Text = "Página — de —";
        lblPagina.Location = new Point(845, 20);
        lblPagina.Size = new Size(150, 25);
        lblPagina.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point);
        lblPagina.ForeColor = Color.FromArgb(103, 116, 137);
        lblPagina.BackColor = Color.Transparent;
        lblPagina.AutoEllipsis = true;
        lblPagina.TabStop = false;
        // lblPagina
        lblPagina.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblPagina.TextAlign = ContentAlignment.MiddleCenter;
        // btnSiguiente
        btnSiguiente.Name = "btnSiguiente";
        btnSiguiente.Text = "";
        btnSiguiente.Location = new Point(1004, 12);
        btnSiguiente.Size = new Size(40, 40);
        btnSiguiente.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSiguiente.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold, GraphicsUnit.Point);
        btnSiguiente.BackColor = Color.White;
        btnSiguiente.ForeColor = Color.FromArgb(20, 43, 78);
        btnSiguiente.IconChar = FontAwesome.Sharp.IconChar.AngleRight;
        btnSiguiente.IconFont = FontAwesome.Sharp.IconFont.Auto;
        btnSiguiente.IconSize = 18;
        btnSiguiente.IconColor = Color.FromArgb(103, 116, 137);
        btnSiguiente.FlatStyle = FlatStyle.Flat;
        btnSiguiente.FlatAppearance.BorderSize = 1;
        btnSiguiente.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        btnSiguiente.FlatAppearance.MouseOverBackColor = Color.FromArgb(244, 246, 250);
        btnSiguiente.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnSiguiente.Cursor = Cursors.Hand;
        btnSiguiente.UseVisualStyleBackColor = false;
        btnSiguiente.Enabled = false;
        // btnSiguiente
        btnSiguiente.TabIndex = 1;
        // lblPie
        lblPie.Name = "lblPie";
        lblPie.Text = "AL DÍA  /  ADMINISTRACIÓN";
        lblPie.Location = new Point(0, 0);
        lblPie.Size = new Size(1068, 30);
        lblPie.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
        lblPie.ForeColor = Color.FromArgb(103, 116, 137);
        lblPie.BackColor = Color.Transparent;
        lblPie.AutoEllipsis = true;
        lblPie.TabStop = false;
        // lblPie
        lblPie.Dock = DockStyle.Fill;
        lblPie.TextAlign = ContentAlignment.BottomLeft;
        lblPie.Margin = new Padding(0);
        panelContenido.Controls.Add(tablaModulo);
        tablaModulo.Controls.Add(panelIntroduccion, 0, 0);
        panelIntroduccion.Controls.Add(lblTitulo);
        panelIntroduccion.Controls.Add(lblDescripcion);
        panelIntroduccion.Controls.Add(btnNuevo);
        tablaModulo.Controls.Add(tarjetaFiltros, 0, 1);
        tarjetaFiltros.Controls.Add(tablaFiltros);
        tablaFiltros.Controls.Add(panelBuscar, 0, 0);
        tablaFiltros.Controls.Add(panelFiltrosModulo, 1, 0);
        tablaFiltros.Controls.Add(panelAccionBuscar, 2, 0);
        panelBuscar.Controls.Add(lblBuscar);
        panelBuscar.Controls.Add(bordeBuscar);
        bordeBuscar.Controls.Add(iconBuscar);
        bordeBuscar.Controls.Add(txtBuscar);
        panelAccionBuscar.Controls.Add(btnBuscar);
        tablaModulo.Controls.Add(tarjetaListado, 0, 2);
        tarjetaListado.Controls.Add(tablaListado);
        tablaListado.Controls.Add(panelTituloListado, 0, 0);
        panelTituloListado.Controls.Add(lblListado);
        panelTituloListado.Controls.Add(btnEditar);
        panelTituloListado.Controls.Add(btnCambiarEstado);
        tablaListado.Controls.Add(panelGrilla, 0, 1);
        panelGrilla.Controls.Add(panelVacio);
        panelVacio.Controls.Add(tablaVacio);
        tablaVacio.Controls.Add(iconSinRegistros, 0, 1);
        tablaVacio.Controls.Add(lblSinRegistros, 0, 2);
        tablaVacio.Controls.Add(lblSinRegistrosDetalle, 0, 3);
        panelGrilla.Controls.Add(dgvListado);
        panelGrilla.Controls.SetChildIndex(panelVacio, 0);
        tablaListado.Controls.Add(panelPaginacion, 0, 2);
        panelPaginacion.Controls.Add(lineaPaginacion);
        panelPaginacion.Controls.Add(lblSeleccion);
        panelPaginacion.Controls.Add(btnAnterior);
        panelPaginacion.Controls.Add(lblPagina);
        panelPaginacion.Controls.Add(btnSiguiente);
        tablaModulo.Controls.Add(lblPie, 0, 3);
        Controls.Add(panelContenido);
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(244, 246, 250);
        ClientSize = new Size(1068, 700);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        FormBorderStyle = FormBorderStyle.None;
        ShowInTaskbar = false;
        Name = "FrmListadoBase";
        Text = "Listado";
        lineaPaginacion.ResumeLayout(false);
        lineaPaginacion.PerformLayout();
        panelPaginacion.ResumeLayout(false);
        panelPaginacion.PerformLayout();
        tablaVacio.ResumeLayout(false);
        tablaVacio.PerformLayout();
        panelVacio.ResumeLayout(false);
        panelVacio.PerformLayout();
        panelGrilla.ResumeLayout(false);
        panelGrilla.PerformLayout();
        panelTituloListado.ResumeLayout(false);
        panelTituloListado.PerformLayout();
        tablaListado.ResumeLayout(false);
        tablaListado.PerformLayout();
        tarjetaListado.ResumeLayout(false);
        tarjetaListado.PerformLayout();
        bordeBuscar.ResumeLayout(false);
        bordeBuscar.PerformLayout();
        panelFiltrosModulo.ResumeLayout(false);
        panelAccionBuscar.ResumeLayout(false);
        panelAccionBuscar.PerformLayout();
        panelBuscar.ResumeLayout(false);
        panelBuscar.PerformLayout();
        tablaFiltros.ResumeLayout(false);
        tablaFiltros.PerformLayout();
        tarjetaFiltros.ResumeLayout(false);
        tarjetaFiltros.PerformLayout();
        panelIntroduccion.ResumeLayout(false);
        panelIntroduccion.PerformLayout();
        tablaModulo.ResumeLayout(false);
        tablaModulo.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)iconBuscar).EndInit();
        ((System.ComponentModel.ISupportInitialize)iconSinRegistros).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvListado).EndInit();
        panelContenido.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    protected Panel panelContenido;
    protected TableLayoutPanel tablaModulo;
    protected Panel panelIntroduccion;
    protected Label lblTitulo;
    protected Label lblDescripcion;
    protected FontAwesome.Sharp.IconButton btnNuevo;
    protected AlDia.UI.Controles.PanelTarjeta tarjetaFiltros;
    protected TableLayoutPanel tablaFiltros;
    protected Panel panelBuscar;
    protected Panel panelFiltrosModulo;
    protected Panel panelAccionBuscar;
    protected Label lblBuscar;
    protected AlDia.UI.Controles.PanelTarjeta bordeBuscar;
    protected FontAwesome.Sharp.IconPictureBox iconBuscar;
    protected TextBox txtBuscar;
    protected FontAwesome.Sharp.IconButton btnBuscar;
    protected AlDia.UI.Controles.PanelTarjeta tarjetaListado;
    protected TableLayoutPanel tablaListado;
    protected Panel panelTituloListado;
    protected Label lblListado;
    protected FontAwesome.Sharp.IconButton btnEditar;
    protected FontAwesome.Sharp.IconButton btnCambiarEstado;
    protected Panel panelGrilla;
    protected Panel panelVacio;
    protected TableLayoutPanel tablaVacio;
    protected FontAwesome.Sharp.IconPictureBox iconSinRegistros;
    protected Label lblSinRegistros;
    protected Label lblSinRegistrosDetalle;
    protected DataGridView dgvListado;
    protected Panel panelPaginacion;
    protected Panel lineaPaginacion;
    protected Label lblSeleccion;
    protected FontAwesome.Sharp.IconButton btnAnterior;
    protected Label lblPagina;
    protected FontAwesome.Sharp.IconButton btnSiguiente;
    protected Label lblPie;
}
