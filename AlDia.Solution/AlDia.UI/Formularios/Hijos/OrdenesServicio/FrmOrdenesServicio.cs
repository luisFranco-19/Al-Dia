using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Reparaciones;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.OrdenesServicio;

public partial class FrmOrdenesServicio : FrmListadoBase
{
    private bool _preparandoFiltros;
    public FrmOrdenesServicio()
    {
        InitializeComponent();
        foreach (string propiedad in new[] { "NumeroOrden", "Cliente", "Equipo", "FechaRecepcion", "Tecnico", "Estado", "Total", "Saldo" })
            VincularColumna("col" + propiedad, propiedad);
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => { if (!_preparandoFiltros) await RecargarAsync(1); };
        cboFiltroAsignacion.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
    }
    public FrmOrdenesServicio(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Recepcionista);
    protected override bool MostrarBotonSecundario => true;
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Recepcionista);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        if (cboFiltroEstado.SelectedItem is not OpcionCombo)
        {
            var estados = await Aplicacion.Ordenes.ConsultarEstadosAsync(ct);
            ct.ThrowIfCancellationRequested();
            _preparandoFiltros = true;
            cboFiltroEstado.Items.Clear();
            cboFiltroEstado.Items.Add(new OpcionCombo(null, "Todos los estados"));
            foreach (var estado in estados) cboFiltroEstado.Items.Add(new OpcionCombo(estado.IdEstado, estado.Nombre));
            cboFiltroEstado.SelectedIndex = 0;
            _preparandoFiltros = false;
        }
        bool? asignado = cboFiltroAsignacion.SelectedIndex is 1 or 2 ? cboFiltroAsignacion.SelectedIndex == 2 : null;
        var resultado = await Aplicacion.Ordenes.MostrarAsync(pagina, tamPagina, buscar,
            (cboFiltroEstado.SelectedItem as OpcionCombo)?.Id, asignado, cboFiltroAsignacion.SelectedIndex == 3, ct);
        var equipos = await Aplicacion.Equipos.ConsultarAsync(soloActivos: false, ct: ct);
        var filas = resultado.Registros.Select(x => new FilaOrden(x,
            equipos.FirstOrDefault(eq => eq.IdEquipo == x.IdEquipo) is { } equipo
                ? $"{equipo.Marca} {equipo.Modelo}".Trim() : $"Equipo #{x.IdEquipo}"));
        MostrarPagina(new PaginacionEntity<FilaOrden>(filas, resultado.TotalRegistros, resultado.PaginaActual, resultado.TamPagina), ct);
    }
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<FilaOrden>()?.Entidad.IdOrden;
    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroOrdenServicio(Aplicacion);
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmDetalleOrdenServicio(Aplicacion, id);
    protected override async Task DesactivarAsync()
    {
        var orden = ObtenerSeleccionado<FilaOrden>()?.Entidad;
        if (orden is null || orden.Anulada) return;
        using var motivo = new FrmSolicitudMotivo(Aplicacion, "Anular orden");
        if (MostrarModal(motivo) != DialogResult.OK) return;
        await Aplicacion.Ordenes.AnularAsync(orden.IdOrden, motivo.Motivo);
        await RecargarAsync();
    }
    private sealed record FilaOrden(OrdenReparacionEntity Entidad, string Equipo)
    {
        public string NumeroOrden => Entidad.NumeroOrden;
        public string Cliente => Entidad.NombreCompletoCliente;
        public DateTime FechaRecepcion => Entidad.FechaRecepcion;
        public string Tecnico => Entidad.IdTecnicoResponsable.HasValue ? $"Técnico #{Entidad.IdTecnicoResponsable}" : "Sin asignar";
        public string Estado => Entidad.EstadoNombre;
        public decimal Total => Entidad.Total;
        public decimal Saldo => Entidad.Saldo;
    }
}
