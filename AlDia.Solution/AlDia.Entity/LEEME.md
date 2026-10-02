# Capa Entity de todo el sistema Al Día

Esta capa representa los datos y protege los cambios en memoria. No abre conexiones, ejecuta procedimientos ni depende de UI, BLL o DAL. Se trabaja con el patrón del maestro: propiedades encapsuladas, constructores y métodos `Cambiar...`.

## Cobertura de las 16 tablas

| Clase | Tabla | Base y datos específicos |
|---|---|---|
| RolEntity | Roles | CatalogoEntity; IdRol; nombre de 50 y descripción de 200 caracteres |
| UsuarioEntity | Usuarios | PersonaEntity; IdUsuario, IdRol, correo, usuario, rol de permisos y fecha |
| ClienteEntity | Clientes | PersonaEntity; IdCliente; teléfono obligatorio mediante override |
| TipoEquipoEntity | TiposEquipo | CatalogoEntity; IdTipoEquipo |
| EquipoEntity | Equipos | IdEquipo, relaciones con cliente y tipo, características y fecha |
| ServicioEntity | Servicios | CatalogoEntity; IdServicio y precio base |
| RepuestoEntity | Repuestos | CatalogoEntity; IdRepuesto, marca, número de parte, precios y stock consultado |
| EstadoReparacionEntity | EstadosReparacion | IdEstado, nombre y descripción; no tiene baja lógica porque la tabla no contiene Estado |
| OrdenReparacionEntity | OrdenesReparacion | Recepción, relaciones, técnico responsable, estado, fechas y total; incluye proyecciones de cliente y pagos |
| DiagnosticoEntity | Diagnosticos | Orden y técnico; problema, reparación propuesta, costo y observaciones |
| ConfirmacionEntity | ConfirmacionesReparacion | Diagnóstico y usuario; decisión, costo autorizado, fecha y observaciones |
| DetalleServicioEntity | DetalleServicios | DetalleOrdenEntity; IdDetalleServicio, IdServicio y observaciones |
| DetalleRepuestoEntity | DetalleRepuestos | DetalleOrdenEntity; IdDetalleRepuesto e IdRepuesto |
| PagoEntity | Pagos | Orden, usuario, monto, método, fecha y datos de anulación |
| HistorialEstadoEntity | HistorialEstados | Auditoría de la orden: estado anterior y nuevo, usuario, fecha y observación |
| LogErrorEntity | tblLogErrores | Mensaje NVARCHAR(MAX), número, procedimiento, línea, usuario y fecha nullable |

Cada entidad de tabla está en su archivo. Los nombres de las propiedades conservan los contratos existentes y representan todas las columnas. `ContrasenaHash` se mantiene separado en CredencialUsuario para no exponerlo como dato del usuario en los listados.

`PersonaEntity`, `CatalogoEntity` y `DetalleOrdenEntity` son abstractas. Las derivadas reutilizan sus propiedades y comportamiento con herencia. La especialización de `CambiarTelefono` en ClienteEntity aplica polimorfismo incluso cuando el cliente se usa mediante una referencia PersonaEntity. DetalleOrdenEntity reúne cantidad, precio, subtotal y la relación con la orden.

Las entidades que admiten altas empiezan con ID cero; los catálogos, personas y equipos nuevos empiezan activos. Los constructores completos reconstruyen los datos consultados, incluido su estado. `AsignarId` acepta un ID positivo después de guardar y evita reemplazar un identificador existente. Los métodos de cambio validan todos los valores antes de asignar, respetando las longitudes y tipos de SQL. HistorialEstadoEntity representa registros de auditoría existentes y no permite editarlos.

`Estado` conserva el booleano que utilizan SQL y los contratos actuales. `EstadoNombre` devuelve Activo/Inactivo para los futuros listados. `CambiarEstado`, `Activar` y `Desactivar` cambian el objeto en memoria; la BLL y SQL deben autorizar y guardar la operación.

```csharp
var rol = new RolEntity("Tecnico", "Diagnóstico y reparación");
rol.CambiarDescripcion("Reparación de equipos electrónicos");
var datosRol = rol.CrearSolicitud();

var equipo = new EquipoEntity(idCliente: 5, idTipoEquipo: 2, marca: "Lenovo");
equipo.CambiarModelo("ThinkPad");
equipo.CambiarNumeroSerie("ABC123");
var datosEquipo = equipo.CrearSolicitud();
// GuardarEntidadAsync en BLL persiste los datos y asigna el ID devuelto por DAL.
```

`RolSolicitud` y `EquipoSolicitud` son datos de entrada para una operación. No contienen consultas ni permisos. RolEntity permite representar los nombres de la tabla Roles; eso por sí solo no asigna permisos a un rol nuevo.

## Usuario y la relación con Roles

`IdRol` representa la FK de Usuarios y es obligatoria en SQL. Las consultas DAL actuales la cargan junto con el nombre del rol. El objeto admite null para los constructores de compatibilidad que reciben únicamente RolUsuario; significa **identificador no cargado**, sin deducirlo de la posición del enum. El contrato anterior resuelve el nombre en SQL; el contrato con FK utiliza el ID real seleccionado.

El constructor del modelo actual recibe un ID positivo y el nombre real del rol. Para crear un usuario también se puede recibir un RolEntity existente y activo. `CambiarRol(RolEntity)` copia el ID y nombre, sin retener una referencia mutable al catálogo. `CrearSolicitudPorRol` genera UsuarioRolSolicitud con la FK utilizada por DAL/BLL. `Copiar` conserva ese dato. Si se cambia solamente el enum a otro rol, se limpia el ID anterior para que no quede asociado al nombre equivocado.

