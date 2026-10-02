using System.Text.RegularExpressions;
using AlDia.BLL.Common;
using AlDia.BLL.Seguridad;
using AlDia.DAL.Common;
using AlDia.DAL.Seguridad;
using AlDia.Entity.Clientes;
using AlDia.Entity.Common;
using AlDia.Entity.Equipos;
using AlDia.Entity.Inventario;
using AlDia.Entity.Pagos;
using AlDia.Entity.Reparaciones;
using AlDia.Entity.Seguridad;
using Microsoft.Data.SqlClient;

// Integracion real de las capas. Solo crea y elimina una base temporal propia.
// Ejecutar desde la raiz: dotnet run --project tests/AlDia.Capas.Tests
var pruebas = new PruebasCapas();
await pruebas.EjecutarAsync();

sealed class PruebasCapas
{
    private readonly string _base = "AlDia_Capas_Test_" + Guid.NewGuid().ToString("N");
    private readonly string _master = "Server=localhost;Initial Catalog=master;Integrated Security=true;TrustServerCertificate=true;Connect Timeout=5";
    private string Cadena => new SqlConnectionStringBuilder(_master) { InitialCatalog = _base }.ConnectionString;
    private int _comprobaciones;
    private const string Clave = "PruebaCapas2026!";

    public async Task EjecutarAsync()
    {
        bool creada = false;
        try
        {
            VerificarNombreTemporal();
            await EjecutarSqlAsync(_master, $"CREATE DATABASE [{_base}]");
            creada = true;
            var raiz = EncontrarRaiz();
            string esquema = await File.ReadAllTextAsync(Path.Combine(raiz, "AlDia.DataBase/AlDia.DataBase/Base de datos.sql"));
            int inicio = esquema.IndexOf("CREATE TABLE dbo.Roles", StringComparison.Ordinal);
            if (inicio < 0) throw new InvalidOperationException("No se encontro el inicio del esquema.");
            // Excluye por completo el preambulo que recrea AlDiaDB.
            await EjecutarLotesAsync(esquema[inicio..]);
            string procedimientos = await File.ReadAllTextAsync(Path.Combine(raiz, "AlDia.DataBase/AlDia.DataBase/Procedimientos_Almacenados.sql"));
            procedimientos = Regex.Replace(procedimientos, @"(?im)^\s*USE\s+\[AlDiaDB\]\s*;?\s*$", "");
            await EjecutarLotesAsync(procedimientos);
            Comprobar(await EscalarAsync<int>("SELECT COUNT(*) FROM sys.tables WHERE is_ms_shipped=0") == 16, "16 tablas del esquema real");
            Comprobar(await EscalarAsync<int>("SELECT COUNT(*) FROM sys.procedures WHERE is_ms_shipped=0") == 51, "51 procedimientos instalados");

            var admin = new AplicacionBll(Cadena);
            await SeguridadAsync(admin);
            var (cliente, equipo, servicio, repuesto) = await CatalogosAsync(admin);
            await FlujoAsync(admin, cliente, equipo, servicio, repuesto);
            await ErroresYCancelacionAsync(admin);
            Console.WriteLine($"DAL/BLL: {_comprobaciones} comprobaciones correctas contra SQL Server.");
        }
        finally
        {
            if (creada)
            {
                VerificarNombreTemporal();
                SqlConnection.ClearAllPools();
                await EjecutarSqlAsync(_master, $"ALTER DATABASE [{_base}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{_base}]");
                Console.WriteLine("Base temporal eliminada; AlDiaDB no se modifico.");
            }
        }
    }

