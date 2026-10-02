using System.Net.Mail;

namespace AlDia.Entity.Common;

public static class Validacion
{
    public static string Texto(string? valor, string campo, int max)
    {
        var texto = valor?.Trim();
        if (string.IsNullOrEmpty(texto) || texto.Length > max) throw new ValidacionException($"{campo} es obligatorio y admite hasta {max} caracteres.");
        return texto;
    }
    public static string? Opcional(string? valor, string campo, int max)
    {
        if (string.IsNullOrWhiteSpace(valor)) return null;
        return Texto(valor, campo, max);
    }
    public static string? Correo(string? valor)
    {
        string? correo = Opcional(valor, "Correo", 100);
        if (correo is not null && !MailAddress.TryCreate(correo, out _))
            throw new ValidacionException("El correo no es valido.");
        return correo;
    }
    public static int Id(int valor, string campo)
    {
        if (valor <= 0) throw new ValidacionException($"{campo} debe ser un identificador valido.");
        return valor;
    }
    public static int IdNuevoOExistente(int valor, string campo)
    {
        if (valor < 0) throw new ValidacionException($"{campo} no puede ser negativo.");
        return valor;
    }
    public static decimal Dinero(decimal valor, string campo, bool positivo = false)
    {
        if (valor < 0 || (positivo && valor == 0) || valor > 99999999.99m || decimal.Round(valor, 2) != valor)
            throw new ValidacionException($"{campo} debe estar dentro del rango permitido y tener hasta dos decimales.");
        return valor;
    }
    public static int AsignarId(int actual, int nuevo, string campo)
    {
        Id(nuevo, campo);
        if (actual != 0 && actual != nuevo)
            throw new ValidacionException($"No se puede cambiar {campo} de un registro existente.");
        return nuevo;
    }
    public static T Enumeracion<T>(T valor, string campo) where T : struct, Enum
    {
        if (!Enum.IsDefined(valor)) throw new ValidacionException($"{campo} no es valido.");
        return valor;
    }
}
