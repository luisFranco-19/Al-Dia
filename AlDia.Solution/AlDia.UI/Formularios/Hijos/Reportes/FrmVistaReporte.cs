using System.Data;
using System.Drawing.Printing;
using System.Text;
using AlDia.BLL.Common;
using AlDia.UI.Formularios.Base;
using FontAwesome.Sharp;

namespace AlDia.UI.Formularios.Hijos.Reportes;

public partial class FrmVistaReporte : FrmRegistroBase
{
    private readonly DatosReporte? _reporte;
    private readonly IconButton _btnExportarPdf = new();
    private readonly IconButton _btnImprimir = new();
    private int _filaImpresion;
    private int _paginaImpresion;

    public FrmVistaReporte()
    {
        InitializeComponent();
        dgvDetalleReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        dgvDetalleReporte.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvDetalleReporte.ColumnHeadersDefaultCellStyle.BackColor;
        dgvDetalleReporte.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
        dgvDetalleReporte.DefaultCellStyle.ForeColor = Color.Black;
        dgvDetalleReporte.DefaultCellStyle.SelectionForeColor = Color.Black;
        btnGuardar.Visible = false;
        PrepararBoton(_btnExportarPdf, "Exportar PDF", IconChar.FilePdf, true);
        PrepararBoton(_btnImprimir, "Imprimir", IconChar.Print, false);
        _btnExportarPdf.Click += (_, _) => ExportarPdf();
        _btnImprimir.Click += (_, _) => ImprimirReporte();
        panelPie.Controls.Add(_btnExportarPdf);
        panelPie.Controls.Add(_btnImprimir);
        panelPie.SizeChanged += (_, _) => AcomodarAcciones();
        AcomodarAcciones();
    }

    internal FrmVistaReporte(DatosReporte reporte, AplicacionBll aplicacion) : this()
    {
        _reporte = reporte ?? throw new ArgumentNullException(nameof(reporte));
        ConfigurarServicios(aplicacion);
    }

