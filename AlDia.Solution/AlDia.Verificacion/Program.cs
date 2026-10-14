using AlDia.BLL.Common;
using AlDia.DAL.Common;
using AlDia.Entity.Common;
using AlDia.Entity.Base;
using AlDia.Entity.Clientes;
using AlDia.Entity.Equipos;
using AlDia.Entity.Inventario;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Reparaciones;
using AlDia.Entity.Pagos;
using AlDia.UI.Formularios.Base;
using AlDia.UI.Formularios.Hijos.Catalogos;
using AlDia.UI.Formularios.Hijos.Usuarios;
using AlDia.UI.Formularios.Hijos.Clientes;
using AlDia.UI.Formularios.Hijos.Equipos;
using AlDia.UI.Formularios.Hijos.Inventario;
using AlDia.UI.Formularios.Hijos.OrdenesServicio;
using AlDia.UI.Formularios.Hijos.Pagos;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace AlDia.Verificacion;

internal static class Program
{
    private static int _comprobaciones;
    private static string _raiz = "";
    private static string _baseTemporal = "";
    private static string _cadenaMaster = "";
    private static bool _creada;
    private static readonly string Clave = "Verificacion-2026!";

    [STAThread]
    private static int Main(string[] args)
    {
        _raiz = args.Length > 0 ? Path.GetFullPath(args[0]) : Path.GetFullPath(".");
        _baseTemporal = "AlDia_Verificacion_" + Guid.NewGuid().ToString("N");
        _cadenaMaster = new SqlConnectionStringBuilder
        {
            DataSource = Environment.GetEnvironmentVariable("ALDIA_SERVIDOR_PRUEBAS") ?? "localhost",
            InitialCatalog = "master", IntegratedSecurity = true, TrustServerCertificate = true, ConnectTimeout = 5
        }.ConnectionString;
        try
        {
            VerificarEntidades();
            PrepararAsync().GetAwaiter().GetResult();
            var builder = new SqlConnectionStringBuilder(_cadenaMaster) { InitialCatalog = _baseTemporal };
            var app = new AplicacionBll(builder.ConnectionString);
            VerificarNegocioAsync(app).GetAwaiter().GetResult();
            ApplicationConfiguration.Initialize();
            using var comprobador = new ComprobadorUi(app, _raiz);
            Application.Run(comprobador);
            if (comprobador.Error is not null) throw comprobador.Error;
            Console.WriteLine($"OK: {_comprobaciones} comprobaciones; Entity, SQL, BLL y herencia UI.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("FALLO: " + ex.Message);
            return 1;
        }
        finally
        {
            if (_creada) EliminarTemporalAsync().GetAwaiter().GetResult();
        }
    }

    private static void VerificarEntidades()
    {
        var incompletas = new Action[]
        {
            () => new RolEntity().CrearSolicitud(),
            () => new TipoEquipoEntity().CrearSolicitud(),
            () => new ServicioEntity().CrearSolicitud(),
            () => new RepuestoEntity().CrearSolicitud(),
            () => new ClienteEntity().CrearSolicitud(),
            () => new UsuarioEntity().CrearSolicitudPorRol(),
            () => new EquipoEntity().CrearSolicitud(),
            () => new DetalleServicioEntity().CrearSolicitud(),
            () => new DetalleRepuestoEntity().CrearSolicitud(),
            () => new OrdenReparacionEntity().CrearSolicitud(),
            () => new DiagnosticoEntity().CrearSolicitud(),
            () => new ConfirmacionEntity().CrearSolicitud(),
            () => new PagoEntity().CrearSolicitud()
        };
        foreach (var crear in incompletas) RechazarDatos(crear, "Una entidad incompleta no genera una solicitud");

        PersonaEntity persona = new ClienteEntity();
        persona.CambiarNombre(" Cliente ");
        persona.CambiarApellido("Prueba");
        persona.CambiarCedula("TEST-ENTITY");
        persona.CambiarTelefono("88888888");
        RechazarDatos(() => persona.CambiarTelefono(null), "El override de Cliente exige teléfono mediante PersonaEntity");
        Comprobar(persona.Nombre == "Cliente" && persona.Telefono == "88888888", "Los métodos heredados normalizan y conservan los datos ante un error");
        Comprobar(((ClienteEntity)persona).CrearSolicitud().Telefono == "88888888", "Cliente se completa con los métodos del base");
        PersonaEntity usuarioSinTelefono = new UsuarioEntity();
        usuarioSinTelefono.CambiarTelefono(null);
        Comprobar(usuarioSinTelefono.Telefono is null, "Usuario conserva el teléfono opcional del base");

        foreach (var catalogo in new CatalogoEntity[] { new RolEntity(), new TipoEquipoEntity(), new ServicioEntity(), new RepuestoEntity() })
        {
            Comprobar(!catalogo.Estado && catalogo.Nombre == "", "El constructor vacío conserva el estado inicial del patrón del profesor");
            catalogo.CambiarNombre("Catálogo");
            catalogo.CambiarDescripcion("Descripción");
            catalogo.Activar();
            Comprobar(catalogo.Nombre == "Catálogo" && catalogo.EstadoNombre == "Activo", "El hijo reutiliza los métodos de CatalogoEntity");
        }
        var rol = new RolEntity();
        rol.CambiarNombre("Administrador");
        rol.Activar();
        rol.AsignarId(1);
        RechazarDatos(() => rol.AsignarId(2), "AsignarId conserva la identidad de un registro existente");
        RechazarDatos(() => rol.CambiarNombre("   "), "El método del profesor rechaza un nombre vacío");
        Comprobar(rol.Nombre == "Administrador", "Un cambio inválido no sustituye el nombre anterior");
        string textoLargo = new('x', 1100);
        rol.CambiarNombre(" " + textoLargo + " ");
        rol.CambiarDescripcion(textoLargo);
        Comprobar(rol.CrearSolicitud().Nombre == textoLargo && rol.Descripcion == textoLargo,
            "CatalogoEntity normaliza los textos sin imponer tamaños SQL");
        persona.CambiarNombre(textoLargo);
        persona.CambiarApellido(textoLargo);
        persona.CambiarTelefono(textoLargo);
        Comprobar(((ClienteEntity)persona).CrearSolicitud().Nombre == textoLargo && persona.Apellido == textoLargo && persona.Telefono == textoLargo,
            "PersonaEntity y el override de Cliente aceptan textos sin límites SQL");
        rol.CambiarNombre("Administrador");
        var usuario = new UsuarioEntity();
        usuario.CambiarNombre("Usuario");
        usuario.CambiarApellido("Prueba");
        usuario.CambiarCedula("TEST-USUARIO");
        usuario.CambiarUsuario("usuario-prueba");
        usuario.CambiarRol(rol);
        usuario.Activar();
        Comprobar(usuario.CrearSolicitudPorRol().IdRol == 1 && usuario.Copiar().IdRol == 1, "Usuario construido mediante métodos conserva la FK y su copia");

        var servicio = new DetalleServicioEntity();
        servicio.CambiarOrden(1);
        servicio.CambiarServicio(1);
        servicio.CambiarCantidad(2);
        servicio.CambiarPrecio(12);
        Comprobar(servicio.CrearSolicitud().Cantidad == 2 && servicio.Subtotal == 24, "DetalleServicio reutiliza los importes y el subtotal del base");
        RechazarDatos(() => servicio.CambiarOrden(2), "Un detalle no puede trasladarse a otra orden");
        RechazarDatos(() => servicio.CambiarImportes(3, -1), "La edición de importes se valida antes de asignar");
        Comprobar(servicio.Cantidad == 2 && servicio.Subtotal == 24, "Una edición inválida conserva ambos importes");
        var repuesto = new DetalleRepuestoEntity();
        repuesto.CambiarOrden(1);
        repuesto.CambiarRepuesto(1);
        repuesto.CambiarImportes(3, 5);
        Comprobar(repuesto.CrearSolicitud().Cantidad == 3 && repuesto.Subtotal == 15, "DetalleRepuesto reutiliza el mismo base");

        var diagnostico = new DiagnosticoEntity();
        diagnostico.CambiarOrden(1);
        diagnostico.CambiarTecnico(1);
        diagnostico.CambiarDiagnostico("Falla", "Reparación", 10);
        Comprobar(diagnostico.CrearSolicitud().CostoEstimado == 10, "Diagnóstico se completa mediante sus métodos");
        diagnostico.CambiarDiagnostico(textoLargo, textoLargo, 10, textoLargo);
        RechazarDatos(() => diagnostico.CambiarDiagnostico("Otro problema", " ", 20), "Diagnóstico rechaza reparación vacía antes de modificar sus campos");
        Comprobar(diagnostico.CrearSolicitud().ProblemaEncontrado == textoLargo && diagnostico.ReparacionPropuesta == textoLargo
            && diagnostico.Observaciones == textoLargo && diagnostico.CostoEstimado == 10,
            "Diagnóstico no limita caracteres y mantiene la edición atómica");
        var confirmacion = new ConfirmacionEntity();
        confirmacion.CambiarDiagnostico(1);
        confirmacion.CambiarUsuario(1);
        confirmacion.CambiarDecision(DecisionCliente.Aprobada, 0);
        RechazarDatos(() => confirmacion.CambiarDecision(DecisionCliente.Rechazada, 1), "Una decisión incompatible se rechaza");
        Comprobar(confirmacion.CrearSolicitud().CostoAprobado == 0, "La decisión anterior permanece y admite costo cero");

        var estado = new EstadoReparacionEntity();
        estado.AsignarId(1);
        estado.CambiarNombre("En Revisión");
        var orden = new OrdenReparacionEntity();
        orden.CambiarNumeroOrden("TEST-ENTITY");
        orden.CambiarEquipo(1);
        orden.CambiarRecepcionista(1);
        orden.CambiarEstadoInicial(estado);
        orden.CambiarRecepcion("No enciende");
        Comprobar(orden.CrearSolicitud().ProblemaReportado == "No enciende", "Orden se prepara con el estado inicial real");
        orden.AsignarId(1);
        RechazarDatos(() => orden.CambiarNumeroOrden("OTRA"), "Una orden guardada conserva su número");

        var pago = new PagoEntity();
        pago.CambiarOrden(1);
        pago.CambiarUsuario(1);
        pago.CambiarMonto(10);
        pago.CambiarMetodoPago(MetodoPago.Efectivo);
        Comprobar(pago.CrearSolicitud().Monto == 10, "Pago nuevo se completa mediante métodos");
        pago.AsignarId(1);
        RechazarDatos(() => pago.CambiarMonto(20), "Un pago persistido conserva su importe histórico");
        Comprobar(pago.CrearCorreccion(20, MetodoPago.Transferencia, "Corrección").Monto == 20 && pago.Monto == 10,
            "La corrección prepara un reemplazo y conserva el pago original");
        Comprobar(pago.CrearCorreccion(20, MetodoPago.Transferencia, textoLargo, textoLargo).Motivo == textoLargo
            && pago.CrearAnulacion(textoLargo).Motivo == textoLargo,
            "Los motivos de pago son obligatorios sin límites de columna en Entity");
        var conexion = new AlDia.Entity.Entity.ConexionEntity(textoLargo, textoLargo, textoLargo, " clave ");
        RechazarDatos(() => conexion.CambiarDatosConexion("otro", " ", "usuario", "clave"), "Configuración incompleta no reemplaza los datos de conexión");
        Comprobar(conexion.Servidor == textoLargo && conexion.Password == " clave ", "Configuración conserva la atomicidad y la contraseña sin imponer tamaños SQL");
        var conexionJson = System.Text.Json.JsonSerializer.Deserialize<AlDia.Entity.Entity.ConexionEntity>(System.Text.Json.JsonSerializer.Serialize(conexion));
        Comprobar(conexionJson?.Servidor == textoLargo && conexionJson.Password == " clave ", "Constructor e inicializadores conservan la compatibilidad JSON");
        Comprobar((string)ParametrosSql.Texto("@Texto", textoLargo[..50], 50).Value == textoLargo[..50], "DAL conserva exactamente el texto que cabe en el parámetro SQL");
        RechazarDatos(() => ParametrosSql.Texto("@Texto", textoLargo[..51], 50), "DAL impide truncar un texto al crear el parámetro SQL");
        Comprobar((string)ParametrosSql.Texto("@MensajeError", textoLargo, -1, true).Value == textoLargo
            && ParametrosSql.Texto("@Opcional", null, 50).Value == DBNull.Value, "DAL conserva NVARCHAR(MAX) y parámetros nulos");
        Console.WriteLine("Constructores, bases, herencia y validaciones de Entity verificadas.");
    }

    private static void RechazarDatos(Action operacion, string mensaje)
    {
        bool rechazada = false;
        try { operacion(); }
        catch (ValidacionException) { rechazada = true; }
        Comprobar(rechazada, mensaje);
    }

    private static async Task PrepararAsync()
    {
        using var cn = new SqlConnection(_cadenaMaster);
        await cn.OpenAsync();
        // Solo crear una base nueva; nunca ejecutar el bloque de eliminación del script original.
        using (var cmd = new SqlCommand($"CREATE DATABASE [{_baseTemporal}]", cn)) await cmd.ExecuteNonQueryAsync();
        _creada = true;
        string script = await File.ReadAllTextAsync(Path.Combine(_raiz, "AlDia.DataBase/AlDia.DataBase/Base de datos.sql"));
        int inicio = script.IndexOf("CREATE TABLE dbo.Roles", StringComparison.Ordinal);
        if (inicio < 0) throw new Exception("No se encontró el esquema de prueba.");
        script = $"USE [{_baseTemporal}];\nGO\n" + script[inicio..];
        await EjecutarLotesAsync(cn, script);
        string procedimientos = await File.ReadAllTextAsync(Path.Combine(_raiz, "AlDia.DataBase/AlDia.DataBase/Procedimientos_Almacenados.sql"));
        procedimientos = procedimientos.Replace("[AlDiaDB]", $"[{_baseTemporal}]", StringComparison.Ordinal);
        await EjecutarLotesAsync(cn, procedimientos);
        Console.WriteLine("Base temporal creada con los procedimientos existentes.");
        await VerificarParametrosAsync(cn);
    }

    private static async Task EjecutarLotesAsync(SqlConnection cn, string script)
    {
        foreach (string lote in Regex.Split(script, @"^\s*GO\s*(?:--[^\r\n]*)?$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
        {
            if (string.IsNullOrWhiteSpace(lote)) continue;
            using var cmd = new SqlCommand(lote, cn) { CommandTimeout = 60 };
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private static async Task VerificarParametrosAsync(SqlConnection cn)
    {
        var metadatos = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        using (var cmd = new SqlCommand("SELECT SCHEMA_NAME(o.schema_id)+'.'+o.name,p.name FROM sys.procedures o JOIN sys.parameters p ON p.object_id=o.object_id", cn))
        using (var dr = await cmd.ExecuteReaderAsync())
            while (await dr.ReadAsync())
            {
                string nombre = dr.GetString(0);
                if (!metadatos.TryGetValue(nombre, out var parametros)) metadatos[nombre] = parametros = [];
                parametros.Add(dr.GetString(1));
            }
        foreach (string archivo in Directory.EnumerateFiles(Path.Combine(_raiz, "AlDia.Solution/AlDia.DAL"), "*.cs", SearchOption.AllDirectories))
        {
            string fuente = await File.ReadAllTextAsync(archivo);
            var comandos = Regex.Matches(fuente, "using SqlCommand cmd = new\\(\"(dbo\\.usp_[^\"]+)\", cn\\);([\\s\\S]*?)await cn.OpenAsync");
            foreach (Match comando in comandos)
            {
                string nombre = comando.Groups[1].Value;
                Comprobar(metadatos.ContainsKey(nombre), "Procedimiento existente: " + nombre);
                foreach (Match parametro in Regex.Matches(comando.Groups[2].Value, "ParametrosSql\\.\\w+\\(\"(@[^\"]+)\""))
                    Comprobar(metadatos[nombre].Contains(parametro.Groups[1].Value), "Parámetro de " + nombre + ": " + parametro.Groups[1].Value);
            }
        }
    }

    private static async Task VerificarNegocioAsync(AplicacionBll app)
    {
        Comprobar(!(await app.Usuarios.ConsultarInicializacionAsync()).HayUsuarios, "Inicialización vacía");
        await app.Usuarios.CrearAdministradorInicialAsync(new UsuarioSolicitud("Admin", "Prueba", "TEST-ADMIN", "admin-prueba", RolUsuario.Administrador), Clave);
        await app.Usuarios.AutenticarAsync("admin-prueba", Clave);
        await RechazarOperacionAsync(() => app.Roles.AgregarAsync(new RolEntity()), "BLL rechaza el rol incompleto antes de SQL");
        await RechazarOperacionAsync(() => app.Usuarios.AgregarAsync(new UsuarioEntity(), Clave), "BLL rechaza el usuario incompleto antes de SQL");
        await RechazarOperacionAsync(() => app.Clientes.AgregarAsync(new ClienteEntity()), "BLL rechaza el cliente incompleto antes de SQL");
        await RechazarOperacionAsync(() => app.Equipos.AgregarAsync(new EquipoEntity()), "BLL rechaza el equipo incompleto antes de SQL");
        await RechazarOperacionAsync(() => app.TiposEquipo.AgregarAsync(new TipoEquipoEntity()), "BLL rechaza el tipo incompleto antes de SQL");
        await RechazarOperacionAsync(() => app.Servicios.AgregarAsync(new ServicioEntity()), "BLL rechaza el servicio incompleto antes de SQL");
        await RechazarOperacionAsync(() => app.Repuestos.AgregarAsync(new RepuestoEntity()), "BLL rechaza el repuesto incompleto antes de SQL");
        var rolLargo = new RolEntity(new string('x', 51));
        await RechazarOperacionAsync(() => app.Roles.AgregarAsync(rolLargo), "La DAL rechaza un nombre mayor que el parámetro del procedimiento");
        Comprobar(rolLargo.IdRol == 0 && !(await app.Roles.ConsultarAsync()).Any(x => x.Nombre == new string('x', 50)),
            "Un texto largo no asigna ID ni guarda una versión recortada en SQL");
        var nuevoRol = new RolEntity("Rol de prueba", "Prueba de creación");
        await RechazarOperacionAsync(() => app.Roles.ActualizarAsync(nuevoRol), "Actualizar no crea un rol nuevo");
        await app.Roles.AgregarAsync(nuevoRol);
        await RechazarOperacionAsync(() => app.Roles.AgregarAsync(nuevoRol), "Agregar no modifica un rol existente");
        Comprobar(nuevoRol.IdRol > 0, "ID OUTPUT asignado a la entidad");
        var rol = await app.Roles.ObtenerPorIdAsync(nuevoRol.IdRol) ?? throw new Exception("Rol no encontrado.");
        rol.CambiarDescripcion("Descripción editada");
        await app.Roles.ActualizarAsync(rol);
        Comprobar((await app.Roles.ObtenerPorIdAsync(rol.IdRol))?.Descripcion == "Descripción editada", "Edición de entidad existente");
        await app.Roles.CambiarEstadoAsync(rol.IdRol, false);
        var inactivos = await app.Roles.MostrarAsync(1, 1, "rol de prueba", false);
        Comprobar(inactivos.TotalRegistros == 1 && inactivos.Registros.Count == 1, "Filtro aplicado antes de paginar");
        var rolRecepcion = (await app.Roles.ConsultarAsync()).Single(x => x.Nombre == "Recepcionista");
        var rolTecnico = (await app.Roles.ConsultarAsync()).Single(x => x.Nombre == "Tecnico");
        await app.Usuarios.AgregarAsync(new UsuarioEntity("Recepción", "Prueba", "TEST-RECEP", "recep-prueba", rolRecepcion), Clave);
        await app.Usuarios.AgregarAsync(new UsuarioEntity("Técnico", "Prueba", "TEST-TECNICO", "tecnico-prueba", rolTecnico), Clave);
        var usuario = (await app.Usuarios.MostrarAsync(nombreRol: "Recepcionista")).Registros.Single();
        Comprobar(usuario.IdRol == rolRecepcion.IdRol, "Rol por identificador SQL");
        usuario.CambiarCorreo("prueba@example.com");
        await app.Usuarios.ActualizarAsync(usuario);
        Comprobar((await app.Usuarios.ObtenerPorIdAsync(usuario.IdUsuario))?.Correo == "prueba@example.com", "Actualizar usuario conserva su ID y la contraseña anterior");
        await RechazarOperacionAsync(() => app.Usuarios.AgregarAsync(usuario, Clave), "Agregar usuario exige un registro nuevo");
        await RechazarOperacionAsync(() => app.Usuarios.ActualizarAsync(new UsuarioEntity("Nuevo", "Usuario", "TEST-NUEVO", "nuevo-prueba", rolRecepcion)), "Actualizar usuario exige un ID persistido");
        Comprobar((await app.Roles.MostrarRolesActivosAsync()).All(x => x.Estado), "El combo de roles recibe únicamente roles activos");
        var tipo = new TipoEquipoEntity("Equipo de prueba");
        await RechazarOperacionAsync(() => app.TiposEquipo.ActualizarAsync(tipo), "Actualizar tipo exige un ID persistido");
        await app.TiposEquipo.AgregarAsync(tipo);
        tipo.CambiarDescripcion("Tipo editado");
        await app.TiposEquipo.ActualizarAsync(tipo);
        Comprobar((await app.TiposEquipo.ObtenerPorIdAsync(tipo.IdTipoEquipo))?.Descripcion == "Tipo editado", "Actualizar tipo conserva su ID");
        await RechazarOperacionAsync(() => app.TiposEquipo.AgregarAsync(tipo), "Agregar tipo exige un registro nuevo");
        var repuesto = new RepuestoEntity("Repuesto de prueba", null, null, "TEST-R", 10, 15);
        await RechazarOperacionAsync(() => app.Repuestos.ActualizarAsync(repuesto), "Actualizar repuesto exige un ID persistido");
        await app.Repuestos.AgregarAsync(repuesto);
        await app.Repuestos.AjustarExistenciasAsync(repuesto.IdRepuesto, 3);
        Comprobar((await app.Repuestos.ObtenerPorIdAsync(repuesto.IdRepuesto))?.Stock == 3, "Ajuste de stock mediante procedimiento");
        repuesto.CambiarPrecios(11, 16);
        repuesto.AsignarStockConsultado(200);
        await app.Repuestos.ActualizarAsync(repuesto);
        var repuestoEditado = await app.Repuestos.ObtenerPorIdAsync(repuesto.IdRepuesto);
        Comprobar(repuestoEditado?.PrecioVenta == 16 && repuestoEditado.Stock == 3, "Actualizar repuesto conserva las existencias");
        await RechazarOperacionAsync(() => app.Repuestos.AgregarAsync(repuesto), "Agregar repuesto exige un registro nuevo");
        var servicio = new ServicioEntity("Servicio de prueba", null, 20);
        await RechazarOperacionAsync(() => app.Servicios.ActualizarAsync(servicio), "Actualizar servicio exige un ID persistido");
        await app.Servicios.AgregarAsync(servicio);
        servicio.CambiarDescripcion("Servicio editado");
        await app.Servicios.ActualizarAsync(servicio);
        Comprobar((await app.Servicios.ObtenerPorIdAsync(servicio.IdServicio))?.Descripcion == "Servicio editado", "Actualizar servicio conserva su ID");
        await RechazarOperacionAsync(() => app.Servicios.AgregarAsync(servicio), "Agregar servicio exige un registro nuevo");
        var cliente = new ClienteEntity("Cliente", "Prueba", "88888888", "TEST-CLIENTE");
        await RechazarOperacionAsync(() => app.Clientes.ActualizarAsync(cliente), "Actualizar cliente exige un ID persistido");
        await app.Clientes.AgregarAsync(cliente);
        cliente.CambiarTelefono("88888889");
        await app.Clientes.ActualizarAsync(cliente);
        Comprobar((await app.Clientes.ObtenerPorIdAsync(cliente.IdCliente))?.Telefono == "88888889", "Actualizar cliente conserva su ID");
        await RechazarOperacionAsync(() => app.Clientes.AgregarAsync(cliente), "Agregar cliente exige un registro nuevo");
        var equipo = new EquipoEntity(cliente.IdCliente, tipo.IdTipoEquipo, "Marca de prueba");
        await RechazarOperacionAsync(() => app.Equipos.ActualizarAsync(equipo), "Actualizar equipo exige un ID persistido");
        await app.Equipos.AgregarAsync(equipo);
        equipo.CambiarModelo("Modelo editado");
        await app.Equipos.ActualizarAsync(equipo);
        Comprobar((await app.Equipos.ObtenerPorIdAsync(equipo.IdEquipo))?.Modelo == "Modelo editado", "Actualizar equipo conserva sus relaciones");
        await RechazarOperacionAsync(() => app.Equipos.AgregarAsync(equipo), "Agregar equipo exige un registro nuevo");
        await app.Usuarios.AutenticarAsync("recep-prueba", Clave);
        var estado = (await app.Ordenes.ConsultarEstadosAsync()).Single(x => x.Nombre == "En Revisión");
        var orden = new OrdenReparacionEntity("TEST-001", equipo.IdEquipo, app.Sesion.UsuarioActual!.IdUsuario, estado, "No enciende");
        await app.Ordenes.RegistrarEntidadAsync(orden);
        Comprobar((await app.Ordenes.ConsultarExpedienteAsync(orden.IdOrden))?.Orden.IdOrden == orden.IdOrden, "Expediente y múltiples resultados");
        await app.Usuarios.AutenticarAsync("tecnico-prueba", Clave);
        await app.Ordenes.TomarAsync(orden.IdOrden);
        int diagnostico = await app.Diagnosticos.RegistrarAsync(new DiagnosticoSolicitud(orden.IdOrden, "Fuente dañada", "Sustituir fuente", 20, null));
        await app.Usuarios.AutenticarAsync("recep-prueba", Clave);
        await app.Diagnosticos.RegistrarDecisionAsync(new DecisionClienteSolicitud(diagnostico, DecisionCliente.Aprobada, 20, null));
        await app.Usuarios.AutenticarAsync("tecnico-prueba", Clave);
        await app.Ordenes.IniciarReparacionAsync(orden.IdOrden);
        await app.Servicios.RegistrarEnOrdenAsync(new ServicioOrdenSolicitud(orden.IdOrden, servicio.IdServicio, 1, 20, null));
        await app.Ordenes.FinalizarReparacionAsync(orden.IdOrden, true, "Reparación completa");
        await app.Usuarios.AutenticarAsync("recep-prueba", Clave);
        var pago = new PagoEntity(orden.IdOrden, app.Sesion.UsuarioActual!.IdUsuario, 20, MetodoPago.Efectivo);
        await app.Pagos.RegistrarEntidadAsync(pago);
        Comprobar((await app.Pagos.ObtenerPorIdAsync(pago.IdPago))?.Monto == 20, "Pago y lectura por ID");
        int pagoCorregido = await app.Pagos.CorregirEntidadAsync(pago, 20, MetodoPago.Transferencia, "Cambiar método");
        Comprobar((await app.Pagos.ObtenerPorIdAsync(pago.IdPago))?.Anulado == true, "Corrección conserva el pago original anulado");
        Comprobar((await app.Pagos.MostrarAsync(metodo: MetodoPago.Transferencia, anulado: false)).TotalRegistros == 1,
            "Filtros de pagos antes de paginar");
        Comprobar((await app.Pagos.ObtenerPorIdAsync(pagoCorregido))?.Monto == 20, "ID OUTPUT del pago corregido");
        await app.Ordenes.EntregarAsync(orden.IdOrden, null);
        await app.Usuarios.AutenticarAsync("admin-prueba", Clave);
        bool rechazado = false;
        try { await app.Clientes.GuardarEntidadAsync(new ClienteEntity("Otro", "Cliente", "77777777", "TEST-CLIENTE")); }
        catch (DatosException ex) { rechazado = ex.InnerException is SqlException; }
        Comprobar(rechazado, "Error SQL traducido conservando la causa");
        Comprobar((await app.Errores.ConsultarPaginaAsync()).TotalRegistros > 0, "Auditoría del error SQL");
        Console.WriteLine("Flujo de negocio y filtros verificados en SQL Server.");
    }

    private static async Task RechazarOperacionAsync(Func<Task> operacion, string mensaje)
    {
        bool rechazada = false;
        try { await operacion(); }
        catch (ValidacionException) { rechazada = true; }
        Comprobar(rechazada, mensaje);
    }

    private static void Comprobar(bool condicion, string mensaje)
    {
        if (!condicion) throw new Exception(mensaje);
        _comprobaciones++;
    }

    private static async Task EliminarTemporalAsync()
    {
        // La única base eliminable es la que esta ejecución creó con GUID.
        if (!Regex.IsMatch(_baseTemporal, @"^AlDia_Verificacion_[0-9a-f]{32}$")) throw new Exception("Nombre temporal inválido.");
        SqlConnection.ClearAllPools();
        using var cn = new SqlConnection(_cadenaMaster);
        await cn.OpenAsync();
        using var cmd = new SqlCommand($"ALTER DATABASE [{_baseTemporal}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [{_baseTemporal}];", cn);
        await cmd.ExecuteNonQueryAsync();
        Console.WriteLine("Base temporal eliminada.");
    }

    private sealed class ComprobadorUi : Form
    {
        private readonly AplicacionBll _app;
        private readonly string _salida;
        public Exception? Error { get; private set; }
        public ComprobadorUi(AplicacionBll app, string raiz)
        {
            _app = app;
            _salida = Path.Combine(raiz, "artifacts/verificacion");
            Opacity = 0;
            ShowInTaskbar = false;
            Size = new Size(1220, 880);
        }
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            try
            {
                Directory.CreateDirectory(_salida);
                using var roles = new FrmRoles(_app);
                Alojar(roles);
                await roles.RecargarAsync();
                var grid = Buscar<DataGridView>(roles, "dgvListado");
                Comprobar(grid.Rows.Count > 0, "El hijo carga los datos desde el método virtual del base");
                Comprobar(grid.Columns["colNombre"]!.DataPropertyName == "Nombre", "Columnas conservadas del diseñador");
                Comprobar(!string.IsNullOrWhiteSpace(grid.Rows[0].Cells["colNombre"].Value?.ToString()), "Los nombres aparecen en las celdas de la grilla");
                Comprobar(grid.DisplayedRowCount(false) > 0, "El área de la grilla permite ver las filas completas");
                // El render de prueba incluye los controles visibles; el panel de estado vacío está oculto.
                var vacio = Buscar<Panel>(roles, "panelVacio");
                var padreVacio = vacio.Parent!;
                int indiceVacio = padreVacio.Controls.GetChildIndex(vacio);
                padreVacio.Controls.Remove(vacio);
                roles.Refresh();
                using (var imagen = new Bitmap(roles.Width, roles.Height))
                {
                    roles.DrawToBitmap(imagen, new Rectangle(Point.Empty, roles.Size));
                    imagen.Save(Path.Combine(_salida, "roles.png"));
                }
                padreVacio.Controls.Add(vacio);
                padreVacio.Controls.SetChildIndex(vacio, indiceVacio);
                vacio.Visible = false;
                using var usuarios = new FrmUsuarios(_app);
                Alojar(usuarios);
                await usuarios.RecargarAsync();
                Comprobar(Buscar<DataGridView>(usuarios, "dgvListado").Rows.Count == 3, "Listado de usuarios mediante BLL");
                await VerificarRegistroAsync();
                await VerificarOtrosListadosAsync();
                await VerificarPaginacionAsync();
                await VerificarBusquedaConcurrenteAsync();
                Console.WriteLine("Listados y herencia UI verificados; captura generada.");
            }
            catch (Exception ex) { Error = ex; }
            finally { Close(); }
        }
        private static T Buscar<T>(Control control, string nombre) where T : Control =>
            (T)(control.Controls.Find(nombre, true).Single());

        private void Alojar(FrmListadoBase formulario)
        {
            formulario.TopLevel = false;
            formulario.Dock = DockStyle.Fill;
            Controls.Add(formulario);
            formulario.Show();
            formulario.BringToFront();
        }

        private async Task VerificarRegistroAsync()
        {
            using (var registro = new FrmRegistroRol(_app) { Opacity = 0, ShowInTaskbar = false })
            {
                registro.Show(this);
                Buscar<TextBox>(registro, "txtNombre").Text = "Rol desde formulario";
                await GuardarYCerrarAsync(registro);
            }
            var nuevo = (await _app.Roles.MostrarAsync(buscar: "Rol desde formulario")).Registros.Single();
            Comprobar(nuevo.IdRol > 0, "El botón Guardar heredado registra una sola vez");
            using (var registro = new FrmRegistroRol(_app, nuevo.IdRol) { Opacity = 0, ShowInTaskbar = false })
            {
                registro.Show(this);
                await EsperarAsync(() => Buscar<Button>(registro, "btnGuardar").Enabled);
                Comprobar(Buscar<TextBox>(registro, "txtNombre").Text == nuevo.Nombre, "La carga heredada recupera la entidad existente");
                Buscar<TextBox>(registro, "txtDescripcion").Text = "Editado desde el formulario";
                await GuardarYCerrarAsync(registro);
            }
            Comprobar((await _app.Roles.ObtenerPorIdAsync(nuevo.IdRol))?.Descripcion == "Editado desde el formulario", "Edición UI conservando ID");
            using (var registro = new FrmRegistroRol(_app) { Opacity = 0, ShowInTaskbar = false })
            {
                registro.Show(this);
                Buscar<TextBox>(registro, "txtNombre").Text = "Registro cancelado";
                Buscar<Button>(registro, "btnCancelar").PerformClick();
                Comprobar(registro.DialogResult == DialogResult.Cancel, "Cancelar usa el comportamiento del base");
            }
            Comprobar((await _app.Roles.MostrarAsync(buscar: "Registro cancelado")).TotalRegistros == 0, "Cancelar no guarda en SQL");
        }

        private static async Task GuardarYCerrarAsync(Form formulario)
        {
            var cerrado = new TaskCompletionSource();
            formulario.FormClosed += (_, _) => cerrado.TrySetResult();
            Buscar<Button>(formulario, "btnGuardar").PerformClick();
            await cerrado.Task.WaitAsync(TimeSpan.FromSeconds(10));
            Comprobar(formulario.DialogResult == DialogResult.OK, "Guardar devuelve OK al listado");
        }

        private async Task VerificarOtrosListadosAsync()
        {
            using var clientes = new FrmClientes(_app);
            using var equipos = new FrmEquipos(_app);
            using var repuestos = new FrmInventario(_app);
            using var catalogos = new FrmCatalogos(_app);
            using var ordenes = new FrmOrdenesServicio(_app);
            using var pagos = new FrmPagos(_app);
            foreach (var formulario in new FrmListadoBase[] { clientes, equipos, repuestos, catalogos, ordenes, pagos })
            {
                Alojar(formulario);
                await formulario.RecargarAsync();
                var grid = Buscar<DataGridView>(formulario, "dgvListado");
                Comprobar(grid.Rows.Count > 0, formulario.Name + " carga mediante herencia");
                Comprobar(grid.Columns.Cast<DataGridViewColumn>().All(x => !string.IsNullOrWhiteSpace(x.DataPropertyName)), formulario.Name + " vincula las columnas del diseñador");
            }
        }

        private async Task VerificarPaginacionAsync()
        {
            for (int i = 0; i < 12; i++) await _app.Roles.GuardarEntidadAsync(new RolEntity($"Página prueba {i}"));
            using var roles = new FrmRoles(_app);
            Alojar(roles);
            await roles.RecargarAsync();
            Comprobar(Buscar<Button>(roles, "btnSiguiente").Enabled, "Paginación habilitada por el total SQL");
            Buscar<Button>(roles, "btnSiguiente").PerformClick();
            await EsperarAsync(() => Buscar<Label>(roles, "lblPagina").Text.StartsWith("Página 2"));
            Buscar<TextBox>(roles, "txtBuscar").Text = "rol de prueba";
            await EsperarAsync(() => Buscar<Label>(roles, "lblPagina").Text == "Página 1 de 1");
            Comprobar(Buscar<DataGridView>(roles, "dgvListado").Rows.Count == 1, "Escribir busca automáticamente y reinicia la paginación");
            Buscar<TextBox>(roles, "txtBuscar").Clear();
            await EsperarAsync(() => Buscar<Button>(roles, "btnSiguiente").Enabled);
            Comprobar(Buscar<DataGridView>(roles, "dgvListado").Rows.Count == 10, "Borrar la búsqueda recupera el listado completo");
        }

        private async Task VerificarBusquedaConcurrenteAsync()
        {
            using var formulario = new ConsultaPrueba();
            formulario.ConfigurarServicios(_app);
            formulario.CreateControl();
            formulario.EstablecerBusqueda("anterior");
            Task anterior = formulario.RecargarAsync();
            formulario.EstablecerBusqueda("actual");
            await formulario.RecargarAsync();
            formulario.LiberarAnterior();
            await anterior;
            Comprobar(formulario.Resultado == "actual", "Una búsqueda lenta no reemplaza la búsqueda más reciente");
        }

        private static async Task EsperarAsync(Func<bool> condicion)
        {
            var limite = DateTime.UtcNow.AddSeconds(10);
            while (!condicion())
            {
                if (DateTime.UtcNow > limite) throw new TimeoutException("La operación UI no terminó.");
                await Task.Delay(20);
            }
        }
    }

    private sealed class ConsultaPrueba : FrmListadoBase
    {
        private readonly TaskCompletionSource _anterior = new();
        public void EstablecerBusqueda(string texto) => txtBuscar.Text = texto;
        public string? Resultado => (dgvListado.DataSource as List<Fila>)?.SingleOrDefault()?.Nombre;
        public void LiberarAnterior() => _anterior.TrySetResult();
        protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
        {
            var ct = TokenCarga;
            if (buscar == "anterior") await _anterior.Task;
            MostrarPagina(new PaginacionEntity<Fila>([new Fila(buscar ?? "")], 1, 1, tamPagina), ct);
        }
        private sealed record Fila(string Nombre);
    }
}
