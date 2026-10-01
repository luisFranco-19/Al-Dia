using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class ServicioEntity : CatalogoEntity
{
    #region Propiedades
    public int IdServicio { get; private set; }
    public decimal PrecioBase { get; private set; }
    #endregion

    #region Constructores
    public ServicioEntity(string nombre, string? descripcion, decimal precioBase)
        : this(0, nombre, descripcion, precioBase, true) { }

    public ServicioEntity(int idServicio, string nombre, string? descripcion, decimal precioBase, bool estado)
        : base(nombre, descripcion, estado, 100, 300)
    {
        IdServicio = Validacion.IdNuevoOExistente(idServicio, "IdServicio");
        CambiarPrecioBase(precioBase);
    }
    #endregion

    #region Metodos
    public void AsignarId(int id)
    {
        Validacion.Id(id, "IdServicio");
        if (IdServicio != 0 && IdServicio != id)
            throw new ValidacionException("No se puede cambiar el identificador de un servicio existente.");
        IdServicio = id;
    }

    public void CambiarPrecioBase(decimal precio) => PrecioBase = Validacion.Dinero(precio, "PrecioBase");
    public ServicioSolicitud CrearSolicitud() => new(Nombre, Descripcion, PrecioBase, Estado, IdServicio);
    #endregion
}

public sealed record ServicioSolicitud(string Nombre, string? Descripcion, decimal PrecioBase, bool Estado = true, int IdServicio = 0);
