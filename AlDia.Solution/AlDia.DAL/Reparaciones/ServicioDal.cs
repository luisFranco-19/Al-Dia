using AlDia.DAL.Common;
using AlDia.Entity.Reparaciones;
namespace AlDia.DAL.Reparaciones;
public sealed class ServicioDal(EjecutorSql sql) : IServicioDal
{
    public Task<int> GuardarAsync(int idActor, ServicioSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarServicio", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdServicio", s.IdServicio), ParametrosSql.Texto("@Nombre", s.Nombre, 100), ParametrosSql.Texto("@Descripcion", s.Descripcion, 300), ParametrosSql.Dinero("@PrecioBase", s.PrecioBase), ParametrosSql.Bit("@Estado", s.Estado)], "@IdServicio", idActor, ct);
    public Task<IReadOnlyList<ServicioEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarServicios", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdServicio", id), ParametrosSql.Bit("@SoloActivos", soloActivos)],
            reader => new ServicioEntity(reader.GetInt32(reader.GetOrdinal("IdServicio")), reader.GetString(reader.GetOrdinal("Nombre")), reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion")), reader.GetDecimal(reader.GetOrdinal("PrecioBase")), reader.GetBoolean(reader.GetOrdinal("Estado"))), idActor, ct);

    public Task<int> RegistrarEnOrdenAsync(int idActor, ServicioOrdenSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RegistrarServicio", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", s.IdOrden), ParametrosSql.Entero("@IdServicio", s.IdServicio), ParametrosSql.Entero("@Cantidad", s.Cantidad), ParametrosSql.Dinero("@Precio", s.Precio), ParametrosSql.Texto("@Observaciones", s.Observaciones, 500), ParametrosSql.Salida("@IdDetalleServicio")], "@IdDetalleServicio", idActor, ct);
    public Task CorregirEnOrdenAsync(int idActor, CorreccionServicioOrdenSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CorregirServicio", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDetalleServicio", s.IdDetalleServicio), ParametrosSql.Entero("@Cantidad", s.Cantidad), ParametrosSql.Dinero("@Precio", s.Precio), ParametrosSql.Texto("@Observaciones", s.Observaciones, 500)], null, idActor, ct);
    public Task RetirarDeOrdenAsync(int idActor, int idDetalleServicio, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RetirarServicio", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdDetalleServicio", idDetalleServicio)], null, idActor, ct);
}
