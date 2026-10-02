using AlDia.Entity.Base;
using AlDia.Entity.Clientes;
using AlDia.Entity.Common;
using AlDia.Entity.Equipos;
using AlDia.Entity.Seguridad;
using AlDia.Entity.Reparaciones;
using AlDia.Entity.Inventario;
using AlDia.Entity.Pagos;
using AlDia.Entity.Entity;
using System.Text.Json;
using System.Text.RegularExpressions;

int comprobaciones = 0;

void Igual<T>(T esperado, T actual)
{
    comprobaciones++;
    if (!EqualityComparer<T>.Default.Equals(esperado, actual))
        throw new Exception($"Se esperaba '{esperado}' y se obtuvo '{actual}'.");
}

void Rechaza(Action accion)
{
    comprobaciones++;
    try { accion(); }
    catch (ValidacionException) { return; }
    throw new Exception("La operacion debia rechazar los datos invalidos.");
}

void Escenario(string nombre, Action accion)
{
    accion();
    Console.WriteLine($"Correcto: {nombre}");
}

Escenario("Rol nuevo, reconstruccion, herencia y baja logica", () =>
{
    var rol = new RolEntity("  Tecnico  ", "  Reparacion de equipos  ");
    Igual(0, rol.IdRol);
    Igual("Tecnico", rol.Nombre);
    Igual("Reparacion de equipos", rol.Descripcion);
    Igual(true, rol.Estado);
    rol.AsignarId(18);
    rol.AsignarId(18);
    Rechaza(() => rol.AsignarId(19));
    Igual(18, rol.IdRol);

    CatalogoEntity catalogo = rol;
    catalogo.CambiarEstado(false);
    Igual("Inactivo", rol.EstadoNombre);
    var solicitud = rol.CrearSolicitud();
    var recuperado = new RolEntity(solicitud.IdRol, solicitud.Nombre, solicitud.Descripcion, solicitud.Estado);
    Igual(solicitud, recuperado.CrearSolicitud());
    recuperado.Activar();
    Igual("Activo", recuperado.EstadoNombre);

    // RolEntity representa el catalogo, incluso si aun no hay permisos para ese nombre.
    Igual("Encargado", new RolEntity("Encargado").Nombre);
});

Escenario("Limites SQL del rol y cambios fallidos conservan sus datos", () =>
{
    var rol = new RolEntity(new string('N', 50), new string('D', 200));
    Rechaza(() => rol.CambiarNombre(new string('N', 51)));
    Rechaza(() => rol.CambiarDescripcion(new string('D', 201)));
    Rechaza(() => rol.CambiarNombre("  "));
    Rechaza(() => rol.AsignarId(0));
    Rechaza(() => new RolEntity(-1, "Tecnico", null, true));
    Igual(50, rol.Nombre.Length);
    Igual(200, rol.Descripcion!.Length);
    rol.CambiarDescripcion(" ");
    Igual<string?>(null, rol.Descripcion);
});

Escenario("Equipo nuevo y reconstruido conservan el contrato de DAL", () =>
{
    var equipo = new EquipoEntity(4, 8, "  Lenovo  ", "  ThinkPad  ", "  ABC123  ", "  Negro  ", "  Sin cargador  ");
    Igual(0, equipo.IdEquipo);
    Igual("Lenovo", equipo.Marca);
    Igual("ThinkPad", equipo.Modelo);
    equipo.AsignarId(25);
    equipo.CambiarCliente(5);
    equipo.CambiarTipoEquipo(9);
    equipo.CambiarEstado(false);
    var solicitud = equipo.CrearSolicitud();
    var recuperado = new EquipoEntity(solicitud.IdEquipo, solicitud.IdCliente, solicitud.IdTipoEquipo,
        solicitud.Marca, solicitud.Modelo, solicitud.NumeroSerie, solicitud.Color, solicitud.Observaciones,
        solicitud.Estado, equipo.FechaRegistro);
    Igual(solicitud, recuperado.CrearSolicitud());
    Igual(equipo.FechaRegistro, recuperado.FechaRegistro);
    Igual("Inactivo", recuperado.EstadoNombre);
    recuperado.Activar();
    Igual(true, recuperado.Estado);
    Rechaza(() => recuperado.AsignarId(26));
    Igual(25, recuperado.IdEquipo);
});