    private async Task SeguridadAsync(AplicacionBll admin)
    {
        Console.WriteLine("Comprobando autenticacion, FK de roles y permisos...");
        await DebeFallarAsync<PermisoException>(() => admin.Clientes.ConsultarAsync(), "consulta sin sesion");
        Comprobar(!(await admin.Usuarios.ConsultarInicializacionAsync()).HayUsuarios, "inicializacion sin usuarios");
        int id = await admin.Usuarios.CrearAdministradorInicialAsync(new("Ana", "Admin", "A-001", "admin", RolUsuario.Administrador), Clave);
        Comprobar(id > 0 && (await admin.Usuarios.ConsultarInicializacionAsync()).HayAdministradorActivo, "administrador inicial");
        await DebeFallarAsync<DatosException>(() => admin.Usuarios.CrearAdministradorInicialAsync(new("Ana", "Admin", "A-002", "admin2", RolUsuario.Administrador), Clave), "no repetir administrador inicial");
        await DebeFallarAsync<ValidacionException>(() => admin.Usuarios.AutenticarAsync("admin", "incorrecta"), "clave incorrecta");
        Comprobar(!admin.Sesion.EstaAutenticada, "autenticacion fallida cierra sesion");
        var actor = await admin.Usuarios.AutenticarAsync("admin", Clave);
        var roles = await admin.Roles.ConsultarAsync();
        Comprobar(roles.Count == 3 && actor.IdRol == roles.Single(r => r.Nombre == "Administrador").IdRol, "FK obtenida de SQL");
        actor.Desactivar();
        Comprobar(admin.Sesion.UsuarioActual!.Estado, "sesion conserva su copia");
        var copia = admin.Sesion.UsuarioActual!;
        copia.CambiarRol(RolUsuario.Tecnico);
        Comprobar(admin.Sesion.UsuarioActual!.Rol == RolUsuario.Administrador, "copia no altera permisos");
        string hash = await EscalarAsync<string>("SELECT ContrasenaHash FROM dbo.Usuarios WHERE Usuario='admin'");
        Comprobar(hash != Clave && new HashContrasena().Verificar(Clave, hash), "hash PBKDF2 almacenado");
        Comprobar(typeof(UsuarioEntity).GetProperty("ContrasenaHash") is null, "listados sin credenciales");
        await DebeFallarAsync<DatosException>(() => admin.Usuarios.CambiarEstadoAsync(id, false), "no desactivar propia sesion");

        var rol = new RolEntity("Auditor", "Consulta prevista");
        await admin.Roles.GuardarEntidadAsync(rol);
        Comprobar(rol.IdRol > 0, "alta rol asigna ID");
        rol.CambiarNombre("Auditor externo");
        await admin.Roles.GuardarEntidadAsync(rol);
        Comprobar((await admin.Roles.ObtenerPorIdAsync(rol.IdRol))!.Nombre == rol.Nombre, "editar rol");
        await admin.Roles.CambiarEstadoAsync(rol.IdRol, false);
        Comprobar(!(await admin.Roles.ObtenerPorIdAsync(rol.IdRol))!.Estado, "baja logica rol");
        Comprobar((await admin.Roles.ConsultarAsync()).Count == 3, "roles activos");
        await admin.Roles.CambiarEstadoAsync(rol.IdRol, true);
        await DebeFallarAsync<DatosException>(() => admin.Roles.GuardarEntidadAsync(new RolEntity("Auditor externo", null)), "rol duplicado");
        await DebeFallarAsync<DatosException>(() => admin.Roles.CambiarEstadoAsync(roles[0].IdRol, false), "rol del sistema protegido");
        var sistema = (await admin.Roles.ObtenerPorIdAsync(roles.Single(r => r.Nombre == "Tecnico").IdRol))!;
        sistema.CambiarNombre("tecnico");
        await DebeFallarAsync<DatosException>(() => admin.Roles.GuardarEntidadAsync(sistema), "proteger capitalizacion de permisos");
        Comprobar(await admin.Roles.ObtenerPorIdAsync(int.MaxValue) is null, "rol inexistente devuelve null");
        Pagina(await admin.Roles.ConsultarPaginaAsync(1, 2), 4, 2, "roles primera pagina");
        Pagina(await admin.Roles.ConsultarPaginaAsync(2, 2), 4, 2, "roles segunda pagina");
        Pagina(await admin.Roles.ConsultarPaginaAsync(buscar: "externo"), 1, 1, "buscar rol");

        var rolRecepcion = roles.Single(r => r.Nombre == "Recepcionista");
        var rolTecnico = roles.Single(r => r.Nombre == "Tecnico");
        var recepcion = new UsuarioEntity("Rosa", "Recepcion", "R-001", "recepcion", rolRecepcion);
        var tecnico = new UsuarioEntity("Tomas", "Tecnico", "T-001", "tecnico", rolTecnico);
        await admin.Usuarios.GuardarEntidadAsync(recepcion, Clave);
        await admin.Usuarios.GuardarEntidadAsync(tecnico, Clave);
        // Tambien se conserva el contrato anterior que resuelve el nombre a la FK en SQL.
        await admin.Usuarios.GuardarAsync(new("Luis", "Tecnico", "T-002", "tecnico2", RolUsuario.Tecnico), Clave);
        Comprobar(recepcion.IdUsuario > 0 && tecnico.IdUsuario > 0, "alta usuarios con rol real");
        tecnico.CambiarCorreo("tecnico@example.com");
        await admin.Usuarios.GuardarEntidadAsync(tecnico);
        var guardado = await admin.Usuarios.ObtenerPorIdAsync(tecnico.IdUsuario);
        Comprobar(guardado!.Correo == tecnico.Correo && guardado.IdRol == rolTecnico.IdRol, "editar usuario mantiene FK y clave");
        await admin.Usuarios.CambiarEstadoAsync(tecnico.IdUsuario, false);
        await DebeFallarAsync<ValidacionException>(() => new AplicacionBll(Cadena).Usuarios.AutenticarAsync("tecnico", Clave), "usuario inactivo no ingresa");
        await admin.Usuarios.CambiarEstadoAsync(tecnico.IdUsuario, true);
        Comprobar(await admin.Usuarios.ObtenerPorIdAsync(int.MaxValue) is null, "usuario inexistente devuelve null");
        Pagina(await admin.Usuarios.ConsultarPaginaAsync(1, 2), 4, 2, "usuarios pagina");
        Pagina(await admin.Usuarios.ConsultarPaginaAsync(buscar: "tecnico@example"), 1, 1, "usuarios busqueda correo");

        var externo = new UsuarioEntity("Eva", "Externa", "X-001", "externo", rol);
        await admin.Usuarios.GuardarEntidadAsync(externo, Clave);
        Comprobar((await admin.Usuarios.ObtenerPorIdAsync(externo.IdUsuario))!.PermisoRol is null, "rol sin permisos se puede reconstruir");
        await DebeFallarAsync<ValidacionException>(() => new AplicacionBll(Cadena).Usuarios.AutenticarAsync("externo", Clave), "rol personalizado no recibe permisos implicitos");
        await DebeFallarAsync<DatosException>(() => admin.Roles.CambiarEstadoAsync(rol.IdRol, false), "rol asignado no se desactiva");
        // Estado de rol adulterado solo en la base temporal para verificar defensa del login.
        await EjecutarSqlAsync(Cadena, $"UPDATE dbo.Roles SET Estado=0 WHERE IdRol={rolRecepcion.IdRol}");
        await DebeFallarAsync<ValidacionException>(() => new AplicacionBll(Cadena).Usuarios.AutenticarAsync("recepcion", Clave), "rol inactivo no autentica");
        await EjecutarSqlAsync(Cadena, $"UPDATE dbo.Roles SET Estado=1 WHERE IdRol={rolRecepcion.IdRol}");
        var sinPermiso = new AplicacionBll(Cadena);
        await sinPermiso.Usuarios.AutenticarAsync("tecnico", Clave);
        await DebeFallarAsync<PermisoException>(() => sinPermiso.Roles.ConsultarAsync(), "roles solo administrador");
        await DebeFallarAsync<PermisoException>(() => sinPermiso.Usuarios.ConsultarAsync(), "usuarios solo administrador");
        await DebeFallarAsync<PermisoException>(() => sinPermiso.Pagos.ConsultarAsync(), "pagos solo recepcion y administrador");
        await DebeFallarAsync<PermisoException>(() => sinPermiso.Errores.ConsultarPaginaAsync(), "log solo administrador");
        // Bypass de BLL: el procedimiento tambien comprueba el actor.
        var dal = new RolDal(new EjecutorSql(new ConexionFactory(Cadena)));
        await DebeFallarAsync<DatosException>(() => dal.ConsultarAsync(tecnico.IdUsuario), "SQL comprueba permisos");
    }

