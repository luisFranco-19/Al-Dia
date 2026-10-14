using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class EstadoReparacionEntity
{
    public int IdEstado { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }

    public EstadoReparacionEntity() { }

    public EstadoReparacionEntity(string nombre, string? descripcion = null)
        : this(0, nombre, descripcion) { }

    public EstadoReparacionEntity(int idEstado, string nombre, string? descripcion)
    {
        Validacion.IdNuevoOExistente(idEstado, "IdEstado");
        if (idEstado > 0) AsignarId(idEstado);
        CambiarNombre(nombre);
        CambiarDescripcion(descripcion);
    }

    public void AsignarId(int id) => IdEstado = Validacion.AsignarId(IdEstado, id, "IdEstado");
    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ValidacionException("Debe ingresar el nombre del estado de reparacion.");
        Nombre = nombre.Trim();
    }

    public void CambiarDescripcion(string? descripcion)
    {
        Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim();
    }
}
