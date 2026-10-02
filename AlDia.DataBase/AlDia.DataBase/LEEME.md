# Lógica del negocio de Al Día

Los procedimientos representan operaciones del taller: recibir equipos, diagnosticar, registrar la decisión del cliente, reparar, consumir repuestos, cobrar y entregar. `Procedimientos_Almacenados.sql` contiene **51 procedimientos consolidados**: 24 operaciones del flujo, 4 auxiliares internos y 23 de soporte para las capas Entity, DAL y BLL. Los 47 públicos tienen llamadas desde DAL.

## Archivos y orden de ejecución

1. Solo para una instalación nueva, ejecutar `Base de datos.sql`. La versión actual elimina AlDiaDB si existe y la crea de nuevo; no se ejecuta para actualizar una base con datos. Crea las 16 tablas, los índices, los tres roles y los ocho estados iniciales.
2. Ejecutar `Procedimientos_Almacenados.sql` sobre el esquema actualizado. Incluye todos los procedimientos y usa CREATE OR ALTER para conservar los datos.

La solución AlDia.DataBase.slnx está en la carpeta superior. Los procedimientos se editan en el archivo consolidado; también se puede seleccionar y ejecutar un bloque completo con su GO en SSMS. El script incluye USE AlDiaDB. Una base con el esquema anterior requiere su migración antes de instalar estos contratos.

La etapa DAL/BLL actualizó este archivo y verificó los 51 procedimientos en una base temporal, eliminada al finalizar. Posteriormente el usuario ejecutó el archivo completo en AlDiaDB y se verificó mediante consultas de solo lectura que los 51 procedimientos coinciden con la versión actual del proyecto. **La instalación ya está verificada.** No se recreó la base real ni se agregaron datos de prueba en ella.

## Operaciones públicas

| Proceso | Procedimientos | Regla que aplican |
|---|---|---|
| Recepción | usp_RegistrarRecepcion, usp_CorregirRecepcion, usp_AnularOrden | Inicia En Revisión e historial; permite corregir o anular antes de que el técnico tome la orden. |
| Toma de trabajo | usp_TomarOrden, usp_LiberarOrden | Impide que dos técnicos tomen la misma orden; permite liberarla antes del diagnóstico. |
| Diagnóstico | usp_RegistrarDiagnostico, usp_CorregirDiagnostico, usp_RetirarDiagnostico | Exige al técnico responsable; pasa a Pendiente de Confirmación y limita correcciones a diagnósticos sin decisión del cliente. |
| Decisión del cliente | usp_RegistrarDecisionCliente, usp_CorregirDecisionCliente, usp_RetirarDecisionCliente | Registra aprobación o rechazo y actualiza el estado; permite corregir antes de reparar o entregar. |
| Reparación | usp_IniciarReparacion, usp_FinalizarReparacion | Exige diagnóstico y aprobación; registra el resultado Reparada o No Reparada con observaciones. |
| Servicios realizados | usp_RegistrarServicio, usp_CorregirServicio, usp_RetirarServicio | Trabaja durante la reparación y recalcula el total de la orden. |
| Consumo de repuestos | usp_ConsumirRepuesto, usp_CorregirConsumoRepuesto, usp_RetirarConsumoRepuesto | Descuenta, ajusta o devuelve existencias y recalcula el total en la misma transacción. |
| Inventario | usp_AjustarExistencias | Permite al administrador registrar una variación sin dejar stock negativo. |
| Cobro | usp_RegistrarPago, usp_CorregirPago, usp_AnularPago | Impide sobrepagos; una corrección conserva el pago anterior anulado y crea un reemplazo. |
| Entrega | usp_EntregarEquipo | Exige un resultado final y saldo cero; guarda fecha de entrega e historial. |

Los cuatro auxiliares usp_Interno_* validan al usuario, bloquean la orden, registran estados y recalculan el total. Son compartidos por las operaciones públicas; no deben llamarse desde los formularios ni recibir permisos directos de ejecución para la aplicación.

Las correcciones y anulaciones se conservan porque requieren reglas de negocio: por ejemplo, retirar un consumo debe devolver stock y recalcular el total, no simplemente borrar una fila.

## Distribución con la aplicación C#