    private async Task<(ClienteEntity, EquipoEntity, ServicioEntity, RepuestoEntity)> CatalogosAsync(AplicacionBll admin)
    {
        Console.WriteLine("Comprobando catalogos, edicion, estados y paginacion SQL...");
        var cliente = new ClienteEntity("Carlos", "Cliente", "88888888", "C-001");
        await admin.Clientes.GuardarEntidadAsync(cliente);
        cliente.CambiarTelefono("77777777");
        await admin.Clientes.GuardarEntidadAsync(cliente);
        Comprobar((await admin.Clientes.ObtenerPorIdAsync(cliente.IdCliente))!.Telefono == "77777777", "editar cliente");
        for (int i = 2; i <= 5; i++) await admin.Clientes.GuardarEntidadAsync(new ClienteEntity("Carlos", "Cliente " + i, "88888888", "C-00" + i));
        Pagina(await admin.Clientes.ConsultarPaginaAsync(1, 2), 5, 2, "clientes primera pagina");
        var pagina2 = await admin.Clientes.ConsultarPaginaAsync(2, 2);
        Pagina(pagina2, 5, 2, "clientes segunda pagina");
        Comprobar(!(await admin.Clientes.ConsultarPaginaAsync(1, 2)).Registros.Select(c => c.IdCliente).Intersect(pagina2.Registros.Select(c => c.IdCliente)).Any(), "orden estable sin repeticion");
        Pagina(await admin.Clientes.ConsultarPaginaAsync(3, 2), 5, 1, "clientes ultima pagina");
        Pagina(await admin.Clientes.ConsultarPaginaAsync(4, 2), 5, 0, "pagina fuera del rango");
        Pagina(await admin.Clientes.ConsultarPaginaAsync(buscar: "C-001"), 1, 1, "buscar cedula");
        Pagina(await admin.Clientes.ConsultarPaginaAsync(buscar: "' OR 1=1 --"), 0, 0, "busqueda parametrizada");
        await admin.Clientes.CambiarEstadoAsync(cliente.IdCliente, false);
        Pagina(await admin.Clientes.ConsultarPaginaAsync(soloActivos: true), 4, 4, "filtro activos clientes");
        Comprobar(!(await admin.Clientes.ObtenerPorIdAsync(cliente.IdCliente))!.Estado, "baja logica cliente");
        await admin.Clientes.CambiarEstadoAsync(cliente.IdCliente, true);
        await DebeFallarAsync<DatosException>(() => admin.Clientes.GuardarEntidadAsync(new ClienteEntity("Duplicado", "Cliente", "88888888", "C-001")), "cedula duplicada");
        Comprobar(await admin.Clientes.ObtenerPorIdAsync(int.MaxValue) is null, "cliente inexistente");

        var tipo = new TipoEquipoEntity("Portatil", "Computadora");
        await admin.TiposEquipo.GuardarEntidadAsync(tipo);
        tipo.CambiarDescripcion("Equipo portatil");
        await admin.TiposEquipo.GuardarEntidadAsync(tipo);
        Comprobar((await admin.TiposEquipo.ObtenerPorIdAsync(tipo.IdTipoEquipo))!.Descripcion == tipo.Descripcion, "editar tipo");
        await admin.TiposEquipo.CambiarEstadoAsync(tipo.IdTipoEquipo, false);
        Pagina(await admin.TiposEquipo.ConsultarPaginaAsync(soloActivos: true), 0, 0, "tipo inactivo excluido");
        await admin.TiposEquipo.CambiarEstadoAsync(tipo.IdTipoEquipo, true);
        Pagina(await admin.TiposEquipo.ConsultarPaginaAsync(buscar: "portatil"), 1, 1, "buscar tipo");
        Comprobar(await admin.TiposEquipo.ObtenerPorIdAsync(int.MaxValue) is null, "tipo inexistente");

        var equipo = new EquipoEntity(cliente.IdCliente, tipo.IdTipoEquipo, "Marca", "Modelo", "SERIE-001");
        await admin.Equipos.GuardarEntidadAsync(equipo);
        equipo.CambiarColor("Gris");
        await admin.Equipos.GuardarEntidadAsync(equipo);
        Comprobar((await admin.Equipos.ObtenerPorIdAsync(equipo.IdEquipo))!.Color == "Gris", "editar equipo");
        await admin.Equipos.CambiarEstadoAsync(equipo.IdEquipo, false);
        Pagina(await admin.Equipos.ConsultarPaginaAsync(soloActivos: true), 0, 0, "equipo inactivo excluido");
        await admin.Equipos.CambiarEstadoAsync(equipo.IdEquipo, true);
        Pagina(await admin.Equipos.ConsultarPaginaAsync(buscar: "SERIE-001"), 1, 1, "buscar equipo");
        Comprobar(await admin.Equipos.ObtenerPorIdAsync(int.MaxValue) is null, "equipo inexistente");

        var servicio = new ServicioEntity("Revision general", "Revision", 30);
        await admin.Servicios.GuardarEntidadAsync(servicio);
        servicio.CambiarPrecioBase(35);
        await admin.Servicios.GuardarEntidadAsync(servicio);
        Comprobar((await admin.Servicios.ObtenerPorIdAsync(servicio.IdServicio))!.PrecioBase == 35, "editar servicio");
        await admin.Servicios.CambiarEstadoAsync(servicio.IdServicio, false);
        Pagina(await admin.Servicios.ConsultarPaginaAsync(soloActivos: true), 0, 0, "servicio inactivo excluido");
        await admin.Servicios.CambiarEstadoAsync(servicio.IdServicio, true);
        Pagina(await admin.Servicios.ConsultarPaginaAsync(buscar: "general"), 1, 1, "buscar servicio");
        Comprobar(await admin.Servicios.ObtenerPorIdAsync(int.MaxValue) is null, "servicio inexistente");

        var repuesto = new RepuestoEntity("Memoria RAM", "Modulo", "Marca", "RAM-001", 10, 20);
        await admin.Repuestos.GuardarEntidadAsync(repuesto);
        repuesto.CambiarPrecios(12, 22);
        await admin.Repuestos.GuardarEntidadAsync(repuesto);
        Comprobar((await admin.Repuestos.ObtenerPorIdAsync(repuesto.IdRepuesto))!.PrecioVenta == 22, "editar repuesto");
        await admin.Repuestos.CambiarEstadoAsync(repuesto.IdRepuesto, false);
        Pagina(await admin.Repuestos.ConsultarPaginaAsync(soloActivos: true), 0, 0, "repuesto inactivo excluido");
        await admin.Repuestos.CambiarEstadoAsync(repuesto.IdRepuesto, true);
        await admin.Repuestos.AjustarExistenciasAsync(repuesto.IdRepuesto, 10);
        Comprobar((await admin.Repuestos.ObtenerPorIdAsync(repuesto.IdRepuesto))!.Stock == 10 && repuesto.Stock == 0, "stock consultado se recarga");
        await DebeFallarAsync<DatosException>(() => admin.Repuestos.AjustarExistenciasAsync(repuesto.IdRepuesto, -11), "stock no negativo");
        Pagina(await admin.Repuestos.ConsultarPaginaAsync(buscar: "RAM-001"), 1, 1, "buscar repuesto");
        Comprobar(await admin.Repuestos.ObtenerPorIdAsync(int.MaxValue) is null, "repuesto inexistente");
        return (cliente, equipo, servicio, repuesto);
    }

