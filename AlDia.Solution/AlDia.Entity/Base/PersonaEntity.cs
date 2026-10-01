using AlDia.Entity.Common;

namespace AlDia.Entity.Base;

public abstract class PersonaEntity
{
    #region Propiedades
    public string Nombre { get; private set; }
    public string Apellido { get; private set; }
    public string Cedula { get; private set; }
    public string? Telefono { get; private set; }
    public bool Estado { get; private set; }
    public string NombreCompleto => $"{Nombre} {Apellido}";
    #endregion

    #region Constructores
    protected PersonaEntity(string nombre, string apellido, string cedula, bool estado)
    {
        Nombre = Validacion.Texto(nombre, "Nombre", 100);
        Apellido = Validacion.Texto(apellido, "Apellido", 100);
        Cedula = Validacion.Texto(cedula, "Cedula", 20);
        Estado = estado;
    }
    #endregion

    #region Metodos
    public void CambiarNombre(string nombre) => Nombre = Validacion.Texto(nombre, "Nombre", 100);
    public void CambiarApellido(string apellido) => Apellido = Validacion.Texto(apellido, "Apellido", 100);
    public void CambiarCedula(string cedula) => Cedula = Validacion.Texto(cedula, "Cedula", 20);

    // Usuario permite telefono opcional; Cliente especializa esta regla con override.
    public virtual void CambiarTelefono(string? telefono) =>
        Telefono = Validacion.Opcional(telefono, "Telefono", 20);

    public void Activar() => Estado = true;
    public void Desactivar() => Estado = false;
    #endregion
}
