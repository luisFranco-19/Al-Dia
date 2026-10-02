using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Seguridad;

public enum RolUsuario { Administrador, Recepcionista, Tecnico }

public sealed class UsuarioEntity : PersonaEntity
{
    #region Propiedades
    public int IdUsuario { get; private set; }
    // Null significa que la consulta actual no devolvio IdRol, no que la FK SQL sea opcional.
    public int? IdRol { get; private set; }
    public string? Correo { get; private set; }
    public string Usuario { get; private set; }
    public string NombreRol { get; private set; }
    // El catalogo puede tener mas nombres que los tres permisos existentes del sistema.
    public RolUsuario? PermisoRol => Enum.TryParse<RolUsuario>(NombreRol, out var rol)
        && Enum.IsDefined(rol) && rol.ToString() == NombreRol ? rol : null;
    public RolUsuario Rol => PermisoRol ?? throw new PermisoException("El rol no tiene permisos configurados en el sistema.");
    public DateTime FechaRegistro { get; }
    #endregion

    #region Constructores
    public UsuarioEntity(string nombre, string apellido, string cedula, string usuario,
        RolUsuario rol, string? telefono = null, string? correo = null)
        : this(0, nombre, apellido, cedula, telefono, correo, usuario, rol, true, DateTime.Now) { }

    public UsuarioEntity(string nombre, string apellido, string cedula, string usuario,
        RolEntity rol, string? telefono = null, string? correo = null)
        : this(0, nombre, apellido, cedula, telefono, correo, usuario,
            ValidarRolSeleccionado(rol), rol.Nombre, true, DateTime.Now) { }

    public UsuarioEntity(int idUsuario, string nombre, string apellido, string cedula,
        string? telefono, string? correo, string usuario, RolUsuario rol, bool estado,
        DateTime fechaRegistro, int? idRol = null) : base(nombre, apellido, cedula, estado)
    {
        IdUsuario = Validacion.IdNuevoOExistente(idUsuario, "IdUsuario");
        Usuario = Validacion.Texto(usuario, "Usuario", 50);
        NombreRol = Validacion.Enumeracion(rol, "Rol").ToString();
        IdRol = idRol.HasValue ? Validacion.Id(idRol.Value, "IdRol") : null;
        FechaRegistro = fechaRegistro;
        CambiarTelefono(telefono);
        CambiarCorreo(correo);
    }

    // Constructor del modelo actual: FK real y nombre obtenido de dbo.Roles.
    public UsuarioEntity(int idUsuario, string nombre, string apellido, string cedula,
        string? telefono, string? correo, string usuario, int idRol, string nombreRol,
        bool estado, DateTime fechaRegistro) : base(nombre, apellido, cedula, estado)
    {
        IdUsuario = Validacion.IdNuevoOExistente(idUsuario, "IdUsuario");
        IdRol = Validacion.Id(idRol, "IdRol");
        NombreRol = Validacion.Texto(nombreRol, "NombreRol", 50);
        Usuario = Validacion.Texto(usuario, "Usuario", 50);
        FechaRegistro = fechaRegistro;
        CambiarTelefono(telefono);
        CambiarCorreo(correo);
    }
    #endregion

    #region Metodos
    public void AsignarId(int id)
    {
        Validacion.Id(id, "IdUsuario");
        if (IdUsuario != 0 && IdUsuario != id)
            throw new ValidacionException("No se puede cambiar el identificador de un usuario existente.");
        IdUsuario = id;
    }

    public void CambiarUsuario(string usuario) => Usuario = Validacion.Texto(usuario, "Usuario", 50);
    public void CambiarRol(RolUsuario rol)
    {
        var validado = Validacion.Enumeracion(rol, "Rol");
        // Al cambiar solo el nombre, la DAL debe resolver nuevamente la FK.
        if (NombreRol != validado.ToString()) IdRol = null;
        NombreRol = validado.ToString();
    }

    public void CambiarRol(int idRol, RolUsuario rol)
    {
        int idValidado = Validacion.Id(idRol, "IdRol");
        var rolValidado = Validacion.Enumeracion(rol, "Rol");
        IdRol = idValidado;
        NombreRol = rolValidado.ToString();
    }

    public void CambiarRol(RolEntity rol)
    {
        int id = ValidarRolSeleccionado(rol);
        IdRol = id;
        NombreRol = rol.Nombre;
    }

    private static int ValidarRolSeleccionado(RolEntity rol)
    {
        ArgumentNullException.ThrowIfNull(rol);
        int id = Validacion.Id(rol.IdRol, "IdRol");
        if (!rol.Estado) throw new ValidacionException("El rol seleccionado debe estar activo.");
        return id;
    }
    public void CambiarCorreo(string? correo) => Correo = Validacion.Correo(correo);

    public UsuarioSolicitud CrearSolicitud() =>
        new(Nombre, Apellido, Cedula, Usuario, Rol, Telefono, Correo, Estado, IdUsuario);

    public UsuarioRolSolicitud CrearSolicitudPorRol() =>
        new(Validacion.Id(IdRol ?? 0, "IdRol"), Nombre, Apellido, Cedula, Usuario, Telefono, Correo, Estado, IdUsuario);

    // La sesion guarda una copia para que editar una entidad no cambie al actor autenticado.
    public UsuarioEntity Copiar() =>
        IdRol.HasValue
            ? new(IdUsuario, Nombre, Apellido, Cedula, Telefono, Correo, Usuario, IdRol.Value, NombreRol, Estado, FechaRegistro)
            : new(IdUsuario, Nombre, Apellido, Cedula, Telefono, Correo, Usuario, Rol, Estado, FechaRegistro);
    #endregion
}

// Este resultado se utiliza exclusivamente entre DAL y BLL durante la autenticacion.
public sealed class CredencialUsuario
{
    public UsuarioEntity Usuario { get; }
    public string Hash { get; }
    public string ContrasenaHash => Hash;

    public CredencialUsuario(UsuarioEntity usuario, string hash)
    {
        ArgumentNullException.ThrowIfNull(usuario);
        if (string.IsNullOrEmpty(hash) || hash.Length > 255)
            throw new ValidacionException("ContrasenaHash es obligatorio y admite hasta 255 caracteres.");
        Usuario = usuario;
        Hash = hash;
    }
}
public sealed record EstadoInicializacion(bool HayUsuarios, bool HayAdministradorActivo);
public sealed record UsuarioSolicitud(string Nombre, string Apellido, string Cedula, string Usuario,
    RolUsuario Rol, string? Telefono = null, string? Correo = null, bool Estado = true, int IdUsuario = 0);
// Contrato del modelo con FK, preparado para adaptar DAL/BLL sin deducir IDs del enum.
public sealed record UsuarioRolSolicitud(int IdRol, string Nombre, string Apellido, string Cedula, string Usuario,
    string? Telefono = null, string? Correo = null, bool Estado = true, int IdUsuario = 0);
