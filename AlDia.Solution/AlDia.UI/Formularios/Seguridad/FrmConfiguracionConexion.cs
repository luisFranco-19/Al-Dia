using AlDia.BLL.Common;
using AlDia.DAL.Common;
using AlDia.Entity.Entity;
using AlDia.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AlDia.UI.Formularios.Seguridad
{
    public partial class FrmConfiguracionConexion : Form
    {
        public FrmConfiguracionConexion()
        {
            InitializeComponent();
        }

        private bool ValidarCampos()
        {
            errorIcon.Clear();

            bool valido = true;

            if (!ValidationHelper.Requerido(txtServidor, errorIcon, "Debe ingresar el servidor."))
                valido = false;

            if (!ValidationHelper.Requerido(txtBaseDatos, errorIcon, "Debe ingresar la base de datos."))
                valido = false;

            if (!ValidationHelper.Requerido(txtUsuarioSql, errorIcon, "Debe ingresar el usuario."))
                valido = false;

            if (!ValidationHelper.Requerido(txtClave, errorIcon, "Debe ingresar la contraseña."))
                valido = false;

            return valido;
        }

        private void Cancelar()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private async void btnProbar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            ConexionBll conexionBLL = new();

            bool ok = await conexionBLL.ProbarConexion(
                txtServidor.Text.Trim(),
                txtBaseDatos.Text.Trim(),
                txtUsuarioSql.Text.Trim(),
                txtClave.Text);

            if (ok)
            {
                MessageBox.Show("Conexión exitosa", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnGuardar.Enabled = true;
                txtServidor.Enabled = false;
                txtBaseDatos.Enabled = false;
                txtUsuarioSql.Enabled = false;
                txtClave.Enabled = false;
            }
            else
            {
                MessageBox.Show("No fue posible conectar", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGuardar.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ConexionEntity config = new()
            {
                Servidor = txtServidor.Text.Trim(),
                BaseDatos = txtBaseDatos.Text.Trim(),
                UsuarioSql = txtUsuarioSql.Text.Trim(),
                Password = txtClave.Text
            };

            ConfiguracionDal dal = new();

            bool resultado = dal.Guardar(config);

            if (!resultado)
            {
                MessageBox.Show("No fue posible guardar la configuración", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Configuarción guardada correctamente", "Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureCerrar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void FrmConfiguracionConexion_Shown(object sender, EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 25);
            RedondearControlHelper.RedondearControl(panelContenido, 25);
            RedondearControlHelper.RedondearControl(panelServidor, 15);
            RedondearControlHelper.RedondearControl(panelBD, 15);
            RedondearControlHelper.RedondearControl(panelUser, 15);
            RedondearControlHelper.RedondearControl(panelContra, 15);
        }
    }
}