Escenario("Equipo rechaza relaciones y longitudes invalidas", () =>
{
    Rechaza(() => new EquipoEntity(0, 1, "Dell"));
    Rechaza(() => new EquipoEntity(1, 0, "Dell"));
    Rechaza(() => new EquipoEntity(1, 1, " "));
    var equipo = new EquipoEntity(1, 2, new string('M', 50), new string('X', 100),
        new string('S', 100), new string('C', 50), new string('O', 500));
    var antes = equipo.CrearSolicitud();
    Rechaza(() => equipo.CambiarCliente(-1));
    Rechaza(() => equipo.CambiarTipoEquipo(0));
    Rechaza(() => equipo.CambiarMarca(new string('M', 51)));
    Rechaza(() => equipo.CambiarModelo(new string('M', 101)));
    Rechaza(() => equipo.CambiarNumeroSerie(new string('S', 101)));
    Rechaza(() => equipo.CambiarColor(new string('C', 51)));
    Rechaza(() => equipo.CambiarObservaciones(new string('O', 501)));
    Igual(antes, equipo.CrearSolicitud());
    equipo.CambiarModelo(" ");
    Igual<string?>(null, equipo.Modelo);
});

Escenario("Usuario conserva IdRol en copias sin adivinar los IDs de SQL", () =>
{
    var fecha = new DateTime(2026, 10, 1);
    var usuario = new UsuarioEntity(7, "Ana", "Lopez", "ABC123", null, null, "ana",
        RolUsuario.Tecnico, true, fecha, 43);
    var copia = usuario.Copiar();
    Igual<int?>(43, copia.IdRol);
    copia.CambiarNombre("Elena");
    copia.CambiarRol(87, RolUsuario.Recepcionista);
    Igual("Ana", usuario.Nombre);
    Igual<int?>(43, usuario.IdRol);
    Igual(RolUsuario.Tecnico, usuario.Rol);
    Igual<int?>(87, copia.IdRol);
    Rechaza(() => copia.CambiarRol(90, (RolUsuario)999));
    Rechaza(() => copia.CambiarRol(0, RolUsuario.Administrador));
    Igual<int?>(87, copia.IdRol);
    Igual(RolUsuario.Recepcionista, copia.Rol);
    copia.CambiarRol(RolUsuario.Recepcionista);
    Igual<int?>(87, copia.IdRol);
    copia.CambiarRol(RolUsuario.Administrador);
    Igual<int?>(null, copia.IdRol);
    var firmaAnterior = new UsuarioEntity(7, "Ana", "Lopez", "ABC123", null, null, "ana",
        RolUsuario.Tecnico, true, fecha);
    Igual<int?>(null, firmaAnterior.IdRol);
    Igual(usuario.CrearSolicitud(), firmaAnterior.CrearSolicitud());
    Rechaza(() => new UsuarioEntity(7, "Ana", "Lopez", "ABC123", null, null, "ana",
        RolUsuario.Tecnico, true, fecha, 0));
});

Escenario("Polimorfismo mantiene obligatorio el telefono del cliente", () =>
{
    PersonaEntity cliente = new ClienteEntity("Ana", "Lopez", "88888888", "ABC123");
    Rechaza(() => cliente.CambiarTelefono(null));
    Igual("88888888", cliente.Telefono);
    cliente.CambiarEstado(false);
    Igual("Inactivo", cliente.EstadoNombre);
    PersonaEntity usuario = new UsuarioEntity("Ana", "Lopez", "ABC123", "ana", RolUsuario.Tecnico);
    usuario.CambiarTelefono(null);
    Igual<string?>(null, usuario.Telefono);
});

