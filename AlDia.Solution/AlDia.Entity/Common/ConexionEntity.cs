using AlDia.Entity.Common;
using System.Text.Json.Serialization;

// Se conserva el namespace y los inicializadores para los formularios y el JSON existentes.
namespace AlDia.Entity.Entity
{
    public sealed class ConexionEntity
    {
        private string _servidor = string.Empty;
        private string _baseDatos = string.Empty;
        private string _usuarioSql = string.Empty;
        private string _password = string.Empty;

        public string Servidor { get => _servidor; init => CambiarServidor(value); }
        public string BaseDatos { get => _baseDatos; init => CambiarBaseDatos(value); }
        public string UsuarioSql { get => _usuarioSql; init => CambiarUsuarioSql(value); }
        public string Password { get => _password; init => CambiarPassword(value); }

        public ConexionEntity() { }

        [JsonConstructor]
        public ConexionEntity(string servidor, string baseDatos, string usuarioSql, string password)
        {
            CambiarDatosConexion(servidor, baseDatos, usuarioSql, password);
        }

        public void CambiarServidor(string servidor)
        {
            if (string.IsNullOrWhiteSpace(servidor))
                throw new ValidacionException("Debe ingresar el servidor SQL.");
            _servidor = servidor.Trim();
        }

        public void CambiarBaseDatos(string baseDatos)
        {
            if (string.IsNullOrWhiteSpace(baseDatos))
                throw new ValidacionException("Debe ingresar la base de datos.");
            _baseDatos = baseDatos.Trim();
        }

        public void CambiarUsuarioSql(string usuarioSql)
        {
            if (string.IsNullOrWhiteSpace(usuarioSql))
                throw new ValidacionException("Debe ingresar el usuario SQL.");
            _usuarioSql = usuarioSql.Trim();
        }
        public void CambiarPassword(string password) => _password = ValidarPassword(password);

        public void CambiarDatosConexion(string servidor, string baseDatos, string usuarioSql, string password)
        {
            if (string.IsNullOrWhiteSpace(servidor))
                throw new ValidacionException("Debe ingresar el servidor SQL.");
            if (string.IsNullOrWhiteSpace(baseDatos))
                throw new ValidacionException("Debe ingresar la base de datos.");
            if (string.IsNullOrWhiteSpace(usuarioSql))
                throw new ValidacionException("Debe ingresar el usuario SQL.");
            var passwordValidado = ValidarPassword(password);
            _servidor = servidor.Trim();
            _baseDatos = baseDatos.Trim();
            _usuarioSql = usuarioSql.Trim();
            _password = passwordValidado;
        }

        private static string ValidarPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ValidacionException("La contrasena SQL es obligatoria.");
            return password; // Se conservan exactamente los espacios y caracteres de la clave.
        }
    }
}
