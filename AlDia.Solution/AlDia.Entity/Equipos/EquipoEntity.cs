namespace AlDia.Entity.Equipos;
public sealed record EquipoEntity(int IdEquipo, int IdCliente, int IdTipoEquipo, string Marca, string? Modelo, string? NumeroSerie, string? Color, string? Observaciones, bool Estado, DateTime FechaRegistro);
public sealed record EquipoSolicitud(int IdCliente, int IdTipoEquipo, string Marca, string? Modelo, string? NumeroSerie, string? Color, string? Observaciones, bool Estado = true, int IdEquipo = 0);
