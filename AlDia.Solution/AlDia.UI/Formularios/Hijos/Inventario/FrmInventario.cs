using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Inventario;
using AlDia.Entity.Seguridad;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Inventario;

public partial class FrmInventario : FrmListadoBase
{
    public FrmInventario()
    {
        InitializeComponent();
        VincularColumna("colNombre", nameof(RepuestoEntity.Nombre));
        VincularColumna("colNumeroParte", nameof(RepuestoEntity.NumeroParte));
        VincularColumna("colMarca", nameof(RepuestoEntity.Marca));
        VincularColumna("colStock", nameof(RepuestoEntity.Stock));
        VincularColumna("colCompra", nameof(RepuestoEntity.PrecioCompra));
        VincularColumna("colVenta", nameof(RepuestoEntity.PrecioVenta));
        VincularColumna("colEstado", nameof(RepuestoEntity.EstadoNombre));
        cboFiltroEstado.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
        cboFiltroExistencias.SelectedIndexChanged += async (_, _) => await RecargarAsync(1);
    }

    public FrmInventario(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    protected override bool MostrarBotonPrincipal => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonSecundario => PuedeMantener(RolUsuario.Administrador);
    protected override bool MostrarBotonTerciario => PuedeMantener(RolUsuario.Administrador);

    protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
    {
        var ct = TokenCarga;
        bool? estado = cboFiltroEstado.SelectedIndex <= 0 ? null : cboFiltroEstado.SelectedIndex == 1;
        var resultado = await Aplicacion.Repuestos.MostrarAsync(pagina, tamPagina, buscar, estado, conExistencias: cboFiltroExistencias.SelectedIndex == 0 ? null : cboFiltroExistencias.SelectedIndex == 1, ct: ct);
        MostrarPagina(resultado, ct);
    }

    protected override Form CrearFormularioAccionPrincipal() => new FrmRegistroRepuesto(Aplicacion);
    protected override Form CrearFormularioAccionSecundaria(int id) => new FrmRegistroRepuesto(Aplicacion, id);
    protected override int? ObtenerIdSeleccionado() => ObtenerSeleccionado<RepuestoEntity>()?.IdRepuesto;

    protected override async Task DesactivarAsync()
    {
        var entidad = ObtenerSeleccionado<RepuestoEntity>();
        if (entidad is null || !ConfirmarCambioEstado(entidad.Estado)) return;
        await Aplicacion.Repuestos.CambiarEstadoAsync(entidad.IdRepuesto, !entidad.Estado);
        await RecargarAsync();
    }

}
