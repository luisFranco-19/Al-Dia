using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Seguridad;

public sealed class RolEntity : CatalogoEntity
{
    #region Propiedades
    public int IdRol { get; private set; }
    #endregion

    #region Constructores
    public RolEntity(string nombre, string? descripcion = null)
        : this(0, nombre, descripcion, true) { }

    // Reconstruye una fila de dbo.Roles; el ID no se deduce del nombre.
    public RolEntity(int idRol, string nombre, string? descripcion, bool estado)
        : base(nombre, descripcion, estado, 50, 200)
    {
        IdRol = Validacion.IdNuevoOExistente(idRol, "IdRol");
    }
    #endregion

    #region Metodos
    public void AsignarId(int id)
    {
        Validacion.Id(id, "IdRol");
        if (IdRol != 0 && IdRol != id)
            throw new ValidacionException("No se puede cambiar el identificador de un rol existente.");
        IdRol = id;
    }

    public RolSolicitud CrearSolicitud() => new(Nombre, Descripcion, Estado, IdRol);
    #endregion
}

public sealed record RolSolicitud(string Nombre, string? Descripcion, bool Estado = true, int IdRol = 0);
