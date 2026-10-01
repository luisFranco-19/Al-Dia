#nullable enable
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AlDia.UI.Properties
{
    internal class Resources
    {
        private static Bitmap? _cerrar24;

        /// <summary>
        ///   Icono de cierre de 24x24 px.
        /// </summary>
        internal static Bitmap cerrar24
        {
            get
            {
                if (_cerrar24 == null)
                {
                    var bmp = new Bitmap(24, 24);
                    using var g = Graphics.FromImage(bmp);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using var pen = new Pen(Color.FromArgb(120, 120, 120), 2.2f);
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    g.DrawLine(pen, 5, 5, 19, 19);
                    g.DrawLine(pen, 19, 5, 5, 19);
                    _cerrar24 = bmp;
                }
                return _cerrar24;
            }
        }
    }
}
