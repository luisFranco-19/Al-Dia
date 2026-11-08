using System.Data;
using System.Globalization;
using System.Text;

namespace AlDia.UI.Formularios.Hijos.Reportes;

internal static class GeneradorPdfReporte
{
    private const double AnchoPagina = 842;
    private const double AltoPagina = 595;
    private const double Margen = 40;

    public static void Guardar(DatosReporte reporte, string ruta)
    {
        ArgumentNullException.ThrowIfNull(reporte);
        if (string.IsNullOrWhiteSpace(ruta)) throw new ArgumentException("Debe indicar la ruta del PDF.", nameof(ruta));

        var paginas = CrearPaginas(reporte);
        using var salida = new MemoryStream();
        EscribirAscii(salida, "%PDF-1.4\n");
        int cantidadObjetos = paginas.Count * 2 + 3;
        var offsets = new long[cantidadObjetos + 1];
        EscribirObjeto(salida, offsets, 1, "<< /Type /Catalog /Pages 2 0 R >>");
        string referencias = string.Join(" ", Enumerable.Range(0, paginas.Count).Select(i => $"{3 + i * 2} 0 R"));
        EscribirObjeto(salida, offsets, 2, $"<< /Type /Pages /Kids [{referencias}] /Count {paginas.Count} >>");

        int fuenteId = cantidadObjetos;
        for (int i = 0; i < paginas.Count; i++)
        {
            int paginaId = 3 + i * 2;
            int contenidoId = paginaId + 1;
            EscribirObjeto(salida, offsets, paginaId,
                $"<< /Type /Page /Parent 2 0 R /MediaBox [0 0 {AnchoPagina.ToString(CultureInfo.InvariantCulture)} {AltoPagina.ToString(CultureInfo.InvariantCulture)}] /Resources << /Font << /F1 {fuenteId} 0 R >> >> /Contents {contenidoId} 0 R >>");
            byte[] contenido = Encoding.ASCII.GetBytes(paginas[i]);
            offsets[contenidoId] = salida.Position;
            EscribirAscii(salida, $"{contenidoId} 0 obj\n<< /Length {contenido.Length} >>\nstream\n");
            salida.Write(contenido);
            EscribirAscii(salida, "\nendstream\nendobj\n");
        }
        EscribirObjeto(salida, offsets, fuenteId, "<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica /Encoding /WinAnsiEncoding >>");

        long inicioXref = salida.Position;
        EscribirAscii(salida, $"xref\n0 {cantidadObjetos + 1}\n0000000000 65535 f \n");
        for (int id = 1; id <= cantidadObjetos; id++)
            EscribirAscii(salida, $"{offsets[id]:D10} 00000 n \n");
        EscribirAscii(salida, $"trailer\n<< /Size {cantidadObjetos + 1} /Root 1 0 R >>\nstartxref\n{inicioXref}\n%%EOF");
        File.WriteAllBytes(ruta, salida.ToArray());
    }

