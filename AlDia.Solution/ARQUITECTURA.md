# Capas de Al Día

La implementación está organizada en `Common`, `Seguridad`, `Clientes`, `Equipos`, `Reparaciones`, `Inventario` y `Pagos`. Las clases base del dominio están en `AlDia.Entity/Base`. UI incorpora el login, el registro inicial y el diseño visual del dashboard; los formularios de los demás módulos se implementarán después.

## Responsabilidades y POO

- **Entity:** clases encapsuladas para las 16 tablas del sistema, solicitudes con tipos explícitos, enumeraciones e interfaces de acceso a datos. Las entidades del flujo tienen constructores y métodos con validación; los pagos, el historial y los errores protegen los datos de auditoría. Las solicitudes conservan records. El detalle y la correspondencia con todas las tablas se encuentran en [AlDia.Entity/LEEME.md](AlDia.Entity/LEEME.md).
- **DAL:** implementa las interfaces y ejecuta exclusivamente procedimientos almacenados con parámetros tipados. Centraliza conexiones, lectura de resultados y registro de errores.
- **BLL:** valida entradas, exige una sesión y un rol, y expone operaciones del negocio. Recibe interfaces por constructor; no ejecuta SQL.
- **AplicacionBll:** compone los servicios y una sesión compartida. Se crea una instancia por sesión de la aplicación.

Se aplica herencia en las entidades, encapsulación de sus propiedades y de la sesión, abstracción mediante interfaces y polimorfismo. Los servicios comparten funcionalidades mediante composición. Las transacciones y las reglas sobre estados, stock y saldos permanecen también en SQL Server.

## Herencia basada en GestionPeliculas

Se sigue el patrón revisado en `PeliculaEntity`, `PeliculasCineEntity` y `PeliculasStreamingEntity`: clase base, constructores con `base(...)`, propiedades con `private set` y métodos `Cambiar...` que validan los datos.

```text
PersonaEntity (abstracta)
├── ClienteEntity
└── UsuarioEntity

CatalogoEntity (abstracta)
├── RolEntity
├── TipoEquipoEntity
├── ServicioEntity
└── RepuestoEntity

EquipoEntity (características y relaciones propias)

DetalleOrdenEntity (abstracta)
├── DetalleServicioEntity
└── DetalleRepuestoEntity
```

`PersonaEntity` reúne nombre, apellido, cédula, teléfono, estado y los métodos comunes. `ClienteEntity` sobrescribe `CambiarTelefono` con `override`: el teléfono es obligatorio para un cliente y opcional para un usuario. La regla se conserva incluso al acceder al cliente mediante una referencia `PersonaEntity`.

`CatalogoEntity` representa un registro de catálogo y reúne nombre, descripción, estado y sus métodos. Cada clase derivada conserva su identificador y sus campos específicos: precio base del servicio o marca, número de parte y precios del repuesto. Los límites de longitud corresponden a las columnas de cada catálogo.

Los constructores de DAL recuperan datos existentes; los constructores sin identificador crean entidades nuevas. `AsignarId` acepta el identificador persistido y no permite sustituirlo por el de otro registro. `Validacion` vive en Entity/Common y se comparte con BLL. Cambiar una entidad en memoria no escribe en SQL hasta guardar mediante BLL.

La sesión mantiene una copia privada del usuario y devuelve copias al consultarlo. Editar una entidad o cambiar su rol en memoria no altera los permisos del actor autenticado.

`RolEntity` representa la nueva tabla `Roles`, reutiliza `CatalogoEntity` y valida nombre de 50 y descripción de 200 caracteres. `EquipoEntity` valida las relaciones con cliente y tipo de equipo y permite editar sus características mediante métodos. Ambas tienen constructores de alta y reconstrucción y generan solicitudes para el guardado posterior. `CambiarEstado` y `EstadoNombre` permiten utilizar el patrón del maestro conservando `Estado` como booleano.