Escenario("Paginacion vacia, ultima pagina, limites e independencia de la lista", () =>
{
    var vacia = new PaginacionEntity<int>([], 0, 1, 10);
    Igual(0, vacia.TotalPaginas);
    Igual(false, vacia.TienePaginaAnterior);
    Igual(false, vacia.TienePaginaSiguiente);
    var origen = new List<int> { 21, 22, 23 };
    var ultima = new PaginacionEntity<int>(origen, 23, 3, 10);
    origen.Clear();
    Igual(3, ultima.Registros.Count);
    Igual(3, ultima.TotalPaginas);
    Igual(true, ultima.TienePaginaAnterior);
    Igual(false, ultima.TienePaginaSiguiente);
    Igual(214748365, new PaginacionEntity<int>([], int.MaxValue, 1, 10).TotalPaginas);
    Rechaza(() => new PaginacionEntity<int>([], -1, 1, 10));
    Rechaza(() => new PaginacionEntity<int>([], 0, 0, 10));
    Rechaza(() => new PaginacionEntity<int>([], 0, 1, 0));
    Rechaza(() => new PaginacionEntity<int>([1], 0, 1, 10));
    Rechaza(() => new PaginacionEntity<int>([1, 2], 2, 1, 1));
});

Escenario("Estados y ordenes: alta, reconstruccion, recepcion y saldo", () =>
{
    var estado = new EstadoReparacionEntity("En Revisión", "Disponible");
    Rechaza(() => new OrdenReparacionEntity("ORD-001", 1, 2, estado, "No enciende"));
    estado.AsignarId(91);
    var orden = new OrdenReparacionEntity("ORD-001", 1, 2, estado, "No enciende");
    Igual(91, orden.IdEstado);
    Igual(0, orden.IdOrden);
    Igual(0m, orden.Saldo);
    orden.AsignarId(31);
    Rechaza(() => orden.AsignarId(32));
    orden.CambiarRecepcion(" Pantalla rota ", " Sin golpes ", " Cargador ");
    Igual("Pantalla rota", orden.CrearSolicitud().ProblemaReportado);
    Igual(31, orden.CrearCorreccion().IdOrden);
    var anterior = orden.CrearSolicitud();
    Rechaza(() => orden.CambiarRecepcion("Otro problema", new string('O', 501)));
    Igual(anterior, orden.CrearSolicitud());
    var fecha = new DateTime(2026, 10, 1);
    var recuperada = new OrdenReparacionEntity(31, "ORD-001", 1, 2, 91, "Reparada", 5,
        fecha, "No enciende", null, null, null, false, 100m, 40m, 60m, 6, "Ana", "Lopez");
    Igual(60m, recuperada.Saldo);
    Igual("Ana Lopez", recuperada.NombreCompletoCliente);
    Rechaza(() => recuperada.CambiarRecepcion("Otro problema"));
    Rechaza(() => new OrdenReparacionEntity(31, "ORD-001", 1, 2, 91, "Reparada", 5,
        fecha, "No enciende", null, null, null, false, 100m, 40m, 61m, 6, "Ana", "Lopez"));
    Rechaza(() => new OrdenReparacionEntity(31, "ORD-001", 1, 2, 91, "Reparada", 5,
        fecha, "No enciende", null, null, null, false, 100m, 101m, -1m, 6, "Ana", "Lopez"));
    Rechaza(() => new OrdenReparacionEntity("ORD-002", 1, 2, new EstadoReparacionEntity(92, "Reparada", null), "Falla"));
    Rechaza(() => estado.CambiarNombre(new string('N', 51)));
    Rechaza(() => estado.CambiarDescripcion(new string('D', 201)));
    Rechaza(() => estado.AsignarId(92));
});

Escenario("Diagnostico: presupuesto y correcciones atomicas", () =>
{
    var diagnostico = new DiagnosticoEntity(31, 5, "No enciende", "Reemplazar fuente", 200m);
    Rechaza(() => diagnostico.CrearCorreccion());
    diagnostico.AsignarId(61);
    var anterior = diagnostico.CrearSolicitud();
    Rechaza(() => diagnostico.CambiarDiagnostico("Pantalla rota", "Reemplazar pantalla", -1m));
    Rechaza(() => diagnostico.CambiarDiagnostico("Pantalla rota", "Reemplazar pantalla", 1.001m));
    Rechaza(() => diagnostico.CambiarDiagnostico("Pantalla rota", "Reemplazar pantalla", 100m, new string('O', 1001)));
    Igual(anterior, diagnostico.CrearSolicitud());
    diagnostico.CambiarDiagnostico(" Fuente dañada ", " Cambiar fuente ", 0m);
    Igual(0m, diagnostico.CostoEstimado);
    Igual("Fuente dañada", diagnostico.ProblemaEncontrado);
    Igual(61, diagnostico.CrearCorreccion().IdDiagnostico);
    Rechaza(() => diagnostico.AsignarId(62));
    Rechaza(() => new DiagnosticoEntity(0, 5, "Falla", "Reparar", 0m));
    Rechaza(() => new DiagnosticoEntity(31, 0, "Falla", "Reparar", 0m));
    Rechaza(() => new DiagnosticoEntity(31, 5, new string('P', 1001), "Reparar", 0m));
    Rechaza(() => new DiagnosticoEntity(31, 5, "Falla", new string('R', 1001), 0m));
});

