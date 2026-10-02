# Método de trabajo para Al Día, basado en las clases del maestro

Revisión: 1 de octubre de 2026. Maestro: Mario Garcia; clase: Desarrollo de Aplicaciones III, IIIA-REG.

El usuario pidió que el desarrollo de Al Día siga la manera de trabajar del maestro: arquitectura por capas y programación orientada a objetos. Aclaró que primero quiere completar Entity de todo el sistema antes de pasar a DAL y BLL. Este documento conserva ese contexto para las próximas tareas.

## Material estudiado y alcance

Se analizaron las transcripciones completas de los seis videos de los dos materiales solicitados, sus 17 archivos de apoyo y el código de HelpDesk enlazado en las descripciones. También se inspeccionaron fotogramas de los seis videos para contrastar la estructura del proyecto, los formularios y el resultado visual. Las transcripciones son automáticas; los nombres y las firmas se contrastaron con el código. Esto es un análisis mediante transcripciones, fotogramas y código, no una reproducción continua de las 4 horas y 43 minutos de video.

Materiales de Classroom:

- [1. Crear proyecto y configuraciones iniciales](https://classroom.google.com/u/0/c/ODcxMTc4MjIzNzg4/m/ODY5Njg0MDA0Nzk2/details?hl=es).
- [2. Gestión de roles](https://classroom.google.com/u/0/c/ODcxMTc4MjIzNzg4/m/ODY5OTEyNTgyNjc4/details?hl=es).

| Video | Duración | Referencias útiles y contenido |
|---|---|---|
| [1. Arquitectura del proyecto](https://www.youtube.com/watch?v=cVprrfi89lY) | 40:33 | 7:33–13:39: solución, proyectos y referencias; 13:41–15:24: carpetas; 22:23–35:42: UsuarioEntity, encapsulamiento, constructores y métodos; helpers de UI |
| [2. Configurar conexión a la base de datos](https://www.youtube.com/watch?v=JSPnr0SZOBU) | 1:11:30 | 0:30–6:54: configuración dinámica, Entity, DAL y BLL; 7:01–38:39: diseño; 39:00–53:03: validación y eventos; 53:29–1:06:19: arranque y administrador inicial; 1:06:40–1:08:43: comprobación |
| [3. Diseño de interfaz del menú de administración](https://www.youtube.com/watch?v=hw8nbDPORyc) | 32:38 | 1:00–24:45: tamaño, menú lateral, estado y panel contenedor; 25:45–30:47: cierre y control de sesión; 31:10–32:31: Git |
| [4. Crear y configurar formulario base listado](https://www.youtube.com/watch?v=ls1-4t4GBwU) | 37:41 | 0:14–8:48: diseño reutilizable; 11:33–27:41: estado de paginación y métodos virtuales; 27:44–36:58: eventos, formato y navegación |
| [5. Gestión de roles – Parte 1](https://www.youtube.com/watch?v=zz9hfYx_RNg) | 46:55 | 1:49–2:51: operaciones SQL; 3:02–5:09: rama del módulo; 5:28–13:07: RolEntity; 13:09–40:46: DAL, paginación y errores; 40:49–46:29: BLL |
| [6. Gestión de roles – Parte 2](https://www.youtube.com/watch?v=NLcDisufRy8) | 53:51 | 0:36–6:30: eventos base y overlay; 6:35–28:28: listado heredado y overrides; 28:40–46:35: registro y edición; 47:35–50:36: correcciones y comprobaciones; 50:39–53:34: Git |

Código contrastado: [HelpDesk en el commit enlazado por el maestro](https://github.com/MGarcia7783/HelpDesk/tree/62e69bcfd2e91a76a68ca5cae2badd384fd38056), mensaje `feat: implementar el modulo de roles`. Se usó esa revisión concreta para evitar mezclar los ejemplos con cambios posteriores.

## Arquitectura que enseña

La solución contiene cuatro proyectos: una aplicación Windows Forms en C# y tres bibliotecas de clases. En la revisión del maestro se usa .NET 10, FontAwesome.Sharp para la UI y Microsoft.Data.SqlClient para SQL Server.

| Capa | Responsabilidad | Referencias de proyecto enseñadas |
|---|---|---|
| UI | Formularios, eventos, validación de controles, presentación y navegación | BLL y Entity |
| BLL | Punto de entrada a las operaciones del negocio; recibe DAL por constructor | DAL y Entity |
| DAL | Conexiones, procedimientos almacenados, parámetros y reconstrucción de objetos | Entity |
| Entity | Entidades, propiedades, constructores y métodos que controlan los cambios | No depende de las otras capas |

Flujo de una operación: **Formulario → BLL → DAL → procedimiento almacenado → SQL Server**. Las entidades transportan la información entre las capas.

Organización del ejemplo: UI separa `Formularios`, `Helpers` y `Seguridad`; los formularios se agrupan en `Base`, `Seguridad` e `Hijos/Roles`. BLL y DAL tienen `Common`; Entity separa `Entidades` y una carpeta prevista para DTO. Para Al Día se conserva la organización existente por módulos y sus namespaces.

Hay que distinguir la arquitectura explicada de ciertos detalles del ejemplo: su UI instancia clases DAL para construir BLL y usa ConfiguracionDal directamente; ConexionBll abre una SqlConnection para probar la conexión. En el mantenimiento de roles, los formularios sí delegan las operaciones a BLL. No son razones para trasladar consultas SQL a los formularios de Al Día.

## POO aplicada en los ejemplos

### Encapsulamiento y constructores

`UsuarioEntity` y `RolEntity` exponen propiedades con `get; private set;`. Las modificaciones se hacen por métodos como `CambiarNombre`, `CambiarEstado` y `AsignarId`. El método que cambia un nombre comprueba que tenga contenido antes de asignarlo; la entidad se protege aunque se use desde otro formulario.

El ejemplo usa sobrecarga de constructores: uno vacío para reconstruir datos obtenidos por DAL y otro con parámetros para crear un registro. El constructor con nombre de RolEntity valida el nombre y establece Activo en verdadero. La sobrecarga se distingue por la firma de los parámetros, no solamente por su cantidad.

`Estado` es una propiedad calculada a partir de Activo para mostrar un texto comprensible en el grid. La contraseña del usuario no se expone en la entidad que se presenta en pantalla.

En Al Día ya hay constructores para registros nuevos y existentes; se conserva esa forma de reconstrucción, sin exigir un constructor vacío a todas las entidades. Los objetos de solicitudes y resultados pueden mantener sus contratos actuales; no todos deben convertirse en entidades mutables.

### Herencia y polimorfismo en formularios

`FrmBaseListado : Form` contiene el diseño y los eventos compartidos. `FrmRolesListado : FrmBaseListado` hereda esa estructura y especializa el comportamiento con `override`.

Los puntos de extensión del maestro son:

- `MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)`.
- `CrearFormularioAccionPrincipal()` para abrir el registro nuevo.
- `CrearFormularioAccionSecundaria(int id)` para abrir la edición.
- `ObtenerIdSeleccionado()`.
- `DesactivarAsync()` para cambiar el estado.
- `FormatearCelda(...)` y propiedades de visibilidad de los tres botones.

Los eventos base invocan esos métodos y se ejecuta la implementación del formulario derivado. Ahí se ve el polimorfismo. Los métodos compartidos como ActualizarPaginacion y AjustarColumnas se reutilizan sin repetir su implementación.

Antes de heredar, el maestro iguala el tamaño del formulario hijo al del base. En el diseñador expone los controles que los hijos necesitan modificar. Al implementar en Al Día hay que mantener el diseñador funcional y dar acceso a esos controles de forma coherente con la herencia.

### Composición y dependencias

RolBll recibe un RolDal por constructor y conserva la referencia en un campo `private readonly`. FrmRolesRegistro recibe la BLL por constructor. El ejemplo usa clases concretas; no presenta una capa de interfaces de repositorios como requisito.

Al Día ya tiene interfaces DAL y AplicacionBll para componer los servicios y compartir la sesión. Se pueden reutilizar con el mismo propósito; estudiar el ejemplo no requiere sustituir esas piezas ni añadir nuevas arquitecturas.

## Secuencia de desarrollo de cada módulo

1. Revisar su tabla, tipos, identificador, relaciones y procedimientos almacenados existentes.
2. Crear o ajustar la entidad: propiedades encapsuladas, constructores y métodos de cambio con validación.
3. Implementar DAL: parámetros, consultas, reconstrucción de entidades y traducción de errores SQL.
4. Exponer las operaciones en BLL y las reglas correspondientes al negocio.
5. Crear el listado que hereda del formulario base: cargar datos, configurar columnas, buscar y paginar.
6. Crear un formulario de registro que también permita editar: recibir BLL y, para edición, el identificador.
7. Integrar el módulo al menú mediante la navegación compartida.
8. Comprobar crear, editar, cambiar estado, buscar, navegar páginas y mostrar errores; comprobar que la pantalla se actualice.
9. Registrar un avance coherente en Git según la tarea autorizada.

Este orden adapta el recorrido de Roles a Clientes, Equipos, Tipos de equipo, Servicios y Repuestos. Órdenes, Diagnósticos y Pagos tienen operaciones propias que deben respetar sus reglas, además de los elementos de interfaz compartidos.

## Persistencia, paginación y errores

RolDal usa SqlConnection, SqlCommand con CommandType.StoredProcedure y parámetros. Abre y lee con métodos async/await y libera recursos con using. Usa ExecuteScalarAsync para contar y ExecuteReaderAsync para obtener registros.

Sus operaciones son mostrar con búsqueda y paginación, mostrar activos, obtener por ID, agregar, actualizar y cambiar estado. La consulta paginada utiliza OFFSET/FETCH; la consulta de conteo usa el mismo filtro. Una consulta por ID puede devolver null.

`PaginacionEntity<T>` contiene Registros, TotalRegistros, PaginaActual, TamPagina y TotalPaginas calculado. Permite reutilizar la estructura con distintas entidades. El listado empieza en la página 1 y con tamaño 10. Buscar vuelve a la primera página; los botones de navegación se habilitan según los límites. Si no hay registros, la UI muestra página 0 de 0. Aunque el helper calcula una página cuando el total es cero, el formulario trata explícitamente el estado vacío.

Los procedimientos de escritura validan duplicados y registros inexistentes, hacen transacciones y registran errores antes de volver a lanzarlos. SqlErrorHelper convierte los códigos conocidos en mensajes comprensibles. Para roles, 50001 corresponde a duplicado, 50002 a inexistente y 50003 a usuarios asignados. Los códigos pertenecen a HelpDesk: en Al Día se usan sus procedimientos y errores reales.

La BLL de roles en esta revisión delega en DAL; la entidad valida el nombre y SQL valida duplicados y relaciones. No sería exacto afirmar que el maestro coloca todas las validaciones en BLL.

## Comportamiento de los formularios

El listado incluye título, descripción, búsqueda, botones Nuevo/Editar/Estado, DataGridView y pie de paginación. Oculta el identificador y el booleano interno y presenta el nombre y el texto del estado. El base también ofrece un estado vacío con imagen, cuya activación debe conectarse a la carga real de datos.

El registro usa dos constructores: BLL para crear y BLL más ID para editar. Un identificador nullable permite distinguir ambos modos. En Load carga el registro existente si es edición; si no existe, informa y cancela.

Al guardar: valida los controles con ErrorProvider, crea o modifica la entidad, llama a BLL y, tras éxito, establece DialogResult.OK y cierra. El listado detecta ese resultado y vuelve a consultar. Cancelar devuelve DialogResult.Cancel. Los métodos async devuelven Task; async void se reserva para los eventos de WinForms.

Estado alterna activo/inactivo después de confirmar la acción y recarga el listado. En HelpDesk SQL impide desactivar un rol con usuarios asignados. Es una baja lógica, no una eliminación del registro.

FrmOverlay oscurece el formulario principal mientras se muestra el modal: copia posición y tamaño, usa una opacidad de 0.45 y no aparece en la barra de tareas. Se dispone al cerrar el modal. El registro debe quedar centrado respecto a su ventana propietaria.

El maestro corrige explícitamente dos omisiones durante Roles parte 2: DialogResult.OK/Close después de guardar y la recarga de datos después de cambiar estado. Esos pasos forman parte del comportamiento final que vamos a seguir.

## Configuración, helpers y diseño

La conexión es configurable: Servidor, BaseDatos, UsuarioSql y Password en ConexionEntity; ConfiguracionDal guarda y lee `Config/configuracion.json`; Conexion.ObtenerConexion crea la conexión con la cadena configurada.

En el arranque se solicita configuración si falta, se lee el archivo y se construye la cadena. El formulario permite probar antes de guardar. Tras una prueba correcta bloquea los campos y habilita Guardar. El ejemplo crea además un administrador inicial mediante SistemaBll/SistemaDal y un procedimiento SQL.

Los helpers estudiados centralizan navegación entre formularios y botón activo, datos y cierre de sesión, redondeo, colores por estado, formato del grid y validación de TextBox/ComboBox/correo. No deben concentrar consultas del negocio.

El diseño se construye con el diseñador de Visual Studio, paneles, Dock, Anchor y TableLayoutPanel. Usa menú lateral oscuro, botones e iconos FontAwesome, campos blancos, fondos claros y esquinas redondeadas. La referencia de Al Día sigue siendo su FrmConfiguracionConexion actual: el estilo de las clases se adapta a ese formulario.

## Adaptación al estado actual de Al Día

Esta comparación es una revisión de estructura y contratos, no una nueva ejecución de las pruebas o de la aplicación.

| Aspecto | Estado encontrado | Aplicación en próximas tareas |
|---|---|---|
| Cuatro proyectos | AlDia.UI, AlDia.BLL, AlDia.DAL y AlDia.Entity | Mantenerlos y explicar el recorrido de cada operación |
| Entidades con POO | PersonaEntity y CatalogoEntity; propiedades privadas, constructores y métodos de cambio | Reutilizar la base existente y especializar reglas donde corresponda |
| Persistencia | DAL parametriza procedimientos almacenados mediante EjecutorSql | Aprovechar las piezas actuales con los contratos SQL reales |
| Servicios y sesión | BLL recibe interfaces; AplicacionBll compone servicios y una sesión | Conservar el actor autenticado y los permisos existentes |
| Configuración e inicio | Hay formulario de conexión, login y registro inicial | Tomarlos como base del flujo y del estilo visual |
| Listado heredado | No se encontró FrmBaseListado en la UI actual | Incorporarlo cuando se pida implementar los listados de módulos |
| Paginación común | Entity incorpora PaginacionEntity; DAL/BLL ya incluyen búsqueda y paginación SQL | Usar ConsultarPaginaAsync al desarrollar los listados; no recortar listas completas en memoria |
| Roles | El usuario creó la tabla Roles y Usuarios.IdRol; Entity incorpora RolEntity; los permisos todavía usan RolUsuario | RolDal y RolBll trabajan con la tabla real; usuarios carga y guarda IdRol, sin deducir IDs del enum |

La implementación existente tiene diferencias deliberadas respecto al ejemplo: sesión en BLL, interfaces, validaciones y procedimientos propios, contraseñas con hash y administrador inicial solicitado al usuario. Se conservan esas reglas mientras se sigue la organización didáctica del maestro.

También hay detalles del ejemplo que no deben copiarse automáticamente: sesión inicial con datos de administrador de demostración, contraseña SQL guardada como texto, Trim al guardar una contraseña que se probó sin Trim y escritura con ExecuteReaderAsync aunque no se use un resultado. En Al Día deben preservarse los valores exactos de las contraseñas y los contratos de persistencia. El constructor vacío de RolEntity deja Activo en falso, pero el formulario de alta lo utiliza y SQL aplica el valor predeterminado: una entidad nueva de Al Día debe tener un estado coherente con lo que se persiste.

## Acuerdo para las próximas tareas

Primero completar la capa Entity de todo el sistema: todas las tablas, relaciones, constructores y métodos con validaciones. El usuario pidió cerrar ese alcance antes de avanzar a DAL y BLL. Después, desarrollar los módulos según la etapa autorizada, con clases y métodos claros y explicables en clase. Relacionar cada archivo con su capa y mostrar dónde se aplican encapsulamiento, constructores, herencia, composición y polimorfismo. Reutilizar un listado base, helpers y un registro con modos de alta/edición. Mantener las reglas propias de Al Día y evitar nuevas dependencias o patrones sin una necesidad concreta.

El estudio inicial agregó solo documentación. Después se completó Entity y el usuario autorizó avanzar a DAL/BLL de todo el sistema. El estado de esa implementación, sus pruebas y la instalación de procedimientos verificada en la base real se documentan en AlDia.Solution/DAL_BLL.md.
