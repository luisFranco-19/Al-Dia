using AlDia.BLL.Common;
using AlDia.Entity.Common;

namespace AlDia.UI.Formularios.Base;

// Reutiliza la validación y los botones del registro base para pedir un motivo.
internal sealed class FrmSolicitudMotivo : FrmRegistroBase
{
    private readonly TextBox _texto = new() { Multiline = true, Dock = DockStyle.Fill, MaxLength = 300 };
    public string Motivo { get; private set; } = "";
    protected override bool PermitirGuardar => true;
    public FrmSolicitudMotivo(AplicacionBll aplicacion, string titulo)
    {
        ConfigurarServicios(aplicacion);
        Size = new Size(620, 360);
        lblTitulo.Text = titulo;
        lblDescripcion.Text = "Escriba el motivo para conservarlo en el historial.";
        panelCuerpo.Padding = new Padding(24);
        panelCuerpo.Controls.Add(_texto);
        btnGuardar.Text = "Continuar";
    }
    protected override bool ValidarControles() => Requerido(_texto, "Ingrese el motivo.");
    protected override Task GuardarAsync()
    {
        Motivo = Validacion.Texto(_texto.Text, "Motivo", 300);
        return Task.CompletedTask;
    }
}
