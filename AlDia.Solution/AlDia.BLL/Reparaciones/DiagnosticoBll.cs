using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Reparaciones;
namespace AlDia.BLL.Reparaciones;
public sealed class DiagnosticoBll(IDiagnosticoDal datos, SesionUsuario sesion)
{
    public Task<int> RegistrarAsync(DiagnosticoSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdOrden = Validacion.Id(s.IdOrden, "IdOrden"), ProblemaEncontrado = Validacion.Texto(s.ProblemaEncontrado, "ProblemaEncontrado", 1000), ReparacionPropuesta = Validacion.Texto(s.ReparacionPropuesta, "ReparacionPropuesta", 1000), CostoEstimado = Validacion.Dinero(s.CostoEstimado, "CostoEstimado"), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 1000) };
        return datos.RegistrarAsync(actor, validada, ct);
    }
    public Task CorregirAsync(CorreccionDiagnosticoSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(s);
        var validada = s with { IdDiagnostico = Validacion.Id(s.IdDiagnostico, "IdDiagnostico"), ProblemaEncontrado = Validacion.Texto(s.ProblemaEncontrado, "ProblemaEncontrado", 1000), ReparacionPropuesta = Validacion.Texto(s.ReparacionPropuesta, "ReparacionPropuesta", 1000), CostoEstimado = Validacion.Dinero(s.CostoEstimado, "CostoEstimado"), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 1000) };
        return datos.CorregirAsync(actor, validada, ct);
    }
    public Task RetirarAsync(int idDiagnostico, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        return datos.RetirarAsync(actor, Validacion.Id(idDiagnostico, "IdDiagnostico"), ct);
    }
    public Task<int> RegistrarDecisionAsync(DecisionClienteSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(s);
        if ((s.Decision == DecisionCliente.Aprobada && s.CostoAprobado is null) || (s.Decision == DecisionCliente.Rechazada && s.CostoAprobado is not null))
            throw new ValidacionException("Indique un costo al aprobar; al rechazar debe quedar sin valor.");
        if (s.CostoAprobado.HasValue) Validacion.Dinero(s.CostoAprobado.Value, "CostoAprobado");
        var validada = s with { IdDiagnostico = Validacion.Id(s.IdDiagnostico, "IdDiagnostico"), Decision = Validacion.Enumeracion(s.Decision, "Decision"), CostoAprobado = s.CostoAprobado, Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 500) };
        return datos.RegistrarDecisionAsync(actor, validada, ct);
    }
    public Task CorregirDecisionAsync(CorreccionDecisionClienteSolicitud s, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(s);
        if ((s.Decision == DecisionCliente.Aprobada && s.CostoAprobado is null) || (s.Decision == DecisionCliente.Rechazada && s.CostoAprobado is not null))
            throw new ValidacionException("Indique un costo al aprobar; al rechazar debe quedar sin valor.");
        if (s.CostoAprobado.HasValue) Validacion.Dinero(s.CostoAprobado.Value, "CostoAprobado");
        var validada = s with { IdConfirmacion = Validacion.Id(s.IdConfirmacion, "IdConfirmacion"), Decision = Validacion.Enumeracion(s.Decision, "Decision"), CostoAprobado = s.CostoAprobado, Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 500) };
        return datos.CorregirDecisionAsync(actor, validada, ct);
    }
    public Task RetirarDecisionAsync(int idConfirmacion, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        return datos.RetirarDecisionAsync(actor, Validacion.Id(idConfirmacion, "IdConfirmacion"), ct);
    }
    public async Task<int> RegistrarEntidadAsync(DiagnosticoEntity diagnostico, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Tecnico);
        ArgumentNullException.ThrowIfNull(diagnostico);
        if (diagnostico.IdDiagnostico != 0) throw new ValidacionException("El diagnostico ya fue registrado.");
        if (diagnostico.IdTecnico != actor) throw new PermisoException("El diagnostico debe corresponder al tecnico autenticado.");
        int id = await RegistrarAsync(diagnostico.CrearSolicitud(), ct);
        diagnostico.AsignarId(id);
        return id;
    }
    public Task CorregirEntidadAsync(DiagnosticoEntity diagnostico, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(diagnostico);
        return CorregirAsync(diagnostico.CrearCorreccion(), ct);
    }
    public async Task<int> RegistrarDecisionEntidadAsync(ConfirmacionEntity confirmacion, CancellationToken ct = default)
    {
        int actor = sesion.Exigir(RolUsuario.Recepcionista);
        ArgumentNullException.ThrowIfNull(confirmacion);
        if (confirmacion.IdConfirmacion != 0) throw new ValidacionException("La decision ya fue registrada.");
        if (confirmacion.IdUsuario != actor) throw new PermisoException("La decision debe corresponder al usuario autenticado.");
        int id = await RegistrarDecisionAsync(confirmacion.CrearSolicitud(), ct);
        confirmacion.AsignarId(id);
        return id;
    }
    public Task CorregirDecisionEntidadAsync(ConfirmacionEntity confirmacion, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(confirmacion);
        return CorregirDecisionAsync(confirmacion.CrearCorreccion(), ct);
    }

}
