namespace AlDia.UI.Formularios.Base;

public sealed class FrmOverlay : Form
{
    public FrmOverlay(Form propietario)
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        Bounds = propietario.Bounds;
        BackColor = Color.Black;
        Opacity = 0.45;
        ShowInTaskbar = false;
    }
}