    private async Task FlujoAsync(AplicacionBll admin, ClienteEntity cliente, EquipoEntity equipo, ServicioEntity servicio, RepuestoEntity repuesto)
    {
        Console.WriteLine("Comprobando recepcion, concurrencia, diagnostico, reparacion, inventario y pagos...");
        var recepcion = new AplicacionBll(Cadena);
        var tecnico1 = new AplicacionBll(Cadena);
        var tecnico2 = new AplicacionBll(Cadena);
        int idRecepcion = (await recepcion.Usuarios.AutenticarAsync("recepcion", Clave)).IdUsuario;
        await tecnico1.Usuarios.AutenticarAsync("tecnico", Clave);
        await tecnico2.Usuarios.AutenticarAsync("tecnico2", Clave);
        await DebeFallarAsync<PermisoException>(() => admin.Ordenes.RegistrarRecepcionAsync(new("NO-PERMITIDA", equipo.IdEquipo, "Problema")), "administrador no suplanta recepcion");
        await DebeFallarAsync<PermisoException>(() => tecnico1.Clientes.GuardarEntidadAsync(new ClienteEntity("Sin", "Permiso", "88888888", "NO")), "tecnico no mantiene clientes");
        await DebeFallarAsync<PermisoException>(() => recepcion.Repuestos.AjustarExistenciasAsync(repuesto.IdRepuesto, 1), "recepcion no ajusta stock");
        var estado = (await recepcion.Ordenes.ConsultarEstadosAsync()).Single(e => e.Nombre == "En Revisión");
        var orden = new OrdenReparacionEntity("ORD-001", equipo.IdEquipo, idRecepcion, estado, "No enciende");
        await recepcion.Ordenes.RegistrarEntidadAsync(orden);
        Comprobar(orden.IdOrden > 0, "recepcion asigna ID");
        await DebeFallarAsync<ValidacionException>(() => recepcion.Ordenes.RegistrarEntidadAsync(orden), "no repetir entidad orden");
        orden.CambiarRecepcion("No enciende al conectar", "Sin golpes", "Cargador");
        await recepcion.Ordenes.CorregirEntidadAsync(orden);
        Comprobar((await recepcion.Ordenes.ObtenerPorIdAsync(orden.IdOrden))!.AccesoriosRecepcion == "Cargador", "corregir recepcion");
        int anulada = await recepcion.Ordenes.RegistrarRecepcionAsync(new("ORD-ANULADA", equipo.IdEquipo, "Registro equivocado"));
        await recepcion.Ordenes.AnularAsync(anulada, "Duplicada");
        Comprobar((await admin.Ordenes.ObtenerPorIdAsync(anulada))!.Anulada, "anular orden y recuperar por ID");
        Pagina(await admin.Ordenes.ConsultarPaginaAsync(filtro: new(IdCliente: cliente.IdCliente)), 1, 1, "ordenes excluyen anuladas");
        Pagina(await admin.Ordenes.ConsultarPaginaAsync(buscar: "ORD-ANULADA", filtro: new(IncluirAnuladas: true)), 1, 1, "buscar orden anulada");
        Comprobar(await admin.Ordenes.ConsultarExpedienteAsync(int.MaxValue) is null, "expediente inexistente");
        await tecnico1.Ordenes.TomarAsync(orden.IdOrden);
        await DebeFallarAsync<DatosException>(() => tecnico2.Ordenes.TomarAsync(orden.IdOrden), "no tomar orden ocupada");
        await tecnico1.Ordenes.LiberarAsync(orden.IdOrden);
        var tomadas = await Task.WhenAll(IntentarAsync(() => tecnico1.Ordenes.TomarAsync(orden.IdOrden)), IntentarAsync(() => tecnico2.Ordenes.TomarAsync(orden.IdOrden)));
        Comprobar(tomadas.Count(x => x) == 1, "dos tecnicos solo uno gana");
        var tecnico = tomadas[0] ? tecnico1 : tecnico2;
        var otro = tomadas[0] ? tecnico2 : tecnico1;
        int idTecnico = tecnico.Sesion.UsuarioActual!.IdUsuario;
        Comprobar((await admin.Ordenes.ObtenerPorIdAsync(orden.IdOrden))!.IdTecnicoResponsable == idTecnico, "responsable persistido");
        Pagina(await admin.Ordenes.ConsultarPaginaAsync(filtro: new(SoloDisponibles: true)), 0, 0, "ocupada no disponible");
        await DebeFallarAsync<DatosException>(() => otro.Diagnosticos.RegistrarAsync(new(orden.IdOrden, "Problema", "Propuesta", 100)), "solo responsable diagnostica");
        await DebeFallarAsync<DatosException>(() => tecnico.Ordenes.IniciarReparacionAsync(orden.IdOrden), "no reparar sin aprobacion");
        var diagnostico = new DiagnosticoEntity(orden.IdOrden, idTecnico, "Fuente danada", "Reparar fuente", 150);
        await tecnico.Diagnosticos.RegistrarEntidadAsync(diagnostico);
        diagnostico.CambiarDiagnostico("Fuente y memoria", "Reparar y reemplazar", 150, "Verificado");
        await tecnico.Diagnosticos.CorregirEntidadAsync(diagnostico);
        Comprobar((await admin.Ordenes.ConsultarExpedienteAsync(orden.IdOrden))!.Diagnostico!.Observaciones == "Verificado", "corregir diagnostico");
        await tecnico.Diagnosticos.RetirarAsync(diagnostico.IdDiagnostico);
        Comprobar((await admin.Ordenes.ConsultarExpedienteAsync(orden.IdOrden))!.Diagnostico is null, "retirar diagnostico");
        diagnostico = new DiagnosticoEntity(orden.IdOrden, idTecnico, "Fuente y memoria", "Reparar y reemplazar", 150);
        await tecnico.Diagnosticos.RegistrarEntidadAsync(diagnostico);
        var decision = new ConfirmacionEntity(diagnostico.IdDiagnostico, idRecepcion, DecisionCliente.Rechazada, null);
        await recepcion.Diagnosticos.RegistrarDecisionEntidadAsync(decision);
        decision.CambiarDecision(DecisionCliente.Aprobada, 150, "Cliente autoriza");
        await recepcion.Diagnosticos.CorregirDecisionEntidadAsync(decision);
        Comprobar((await admin.Ordenes.ConsultarExpedienteAsync(orden.IdOrden))!.Confirmacion!.CostoAprobado == 150, "corregir decision");
        await recepcion.Diagnosticos.RetirarDecisionAsync(decision.IdConfirmacion);
        Comprobar((await admin.Ordenes.ConsultarExpedienteAsync(orden.IdOrden))!.Confirmacion is null, "retirar decision");
        decision = new ConfirmacionEntity(diagnostico.IdDiagnostico, idRecepcion, DecisionCliente.Aprobada, 150);
        await recepcion.Diagnosticos.RegistrarDecisionEntidadAsync(decision);
        await tecnico.Ordenes.IniciarReparacionAsync(orden.IdOrden);
        var detalle = new DetalleServicioEntity(orden.IdOrden, servicio.IdServicio, 1, 30);
        await tecnico.Servicios.RegistrarEntidadEnOrdenAsync(detalle);
        detalle.CambiarImportes(2, 30);
        detalle.CambiarObservaciones("Revision completa");
        await tecnico.Servicios.CorregirEntidadEnOrdenAsync(detalle);
        var consumo = new DetalleRepuestoEntity(orden.IdOrden, repuesto.IdRepuesto, 2, 20);
        await tecnico.Repuestos.ConsumirEntidadAsync(consumo);
        consumo.CambiarImportes(3, 20);
        await tecnico.Repuestos.CorregirConsumoEntidadAsync(consumo);
        Comprobar((await admin.Repuestos.ObtenerPorIdAsync(repuesto.IdRepuesto))!.Stock == 7, "correccion consumo ajusta diferencia");
        consumo.CambiarImportes(2, 20);
        await tecnico.Repuestos.CorregirConsumoEntidadAsync(consumo);
        var expediente = (await admin.Ordenes.ConsultarExpedienteAsync(orden.IdOrden))!;
        Comprobar(expediente.Orden.Total == 100 && expediente.Servicios.Single().Subtotal == 60 && expediente.Repuestos.Single().Subtotal == 40, "total de servicios y repuestos");
        Comprobar(expediente.Servicios.Single().IdTecnicoResponsable == idTecnico && expediente.Repuestos.Single().IdTecnicoResponsable == idTecnico, "tecnico se deriva de la orden");
        await DebeFallarAsync<DatosException>(() => tecnico.Repuestos.CorregirConsumoAsync(new(consumo.IdDetalleRepuesto, 20, 20)), "stock insuficiente revierte");
        await DebeFallarAsync<DatosException>(() => tecnico.Repuestos.CorregirConsumoAsync(new(consumo.IdDetalleRepuesto, 7, 20)), "costo autorizado revierte");
        Comprobar((await admin.Repuestos.ObtenerPorIdAsync(repuesto.IdRepuesto))!.Stock == 8 && (await admin.Ordenes.ObtenerPorIdAsync(orden.IdOrden))!.Total == 100, "rollback conserva stock y total");
        var extraServicio = new ServicioEntity("Limpieza", null, 5);
        await admin.Servicios.GuardarEntidadAsync(extraServicio);
        int idExtraServicio = await tecnico.Servicios.RegistrarEnOrdenAsync(new(orden.IdOrden, extraServicio.IdServicio, 1, 5));
        await tecnico.Servicios.RetirarDeOrdenAsync(idExtraServicio);
        var extraRepuesto = new RepuestoEntity("Cable", null, null, "CB-001", 5, 10);
        await admin.Repuestos.GuardarEntidadAsync(extraRepuesto);
        await admin.Repuestos.AjustarExistenciasAsync(extraRepuesto.IdRepuesto, 5);
        int idExtraConsumo = await tecnico.Repuestos.ConsumirAsync(new(orden.IdOrden, extraRepuesto.IdRepuesto, 1, 10));
        await tecnico.Repuestos.RetirarConsumoAsync(idExtraConsumo);
        Comprobar((await admin.Repuestos.ObtenerPorIdAsync(extraRepuesto.IdRepuesto))!.Stock == 5, "retirar consumo restaura stock");
        await tecnico.Ordenes.FinalizarReparacionAsync(orden.IdOrden, true, "Equipo operativo");
        await DebeFallarAsync<DatosException>(() => recepcion.Ordenes.EntregarAsync(orden.IdOrden, null), "entrega con saldo bloqueada");
        var pago = new PagoEntity(orden.IdOrden, idRecepcion, 40, MetodoPago.Efectivo, "Abono");
        await recepcion.Pagos.RegistrarEntidadAsync(pago);
        int pagoNuevo = await recepcion.Pagos.CorregirEntidadAsync(pago, 30, MetodoPago.Tarjeta, "Monto correcto", "Abono corregido");
        Comprobar(pagoNuevo != pago.IdPago && pago.Monto == 40 && !pago.Anulado, "correccion no modifica entidad historica");
        var pagos = await recepcion.Pagos.ConsultarAsync(orden.IdOrden, true);
        Comprobar(pagos.Count == 2 && pagos.Single(p => p.IdPago == pago.IdPago).Anulado, "original anulado conservado");
        Comprobar((await admin.Pagos.ObtenerPorIdAsync(pago.IdPago))!.Anulado, "obtener pago anulado por ID");
        Comprobar(await admin.Pagos.ObtenerPorIdAsync(int.MaxValue) is null, "pago inexistente devuelve null");
        await recepcion.Pagos.AnularEntidadAsync(pagos.Single(p => p.IdPago == pagoNuevo), "Medio equivocado");
        Comprobar((await recepcion.Pagos.ConsultarAsync(orden.IdOrden)).Count == 0, "anular pago excluye de saldo");
        await recepcion.Pagos.RegistrarAsync(new(orden.IdOrden, 40, MetodoPago.Efectivo, "Abono vigente"));
        await DebeFallarAsync<DatosException>(() => recepcion.Pagos.RegistrarAsync(new(orden.IdOrden, 61, MetodoPago.Efectivo)), "no sobrepagar");
        await recepcion.Pagos.RegistrarAsync(new(orden.IdOrden, 60, MetodoPago.Transferencia, "Saldo final"));
        Pagina(await admin.Pagos.ConsultarPaginaAsync(1, 1, idOrden: orden.IdOrden), 2, 1, "paginacion de pagos vigentes");
        Pagina(await admin.Pagos.ConsultarPaginaAsync(buscar: "Monto correcto", idOrden: orden.IdOrden, incluirAnulados: true), 1, 1, "buscar motivo de anulacion");
        Comprobar((await admin.Ordenes.ObtenerPorIdAsync(orden.IdOrden))!.Saldo == 0, "saldo final cero");
        await recepcion.Ordenes.EntregarAsync(orden.IdOrden, "Cliente recibe");
        expediente = (await admin.Ordenes.ConsultarExpedienteAsync(orden.IdOrden))!;
        Comprobar(expediente.Orden.EstadoNombre == "Entregada" && expediente.Orden.FechaEntrega.HasValue, "entrega completa");
        Comprobar(expediente.Historial.Count >= 8 && expediente.Historial.All(h => h.IdOrden == orden.IdOrden), "historial reconstruido");
        await DebeFallarAsync<DatosException>(() => recepcion.Pagos.AnularAsync(pago.IdPago, "No permitido"), "auditoria despues de entrega protegida");
        Pagina(await admin.Ordenes.ConsultarPaginaAsync(buscar: "Carlos", filtro: new(IdCliente: cliente.IdCliente, IdEstado: expediente.Orden.IdEstado)), 1, 1, "filtros compuestos ordenes");
        await DebeFallarAsync<DatosException>(() => recepcion.Ordenes.CorregirRecepcionAsync(new(orden.IdOrden, "Cambio tardio")), "correccion tardia bloqueada");
        Comprobar(await admin.Ordenes.ObtenerPorIdAsync(int.MaxValue) is null, "orden inexistente");

        // Dos ordenes y dos tecnicos consumen la ultima unidad del mismo repuesto.
        var escaso = new RepuestoEntity("Ultima unidad", null, null, "ESCASO", 5, 10);
        await admin.Repuestos.GuardarEntidadAsync(escaso);
        await admin.Repuestos.AjustarExistenciasAsync(escaso.IdRepuesto, 1);
        int ordenA = await PrepararOrdenAsync(recepcion, tecnico1, "ORD-STOCK-A", equipo.IdEquipo);
        int ordenB = await PrepararOrdenAsync(recepcion, tecnico2, "ORD-STOCK-B", equipo.IdEquipo);
        var consumos = await Task.WhenAll(
            IntentarAsync(() => tecnico1.Repuestos.ConsumirAsync(new(ordenA, escaso.IdRepuesto, 1, 10))),
            IntentarAsync(() => tecnico2.Repuestos.ConsumirAsync(new(ordenB, escaso.IdRepuesto, 1, 10))));
        Comprobar(consumos.Count(x => x) == 1, "consumo simultaneo solo uno gana");
        Comprobar((await admin.Repuestos.ObtenerPorIdAsync(escaso.IdRepuesto))!.Stock == 0, "stock simultaneo no negativo");
        var expA = (await admin.Ordenes.ConsultarExpedienteAsync(ordenA))!;
        var expB = (await admin.Ordenes.ConsultarExpedienteAsync(ordenB))!;
        Comprobar(expA.Repuestos.Count + expB.Repuestos.Count == 1 && expA.Orden.Total + expB.Orden.Total == 10, "transaccion perdedora no deja detalle ni total");

        int rechazada = await recepcion.Ordenes.RegistrarRecepcionAsync(new("ORD-RECHAZADA", equipo.IdEquipo, "Revision"));
        await tecnico1.Ordenes.TomarAsync(rechazada);
        int diagRechazado = await tecnico1.Diagnosticos.RegistrarAsync(new(rechazada, "Falla", "Reparacion", 100));
        await recepcion.Diagnosticos.RegistrarDecisionAsync(new(diagRechazado, DecisionCliente.Rechazada, null));
        await DebeFallarAsync<DatosException>(() => tecnico1.Ordenes.IniciarReparacionAsync(rechazada), "decision rechazada impide reparar");
        await recepcion.Ordenes.EntregarAsync(rechazada, "Devuelto sin reparar");
        Comprobar((await admin.Ordenes.ObtenerPorIdAsync(rechazada))!.EstadoNombre == "Entregada", "entregar rechazo sin saldo");
        int noReparada = await PrepararOrdenAsync(recepcion, tecnico1, "ORD-NO-REPARADA", equipo.IdEquipo);
        await tecnico1.Ordenes.FinalizarReparacionAsync(noReparada, false, "No fue posible reparar");
        Comprobar((await admin.Ordenes.ObtenerPorIdAsync(noReparada))!.EstadoNombre == "No Reparada", "resultado no reparada");
        await recepcion.Ordenes.EntregarAsync(noReparada, "Devuelto");
        Comprobar((await admin.Ordenes.ObtenerPorIdAsync(noReparada))!.FechaEntrega.HasValue, "entrega de equipo no reparado");
    }

