using AlDia.Entity.Common;

namespace AlDia.Entity.Base;

public abstract class DetalleOrdenEntity
{
    public int IdOrden { get; private set; }
    public int Cantidad { get; private set; }
    public decimal Precio { get; private set; }
    public decimal Subtotal => Cantidad * Precio;
    // Proyeccion de la consulta del expediente; no es una columna de las tablas de detalles.
    public int? IdTecnicoResponsable { get; private set; }
    public int? IdTecnico => IdTecnicoResponsable;

    protected DetalleOrdenEntity() { }

    protected DetalleOrdenEntity(int idOrden, int cantidad, decimal precio, int? idTecnicoResponsable) : this()
    {
        CambiarOrden(idOrden);
        CambiarImportes(cantidad, precio);
        AsignarTecnicoResponsable(idTecnicoResponsable);
    }

    public void CambiarOrden(int idOrden)
    {
        int id = Validacion.Id(idOrden, "IdOrden");
        if (IdOrden != 0 && IdOrden != id)
            throw new ValidacionException("No se puede trasladar un detalle a otra orden.");
        IdOrden = id;
    }

    public void CambiarCantidad(int cantidad) => Cantidad = Validacion.Id(cantidad, "Cantidad");
    public void CambiarPrecio(decimal precio) => Precio = Validacion.Dinero(precio, "Precio");
    public void AsignarTecnicoResponsable(int? idTecnico) =>
        IdTecnicoResponsable = idTecnico.HasValue ? Validacion.Id(idTecnico.Value, "IdTecnicoResponsable") : null;

    public void CambiarImportes(int cantidad, decimal precio)
    {
        int cantidadValidada = Validacion.Id(cantidad, "Cantidad");
        decimal precioValidado = Validacion.Dinero(precio, "Precio");
        Cantidad = cantidadValidada;
        Precio = precioValidado;
    }

    protected void ValidarDetalle()
    {
        Validacion.Id(IdOrden, "IdOrden");
        Validacion.Id(Cantidad, "Cantidad");
        Validacion.Dinero(Precio, "Precio");
    }
}
