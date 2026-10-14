using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Reportes;

// Vista previa visual del reporte; no consulta datos ni genera documentos.
public partial class FrmVistaReporte : FrmRegistroBase
{
    public FrmVistaReporte()
    {
        InitializeComponent();
    }

    public FrmVistaReporte(int tipoReporte) : this()
    {
        ConfigurarPresentacion(tipoReporte);
    }

    private void ConfigurarPresentacion(int tipoReporte)
    {
        string[] titulos;
        switch (tipoReporte)
        {
            case 1:
                lblTipoReporte.Text = "Pagos";
                lblFiltrosReporte.Text = "Método: Todos · Estado: Todos";
                lblDetalleReporte.Text = "Detalle de pagos";
                lblObligatorios.Text = "Vista previa · Pagos";
                lblOrdenesReporte.Text = "Pagos registrados";
                lblTotalReporte.Text = "Total cobrado (C$)";
                lblCobradoReporte.Text = "Efectivo (C$)";
                lblPendienteReporte.Text = "Transferencias (C$)";
                titulos = ["Recibo", "Fecha", "Cliente", "Orden", "Método", "Importe (C$)"];
                break;
            case 2:
                lblTipoReporte.Text = "Inventario";
                lblPeriodoReporte.Text = "Fecha de corte: — / — / —";
                lblFiltrosReporte.Text = "Marca: Todas · Estado: Todos";
                lblDetalleReporte.Text = "Detalle de repuestos";
                lblObligatorios.Text = "Vista previa · Inventario";
                lblOrdenesReporte.Text = "Repuestos";
                lblTotalReporte.Text = "Valor de inventario (C$)";
                lblCobradoReporte.Text = "Unidades disponibles";
                lblPendienteReporte.Text = "Marcas";
                titulos = ["Código", "Repuesto", "Marca", "Existencias", "Precio compra (C$)", "Precio venta (C$)"];
                dgvDetalleReporte.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDetalleReporte.Columns[3].DefaultCellStyle.Format = "N0";
                dgvDetalleReporte.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDetalleReporte.Columns[4].DefaultCellStyle.Format = "N2";
                dgvDetalleReporte.Columns[0].FillWeight = 10F;
                dgvDetalleReporte.Columns[1].FillWeight = 24F;
                dgvDetalleReporte.Columns[2].FillWeight = 16F;
                dgvDetalleReporte.Columns[3].FillWeight = 14F;
                dgvDetalleReporte.Columns[4].FillWeight = 18F;
                dgvDetalleReporte.Columns[5].FillWeight = 18F;
                break;
            default:
                return;
        }

        for (var indice = 0; indice < titulos.Length; indice++)
            dgvDetalleReporte.Columns[indice].HeaderText = titulos[indice];
    }
}
