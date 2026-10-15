using AlDia.Entity.Common;

namespace AlDia.Entity.Base;

public abstract class PersonaEntity // Clase base con datos comunes de una persona.
{
    #region Propiedades
    public string Nombre { get; private set; } = string.Empty; // Nombre de la persona; inicia vacío.
    public string Apellido { get; private set; } = string.Empty; // Apellido de la persona.
    public string Cedula { get; private set; } = string.Empty; // Cédula de la persona.
    public string? Telefono { get; private set; } // Teléfono opcional en la clase base.
    public bool Estado { get; private set; } // Indica si la persona está activa.
    public string EstadoNombre => Estado ? "Activo" : "Inactivo"; // Devuelve el estado como Activo o Inactivo.
    public string NombreCompleto => $"{Nombre} {Apellido}"; // Nombre de la persona; inicia vacío.
    #endregion

    #region Constructores
    protected PersonaEntity() { } // Constructor que usan las clases derivadas.

    protected PersonaEntity(string nombre, string apellido, string cedula, bool estado) : this() // Inicializa los datos compartidos de la persona.
    {
        CambiarNombre(nombre); // Asigna el nombre y lo valida.
        CambiarApellido(apellido); // Asigna el apellido y lo valida.
        CambiarCedula(cedula); // Asigna la cédula y la valida.
        CambiarEstado(estado); // Guarda el estado recibido.
    }
    #endregion

    #region Metodos
    public void CambiarNombre(string nombre) // Valida y guarda el nombre.
    {
        if (string.IsNullOrWhiteSpace(nombre)) // Rechaza nombres vacíos o con solo espacios.
            throw new ValidacionException("Debe ingresar el nombre.");
        Nombre = nombre.Trim(); // Quita espacios sobrantes al guardar el nombre.
    }

    public void CambiarApellido(string apellido) // Valida y guarda el apellido.
    {
        if (string.IsNullOrWhiteSpace(apellido)) // Rechaza apellidos vacíos o con solo espacios.
            throw new ValidacionException("Debe ingresar el apellido.");
        Apellido = apellido.Trim(); // Quita espacios sobrantes del apellido.
    }

    public void CambiarCedula(string cedula) // Valida y guarda la cédula.
    {
        if (string.IsNullOrWhiteSpace(cedula)) // Rechaza cédulas vacías o con solo espacios.
            throw new ValidacionException("Debe ingresar la cedula.");
        Cedula = cedula.Trim(); // Quita espacios sobrantes de la cédula.
    }

    public virtual void CambiarTelefono(string? telefono) // Permite que una clase hija cambie la regla del teléfono.
    {
        Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim(); // Guarda null si no hay teléfono; si hay, quita espacios.
    }

    public void CambiarEstado(bool estado) => Estado = estado; // Cambia el estado al valor recibido.
    public void Activar() => CambiarEstado(true); // Activa a la persona.
    public void Desactivar() => CambiarEstado(false); // Desactiva a la persona.

    protected void ValidarPersona() // Comprueba que nombre, apellido y cédula estén completos.
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
