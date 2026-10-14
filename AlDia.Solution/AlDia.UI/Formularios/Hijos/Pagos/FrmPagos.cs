using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Pagos;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;

namespace AlDia.UI.Formularios.Hijos.Pagos;

public partial class FrmPagos : FrmListadoBase
{
    public FrmPagos()
    {
        InitializeComponent();
        foreach (string propiedad in new[] { "NumeroPago", "NumeroOrden", "Cliente", "FechaPago", "Monto", "Metodo", "Usuario", "Estado" })
            VincularColumna("col" + propiedad, propiedad);
        cboFiltroMetodo.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
    }
    public FrmPagos(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Recepcionista);
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Recepcionista);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Recepcionista);
    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        MetodoPago? metodo = cboFiltroMetodo.SelectedIndex > 0 ? Enum.Parse<MetodoPago>(cboFiltroMetodo.Text) : null;
        bool? anulado = cboFiltroEstado.SelectedIndex > 0 ? cboFiltroEstado.SelectedIndex == 2 : null;
        var resultado = await Aplicacion.Pagos.MostrarAsync(pagina, tamPagina, buscar, metodo, anulado, ct);
        var ordenes = await Aplicacion.Ordenes.ConsultarAsync(new(IncluirAnuladas: true), ct);
        var filas = resultado.Registros.Select(x => new FilaPago(x,
            ordenes.FirstOrDefault(o => o.IdOrden == x.IdOrden)?.NumeroOrden ?? $"Orden #{x.IdOrden}",
            ordenes.FirstOrDefault(o => o.IdOrden == x.IdOrden)?.NombreCompletoCliente ?? ""));
        MostrarPagina(new PaginacionEntity<FilaPago>(filas, resultado.TotalRegistros, resultado.PaginaActual, resultado.TamPagina), ct);
    }
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<FilaPago>()?.Entidad.IdPago;
    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroPago(Aplicacion);
    protected override Form? CrearFormularioAccionSecundaria(int id)
    {
        if (ObtenerSeleccionado<FilaPago>()?.Entidad.Anulado != false) return null;
        using var motivo = new FrmSolicitudMotivo(Aplicacion, "Corregir pago");
        return MostrarModal(motivo) == DialogResult.OK ? new FrmRegistroPago(Aplicacion, id, motivo.Motivo) : null;
    }
    protected override async Task DesactivarAsync()
    {
        var pago = ObtenerSeleccionado<FilaPago>()?.Entidad;
        if (pago is null || pago.Anulado) return;
        using var motivo = new FrmSolicitudMotivo(Aplicacion, "Anular pago");
        if (MostrarModal(motivo) != DialogResult.OK) return;
        await Aplicacion.Pagos.AnularEntidadAsync(pago, motivo.Motivo);
        await RecargarAsync();
    }
    private sealed record FilaPago(PagoEntity Entidad, string NumeroOrden, string Cliente)
    {
        public int NumeroPago => Entidad.IdPago;
        public DateTime FechaPago => Entidad.FechaPago;
        public decimal Monto => Entidad.Monto;
        public string Metodo => Entidad.MetodoPago.ToString();
        public string Usuario => $"Usuario #{Entidad.IdUsuario}";
        public string Estado => Entidad.Anulado ? "Anulado" : "Vigente";
    }
}
