using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AlDia.BLL.Common;
using AlDia.UI.Formularios.Hijos.Catalogos;
using AlDia.UI.Formularios.Hijos.Clientes;
using AlDia.UI.Formularios.Hijos.Equipos;
using AlDia.UI.Formularios.Hijos.Inventario;
using AlDia.UI.Formularios.Hijos.OrdenesServicio;
using AlDia.UI.Formularios.Hijos.Pagos;
using AlDia.UI.Formularios.Hijos.Reportes;
using AlDia.UI.Formularios.Hijos.Usuarios;
using FontAwesome.Sharp;

namespace AlDia.UI.Formularios
{
    public partial class FrmDashsbor : Form
    {
        private readonly AplicacionBll? _aplicacion;
        private readonly Dictionary<Type, Form> _formulariosModulos = new();
        private Form? _formularioActivo;
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
            btnUsuarios.Enabled = btnCatalogos.Enabled = usuario.Rol == AlDia.Entity.Seguridad.RolUsuario.Administrador;
            btnPagos.Enabled = usuario.Rol is AlDia.Entity.Seguridad.RolUsuario.Administrador or AlDia.Entity.Seguridad.RolUsuario.Recepcionista;
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

        private void btnClientes_Click(object? sender, EventArgs e)
        {
            AbrirModulo<FrmClientes>("Clientes", "GESTIÓN DEL TALLER", btnClientes);
        }

        private void btnEquipos_Click(object? sender, EventArgs e)
        {
            AbrirModulo<FrmEquipos>("Equipos", "GESTIÓN DEL TALLER", btnEquipos);
        }

        private void btnOrdenes_Click(object? sender, EventArgs e)
        {
            AbrirModulo<FrmOrdenesServicio>("Órdenes de servicio", "GESTIÓN DEL TALLER", btnOrdenes);
        }

        private void btnInventario_Click(object? sender, EventArgs e)
        {
            AbrirModulo<FrmInventario>("Inventario", "GESTIÓN DEL TALLER", btnInventario);
        }

        private void btnPagos_Click(object? sender, EventArgs e)
        {
            AbrirModulo<FrmPagos>("Pagos", "GESTIÓN DEL TALLER", btnPagos);
        }

        private void btnCatalogos_Click(object? sender, EventArgs e)
        {
            AbrirModulo<FrmCatalogos>("Catálogos", "ADMINISTRACIÓN", btnCatalogos);
        }

        private void btnReportes_Click(object? sender, EventArgs e)
        {
            AbrirModulo<FrmReportes>("Reportes", "GESTIÓN DEL TALLER", btnReportes);
        }

        private void btnUsuarios_Click(object? sender, EventArgs e)
        {
            AbrirModulo<FrmUsuarios>("Usuarios", "ADMINISTRACIÓN", btnUsuarios);
        }

        // Los módulos comparten los servicios y la sesión del dashboard.
        private void AbrirModulo<TFormulario>(string titulo, string seccion, IconButton boton)
            where TFormulario : Form, new()
        {
            if (!_formulariosModulos.TryGetValue(typeof(TFormulario), out var formulario) || formulario.IsDisposed)
            {
                formulario = new TFormulario
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                if (formulario is IModuloConServicios moduloNuevo && _aplicacion is not null)
                {
                    moduloNuevo.ConfigurarServicios(_aplicacion);
                }
                _formulariosModulos[typeof(TFormulario)] = formulario;
                panelCuerpo.Controls.Add(formulario);
            }
            else if (formulario is IModuloConServicios moduloExistente && _aplicacion is not null)
            {
                moduloExistente.ConfigurarServicios(_aplicacion);
            }

            panelCuerpo.SuspendLayout();
            try
            {
                _formularioActivo?.Hide();
                tablaContenido.Visible = false;
                panelCuerpo.AutoScrollPosition = Point.Empty;
                panelCuerpo.AutoScroll = false;
                formulario.Show();
                formulario.BringToFront();
                _formularioActivo = formulario;
                lblTitulo.Text = titulo;
                lblSeccion.Text = seccion;
                MarcarSeccion(boton);
            }
            finally
            {
                panelCuerpo.ResumeLayout(true);
            }
        }

        private void btnInicio_Click(object? sender, EventArgs e)
        {
            _formularioActivo?.Hide();
            _formularioActivo = null;
            tablaContenido.Visible = true;
            panelCuerpo.AutoScroll = true;
            lblTitulo.Text = "Inicio";
            lblSeccion.Text = "PANEL GENERAL";
            MarcarSeccion(btnInicio);
        }

        private void MarcarSeccion(IconButton seleccionado)
        {
            var azul = Color.FromArgb(37, 99, 235);
            var texto = Color.FromArgb(20, 43, 78);
            var gris = Color.FromArgb(103, 116, 137);
            var fondo = Color.FromArgb(234, 240, 252);
            foreach (var boton in new[] { btnInicio, btnClientes, btnEquipos, btnOrdenes,
                btnInventario, btnPagos, btnReportes, btnCatalogos, btnUsuarios })
            {
                bool activo = boton == seleccionado;
                boton.BackColor = activo ? fondo : Color.White;
                boton.ForeColor = activo ? azul : texto;
                boton.IconColor = activo ? azul : gris;
            }
        }
    }
}
