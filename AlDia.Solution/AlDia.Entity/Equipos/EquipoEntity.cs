using AlDia.Entity.Common;

namespace AlDia.Entity.Equipos;

public sealed class EquipoEntity
{
    #region Propiedades
    public int IdEquipo { get; private set; }
    public int IdCliente { get; private set; }
    public int IdTipoEquipo { get; private set; }
    public string Marca { get; private set; }
    public string? Modelo { get; private set; }
    public string? NumeroSerie { get; private set; }
    public string? Color { get; private set; }
    public string? Observaciones { get; private set; }
    public bool Estado { get; private set; }
    public string EstadoNombre => Estado ? "Activo" : "Inactivo";
    public DateTime FechaRegistro { get; }
    #endregion

    #region Constructores
    public EquipoEntity(int idCliente, int idTipoEquipo, string marca, string? modelo = null,
        string? numeroSerie = null, string? color = null, string? observaciones = null)
        : this(0, idCliente, idTipoEquipo, marca, modelo, numeroSerie, color, observaciones, true, DateTime.Now) { }

    // Conserva la firma utilizada por EquipoDal para reconstruir los datos.
    public EquipoEntity(int idEquipo, int idCliente, int idTipoEquipo, string marca, string? modelo,
        string? numeroSerie, string? color, string? observaciones, bool estado, DateTime fechaRegistro)
    {
        IdEquipo = Validacion.IdNuevoOExistente(idEquipo, "IdEquipo");
        IdCliente = Validacion.Id(idCliente, "IdCliente");
        IdTipoEquipo = Validacion.Id(idTipoEquipo, "IdTipoEquipo");
        Marca = Validacion.Texto(marca, "Marca", 50);
        Modelo = Validacion.Opcional(modelo, "Modelo", 100);
        NumeroSerie = Validacion.Opcional(numeroSerie, "NumeroSerie", 100);
        Color = Validacion.Opcional(color, "Color", 50);
        Observaciones = Validacion.Opcional(observaciones, "Observaciones", 500);
        Estado = estado;
        FechaRegistro = fechaRegistro;
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
    public void CambiarMarca(string marca) => Marca = Validacion.Texto(marca, "Marca", 50);
    public void CambiarModelo(string? modelo) => Modelo = Validacion.Opcional(modelo, "Modelo", 100);
    public void CambiarNumeroSerie(string? numeroSerie) => NumeroSerie = Validacion.Opcional(numeroSerie, "NumeroSerie", 100);
    public void CambiarColor(string? color) => Color = Validacion.Opcional(color, "Color", 50);
    public void CambiarObservaciones(string? observaciones) => Observaciones = Validacion.Opcional(observaciones, "Observaciones", 500);
    public void CambiarEstado(bool estado) => Estado = estado;
    public void Activar() => CambiarEstado(true);
    public void Desactivar() => CambiarEstado(false);

    public EquipoSolicitud CrearSolicitud() =>
        new(IdCliente, IdTipoEquipo, Marca, Modelo, NumeroSerie, Color, Observaciones, Estado, IdEquipo);
    #endregion
}

public sealed record EquipoSolicitud(int IdCliente, int IdTipoEquipo, string Marca, string? Modelo, string? NumeroSerie, string? Color, string? Observaciones, bool Estado = true, int IdEquipo = 0);
