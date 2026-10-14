using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Equipos;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Equipos;

public partial class FrmEquipos : FrmListadoBase
{
    private bool _preparandoFiltros;
    public FrmEquipos()
    {
        InitializeComponent();
        VincularColumna("colIdEquipo", nameof(EquipoEntity.IdEquipo));
        VincularColumna("colMarca", nameof(EquipoEntity.Marca));
        VincularColumna("colModelo", nameof(EquipoEntity.Modelo));
        VincularColumna("colNumeroSerie", nameof(EquipoEntity.NumeroSerie));
        VincularColumna("colEstado", nameof(EquipoEntity.EstadoNombre));
        VincularColumna("colCliente", "Cliente");
        VincularColumna("colTipoEquipo", "TipoEquipo");
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
        cboFiltroTipoEquipo.SelectedIndexChanged += async (_, _) => { if (!_preparandoFiltros) await RecargarAsync(1); };
    }

    public FrmEquipos(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador, RolUsuario.Recepcionista);
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador, RolUsuario.Recepcionista);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador, RolUsuario.Recepcionista);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        var resultado = await Aplicacion.Equipos.MostrarAsync(pagina, tamPagina, buscar, estado, idTipoEquipo: (cboFiltroTipoEquipo.SelectedItem as OpcionCombo)?.Id, ct: ct);
        
        var clientes = await Aplicacion.Clientes.ConsultarAsync(soloActivos: false, ct: ct);
        var tipos = await Aplicacion.TiposEquipo.ConsultarAsync(soloActivos: false, ct: ct);
        ct.ThrowIfCancellationRequested();
        if (cboFiltroTipoEquipo.Items.Count <= 1)
        {
            _preparandoFiltros = true;
            cboFiltroTipoEquipo.Items.Clear();
            cboFiltroTipoEquipo.Items.Add(new OpcionCombo(null, "Todos los tipos"));
            foreach (var tipo in tipos) cboFiltroTipoEquipo.Items.Add(new OpcionCombo(tipo.IdTipoEquipo, tipo.Nombre));
            cboFiltroTipoEquipo.SelectedIndex = 0;
            _preparandoFiltros = false;
        }
        var filas = resultado.Registros.Select(x => new FilaEquipo(x,
            clientes.FirstOrDefault(c => c.IdCliente == x.IdCliente)?.NombreCompleto ?? $"Cliente #{x.IdCliente}",
            tipos.FirstOrDefault(t => t.IdTipoEquipo == x.IdTipoEquipo)?.Nombre ?? $"Tipo #{x.IdTipoEquipo}"));
        MostrarPagina(new PaginacionEntity<FilaEquipo>(filas, resultado.TotalRegistros, resultado.PaginaActual, resultado.TamPagina), ct);
    }

    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroEquipo(Aplicacion);
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmRegistroEquipo(Aplicacion, id);
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<FilaEquipo>()?.Entidad?.IdEquipo;

    protected override async Task DesactivarAsync()
    {
        var entidad = ObtenerSeleccionado<FilaEquipo>()?.Entidad;
        if (entidad is null || !ConfirmarCambioEstado(entidad.Estado)) return;
        await Aplicacion.Equipos.CambiarEstadoAsync(entidad.IdEquipo, !entidad.Estado);
        await RecargarAsync();
    }

    private sealed record FilaEquipo(EquipoEntity Entidad, string Cliente, string TipoEquipo)
    {
        public int IdEquipo => Entidad.IdEquipo;
        public string Marca => Entidad.Marca;
        public string? Modelo => Entidad.Modelo;
        public string? NumeroSerie => Entidad.NumeroSerie;
        public string EstadoNombre => Entidad.EstadoNombre;
    }

}
