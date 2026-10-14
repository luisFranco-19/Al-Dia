using System.Net.Mail;

namespace AlDia.Entity.Common;

public static class Validacion
{
    //Valida que el texto no sea nulo ni este vacio y que no exceda la longitud maxima.
    public static string Texto(string? valor, string campo, int max)
    {
        var texto = valor?.Trim(); //elimina los espacios en blanco al inicio y al final.
        if (string.IsNullOrEmpty(texto) || texto.Length > max) throw new ValidacionException($"{campo} es obligatorio y admite hasta {max} caracteres.");
        return texto;
    }
    //Valida que el texto sea opcional y que no exceda la longitud maxima.
    public static string? Opcional(string? valor, string campo, int max)
    {
        if (string.IsNullOrWhiteSpace(valor)) return null; // si el valor es nulo o esta vacio, devuelve null.
        return Texto(valor, campo, max);
    }
    //Valida que el correo sea opcional y que no exceda la longitud maxima y que sea un correo valido.
    public static string? Correo(string? valor)
    {
        string? correo = Opcional(valor, "Correo", 100);
        //si el correo no es null, se valida que sea un correo valido.
        if (correo is not null && !MailAddress.TryCreate(correo, out _))
            throw new ValidacionException("El correo no es valido.");
        return correo; //devuelve el correo.  
    }
    //Valida que el id sea un identificador valido.
    public static int Id(int valor, string campo)
    {
        if (valor <= 0) throw new ValidacionException($"{campo} debe ser un identificador valido.");
        return valor;
    }
    //Valida que el id sea un identificador valido, puede ser 0 o mayor a 0.
    public static int IdNuevoOExistente(int valor, string campo)
    {
        if (valor < 0) throw new ValidacionException($"{campo} no puede ser negativo.");
        return valor; //devuelve el id.
    }
    //Valida que el valor sea un numero y que no exceda la longitud maxima.
    public static decimal Dinero(decimal valor, string campo, bool positivo = false)
    {
        if (valor < 0 || (positivo && valor == 0) || valor > 99999999.99m || decimal.Round(valor, 2) != valor)
            throw new ValidacionException($"{campo} debe estar dentro del rango permitido y tener hasta dos decimales.");
        return valor; //devuelve el valor.
    }
    //Se asigna el id a un registro existente o a uno nuevo.
    public static int AsignarId(int actual, int nuevo, string campo)
    {
        Id(nuevo, campo); // Si el nuevo es menor a 0, se lanza una excepcion.
        if (actual != 0 && actual != nuevo)
            throw new ValidacionException($"No se puede cambiar {campo} de un registro existente.");
        return nuevo; //devuelve el id.
    }
    //Valida que el valor sea una enumeracion valida.
    public static T  Enumeracion<T>(T valor, string campo) where T : struct, Enum
    {
        if (!Enum.IsDefined(valor)) throw new ValidacionException($"{campo} no es valido.");
        return valor; //devuelve el valor.
    }
}