Escenario("Confirmacion: aprobacion y rechazo conservan la regla del costo", () =>
{
    Rechaza(() => new ConfirmacionEntity(61, 2, DecisionCliente.Aprobada, null));
    Rechaza(() => new ConfirmacionEntity(61, 2, DecisionCliente.Rechazada, 0m));
    Rechaza(() => new ConfirmacionEntity(61, 2, (DecisionCliente)99, null));
    var confirmacion = new ConfirmacionEntity(61, 2, DecisionCliente.Aprobada, 0m);
    confirmacion.AsignarId(71);
    Igual(0m, confirmacion.CostoAprobado);
    var anterior = confirmacion.CrearSolicitud();
    Rechaza(() => confirmacion.CambiarDecision(DecisionCliente.Rechazada, 20m));
    Rechaza(() => confirmacion.CambiarDecision(DecisionCliente.Aprobada, -1m));
    Rechaza(() => confirmacion.CambiarDecision(DecisionCliente.Rechazada, null, new string('O', 501)));
    Igual(anterior, confirmacion.CrearSolicitud());
    confirmacion.CambiarDecision(DecisionCliente.Rechazada, null);
    Igual<decimal?>(null, confirmacion.CostoAprobado);
    Igual(DecisionCliente.Rechazada, confirmacion.Decision);
    Igual(71, confirmacion.CrearCorreccion().IdConfirmacion);
    Rechaza(() => confirmacion.AsignarId(72));
});

Escenario("Detalles normalizados: herencia, subtotales y compatibilidad del expediente", () =>
{
    var servicio = new DetalleServicioEntity(31, 10, 2, 40m, "Limpieza");
    var repuesto = new DetalleRepuestoEntity(31, 11, 3, 15m);
    Igual<int?>(null, servicio.IdTecnico);
    Igual<int?>(null, repuesto.IdTecnicoResponsable);
    servicio.AsignarId(81);
    repuesto.AsignarId(82);
    DetalleOrdenEntity detalle = servicio;
    Igual(80m, detalle.Subtotal);
    detalle.CambiarImportes(3, 40m);
    Igual(120m, servicio.Subtotal);
    Rechaza(() => detalle.CambiarImportes(9, -1m));
    Igual(3, servicio.Cantidad);
    Igual(40m, servicio.Precio);
    Rechaza(() => detalle.CambiarImportes(0, 0m));
    Igual(81, servicio.CrearCorreccion().IdDetalleServicio);
    Igual(82, repuesto.CrearCorreccion().IdDetalleRepuesto);
    Igual(45m, repuesto.Subtotal);
    Igual(31, servicio.CrearSolicitud().IdOrden);
    Igual(11, repuesto.CrearSolicitud().IdRepuesto);
    var servicioDal = new DetalleServicioEntity(81, 31, 10, 5, 2, 40m, null);
    var repuestoDal = new DetalleRepuestoEntity(82, 31, 11, 5, 3, 15m);
    Igual<int?>(5, servicioDal.IdTecnico);
    Igual<int?>(5, repuestoDal.IdTecnicoResponsable);
    Igual(2, servicioDal.Cantidad);
    Igual(3, repuestoDal.Cantidad);
    Rechaza(() => servicio.CambiarObservaciones(new string('O', 501)));
    Rechaza(() => new DetalleRepuestoEntity(31, 0, 1, 1m));
    Rechaza(() => new DetalleServicioEntity(0, 1, 1, 1m));
    Rechaza(() => new DetalleRepuestoEntity(31, 11, 1, 1.001m));
    Rechaza(() => servicio.AsignarId(83));
});

