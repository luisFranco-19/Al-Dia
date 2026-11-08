using System.Data;
using AlDia.BLL.Common;
using AlDia.Entity.Inventario;
using AlDia.Entity.Pagos;
using AlDia.Entity.Reparaciones;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Reportes;

public partial class FrmReportes : FrmListadoBase
{
    private DatosReporte? _reporteActual;

    public FrmReportes()
    {
        InitializeComponent();
        btnBuscar.Enabled = true;
        tabReportes.SelectedIndexChanged += async (_, _) =>
        {
            bool filtraPorFecha = tabReportes.SelectedIndex != 2;
            dtpDesde.Enabled = dtpHasta.Enabled = filtraPorFecha;
            await RecargarAsync(1);
        };
        PrepararGrilla(dgvOrdenes, colOrdenes0, colOrdenes1, colOrdenes2, colOrdenes3, colOrdenes4, colOrdenes5);
        PrepararGrilla(dgvPagos, colPagos0, colPagos1, colPagos2, colPagos3, colPagos4, colPagos5);
        PrepararGrilla(dgvInventario, colInventario0, colInventario1, colInventario2, colInventario3, colInventario4, colInventario5);
    }

    public override void ConfigurarServicios(AplicacionBll aplicacion)
    {
        base.ConfigurarServicios(aplicacion);

        // El procedimiento de pagos solo autoriza a administradores y recepcionistas.
        if (aplicacion.Sesion.UsuarioActual?.PermisoRol == AlDia.Entity.Seguridad.RolUsuario.Tecnico
            && tabReportes.TabPages.Contains(tabPagos))
            tabReportes.TabPages.Remove(tabPagos);
    }

    protected override bool MostrarBotonPrincipal => _reporteActual is not null;

    protected override bool MostrarBotonSecundario => false;

    protected override bool MostrarBotonTerciario => false;

    protected override Form? CrearFormularioAccionPrincipal() => _reporteActual is null
        ? null
        : new FrmVistaReporte(_reporteActual, Aplicacion);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        ValidarPeriodo();
        string termino = buscar?.Trim() ?? "";
        _reporteActual = tabReportes.SelectedIndex switch
        {
            1 => await CrearReportePagosAsync(termino, TokenCarga),
            2 => await CrearReporteInventarioAsync(termino, TokenCarga),
            _ => await CrearReporteOrdenesAsync(termino, TokenCarga)
        };

