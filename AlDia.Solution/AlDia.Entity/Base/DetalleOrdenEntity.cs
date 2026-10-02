using AlDia.Entity.Common;

namespace AlDia.Entity.Base;

public abstract class DetalleOrdenEntity
{
    public int IdOrden { get; }
    public int Cantidad { get; private set; }
    public decimal Precio { get; private set; }
    public decimal Subtotal => Cantidad * Precio;
    // Proyeccion de la consulta del expediente; no es una columna de las tablas de detalles.
    public int? IdTecnicoResponsable { get; }
    public int? IdTecnico => IdTecnicoResponsable;

    protected DetalleOrdenEntity(int idOrden, int cantidad, decimal precio, int? idTecnicoResponsable)
    {
        IdOrden = Validacion.Id(idOrden, "IdOrden");
        Cantidad = Validacion.Id(cantidad, "Cantidad");
        Precio = Validacion.Dinero(precio, "Precio");
        IdTecnicoResponsable = idTecnicoResponsable.HasValue ? Validacion.Id(idTecnicoResponsable.Value, "IdTecnicoResponsable") : null;
    }

    public void CambiarImportes(int cantidad, decimal precio)
    {
        int cantidadValidada = Validacion.Id(cantidad, "Cantidad");
        decimal precioValidado = Validacion.Dinero(precio, "Precio");
        Cantidad = cantidadValidada;
        Precio = precioValidado;
    }
}
