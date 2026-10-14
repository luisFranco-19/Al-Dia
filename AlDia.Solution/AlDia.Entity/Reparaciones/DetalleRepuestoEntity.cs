using AlDia.Entity.Base;
using AlDia.Entity.Common;
using AlDia.Entity.Inventario;

namespace AlDia.Entity.Reparaciones;

public sealed class DetalleRepuestoEntity : DetalleOrdenEntity
{
    public int IdDetalleRepuesto { get; private set; }
    public int IdRepuesto { get; private set; }

    public DetalleRepuestoEntity() : base() { }

    public DetalleRepuestoEntity(int idOrden, int idRepuesto, int cantidad, decimal precio)
        : this(0, idOrden, idRepuesto, cantidad, precio) { }

    public DetalleRepuestoEntity(int idDetalleRepuesto, int idOrden, int idRepuesto, int cantidad, decimal precio, int? idTecnicoResponsable = null)
        : base(idOrden, cantidad, precio, idTecnicoResponsable)
    {
        Validacion.IdNuevoOExistente(idDetalleRepuesto, "IdDetalleRepuesto");
        if (idDetalleRepuesto > 0) AsignarId(idDetalleRepuesto);
        CambiarRepuesto(idRepuesto);
    }

    public DetalleRepuestoEntity(int idDetalleRepuesto, int idOrden, int idRepuesto, int idTecnico, int cantidad, decimal precio)
        : this(idDetalleRepuesto, idOrden, idRepuesto, cantidad, precio, idTecnico) { }

    public void AsignarId(int id) => IdDetalleRepuesto = Validacion.AsignarId(IdDetalleRepuesto, id, "IdDetalleRepuesto");
    public void CambiarRepuesto(int idRepuesto)
    {
        int id = Validacion.Id(idRepuesto, "IdRepuesto");
        if (IdRepuesto != 0 && IdRepuesto != id)
            throw new ValidacionException("No se puede sustituir el repuesto de un detalle existente.");
        IdRepuesto = id;
    }

    public ConsumoRepuestoSolicitud CrearSolicitud()
    {
        ValidarDetalle();
        Validacion.Id(IdRepuesto, "IdRepuesto");
        return new(IdOrden, IdRepuesto, Cantidad, Precio);
    }
    public CorreccionConsumoRepuestoSolicitud CrearCorreccion()
    {
        ValidarDetalle();
        Validacion.Id(IdRepuesto, "IdRepuesto");
        return new(Validacion.Id(IdDetalleRepuesto, "IdDetalleRepuesto"), Cantidad, Precio);
    }
}