Escenario("Pagos: correccion y anulacion preparan solicitudes sin modificar la auditoria", () =>
{
    var pago = new PagoEntity(31, 2, 100m, MetodoPago.Efectivo);
    Rechaza(() => pago.CrearCorreccion(90m, MetodoPago.Tarjeta, "Error de captura"));
    pago.AsignarId(101);
    var correccion = pago.CrearCorreccion(90m, MetodoPago.Tarjeta, " Error de captura ");
    Igual(90m, correccion.Monto);
    Igual("Error de captura", correccion.Motivo);
    Igual(100m, pago.Monto);
    Igual(MetodoPago.Efectivo, pago.MetodoPago);
    Igual(false, pago.Anulado);
    Igual(101, pago.CrearAnulacion("Pago duplicado").IdPago);
    Rechaza(() => pago.CrearAnulacion(" "));
    Rechaza(() => pago.CrearCorreccion(0m, MetodoPago.Efectivo, "Error"));
    Rechaza(() => pago.CrearCorreccion(1m, (MetodoPago)99, "Error"));
    Rechaza(() => new PagoEntity(31, 2, 0m, MetodoPago.Efectivo));
    Rechaza(() => new PagoEntity(31, 2, 1.001m, MetodoPago.Efectivo));
    var fecha = new DateTime(2026, 10, 1);
    var anulado = new PagoEntity(101, 31, 2, 100m, MetodoPago.Efectivo, fecha, null, true, fecha, 2, "Duplicado");
    Igual(true, anulado.Anulado);
    Rechaza(() => anulado.CrearCorreccion(10m, MetodoPago.Efectivo, "Error"));
    Rechaza(() => anulado.CrearAnulacion("Otro motivo"));
    Rechaza(() => new PagoEntity(101, 31, 2, 100m, MetodoPago.Efectivo, fecha, null, true, null, 2, "Duplicado"));
    Rechaza(() => new PagoEntity(101, 31, 2, 100m, MetodoPago.Efectivo, fecha, null, false, fecha, 2, "Duplicado"));
});

Escenario("Historial, errores y expediente preservan relaciones y colecciones", () =>
{
    var fecha = new DateTime(2026, 10, 1);
    var historial = new HistorialEstadoEntity(1, 31, null, 91, 2, fecha, "Recepcion");
    Igual<int?>(null, historial.IdEstadoAnterior);
    Rechaza(() => new HistorialEstadoEntity(1, 31, 0, 91, 2, fecha, null));
    var mensaje = "  Error técnico: " + new string('á', 5000) + "  ";
    var log = new LogErrorEntity(1, mensaje, null, null, null, "SinSesion", null);
    Igual(mensaje, log.MensajeError);
    Igual<DateTime?>(null, log.FechaError);
    Rechaza(() => new LogErrorEntity("Error", 51000, new string('P', 101), 10, "Ana"));
    Rechaza(() => new LogErrorEntity("Error", 51000, null, -1, "Ana"));
    Rechaza(() => new LogErrorEntity("Error", 51000, null, 10, new string('U', 26)));
    var orden = new OrdenReparacionEntity(31, "ORD-001", 1, 2, 91, "En Revisión", null,
        fecha, "Falla", null, null, null, false, 0m, 0m, 0m, 6, "Ana", "Lopez");
    var servicios = new List<DetalleServicioEntity> { new(81, 31, 10, 1, 0m, null) };
    var expediente = new ExpedienteOrdenEntity(orden, null, null, servicios, [], [], [historial]);
    servicios.Clear();
    Igual(1, expediente.Servicios.Count);
    Igual(31, expediente.Historial[0].IdOrden);
    Rechaza(() => new ExpedienteOrdenEntity(orden, null, null, [new(81, 32, 10, 1, 0m, null)], [], [], []));
    Rechaza(() => new ExpedienteOrdenEntity(orden, new DiagnosticoEntity(32, 5, "Falla", "Reparar", 0m), null, [], [], [], []));
    Rechaza(() => new ExpedienteOrdenEntity(orden, null, new ConfirmacionEntity(61, 2, DecisionCliente.Rechazada, null), [], [], [], []));
});

