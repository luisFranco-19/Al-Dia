using AlDia.Entity.Common;
using AlDia.Entity.Pagos;

namespace AlDia.Entity.Reparaciones;

// Composicion: reune los datos consultados sin ejecutar operaciones de persistencia.
public sealed class ExpedienteOrdenEntity
{
    public OrdenReparacionEntity Orden { get; }
    public DiagnosticoEntity? Diagnostico { get; }
    public ConfirmacionEntity? Confirmacion { get; }
    public IReadOnlyList<DetalleServicioEntity> Servicios { get; }
    public IReadOnlyList<DetalleRepuestoEntity> Repuestos { get; }
    public IReadOnlyList<PagoEntity> Pagos { get; }
    public IReadOnlyList<HistorialEstadoEntity> Historial { get; }

    public ExpedienteOrdenEntity(OrdenReparacionEntity orden, DiagnosticoEntity? diagnostico,
        ConfirmacionEntity? confirmacion, IReadOnlyList<DetalleServicioEntity> servicios,
        IReadOnlyList<DetalleRepuestoEntity> repuestos, IReadOnlyList<PagoEntity> pagos,
        IReadOnlyList<HistorialEstadoEntity> historial)
    {
        ArgumentNullException.ThrowIfNull(orden);
        ArgumentNullException.ThrowIfNull(servicios);
        ArgumentNullException.ThrowIfNull(repuestos);
        ArgumentNullException.ThrowIfNull(pagos);
        ArgumentNullException.ThrowIfNull(historial);
        var copiaServicios = servicios.ToList();
        var copiaRepuestos = repuestos.ToList();
        var copiaPagos = pagos.ToList();
        var copiaHistorial = historial.ToList();
        if ((diagnostico is not null && diagnostico.IdOrden != orden.IdOrden)
            || (confirmacion is not null && (diagnostico is null || confirmacion.IdDiagnostico != diagnostico.IdDiagnostico))
            || copiaServicios.Any(x => x is null || x.IdOrden != orden.IdOrden)
            || copiaRepuestos.Any(x => x is null || x.IdOrden != orden.IdOrden)
            || copiaPagos.Any(x => x is null || x.IdOrden != orden.IdOrden)
            || copiaHistorial.Any(x => x is null || x.IdOrden != orden.IdOrden))
            throw new ValidacionException("Los registros del expediente deben pertenecer a la misma orden y diagnostico.");
        Orden = orden;
        Diagnostico = diagnostico;
        Confirmacion = confirmacion;
        Servicios = copiaServicios.AsReadOnly();
        Repuestos = copiaRepuestos.AsReadOnly();
        Pagos = copiaPagos.AsReadOnly();
        Historial = copiaHistorial.AsReadOnly();
    }
}
