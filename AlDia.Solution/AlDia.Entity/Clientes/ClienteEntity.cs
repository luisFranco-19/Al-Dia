using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Clientes;

public sealed class ClienteEntity : PersonaEntity // Representa a un cliente y hereda datos de PersonaEntity.
{
    #region Propiedades

    public int IdCliente { get; private set; } // ID del cliente; solo la entidad puede modificarlo.
    #endregion

    #region Constructores
    public ClienteEntity() : base() { } // Crea un cliente vacío para completarlo después.
    public ClienteEntity(string nombre, string apellido, string telefono, string cedula) // Crea un cliente nuevo con estado activo.
        : this(0, nombre, apellido, telefono, cedula, true) { }

    public ClienteEntity(int idCliente, string nombre, string apellido, string telefono, // Crea un cliente con sus datos e ID.
        string cedula, bool estado) : base(nombre, apellido, cedula, estado)
    {
        Validacion.IdNuevoOExistente(idCliente, "IdCliente"); // Comprueba que el ID sea válido para un cliente nuevo o existente.
        if (idCliente > 0) AsignarId(idCliente); // Asigna el ID solo a un cliente existente.
        CambiarTelefono(telefono); // Guarda el teléfono aplicando la regla del cliente.
    }
    #endregion

    #region Metodos
    public void AsignarId(int id) // Asigna el ID y evita cambiarlo después.
    {
        Validacion.Id(id, "IdCliente"); // Comprueba que el ID sea válido.
        if (IdCliente != 0 && IdCliente != id) // Revisa si ya existe un ID distinto asignado.
            throw new ValidacionException("No se puede cambiar el identificador de un cliente existente.");
        IdCliente = id; // Guarda el ID validado.
    }

    public override void CambiarTelefono(string? telefono) // Hace obligatorio el teléfono para los clientes.
    {
        if (string.IsNullOrWhiteSpace(telefono))
            throw new ValidacionException("Debe ingresar el telefono del cliente.");
        base.CambiarTelefono(telefono); // Guarda el teléfono aplicando la regla del cliente.
    }

    public ClienteSolicitud CrearSolicitud() // Prepara los datos validados para guardarlos.
    {
        ValidarPersona(); // Comprueba nombre, apellido y cédula.
        if (string.IsNullOrWhiteSpace(Telefono))
            throw new ValidacionException("Debe ingresar el telefono del cliente.");
        return new(Nombre, Apellido, Telefono, Cedula, Estado, IdCliente); // Crea la solicitud con los datos del cliente.
    }
    #endregion
}
public sealed record ClienteSolicitud(string Nombre, string Apellido, string Telefono, string Cedula, // Agrupa los datos que se envían a la capa de negocio.
    bool Estado = true, int IdCliente = 0);
