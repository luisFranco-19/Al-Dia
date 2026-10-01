using AlDia.UI.Formularios;
using AlDia.UI.Formularios.Seguridad;
using AlDia.DAL.Common;
using AlDia.BLL.Common;

namespace AlDia.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var configuracion = new ConfiguracionDal();
            if (!configuracion.ExisteConfiguracion())
            {
                if (!ConfigurarConexion(null)) return;
            }
            else if (!CargarConexion() && !ConfigurarConexion(null)) return;

            // Una sola aplicacion comparte la sesion y los servicios con los formularios.
            var aplicacion = AplicacionBll.DesdeConexionActual();
            while (true)
            {
                using (var login = new FrmLogin(aplicacion.Usuarios))
                {
                    if (login.ShowDialog() != DialogResult.OK || !aplicacion.Sesion.EstaAutenticada)
                        return;
                }

                using var principal = new FrmDashsbor(aplicacion);
                try { Application.Run(principal); }
                finally { aplicacion.Usuarios.CerrarSesion(); }
                if (!principal.SolicitoCerrarSesion) return;
            }
        }

        private static bool ConfigurarConexion(IWin32Window? propietario)
        {
            using var formulario = new FrmConfiguracionConexion();
            if (formulario.ShowDialog(propietario) != DialogResult.OK) return false;
            return CargarConexion();
        }

        private static bool CargarConexion()
        {
            try
            {
                var config = new ConfiguracionDal().Obtener();
                if (config is null) throw new InvalidOperationException("No se encontró la configuración de conexión.");
                Conexion.CadenaConexion = Conexion.ConstruirCadena(config);
                return true;
            }
            catch
            {
                MessageBox.Show("No fue posible leer la configuración. Configura la conexión para continuar.",
                    "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
    }
}
