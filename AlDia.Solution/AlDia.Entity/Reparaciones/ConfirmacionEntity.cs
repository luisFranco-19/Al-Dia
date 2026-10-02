using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class ConfirmacionEntity
{
    public int IdConfirmacion { get; private set; }
    public int IdDiagnostico { get; }
    public int IdUsuario { get; }
    public DecisionCliente Decision { get; private set; }
    public decimal? CostoAprobado { get; private set; }
    public DateTime FechaConfirmacion { get; }
    public string? Observaciones { get; private set; }

    public ConfirmacionEntity(int idDiagnostico, int idUsuario, DecisionCliente decision, decimal? costoAprobado, string? observaciones = null)
        : this(0, idDiagnostico, idUsuario, decision, costoAprobado, DateTime.Now, observaciones) { }

    public ConfirmacionEntity(int idConfirmacion, int idDiagnostico, int idUsuario, DecisionCliente decision,
        decimal? costoAprobado, DateTime fechaConfirmacion, string? observaciones)
    {
        IdConfirmacion = Validacion.IdNuevoOExistente(idConfirmacion, "IdConfirmacion");
        IdDiagnostico = Validacion.Id(idDiagnostico, "IdDiagnostico");
        IdUsuario = Validacion.Id(idUsuario, "IdUsuario");
        FechaConfirmacion = fechaConfirmacion;
        CambiarDecision(decision, costoAprobado, observaciones);
    }

    public void AsignarId(int id) => IdConfirmacion = Validacion.AsignarId(IdConfirmacion, id, "IdConfirmacion");

    public void CambiarDecision(DecisionCliente decision, decimal? costoAprobado, string? observaciones = null)
    {
        var validada = Validacion.Enumeracion(decision, "Decision");
        if (validada == DecisionCliente.Aprobada && !costoAprobado.HasValue)
            throw new ValidacionException("Una decision aprobada requiere un costo autorizado.");
        if (validada == DecisionCliente.Rechazada && costoAprobado.HasValue)
            throw new ValidacionException("Una decision rechazada no tiene costo aprobado.");
        decimal? costo = costoAprobado.HasValue ? Validacion.Dinero(costoAprobado.Value, "CostoAprobado") : null;
        var notas = Validacion.Opcional(observaciones, "Observaciones", 500);
        Decision = validada;
        CostoAprobado = costo;
        Observaciones = notas;
    }

    public DecisionClienteSolicitud CrearSolicitud() => new(IdDiagnostico, Decision, CostoAprobado, Observaciones);
    public CorreccionDecisionClienteSolicitud CrearCorreccion() =>
        new(Validacion.Id(IdConfirmacion, "IdConfirmacion"), Decision, CostoAprobado, Observaciones);
}
