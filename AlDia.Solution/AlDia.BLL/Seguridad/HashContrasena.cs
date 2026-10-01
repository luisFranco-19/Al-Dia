using System.Security.Cryptography;
namespace AlDia.BLL.Seguridad;

public interface IHashContrasena
{
    string Crear(string contrasena);
    bool Verificar(string contrasena, string hash);
}
public sealed class HashContrasena : IHashContrasena
{
    private const int Iteraciones = 600000;
    public string Crear(string contrasena)
    {
        ArgumentNullException.ThrowIfNull(contrasena);
        var sal = RandomNumberGenerator.GetBytes(16);
        var derivado = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, 32);
        return $"PBKDF2-SHA256${Iteraciones}${Convert.ToBase64String(sal)}${Convert.ToBase64String(derivado)}";
    }
    public bool Verificar(string contrasena, string hash)
    {
        try
        {
            var partes = hash.Split('$');
            if (partes.Length != 4 || partes[0] != "PBKDF2-SHA256" || !int.TryParse(partes[1], out int iteraciones)
                || iteraciones < 100000 || iteraciones > 1000000) return false;
            var sal = Convert.FromBase64String(partes[2]);
            var esperado = Convert.FromBase64String(partes[3]);
            if (sal.Length != 16 || esperado.Length != 32) return false;
            var actual = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, iteraciones, HashAlgorithmName.SHA256, 32);
            return CryptographicOperations.FixedTimeEquals(actual, esperado);
        }
        catch (FormatException) { return false; }
    }
}
