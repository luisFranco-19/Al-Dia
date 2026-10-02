using AlDia.DAL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using Microsoft.Data.SqlClient;
using System.Data;
namespace AlDia.DAL.Seguridad;

public sealed class UsuarioDal(EjecutorSql sql) : IUsuarioDal
{
    internal static UsuarioEntity Mapear(SqlDataReader r) => new(
        r.Entero("IdUsuario"), r.Texto("Nombre"), r.Texto("Apellido"), r.Texto("Cedula"),
        r.TextoOpcional("Telefono"), r.TextoOpcional("Correo"), r.Texto("Usuario"),
        r.Entero("IdRol"), r.Texto("Rol"), r.Bit("Estado"), r.Fecha("FechaRegistro"));
    public Task<EstadoInicializacion> ConsultarInicializacionAsync(CancellationToken ct = default) =>
        sql.EjecutarAsync("dbo.usp_ConsultarInicializacion",
            [ParametrosSql.Salida("@HayUsuarios", SqlDbType.Bit), ParametrosSql.Salida("@HayAdministradorActivo", SqlDbType.Bit)],
            async (cmd, token) =>
            {
                await cmd.ExecuteNonQueryAsync(token);
                return new EstadoInicializacion(Convert.ToBoolean(cmd.Parameters["@HayUsuarios"].Value), Convert.ToBoolean(cmd.Parameters["@HayAdministradorActivo"].Value));
            }, null, ct);
    public async Task<CredencialUsuario?> BuscarCredencialAsync(string usuario, CancellationToken ct = default)
    {
        var resultados = await sql.ConsultarAsync("dbo.usp_BuscarCredencialUsuario", [ParametrosSql.Texto("@Usuario", usuario, 50)],
            r => new CredencialUsuario(Mapear(r), r.Texto("ContrasenaHash")), null, ct);
        return resultados.SingleOrDefault();
    }
    private static SqlParameter[] DatosUsuario(UsuarioSolicitud s, string? hash) =>
    [
        ParametrosSql.Texto("@Nombre", s.Nombre, 100), ParametrosSql.Texto("@Apellido", s.Apellido, 100),
        ParametrosSql.Texto("@Cedula", s.Cedula, 20), ParametrosSql.Texto("@Telefono", s.Telefono, 20),
        ParametrosSql.Texto("@Correo", s.Correo, 100), ParametrosSql.Texto("@Usuario", s.Usuario, 50),
        ParametrosSql.Texto("@ContrasenaHash", hash, 255)
    ];
    public Task<int> CrearAdministradorInicialAsync(UsuarioSolicitud s, string hash, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_CrearAdministradorInicial", [..DatosUsuario(s, hash), ParametrosSql.Salida("@IdUsuario")], "@IdUsuario", null, ct);
    public Task<int> GuardarAsync(int idActor, UsuarioSolicitud s, string? hash, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarUsuario", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.EntradaSalida("@IdUsuario", s.IdUsuario),
            ..DatosUsuario(s, hash), ParametrosSql.Texto("@Rol", s.Rol.ToString(), 50), ParametrosSql.Bit("@Estado", s.Estado)], "@IdUsuario", idActor, ct);
    public Task<IReadOnlyList<UsuarioEntity>> ConsultarAsync(int idActor, int? idUsuario = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarUsuarios", [ParametrosSql.Entero("@IdUsuarioActor", idActor), ParametrosSql.Entero("@IdUsuario", idUsuario),
            ParametrosSql.Bit("@SoloActivos", soloActivos)], Mapear, idActor, ct);
    public Task<int> GuardarPorRolAsync(int idActor, UsuarioRolSolicitud s, string? hash, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarUsuario", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.EntradaSalida("@IdUsuario", s.IdUsuario), ParametrosSql.Entero("@IdRol", s.IdRol),
            ParametrosSql.Texto("@Nombre", s.Nombre, 100), ParametrosSql.Texto("@Apellido", s.Apellido, 100),
            ParametrosSql.Texto("@Cedula", s.Cedula, 20), ParametrosSql.Texto("@Telefono", s.Telefono, 20),
            ParametrosSql.Texto("@Correo", s.Correo, 100), ParametrosSql.Texto("@Usuario", s.Usuario, 50),
            ParametrosSql.Texto("@ContrasenaHash", hash, 255), ParametrosSql.Bit("@Estado", s.Estado)], "@IdUsuario", idActor, ct);
    public Task<PaginacionEntity<UsuarioEntity>> ConsultarPaginaAsync(int idActor, int pagina, int tamPagina,
        string? buscar, bool soloActivos, CancellationToken ct = default) =>
        sql.ConsultarPaginaAsync("dbo.usp_ConsultarUsuarios", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Bit("@SoloActivos", soloActivos)], Mapear, idActor, pagina, tamPagina, buscar, ct);
}
