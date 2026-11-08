using System.Globalization;
using System.Windows.Forms;

namespace AlDia.UI.Helpers;

internal static class DineroTextBoxHelper
{
    private const decimal Maximo = 99999999.99m;

    public static bool TryLeer(TextBox control, out decimal valor)
    {
        const NumberStyles estilos = NumberStyles.Number;
        bool parseado = decimal.TryParse(control.Text, estilos, CultureInfo.CurrentCulture, out valor)
            || decimal.TryParse(control.Text, estilos, CultureInfo.InvariantCulture, out valor);

        return parseado && valor >= 0 && valor <= Maximo && decimal.Round(valor, 2) == valor;
    }

    public static string Formatear(decimal valor) => valor.ToString("N2", CultureInfo.CurrentCulture);
}
