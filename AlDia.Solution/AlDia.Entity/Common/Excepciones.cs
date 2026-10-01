namespace AlDia.Entity.Common;

public sealed class ValidacionException(string mensaje) : Exception(mensaje);
public sealed class PermisoException(string mensaje) : Exception(mensaje);
public sealed class DatosException(string mensaje, int numero, string? procedimiento, int linea, Exception causa)
    : Exception(mensaje, causa)
{
    public int Numero { get; } = numero;
    public string? Procedimiento { get; } = procedimiento;
    public int Linea { get; } = linea;
    public bool EsErrorDeNegocio => Numero is >= 51000 and <= 51003 or 2601 or 2627 or 547;
}