La interfaz llama a BLL; BLL valida la sesión y coordina las operaciones; DAL ejecuta estos procedimientos con parámetros. Las validaciones de estado, inventario, pagos y las transacciones que protegen varias tablas se ejecutan también en SQL Server para mantener la consistencia ante accesos simultáneos.

El mantenimiento de roles, usuarios, clientes, equipos, tipos, servicios y catálogo de repuestos está implementado mediante los procedimientos `usp_Guardar*` y `usp_Consultar*`. Se incluyen consultas de órdenes, estados, expediente y pagos, autenticación, registro y consulta de errores. Las consultas de listados admiten búsqueda, paginación OFFSET/FETCH y total OUTPUT; @Pagina NULL conserva las consultas completas anteriores. Usuarios carga IdRol y usa ContrasenaHash. Los tres roles de permisos conservan sus nombres y estado activo; un rol nuevo no obtiene permisos implícitos. Entity, DAL y BLL se documentan en `AlDia.Solution/ARQUITECTURA.md`. No se debe conceder escritura directa sobre las tablas para sustituir las operaciones del flujo.

Los permisos del usuario SQL de la aplicación se definirán cuando se integre con estos procedimientos.

## Reglas de esta versión

- El técnico que toma la orden queda como responsable; el administrador no asigna órdenes.
- Los cambios de estado, la toma, la liberación y la anulación quedan en HistorialEstados. Los eventos sin transición pueden conservar el mismo estado anterior y nuevo.
- El total suma cantidad por precio de los servicios y repuestos; no puede superar el costo autorizado. Los cambios fallidos revierten tanto detalles como stock y total.
- Los pagos se registran después de finalizar el trabajo. Se permiten varios pagos sin superar el saldo.
- Se entregan órdenes Reparada, No Reparada o Rechazada con saldo cero. La entrega cierra cambios de detalles y pagos.
- Una decisión rechazada tiene CostoAprobado NULL. Una aprobada exige un importe no negativo.
- Una orden rechazada se entrega sin cobro porque todavía no tiene servicios realizados en este flujo. No se implementan anticipos, devoluciones, cobro separado de diagnóstico ni ampliación del presupuesto durante una reparación; esos casos requieren definir sus reglas.

## Ajustes del esquema que se conservan

Cédula del cliente obligatoria, no vacía y única; accesorios por recepción; técnico responsable; estados iniciales; bajas lógicas de clientes y equipos; anulación de órdenes y pagos; e índices de consulta.

Al actualizar el esquema original se conserva Equipos.Accesorios como dato legado, sin inventar los accesorios de visitas anteriores. Una instalación nueva usa solamente OrdenesReparacion.AccesoriosRecepcion. La migración conserva al autor del diagnóstico como responsable si la orden aún no tiene uno.

La cédula de los clientes es obligatoria y única. Este archivo no contiene migraciones para una base existente. Si la ejecución falla a mitad del script, las tablas ya creadas pueden permanecer y habrá que revisar el estado de la base antes de intentarlo de nuevo.

## Contrato de integración

- Usar CommandType.StoredProcedure y parámetros con tipos y tamaños explícitos. Las altas devuelven su identificador mediante INT OUTPUT; usarlo únicamente si la llamada finalizó correctamente.
- Enviar NULL explícitamente en campos opcionales. Las operaciones de corrección reemplazan los campos indicados en su firma.
- Tomar IdUsuarioActor de la sesión autenticada. Validar un identificador y un rol en SQL no autentica al operador: quien controle la conexión y pueda enviar otro identificador puede suplantarlo. La aplicación debe proteger su autenticación y credenciales.
- Ejecutar estos scripts no crea usuarios de negocio ni contraseñas predeterminadas. `usp_CrearAdministradorInicial` permite el registro explícito del primer administrador con un hash generado por BLL; solo funciona si la tabla Usuarios está vacía.
- Las operaciones de escritura del negocio usan transacción, XACT_ABORT, TRY/CATCH y THROW. Un error revierte toda la transacción activa, incluida una transacción externa iniciada por el cliente.
- Los errores funcionales usan 51000–51003; las restricciones pueden generar errores nativos de SQL Server. Los auxiliares no son una API pública.
- No entregar a la aplicación una conexión sysadmin ni db_owner. Las garantías del proceso se implementan en los procedimientos; no mediante triggers.

