using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Clientes;

public sealed class ClienteEntity : PersonaEntity
{
    #region Propiedades
    public int IdCliente { get; private set; }
    #endregion

    #region Constructores
    // se crea un objeto cliente sin datos, para completarlos mediante metodos.
    public ClienteEntity() : base() { }
    //se prepara para crear un cliente nuevo con un ID 0 y un esado activo por defecto.
    public ClienteEntity(string nombre, string apellido, string telefono, string cedula)
        : this(0, nombre, apellido, telefono, cedula, true) { }

    // DAL utiliza este constructor para recuperar un cliente existente.
    public ClienteEntity(int idCliente, string nombre, string apellido, string telefono,
        string cedula, bool estado) : base(nombre, apellido, cedula, estado)
    {
        Validacion.IdNuevoOExistente(idCliente, "IdCliente");
        if (idCliente > 0) AsignarId(idCliente);
        CambiarTelefono(telefono);
    }
    #endregion

    #region Metodos
    public void AsignarId(int id)
    {
        Validacion.Id(id, "IdCliente");
        if (IdCliente != 0 && IdCliente != id)
            throw new ValidacionException("No se puede cambiar el identificador de un cliente existente.");
        IdCliente = id;
    }

    public override void CambiarTelefono(string? telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
            throw new ValidacionException("Debe ingresar el telefono del cliente.");
        base.CambiarTelefono(telefono);
    }

    public ClienteSolicitud CrearSolicitud()
    {
        ValidarPersona();
        if (string.IsNullOrWhiteSpace(Telefono))
            throw new ValidacionException("Debe ingresar el telefono del cliente.");
        return new(Nombre, Apellido, Telefono, Cedula, Estado, IdCliente);
    }
    #endregion
}
// Objeto para que recopile la imformacion de un cliente y
// y se prepara medinate Crear Solicitud.
public sealed record ClienteSolicitud(string Nombre, string Apellido, string Telefono, string Cedula,
    bool Estado = true, int IdCliente = 0);
