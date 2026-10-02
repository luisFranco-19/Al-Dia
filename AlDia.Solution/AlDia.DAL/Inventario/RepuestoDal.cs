using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Inventario;
namespace AlDia.DAL.Inventario;
public sealed class RepuestoDal(EjecutorSql sql) : IRepuestoDal
{
    public Task<int> GuardarAsync(int idActor, RepuestoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarRepuesto", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdRepuesto", s.IdRepuesto), ParametrosSql.Texto("@Nombre", s.Nombre, 100), ParametrosSql.Texto("@Descripcion", s.Descripcion, 300), ParametrosSql.Texto("@Marca", s.Marca, 50), ParametrosSql.Texto("@NumeroParte", s.NumeroParte, 100), ParametrosSql.Dinero("@PrecioCompra", s.PrecioCompra), ParametrosSql.Dinero("@PrecioVenta", s.PrecioVenta), ParametrosSql.Bit("@Estado", s.Estado)], "@IdRepuesto", idActor, ct);
    public Task<IReadOnlyList<RepuestoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarRepuestos", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdRepuesto", id), ParametrosSql.Bit("@SoloActivos", soloActivos)],
            Mapear, idActor, ct);

    public Task<int> ConsumirAsync(int idActor, ConsumoRepuestoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_ConsumirRepuesto", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", s.IdOrden), ParametrosSql.Entero("@IdRepuesto", s.IdRepuesto), ParametrosSql.Entero("@Cantidad", s.Cantidad), ParametrosSql.Dinero("@Precio", s.Precio), ParametrosSql.Salida("@IdDetalleRepuesto")], "@IdDetalleRepuesto", idActor, ct);
    public Task CorregirConsumoAsync(int idActor, CorreccionConsumoRepuestoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CorregirConsumoRepuesto", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDetalleRepuesto", s.IdDetalleRepuesto), ParametrosSql.Entero("@Cantidad", s.Cantidad), ParametrosSql.Dinero("@Precio", s.Precio)], null, idActor, ct);
    public Task RetirarConsumoAsync(int idActor, int idDetalleRepuesto, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RetirarConsumoRepuesto", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDetalleRepuesto", idDetalleRepuesto)], null, idActor, ct);
    public Task AjustarExistenciasAsync(int idActor, int idRepuesto, int variacion, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_AjustarExistencias", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdRepuesto", idRepuesto), ParametrosSql.Entero("@Variacion", variacion)], null, idActor, ct);

    private static RepuestoEntity Mapear(SqlDataReader r) => new(
        r.Entero("IdRepuesto"), r.Texto("Nombre"), r.TextoOpcional("Descripcion"), r.TextoOpcional("Marca"),
        r.TextoOpcional("NumeroParte"), r.Dinero("PrecioCompra"), r.Dinero("PrecioVenta"), r.Bit("Estado"),
        r.Entero("Stock"));
    public Task<PaginacionEntity<RepuestoEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default) =>
        sql.ConsultarPaginaAsync("dbo.usp_ConsultarRepuestos", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Bit("@SoloActivos", soloActivos)], Mapear, idActor, pagina, tamPagina, buscar, ct);
}
