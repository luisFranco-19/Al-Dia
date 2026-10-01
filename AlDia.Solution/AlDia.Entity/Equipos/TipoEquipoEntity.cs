using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Equipos;

public sealed class TipoEquipoEntity : CatalogoEntity
{
    #region Propiedades
    public int IdTipoEquipo { get; private set; }
    #endregion

    #region Constructores
    public TipoEquipoEntity(string nombre, string? descripcion = null)
        : this(0, nombre, descripcion, true) { }

    public TipoEquipoEntity(int idTipoEquipo, string nombre, string? descripcion, bool estado)
        : base(nombre, descripcion, estado, 50, 200)
    {
        IdTipoEquipo = Validacion.IdNuevoOExistente(idTipoEquipo, "IdTipoEquipo");
    }
    #endregion

    #region Metodos
    public void AsignarId(int id)
    {
        Validacion.Id(id, "IdTipoEquipo");
        if (IdTipoEquipo != 0 && IdTipoEquipo != id)
            throw new ValidacionException("No se puede cambiar el identificador de un tipo de equipo existente.");
        IdTipoEquipo = id;
    }

    public TipoEquipoSolicitud CrearSolicitud() => new(Nombre, Descripcion, Estado, IdTipoEquipo);
    #endregion
}

public sealed record TipoEquipoSolicitud(string Nombre, string? Descripcion, bool Estado = true, int IdTipoEquipo = 0);
