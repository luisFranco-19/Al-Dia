using System.Data;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Reparaciones;
namespace AlDia.DAL.Reparaciones;
public sealed class DiagnosticoDal : IDiagnosticoDal
{
    private readonly EjecutorSql _sql;

    public DiagnosticoDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public async Task<int> RegistrarAsync(int idActor, DiagnosticoSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RegistrarDiagnostico", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdOrden", s.IdOrden));
            cmd.Parameters.Add(ParametrosSql.Texto("@ProblemaEncontrado", s.ProblemaEncontrado, 1000));
            cmd.Parameters.Add(ParametrosSql.Texto("@ReparacionPropuesta", s.ReparacionPropuesta, 1000));
            cmd.Parameters.Add(ParametrosSql.Dinero("@CostoEstimado", s.CostoEstimado));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", s.Observaciones, 1000));
            cmd.Parameters.Add(ParametrosSql.Salida("@IdDiagnostico"));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdDiagnostico"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task CorregirAsync(int idActor, CorreccionDiagnosticoSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_CorregirDiagnostico", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdDiagnostico", s.IdDiagnostico));
            cmd.Parameters.Add(ParametrosSql.Texto("@ProblemaEncontrado", s.ProblemaEncontrado, 1000));
            cmd.Parameters.Add(ParametrosSql.Texto("@ReparacionPropuesta", s.ReparacionPropuesta, 1000));
            cmd.Parameters.Add(ParametrosSql.Dinero("@CostoEstimado", s.CostoEstimado));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", s.Observaciones, 1000));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task RetirarAsync(int idActor, int idDiagnostico, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RetirarDiagnostico", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdDiagnostico", idDiagnostico));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<int> RegistrarDecisionAsync(int idActor, DecisionClienteSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RegistrarDecisionCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdDiagnostico", s.IdDiagnostico));
            cmd.Parameters.Add(ParametrosSql.Texto("@Decision", s.Decision.ToString().ToUpperInvariant(), 20));
            cmd.Parameters.Add(ParametrosSql.Dinero("@CostoAprobado", s.CostoAprobado));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", s.Observaciones, 500));
            cmd.Parameters.Add(ParametrosSql.Salida("@IdConfirmacion"));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdConfirmacion"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task CorregirDecisionAsync(int idActor, CorreccionDecisionClienteSolicitud s, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_CorregirDecisionCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdConfirmacion", s.IdConfirmacion));
            cmd.Parameters.Add(ParametrosSql.Texto("@Decision", s.Decision.ToString().ToUpperInvariant(), 20));
            cmd.Parameters.Add(ParametrosSql.Dinero("@CostoAprobado", s.CostoAprobado));
            cmd.Parameters.Add(ParametrosSql.Texto("@Observaciones", s.Observaciones, 500));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task RetirarDecisionAsync(int idActor, int idConfirmacion, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_RetirarDecisionCliente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdConfirmacion", idConfirmacion));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
