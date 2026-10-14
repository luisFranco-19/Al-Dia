namespace AlDia.Entity.Common;

// Representa tblLogErrores. Un error registrado no se edita como un catalogo.
public sealed class LogErrorEntity
{
    public int IdLog { get; }
    public string MensajeError { get; }
    public int? NumeroError { get; }
    public string? Procedimiento { get; }
    public int? LineaError { get; }
    public string UsuarioApp { get; }
    public DateTime? FechaError { get; }

    public LogErrorEntity(int idLog, string mensajeError, int? numeroError, string? procedimiento,
        int? lineaError, string usuarioApp, DateTime? fechaError)
    {
        IdLog = Validacion.IdNuevoOExistente(idLog, "IdLog");
        if (string.IsNullOrWhiteSpace(mensajeError)) throw new ValidacionException("MensajeError es obligatorio.");
        // Se conserva el mensaje de auditoria original.
        MensajeError = mensajeError;
        NumeroError = numeroError;
        Procedimiento = string.IsNullOrWhiteSpace(procedimiento) ? null : procedimiento.Trim();
        if (lineaError < 0) throw new ValidacionException("LineaError no puede ser negativa.");
        LineaError = lineaError;
        if (string.IsNullOrWhiteSpace(usuarioApp))
            throw new ValidacionException("Debe ingresar el usuario de la aplicacion.");
        UsuarioApp = usuarioApp.Trim();
        FechaError = fechaError;
    }

    public LogErrorEntity(string mensajeError, int? numeroError, string? procedimiento, int? lineaError, string usuarioApp)
        : this(0, mensajeError, numeroError, procedimiento, lineaError, usuarioApp, DateTime.Now) { }
}