Escenario("Usuario con FK real puede representar el catalogo sin conceder permisos nuevos", () =>
{
    var rol = new RolEntity(43, "Tecnico", null, true);
    var usuario = new UsuarioEntity("Ana", "Lopez", "ABC123", "ana", rol);
    rol.CambiarNombre("Encargado");
    Igual("Tecnico", usuario.NombreRol);
    Igual(RolUsuario.Tecnico, usuario.Rol);
    Igual(43, usuario.CrearSolicitudPorRol().IdRol);
    usuario.CambiarRol(new RolEntity(88, "Encargado", null, true));
    Igual<RolUsuario?>(null, usuario.PermisoRol);
    Igual("Encargado", usuario.Copiar().NombreRol);
    Igual<int?>(88, usuario.Copiar().IdRol);
    Igual(88, usuario.CrearSolicitudPorRol().IdRol);
    comprobaciones++;
    try { usuario.CrearSolicitud(); throw new Exception("Un nombre nuevo no debe obtener permisos del enum."); }
    catch (PermisoException) { }
    Rechaza(() => usuario.CambiarRol(new RolEntity(43, "Tecnico", null, false)));
    Igual("Encargado", usuario.NombreRol);
    Igual<int?>(88, usuario.IdRol);
    Rechaza(() => usuario.CambiarRol(new RolEntity("Tecnico")));
});

Escenario("Conexion encapsulada conserva inicializadores, JSON y la contrasena exacta", () =>
{
    var conexion = new ConexionEntity { Servidor = " LOCALHOST ", BaseDatos = " AlDiaDB ", UsuarioSql = " UsuarioSql ", Password = " Clave de prueba " };
    var json = JsonSerializer.Serialize(conexion);
    var recuperada = JsonSerializer.Deserialize<ConexionEntity>(json)!;
    Igual("LOCALHOST", recuperada.Servidor);
    Igual("AlDiaDB", recuperada.BaseDatos);
    Igual("UsuarioSql", recuperada.UsuarioSql);
    Igual(" Clave de prueba ", recuperada.Password);
    Rechaza(() => recuperada.CambiarDatosConexion("OTRO", "OtraDB", "Usuario", ""));
    Igual("LOCALHOST", recuperada.Servidor);
    Igual("AlDiaDB", recuperada.BaseDatos);
    Igual(" Clave de prueba ", recuperada.Password);
    Rechaza(() => recuperada.CambiarServidor(new string('S', 129)));
    Rechaza(() => recuperada.CambiarBaseDatos(" "));
    Rechaza(() => recuperada.CambiarUsuarioSql(" "));
    recuperada.CambiarPassword(" Otra clave ");
    Igual(" Otra clave ", recuperada.Password);
});