    private static async Task<int> PrepararOrdenAsync(AplicacionBll recepcion, AplicacionBll tecnico, string numero, int idEquipo)
    {
        int orden = await recepcion.Ordenes.RegistrarRecepcionAsync(new(numero, idEquipo, "Revision"));
        await tecnico.Ordenes.TomarAsync(orden);
        int diagnostico = await tecnico.Diagnosticos.RegistrarAsync(new(orden, "Falla", "Reparacion", 100));
        await recepcion.Diagnosticos.RegistrarDecisionAsync(new(diagnostico, DecisionCliente.Aprobada, 100));
        await tecnico.Ordenes.IniciarReparacionAsync(orden);
        return orden;
    }

    private async Task ErroresYCancelacionAsync(AplicacionBll admin)
    {
        Console.WriteLine("Comprobando errores SQL, validaciones y cancelacion...");
        var errores = await admin.Errores.ConsultarPaginaAsync(tamPagina: 200);
        Comprobar(errores.TotalRegistros > 10 && errores.Registros.All(e => e.IdLog > 0 && e.FechaError.HasValue), "errores registrados y reconstruidos");
        Comprobar(errores.Registros.Any(e => e.Procedimiento?.Contains("usp_GuardarRol") == true), "log identifica procedimiento");
        var filtrados = await admin.Errores.ConsultarPaginaAsync(buscar: "Stock insuficiente");
        Comprobar(filtrados.TotalRegistros >= 1 && filtrados.Registros.All(e => e.MensajeError.Contains("Stock insuficiente")), "busqueda de errores");
        await DebeFallarAsync<ValidacionException>(() => admin.Clientes.ConsultarPaginaAsync(0), "pagina positiva");
        await DebeFallarAsync<ValidacionException>(() => admin.Clientes.ConsultarPaginaAsync(tamPagina: 201), "tamano maximo");
        await DebeFallarAsync<ValidacionException>(() => admin.Roles.ConsultarPaginaAsync(buscar: new string('x', 101)), "limite de busqueda");
        await DebeFallarAsync<ValidacionException>(() => admin.Ordenes.ConsultarPaginaAsync(filtro: new(IdCliente: -1)), "filtro ID invalido");
        await DebeFallarAsync<ValidacionException>(() => admin.Clientes.CambiarEstadoAsync(int.MaxValue, false), "cambiar inexistente");
        using var cancelacion = new CancellationTokenSource();
        cancelacion.Cancel();
        await DebeFallarAsync<OperationCanceledException>(() => admin.Roles.ConsultarPaginaAsync(ct: cancelacion.Token), "cancelacion llega a DAL");
        Comprobar((await admin.Roles.ConsultarAsync()).Count >= 3, "conexion disponible tras cancelacion");
        admin.Usuarios.CerrarSesion();
        await DebeFallarAsync<PermisoException>(() => admin.Ordenes.ConsultarAsync(), "cierre de sesion impide operaciones");
    }

