using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class OrdenReparacionEntity
{
    #region Propiedades
    public int IdOrden { get; private set; }
    public string NumeroOrden { get; }
    public int IdEquipo { get; }
    public int IdRecepcionista { get; }
    public int IdEstado { get; }
    public string EstadoNombre { get; }
    public int? IdTecnicoResponsable { get; }
    public DateTime FechaRecepcion { get; }
    public string ProblemaReportado { get; private set; }
    public string? ObservacionesRecepcion { get; private set; }
    public string? AccesoriosRecepcion { get; private set; }
    public DateTime? FechaEntrega { get; }
    public bool Anulada { get; }
    public decimal Total { get; }
    public decimal Pagado { get; }
    public decimal Saldo => Total - Pagado;
    // Proyeccion del cliente del equipo; no son columnas de OrdenesReparacion.
    public int IdCliente { get; }
    public string NombreCliente { get; }
    public string ApellidoCliente { get; }
    public string NombreCompletoCliente => $"{NombreCliente} {ApellidoCliente}";
    #endregion

    #region Constructores
    public OrdenReparacionEntity(string numeroOrden, int idEquipo, int idRecepcionista,
        EstadoReparacionEntity estadoInicial, string problemaReportado,
        string? observacionesRecepcion = null, string? accesoriosRecepcion = null)
    {
        ArgumentNullException.ThrowIfNull(estadoInicial);
        if (estadoInicial.Nombre != "En Revisión")
            throw new ValidacionException("Una orden nueva debe comenzar en revision.");
        IdEstado = Validacion.Id(estadoInicial.IdEstado, "IdEstado");
        NumeroOrden = Validacion.Texto(numeroOrden, "NumeroOrden", 20);
        IdEquipo = Validacion.Id(idEquipo, "IdEquipo");
        IdRecepcionista = Validacion.Id(idRecepcionista, "IdRecepcionista");
        EstadoNombre = estadoInicial.Nombre;
        FechaRecepcion = DateTime.Now;
        ProblemaReportado = Validacion.Texto(problemaReportado, "ProblemaReportado", 500);
        ObservacionesRecepcion = Validacion.Opcional(observacionesRecepcion, "ObservacionesRecepcion", 500);
        AccesoriosRecepcion = Validacion.Opcional(accesoriosRecepcion, "AccesoriosRecepcion", 300);
        NombreCliente = string.Empty;
        ApellidoCliente = string.Empty;
    }

    public OrdenReparacionEntity(int idOrden, string numeroOrden, int idEquipo, int idRecepcionista,
        int idEstado, string estadoNombre, int? idTecnicoResponsable, DateTime fechaRecepcion,
        string problemaReportado, string? observacionesRecepcion, string? accesoriosRecepcion,
        DateTime? fechaEntrega, bool anulada, decimal total, decimal pagado, decimal saldo,
        int idCliente, string nombreCliente, string apellidoCliente)
    {
        IdOrden = Validacion.IdNuevoOExistente(idOrden, "IdOrden");
        NumeroOrden = Validacion.Texto(numeroOrden, "NumeroOrden", 20);
        IdEquipo = Validacion.Id(idEquipo, "IdEquipo");
        IdRecepcionista = Validacion.Id(idRecepcionista, "IdRecepcionista");
        IdEstado = Validacion.Id(idEstado, "IdEstado");
        EstadoNombre = Validacion.Texto(estadoNombre, "EstadoNombre", 50);
        IdTecnicoResponsable = idTecnicoResponsable.HasValue ? Validacion.Id(idTecnicoResponsable.Value, "IdTecnicoResponsable") : null;
        FechaRecepcion = fechaRecepcion;
        ProblemaReportado = Validacion.Texto(problemaReportado, "ProblemaReportado", 500);
        ObservacionesRecepcion = Validacion.Opcional(observacionesRecepcion, "ObservacionesRecepcion", 500);
        AccesoriosRecepcion = Validacion.Opcional(accesoriosRecepcion, "AccesoriosRecepcion", 300);
        FechaEntrega = fechaEntrega;
        Anulada = anulada;
        Total = Validacion.Dinero(total, "Total");
        Pagado = Validacion.Dinero(pagado, "Pagado");
        if (pagado > total || saldo != Saldo)
            throw new ValidacionException("El total, los pagos y el saldo de la orden no son coherentes.");
        IdCliente = Validacion.Id(idCliente, "IdCliente");
        NombreCliente = Validacion.Texto(nombreCliente, "NombreCliente", 100);
        ApellidoCliente = Validacion.Texto(apellidoCliente, "ApellidoCliente", 100);
    }
    #endregion

    #region Metodos
    public void AsignarId(int id) => IdOrden = Validacion.AsignarId(IdOrden, id, "IdOrden");

    public void CambiarRecepcion(string problemaReportado, string? observacionesRecepcion = null, string? accesoriosRecepcion = null)
    {
        if (Anulada || EstadoNombre != "En Revisión" || IdTecnicoResponsable.HasValue)
            throw new ValidacionException("La recepcion solo puede corregirse antes de que un tecnico tome la orden.");
        var problema = Validacion.Texto(problemaReportado, "ProblemaReportado", 500);
        var observaciones = Validacion.Opcional(observacionesRecepcion, "ObservacionesRecepcion", 500);
        var accesorios = Validacion.Opcional(accesoriosRecepcion, "AccesoriosRecepcion", 300);
        ProblemaReportado = problema;
        ObservacionesRecepcion = observaciones;
        AccesoriosRecepcion = accesorios;
    }

    public RecepcionSolicitud CrearSolicitud() => new(NumeroOrden, IdEquipo, ProblemaReportado, ObservacionesRecepcion, AccesoriosRecepcion);
    public CorreccionRecepcionSolicitud CrearCorreccion() =>
        new(Validacion.Id(IdOrden, "IdOrden"), ProblemaReportado, ObservacionesRecepcion, AccesoriosRecepcion);
    // Estado, tecnico, saldos y entrega se reconstruyen despues de la operacion en BLL/DAL.
    #endregion
}
