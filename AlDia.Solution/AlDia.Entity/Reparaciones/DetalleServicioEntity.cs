using AlDia.Entity.Base;
using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class DetalleServicioEntity : DetalleOrdenEntity
{
    public int IdDetalleServicio { get; private set; }
    public int IdServicio { get; private set; }
    public string? Observaciones { get; private set; }

    public DetalleServicioEntity() : base() { }

    public DetalleServicioEntity(int idOrden, int idServicio, int cantidad, decimal precio, string? observaciones = null)
        : this(0, idOrden, idServicio, cantidad, precio, observaciones) { }

    public DetalleServicioEntity(int idDetalleServicio, int idOrden, int idServicio, int cantidad,
        decimal precio, string? observaciones, int? idTecnicoResponsable = null) : base(idOrden, cantidad, precio, idTecnicoResponsable)
    {
        Validacion.IdNuevoOExistente(idDetalleServicio, "IdDetalleServicio");
        if (idDetalleServicio > 0) AsignarId(idDetalleServicio);
        CambiarServicio(idServicio);
        CambiarObservaciones(observaciones);
    }

    public DetalleServicioEntity(int idDetalleServicio, int idOrden, int idServicio, int idTecnico, int cantidad, decimal precio, string? observaciones)
        : this(idDetalleServicio, idOrden, idServicio, cantidad, precio, observaciones, idTecnico) { }

    public void AsignarId(int id) => IdDetalleServicio = Validacion.AsignarId(IdDetalleServicio, id, "IdDetalleServicio");
    public void CambiarObservaciones(string? observaciones)
    {
        Observaciones = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim();
    }
    public void CambiarServicio(int idServicio)
    {
        int id = Validacion.Id(idServicio, "IdServicio");
        if (IdServicio != 0 && IdServicio != id)
            throw new ValidacionException("No se puede sustituir el servicio de un detalle existente.");
        IdServicio = id;
    }

    public ServicioOrdenSolicitud CrearSolicitud()
    {
        ValidarDetalle();
        Validacion.Id(IdServicio, "IdServicio");
        return new(IdOrden, IdServicio, Cantidad, Precio, Observaciones);
    }
    public CorreccionServicioOrdenSolicitud CrearCorreccion()
    {
        ValidarDetalle();
        Validacion.Id(IdServicio, "IdServicio");
        return new(Validacion.Id(IdDetalleServicio, "IdDetalleServicio"), Cantidad, Precio, Observaciones);
    }
}
