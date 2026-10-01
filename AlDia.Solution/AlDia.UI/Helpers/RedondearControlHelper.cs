using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace AlDia.UI.Helpers
{
    public static class RedondearControlHelper
    {
        /// <summary>
        /// Redondea los bordes de cualquier control.
        /// </summary>
        /// <param name="control">Control a redondear.</param>
        /// <param name="radio">Radio de las esquinas.</param>
        public static void RedondearControl(Control control, int radio)
        {
            GraphicsPath ruta = new GraphicsPath();

            ruta.AddArc(0, 0, radio, radio, 180, 90);
            ruta.AddArc(control.Width - radio, 0, radio, radio, 270, 90);
            ruta.AddArc(control.Width - radio, control.Height - radio, radio, radio, 0, 90);
            ruta.AddArc(0, control.Height - radio, radio, radio, 90, 90);

            ruta.CloseFigure();

            control.Region?.Dispose();
            control.Region = new Region(ruta);
        }
    }
}