`NombreRol` permite representar los nombres del catálogo. `PermisoRol` devuelve el enum solo para los tres permisos configurados: Administrador, Recepcionista y Tecnico. Para un nombre nuevo devuelve null y el acceso al adaptador `Rol` rechaza la operación; no se concede un permiso por defecto. Los contratos actuales siguen funcionando para los tres roles existentes. DAL incluye IdRol en las consultas y BLL utiliza el contrato con FK. IRolDal tiene implementación en RolDal y es utilizada por RolBll; sus procedimientos están en el archivo SQL consolidado.

La contraseña no se guarda como propiedad de UsuarioEntity. `CredencialUsuario` transporta exclusivamente el hash entre DAL y BLL para autenticar, como dato de solo lectura y con el límite de 255 caracteres de SQL. `Copiar` continúa aislando los datos del usuario de la sesión.

## Entidades del flujo y sus reglas

- **Orden:** el alta recibe el estado inicial consultado, con su ID real y nombre En Revisión. CambiarRecepcion valida todos los campos y solo permite preparar correcciones cuando la orden está en revisión, no anulada y sin técnico asignado. Saldo se calcula a partir de Total y Pagado; la reconstrucción rechaza resultados inconsistentes. Cliente, pagos y nombre del estado son proyecciones de consulta.
- **Diagnóstico:** valida la relación con orden y técnico, textos de hasta 1000 caracteres y costo no negativo con dos decimales. CambiarDiagnostico prepara una edición atómica. CrearSolicitud y CrearCorreccion generan los datos para las operaciones existentes.
- **Confirmación:** una decisión aprobada exige costo no negativo, incluido cero; una rechazada exige costo null. Una corrección inválida conserva la decisión y el costo anteriores.
- **Detalles:** tienen cantidad positiva, precio no negativo y Subtotal calculado. CambiarImportes valida ambos valores antes de modificarlos. Los constructores nuevos no requieren un técnico porque ese campo fue eliminado de ambas tablas.
- **Pagos:** el monto es positivo y el método corresponde al enum. Un pago anulado exige fecha, usuario y motivo. CrearCorreccion y CrearAnulacion producen solicitudes; no cambian el pago histórico. Esto sigue el procedimiento que anula el anterior y registra uno nuevo.
- **Historial y errores:** exponen los datos de auditoría como solo lectura. LogErrorEntity conserva el mensaje completo y respeta los campos nullable de la tabla.

Los permisos, las relaciones existentes en la base, la disponibilidad de stock, la aprobación previa y las transacciones se comprueban al ejecutar las operaciones en BLL y SQL. La entidad protege sus valores, pero un método de cambio no escribe en la base.

## Solicitudes y resultados de consulta

Las solicitudes de recepción, diagnóstico, decisión, servicios, consumo y pagos conservan records. Representan una operación concreta y se pueden normalizar en BLL con `with` sin cambiar el objeto original.

Las entidades de órdenes, diagnósticos, confirmaciones, detalles, pagos e historial son clases encapsuladas. Sus firmas de reconstrucción conservan las llamadas de DAL. ExpedienteOrdenEntity aplica composición, valida que sus registros pertenezcan a la misma orden y diagnóstico y copia las colecciones para impedir que se altere el conjunto de filas mediante las listas originales. Las entidades editables contenidas conservan sus métodos de cambio.

`DetalleServicioEntity.IdTecnicoResponsable` y `DetalleRepuestoEntity.IdTecnicoResponsable` son datos opcionales de consulta obtenidos de la orden; `IdTecnico` conserva el alias de compatibilidad. Las tablas de detalles no contienen esa columna. Los constructores de reconstrucción admiten el alias que devuelve el procedimiento actual.

`ConexionEntity` conserva su contrato de configuración y serialización JSON para el formulario existente. Sus inicializadores validan los valores, los métodos Cambiar... controlan los cambios y el constructor JSON reconstruye datos completos. CambiarDatosConexion valida todos los campos antes de modificarlos. La contraseña conserva exactamente sus espacios y caracteres. No es una tabla del negocio.

## Resultado común para paginación

`PaginacionEntity<T>` contiene una copia de la colección de registros, TotalRegistros, PaginaActual, TamPagina, TotalPaginas y los indicadores para navegar. TotalPaginas es cero cuando no hay datos; la página solicitada comienza en uno. El cálculo evita desbordamientos con totales grandes. Copiar la colección impide agregar o quitar filas desde la lista original; las entidades contenidas mantienen su propio comportamiento.

Este contrato se utiliza en los listados de DAL/BLL siguiendo el patrón del maestro. SQL devuelve una página y el total del mismo filtro; DAL cierra el lector antes de leer el parámetro OUTPUT. No se recortan listas completas en memoria para simular paginación. Ver [DAL_BLL.md](../DAL_BLL.md).

## Compilación

Desde la raíz del repositorio:

```powershell
dotnet build AlDia.Solution/AlDia.Solution.slnx
```

Entity representa las 16 tablas del esquema y mantiene sus límites y relaciones, la validación de los cambios, la copia del usuario, el polimorfismo, la composición del expediente y la compatibilidad JSON. El contexto del proyecto se encuentra en el [README principal](../../README.md).
