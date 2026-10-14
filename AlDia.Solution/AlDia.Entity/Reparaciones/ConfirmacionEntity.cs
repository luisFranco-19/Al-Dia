using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class ConfirmacionEntity
{
    public int IdConfirmacion { get; private set; }
    public int IdDiagnostico { get; private set; }
    public int IdUsuario { get; private set; }
    public DecisionCliente Decision { get; private set; }
    public decimal? CostoAprobado { get; private set; }
    public DateTime FechaConfirmacion { get; } = DateTime.Now;
    public string? Observaciones { get; private set; }

    public ConfirmacionEntity() { }

    public ConfirmacionEntity(int idDiagnostico, int idUsuario, DecisionCliente decision, decimal? costoAprobado, string? observaciones = null)
        : this(0, idDiagnostico, idUsuario, decision, costoAprobado, DateTime.Now, observaciones) { }

    public ConfirmacionEntity(int idConfirmacion, int idDiagnostico, int idUsuario, DecisionCliente decision,
        decimal? costoAprobado, DateTime fechaConfirmacion, string? observaciones)
    {
        Validacion.IdNuevoOExistente(idConfirmacion, "IdConfirmacion");
        if (idConfirmacion > 0) AsignarId(idConfirmacion);
        CambiarDiagnostico(idDiagnostico);
        CambiarUsuario(idUsuario);
        FechaConfirmacion = fechaConfirmacion;
        CambiarDecision(decision, costoAprobado, observaciones);
    }

    public void AsignarId(int id) => IdConfirmacion = Validacion.AsignarId(IdConfirmacion, id, "IdConfirmacion");

    public void CambiarDiagnostico(int idDiagnostico)
    {
        int id = Validacion.Id(idDiagnostico, "IdDiagnostico");
        if (IdDiagnostico != 0 && IdDiagnostico != id)
            throw new ValidacionException("No se puede trasladar una confirmación a otro diagnóstico.");
        IdDiagnostico = id;
    }

    public void CambiarUsuario(int idUsuario)
    {
        int id = Validacion.Id(idUsuario, "IdUsuario");
        if (IdUsuario != 0 && IdUsuario != id)
            throw new ValidacionException("No se puede sustituir el usuario de la confirmación.");
        IdUsuario = id;
    }

    public void CambiarDecision(DecisionCliente decision, decimal? costoAprobado, string? observaciones = null)
    {
        var validada = Validacion.Enumeracion(decision, "Decision");
        if (validada == DecisionCliente.Aprobada && !costoAprobado.HasValue)
            throw new ValidacionException("Una decision aprobada requiere un costo autorizado.");
        if (validada == DecisionCliente.Rechazada && costoAprobado.HasValue)
            throw new ValidacionException("Una decision rechazada no tiene costo aprobado.");
        decimal? costo = costoAprobado.HasValue ? Validacion.Dinero(costoAprobado.Value, "CostoAprobado") : null;
        string? notas = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim();
        Decision = validada;
        CostoAprobado = costo;
        Observaciones = notas;
    }

    public DecisionClienteSolicitud CrearSolicitud()
    {
        Validacion.Id(IdDiagnostico, "IdDiagnostico");
        Validacion.Id(IdUsuario, "IdUsuario");
        Validacion.Enumeracion(Decision, "Decision");
        if (Decision == DecisionCliente.Aprobada && !CostoAprobado.HasValue)
            throw new ValidacionException("Una decision aprobada requiere un costo autorizado.");
        return new(IdDiagnostico, Decision, CostoAprobado, Observaciones);
    }
    public CorreccionDecisionClienteSolicitud CrearCorreccion()
    {
        var datos = CrearSolicitud();
        return new(Validacion.Id(IdConfirmacion, "IdConfirmacion"), datos.Decision, datos.CostoAprobado, datos.Observaciones);
    }
}
