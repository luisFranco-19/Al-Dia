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

        public void CambiarServidor(string servidor) => _servidor = Validacion.Texto(servidor, "Servidor", 128);
        public void CambiarBaseDatos(string baseDatos) => _baseDatos = Validacion.Texto(baseDatos, "BaseDatos", 128);
        public void CambiarUsuarioSql(string usuarioSql) => _usuarioSql = Validacion.Texto(usuarioSql, "UsuarioSql", 128);
        public void CambiarPassword(string password) => _password = ValidarPassword(password);

        public void CambiarDatosConexion(string servidor, string baseDatos, string usuarioSql, string password)
        {
            var servidorValidado = Validacion.Texto(servidor, "Servidor", 128);
            var baseValidada = Validacion.Texto(baseDatos, "BaseDatos", 128);
            var usuarioValidado = Validacion.Texto(usuarioSql, "UsuarioSql", 128);
            var passwordValidado = ValidarPassword(password);
            _servidor = servidorValidado;
            _baseDatos = baseValidada;
            _usuarioSql = usuarioValidado;
            _password = passwordValidado;
        }

        private static string ValidarPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ValidacionException("La contrasena SQL es obligatoria.");
            return password; // Se conservan exactamente los espacios y caracteres de la clave.
        }
    }
}
