using AlDia.Entity.Base;
using AlDia.Entity.Common;
using AlDia.Entity.Inventario;

namespace AlDia.Entity.Reparaciones;

public sealed class DetalleRepuestoEntity : DetalleOrdenEntity
{
    public int IdDetalleRepuesto { get; private set; }
    public int IdRepuesto { get; }

    public DetalleRepuestoEntity(int idOrden, int idRepuesto, int cantidad, decimal precio)
        : this(0, idOrden, idRepuesto, cantidad, precio) { }

    public DetalleRepuestoEntity(int idDetalleRepuesto, int idOrden, int idRepuesto, int cantidad, decimal precio, int? idTecnicoResponsable = null)
        : base(idOrden, cantidad, precio, idTecnicoResponsable)
    {
        IdDetalleRepuesto = Validacion.IdNuevoOExistente(idDetalleRepuesto, "IdDetalleRepuesto");
        IdRepuesto = Validacion.Id(idRepuesto, "IdRepuesto");
    }

    public DetalleRepuestoEntity(int idDetalleRepuesto, int idOrden, int idRepuesto, int idTecnico, int cantidad, decimal precio)
        : this(idDetalleRepuesto, idOrden, idRepuesto, cantidad, precio, idTecnico) { }

    public void AsignarId(int id) => IdDetalleRepuesto = Validacion.AsignarId(IdDetalleRepuesto, id, "IdDetalleRepuesto");
    public ConsumoRepuestoSolicitud CrearSolicitud() => new(IdOrden, IdRepuesto, Cantidad, Precio);
    public CorreccionConsumoRepuestoSolicitud CrearCorreccion() =>
        new(Validacion.Id(IdDetalleRepuesto, "IdDetalleRepuesto"), Cantidad, Precio);
}
