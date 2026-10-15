# Al Día

Sistema de escritorio para gestionar un taller de reparación de equipos electrónicos. Organiza la recepción de equipos, el diagnóstico, la autorización del cliente, la reparación, el consumo de repuestos, los pagos y la entrega.

El proyecto se desarrolla en C# con Windows Forms, SQL Server y una arquitectura de cuatro capas, aplicando programación orientada a objetos.

## Estado del proyecto

Las capas **Entity, DAL y BLL están implementadas** para todo el sistema. La base de datos contiene 16 tablas y 51 procedimientos almacenados: 47 públicos utilizados desde DAL y 4 auxiliares internos.

La UI cuenta con configuración de conexión, registro del primer administrador, sesión, dashboard y formularios conectados mediante herencia. Los listados y registros reutilizan formularios base con métodos virtuales, siguiendo las clases de roles y usuarios del profesor. Los indicadores del inicio, las acciones técnicas del expediente y los reportes todavía tienen integración pendiente. El alcance se detalla en [Metodología del profesor](AlDia.Solution/METODOLOGIA_PROFESOR.md).

## Módulos

| Módulo | Alcance implementado en las capas del negocio |
|---|---|
| Seguridad | Roles, usuarios, autenticación, sesión y permisos |
| Clientes | Registro, edición, consulta y cambio de estado |
| Equipos | Tipos de equipo, datos del equipo y relación con su cliente |
| Órdenes de reparación | Recepción, corrección, toma y liberación por el técnico, reparación, anulación y entrega |
| Diagnósticos | Problema encontrado, reparación propuesta, presupuesto y decisión del cliente |
| Servicios | Catálogo y trabajos realizados en una orden |
| Inventario | Catálogo de repuestos, existencias y consumos asociados a reparaciones |
| Pagos | Registro, corrección, anulación y consulta de pagos y saldos |
| Auditoría | Historial de estados y registro de errores SQL |

Los catálogos y listados del negocio disponen de consultas con búsqueda y paginación en SQL. El expediente de una orden reúne su cabecera, diagnóstico, confirmación, servicios, repuestos, pagos e historial.

## Arquitectura

```text
Formulario → BLL → DAL → Procedimiento almacenado → SQL Server
```

| Capa | Responsabilidad |
|---|---|
| **AlDia.Entity** | Entidades, solicitudes, resultados e interfaces de acceso a datos; protege los valores y cambios en memoria |
| **AlDia.DAL** | Comandos SQL explícitos por módulo, parámetros tipados, ejecución de procedimientos y reconstrucción de entidades |
| **AlDia.BLL** | Validaciones, sesión, permisos y operaciones del negocio |
| **AlDia.UI** | Formularios Windows Forms, presentación, eventos y navegación |

`AplicacionBll` compone los servicios y comparte una sesión de usuario. Los formularios de los módulos reciben BLL por constructor; las operaciones del negocio se persisten desde DAL. Las transacciones y las reglas de stock, estados y saldos se comprueban también en SQL Server.

La POO se aplica mediante propiedades encapsuladas, constructor vacío, constructores de alta y reconstrucción, métodos `AsignarId` y `Cambiar...`, herencia en `PersonaEntity`, `CatalogoEntity` y `DetalleOrdenEntity`, polimorfismo, interfaces y composición. DAL y los formularios preparan las entidades mediante esos métodos; las solicitudes validan los campos obligatorios antes de guardar.

## Roles y reglas del taller

- **Administrador:** mantiene roles, usuarios, tipos de equipo, servicios y repuestos; ajusta existencias y consulta la auditoría.
- **Recepcionista:** atiende clientes y equipos, registra recepciones y decisiones del cliente, gestiona pagos y entrega equipos. Clientes y equipos también pueden mantenerse desde administración.
- **Técnico:** toma órdenes disponibles y realiza el diagnóstico y la reparación de las órdenes de las que es responsable.

Un rol nuevo en el catálogo no recibe permisos automáticamente. Los tres roles del sistema conservan sus nombres y su estado activo. La autenticación rechaza usuarios o roles inactivos y nombres sin permisos configurados.

El flujo del taller sigue estas reglas:

1. La recepción inicia la orden en **En Revisión**.
2. Un técnico toma la orden y registra el diagnóstico; pasa a **Pendiente de Confirmación**.
3. Recepción registra la aprobación o el rechazo del cliente. La aprobación incluye el costo autorizado.
4. Una orden aprobada puede pasar a **En Reparación**. Los servicios y repuestos recalculan el total sin superar el importe autorizado.
5. El técnico registra el resultado **Reparada** o **No Reparada**.
6. Recepción registra los pagos sin superar el saldo y entrega el equipo cuando este llega a cero. Una orden rechazada puede entregarse sin iniciar la reparación.

El stock no puede quedar negativo. Las operaciones fallidas revierten los cambios relacionados. Corregir un pago conserva el original anulado y crea un reemplazo. Los cambios de estado quedan en el historial.

## Tecnologías

- C# y .NET 10.
- Windows Forms.
- SQL Server.
- Microsoft.Data.SqlClient 7.1.0.
- FontAwesome.Sharp 6.6.0.

## Estructura del repositorio

