# Adaptación a las clases del profesor

Se analizaron las explicaciones, las transcripciones completas y el código publicado de las cuatro partes adjuntas a los dos materiales de Classroom. También se contrastaron en el reproductor los pasajes de DAL, BLL y herencia de formularios.

## Material revisado

| Video | Pasajes de referencia |
|---|---|
| [5. Gestión de roles — Parte 1](https://www.youtube.com/watch?v=zz9hfYx_RNg) | 13:09: DAL; 17:36: conexión y comandos; 24:28: reconstrucción de entidades; 27:03: errores; 40:49: BLL y constructor |
| [6. Gestión de roles — Parte 2](https://www.youtube.com/watch?v=NLcDisufRy8) | 0:47: eventos del base; 3:03: overlay; 8:03: herencia; 9:34: métodos virtuales; 15:45: crear registro y edición |
| [7. Gestión de usuarios — Parte 1](https://www.youtube.com/watch?v=Nwvv681xWKU) | Reutilización del patrón DAL de roles, parámetros de cada procedimiento, obtención por ID y dependencia recibida por constructor en BLL desde 19:14 |
| [8. Gestión de usuarios — Parte 2](https://www.youtube.com/watch?v=JTivUmHLenI) | 1:27: herencia; 5:15: sobrescribir carga; 16:38: constructor de alta; 18:48: constructor de edición; 32:15: cargar roles |

Fuentes de código: [repositorio HelpDesk del profesor](https://github.com/MGarcia7783/HelpDesk), versiones `62e69bc` para roles y `7d55fdb` para usuarios; [eventos del formulario base](https://drive.google.com/file/d/17rak9ji_5-v44mj50fQevG-9C_YKtSMU/view) y [traductor de errores SQL](https://drive.google.com/file/d/1p0QQEx9vGJ-HGDK2TYxhDQuCdHv44Mll/view) adjuntos a Classroom.

## Comparación directa con el repositorio

Se volvió a consultar el remoto del profesor y `main` coincide con el commit `7d55fdbfedf1373da2a50b3dcf7ce1348406449d`. Los enlaces siguientes fijan esa versión, para que la comparación pueda reproducirse aunque el profesor publique cambios posteriores.

| Patrón del profesor | Aplicación en Al Día |
|---|---|
| [RolDal](https://github.com/MGarcia7783/HelpDesk/blob/7d55fdbfedf1373da2a50b3dcf7ce1348406449d/HelpDesk.Solution/HelpDesk.Dal/RolDal.cs): conexión, comando, parámetros, `await`, lector y traducción de errores | [RolDal](AlDia.DAL/Seguridad/RolDal.cs); se aplica el mismo recorrido a cada módulo DAL |
| [RolBll](https://github.com/MGarcia7783/HelpDesk/blob/7d55fdbfedf1373da2a50b3dcf7ce1348406449d/HelpDesk.Solution/HelpDesk.Bll/RolBll.cs): campo `readonly` y dependencia DAL recibida por constructor | [RolBll](AlDia.BLL/Seguridad/RolBll.cs); Al Día conserva la interfaz de Entity y la sesión compartida |
| Roles y usuarios separan `AgregarAsync`, `ActualizarAsync`, `ObtenerPorIdAsync`, `MostrarAsync` y `CambiarEstadoAsync` | Alta y edición separadas en los siete mantenimientos; `GuardarAsync` conserva los contratos de Entity y dirige la llamada a la operación correspondiente |
| [FrmBaseListado](https://github.com/MGarcia7783/HelpDesk/blob/7d55fdbfedf1373da2a50b3dcf7ce1348406449d/HelpDesk.Solution/HelpDesk.UI/Formularios/Base/FrmBaseListado.cs): eventos comunes y métodos virtuales | [FrmListadoBase](AlDia.UI/Formularios/Base/FrmListadoBase.cs): eventos con nombre, búsqueda al escribir, paginación y acciones comunes |
| [FrmRolesListado](https://github.com/MGarcia7783/HelpDesk/blob/7d55fdbfedf1373da2a50b3dcf7ce1348406449d/HelpDesk.Solution/HelpDesk.UI/Formularios/Hijos/Roles/FrmRolesListado.cs): `override` para cargar y abrir alta o edición | [FrmRoles](AlDia.UI/Formularios/Hijos/Catalogos/FrmRoles.cs), y el mismo patrón en los demás hijos |
| [FrmRolesRegistro](https://github.com/MGarcia7783/HelpDesk/blob/7d55fdbfedf1373da2a50b3dcf7ce1348406449d/HelpDesk.Solution/HelpDesk.UI/Formularios/Hijos/Roles/FrmRolesRegistro.cs): dos constructores, ID de edición, validación, rama de alta o edición y `DialogResult.OK` | [FrmRegistroRol](AlDia.UI/Formularios/Hijos/Catalogos/FrmRegistroRol.cs); los otros seis registros también distinguen alta y edición explícitamente |
| [FrmUsuariosRegistro](https://github.com/MGarcia7783/HelpDesk/blob/7d55fdbfedf1373da2a50b3dcf7ce1348406449d/HelpDesk.Solution/HelpDesk.UI/Formularios/Hijos/Usuarios/FrmUsuariosRegistro.cs): carga de roles activos y selección de la FK | [FrmRegistroUsuario](AlDia.UI/Formularios/Hijos/Usuarios/FrmRegistroUsuario.cs) utiliza `MostrarRolesActivosAsync` y guarda el ID real del rol |
| Overlay y recarga del listado al guardar | [FrmOverlay](AlDia.UI/Formularios/Base/FrmOverlay.cs) y el evento heredado del listado |

Hay una diferencia que debe quedar explícita: **el profesor tiene un base para listados; sus formularios de registro heredan directamente de `Form`**. `FrmRegistroBase` ya existía en Al Día y se conserva para reutilizar sus controles y eventos. Sus hijos implementan la carga, la validación y las ramas de alta o edición del profesor. La herencia del registro es una adaptación a los diseños de Al Día, no una clase presente en HelpDesk.

Entity utiliza ahora el constructor vacío y los métodos de asignación del profesor. Se conservan las diferencias impuestas por Al Día: los parámetros SQL son tipados, los procedimientos devuelven IDs y totales por `OUTPUT`, y los permisos usan el ID del actor de la sesión. El profesor recibe el nombre del actor desde UI y utiliza otros nombres de tablas y procedimientos. La réplica sigue su organización y recorrido de una operación, respetando estos contratos del proyecto existente.

## Entity: constructores y métodos del profesor

Las referencias directas son [RolEntity](https://github.com/MGarcia7783/HelpDesk/blob/7d55fdbfedf1373da2a50b3dcf7ce1348406449d/HelpDesk.Solution/HelpHesk.Entities/Entidades/RolEntity.cs) y [UsuarioEntity](https://github.com/MGarcia7783/HelpDesk/blob/7d55fdbfedf1373da2a50b3dcf7ce1348406449d/HelpDesk.Solution/HelpHesk.Entities/Entidades/UsuarioEntity.cs). Ambas ofrecen constructor sin parámetros, constructor de alta, propiedades con `private set` y métodos `AsignarId` y `Cambiar...`. El constructor de alta utiliza los métodos para validar y asignar.

Ese patrón está aplicado a las catorce entidades de tablas que se preparan o mantienen: roles, usuarios, clientes, tipos, equipos, servicios, repuestos, estados, órdenes, diagnósticos, confirmaciones, detalles de servicios, detalles de repuestos y pagos. Se conservan los constructores completos existentes para reconstrucción y compatibilidad.

La herencia propia de Al Día se conserva:

| Base abstracta | Hijos |
|---|---|
| `PersonaEntity` | `ClienteEntity`, `UsuarioEntity` |
| `CatalogoEntity` | `RolEntity`, `TipoEquipoEntity`, `ServicioEntity`, `RepuestoEntity` |
| `DetalleOrdenEntity` | `DetalleServicioEntity`, `DetalleRepuestoEntity` |

Los constructores de los bases llaman a sus métodos comunes. Los hijos ofrecen el constructor vacío y utilizan `base(...)` para compartir datos y comportamiento. `ClienteEntity.CambiarTelefono` mantiene su `override`; el teléfono es obligatorio para Cliente y opcional para Usuario.

Los textos se validan dentro del método mediante `string.IsNullOrWhiteSpace` y se asignan con `Trim()`, como en las entidades del profesor. Se eliminaron los máximos de caracteres de las entidades y los tamaños que recibía `CatalogoEntity` por constructor. Los opcionales vacíos se convierten en `null`; se conserva la validación de formato de correo y las reglas de identificadores, importes y relaciones.

```csharp
public void CambiarNombre(string nombre)
{
    if (string.IsNullOrWhiteSpace(nombre))
        throw new ValidacionException("Debe ingresar el nombre del catalogo.");
    Nombre = nombre.Trim();
}
```

```csharp
RolEntity rol = new();
rol.CambiarNombre("Rol de ejemplo");
rol.CambiarDescripcion("Creado mediante métodos");
rol.CambiarEstado(true);
await aplicacion.Roles.AgregarAsync(rol);
```

El constructor vacío representa una entidad en preparación; sus textos empiezan vacíos, el ID en cero y el estado booleano en falso, como en los ejemplos del profesor. Los constructores de alta con parámetros siguen creando personas, equipos y catálogos activos. `CrearSolicitud` valida que se hayan completado los datos obligatorios antes de entregar una solicitud a BLL. Los IDs persistidos siguen protegidos contra sustitución.

DAL reconstruye los siete mantenimientos con `new()`, `AsignarId` y `Cambiar...`. Los formularios crean las entidades nuevas con `new()` y completan los campos mediante métodos; la edición conserva el objeto cargado y su ID. Órdenes y pagos también utilizan esa preparación desde sus formularios.

Los métodos nuevos respetan las reglas existentes: los detalles no se trasladan a otra orden; un pago persistido se corrige mediante reemplazo; el número, equipo, actor y estado inicial de una orden solo se preparan antes de guardarla. `AsignarStockConsultado` y `AsignarFechaRegistro` reconstruyen proyecciones de SQL; el guardado del catálogo no incluye stock ni fechas consultadas.

`HistorialEstadoEntity` y `LogErrorEntity` conservan sus datos de auditoría como solo lectura y se reconstruyen mediante constructores completos. Las solicitudes, la paginación, las credenciales y el expediente conservan sus contratos. HelpDesk no contiene una jerarquía de bases de Entity ni estos módulos del taller: son adaptaciones del patrón de sus entidades de mantenimiento al dominio de Al Día.

## Recorrido de una operación

```text
Evento del formulario base
  → método virtual sobrescrito por el formulario hijo
    → BLL: entidad, validación y sesión
      → DAL: conexión, SqlCommand, parámetros y lector
        → procedimiento almacenado de Al Día
```

La adaptación está integrada en el proyecto original `C:\Users\admin\Desktop\Al dia`, en su rama `Capa-UI`. Se conservaron sus diseños y los scripts SQL. Entity se adaptó al patrón de constructores y métodos; DAL, BLL y los eventos heredados de UI utilizan esas entidades en la misma carpeta.

## DAL

Los comandos de cada operación se declaran en su clase DAL: conexión, nombre del procedimiento, `CommandType.StoredProcedure`, parámetros y ejecución con `await`. Alta y edición comparten el comando privado de guardado de su módulo. Las escrituras usan `ExecuteNonQueryAsync`; las consultas usan `ExecuteReaderAsync` y `ReadAsync`. `using` libera los recursos al terminar o fallar.

`EjecutorSql.ObtenerConexion()` comparte la fábrica de conexiones que ya existía. `TraducirErrorAsync` comparte la auditoría y delega la traducción en `SqlErrorHelper`. Los comandos de cada módulo se ven en su propia clase DAL.

Se retiraron los métodos genéricos antiguos de ejecución, escritura y lectura de `EjecutorSql`, porque ningún módulo los utiliza después de declarar sus comandos explícitos. Esta clase conserva únicamente la conexión y la traducción con auditoría.

Se conservaron los tipos, tamaños y precisión de `ParametrosSql`, los lectores de `LecturaSql`, la cancelación y los parámetros de salida. Los siete mantenimientos se reconstruyen con el constructor vacío y métodos de asignación; las consultas del flujo y de auditoría conservan los constructores completos que validan sus relaciones.

Los tamaños textuales pertenecen a los contratos SQL. `ParametrosSql.Texto` rechaza valores mayores que el parámetro declarado para evitar que `SqlParameter.Size` o el procedimiento los recorten silenciosamente; no añade máximos a Entity ni modifica el esquema. Los parámetros de tamaño `-1` conservan los mensajes completos de `NVARCHAR(MAX)`. Los helpers textuales con máximo existentes se mantienen para consumidores de BLL, pero las entidades ya no los utilizan.

El profesor cuenta y lista mediante procedimientos separados. En Al Día, el procedimiento de consulta devuelve las filas y `@TotalRegistros OUTPUT`; se obtiene ese valor después de cerrar el lector. Los procedimientos `usp_Guardar...` ya distinguen alta y edición por el ID, por lo que se utiliza ese contrato existente.

Los siete mantenimientos exponen `AgregarAsync` y `ActualizarAsync` en DAL. La primera operación exige ID cero y la segunda exige un ID positivo. Ambas usan el comando del procedimiento existente del módulo; no se duplica el SQL. Las firmas `GuardarAsync` de las interfaces que ya estaban en Entity dirigen las llamadas a estas operaciones. Usuarios conserva las dos solicitudes existentes, por enum y por FK.

## BLL

Las dependencias tienen campos `private readonly` y constructores explícitos, como en las clases. BLL recibe las interfaces DAL existentes y la sesión compartida. Mantiene los permisos, la validación de entidades y el hash de contraseñas.

`AgregarAsync` y `ActualizarAsync` distinguen alta y edición, como en HelpDesk, y rechazan un ID incompatible con la acción. `GuardarEntidadAsync` comparte la persistencia y asigna el ID devuelto por SQL; se conserva para los consumidores existentes. `ObtenerPorIdAsync` carga el registro para editarlo, incluidos los inactivos. `CambiarEstadoAsync` consulta la entidad, cambia su estado mediante su método y la guarda. `MostrarRolesActivosAsync` proporciona los roles del combo de usuarios.

`MostrarAsync` permite a los listados usar sus filtros. Los filtros que acepta SQL conservan la paginación en el procedimiento. Para los filtros adicionales de los controles existentes, BLL filtra el conjunto antes de calcular el total y paginar. Esto evita mostrar únicamente las coincidencias de una página. Estas consultas adicionales reúnen datos en memoria y deben revisarse si crece mucho el volumen.

`CatalogosBll` compone roles, tipos de equipo y servicios para la pantalla común; conserva el ID y el tipo de catálogo de cada fila.

## Herencia y polimorfismo en UI

`FrmListadoBase` concentra los eventos de Buscar, Anterior, Siguiente, Nuevo, Editar y Cambiar estado. Los hijos sobrescriben:

- `MostrarDatosAsync`: consulta BLL y entrega la página al base.
- `CrearFormularioAccionPrincipal`: crea el formulario de alta.
- `CrearFormularioAccionSecundaria`: crea la edición o consulta por ID.
- `ObtenerIdSeleccionado`: recupera el ID de la entidad de la fila.
- `DesactivarAsync`: ejecuta la acción del módulo mediante BLL.

Los hijos reutilizan `MostrarPagina`, la presentación del estado vacío, la selección, los botones de paginación y el overlay. `TextChanged` inicia la búsqueda desde la primera página, como en HelpDesk; se conservan también el botón Buscar y Enter. El base vuelve a consultar al recibir `DialogResult.OK`. Una consulta anterior cancelada no puede sustituir una búsqueda más reciente.

`FrmRegistroBase` concentra Guardar, Cancelar, Cerrar y la carga inicial. Cada registro sobrescribe `CargarDatosAsync`, `ValidarControles` y `GuardarAsync`. La sobrecarga de constructor con ID abre la edición; el constructor sin argumentos conserva el diseñador de Visual Studio. El hijo comprueba `EsEdicion` y llama a `ActualizarAsync` o `AgregarAsync` de BLL. Los botones evitan envíos repetidos mientras se guarda.

Se retiraron los eventos duplicados de Usuarios y Reportes. En Catálogos se conectaron al base las columnas y los filtros que el diseñador había creado sin agregarlos a sus contenedores. Al recibir registros, la grilla del base ocupa toda el área; la altura limitada al encabezado se utiliza únicamente para el estado vacío.

## Alcance de las pantallas

| Pantallas | Integración actual |
|---|---|
| Roles, usuarios, clientes, equipos, tipos de equipo, servicios y repuestos | Listados, filtros, alta, edición y estado mediante los bases |
| Catálogos | Listado conjunto y acciones según el catálogo de la fila; para crear se selecciona un catálogo |
| Órdenes | Listado, filtros, recepción, anulación con motivo y consulta del expediente |
| Pagos | Listado, filtros, registro, corrección con reemplazo y anulación con motivo |
| Expediente | Consulta de recepción, diagnóstico, decisión, detalles, pagos e historial; las acciones técnicas de esta pantalla continúan pendientes |
| Reportes | Vista previa visual mediante el evento heredado; generación, exportación e impresión continúan pendientes |
| Ajuste de existencias | BLL y DAL verificadas; su formulario sigue como diseño para una integración posterior |

Los indicadores del inicio todavía conservan su diseño. Esta adaptación mantiene las operaciones completas de DAL/BLL y conecta la herencia de los formularios existentes; no añade procedimientos de reportes ni cambia las reglas del taller.

## Verificación reproducible

Desde la raíz del repositorio:

```powershell
dotnet build AlDia.Solution/AlDia.Solution.slnx
dotnet run --project AlDia.Solution/AlDia.Verificacion/AlDia.Verificacion.csproj -- .
```

La verificación necesita SQL Server con autenticación integrada y permiso para crear una base temporal. Por defecto utiliza `localhost`; `ALDIA_SERVIDOR_PRUEBAS` permite indicar otra instancia de pruebas.

El comprobador crea una base nueva `AlDia_Verificacion_<GUID>`, instala el esquema y los procedimientos existentes y comprueba los nombres de parámetros contra los metadatos de SQL Server. No ejecuta el bloque de eliminación de `Base de datos.sql`. Al finalizar elimina exclusivamente su propia base temporal.

Comprueba entidades preparadas mediante métodos, rechazo de entidades incompletas antes de SQL, herencia de los bases, el `override` de Cliente mediante `PersonaEntity`, textos sin máximos en Entity, asignación de IDs y conservación de importes e historial. Verifica que la DAL rechace textos mayores que el parámetro SQL sin guardar una versión recortada, y que conserve nulos y `NVARCHAR(MAX)`. También comprueba alta y edición separadas en los siete mantenimientos, IDs de salida, roles por FK, filtros y paginación, stock, recepción, diagnóstico, aprobación, reparación, corrección de pago, entrega y auditoría de errores. Las pruebas de formularios comprueban sus datos vinculados, Guardar, Cancelar, edición, navegación, búsqueda automática al escribir y búsquedas concurrentes. La captura de Roles se genera en `artifacts/verificacion/roles.png`, con datos temporales de prueba; es un render de WinForms. [Microsoft documenta las limitaciones de DrawToBitmap](https://learn.microsoft.com/es-es/dotnet/api/system.windows.forms.control.drawtobitmap?view=windowsdesktop-10.0).
