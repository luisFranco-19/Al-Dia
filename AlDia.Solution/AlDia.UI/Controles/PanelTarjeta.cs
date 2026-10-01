using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace AlDia.UI.Controles;

// Contenedor visual reutilizable para las tarjetas de los formularios.
public class PanelTarjeta : Panel
{
    private int _radioBorde = 14;
    private Color _colorBorde = Color.FromArgb(222, 228, 237);

    public PanelTarjeta()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.White;
    }

    [Category("Apariencia")]
    [DefaultValue(14)]
    public int RadioBorde
    {
        get => _radioBorde;
        set { _radioBorde = Math.Max(0, value); Invalidate(); }
    }

    [Category("Apariencia")]
    [DefaultValue(typeof(Color), "222, 228, 237")]
    public Color ColorBorde
    {
        get => _colorBorde;
        set { _colorBorde = value; Invalidate(); }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (Width < 2 || Height < 2) return;

        e.Graphics.Clear(Parent?.BackColor ?? SystemColors.Control);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        var rectangulo = new RectangleF(0.5F, 0.5F, Width - 1F, Height - 1F);
        float diametro = Math.Min(_radioBorde * 2F, Math.Min(rectangulo.Width, rectangulo.Height));
        using var contorno = new GraphicsPath();
        if (diametro == 0)
            contorno.AddRectangle(rectangulo);
        else
        {
            contorno.AddArc(rectangulo.Left, rectangulo.Top, diametro, diametro, 180, 90);
            contorno.AddArc(rectangulo.Right - diametro, rectangulo.Top, diametro, diametro, 270, 90);
            contorno.AddArc(rectangulo.Right - diametro, rectangulo.Bottom - diametro, diametro, diametro, 0, 90);
            contorno.AddArc(rectangulo.Left, rectangulo.Bottom - diametro, diametro, diametro, 90, 90);
            contorno.CloseFigure();
        }

        using var fondo = new SolidBrush(BackColor);
        using var borde = new Pen(_colorBorde);
        e.Graphics.FillPath(fondo, contorno);
        e.Graphics.DrawPath(borde, contorno);
        base.OnPaint(e);
    }
}
