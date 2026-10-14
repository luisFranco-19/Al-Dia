using System.Data;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Reparaciones;
namespace AlDia.DAL.Reparaciones;
public sealed class OrdenReparacionDal : IOrdenReparacionDal
{
    private readonly EjecutorSql _sql;

    public OrdenReparacionDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public async Task<int> RegistrarRecepcionAsync(int idActor, RecepcionSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RegistrarRecepcion", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Texto("@NumeroOrden", s.NumeroOrden, 20));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdEquipo", s.IdEquipo));
            cmd.Parameters.Add(ParametrosSql.Texto("@ProblemaReportado", s.ProblemaReportado, 500));
            cmd.Parameters.Add(ParametrosSql.Texto("@ObservacionesRecepcion", s.ObservacionesRecepcion, 500));
            cmd.Parameters.Add(ParametrosSql.Texto("@AccesoriosRecepcion", s.AccesoriosRecepcion, 300));
            cmd.Parameters.Add(ParametrosSql.Salida("@IdOrden"));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdOrden"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task CorregirRecepcionAsync(int idActor, CorreccionRecepcionSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_CorregirRecepcion", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", s.IdOrden));
            cmd.Parameters.Add(ParametrosSql.Texto("@ProblemaReportado", s.ProblemaReportado, 500));
            cmd.Parameters.Add(ParametrosSql.Texto("@ObservacionesRecepcion", s.ObservacionesRecepcion, 500));
            cmd.Parameters.Add(ParametrosSql.Texto("@AccesoriosRecepcion", s.AccesoriosRecepcion, 300));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task AnularAsync(int idActor, int idOrden, string motivo, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_AnularOrden", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));
            cmd.Parameters.Add(ParametrosSql.Texto("@Motivo", motivo, 500));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task TomarAsync(int idActor, int idOrden, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_TomarOrden", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task LiberarAsync(int idActor, int idOrden, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_LiberarOrden", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task IniciarReparacionAsync(int idActor, int idOrden, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_IniciarReparacion", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task FinalizarReparacionAsync(int idActor, int idOrden, bool reparada, string observaciones, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_FinalizarReparacion", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));
            cmd.Parameters.Add(ParametrosSql.Bit("@Reparada", reparada));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", observaciones, 500));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task EntregarAsync(int idActor, int idOrden, string? observacion, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_EntregarEquipo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observacion", observacion, 500));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    private static OrdenReparacionEntity MapearOrden(Microsoft.Data.SqlClient.SqlDataReader r) => new(
        r.Entero("IdOrden"), r.Texto("NumeroOrden"), r.Entero("IdEquipo"), r.Entero("IdRecepcionista"), r.Entero("IdEstado"),
        r.Texto("EstadoNombre"), r.EnteroOpcional("IdTecnicoResponsable"), r.Fecha("FechaRecepcion"), r.Texto("ProblemaReportado"),
        r.TextoOpcional("ObservacionesRecepcion"), r.TextoOpcional("AccesoriosRecepcion"), r.FechaOpcional("FechaEntrega"), r.Bit("Anulada"),
        r.Dinero("Total"), r.Dinero("Pagado"), r.Dinero("Saldo"), r.Entero("IdCliente"), r.Texto("NombreCliente"), r.Texto("ApellidoCliente"));
    public async Task<IReadOnlyList<OrdenReparacionEntity>> ConsultarAsync(int idActor, FiltroOrdenes f, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarOrdenes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", f.IdOrden));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdEstado", f.IdEstado));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdCliente", f.IdCliente));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloDisponibles", f.SoloDisponibles));
            cmd.Parameters.Add(ParametrosSql.Bit("@IncluirAnuladas", f.IncluirAnuladas));

            await cn.OpenAsync(ct);
            List<OrdenReparacionEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(MapearOrden(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<IReadOnlyList<EstadoReparacionEntity>> ConsultarEstadosAsync(int idActor, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarEstadosReparacion", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));

            await cn.OpenAsync(ct);
            Func<SqlDataReader, EstadoReparacionEntity> mapear = r => new EstadoReparacionEntity(r.Entero("IdEstado"), r.Texto("Nombre"), r.TextoOpcional("Descripcion"));
            List<EstadoReparacionEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(mapear(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    private static async Task<IReadOnlyList<T>> LeerListaAsync<T>(Microsoft.Data.SqlClient.SqlDataReader r,
        Func<Microsoft.Data.SqlClient.SqlDataReader, T> mapear, CancellationToken ct)
    {
        var lista = new List<T>();
        while (await r.ReadAsync(ct)) lista.Add(mapear(r));
        return lista.AsReadOnly();
    }
    public async Task<ExpedienteOrdenEntity?> ConsultarExpedienteAsync(int idActor, int idOrden, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarExpedienteOrden", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", idOrden));

            await cn.OpenAsync(ct);
            using var r = await cmd.ExecuteReaderAsync(ct);
            if (!await r.ReadAsync(ct)) return null;
            var orden = MapearOrden(r);
            await r.NextResultAsync(ct);
            var diagnosticos = await LeerListaAsync(r, x => new DiagnosticoEntity(x.Entero("IdDiagnostico"), x.Entero("IdOrden"), x.Entero("IdTecnico"),
                x.Fecha("FechaDiagnostico"), x.Texto("ProblemaEncontrado"), x.Texto("ReparacionPropuesta"), x.Dinero("CostoEstimado"), x.TextoOpcional("Observaciones")), ct);
            await r.NextResultAsync(ct);
            var confirmaciones = await LeerListaAsync(r, x => new ConfirmacionEntity(x.Entero("IdConfirmacion"), x.Entero("IdDiagnostico"), x.Entero("IdUsuario"),
                Enum.Parse<DecisionCliente>(x.Texto("Decision"), true), x.DineroOpcional("CostoAprobado"), x.Fecha("FechaConfirmacion"), x.TextoOpcional("Observaciones")), ct);
            await r.NextResultAsync(ct);
            var servicios = await LeerListaAsync(r, x => new DetalleServicioEntity(x.Entero("IdDetalleServicio"), x.Entero("IdOrden"), x.Entero("IdServicio"),
                x.Entero("Cantidad"), x.Dinero("Precio"), x.TextoOpcional("Observaciones"), x.EnteroOpcional("IdTecnico")), ct);
            await r.NextResultAsync(ct);
            var repuestos = await LeerListaAsync(r, x => new DetalleRepuestoEntity(x.Entero("IdDetalleRepuesto"), x.Entero("IdOrden"), x.Entero("IdRepuesto"),
                x.Entero("Cantidad"), x.Dinero("Precio"), x.EnteroOpcional("IdTecnico")), ct);
            await r.NextResultAsync(ct);
            var pagos = await LeerListaAsync(r, AlDia.DAL.Pagos.PagoDal.Mapear, ct);
            await r.NextResultAsync(ct);
            var historial = await LeerListaAsync(r, x => new HistorialEstadoEntity(x.Entero("IdHistorial"), x.Entero("IdOrden"), x.EnteroOpcional("IdEstadoAnterior"),
                x.Entero("IdEstadoNuevo"), x.Entero("IdUsuario"), x.Fecha("FechaCambio"), x.TextoOpcional("Observacion")), ct);
            return new ExpedienteOrdenEntity(orden, diagnosticos.SingleOrDefault(), confirmaciones.SingleOrDefault(), servicios, repuestos, pagos, historial);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    public async Task<PaginacionEntity<OrdenReparacionEntity>> ConsultarPaginaAsync(int idActor, FiltroOrdenes f,
        int pagina, int tamPagina, string? buscar, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarOrdenes", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", f.IdOrden));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdEstado", f.IdEstado));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdCliente", f.IdCliente));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloDisponibles", f.SoloDisponibles));
            cmd.Parameters.Add(ParametrosSql.Bit("@IncluirAnuladas", f.IncluirAnuladas));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            List<OrdenReparacionEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(MapearOrden(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<OrdenReparacionEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

}
