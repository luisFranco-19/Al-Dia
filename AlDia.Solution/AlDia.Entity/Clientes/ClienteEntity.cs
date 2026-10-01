using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Clientes;

public sealed class ClienteEntity : PersonaEntity
{
    #region Propiedades
    public int IdCliente { get; private set; }
    #endregion

    #region Constructores
    public ClienteEntity(string nombre, string apellido, string telefono, string cedula)
        : this(0, nombre, apellido, telefono, cedula, true) { }

    // DAL utiliza este constructor para recuperar un cliente existente.
    public ClienteEntity(int idCliente, string nombre, string apellido, string telefono,
        string cedula, bool estado) : base(nombre, apellido, cedula, estado)
    {
        IdCliente = Validacion.IdNuevoOExistente(idCliente, "IdCliente");
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

    public override void CambiarTelefono(string? telefono) =>
        base.CambiarTelefono(Validacion.Texto(telefono, "Telefono", 20));

    public ClienteSolicitud CrearSolicitud() =>
        new(Nombre, Apellido, Telefono!, Cedula, Estado, IdCliente);
    #endregion
}

public sealed record ClienteSolicitud(string Nombre, string Apellido, string Telefono, string Cedula, bool Estado = true, int IdCliente = 0);
