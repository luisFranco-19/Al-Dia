// Se conserva el namespace para el formulario de conexion existente.
namespace AlDia.Entity.Entity
{
    public class ConexionEntity
    {
        public string Servidor { get; set; } = string.Empty;
        public string BaseDatos { get; set; } = string.Empty;
        public string UsuarioSql { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
