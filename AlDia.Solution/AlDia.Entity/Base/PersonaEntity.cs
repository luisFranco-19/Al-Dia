using AlDia.Entity.Common;

namespace AlDia.Entity.Base;

public abstract class PersonaEntity
{
    #region Propiedades
    public string Nombre { get; private set; } = string.Empty;
    public string Apellido { get; private set; } = string.Empty;
    public string Cedula { get; private set; } = string.Empty;
    public string? Telefono { get; private set; }
    public bool Estado { get; private set; }
    public string EstadoNombre => Estado ? "Activo" : "Inactivo";
    public string NombreCompleto => $"{Nombre} {Apellido}";
    #endregion

    #region Constructores
    protected PersonaEntity() { }

    protected PersonaEntity(string nombre, string apellido, string cedula, bool estado) : this()
    {
        CambiarNombre(nombre);
        CambiarApellido(apellido);
        CambiarCedula(cedula);
        CambiarEstado(estado);
    }
    #endregion

    #region Metodos
    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ValidacionException("Debe ingresar el nombre.");
        Nombre = nombre.Trim();
    }

    public void CambiarApellido(string apellido)
    {
        if (string.IsNullOrWhiteSpace(apellido))
            throw new ValidacionException("Debe ingresar el apellido.");
        Apellido = apellido.Trim();
    }

    public void CambiarCedula(string cedula)
    {
        if (string.IsNullOrWhiteSpace(cedula))
            throw new ValidacionException("Debe ingresar la cedula.");
        Cedula = cedula.Trim();
    }

    // Usuario permite telefono opcional; Cliente especializa esta regla con override.
    public virtual void CambiarTelefono(string? telefono)
    {
        Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
    }

    public void CambiarEstado(bool estado) => Estado = estado;
    public void Activar() => CambiarEstado(true);
    public void Desactivar() => CambiarEstado(false);

    protected void ValidarPersona()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
            throw new ValidacionException("Debe ingresar el nombre.");
        if (string.IsNullOrWhiteSpace(Apellido))
            throw new ValidacionException("Debe ingresar el apellido.");
        if (string.IsNullOrWhiteSpace(Cedula))
            throw new ValidacionException("Debe ingresar la cedula.");
    }
    #endregion
}
