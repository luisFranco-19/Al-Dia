using AlDia.Entity.Common;

namespace AlDia.Entity.Reparaciones;

public sealed class OrdenReparacionEntity
{
    #region Propiedades
    public int IdOrden { get; private set; }
    public string NumeroOrden { get; private set; } = string.Empty;
    public int IdEquipo { get; private set; }
    public int IdRecepcionista { get; private set; }
    public int IdEstado { get; private set; }
    public string EstadoNombre { get; private set; } = string.Empty;
    public int? IdTecnicoResponsable { get; }
    public DateTime FechaRecepcion { get; } = DateTime.Now;
    public string ProblemaReportado { get; private set; } = string.Empty;
    public string? ObservacionesRecepcion { get; private set; }
    public string? AccesoriosRecepcion { get; private set; }
    public DateTime? FechaEntrega { get; }
    public bool Anulada { get; }
    public decimal Total { get; }
    public decimal Pagado { get; }
    public decimal Saldo => Total - Pagado;
    // Proyeccion del cliente del equipo; no son columnas de OrdenesReparacion.
    public int IdCliente { get; }
    public string NombreCliente { get; } = string.Empty;
    public string ApellidoCliente { get; } = string.Empty;
    public string NombreCompletoCliente => $"{NombreCliente} {ApellidoCliente}";
    #endregion

    #region Constructores
    public OrdenReparacionEntity() { }

    public OrdenReparacionEntity(string numeroOrden, int idEquipo, int idRecepcionista,
        EstadoReparacionEntity estadoInicial, string problemaReportado,
        string? observacionesRecepcion = null, string? accesoriosRecepcion = null)
    {
        CambiarNumeroOrden(numeroOrden);
        CambiarEquipo(idEquipo);
        CambiarRecepcionista(idRecepcionista);
        CambiarEstadoInicial(estadoInicial);
        CambiarRecepcion(problemaReportado, observacionesRecepcion, accesoriosRecepcion);
    }

    public OrdenReparacionEntity(int idOrden, string numeroOrden, int idEquipo, int idRecepcionista,
        int idEstado, string estadoNombre, int? idTecnicoResponsable, DateTime fechaRecepcion,
        string problemaReportado, string? observacionesRecepcion, string? accesoriosRecepcion,
        DateTime? fechaEntrega, bool anulada, decimal total, decimal pagado, decimal saldo,
        int idCliente, string nombreCliente, string apellidoCliente)
    {
        IdOrden = Validacion.IdNuevoOExistente(idOrden, "IdOrden");
        if (string.IsNullOrWhiteSpace(numeroOrden))
            throw new ValidacionException("Debe ingresar el numero de orden.");
        NumeroOrden = numeroOrden.Trim();
        IdEquipo = Validacion.Id(idEquipo, "IdEquipo");
        IdRecepcionista = Validacion.Id(idRecepcionista, "IdRecepcionista");
        IdEstado = Validacion.Id(idEstado, "IdEstado");
        if (string.IsNullOrWhiteSpace(estadoNombre))
            throw new ValidacionException("Debe ingresar el nombre del estado.");
        EstadoNombre = estadoNombre.Trim();
        IdTecnicoResponsable = idTecnicoResponsable.HasValue ? Validacion.Id(idTecnicoResponsable.Value, "IdTecnicoResponsable") : null;
        FechaRecepcion = fechaRecepcion;
        if (string.IsNullOrWhiteSpace(problemaReportado))
            throw new ValidacionException("Debe ingresar el problema reportado.");
        ProblemaReportado = problemaReportado.Trim();
        ObservacionesRecepcion = string.IsNullOrWhiteSpace(observacionesRecepcion) ? null : observacionesRecepcion.Trim();
        AccesoriosRecepcion = string.IsNullOrWhiteSpace(accesoriosRecepcion) ? null : accesoriosRecepcion.Trim();
        FechaEntrega = fechaEntrega;
        Anulada = anulada;
        Total = Validacion.Dinero(total, "Total");
        Pagado = Validacion.Dinero(pagado, "Pagado");
        if (pagado > total || saldo != Saldo)
            throw new ValidacionException("El total, los pagos y el saldo de la orden no son coherentes.");
        IdCliente = Validacion.Id(idCliente, "IdCliente");
        if (string.IsNullOrWhiteSpace(nombreCliente))
            throw new ValidacionException("Debe ingresar el nombre del cliente.");
        if (string.IsNullOrWhiteSpace(apellidoCliente))
            throw new ValidacionException("Debe ingresar el apellido del cliente.");
        NombreCliente = nombreCliente.Trim();
        ApellidoCliente = apellidoCliente.Trim();
    }
    #endregion

