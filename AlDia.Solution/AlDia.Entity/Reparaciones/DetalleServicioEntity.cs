using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class DetalleServicioEntity : DetalleOrdenEntity
{
    public int IdDetalleServicio { get; private set; }
    public int IdServicio { get; }
    public string? Observaciones { get; private set; }

    public DetalleServicioEntity(int idOrden, int idServicio, int cantidad, decimal precio, string? observaciones = null)
        : this(0, idOrden, idServicio, cantidad, precio, observaciones) { }

    public DetalleServicioEntity(int idDetalleServicio, int idOrden, int idServicio, int cantidad,
        decimal precio, string? observaciones, int? idTecnicoResponsable = null) : base(idOrden, cantidad, precio, idTecnicoResponsable)
    {
        IdDetalleServicio = Validacion.IdNuevoOExistente(idDetalleServicio, "IdDetalleServicio");
        IdServicio = Validacion.Id(idServicio, "IdServicio");
        Observaciones = Validacion.Opcional(observaciones, "Observaciones", 500);
    }

    public DetalleServicioEntity(int idDetalleServicio, int idOrden, int idServicio, int idTecnico, int cantidad, decimal precio, string? observaciones)
        : this(idDetalleServicio, idOrden, idServicio, cantidad, precio, observaciones, idTecnico) { }

    public void AsignarId(int id) => IdDetalleServicio = Validacion.AsignarId(IdDetalleServicio, id, "IdDetalleServicio");
    public void CambiarObservaciones(string? observaciones) => Observaciones = Validacion.Opcional(observaciones, "Observaciones", 500);
    public ServicioOrdenSolicitud CrearSolicitud() => new(IdOrden, IdServicio, Cantidad, Precio, Observaciones);
    public CorreccionServicioOrdenSolicitud CrearCorreccion() =>
        new(Validacion.Id(IdDetalleServicio, "IdDetalleServicio"), Cantidad, Precio, Observaciones);
}