`UsuarioEntity.IdRol` representa la FK real. DAL la carga junto con el nombre de Roles y BLL guarda mediante UsuarioRolSolicitud. El valor nullable se conserva para constructores de compatibilidad que reciben solo el enum; no se inventan IDs. `Copiar` conserva el ID y cambiar solamente el enum a otro rol limpia el ID anterior. RolUsuario representa los tres permisos existentes; un nombre adicional no recibe permisos automaticamente.

`PaginacionEntity<T>` devuelve registros, total, pagina y tamaño. DAL pagina en SQL con OFFSET/FETCH, búsqueda y conteo del mismo filtro. Los listados de roles, usuarios, clientes, tipos, equipos, servicios, repuestos, órdenes, pagos y errores tienen consultas paginadas. IdTecnico en los detalles del expediente se deriva del responsable de la orden.

Entity cubre también EstadoReparacionEntity, OrdenReparacionEntity, DiagnosticoEntity, ConfirmacionEntity, PagoEntity, HistorialEstadoEntity y LogErrorEntity, cada una en su archivo. ExpedienteOrdenEntity compone los datos y comprueba sus relaciones. Las correcciones validan todos sus valores antes de asignar; los subtotales y saldos son calculados. PagoEntity genera solicitudes de corrección o anulación conservando el pago histórico. El usuario pidió completar esta capa de todo el sistema antes de pasar a DAL/BLL.

UsuarioEntity admite el constructor con FK y NombreRol y el alta desde RolEntity. UsuarioRolSolicitud conserva IdRol para el guardado en DAL/BLL. PermisoRol solo reconoce los tres permisos existentes; un rol nuevo se representa sin concederle un permiso automáticamente. IRolDal es implementada por RolDal y utilizada por RolBll. ConexionEntity valida la construcción y los cambios, conservando los inicializadores y el JSON de los formularios existentes.

## Operaciones disponibles

| Servicio | Responsabilidad |
|---|---|
| Roles | Mantenimiento, cambio de estado, activos, ID, búsqueda y paginación |
| Usuarios | Administrador inicial, autenticación, mantenimiento por FK, estados y consultas |
| Clientes / Equipos / TiposEquipo | Mantenimiento y consultas de catálogos |
| Ordenes | Recepción, toma, liberación, reparación, entrega, anulación y expediente |
| Diagnosticos | Diagnóstico y decisión del cliente, con correcciones y retiros |
| Servicios | Catálogo y servicios realizados en una orden |
| Repuestos | Catálogo, existencias y consumo en reparaciones |
| Pagos | Registro, corrección, anulación, consulta por orden o ID y paginación |
| Errores | Consulta paginada de la auditoría técnica para administrador |

`GuardarAsync` crea cuando el identificador es cero y actualiza cuando existe. Para las bajas de catálogos se envía `Estado = false`; el consumo de stock se modifica únicamente mediante las operaciones de inventario. Las altas devuelven el identificador generado.

`GuardarEntidadAsync` permite guardar los siete catálogos y entidades de mantenimiento desde su servicio correspondiente y asigna el identificador devuelto a la entidad. `GuardarAsync` conserva el contrato de solicitudes existente y utiliza las validaciones de las entidades. El stock del repuesto es de solo lectura y no se incluye en el guardado del catálogo.

## Uso al integrar los formularios

Después de configurar `Conexion.CadenaConexion`, crear la aplicación y autenticar al operador:

```csharp
using AlDia.BLL.Common;
using AlDia.Entity.Clientes;

var aplicacion = AplicacionBll.DesdeConexionActual();
await aplicacion.Usuarios.AutenticarAsync(nombreUsuario, contrasena);
var cliente = new ClienteEntity(nombre, apellido, telefono, cedula);
cliente.CambiarTelefono(telefonoActualizado);
var idCliente = await aplicacion.Clientes.GuardarEntidadAsync(cliente);
var expediente = await aplicacion.Ordenes.ConsultarExpedienteAsync(idOrden);
```

La BLL toma el actor de la sesión, sin pedir su identificador a los formularios. Los permisos concretos se comprueban en BLL y SQL. Para cerrar la sesión, usar `aplicacion.Usuarios.CerrarSesion()`.