    private void Pagina<T>(PaginacionEntity<T> pagina, int total, int filas, string nombre)
    {
        Comprobar(pagina.TotalRegistros == total && pagina.Registros.Count == filas, nombre);
        Comprobar(pagina.TotalPaginas == (total == 0 ? 0 : (total + pagina.TamPagina - 1) / pagina.TamPagina), nombre + " total paginas");
    }
    private void Comprobar(bool condicion, string nombre)
    {
        if (!condicion) throw new InvalidOperationException("FALLO: " + nombre);
        _comprobaciones++;
    }
    private async Task DebeFallarAsync<T>(Func<Task> accion, string nombre) where T : Exception
    {
        try { await accion(); }
        catch (T) { _comprobaciones++; return; }
        throw new InvalidOperationException("FALLO: se esperaba " + typeof(T).Name + ": " + nombre);
    }
    private static async Task<bool> IntentarAsync(Func<Task> accion)
    {
        try { await accion(); return true; }
        catch (DatosException) { return false; }
    }
    private async Task<T> EscalarAsync<T>(string sql)
    {
        using var cn = new SqlConnection(Cadena);
        await cn.OpenAsync();
        using var cmd = new SqlCommand(sql, cn);
        return (T)(await cmd.ExecuteScalarAsync())!;
    }
    private static async Task EjecutarSqlAsync(string cadena, string sql)
    {
        using var cn = new SqlConnection(cadena);
        await cn.OpenAsync();
        using var cmd = new SqlCommand(sql, cn) { CommandTimeout = 30 };
        await cmd.ExecuteNonQueryAsync();
    }
    private async Task EjecutarLotesAsync(string script)
    {
        if (Regex.IsMatch(script, @"(?i)\b(?:USE|DROP\s+DATABASE|ALTER\s+DATABASE|CREATE\s+DATABASE)\b"))
            throw new InvalidOperationException("El script de prueba no puede cambiar la base seleccionada.");
        foreach (string lote in Regex.Split(script, @"(?im)^\s*GO\s*(?:--[^\r\n]*)?$"))
            if (!string.IsNullOrWhiteSpace(lote)) await EjecutarSqlAsync(Cadena, lote);
    }
    private void VerificarNombreTemporal()
    {
        if (!Regex.IsMatch(_base, @"^AlDia_Capas_Test_[a-f0-9]{32}$"))
            throw new InvalidOperationException("Nombre temporal invalido.");
    }
    private static string EncontrarRaiz()
    {
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
            if (File.Exists(Path.Combine(dir.FullName, "AGENTS.md")) && Directory.Exists(Path.Combine(dir.FullName, "AlDia.DataBase"))) return dir.FullName;
        throw new InvalidOperationException("No se encontro la raiz del proyecto.");
    }
}
