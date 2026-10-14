using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class DiagnosticoEntity
{
    public int IdDiagnostico { get; private set; }
    public int IdOrden { get; private set; }
    public int IdTecnico { get; private set; }
    public DateTime FechaDiagnostico { get; } = DateTime.Now;
    public string ProblemaEncontrado { get; private set; } = string.Empty;
    public string ReparacionPropuesta { get; private set; } = string.Empty;
    public decimal CostoEstimado { get; private set; }
    public string? Observaciones { get; private set; }

    public DiagnosticoEntity() { }

    public DiagnosticoEntity(int idOrden, int idTecnico, string problemaEncontrado,
        string reparacionPropuesta, decimal costoEstimado, string? observaciones = null)
        : this(0, idOrden, idTecnico, DateTime.Now, problemaEncontrado, reparacionPropuesta, costoEstimado, observaciones) { }

    public DiagnosticoEntity(int idDiagnostico, int idOrden, int idTecnico, DateTime fechaDiagnostico,
        string problemaEncontrado, string reparacionPropuesta, decimal costoEstimado, string? observaciones)
    {
        Validacion.IdNuevoOExistente(idDiagnostico, "IdDiagnostico");
        if (idDiagnostico > 0) AsignarId(idDiagnostico);
        CambiarOrden(idOrden);
        CambiarTecnico(idTecnico);
        FechaDiagnostico = fechaDiagnostico;
        CambiarDiagnostico(problemaEncontrado, reparacionPropuesta, costoEstimado, observaciones);
    }

    public void AsignarId(int id) => IdDiagnostico = Validacion.AsignarId(IdDiagnostico, id, "IdDiagnostico");

    public void CambiarOrden(int idOrden)
    {
        int id = Validacion.Id(idOrden, "IdOrden");
        if (IdOrden != 0 && IdOrden != id)
            throw new ValidacionException("No se puede trasladar un diagnóstico a otra orden.");
        IdOrden = id;
    }

    public void CambiarTecnico(int idTecnico)
    {
        int id = Validacion.Id(idTecnico, "IdTecnico");
        if (IdTecnico != 0 && IdTecnico != id)
            throw new ValidacionException("No se puede sustituir el técnico del diagnóstico.");
        IdTecnico = id;
    }

    public void CambiarDiagnostico(string problemaEncontrado, string reparacionPropuesta, decimal costoEstimado, string? observaciones = null)
    {
        if (string.IsNullOrWhiteSpace(problemaEncontrado))
            throw new ValidacionException("Debe ingresar el problema encontrado.");
        if (string.IsNullOrWhiteSpace(reparacionPropuesta))
            throw new ValidacionException("Debe ingresar la reparacion propuesta.");
        string problema = problemaEncontrado.Trim();
        string reparacion = reparacionPropuesta.Trim();
        var costo = Validacion.Dinero(costoEstimado, "CostoEstimado");
        string? notas = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim();
        ProblemaEncontrado = problema;
        ReparacionPropuesta = reparacion;
        CostoEstimado = costo;
        Observaciones = notas;
    }

    public DiagnosticoSolicitud CrearSolicitud()
    {
        Validacion.Id(IdOrden, "IdOrden");
        Validacion.Id(IdTecnico, "IdTecnico");
        if (string.IsNullOrWhiteSpace(ProblemaEncontrado))
            throw new ValidacionException("Debe ingresar el problema encontrado.");
        if (string.IsNullOrWhiteSpace(ReparacionPropuesta))
            throw new ValidacionException("Debe ingresar la reparacion propuesta.");
        return new(IdOrden, ProblemaEncontrado, ReparacionPropuesta, CostoEstimado, Observaciones);
    }
    public CorreccionDiagnosticoSolicitud CrearCorreccion()
    {
        var datos = CrearSolicitud();
        return new(Validacion.Id(IdDiagnostico, "IdDiagnostico"), datos.ProblemaEncontrado, datos.ReparacionPropuesta, datos.CostoEstimado, datos.Observaciones);
    }
}
