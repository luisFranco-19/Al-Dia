using System.Data;
using AlDia.Entity.Common;
using Microsoft.Data.SqlClient;
using AlDia.DAL.Common;
using AlDia.Entity.Equipos;
namespace AlDia.DAL.Equipos;
public sealed class TipoEquipoDal : ITipoEquipoDal
{
    private readonly EjecutorSql _sql;

    public TipoEquipoDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }

    public Task<int> GuardarAsync(int idActor, TipoEquipoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        return s.IdTipoEquipo == 0
            ? AgregarAsync(idActor, s, ct)
            : ActualizarAsync(idActor, s, ct);
    }

    public Task<int> AgregarAsync(int idActor, TipoEquipoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.IdTipoEquipo != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    public Task<int> ActualizarAsync(int idActor, TipoEquipoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        Validacion.Id(s.IdTipoEquipo, "IdTipoEquipo");
        return GuardarRegistroAsync(idActor, s, ct);
    }

    private async Task<int> GuardarRegistroAsync(int idActor, TipoEquipoSolicitud s, CancellationToken ct)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_GuardarTipoEquipo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.EntradaSalida("@IdTipoEquipo", s.IdTipoEquipo));
            cmd.Parameters.Add(ParametrosSql.Texto("@Nombre", s.Nombre, 50));
            cmd.Parameters.Add(ParametrosSql.Texto("@Descripcion", s.Descripcion, 200));
            cmd.Parameters.Add(ParametrosSql.Bit("@Estado", s.Estado));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdTipoEquipo"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<IReadOnlyList<TipoEquipoEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarTiposEquipo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdTipoEquipo", id));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));

            await cn.OpenAsync(ct);
            List<TipoEquipoEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }

    private static TipoEquipoEntity Mapear(SqlDataReader r)
    {
        TipoEquipoEntity entidad = new();
        entidad.AsignarId(r.Entero("IdTipoEquipo"));
        entidad.CambiarNombre(r.Texto("Nombre"));
        entidad.CambiarDescripcion(r.TextoOpcional("Descripcion"));
        entidad.CambiarEstado(r.Bit("Estado"));
        return entidad;
    }
    public async Task<PaginacionEntity<TipoEquipoEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarTiposEquipo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            List<TipoEquipoEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<TipoEquipoEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
