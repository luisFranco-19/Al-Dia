using AlDia.DAL.Common;
using AlDia.Entity.Inventario;
namespace AlDia.DAL.Inventario;
public sealed class RepuestoDal(EjecutorSql sql) : IRepuestoDal
{
    public Task<int> GuardarAsync(int idActor, RepuestoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarRepuesto", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdRepuesto", s.IdRepuesto), ParametrosSql.Texto("@Nombre", s.Nombre, 100), ParametrosSql.Texto("@Descripcion", s.Descripcion, 300), ParametrosSql.Texto("@Marca", s.Marca, 50), ParametrosSql.Texto("@NumeroParte", s.NumeroParte, 100), ParametrosSql.Dinero("@PrecioCompra", s.PrecioCompra), ParametrosSql.Dinero("@PrecioVenta", s.PrecioVenta), ParametrosSql.Bit("@Estado", s.Estado)], "@IdRepuesto", idActor, ct);
    public Task<IReadOnlyList<RepuestoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarRepuestos", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdRepuesto", id), ParametrosSql.Bit("@SoloActivos", soloActivos)],
            reader => new RepuestoEntity(reader.GetInt32(reader.GetOrdinal("IdRepuesto")), reader.GetString(reader.GetOrdinal("Nombre")), reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")), reader.IsDBNull(reader.GetOrdinal("Marca")) ? null : reader.GetString(reader.GetOrdinal("Marca")), reader.IsDBNull(reader.GetOrdinal("NumeroParte")) ? null : reader.GetString(reader.GetOrdinal("NumeroParte")), reader.GetDecimal(reader.GetOrdinal("PrecioCompra")), reader.GetDecimal(reader.GetOrdinal("PrecioVenta")), reader.GetBoolean(reader.GetOrdinal("Estado")), reader.GetInt32(reader.GetOrdinal("Stock"))), idActor, ct);

    public Task<int> ConsumirAsync(int idActor, ConsumoRepuestoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_ConsumirRepuesto", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", s.IdOrden), ParametrosSql.Entero("@IdRepuesto", s.IdRepuesto), ParametrosSql.Entero("@Cantidad", s.Cantidad), ParametrosSql.Dinero("@Precio", s.Precio), ParametrosSql.Salida("@IdDetalleRepuesto")], "@IdDetalleRepuesto", idActor, ct);
    public Task CorregirConsumoAsync(int idActor, CorreccionConsumoRepuestoSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CorregirConsumoRepuesto", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDetalleRepuesto", s.IdDetalleRepuesto), ParametrosSql.Entero("@Cantidad", s.Cantidad), ParametrosSql.Dinero("@Precio", s.Precio)], null, idActor, ct);
    public Task RetirarConsumoAsync(int idActor, int idDetalleRepuesto, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RetirarConsumoRepuesto", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDetalleRepuesto", idDetalleRepuesto)], null, idActor, ct);
    public Task AjustarExistenciasAsync(int idActor, int idRepuesto, int variacion, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_AjustarExistencias", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdRepuesto", idRepuesto), ParametrosSql.Entero("@Variacion", variacion)], null, idActor, ct);
}
