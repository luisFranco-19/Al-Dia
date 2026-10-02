using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

// Auditoria inmutable: las transiciones se registran mediante los procedimientos del flujo.
public sealed class HistorialEstadoEntity
{
    public int IdHistorial { get; }
    public int IdOrden { get; }
    public int? IdEstadoAnterior { get; }
    public int IdEstadoNuevo { get; }
    public int IdUsuario { get; }
    public DateTime FechaCambio { get; }
    public string? Observacion { get; }

    public HistorialEstadoEntity(int idHistorial, int idOrden, int? idEstadoAnterior,
        int idEstadoNuevo, int idUsuario, DateTime fechaCambio, string? observacion)
    {
        IdHistorial = Validacion.Id(idHistorial, "IdHistorial");
        IdOrden = Validacion.Id(idOrden, "IdOrden");
        IdEstadoAnterior = idEstadoAnterior.HasValue ? Validacion.Id(idEstadoAnterior.Value, "IdEstadoAnterior") : null;
        IdEstadoNuevo = Validacion.Id(idEstadoNuevo, "IdEstadoNuevo");
        IdUsuario = Validacion.Id(idUsuario, "IdUsuario");
        FechaCambio = fechaCambio;
        Observacion = Validacion.Opcional(observacion, "Observacion", 500);
    }
}
