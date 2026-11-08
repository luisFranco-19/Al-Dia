using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlDia.BLL.Common;
using AlDia.Entity.Reparaciones;
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
        private CancellationTokenSource? _cargaDashboard;
        private TableLayoutPanel? _filasOrdenes;
        private readonly Label _lblOrdenesDetalle = new();
        private readonly Label _lblReparacionDetalle = new();
        private readonly Label _lblEntregaDetalle = new();
        public bool SolicitoCerrarSesion { get; private set; }

        public FrmDashsbor()
        {
            InitializeComponent();
            ConfigurarResumenVisual();
        }

        public FrmDashsbor(AplicacionBll aplicacion) : this()
        {
            _aplicacion = aplicacion ?? throw new ArgumentNullException(nameof(aplicacion));
            var usuario = aplicacion.Sesion.UsuarioActual
                ?? throw new InvalidOperationException("Debe iniciar sesión antes de abrir el formulario principal.");
            lblUsuario.Text = usuario.NombreCompleto;
            lblRol.Text = usuario.Rol.ToString();
            ayudaControles.SetToolTip(lblUsuario, usuario.NombreCompleto);
            bool administrador = usuario.Rol == AlDia.Entity.Seguridad.RolUsuario.Administrador;
            bool recepcionista = usuario.Rol == AlDia.Entity.Seguridad.RolUsuario.Recepcionista;
            bool adminORecepcion = administrador || recepcionista;
            btnUsuarios.Visible = administrador;
            btnCatalogos.Visible = administrador;
            btnInventario.Visible = administrador;
            btnClientes.Visible = adminORecepcion;
            btnEquipos.Visible = adminORecepcion;
            btnPagos.Visible = adminORecepcion;
            lblAdministracion.Visible = administrador;
            AcomodarMenu(administrador, recepcionista);
        }

        private void AcomodarMenu(bool administrador, bool recepcionista)
        {
            int arriba = 52;
            Posicionar(btnInicio, true, ref arriba);
            Posicionar(btnClientes, administrador || recepcionista, ref arriba);
            Posicionar(btnEquipos, administrador || recepcionista, ref arriba);
            Posicionar(btnOrdenes, true, ref arriba);
            Posicionar(btnInventario, administrador, ref arriba);
            Posicionar(btnPagos, administrador || recepcionista, ref arriba);
            Posicionar(btnReportes, true, ref arriba);

            lblAdministracion.Visible = administrador;
            if (!administrador) return;
            lblAdministracion.Top = arriba + 4;
            arriba += 34;
            Posicionar(btnCatalogos, true, ref arriba);
            Posicionar(btnUsuarios, true, ref arriba);
        }

        private static void Posicionar(IconButton boton, bool visible, ref int arriba)
        {
            boton.Visible = visible;
            if (!visible) return;
            boton.Top = arriba;
            arriba += boton.Height + 4;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && (_aplicacion is null || !_aplicacion.Sesion.EstaAutenticada)) Close();
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            lblPie.Text = "AL DÍA  /  Cargando datos del taller...";
            await CargarDashboardAsync();
        }

        private void ConfigurarResumenVisual()
        {
            lblResumen.Text = "Resumen financiero y operativo";
            tablaContenido.RowStyles[1].SizeType = SizeType.Absolute;
            tablaContenido.RowStyles[1].Height = 144F;
            tarjetaResumen.Padding = new Padding(8);
            tablaIndicadores.Controls.Clear();
            tablaIndicadores.ColumnStyles.Clear();
            tablaIndicadores.ColumnCount = 3;
            for (int i = 0; i < 3; i++)
                tablaIndicadores.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / 3F));
            tablaIndicadores.RowStyles.Clear();
            tablaIndicadores.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            ConfigurarTarjetaExistente(tarjetaOrdenes, lblOrdenesTitulo, lblOrdenesValor, _lblOrdenesDetalle,
                "Ingresos del mes", Color.FromArgb(5, 150, 105));
            ConfigurarTarjetaExistente(tarjetaReparacion, lblReparacionTitulo, lblReparacionValor, _lblReparacionDetalle,
                "Órdenes activas", Color.FromArgb(20, 43, 78));
            ConfigurarTarjetaExistente(tarjetaEntrega, lblEntregaTitulo, lblEntregaValor, _lblEntregaDetalle,
                "Listas para entregar", Color.FromArgb(37, 99, 235));
            tablaIndicadores.Controls.Add(tarjetaOrdenes, 0, 0);
            tablaIndicadores.Controls.Add(tarjetaReparacion, 1, 0);
            tablaIndicadores.Controls.Add(tarjetaEntrega, 2, 0);
        }

        private static void ConfigurarTarjetaExistente(Panel tarjeta, Label titulo, Label valor, Label detalle,
            string textoTitulo, Color colorValor)
        {
            tarjeta.BackColor = Color.White;
            tarjeta.Margin = new Padding(4, 0, 4, 0);
            titulo.Text = textoTitulo;
            titulo.Location = new Point(16, 8);
            titulo.Size = new Size(250, 22);
            titulo.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            valor.Location = new Point(16, 32);
            valor.Size = new Size(250, 42);
            valor.Font = new Font("Segoe UI Semibold", 19F, FontStyle.Bold);
            valor.ForeColor = colorValor;
            ConfigurarEtiquetaResumen(detalle, 8.5F, FontStyle.Regular, Color.FromArgb(103, 116, 137));
            tarjeta.Controls.Add(detalle);
            detalle.Location = new Point(16, 78);
            detalle.Size = new Size(250, 20);
            detalle.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
        }

        private static void ConfigurarEtiquetaResumen(Label etiqueta, float tamano, FontStyle estilo, Color color)
        {
            etiqueta.AutoEllipsis = true;
            etiqueta.BackColor = Color.Transparent;
            etiqueta.ForeColor = color;
            etiqueta.Font = new Font("Segoe UI", tamano, estilo);
            etiqueta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
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
            _ = CargarDashboardAsync();
        }

        private async Task CargarDashboardAsync()
        {
            if (_aplicacion is null || IsDisposed || !IsHandleCreated) return;
            _cargaDashboard?.Cancel();
            _cargaDashboard?.Dispose();
            _cargaDashboard = new CancellationTokenSource();
            var ct = _cargaDashboard.Token;
            try
            {
                var ordenes = await _aplicacion.Ordenes.ConsultarAsync(ct: ct);
                ct.ThrowIfCancellationRequested();
                var equipos = await _aplicacion.Equipos.ConsultarAsync(soloActivos: false, ct: ct);
                ct.ThrowIfCancellationRequested();
                var usuario = _aplicacion.Sesion.UsuarioActual!;
                var ordenesVigentes = ordenes.Where(x => !x.Anulada).ToList();
                bool puedeVerFinanzas = usuario.PermisoRol is AlDia.Entity.Seguridad.RolUsuario.Administrador
                    or AlDia.Entity.Seguridad.RolUsuario.Recepcionista;
                if (puedeVerFinanzas)
                {
                    var pagos = await _aplicacion.Pagos.ConsultarAsync(ct: ct);
                    ct.ThrowIfCancellationRequested();
                    var hoy = DateTime.Today;
                    var ordenesMes = ordenesVigentes.Where(x =>
                        x.FechaRecepcion.Year == hoy.Year && x.FechaRecepcion.Month == hoy.Month).ToList();
                    var pagosMes = pagos.Where(x => x.FechaPago.Year == hoy.Year && x.FechaPago.Month == hoy.Month).ToList();
                    var ordenesActivas = ordenesVigentes.Where(x => x.EstadoNombre != "Entregada").ToList();
                    int enReparacion = ordenesActivas.Count(x => x.EstadoNombre == "En Reparación");
                    int listasParaEntregar = ordenesActivas.Count(x =>
                        (x.EstadoNombre is "Reparada" or "No Reparada" or "Rechazada") && x.Saldo <= 0);
                    string mes = hoy.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("es-NI"));
                    lblOrdenesTitulo.Text = "Ventas del mes";
                    lblOrdenesValor.Text = $"C$ {ordenesMes.Sum(x => x.Total):N2}";
                    _lblOrdenesDetalle.Text = $"{ordenesMes.Count:N0} órdenes recibidas · {mes}";
                    lblReparacionTitulo.Text = "Cobrado del mes";
                    lblReparacionValor.Text = $"C$ {pagosMes.Sum(x => x.Monto):N2}";
                    _lblReparacionDetalle.Text = $"{pagosMes.Count:N0} pagos registrados · {mes}";
                    lblEntregaTitulo.Text = "Órdenes activas";
                    lblEntregaValor.Text = ordenesActivas.Count.ToString("N0");
                    _lblEntregaDetalle.Text = $"{enReparacion:N0} en reparación · {listasParaEntregar:N0} listas";
                    ayudaControles.SetToolTip(tarjetaOrdenes, "Importe de las órdenes recibidas este mes.");
                    ayudaControles.SetToolTip(tarjetaReparacion, "Pagos vigentes registrados este mes.");
                    ayudaControles.SetToolTip(tarjetaEntrega, "Órdenes que todavía no se han entregado.");
                }
                else
                {
                    var misOrdenes = ordenesVigentes.Where(x => x.IdTecnicoResponsable == usuario.IdUsuario).ToList();
                    var misOrdenesActivas = misOrdenes.Where(x => x.EstadoNombre != "Entregada").ToList();
                    lblOrdenesTitulo.Text = "Mis órdenes activas";
                    lblOrdenesValor.Text = misOrdenesActivas.Count.ToString("N0");
                    _lblOrdenesDetalle.Text = "Asignadas a tu usuario";
                    lblReparacionTitulo.Text = "En reparación";
                    lblReparacionValor.Text = misOrdenesActivas.Count(x => x.EstadoNombre == "En Reparación").ToString("N0");
                    _lblReparacionDetalle.Text = "Trabajos en curso";
                    lblEntregaTitulo.Text = "Listas para entregar";
                    lblEntregaValor.Text = misOrdenesActivas.Count(x =>
                        (x.EstadoNombre is "Reparada" or "No Reparada") && x.Saldo <= 0).ToString("N0");
                    _lblEntregaDetalle.Text = "Trabajos finalizados y pagados";
                    int pendientesDiagnostico = misOrdenesActivas.Count(x => x.EstadoNombre == "En Revisión");
                    _lblReparacionDetalle.Text = $"Trabajos en curso · {pendientesDiagnostico:N0} por diagnosticar";
                }

                var equiposPorId = equipos.ToDictionary(x => x.IdEquipo);
                var ordenesVisibles = usuario.PermisoRol == AlDia.Entity.Seguridad.RolUsuario.Tecnico
                    ? ordenesVigentes.Where(x => x.IdTecnicoResponsable == usuario.IdUsuario)
                    : ordenesVigentes;
                var recientes = ordenesVisibles
                    .OrderByDescending(x => x.FechaRecepcion)
                    .Take(6)
                    .Select(x => new
                    {
                        Orden = x,
                        Equipo = equiposPorId.GetValueOrDefault(x.IdEquipo) is { } equipo
                            ? $"{equipo.Marca} {equipo.Modelo}".Trim()
                            : $"Equipo #{x.IdEquipo}"
                    }).ToList();
                MostrarOrdenesRecientes(recientes.Select(x => (x.Orden, x.Equipo)).ToList());
                lblPie.Text = $"AL DÍA  /  Datos actualizados {DateTime.Now:dd/MM/yyyy HH:mm}";
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { }
            catch (Exception ex)
            {
                if (IsDisposed || ct.IsCancellationRequested) return;
                lblOrdenesValor.Text = lblReparacionValor.Text = lblEntregaValor.Text = "—";
                lblSinOrdenes.Text = "No fue posible cargar las órdenes. Comprueba la conexión e inténtalo de nuevo.";
                MostrarEstadoOrdenes(false);
                lblPie.Text = $"No se pudieron cargar los datos del dashboard: {ex.Message}";
            }
        }

        private void MostrarOrdenesRecientes(IReadOnlyList<(OrdenReparacionEntity Orden, string Equipo)> recientes)
        {
            if (_filasOrdenes is not null)
            {
                tablaOrdenes.Controls.Remove(_filasOrdenes);
                _filasOrdenes.Dispose();
                _filasOrdenes = null;
            }

            if (recientes.Count == 0)
            {
                lblSinOrdenes.Text = "Sin órdenes para mostrar";
                MostrarEstadoOrdenes(false);
                return;
            }

            MostrarEstadoOrdenes(true);
            _filasOrdenes = new TableLayoutPanel
            {
                Name = "tablaFilasOrdenes",
                Dock = DockStyle.Top,
                BackColor = Color.White,
                ColumnCount = 4,
                RowCount = recientes.Count,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize,
                Height = recientes.Count * 52,
                Margin = Padding.Empty,
                Padding = new Padding(8, 0, 8, 0),
                AutoScroll = false
            };
            _filasOrdenes.ColumnStyles.Clear();
            _filasOrdenes.RowStyles.Clear();
            _filasOrdenes.RowCount = recientes.Count;
            _filasOrdenes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            _filasOrdenes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            _filasOrdenes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29F));
            _filasOrdenes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23F));
            for (int fila = 0; fila < recientes.Count; fila++)
            {
                _filasOrdenes.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
                var (orden, equipo) = recientes[fila];
                Color fondoFila = fila % 2 == 0 ? Color.White : Color.FromArgb(248, 250, 253);
                _filasOrdenes.Controls.Add(Celda(orden.NumeroOrden, true, fondo: fondoFila), 0, fila);
                _filasOrdenes.Controls.Add(Celda(orden.NombreCompletoCliente, fondo: fondoFila), 1, fila);
                _filasOrdenes.Controls.Add(Celda(equipo, fondo: fondoFila), 2, fila);
                _filasOrdenes.Controls.Add(Celda(orden.EstadoNombre, false, true, fondoFila), 3, fila);
            }
            tablaOrdenes.Controls.Add(_filasOrdenes, 0, 2);
            _filasOrdenes.BringToFront();
        }

        private static Label Celda(string texto, bool destacado = false, bool estado = false, Color? fondo = null) => new()
        {
            Text = texto,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            AutoEllipsis = true,
            Margin = Padding.Empty,
            Padding = new Padding(8, 0, 4, 0),
            Font = new Font(destacado ? "Segoe UI Semibold" : "Segoe UI", 9F,
                destacado ? FontStyle.Bold : FontStyle.Regular),
            ForeColor = estado ? Color.FromArgb(37, 99, 235)
                : destacado ? Color.FromArgb(20, 43, 78) : Color.FromArgb(77, 91, 112),
            BackColor = fondo ?? Color.White
        };

        private void MostrarEstadoOrdenes(bool hayOrdenes)
        {
            tablaSinOrdenes.Visible = !hayOrdenes;
            if (!hayOrdenes) tablaSinOrdenes.BringToFront();
            else _filasOrdenes?.BringToFront();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _cargaDashboard?.Cancel();
            _cargaDashboard?.Dispose();
            base.OnFormClosed(e);
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