    protected override async Task CargarDatosAsync()
    {
        if (_reporte is null) return;
        await Task.Yield();

        lblTitulo.Text = $"Vista previa · {_reporte.Titulo}";
        lblObligatorios.Text = $"Vista previa · {_reporte.Titulo}";
        lblTipoReporte.Text = _reporte.Titulo;
        lblPeriodoReporte.Text = _reporte.Periodo;
        lblFiltrosReporte.Text = _reporte.Filtros;
        lblDetalleReporte.Text = _reporte.TituloDetalle;
        lblOrdenesReporte.Text = _reporte.Indicador1;
        lblValorOrdenes.Text = _reporte.Valor1;
        lblTotalReporte.Text = _reporte.Indicador2;
        lblValorTotal.Text = _reporte.Valor2;
        lblCobradoReporte.Text = _reporte.Indicador3;
        lblValorCobrado.Text = _reporte.Valor3;
        lblPendienteReporte.Text = _reporte.Indicador4;
        lblValorPendiente.Text = _reporte.Valor4;

        for (int i = 0; i < dgvDetalleReporte.Columns.Count; i++)
        {
            dgvDetalleReporte.Columns[i].HeaderText = _reporte.Filas.ExtendedProperties[$"Header{i}"]?.ToString()
                ?? dgvDetalleReporte.Columns[i].HeaderText;
            dgvDetalleReporte.Columns[i].DataPropertyName = $"C{i}";
        }
        dgvDetalleReporte.DataSource = _reporte.Filas.Copy();
        bool hayDatos = _reporte.Filas.Rows.Count > 0;
        dgvDetalleReporte.Dock = hayDatos ? DockStyle.Fill : DockStyle.Top;
        if (!hayDatos) dgvDetalleReporte.Height = dgvDetalleReporte.ColumnHeadersHeight;
        dgvDetalleReporte.Visible = hayDatos;
        tablaSinDatos.Visible = !hayDatos;
        if (_reporte.Titulo == "Inventario")
        {
            dgvDetalleReporte.Columns[3].DefaultCellStyle.Format = "N0";
            dgvDetalleReporte.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalleReporte.Columns[4].DefaultCellStyle.Format = "N2";
            dgvDetalleReporte.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        dgvDetalleReporte.Columns[^1].DefaultCellStyle.Format = "N2";
        dgvDetalleReporte.Columns[^1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        dgvDetalleReporte.Columns[1].DefaultCellStyle.Format = "g";
        if (hayDatos) dgvDetalleReporte.BringToFront();
        else tablaSinDatos.BringToFront();
        lblReporteVacioDetalle.Text = $"No se encontraron registros para {_reporte.Titulo.ToLowerInvariant()} con los filtros seleccionados.";
        lblPaginaDocumento.Text = $"{_reporte.Filas.Rows.Count:N0} registros · generado {DateTime.Now:dd/MM/yyyy HH:mm}";
        _btnExportarPdf.Visible = _btnImprimir.Visible = true;
        AcomodarAcciones();
    }

    private static void PrepararBoton(IconButton boton, string texto, IconChar icono, bool principal)
    {
        boton.Text = texto;
        boton.IconChar = icono;
        boton.IconColor = principal ? Color.White : Color.FromArgb(37, 99, 235);
        boton.IconSize = 18;
        boton.TextImageRelation = TextImageRelation.ImageBeforeText;
        boton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        boton.ForeColor = principal ? Color.White : Color.FromArgb(20, 43, 78);
        boton.BackColor = principal ? Color.FromArgb(37, 99, 235) : Color.White;
        boton.FlatStyle = FlatStyle.Flat;
        boton.FlatAppearance.BorderSize = principal ? 0 : 1;
        boton.FlatAppearance.BorderColor = Color.FromArgb(211, 221, 232);
        boton.Size = new Size(principal ? 166 : 132, 48);
        boton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        boton.Visible = false;
        boton.Cursor = Cursors.Hand;
    }

    private void AcomodarAcciones()
    {
        if (panelPie.ClientSize.Width <= 0) return;
        int y = Math.Max(8, (panelPie.ClientSize.Height - _btnExportarPdf.Height) / 2);
        int derecha = panelPie.ClientSize.Width - 32;
        btnCancelar.Location = new Point(derecha - btnCancelar.Width, y);
        _btnImprimir.Location = new Point(btnCancelar.Left - 12 - _btnImprimir.Width, y);
        _btnExportarPdf.Location = new Point(_btnImprimir.Left - 12 - _btnExportarPdf.Width, y);
        lblObligatorios.Location = new Point(32, y);
        lblObligatorios.Size = new Size(Math.Max(0, _btnExportarPdf.Left - 48), lblObligatorios.Height);
    }

    private void ExportarPdf()
    {
        if (_reporte is null) return;
        using var dialogo = new SaveFileDialog
        {
            Title = "Exportar reporte a PDF",
            FileName = $"{NombreArchivo(_reporte.Titulo)}_{DateTime.Now:yyyyMMdd_HHmm}.pdf",
            DefaultExt = "pdf",
            AddExtension = true,
            Filter = "Documento PDF (*.pdf)|*.pdf",
            OverwritePrompt = true
        };
        if (dialogo.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            GeneradorPdfReporte.Guardar(_reporte, dialogo.FileName);
            MessageBox.Show(this, "El reporte se exportó correctamente.", "Exportación PDF",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"No fue posible exportar el reporte: {ex.Message}", "Exportación PDF",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ImprimirReporte()
    {
        if (_reporte is null) return;
        using var documento = new PrintDocument
        {
            DocumentName = $"Al Día - {_reporte.Titulo}"
        };
        documento.DefaultPageSettings.Landscape = true;
        documento.DefaultPageSettings.Margins = new Margins(42, 42, 42, 42);
        documento.PrintPage += DibujarPagina;
        using var dialogo = new PrintDialog
        {
            Document = documento,
            AllowSelection = false,
            AllowSomePages = false,
            UseEXDialog = true
        };
        if (dialogo.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            _filaImpresion = 0;
            _paginaImpresion = 1;
            documento.Print();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"No fue posible imprimir el reporte: {ex.Message}", "Impresión",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DibujarPagina(object? sender, PrintPageEventArgs e)
    {
        if (_reporte is null) { e.HasMorePages = false; return; }
        Graphics? graphics = e.Graphics;
        if (graphics is null) { e.HasMorePages = false; return; }
        Rectangle area = e.MarginBounds;
        using var fuenteTitulo = new Font("Segoe UI", 16, FontStyle.Bold);
        using var fuente = new Font("Segoe UI", 8.5F);
        using var fuenteNegrita = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        using var lapiz = new Pen(Color.FromArgb(210, 218, 229));
        using var formato = new StringFormat(StringFormatFlags.NoWrap)
        {
            Trimming = StringTrimming.EllipsisCharacter,
            LineAlignment = StringAlignment.Center
        };
        float y = area.Top;
        graphics.DrawString($"Al Día · {_reporte.Titulo}", fuenteTitulo, Brushes.Black, area.Left, y);
        y += 30;
        graphics.DrawString(_reporte.Periodo, fuente, Brushes.Gray, area.Left, y);
        y += 17;
        graphics.DrawString(_reporte.Filtros, fuente, Brushes.Gray, area.Left, y);
        y += 22;
        graphics.DrawString($"{_reporte.Indicador1}: {_reporte.Valor1}     {_reporte.Indicador2}: {_reporte.Valor2}", fuenteNegrita, Brushes.Black, area.Left, y);
        y += 16;
        graphics.DrawString($"{_reporte.Indicador3}: {_reporte.Valor3}     {_reporte.Indicador4}: {_reporte.Valor4}", fuenteNegrita, Brushes.Black, area.Left, y);
        y += 24;

        int columnas = Math.Max(1, _reporte.Filas.Columns.Count);
        float anchoColumna = area.Width / (float)columnas;
        const float altoFila = 24;
        for (int c = 0; c < columnas; c++)
        {
            string encabezado = _reporte.Filas.ExtendedProperties[$"Header{c}"]?.ToString()
                ?? _reporte.Filas.Columns[c].ColumnName;
            var celda = new RectangleF(area.Left + c * anchoColumna, y, anchoColumna - 8, altoFila);
            graphics.DrawString(encabezado, fuenteNegrita, Brushes.Black, celda, formato);
        }
        y += altoFila;
        graphics.DrawLine(lapiz, area.Left, y, area.Right, y);
        y += 2;

        while (_filaImpresion < _reporte.Filas.Rows.Count && y + altoFila <= area.Bottom - 22)
        {
            DataRow fila = _reporte.Filas.Rows[_filaImpresion];
            for (int c = 0; c < columnas; c++)
            {
                var celda = new RectangleF(area.Left + c * anchoColumna, y, anchoColumna - 8, altoFila);
                graphics.DrawString(Formatear(fila[c]), fuente, Brushes.Black, celda, formato);
            }
            y += altoFila;
            if (_filaImpresion % 2 == 0) graphics.DrawLine(lapiz, area.Left, y, area.Right, y);
            _filaImpresion++;
        }

        graphics.DrawString($"Página {_paginaImpresion} · {_reporte.Filas.Rows.Count:N0} registros",
            fuente, Brushes.Gray, area.Left, area.Bottom - 14);
        e.HasMorePages = _filaImpresion < _reporte.Filas.Rows.Count;
        _paginaImpresion++;
    }

    private static string Formatear(object valor) => valor switch
    {
        DBNull => "",
        DateTime fecha => fecha.ToString("dd/MM/yyyy HH:mm"),
        decimal importe => importe.ToString("N2"),
        int entero => entero.ToString("N0"),
        long entero => entero.ToString("N0"),
        _ => valor.ToString() ?? ""
    };

    private static string NombreArchivo(string titulo)
    {
        var invalido = Path.GetInvalidFileNameChars();
        var limpio = new StringBuilder(titulo.Length);
        foreach (char caracter in titulo)
            limpio.Append(invalido.Contains(caracter) ? '_' : caracter);
        return limpio.ToString().Replace(' ', '_');
    }
}