    #region Metodos
    public void AsignarId(int id) => IdOrden = Validacion.AsignarId(IdOrden, id, "IdOrden");

    private void ExigirOrdenNueva()
    {
        if (IdOrden != 0)
            throw new ValidacionException("Estos datos se preparan antes de registrar la orden.");
    }

    public void CambiarNumeroOrden(string numeroOrden)
    {
        ExigirOrdenNueva();
        if (string.IsNullOrWhiteSpace(numeroOrden))
            throw new ValidacionException("Debe ingresar el numero de orden.");
        NumeroOrden = numeroOrden.Trim();
    }

    public void CambiarEquipo(int idEquipo)
    {
        ExigirOrdenNueva();
        IdEquipo = Validacion.Id(idEquipo, "IdEquipo");
    }

    public void CambiarRecepcionista(int idRecepcionista)
    {
        ExigirOrdenNueva();
        IdRecepcionista = Validacion.Id(idRecepcionista, "IdRecepcionista");
    }

    public void CambiarEstadoInicial(EstadoReparacionEntity estadoInicial)
    {
        ExigirOrdenNueva();
        ArgumentNullException.ThrowIfNull(estadoInicial);
        if (estadoInicial.Nombre != "En Revisión")
            throw new ValidacionException("Una orden nueva debe comenzar en revision.");
        int id = Validacion.Id(estadoInicial.IdEstado, "IdEstado");
        IdEstado = id;
        EstadoNombre = estadoInicial.Nombre;
    }

    public void CambiarRecepcion(string problemaReportado, string? observacionesRecepcion = null, string? accesoriosRecepcion = null)
    {
        if (Anulada || EstadoNombre != "En Revisión" || IdTecnicoResponsable.HasValue)
            throw new ValidacionException("La recepcion solo puede corregirse antes de que un tecnico tome la orden.");
        if (string.IsNullOrWhiteSpace(problemaReportado))
            throw new ValidacionException("Debe ingresar el problema reportado.");
        ProblemaReportado = problemaReportado.Trim();
        ObservacionesRecepcion = string.IsNullOrWhiteSpace(observacionesRecepcion) ? null : observacionesRecepcion.Trim();
        AccesoriosRecepcion = string.IsNullOrWhiteSpace(accesoriosRecepcion) ? null : accesoriosRecepcion.Trim();
    }

    public RecepcionSolicitud CrearSolicitud()
    {
        ExigirOrdenNueva();
        if (string.IsNullOrWhiteSpace(NumeroOrden))
            throw new ValidacionException("Debe ingresar el numero de orden.");
        Validacion.Id(IdEquipo, "IdEquipo");
        Validacion.Id(IdRecepcionista, "IdRecepcionista");
        Validacion.Id(IdEstado, "IdEstado");
        if (EstadoNombre != "En Revisión")
            throw new ValidacionException("Una orden nueva debe comenzar en revision.");
        if (string.IsNullOrWhiteSpace(ProblemaReportado))
            throw new ValidacionException("Debe ingresar el problema reportado.");
        return new(NumeroOrden, IdEquipo, ProblemaReportado, ObservacionesRecepcion, AccesoriosRecepcion);
    }
    public CorreccionRecepcionSolicitud CrearCorreccion()
    {
        if (string.IsNullOrWhiteSpace(ProblemaReportado))
            throw new ValidacionException("Debe ingresar el problema reportado.");
        return new(Validacion.Id(IdOrden, "IdOrden"), ProblemaReportado, ObservacionesRecepcion, AccesoriosRecepcion);
    }
    // Estado, tecnico, saldos y entrega se reconstruyen despues de la operacion en BLL/DAL.
    #endregion
}