        TotalRegistros = _reporteActual.Filas.Rows.Count;
        TotalPaginas = 0;
        PaginaActual = 1;
        lblSeleccion.Text = $"Registros: {TotalRegistros:N0}";
        lblPie.Text = $"Reporte: {_reporteActual.Titulo} · {TotalRegistros:N0} registros";
        MostrarGrilla(tabReportes.SelectedIndex, TotalRegistros > 0, _reporteActual.Filas);
    }

    private async Task<DatosReporte> CrearReporteOrdenesAsync(string buscar, CancellationToken ct)
    {
        var ordenes = await Aplicacion.Ordenes.ConsultarAsync(ct: ct);
        var equipos = await Aplicacion.Equipos.ConsultarAsync(soloActivos: false, ct: ct);
        var equiposPorId = equipos.ToDictionary(x => x.IdEquipo);
        var filas = ordenes
            .Where(x => EnPeriodo(x.FechaRecepcion))
            .Select(x => new
            {
                Orden = x,
                Equipo = equiposPorId.GetValueOrDefault(x.IdEquipo)
            })
            .Where(x => Coincide(buscar, x.Orden.NumeroOrden, x.Orden.NombreCompletoCliente,
                x.Orden.EstadoNombre, x.Equipo?.Marca, x.Equipo?.Modelo))
            .OrderByDescending(x => x.Orden.FechaRecepcion)
            .ToList();

        var tabla = CrearTabla(
            ["Orden", "Recepción", "Cliente", "Equipo", "Estado", "Total (C$)"],
            filas.Select(x => new object?[]
            {
                x.Orden.NumeroOrden, x.Orden.FechaRecepcion, x.Orden.NombreCompletoCliente,
                x.Equipo is null ? $"Equipo #{x.Orden.IdEquipo}" : $"{x.Equipo.Marca} {x.Equipo.Modelo}".Trim(),
                x.Orden.EstadoNombre, x.Orden.Total
            }));

        return new DatosReporte("Órdenes de servicio", PeriodoTexto(), FiltrosTexto("Órdenes vigentes"),
            "Detalle de órdenes", tabla, "Órdenes", filas.Count.ToString("N0"),
            "Total (C$)", filas.Sum(x => x.Orden.Total).ToString("N2"),
            "Cobrado (C$)", filas.Sum(x => x.Orden.Pagado).ToString("N2"),
            "Saldo (C$)", filas.Sum(x => x.Orden.Saldo).ToString("N2"));
    }

    private async Task<DatosReporte> CrearReportePagosAsync(string buscar, CancellationToken ct)
    {
        var pagos = await Aplicacion.Pagos.ConsultarAsync(incluirAnulados: false, ct: ct);
        var ordenes = await Aplicacion.Ordenes.ConsultarAsync(new FiltroOrdenes(IncluirAnuladas: true), ct);
        var ordenesPorId = ordenes.ToDictionary(x => x.IdOrden);
        var filas = pagos
            .Where(x => EnPeriodo(x.FechaPago))
            .Select(x => new { Pago = x, Orden = ordenesPorId.GetValueOrDefault(x.IdOrden) })
            .Where(x => Coincide(buscar, x.Pago.IdPago.ToString(), x.Orden?.NumeroOrden,
                x.Orden?.NombreCompletoCliente, x.Pago.MetodoPago.ToString()))
            .OrderByDescending(x => x.Pago.FechaPago)
            .ToList();

        var tabla = CrearTabla(
            ["Recibo", "Fecha", "Cliente", "Orden", "Método", "Importe (C$)"],
            filas.Select(x => new object?[]
            {
                $"P-{x.Pago.IdPago:D6}", x.Pago.FechaPago,
                x.Orden?.NombreCompletoCliente ?? "Cliente no disponible",
                x.Orden?.NumeroOrden ?? $"Orden #{x.Pago.IdOrden}",
                x.Pago.MetodoPago.ToString(), x.Pago.Monto
            }));

        decimal TotalMetodo(MetodoPago metodo) => filas.Where(x => x.Pago.MetodoPago == metodo).Sum(x => x.Pago.Monto);
        return new DatosReporte("Pagos", PeriodoTexto(), FiltrosTexto("Pagos vigentes"),
            "Detalle de pagos", tabla, "Total cobrado (C$)", filas.Sum(x => x.Pago.Monto).ToString("N2"),
            "Efectivo (C$)", TotalMetodo(MetodoPago.Efectivo).ToString("N2"),
            "Tarjeta (C$)", TotalMetodo(MetodoPago.Tarjeta).ToString("N2"),
            "Transferencia (C$)", TotalMetodo(MetodoPago.Transferencia).ToString("N2"));
    }

    private async Task<DatosReporte> CrearReporteInventarioAsync(string buscar, CancellationToken ct)
    {
        var repuestos = await Aplicacion.Repuestos.ConsultarAsync(soloActivos: true, ct: ct);
        var filas = repuestos
            .Where(x => Coincide(buscar, x.Nombre, x.Marca, x.NumeroParte))
            .OrderBy(x => x.Nombre)
            .ToList();

        var tabla = CrearTabla(
            ["Código", "Repuesto", "Marca", "Existencias", "Precio compra (C$)", "Precio venta (C$)"],
            filas.Select(x => new object?[]
            {
                $"R-{x.IdRepuesto:D5}", x.Nombre, x.Marca ?? "—", x.Stock, x.PrecioCompra, x.PrecioVenta
            }));

        int unidades = filas.Sum(x => x.Stock);
        int marcas = filas.Select(x => x.Marca).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase).Count();
        decimal valorInventario = filas.Sum(x => x.Stock * x.PrecioCompra);
        return new DatosReporte("Inventario", $"Fecha de corte: {DateTime.Today:dd/MM/yyyy}",
            "Repuestos activos · precios y existencias actuales", "Detalle de repuestos", tabla,
            "Valor de inventario (C$)", valorInventario.ToString("N2"),
            "Unidades", unidades.ToString("N0"), "Marcas", marcas.ToString("N0"),
            "Precio venta total (C$)", filas.Sum(x => x.Stock * x.PrecioVenta).ToString("N2"));
    }

    private static void PrepararGrilla(DataGridView grid, params DataGridViewTextBoxColumn[] columnas)
    {
        grid.AutoGenerateColumns = false;
        grid.Dock = DockStyle.Fill;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = grid.ColumnHeadersDefaultCellStyle.BackColor;
        grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
        grid.DefaultCellStyle.ForeColor = Color.Black;
        grid.DefaultCellStyle.SelectionForeColor = Color.Black;
        for (int i = 0; i < columnas.Length; i++)
            columnas[i].DataPropertyName = $"C{i}";
    }

    private void MostrarGrilla(int indice, bool hayDatos, DataTable datos)
    {
        var (grid, vacio) = indice switch
        {
            1 => (dgvPagos, panelVacioPagos),
            2 => (dgvInventario, panelVacioInventario),
            _ => (dgvOrdenes, panelVacioOrdenes)
        };
        grid.DataSource = datos;
        grid.Dock = hayDatos ? DockStyle.Fill : DockStyle.Top;
        if (!hayDatos) grid.Height = grid.ColumnHeadersHeight;
        vacio.Visible = !hayDatos;
        if (!hayDatos) vacio.BringToFront();
        else grid.BringToFront();
    }

    private static DataTable CrearTabla(string[] titulos, IEnumerable<object?[]> filas)
    {
        var tabla = new DataTable("Reporte");
        for (int i = 0; i < titulos.Length; i++)
        {
            tabla.Columns.Add($"C{i}", typeof(object));
            tabla.ExtendedProperties[$"Header{i}"] = titulos[i];
        }
        foreach (var fila in filas) tabla.Rows.Add(fila.Select(x => x ?? DBNull.Value).ToArray());
        return tabla;
    }

    private bool EnPeriodo(DateTime fecha) =>
        (!dtpDesde.Checked || fecha.Date >= dtpDesde.Value.Date)
        && (!dtpHasta.Checked || fecha.Date <= dtpHasta.Value.Date);

    private void ValidarPeriodo()
    {
        if (tabReportes.SelectedIndex == 2) return;
        if (dtpDesde.Checked && dtpHasta.Checked && dtpDesde.Value.Date > dtpHasta.Value.Date)
            throw new AlDia.Entity.Common.ValidacionException("La fecha inicial no puede ser posterior a la fecha final.");
    }

    private bool Coincide(string buscar, params string?[] valores) => string.IsNullOrWhiteSpace(buscar)
        || valores.Any(x => x?.Contains(buscar, StringComparison.CurrentCultureIgnoreCase) == true);

    private string PeriodoTexto() =>
        $"Período: {(dtpDesde.Checked ? dtpDesde.Value.ToString("dd/MM/yyyy") : "inicio")}" +
        $" — {(dtpHasta.Checked ? dtpHasta.Value.ToString("dd/MM/yyyy") : "sin límite")}";

    private string FiltrosTexto(string estado) => $"Búsqueda: {(string.IsNullOrWhiteSpace(txtBuscar.Text) ? "Todos" : txtBuscar.Text.Trim())} · Estado: {estado}";
}

internal sealed record DatosReporte(
    string Titulo, string Periodo, string Filtros, string TituloDetalle, DataTable Filas,
    string Indicador1, string Valor1, string Indicador2, string Valor2,
    string Indicador3, string Valor3, string Indicador4, string Valor4);
