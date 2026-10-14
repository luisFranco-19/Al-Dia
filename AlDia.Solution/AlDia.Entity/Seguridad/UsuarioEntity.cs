using AlDia.Entity.Base;
using AlDia.Entity.Common;
using System.Net.Mail;

namespace AlDia.Entity.Seguridad;

public enum RolUsuario { Administrador, Recepcionista, Tecnico }

public sealed class UsuarioEntity : PersonaEntity
{
    #region Propiedades
    public int IdUsuario { get; private set; }
    // Null significa que la consulta actual no devolvio IdRol, no que la FK SQL sea opcional.
    public int? IdRol { get; private set; }
    public string? Correo { get; private set; }
    public string Usuario { get; private set; } = string.Empty;
    public string NombreRol { get; private set; } = string.Empty;
    // El catalogo puede tener mas nombres que los tres permisos existentes del sistema.
    public RolUsuario? PermisoRol => Enum.TryParse<RolUsuario>(NombreRol, out var rol)
        && Enum.IsDefined(rol) && rol.ToString() == NombreRol ? rol : null;
    public RolUsuario Rol => PermisoRol ?? throw new PermisoException("El rol no tiene permisos configurados en el sistema.");
    public DateTime FechaRegistro { get; private set; } = DateTime.Now;
    #endregion

    #region Constructores
    public UsuarioEntity() : base() { }

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
        Validacion.IdNuevoOExistente(idUsuario, "IdUsuario");
        if (idUsuario > 0) AsignarId(idUsuario);
        CambiarUsuario(usuario);
        if (idRol.HasValue) CambiarRol(idRol.Value, rol);
        else CambiarRol(rol);
        AsignarFechaRegistro(fechaRegistro);
        CambiarTelefono(telefono);
        CambiarCorreo(correo);
    }

    // Constructor del modelo actual: FK real y nombre obtenido de dbo.Roles.
    public UsuarioEntity(int idUsuario, string nombre, string apellido, string cedula,
        string? telefono, string? correo, string usuario, int idRol, string nombreRol,
        bool estado, DateTime fechaRegistro) : base(nombre, apellido, cedula, estado)
    {
        Validacion.IdNuevoOExistente(idUsuario, "IdUsuario");
        if (idUsuario > 0) AsignarId(idUsuario);
        CambiarRol(idRol, nombreRol);
        CambiarUsuario(usuario);
        AsignarFechaRegistro(fechaRegistro);
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

    public void CambiarUsuario(string usuario)
    {
        if (string.IsNullOrWhiteSpace(usuario))
            throw new ValidacionException("Debe ingresar el nombre de usuario.");
        Usuario = usuario.Trim();
    }
    public void AsignarFechaRegistro(DateTime fechaRegistro) => FechaRegistro = fechaRegistro;

    public void CambiarRol(int idRol, string nombreRol)
    {
        int id = Validacion.Id(idRol, "IdRol");
        if (string.IsNullOrWhiteSpace(nombreRol))
            throw new ValidacionException("Debe ingresar el nombre del rol.");
        string nombre = nombreRol.Trim();
        IdRol = id;
        NombreRol = nombre;
    }
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
    public void CambiarCorreo(string? correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
        {
            Correo = null;
            return;
        }
        string correoValidado = correo.Trim();
        if (!MailAddress.TryCreate(correoValidado, out _))
            throw new ValidacionException("El correo no es valido.");
        Correo = correoValidado;
    }

    private void ValidarUsuario()
    {
        ValidarPersona();
        if (string.IsNullOrWhiteSpace(Usuario))
            throw new ValidacionException("Debe ingresar el nombre de usuario.");
        if (string.IsNullOrWhiteSpace(NombreRol))
            throw new ValidacionException("Debe ingresar el nombre del rol.");
        if (Correo is not null && !MailAddress.TryCreate(Correo, out _))
            throw new ValidacionException("El correo no es valido.");
    }

    public UsuarioSolicitud CrearSolicitud()
    {
        ValidarUsuario();
        return new(Nombre, Apellido, Cedula, Usuario, Rol, Telefono, Correo, Estado, IdUsuario);
    }

    public UsuarioRolSolicitud CrearSolicitudPorRol()
    {
        ValidarUsuario();
        return new(Validacion.Id(IdRol ?? 0, "IdRol"), Nombre, Apellido, Cedula, Usuario, Telefono, Correo, Estado, IdUsuario);
    }

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
        if (string.IsNullOrWhiteSpace(hash))
            throw new ValidacionException("ContrasenaHash es obligatorio.");
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
