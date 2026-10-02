using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class EstadoReparacionEntity
{
    public int IdEstado { get; private set; }
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }

    public EstadoReparacionEntity(string nombre, string? descripcion = null)
        : this(0, nombre, descripcion) { }

    public EstadoReparacionEntity(int idEstado, string nombre, string? descripcion)
    {
        IdEstado = Validacion.IdNuevoOExistente(idEstado, "IdEstado");
        Nombre = Validacion.Texto(nombre, "Nombre", 50);
        Descripcion = Validacion.Opcional(descripcion, "Descripcion", 200);
    }

    public void AsignarId(int id) => IdEstado = Validacion.AsignarId(IdEstado, id, "IdEstado");
    public void CambiarNombre(string nombre) => Nombre = Validacion.Texto(nombre, "Nombre", 50);
    public void CambiarDescripcion(string? descripcion) => Descripcion = Validacion.Opcional(descripcion, "Descripcion", 200);
}
