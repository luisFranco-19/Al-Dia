using AlDia.Entity.Common;

namespace AlDia.Entity.Equipos;

public sealed class EquipoEntity
{
    #region Propiedades
    public int IdEquipo { get; private set; }
    public int IdCliente { get; private set; }
    public int IdTipoEquipo { get; private set; }
    public string Marca { get; private set; } = string.Empty;
    public string? Modelo { get; private set; }
    public string? NumeroSerie { get; private set; }
    public string? Color { get; private set; }
    public string? Observaciones { get; private set; }
    public bool Estado { get; private set; }
    public string EstadoNombre => Estado ? "Activo" : "Inactivo";
    public DateTime FechaRegistro { get; private set; } = DateTime.Now;
    #endregion

    #region Constructores
    public EquipoEntity() { }

    public EquipoEntity(int idCliente, int idTipoEquipo, string marca, string? modelo = null,
        string? numeroSerie = null, string? color = null, string? observaciones = null)
        : this(0, idCliente, idTipoEquipo, marca, modelo, numeroSerie, color, observaciones, true, DateTime.Now) { }

    // Conserva la firma utilizada por EquipoDal para reconstruir los datos.
    public EquipoEntity(int idEquipo, int idCliente, int idTipoEquipo, string marca, string? modelo,
        string? numeroSerie, string? color, string? observaciones, bool estado, DateTime fechaRegistro)
    {
        Validacion.IdNuevoOExistente(idEquipo, "IdEquipo");
        if (idEquipo > 0) AsignarId(idEquipo);
        CambiarCliente(idCliente);
        CambiarTipoEquipo(idTipoEquipo);
        CambiarMarca(marca);
        CambiarModelo(modelo);
        CambiarNumeroSerie(numeroSerie);
        CambiarColor(color);
        CambiarObservaciones(observaciones);
        CambiarEstado(estado);
        AsignarFechaRegistro(fechaRegistro);
    }
    #endregion

    #region Metodos
    public void AsignarId(int id)
    {
        Validacion.Id(id, "IdEquipo");
        if (IdEquipo != 0 && IdEquipo != id)
            throw new ValidacionException("No se puede cambiar el identificador de un equipo existente.");
        IdEquipo = id;
    }

    public void CambiarCliente(int idCliente) => IdCliente = Validacion.Id(idCliente, "IdCliente");
    public void CambiarTipoEquipo(int idTipoEquipo) => IdTipoEquipo = Validacion.Id(idTipoEquipo, "IdTipoEquipo");
    public void CambiarMarca(string marca)
    {
        if (string.IsNullOrWhiteSpace(marca))
            throw new ValidacionException("Debe ingresar la marca del equipo.");
        Marca = marca.Trim();
    }

    public void CambiarModelo(string? modelo)
    {
        Modelo = string.IsNullOrWhiteSpace(modelo) ? null : modelo.Trim();
    }

    public void CambiarNumeroSerie(string? numeroSerie)
    {
        NumeroSerie = string.IsNullOrWhiteSpace(numeroSerie) ? null : numeroSerie.Trim();
    }

    public void CambiarColor(string? color)
    {
        Color = string.IsNullOrWhiteSpace(color) ? null : color.Trim();
    }

    public void CambiarObservaciones(string? observaciones)
    {
        Observaciones = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim();
    }
    public void CambiarEstado(bool estado) => Estado = estado;
    public void Activar() => CambiarEstado(true);
    public void Desactivar() => CambiarEstado(false);
    public void AsignarFechaRegistro(DateTime fechaRegistro) => FechaRegistro = fechaRegistro;

    public EquipoSolicitud CrearSolicitud()
    {
        Validacion.Id(IdCliente, "IdCliente");
        Validacion.Id(IdTipoEquipo, "IdTipoEquipo");
        if (string.IsNullOrWhiteSpace(Marca))
            throw new ValidacionException("Debe ingresar la marca del equipo.");
        return new(IdCliente, IdTipoEquipo, Marca, Modelo, NumeroSerie, Color, Observaciones, Estado, IdEquipo);
    }
    #endregion
}

public sealed record EquipoSolicitud(int IdCliente, int IdTipoEquipo, string Marca, string? Modelo, string? NumeroSerie, string? Color, string? Observaciones, bool Estado = true, int IdEquipo = 0);
