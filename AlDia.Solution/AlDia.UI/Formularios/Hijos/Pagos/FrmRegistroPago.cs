using AlDia.BLL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Pagos;
using AlDia.Entity.Reparaciones;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Helpers;

namespace AlDia.UI.Formularios.Hijos.Pagos;

public partial class FrmRegistroPago : FrmRegistroBase
{
    private readonly int? _idPago;
    private readonly int? _idOrdenFija;
    private readonly string? _motivo;
    private PagoEntity? _pago;
    private List<OrdenReparacionEntity> _ordenes = [];
    protected override bool PermitirGuardar => true;
    public FrmRegistroPago()
    {
        InitializeComponent();
        cboOrdenServicio.SelectedIndexChanged += (_, _) => MostrarOrden();
        cboUsuarioRegistro.Enabled = dtpFechaPago.Enabled = false;
        txtClienteOrden.ReadOnly = txtEquipoOrden.ReadOnly = txtSaldoOrden.ReadOnly = txtTotalOrden.ReadOnly = true;
    }
    public FrmRegistroPago(AplicacionBll aplicacion) : this() => ConfigurarServicios(aplicacion);
    public FrmRegistroPago(AplicacionBll aplicacion, int idOrden) : this(aplicacion)
    {
        _idOrdenFija = Validacion.Id(idOrden, "IdOrden");
        cboOrdenServicio.Enabled = false;
        lblTitulo.Text = "Registrar pago de la orden";
    }
    public FrmRegistroPago(AplicacionBll aplicacion, int id, string motivo) : this(aplicacion)
    {
        _idPago = Validacion.Id(id, "IdPago");
        _motivo = Validacion.Texto(motivo, "Motivo", 300);
        lblTitulo.Text = "Corregir pago";
        cboOrdenServicio.Enabled = false;
    }
    protected override async Task CargarDatosAsync()
    {
        if (_idPago.HasValue)
        {
            _pago = await Aplicacion.Pagos.ObtenerPorIdAsync(_idPago.Value, TokenFormulario)
                ?? throw new ValidacionException("El pago ya no existe.");
            if (_pago.Anulado) throw new ValidacionException("No se puede corregir un pago anulado.");
        }
        _ordenes = (await Aplicacion.Ordenes.ConsultarAsync(ct: TokenFormulario))
            .Where(x => x.EstadoNombre is "Reparada" or "No Reparada")
            .Where(x => (x.Saldo > 0 || x.IdOrden == _pago?.IdOrden)
                && (!_idOrdenFija.HasValue || x.IdOrden == _idOrdenFija.Value)).ToList();
        cboOrdenServicio.DataSource = _ordenes.Select(x => new OpcionCombo(x.IdOrden, $"{x.NumeroOrden} · {x.NombreCompletoCliente}")).ToList();
        cboOrdenServicio.SelectedIndex = -1;
        cboMetodoPago.DataSource = Enum.GetValues<MetodoPago>();
        var actor = Aplicacion.Sesion.UsuarioActual!;
        cboUsuarioRegistro.DataSource = new[] { new OpcionCombo(actor.IdUsuario, actor.NombreCompleto) };
        if (_pago is not null)
        {
            cboOrdenServicio.SelectedItem = ((List<OpcionCombo>)cboOrdenServicio.DataSource).First(x => x.Id == _pago.IdOrden);
            numMonto.Text = DineroTextBoxHelper.Formatear(_pago.Monto);
            cboMetodoPago.SelectedItem = _pago.MetodoPago;
            txtObservaciones.Text = _pago.Observaciones;
        }
        else if (_idOrdenFija.HasValue)
        {
            var opcion = ((List<OpcionCombo>)cboOrdenServicio.DataSource).FirstOrDefault(x => x.Id == _idOrdenFija.Value);
            if (opcion is null) throw new ValidacionException("La orden no está finalizada o no tiene saldo pendiente para registrar un pago.");
            cboOrdenServicio.SelectedItem = opcion;
        }
    }
    private void MostrarOrden()
    {
        var orden = _ordenes.FirstOrDefault(x => x.IdOrden == (cboOrdenServicio.SelectedItem as OpcionCombo)?.Id);
        txtClienteOrden.Text = orden?.NombreCompletoCliente ?? "";
        txtEquipoOrden.Text = orden is null ? "" : $"Equipo #{orden.IdEquipo}";
        txtTotalOrden.Text = orden?.Total.ToString("N2") ?? "";
        txtSaldoOrden.Text = orden?.Saldo.ToString("N2") ?? "";
        if (_pago is null)
            numMonto.Text = orden is null ? "" : DineroTextBoxHelper.Formatear(orden.Saldo);
    }
    protected override bool ValidarControles()
    {
        errorIcon.Clear();
        bool valido = true;
        if (cboOrdenServicio.SelectedItem is not OpcionCombo { Id: not null })
        { errorIcon.SetError(cboOrdenServicio, "Seleccione una orden con saldo."); valido = false; }
        if (!DineroTextBoxHelper.TryLeer(numMonto, out decimal monto) || monto <= 0)
        { errorIcon.SetError(numMonto, "Ingrese un importe mayor que cero, con máximo dos decimales."); valido = false; }
        else
        {
            var orden = _ordenes.FirstOrDefault(x => x.IdOrden == (cboOrdenServicio.SelectedItem as OpcionCombo)?.Id);
            decimal limite = (orden?.Saldo ?? 0) + (_pago?.Monto ?? 0);
            if (monto > limite)
            { errorIcon.SetError(numMonto, $"El importe supera el saldo disponible de C$ {limite:N2}."); valido = false; }
        }
        return valido;
    }
    protected override async Task GuardarAsync()
    {
        var metodo = (MetodoPago)cboMetodoPago.SelectedItem!;
        if (_pago is not null)
        {
            DineroTextBoxHelper.TryLeer(numMonto, out decimal monto);
            await Aplicacion.Pagos.CorregirEntidadAsync(_pago, monto, metodo, _motivo!, txtObservaciones.Text, TokenFormulario);
        }
        else
        {
            PagoEntity pago = new();
            pago.CambiarOrden(((OpcionCombo)cboOrdenServicio.SelectedItem!).Id!.Value);
            pago.CambiarUsuario(Aplicacion.Sesion.UsuarioActual!.IdUsuario);
            DineroTextBoxHelper.TryLeer(numMonto, out decimal monto);
            pago.CambiarMonto(monto);
            pago.CambiarMetodoPago(metodo);
            pago.CambiarObservaciones(txtObservaciones.Text);
            await Aplicacion.Pagos.RegistrarEntidadAsync(pago, TokenFormulario);
        }
    }
}