El primer usuario del negocio se registra con datos y contraseña explícitos mediante `Usuarios.CrearAdministradorInicialAsync(UsuarioSolicitud, contrasena)`. Solo se permite si no existen usuarios; debe ser un administrador activo. No hay credenciales predeterminadas. Las contraseñas se almacenan con PBKDF2 y sal individual; las consultas de usuarios no devuelven hashes.

El inicio de sesión SQL de la conexión y el usuario del negocio son distintos. `Developer` conecta con SQL Server; `Usuarios.AutenticarAsync` autentica a un operador registrado en la tabla `Usuarios`.

## Inicio de sesión en UI

### Estilo de los formularios

`FrmConfiguracionConexion` es la referencia de diseño para los formularios del proyecto. Se conserva su ventana sin barra de título, encabezado blanco de 86 px con icono azul de 55 px, título Segoe UI Semibold de 18 pt, subtítulo gris, fondo RGB(230, 236, 243), campos blancos de 40 px con TextBox sin borde y pie blanco de 70 px con botones FontAwesome. Los contornos se aplican con `RedondearControlHelper`: 25 para el formulario y su panel de contenido, y 15 para los paneles de los campos.

El login utiliza esa estructura con un ojo integrado en el campo de contraseña y las acciones Ingresar y Salir en el pie. El registro del administrador inicial sigue el mismo patrón y aumenta la altura para alojar sus seis campos. Incluye ojos independientes en Contraseña y Confirmar, en lugar de una casilla para mostrar ambas claves. Los próximos formularios deben seguir esta referencia visual.

El proyecto de inicio es `AlDia.UI`. El flujo de `Program.cs` es:

1. Leer la configuración o mostrar el formulario de conexión si hace falta.
2. Abrir `Formularios/Seguridad/FrmLogin`, conectado a `UsuarioBll`.
3. Si la base no tiene usuarios, mostrar la opción **Crear primer administrador**. `FrmAdministradorInicial` solicita nombre, apellido, cédula, usuario, contraseña y confirmación. El rol inicial siempre es Administrador y SQL impide repetir esta inicialización.
4. Autenticar la cuenta del negocio y abrir `FrmDashsbor` con la misma instancia de `AplicacionBll`. Se muestra el nombre del usuario y su rol.
5. **Cerrar sesión** borra la sesión y vuelve al login; cerrar el formulario principal termina la aplicación y limpia la sesión.

El login incluye contraseña oculta, un botón de ojo para mostrarla u ocultarla, validación de campos, acceso con Enter, salida con Escape, bloqueo de envíos duplicados y reintento de conexión. El ojo conserva el foco y la selección del texto; su descripción accesible y su ayuda cambian según la acción disponible. El campo Usuario recibe el foco al terminar la comprobación inicial o un reintento. Hacer clic en los márgenes blancos de cada campo enfoca su TextBox. Los campos permanecen editables si aún no hay usuarios o si falla la conexión; Ingresar se habilita únicamente cuando la comprobación confirma que existen usuarios. Durante las operaciones pendientes se bloquean los campos para evitar cambios en las credenciales enviadas.

La configuración de conexión se solicita al iniciar cuando hace falta; el login no incluye un botón de conexión. Ante credenciales incorrectas muestra un mensaje genérico, limpia la contraseña y devuelve el foco a su campo. Los formularios cancelan las operaciones pendientes al cerrarse. Las contraseñas se envían sin recortar espacios; también se corrigió su guardado en el formulario de configuración de conexión.

Si falla la comprobación inicial de conexión, el mismo botón Ingresar cambia a Reintentar y vuelve a consultar el estado del sistema. Después de recuperar la conexión vuelve a Ingresar; si no hay usuarios, se mantiene deshabilitado y se ofrece crear el administrador inicial. No se necesita un control `btnReintentar` separado en el diseñador.

