using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AlDia.BLL.Common;

namespace AlDia.UI.Formularios
{
    public partial class FrmDashsbor : Form
    {
        private readonly AplicacionBll? _aplicacion;
        public bool SolicitoCerrarSesion { get; private set; }

        public FrmDashsbor()
        {
            InitializeComponent();
        }

        public FrmDashsbor(AplicacionBll aplicacion) : this()
        {
            _aplicacion = aplicacion ?? throw new ArgumentNullException(nameof(aplicacion));
            var usuario = aplicacion.Sesion.UsuarioActual
                ?? throw new InvalidOperationException("Debe iniciar sesión antes de abrir el formulario principal.");
            lblUsuario.Text = usuario.NombreCompleto;
            lblRol.Text = usuario.Rol.ToString();
            ayudaControles.SetToolTip(lblUsuario, usuario.NombreCompleto);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && (_aplicacion is null || !_aplicacion.Sesion.EstaAutenticada)) Close();
        }

        private void btnCerrarSesion_Click(object? sender, EventArgs e)
        {
            _aplicacion?.Usuarios.CerrarSesion();
            SolicitoCerrarSesion = true;
            Close();
        }
    }
}
