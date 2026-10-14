using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Inventario;

public sealed class RepuestoEntity : CatalogoEntity
{
    #region Propiedades
    public int IdRepuesto { get; private set; }
    public string? Marca { get; private set; }
    public string? NumeroParte { get; private set; }
    public decimal PrecioCompra { get; private set; }
    public decimal PrecioVenta { get; private set; }
    // El stock consultado solo cambia en SQL mediante los procedimientos de inventario.
    public int Stock { get; private set; }
    #endregion

    #region Constructores
    public RepuestoEntity() : base() { }

    public RepuestoEntity(string nombre, string? descripcion, string? marca, string? numeroParte,
        decimal precioCompra, decimal precioVenta)
        : this(0, nombre, descripcion, marca, numeroParte, precioCompra, precioVenta, true, 0) { }

    public RepuestoEntity(int idRepuesto, string nombre, string? descripcion, string? marca,
        string? numeroParte, decimal precioCompra, decimal precioVenta, bool estado, int stock)
        : base(nombre, descripcion, estado)
    {
        Validacion.IdNuevoOExistente(idRepuesto, "IdRepuesto");
        if (idRepuesto > 0) AsignarId(idRepuesto);
        AsignarStockConsultado(stock);
        CambiarMarca(marca);
        CambiarNumeroParte(numeroParte);
        CambiarPrecios(precioCompra, precioVenta);
    }
    #endregion

    #region Metodos
    public void AsignarId(int id)
    {
        Validacion.Id(id, "IdRepuesto");
        if (IdRepuesto != 0 && IdRepuesto != id)
            throw new ValidacionException("No se puede cambiar el identificador de un repuesto existente.");
        IdRepuesto = id;
    }

    public void CambiarMarca(string? marca)
    {
        Marca = string.IsNullOrWhiteSpace(marca) ? null : marca.Trim();
    }

    public void CambiarNumeroParte(string? numeroParte)
    {
        NumeroParte = string.IsNullOrWhiteSpace(numeroParte) ? null : numeroParte.Trim();
    }
    public void CambiarPrecios(decimal precioCompra, decimal precioVenta)
    {
        var compra = Validacion.Dinero(precioCompra, "PrecioCompra");
        var venta = Validacion.Dinero(precioVenta, "PrecioVenta");
        PrecioCompra = compra;
        PrecioVenta = venta;
    }

    // Proyección de SQL: no forma parte de la solicitud de guardado del catálogo.
    public void AsignarStockConsultado(int stock)
    {
        if (stock < 0) throw new ValidacionException("El stock no puede ser negativo.");
        Stock = stock;
    }

    public RepuestoSolicitud CrearSolicitud()
    {
        ValidarCatalogo();
        return new(Nombre, Descripcion, Marca, NumeroParte, PrecioCompra, PrecioVenta, Estado, IdRepuesto);
    }
    #endregion
}

public sealed record RepuestoSolicitud(string Nombre, string? Descripcion, string? Marca, string? NumeroParte, decimal PrecioCompra, decimal PrecioVenta, bool Estado = true, int IdRepuesto = 0);
