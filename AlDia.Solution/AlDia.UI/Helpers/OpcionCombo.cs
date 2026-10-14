namespace AlDia.UI.Helpers;

// Los combos conservan el identificador del registro, sin depender de su posición.
internal sealed record OpcionCombo(int? Id, string Nombre)
{
    public override string ToString() => Nombre;
}