Escenario("Cobertura de las 16 tablas y sus columnas contra el esquema SQL actual", () =>
{
    var tipos = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
    {
        ["Roles"] = typeof(RolEntity), ["Usuarios"] = typeof(UsuarioEntity),
        ["Clientes"] = typeof(ClienteEntity), ["TiposEquipo"] = typeof(TipoEquipoEntity),
        ["Equipos"] = typeof(EquipoEntity), ["EstadosReparacion"] = typeof(EstadoReparacionEntity),
        ["OrdenesReparacion"] = typeof(OrdenReparacionEntity), ["Diagnosticos"] = typeof(DiagnosticoEntity),
        ["ConfirmacionesReparacion"] = typeof(ConfirmacionEntity), ["Servicios"] = typeof(ServicioEntity),
        ["DetalleServicios"] = typeof(DetalleServicioEntity), ["Repuestos"] = typeof(RepuestoEntity),
        ["DetalleRepuestos"] = typeof(DetalleRepuestoEntity), ["Pagos"] = typeof(PagoEntity),
        ["HistorialEstados"] = typeof(HistorialEstadoEntity), ["tblLogErrores"] = typeof(LogErrorEntity)
    };
    DirectoryInfo? carpeta = new(AppContext.BaseDirectory);
    string? rutaEsquema = null;
    while (carpeta is not null)
    {
        var candidata = Path.Combine(carpeta.FullName, "AlDia.DataBase", "AlDia.DataBase", "Base de datos.sql");
        if (File.Exists(candidata)) { rutaEsquema = candidata; break; }
        carpeta = carpeta.Parent;
    }
    if (rutaEsquema is null) throw new Exception("No se encontro el esquema SQL de referencia.");
    // Solo se lee el archivo: nunca se ejecuta el script.
    var esquema = File.ReadAllText(rutaEsquema);
    var tablas = Regex.Matches(esquema, @"CREATE\s+TABLE\s+(?:dbo\.)?(\w+)\s*\((.*?)\);", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    Igual(16, tablas.Count);
    foreach (Match tabla in tablas)
    {
        var tipo = tipos[tabla.Groups[1].Value];
        foreach (Match columna in Regex.Matches(tabla.Groups[2].Value, @"^\s*(\w+)\s+(INT|VARCHAR|NVARCHAR|BIT|DATETIME|DECIMAL)\b", RegexOptions.Multiline | RegexOptions.IgnoreCase))
        {
            string nombre = columna.Groups[1].Value;
            var propiedad = nombre == "ContrasenaHash" && tipo == typeof(UsuarioEntity)
                ? typeof(CredencialUsuario).GetProperty("Hash")
                : tipo.GetProperties().SingleOrDefault(p => p.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            comprobaciones++;
            if (propiedad is null) throw new Exception($"Falta representar {tabla.Groups[1].Value}.{nombre} en Entity.");
            var tipoReal = Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType;
            var tipoSql = columna.Groups[2].Value.ToUpperInvariant();
            var esperado = tipoSql switch
            {
                "INT" => typeof(int), "BIT" => typeof(bool), "DATETIME" => typeof(DateTime),
                "DECIMAL" => typeof(decimal), _ => typeof(string)
            };
            // Decision y MetodoPago tienen enums; el resto conserva el tipo de la columna.
            Igual(true, tipoReal == esperado || (esperado == typeof(string) && tipoReal.IsEnum));
            comprobaciones++;
            if (propiedad.SetMethod?.IsPublic == true)
                throw new Exception($"{tipo.Name}.{propiedad.Name} permite cambios sin encapsulamiento.");
        }
    }
});

Escenario("Clientes y catalogos existentes conservan validaciones y cambios atomicos", () =>
{
    var cliente = new ClienteEntity("Ana", "Lopez", "88888888", "ABC123");
    var anterior = cliente.CrearSolicitud();
    Rechaza(() => cliente.CambiarCedula(new string('C', 21)));
    Rechaza(() => cliente.CambiarNombre(new string('N', 101)));
    Igual(anterior, cliente.CrearSolicitud());
    var tipo = new TipoEquipoEntity(new string('T', 50), new string('D', 200));
    Rechaza(() => tipo.CambiarNombre(new string('T', 51)));
    Rechaza(() => tipo.CambiarDescripcion(new string('D', 201)));
    tipo.AsignarId(9);
    Igual(9, tipo.CrearSolicitud().IdTipoEquipo);
    var servicio = new ServicioEntity("Limpieza", null, 99999999.99m);
    Rechaza(() => servicio.CambiarPrecioBase(100000000m));
    Rechaza(() => servicio.CambiarPrecioBase(1.001m));
    Igual(99999999.99m, servicio.PrecioBase);
    var repuesto = new RepuestoEntity(11, "Fuente", null, null, null, 30m, 40m, true, 5);
    Rechaza(() => repuesto.CambiarPrecios(50m, -1m));
    Igual(30m, repuesto.PrecioCompra);
    Igual(40m, repuesto.PrecioVenta);
    Igual(5, repuesto.Stock);
    Rechaza(() => new RepuestoEntity(11, "Fuente", null, null, null, 30m, 40m, true, -1));
    var usuario = new UsuarioEntity("Ana", "Lopez", "ABC123", "ana", RolUsuario.Tecnico, correo: "ana@example.com");
    Rechaza(() => usuario.CambiarCorreo("correo incorrecto"));
    Igual("ana@example.com", usuario.Correo);
    var credencial = new CredencialUsuario(usuario, "hash-de-prueba");
    Igual("hash-de-prueba", credencial.ContrasenaHash);
    Rechaza(() => new CredencialUsuario(usuario, ""));
    Rechaza(() => new CredencialUsuario(usuario, new string('H', 256)));
});

Console.WriteLine($"{comprobaciones} comprobaciones superadas. No se utilizo SQL Server.");