En `FrmAdministradorInicial`, cada ojo cambia únicamente la visibilidad de su campo y conserva el foco, la posición del cursor y la selección. Los márgenes blancos de Contraseña y Confirmar también permiten enfocar sus cajas de texto. Los ojos se deshabilitan junto con los campos mientras se crea la cuenta; la validación de longitud y coincidencia de las contraseñas se mantiene.

Los formularios de seguridad reciben la BLL por constructor y no ejecutan SQL. Sus constructores sin argumentos permiten abrir el diseñador de Visual Studio. La BLL mantiene la autenticación y SQL mantiene las reglas del negocio. No hay cuentas ni contraseñas predeterminadas.

## Diseño del dashboard

`Formularios/FrmDashsbor.Designer.cs` contiene el diseño editable desde Visual Studio: panel lateral con Inicio, Clientes, Equipos, Órdenes de servicio, Inventario, Pagos, Reportes, Catálogos y Usuarios; encabezado; un resumen compacto con tres indicadores y un área amplia de órdenes recientes. La distribución utiliza `TableLayoutPanel`, `Dock` y `Anchor` para ajustarse al tamaño de la ventana.

Esta etapa implementa únicamente la interfaz. El enlace visual de órdenes no tiene navegación ni operaciones de negocio. Los indicadores muestran un guion y las órdenes un estado vacío; no se cargan datos ni se agregan consultas a BLL o SQL. Se conserva el flujo de sesión que ya existía, con el nombre y rol en el pie lateral y el cierre de sesión existente.

`Controles/PanelTarjeta.cs` hereda de `Panel` y centraliza únicamente el dibujo de fondos y bordes redondeados. Expone `RadioBorde` y `ColorBorde` para editar la apariencia desde el diseñador. Las vistas se revisaron a tamaño normal y mínimo con un formulario temporal, sin iniciar sesión ni consultar la base de datos.

## Errores e integración pendiente

`ValidacionException` indica entradas incorrectas, `PermisoException` indica falta de sesión o permisos y `DatosException` conserva número, procedimiento y causa SQL. DAL intenta registrar los errores SQL mediante `usp_RegistrarError` con otra conexión; si falla el registro, conserva el error original.

La UI ya usa el flujo de login en lugar de la llamada antigua sin argumentos a `SistemaBll.CrearAdministradorInicialAsync()`. Se conservan las firmas y namespaces de compatibilidad de las capas. La conexión configurada no abre automáticamente una sesión del negocio. Quedan pendientes los formularios de clientes, equipos, órdenes, inventario, pagos y administración de usuarios.

## Base de datos y verificación

El archivo consolidado `AlDia.DataBase/AlDia.DataBase/Procedimientos_Almacenados.sql` contiene 51 procedimientos: 47 públicos llamados desde DAL y 4 auxiliares internos. El esquema incluye Roles, Usuarios.IdRol y ContrasenaHash; los detalles derivan el técnico de la orden. Las consultas de usuarios y credenciales cargan IdRol; el login exige rol activo y permisos configurados.

La implementación de DAL y BLL de todo el sistema se documenta en [DAL_BLL.md](DAL_BLL.md), con el alcance por tabla, métodos, permisos, paginación y requisitos de instalación. Esta etapa actualizó el archivo de procedimientos. Posteriormente el usuario lo ejecutó en AlDiaDB; se verificó mediante consultas de solo lectura que los 51 procedimientos instalados coinciden con las definiciones actuales del proyecto. La instalación de procedimientos ya está verificada. Una base existente debe tener el esquema actualizado y estos procedimientos. Base de datos.sql recrea la base y no se usa para actualizar datos existentes.

Las capas Entity, DAL y BLL cubren las operaciones del sistema. El contexto general, los requisitos y el procedimiento de instalación se encuentran en el [README principal](../README.md).

Desde la raíz:

```powershell
dotnet build AlDia.Solution/AlDia.Solution.slnx
```

Se conservan el login, el registro inicial y el dashboard actuales; los formularios de los módulos pertenecen a la siguiente etapa UI.
