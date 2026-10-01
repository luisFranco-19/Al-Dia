using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.Entity.Common;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Equipos;
namespace AlDia.BLL.Equipos;
public sealed class EquipoBll(IEquipoDal datos, SesionUsuario sesion)
{
    public Task<int> GuardarAsync(EquipoSolicitud s, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(s);
        int actor = sesion.Exigir(RolUsuario.Administrador, RolUsuario.Recepcionista);
        if (s.IdEquipo < 0) throw new ValidacionException("Identificador invalido.");
        var validada = s with { IdCliente = Validacion.Id(s.IdCliente, "IdCliente"), IdTipoEquipo = Validacion.Id(s.IdTipoEquipo, "IdTipoEquipo"), Marca = Validacion.Texto(s.Marca, "Marca", 50), Modelo = Validacion.Opcional(s.Modelo, "Modelo", 100), NumeroSerie = Validacion.Opcional(s.NumeroSerie, "NumeroSerie", 100), Color = Validacion.Opcional(s.Color, "Color", 50), Observaciones = Validacion.Opcional(s.Observaciones, "Observaciones", 500) };
        return datos.GuardarAsync(actor, validada, ct);
    }
    public Task<IReadOnlyList<EquipoEntity>> ConsultarAsync(int? id = null, bool soloActivos = true, CancellationToken ct = default)
    {
        int actor = sesion.Exigir();
        if (id.HasValue) Validacion.Id(id.Value, "Identificador");
        return datos.ConsultarAsync(actor, id, soloActivos, ct);
    }
}
