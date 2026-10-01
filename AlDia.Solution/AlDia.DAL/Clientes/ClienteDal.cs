using AlDia.DAL.Common;
using AlDia.Entity.Clientes;
namespace AlDia.DAL.Clientes;
public sealed class ClienteDal(EjecutorSql sql) : IClienteDal
{
    public Task<int> GuardarAsync(int idActor, ClienteSolicitud s, CancellationToken ct = default) =>
        sql.EscribirAsync("dbo.usp_GuardarCliente", 
            [ParametrosSql.Entero("@IdUsuarioActor", idActor), 
            ParametrosSql.EntradaSalida("@IdCliente", s.IdCliente),
            ParametrosSql.Texto("@Nombre", s.Nombre, 100), ParametrosSql.Texto("@Apellido",
                s.Apellido, 100), ParametrosSql.Texto("@Telefono", s.Telefono, 20),
            ParametrosSql.Texto("@Cedula", s.Cedula, 20), ParametrosSql.Bit("@Estado", s.Estado)],
            "@IdCliente", idActor, ct);
    public Task<IReadOnlyList<ClienteEntity>> ConsultarAsync(int idActor, int? id = null, bool soloActivos = true, CancellationToken ct = default) =>
        sql.ConsultarAsync("dbo.usp_ConsultarClientes", [ParametrosSql.Entero("@IdUsuarioActor", idActor),
            ParametrosSql.Entero("@IdCliente", id), 
            ParametrosSql.Bit("@SoloActivos", soloActivos)],
            reader => new ClienteEntity(reader.GetInt32(reader.GetOrdinal("IdCliente")), 
                reader.GetString(reader.GetOrdinal("Nombre")), reader.GetString(reader.GetOrdinal("Apellido")),
                reader.GetString(reader.GetOrdinal("Telefono")), reader.GetString(reader.GetOrdinal("Cedula")),
                reader.GetBoolean(reader.GetOrdinal("Estado"))), idActor, ct);
}
