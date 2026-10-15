using AlDia.DAL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using Microsoft.Data.SqlClient;
using System.Data;
namespace AlDia.DAL.Seguridad;

public sealed class UsuarioDal : IUsuarioDal
{
    private readonly EjecutorSql _sql;

    public UsuarioDal(EjecutorSql sql)
    {
        _sql = sql ?? throw new ArgumentNullException(nameof(sql));
    }
    // Convierte una fila de SQL en un usuario.
    internal static UsuarioEntity Mapear(SqlDataReader r)
    {
        UsuarioEntity entidad = new();
        entidad.AsignarId(r.Entero("IdUsuario"));
        entidad.CambiarNombre(r.Texto("Nombre"));
        entidad.CambiarApellido(r.Texto("Apellido"));
        entidad.CambiarCedula(r.Texto("Cedula"));
        entidad.CambiarTelefono(r.TextoOpcional("Telefono"));
        entidad.CambiarCorreo(r.TextoOpcional("Correo"));
        entidad.CambiarUsuario(r.Texto("Usuario"));
        entidad.CambiarRol(r.Entero("IdRol"), r.Texto("Rol"));
        entidad.CambiarEstado(r.Bit("Estado"));
        entidad.AsignarFechaRegistro(r.Fecha("FechaRegistro"));
        return entidad;
    }
    // Consulta si hay usuarios y si hay un administrador activo.
    public async Task<EstadoInicializacion> ConsultarInicializacionAsync(CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarInicializacion", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Salida("@HayUsuarios", SqlDbType.Bit));
            cmd.Parameters.Add(ParametrosSql.Salida("@HayAdministradorActivo", SqlDbType.Bit));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return new EstadoInicializacion(Convert.ToBoolean(cmd.Parameters["@HayUsuarios"].Value), Convert.ToBoolean(cmd.Parameters["@HayAdministradorActivo"].Value));
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, null);
        }
    }
    // Busca la cuenta y su hash para el login.
    public async Task<CredencialUsuario?> BuscarCredencialAsync(string usuario, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            // Ejecuta la consulta de credenciales.
            using SqlCommand cmd = new("dbo.usp_BuscarCredencialUsuario", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Texto("@Usuario", usuario, 50));
            await cn.OpenAsync(ct);
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            if (!await dr.ReadAsync(ct)) return null;
            return new CredencialUsuario(Mapear(dr), dr.Texto("ContrasenaHash"));
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, null);
        }
    }
    // Convierte los datos de un usuario en parámetros SQL.
    private static SqlParameter[] DatosUsuario(UsuarioSolicitud s, string? hash) =>
    [
        ParametrosSql.Texto("@Nombre", s.Nombre, 100), ParametrosSql.Texto("@Apellido", s.Apellido, 100),
        ParametrosSql.Texto("@Cedula", s.Cedula, 20), ParametrosSql.Texto("@Telefono", s.Telefono, 20),
        ParametrosSql.Texto("@Correo", s.Correo, 100), ParametrosSql.Texto("@Usuario", s.Usuario, 50),
        ParametrosSql.Texto("@ContrasenaHash", hash, 255)
    ];
    // Crea el primer administrador del sistema.
    public async Task<int> CrearAdministradorInicialAsync(UsuarioSolicitud s, string hash, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_CrearAdministradorInicial", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(DatosUsuario(s, hash));
            cmd.Parameters.Add(ParametrosSql.Salida("@IdUsuario"));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdUsuario"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, null);
        }
    } // Guarda un usuario, agregando o actualizando según corresponda.
    public Task<int> GuardarAsync(int idActor, UsuarioSolicitud s, string? hash, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        return s.IdUsuario == 0
            ? AgregarAsync(idActor, s, hash, ct)
            : ActualizarAsync(idActor, s, hash, ct);
    }
    // Agrega un nuevo usuario.
    public Task<int> AgregarAsync(int idActor, UsuarioSolicitud s, string? hash, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.IdUsuario != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarRegistroAsync(idActor, s, hash, ct);
    }

    // Actualiza un usuario existente.
    public Task<int> ActualizarAsync(int idActor, UsuarioSolicitud s, string? hash, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        Validacion.Id(s.IdUsuario, "IdUsuario");
        return GuardarRegistroAsync(idActor, s, hash, ct);
    }

    // Guarda un usuario, agregando o actualizando según corresponda.
    private async Task<int> GuardarRegistroAsync(int idActor, UsuarioSolicitud s, string? hash, CancellationToken ct)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_GuardarUsuario", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.EntradaSalida("@IdUsuario", s.IdUsuario));
            cmd.Parameters.AddRange(DatosUsuario(s, hash));
            cmd.Parameters.Add(ParametrosSql.Texto("@Rol", s.Rol.ToString(), 50));
            cmd.Parameters.Add(ParametrosSql.Bit("@Estado", s.Estado));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdUsuario"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<IReadOnlyList<UsuarioEntity>> ConsultarAsync(int idActor, int? idUsuario = null, bool soloActivos = true, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarUsuarios", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuario", idUsuario));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));

            await cn.OpenAsync(ct);
            List<UsuarioEntity> registros = [];
            using SqlDataReader dr = await cmd.ExecuteReaderAsync(ct);
            while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            return registros.AsReadOnly();
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public Task<int> GuardarPorRolAsync(int idActor, UsuarioRolSolicitud s, string? hash, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        return s.IdUsuario == 0
            ? AgregarAsync(idActor, s, hash, ct)
            : ActualizarAsync(idActor, s, hash, ct);
    }

    public Task<int> AgregarAsync(int idActor, UsuarioRolSolicitud s, string? hash, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        if (s.IdUsuario != 0)
            throw new ValidacionException("Para agregar, el registro debe ser nuevo.");
        return GuardarPorRolRegistroAsync(idActor, s, hash, ct);
    }

    public Task<int> ActualizarAsync(int idActor, UsuarioRolSolicitud s, string? hash, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        Validacion.Id(s.IdUsuario, "IdUsuario");
        return GuardarPorRolRegistroAsync(idActor, s, hash, ct);
    }
    // Guarda un usuario vinculado a un rol, agregando o actualizando según corresponda.
    private async Task<int> GuardarPorRolRegistroAsync(int idActor, UsuarioRolSolicitud s, string? hash, CancellationToken ct)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_GuardarUsuario", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.EntradaSalida("@IdUsuario", s.IdUsuario));
            cmd.Parameters.Add(ParametrosSql.Entero("@IdRol", s.IdRol));
            cmd.Parameters.Add(ParametrosSql.Texto("@Nombre", s.Nombre, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Apellido", s.Apellido, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Cedula", s.Cedula, 20));
            cmd.Parameters.Add(ParametrosSql.Texto("@Telefono", s.Telefono, 20));
            cmd.Parameters.Add(ParametrosSql.Texto("@Correo", s.Correo, 100));
            cmd.Parameters.Add(ParametrosSql.Texto("@Usuario", s.Usuario, 50));
            cmd.Parameters.Add(ParametrosSql.Texto("@ContrasenaHash", hash, 255));
            cmd.Parameters.Add(ParametrosSql.Bit("@Estado", s.Estado));

            await cn.OpenAsync(ct);
            await cmd.ExecuteNonQueryAsync(ct);
            return Convert.ToInt32(cmd.Parameters["@IdUsuario"].Value);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
    public async Task<PaginacionEntity<UsuarioEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default)
    {
        try
        {
            using SqlConnection cn = _sql.ObtenerConexion();
            using SqlCommand cmd = new("dbo.usp_ConsultarUsuarios", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(ParametrosSql.Entero("@IdUsuarioActor", idActor));
            cmd.Parameters.Add(ParametrosSql.Bit("@SoloActivos", soloActivos));
            cmd.Parameters.Add(ParametrosSql.Entero("@Pagina", pagina));
            cmd.Parameters.Add(ParametrosSql.Entero("@TamPagina", tamPagina));
            cmd.Parameters.Add(ParametrosSql.Texto("@Buscar", buscar, 100, true));
            cmd.Parameters.Add(ParametrosSql.Salida("@TotalRegistros"));

            await cn.OpenAsync(ct);
            List<UsuarioEntity> registros = [];
            using (SqlDataReader dr = await cmd.ExecuteReaderAsync(ct))
            {
                while (await dr.ReadAsync(ct)) registros.Add(Mapear(dr));
            }
            // SQL entrega el total mediante OUTPUT al cerrar el lector.
            return new PaginacionEntity<UsuarioEntity>(registros,
                Convert.ToInt32(cmd.Parameters["@TotalRegistros"].Value), pagina, tamPagina);
        }
        catch (SqlException ex) when (!ct.IsCancellationRequested)
        {
            throw await _sql.TraducirErrorAsync(ex, idActor);
        }
    }
}
