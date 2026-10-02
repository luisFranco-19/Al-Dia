# DAL y BLL de todo el sistema

Estas capas cubren las operaciones de las 16 tablas de Al Día y conservan el método del maestro: entidades encapsuladas, dependencias recibidas por constructor, procedimientos almacenados parametrizados y métodos asíncronos. Se reutilizan las interfaces y los helpers del proyecto.

## Recorrido de una operación

El formulario recibe un servicio BLL. BLL toma el actor de la sesión, valida los datos y llama a la interfaz DAL. DAL prepara los parámetros, ejecuta el procedimiento y reconstruye las entidades mediante sus constructores. SQL comprueba las relaciones, los permisos y las reglas del taller dentro de sus transacciones.

`AplicacionBll` compone los servicios y comparte una sola `SesionUsuario`. `EjecutorSql` abre una conexión por operación, libera los recursos y traduce los errores SQL. `ParametrosSql` declara tipos, tamaños y precisión; `LecturaSql` reconstruye los resultados, incluidos sus campos nullable. No se agregaron paquetes ni consultas SQL en BLL.

## Cobertura

| Servicio BLL / DAL | Datos y operaciones |
|---|---|
| RolBll / RolDal | Roles: alta, edición, cambio de estado, activos, por ID, búsqueda y paginación |
| UsuarioBll / UsuarioDal | Usuarios: administrador inicial, autenticación, mantenimiento por IdRol, consultas, por ID, estados y paginación |
| ClienteBll / ClienteDal | Clientes: guardar, editar, estados, activos, por ID, búsqueda y paginación |
| TipoEquipoBll / TipoEquipoDal | TiposEquipo: guardar, editar, estados, activos, por ID, búsqueda y paginación |
| EquipoBll / EquipoDal | Equipos: guardar, editar, estados, activos, por ID, búsqueda y paginación |
| ServicioBll / ServicioDal | Servicios y DetalleServicios: catálogo; registrar, corregir y retirar trabajos de una orden |
| RepuestoBll / RepuestoDal | Repuestos y DetalleRepuestos: catálogo, ajuste de existencias, consumo, corrección y retiro con reversión de stock |
| OrdenReparacionBll / OrdenReparacionDal | OrdenesReparacion, EstadosReparacion e HistorialEstados: recibir, corregir, anular, tomar, liberar, reparar, entregar, consultar estados, listar y reconstruir expediente |
| DiagnosticoBll / DiagnosticoDal | Diagnosticos y ConfirmacionesReparacion: registrar, corregir y retirar diagnóstico y decisión del cliente |
| PagoBll / PagoDal | Pagos: registrar, corregir con reemplazo, anular; consultas por orden, por ID, con búsqueda y paginación |
| LogErrorBll / LogErrorDal / RegistroErroresDal | tblLogErrores: registro de excepciones SQL y consulta paginada para administrador |
| ConfiguracionBll / ConfiguracionDal; ConexionBll / ConexionDal | Configuración JSON y prueba de conexión existentes |

Los catálogos tienen `GuardarEntidadAsync`, `GuardarAsync`, `ConsultarAsync`, `ConsultarPaginaAsync`, `ObtenerPorIdAsync` y `CambiarEstadoAsync`. La consulta por ID incluye registros inactivos y devuelve null cuando no existe el registro. Un cambio de estado de un ID inexistente informa un error de validación.

Las tablas del flujo se modifican mediante sus operaciones propias. EstadosReparacion se consulta como catálogo fijo; HistorialEstados se genera en SQL al trabajar con órdenes; los errores se registran mediante el ejecutor. Estos registros no tienen un mantenimiento genérico que permita alterar la auditoría.

## POO al guardar

Los métodos de entidades preparan las solicitudes. BLL ejecuta el guardado y llama a `AsignarId` únicamente después de una respuesta correcta. Los métodos de alta de órdenes, diagnósticos, decisiones, detalles y pagos rechazan entidades ya registradas. Para recepción, diagnóstico, decisión y pago también se comprueba que el autor de la entidad corresponda a la sesión.

```csharp
var aplicacion = AplicacionBll.DesdeConexionActual();
await aplicacion.Usuarios.AutenticarAsync(usuario, contrasena);

var cliente = new ClienteEntity("Ana", "Lopez", "88888888", "CED-001");
await aplicacion.Clientes.GuardarEntidadAsync(cliente);
cliente.CambiarTelefono("77777777");
await aplicacion.Clientes.GuardarEntidadAsync(cliente);

var pagina = await aplicacion.Clientes.ConsultarPaginaAsync(
    pagina: 1, tamPagina: 10, buscar: "Lopez", soloActivos: true);
await aplicacion.Clientes.CambiarEstadoAsync(cliente.IdCliente, false);
```