```text
Al-Dia/
├── AlDia.Solution/
│   ├── AlDia.Solution.slnx
│   ├── AlDia.Entity/
│   ├── AlDia.DAL/
│   ├── AlDia.BLL/
│   ├── AlDia.UI/
│   ├── ARQUITECTURA.md
│   └── DAL_BLL.md
├── AlDia.DataBase/
│   ├── AlDia.DataBase.slnx
│   └── AlDia.DataBase/
│       ├── Base de datos.sql
│       ├── Procedimientos_Almacenados.sql
│       └── LEEME.md
└── README.md
```

## Preparar y ejecutar

Se necesita Windows, el SDK de .NET 10 y una instancia de SQL Server. También se puede abrir `AlDia.Solution/AlDia.Solution.slnx` en Visual Studio con soporte para .NET 10 y desarrollo de escritorio.

### Base de datos

Para una instalación nueva:

1. Abrir [Base de datos.sql](AlDia.DataBase/AlDia.DataBase/Base%20de%20datos.sql) en SQL Server Management Studio y ejecutarlo.
2. Ejecutar completo [Procedimientos_Almacenados.sql](AlDia.DataBase/AlDia.DataBase/Procedimientos_Almacenados.sql).

**`Base de datos.sql` elimina y recrea `AlDiaDB` si ya existe. No debe ejecutarse para actualizar una base con datos.** Si el esquema ya está actualizado, ejecutar únicamente el archivo de procedimientos; utiliza `CREATE OR ALTER` y conserva los registros. El script incluye `USE [AlDiaDB]`.

El esquema incluye los tres roles y los ocho estados del flujo. No incluye usuarios de negocio ni contraseñas predeterminadas.

Para probar el sistema con registros ficticios, ejecutar después [Datos_Prueba.sql](AlDia.DataBase/AlDia.DataBase/Datos_Prueba.sql). Incluye clientes, equipos, inventario, órdenes en los ocho estados, pagos e historial. Crea las cuentas `admin.demo`, `recepcion.demo`, `tecnico.demo` y `tecnico2.demo`, con contraseña inicial `Demo2026!`. Se puede repetir sin duplicar los datos ni reiniciar las órdenes existentes. Los escenarios se detallan en [LEEME de la base de datos](AlDia.DataBase/AlDia.DataBase/LEEME.md).

### Aplicación

Desde la raíz del repositorio:

```powershell
dotnet restore AlDia.Solution/AlDia.Solution.slnx
dotnet build AlDia.Solution/AlDia.Solution.slnx
dotnet run --project AlDia.Solution/AlDia.UI/AlDia.UI.csproj
```

El proyecto de inicio es **AlDia.UI**. En el primer arranque se configura el servidor, la base de datos y las credenciales SQL desde el formulario de conexión. La configuración se guarda en `Config/configuracion.json`, junto al ejecutable.

Si todavía no existen usuarios, el login permite registrar el primer administrador. Después se inicia sesión con una cuenta del negocio. Esa cuenta es distinta del usuario SQL de la conexión. Las contraseñas del negocio se almacenan con PBKDF2 y una sal individual; sus hashes no se muestran en los listados.

## Criterios de desarrollo

El trabajo continúa por módulos, siguiendo tabla y procedimientos → Entity → DAL → BLL → listado → registro y edición → integración. Entity, DAL y BLL ya cubren el sistema; el avance siguiente corresponde a UI.

Los listados reutilizan `FrmListadoBase` mediante métodos virtuales y `override`, con búsqueda al escribir, paginación y acciones por módulo. Los registros reutilizan el `FrmRegistroBase` existente de Al Día, admiten alta y edición por constructor y llaman a `AgregarAsync` o `ActualizarAsync` según el modo. Devuelven `DialogResult.OK` al guardar. Se conserva el diseñador de Visual Studio y el estilo de los formularios existentes. La comparación con el código del profesor y las adaptaciones necesarias están documentadas en [Metodología del profesor](AlDia.Solution/METODOLOGIA_PROFESOR.md).

Los marcos comunes están en `Formularios/Base`: `FrmListadoBase` contiene cabecera, búsqueda, filtros, tabla y paginación; `FrmRegistroBase` contiene cabecera, cuerpo y pie. Los formularios concretos están en `Formularios/Hijos/<Modulo>`, con su propio `.cs`, `.Designer.cs` y `.resx` para editarlos desde Visual Studio.

Las carpetas de módulos son `Usuarios`, `Clientes`, `Equipos`, `OrdenesServicio`, `Inventario`, `Pagos`, `Reportes` y `Catalogos`. Órdenes incluye recepción y expediente; Inventario incluye repuestos y ajuste de existencias; Catálogos contiene roles, tipos de equipo y servicios. Reportes incluye un centro con pestañas para órdenes, pagos e inventario, filtros por período y un formulario de vista previa; generar, exportar e imprimir quedan deshabilitados hasta integrar los datos. Los botones del dashboard abren estos módulos, resaltan la sección activa y permiten volver a Inicio. Ver todas abre Órdenes de servicio. El detalle de las operaciones conectadas y pendientes está en la documentación de metodología.

## Documentación

- [Arquitectura, entidades y estado de la UI](AlDia.Solution/ARQUITECTURA.md).
- [Operaciones disponibles en DAL y BLL](AlDia.Solution/DAL_BLL.md).
- [Análisis de los videos, adaptación e instrucciones de verificación](AlDia.Solution/METODOLOGIA_PROFESOR.md).
- [Entidades y correspondencia con las tablas](AlDia.Solution/AlDia.Entity/LEEME.md).
- [Procedimientos y reglas de la base de datos](AlDia.DataBase/AlDia.DataBase/LEEME.md).
