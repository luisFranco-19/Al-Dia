using AlDia.Entity.Base;
using AlDia.Entity.Common;
using System.Net.Mail;

namespace AlDia.Entity.Seguridad;

public enum RolUsuario { Administrador, Recepcionista, Tecnico }

public sealed class UsuarioEntity : PersonaEntity
{
    #region Propiedades
    public int IdUsuario { get; private set; }
    public string? Correo { get; private set; }
    public string Usuario { get; private set; }
    public RolUsuario Rol { get; private set; }
    public DateTime FechaRegistro { get; }
    #endregion

    #region Constructores
    public UsuarioEntity(string nombre, string apellido, string cedula, string usuario,
        RolUsuario rol, string? telefono = null, string? correo = null)
        : this(0, nombre, apellido, cedula, telefono, correo, usuario, rol, true, DateTime.Now) { }

    public UsuarioEntity(int idUsuario, string nombre, string apellido, string cedula,
        string? telefono, string? correo, string usuario, RolUsuario rol, bool estado,
        DateTime fechaRegistro) : base(nombre, apellido, cedula, estado)
    {
        IdUsuario = Validacion.IdNuevoOExistente(idUsuario, "IdUsuario");
        Usuario = Validacion.Texto(usuario, "Usuario", 50);
        Rol = Validacion.Enumeracion(rol, "Rol");
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
    public void CambiarRol(RolUsuario rol) => Rol = Validacion.Enumeracion(rol, "Rol");
    public void CambiarCorreo(string? correo)
    {
        string? validado = Validacion.Opcional(correo, "Correo", 100);
        if (validado is not null && !MailAddress.TryCreate(validado, out _))
            throw new ValidacionException("El correo no es valido.");
        Correo = validado;
    }

    public UsuarioSolicitud CrearSolicitud() =>
        new(Nombre, Apellido, Cedula, Usuario, Rol, Telefono, Correo, Estado, IdUsuario);

    // La sesion guarda una copia para que editar una entidad no cambie al actor autenticado.
    public UsuarioEntity Copiar() =>
        new(IdUsuario, Nombre, Apellido, Cedula, Telefono, Correo, Usuario, Rol, Estado, FechaRegistro);
    #endregion
}

// Este resultado se utiliza exclusivamente entre DAL y BLL durante la autenticacion.
public sealed record CredencialUsuario(UsuarioEntity Usuario, string Hash);
public sealed record EstadoInicializacion(bool HayUsuarios, bool HayAdministradorActivo);
public sealed record UsuarioSolicitud(string Nombre, string Apellido, string Cedula, string Usuario,
    RolUsuario Rol, string? Telefono = null, string? Correo = null, bool Estado = true, int IdUsuario = 0);