    private static List<string> CrearPaginas(DatosReporte reporte)
    {
        var paginas = new List<string>();
        int fila = 0;
        int numeroPagina = 1;
        do
        {
            var contenido = new StringBuilder();
            if (numeroPagina == 1)
            {
                Texto(contenido, Margen, 552, 18, "Al Día · " + reporte.Titulo);
                Texto(contenido, Margen, 532, 9, reporte.Periodo);
                Texto(contenido, Margen, 517, 8, reporte.Filtros);
                Texto(contenido, Margen, 493, 8,
                    $"{reporte.Indicador1}: {reporte.Valor1}    {reporte.Indicador2}: {reporte.Valor2}    {reporte.Indicador3}: {reporte.Valor3}    {reporte.Indicador4}: {reporte.Valor4}");
            }
            else
            {
                Texto(contenido, Margen, 552, 14, $"Al Día · {reporte.Titulo} · continuación");
                Texto(contenido, Margen, 534, 8, reporte.Periodo);
            }

            int columnas = Math.Max(1, reporte.Filas.Columns.Count);
            double anchoTabla = AnchoPagina - Margen * 2;
            double anchoColumna = anchoTabla / columnas;
            double y = numeroPagina == 1 ? 462 : 512;
            Linea(contenido, Margen, y - 5, AnchoPagina - Margen, y - 5);
            for (int c = 0; c < columnas; c++)
                TextoAjustado(contenido, Margen + c * anchoColumna + 3, y, 7.5,
                    reporte.Filas.Columns.Count > c ? reporte.Filas.ExtendedProperties[$"Header{c}"]?.ToString() ?? reporte.Filas.Columns[c].ColumnName : "", anchoColumna - 6);
            y -= 16;
            Linea(contenido, Margen, y + 7, AnchoPagina - Margen, y + 7);

            while (fila < reporte.Filas.Rows.Count && y > 42)
            {
                var datos = reporte.Filas.Rows[fila];
                for (int c = 0; c < columnas; c++)
                {
                    string valor = c < reporte.Filas.Columns.Count ? Formatear(datos[c]) : "";
                    TextoAjustado(contenido, Margen + c * anchoColumna + 3, y, 7, valor, anchoColumna - 6);
                }
                y -= 18;
                if (fila % 2 == 0) Linea(contenido, Margen, y + 7, AnchoPagina - Margen, y + 7, 0.92);
                fila++;
            }

            Texto(contenido, Margen, 24, 8,
                $"{reporte.Filas.Rows.Count:N0} registros · generado {DateTime.Now:dd/MM/yyyy HH:mm} · página {numeroPagina}");
            paginas.Add(contenido.ToString());
            numeroPagina++;
        } while (fila < reporte.Filas.Rows.Count);

        return paginas;
    }

    private static string Formatear(object valor) => valor switch
    {
        DBNull => "",
        DateTime fecha => fecha.ToString("dd/MM/yyyy HH:mm"),
        decimal importe => importe.ToString("N2"),
        int entero => entero.ToString("N0"),
        long entero => entero.ToString("N0"),
        _ => valor.ToString() ?? ""
    };

    private static void TextoAjustado(StringBuilder contenido, double x, double y, double tamano, string valor, double ancho)
    {
        string limpio = Normalizar(valor);
        int maximo = Math.Max(4, (int)(ancho / (tamano * 0.52)));
        if (limpio.Length > maximo) limpio = limpio[..Math.Max(1, maximo - 1)] + ".";
        Texto(contenido, x, y, tamano, limpio);
    }

    private static void Texto(StringBuilder contenido, double x, double y, double tamano, string valor)
    {
        string hex = Convert.ToHexString(Encoding.Latin1.GetBytes(Normalizar(valor)));
        contenido.Append("BT /F1 ").Append(tamano.ToString(CultureInfo.InvariantCulture)).Append(" Tf 1 0 0 1 ")
            .Append(x.ToString("0.##", CultureInfo.InvariantCulture)).Append(' ')
            .Append(y.ToString("0.##", CultureInfo.InvariantCulture)).Append(" Tm <")
            .Append(hex).Append("> Tj ET\n");
    }

    private static string Normalizar(string valor) => valor
        .Replace('·', '-')
        .Replace('—', '-')
        .Replace('–', '-')
        .Replace('\r', ' ')
        .Replace('\n', ' ');

    private static void Linea(StringBuilder contenido, double x1, double y1, double x2, double y2, double gris = 0.82) =>
        contenido.Append("q ").Append(gris.ToString("0.##", CultureInfo.InvariantCulture)).Append(" G ")
            .Append(x1.ToString("0.##", CultureInfo.InvariantCulture)).Append(' ')
            .Append(y1.ToString("0.##", CultureInfo.InvariantCulture)).Append(" m ")
            .Append(x2.ToString("0.##", CultureInfo.InvariantCulture)).Append(' ')
            .Append(y2.ToString("0.##", CultureInfo.InvariantCulture)).Append(" l S Q\n");

    private static void EscribirObjeto(Stream salida, long[] offsets, int id, string cuerpo)
    {
        offsets[id] = salida.Position;
        EscribirAscii(salida, $"{id} 0 obj\n{cuerpo}\nendobj\n");
    }

    private static void EscribirAscii(Stream salida, string texto) => salida.Write(Encoding.ASCII.GetBytes(texto));
}
