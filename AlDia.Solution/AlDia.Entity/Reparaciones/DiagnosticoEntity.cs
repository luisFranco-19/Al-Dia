using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class DiagnosticoEntity
{
    public int IdDiagnostico { get; private set; }
    public int IdOrden { get; }
    public int IdTecnico { get; }
    public DateTime FechaDiagnostico { get; }
    public string ProblemaEncontrado { get; private set; }
    public string ReparacionPropuesta { get; private set; }
    public decimal CostoEstimado { get; private set; }
    public string? Observaciones { get; private set; }

    public DiagnosticoEntity(int idOrden, int idTecnico, string problemaEncontrado,
        string reparacionPropuesta, decimal costoEstimado, string? observaciones = null)
        : this(0, idOrden, idTecnico, DateTime.Now, problemaEncontrado, reparacionPropuesta, costoEstimado, observaciones) { }

    public DiagnosticoEntity(int idDiagnostico, int idOrden, int idTecnico, DateTime fechaDiagnostico,
        string problemaEncontrado, string reparacionPropuesta, decimal costoEstimado, string? observaciones)
    {
        IdDiagnostico = Validacion.IdNuevoOExistente(idDiagnostico, "IdDiagnostico");
        IdOrden = Validacion.Id(idOrden, "IdOrden");
        IdTecnico = Validacion.Id(idTecnico, "IdTecnico");
        FechaDiagnostico = fechaDiagnostico;
        ProblemaEncontrado = Validacion.Texto(problemaEncontrado, "ProblemaEncontrado", 1000);
        ReparacionPropuesta = Validacion.Texto(reparacionPropuesta, "ReparacionPropuesta", 1000);
        CostoEstimado = Validacion.Dinero(costoEstimado, "CostoEstimado");
        Observaciones = Validacion.Opcional(observaciones, "Observaciones", 1000);
    }

    public void AsignarId(int id) => IdDiagnostico = Validacion.AsignarId(IdDiagnostico, id, "IdDiagnostico");

    public void CambiarDiagnostico(string problemaEncontrado, string reparacionPropuesta, decimal costoEstimado, string? observaciones = null)
    {
        var problema = Validacion.Texto(problemaEncontrado, "ProblemaEncontrado", 1000);
        var reparacion = Validacion.Texto(reparacionPropuesta, "ReparacionPropuesta", 1000);
        var costo = Validacion.Dinero(costoEstimado, "CostoEstimado");
        var notas = Validacion.Opcional(observaciones, "Observaciones", 1000);
        ProblemaEncontrado = problema;
        ReparacionPropuesta = reparacion;
        CostoEstimado = costo;
        Observaciones = notas;
    }

    public DiagnosticoSolicitud CrearSolicitud() => new(IdOrden, ProblemaEncontrado, ReparacionPropuesta, CostoEstimado, Observaciones);
    public CorreccionDiagnosticoSolicitud CrearCorreccion() =>
        new(Validacion.Id(IdDiagnostico, "IdDiagnostico"), ProblemaEncontrado, ReparacionPropuesta, CostoEstimado, Observaciones);
}
