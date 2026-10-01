using AlDia.Entity.Common;

namespace AlDia.Entity.Base;

public abstract class CatalogoEntity
{
    private readonly int _longitudNombre;
    private readonly int _longitudDescripcion;

    #region Propiedades
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    public bool Estado { get; private set; }
    #endregion

    #region Constructores
    protected CatalogoEntity(string nombre, string? descripcion, bool estado,
        int longitudNombre, int longitudDescripcion)
    {
        _longitudNombre = longitudNombre;
        _longitudDescripcion = longitudDescripcion;
        Nombre = Validacion.Texto(nombre, "Nombre", _longitudNombre);
        Descripcion = Validacion.Opcional(descripcion, "Descripcion", _longitudDescripcion);
        Estado = estado;
    }
    #endregion

    #region Metodos
    public void CambiarNombre(string nombre) =>
        Nombre = Validacion.Texto(nombre, "Nombre", _longitudNombre);
    public void CambiarDescripcion(string? descripcion) =>
        Descripcion = Validacion.Opcional(descripcion, "Descripcion", _longitudDescripcion);
    public void Activar() => Estado = true;
    public void Desactivar() => Estado = false;
    #endregion
}
