using AlDia.DAL.Common;
using AlDia.Entity.Reparaciones;
namespace AlDia.DAL.Reparaciones;
public sealed class OrdenReparacionDal(EjecutorSql sql) : IOrdenReparacionDal
{
    public Task<int> RegistrarRecepcionAsync(int idActor, RecepcionSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_RegistrarRecepcion", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Texto("@NumeroOrden", s.NumeroOrden, 20), ParametrosSql.Entero("@IdEquipo", s.IdEquipo), ParametrosSql.Texto("@ProblemaReportado", s.ProblemaReportado, 500), ParametrosSql.Texto("@ObservacionesRecepcion", s.ObservacionesRecepcion, 500), ParametrosSql.Texto("@AccesoriosRecepcion", s.AccesoriosRecepcion, 300), ParametrosSql.Salida("@IdOrden")], "@IdOrden", idActor, ct);
    public Task CorregirRecepcionAsync(int idActor, CorreccionRecepcionSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CorregirRecepcion", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", s.IdOrden), ParametrosSql.Texto("@ProblemaReportado", s.ProblemaReportado, 500), ParametrosSql.Texto("@ObservacionesRecepcion", s.ObservacionesRecepcion, 500), ParametrosSql.Texto("@AccesoriosRecepcion", s.AccesoriosRecepcion, 300)], null, idActor, ct);
    public Task AnularAsync(int idActor, int idOrden, string motivo, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_AnularOrden", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", idOrden), ParametrosSql.Texto("@Motivo", motivo, 500)], null, idActor, ct);
    public Task TomarAsync(int idActor, int idOrden, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_TomarOrden", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", idOrden)], null, idActor, ct);
    public Task LiberarAsync(int idActor, int idOrden, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_LiberarOrden", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", idOrden)], null, idActor, ct);
    public Task IniciarReparacionAsync(int idActor, int idOrden, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_IniciarReparacion", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", idOrden)], null, idActor, ct);
    public Task FinalizarReparacionAsync(int idActor, int idOrden, bool reparada, string observaciones, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_FinalizarReparacion", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", idOrden), ParametrosSql.Bit("@Reparada", reparada), ParametrosSql.Texto("@Observaciones", observaciones, 500)], null, idActor, ct);
    public Task EntregarAsync(int idActor, int idOrden, string? observacion, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_EntregarEquipo", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", idOrden), ParametrosSql.Texto("@Observacion", observacion, 500)], null, idActor, ct);

    private static OrdenReparacionEntity MapearOrden(Microsoft.Data.SqlClient.SqlDataReader r) => new(
        r.Entero("IdOrden"), r.Texto("NumeroOrden"), r.Entero("IdEquipo"), r.Entero("IdRecepcionista"), r.Entero("IdEstado"),
        r.Texto("EstadoNombre"), r.EnteroOpcional("IdTecnicoResponsable"), r.Fecha("FechaRecepcion"), r.Texto("ProblemaReportado"),
        r.TextoOpcional("ObservacionesRecepcion"), r.TextoOpcional("AccesoriosRecepcion"), r.FechaOpcional("FechaEntrega"), r.Bit("Anulada"),
        r.Dinero("Total"), r.Dinero("Pagado"), r.Dinero("Saldo"), r.Entero("IdCliente"), r.Texto("NombreCliente"), r.Texto("ApellidoCliente"));
    public Task<IReadOnlyList<OrdenReparacionEntity>> ConsultarAsync(int idActor, FiltroOrdenes f, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarOrdenes", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", f.IdOrden),
            ParametrosSql.Entero("@IdEstado", f.IdEstado), ParametrosSql.Entero("@IdCliente", f.IdCliente),
            ParametrosSql.Bit("@SoloDisponibles", f.SoloDisponibles), ParametrosSql.Bit("@IncluirAnuladas", f.IncluirAnuladas)], MapearOrden, idActor, ct);
    public Task<IReadOnlyList<EstadoReparacionEntity>> ConsultarEstadosAsync(int idActor, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarEstadosReparacion", [ParametrosSql.Entero("@IdUsuarioActor", idActor)],
            r => new EstadoReparacionEntity(r.Entero("IdEstado"), r.Texto("Nombre"), r.TextoOpcional("Descripcion")), idActor, ct);
    private static async Task<IReadOnlyList<T>> LeerListaAsync<T>(Microsoft.Data.SqlClient.SqlDataReader r,
        Func<Microsoft.Data.SqlClient.SqlDataReader, T> mapear, CancellationToken ct)
    {
        var lista = new List<T>();
        while (await r.ReadAsync(ct)) lista.Add(mapear(r));
        return lista.AsReadOnly();
    }
    public Task<ExpedienteOrdenEntity?> ConsultarExpedienteAsync(int idActor, int idOrden, CancellationToken ct = default) =>
        sql.EjecutarAsync<ExpedienteOrdenEntity?>("dbo.usp_ConsultarExpedienteOrden",
            [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdOrden", idOrden)], async (cmd, token) =>
        {
            using var r = await cmd.ExecuteReaderAsync(token);
            if (!await r.ReadAsync(token)) return null;
            var orden = MapearOrden(r);
            await r.NextResultAsync(token);
            var diagnosticos = await LeerListaAsync(r, x => new DiagnosticoEntity(x.Entero("IdDiagnostico"), x.Entero("IdOrden"), x.Entero("IdTecnico"),
                x.Fecha("FechaDiagnostico"), x.Texto("ProblemaEncontrado"), x.Texto("ReparacionPropuesta"), x.Dinero("CostoEstimado"), x.TextoOpcional("Observaciones")), token);
            await r.NextResultAsync(token);
            var confirmaciones = await LeerListaAsync(r, x => new ConfirmacionEntity(x.Entero("IdConfirmacion"), x.Entero("IdDiagnostico"), x.Entero("IdUsuario"),
                Enum.Parse<DecisionCliente>(x.Texto("Decision"), true), x.DineroOpcional("CostoAprobado"), x.Fecha("FechaConfirmacion"), x.TextoOpcional("Observaciones")), token);
            await r.NextResultAsync(token);
            var servicios = await LeerListaAsync(r, x => new DetalleServicioEntity(x.Entero("IdDetalleServicio"), x.Entero("IdOrden"), x.Entero("IdServicio"),
                x.Entero("IdTecnico"), x.Entero("Cantidad"), x.Dinero("Precio"), x.TextoOpcional("Observaciones")), token);
            await r.NextResultAsync(token);
            var repuestos = await LeerListaAsync(r, x => new DetalleRepuestoEntity(x.Entero("IdDetalleRepuesto"), x.Entero("IdOrden"), x.Entero("IdRepuesto"),
                x.Entero("IdTecnico"), x.Entero("Cantidad"), x.Dinero("Precio")), token);
            await r.NextResultAsync(token);
            var pagos = await LeerListaAsync(r, x => new AlDia.Entity.Pagos.PagoEntity(x.Entero("IdPago"), x.Entero("IdOrden"), x.Entero("IdUsuario"),
                x.Dinero("Monto"), Enum.Parse<AlDia.Entity.Pagos.MetodoPago>(x.Texto("MetodoPago")), x.Fecha("FechaPago"), x.TextoOpcional("Observaciones"),
                x.Bit("Anulado"), x.FechaOpcional("FechaAnulacion"), x.EnteroOpcional("IdUsuarioAnulacion"), x.TextoOpcional("MotivoAnulacion")), token);
            await r.NextResultAsync(token);
            var historial = await LeerListaAsync(r, x => new HistorialEstadoEntity(x.Entero("IdHistorial"), x.Entero("IdOrden"), x.EnteroOpcional("IdEstadoAnterior"),
                x.Entero("IdEstadoNuevo"), x.Entero("IdUsuario"), x.Fecha("FechaCambio"), x.TextoOpcional("Observacion")), token);
            return new ExpedienteOrdenEntity(orden, diagnosticos.SingleOrDefault(), confirmaciones.SingleOrDefault(), servicios, repuestos, pagos, historial);
        }, idActor, ct);

}
