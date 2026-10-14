using AlDia.Entity.Common;

namespace AlDia.Entity.Base;

public abstract class CatalogoEntity
{
    #region Propiedades
    //Defino las propriedades de la clase CatalogoEntity.
    //Nombre: Nombre del catálogo.
    public string Nombre { get; private set; } = string.Empty;
    //Descripcion: Descripción del catálogo.
    public string? Descripcion { get; private set; }
    //Estado: Estado del catálogo.
    public bool Estado { get; private set; }
    // Nombre del estado del catálogo.
    public string EstadoNombre => Estado ? "Activo" : "Inactivo";
    #endregion

    #region Constructores
    // Los datos se completan con los métodos Cambiar...
    protected CatalogoEntity() { }

    //Constructor de la clase CatalogoEntity.
    protected CatalogoEntity(string nombre, string? descripcion, bool estado) : this()
    {
        CambiarNombre(nombre);
        CambiarDescripcion(descripcion);
        CambiarEstado(estado);
    }
    #endregion

    #region Metodos
    //Metodos para cambiar el nombre, la descripción y el estado del catálogo.
    public void CambiarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ValidacionException("Debe ingresar el nombre del catalogo.");
        Nombre = nombre.Trim();
    }

    public void CambiarDescripcion(string? descripcion)
    {
        Descripcion = string.IsNullOrWhiteSpace(descripcion) ? null : descripcion.Trim();
    }
    //el estado va ser igual al estado.
    public void CambiarEstado(bool estado) => Estado = estado;
    //Se activa el estado.
    public void Activar() => CambiarEstado(true);
    public void Desactivar() => CambiarEstado(false);

    protected void ValidarCatalogo()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
            throw new ValidacionException("Debe ingresar el nombre del catalogo.");
    }
    #endregion
}