Para el flujo se agregaron `RegistrarEntidadAsync`, `CorregirEntidadAsync`, `RegistrarDecisionEntidadAsync`, `CorregirDecisionEntidadAsync`, `RegistrarEntidadEnOrdenAsync`, `CorregirEntidadEnOrdenAsync`, `ConsumirEntidadAsync` y `CorregirConsumoEntidadAsync` en sus servicios correspondientes. Los contratos de solicitudes anteriores se conservan.

Después de modificar el estado de una orden, su responsable, stock, total o pagos, volver a consultar los datos. Esas propiedades representan el resultado de SQL; no se modifican artificialmente en memoria. `PagoBll.CorregirEntidadAsync` devuelve el ID del reemplazo y conserva el objeto del pago anterior; al consultar nuevamente se recupera su anulación.

## Roles, sesión y permisos

Usuarios y credenciales devuelven `IdRol` y el nombre real de Roles. `GuardarPorRolAsync` utiliza la FK seleccionada. El contrato anterior con `RolUsuario` sigue resolviendo el nombre en SQL, sin deducir IDs a partir del enum.

El login rechaza usuarios inactivos, roles inactivos y nombres de rol sin permisos configurados. Crear un rol en el catálogo no le concede permisos automáticamente. SQL protege el nombre exacto y el estado activo de Administrador, Recepcionista y Tecnico, porque las reglas existentes dependen de ellos. Tampoco se permite desactivar un rol que tenga usuarios asignados.

Los permisos actuales se conservan: administrador mantiene roles, usuarios y catálogos; administrador o recepción mantiene clientes y equipos; recepción registra entradas, decisiones, cobros y entregas; el técnico responsable realiza el trabajo. BLL y SQL comprueban los permisos. Las contraseñas se generan con PBKDF2 y no aparecen en los listados. La sesión conserva una copia privada del usuario.

## Consultas paginadas

Se pagina en SQL con `OFFSET/FETCH`, orden estable y total con el mismo filtro. El tamaño permitido es de 1 a 200; el valor habitual es 10. `PaginacionEntity<T>` devuelve registros, total, página y navegación. Una página fuera del rango devuelve filas vacías y conserva el total; una búsqueda sin resultados devuelve total cero.

Las consultas antiguas continúan disponibles: `@Pagina = NULL` devuelve el conjunto completo. Las nuevas agregan `@Pagina`, `@TamPagina`, `@Buscar` y `@TotalRegistros OUTPUT`. DAL lee el total después de cerrar el lector. Órdenes conserva sus filtros de estado, cliente, disponibilidad y anulación; pagos permite incluir anulados.

## Procedimientos e instalación

`AlDia.DataBase/AlDia.DataBase/Procedimientos_Almacenados.sql` contiene 51 procedimientos: 47 públicos con llamada desde DAL y 4 auxiliares internos. Se agregaron guardar/consultar roles, consultar pagos y consultar errores; se adaptaron usuarios a IdRol y se incorporó paginación a las consultas.

Esta etapa actualizó el archivo SQL y probó su instalación en bases temporales. La instalación inicial en AlDiaDB quedó pendiente. Posteriormente el usuario ejecutó el archivo completo; se comprobó mediante consultas de solo lectura en LAPTOP-1FRB462E/AlDiaDB que los 51 procedimientos instalados coinciden con las definiciones del archivo actual, incluidos los parámetros de paginación, IdRol, IdPago y el login con rol activo. **La instalación de procedimientos ya está verificada.** Para utilizar las capas con una base existente, esta debe tener el esquema actualizado del usuario y los procedimientos de ese archivo. El script de procedimientos utiliza `CREATE OR ALTER` y conserva los datos. `Base de datos.sql` recrea la base; se reserva para instalaciones nuevas.

## Verificación reproducible

Desde la raíz del proyecto:

```powershell
dotnet build AlDia.Solution/AlDia.Solution.slnx --no-restore
dotnet run --project tests/AlDia.Entity.Tests/AlDia.Entity.Tests.csproj
dotnet run --project tests/AlDia.Capas.Tests/AlDia.Capas.Tests.csproj
```

Las pruebas de capas requieren SQL Server local y autenticación integrada con permiso para crear una base temporal. Crean un nombre único `AlDia_Capas_Test_<GUID>`, cargan las 16 tablas y los 51 procedimientos y lo eliminan en `finally`. Excluyen el preámbulo que recrea AlDiaDB y rechazan instrucciones que cambien de base en los lotes cargados.

Cubren mantenimiento y paginación, FK de roles, hash y sesión, permisos en BLL y SQL, expediente, recepción hasta entrega, rechazo y resultado no reparado, correcciones y anulaciones, errores, cancelación, competencia de técnicos y consumo simultáneo de la última unidad. Comprueban que las transacciones fallidas conserven stock y total.

Resultado final: compilación con cero errores y cero advertencias, 556 comprobaciones de Entity y 157 comprobaciones DAL/BLL correctas.

Los formularios de los módulos y el listado base quedan para la etapa UI. Estas capas proporcionan las operaciones que utilizarán.
