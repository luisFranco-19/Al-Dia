# Al Día - Sistema de Gestión para Taller de Reparación de Equipos Electrónicos

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13.0-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-0078D6?logo=windows&logoColor=white)](https://learn.microsoft.com/dotnet/desktop/winforms/)
[![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC292B?logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server/)
[![Status](https://img.shields.io/badge/Estado-Completado-success)](#estado-del-proyecto)

**Al Día** es una solución integral de escritorio desarrollada en C# con Windows Forms y Microsoft SQL Server, diseñada para administrar de punta a punta las operaciones de un taller profesional de servicio técnico y reparación de dispositivos electrónicos (computadoras, smartphones, tablets, consolas y electrodomésticos).

El sistema controla todo el ciclo de vida del servicio: recepción de equipos, asignación a técnicos, emisión de diagnósticos, presupuestos y cotizaciones, autorización del cliente, ejecución de reparaciones, descargo de repuestos de inventario, mano de obra de servicios, control de caja y cobranza, recibos y entrega final de los dispositivos.

---

## Estado del Proyecto

El proyecto se encuentra **completamente desarrollado, integrado y funcional**. 

- **Capas Entity, DAL, BLL y UI al 100% integradas y operativas.**
- **Base de Datos:** 16 tablas relacionales y 52 procedimientos almacenados en SQL Server (48 de consumo público desde DAL y 4 auxiliares internos de seguridad y auditoría).
- **Interfaz de Usuario (UI):** Navegación fluida y adaptativa con control de acceso basado en roles (RBAC). Incorpora un panel central (Dashboard) con métricas en tiempo real, expedientes completos de órdenes, formularios modales heredados, control de inventario, catálogo general, módulo de pagos y generador de reportes con vista previa, exportación a PDF e impresión directa.
- **Suite de Pruebas y Verificación:** Incluye el proyecto `AlDia.Verificacion` para pruebas automatizadas de integridad y scripts de datos semilla para pruebas inmediatas.

---

## Módulos del Sistema

| Módulo | Alcance y Funcionalidades Principales |
|---|---|
| **Seguridad y Sesión (RBAC)** | Autenticación criptográfica segura (PBKDF2 con sal individual de 128 bits y 10,000 iteraciones). Control de acceso dinámico por roles (`Administrador`, `Recepcionista`, `Técnico`). Ocultamiento contextual de módulos y acciones según permisos. |
| **Dashboard Ejecutivo** | Pantalla principal adaptativa. Métricas dinámicas: ventas del mes, cobranzas totales y órdenes activas para administración y recepción; resumen de órdenes en diagnóstico, en reparación y listas para entrega para los técnicos. Listado en tiempo real de órdenes recientes con acceso directo al expediente. |
| **Clientes** | Registro, edición, validación de cédulas/teléfonos, historial de equipos asociados y búsqueda con paginación en servidor. |
| **Equipos** | Catálogo de tipos de equipo, vinculación con el cliente propietario, número de serie, marca, modelo y características técnicas. |
| **Órdenes de Reparación** | Recepción de equipos, asignación de técnico responsable, seguimiento del ciclo de vida, anulación justificada y entrega final de equipos con validación de saldo cero. |
| **Expediente de Orden** | Vista 360° de la orden de trabajo: cabecera del equipo/cliente, registro del diagnóstico técnico, presupuesto preliminar, registro de autorización/rechazo del cliente con costo máximo autorizado, servicios aplicados, repuestos consumidos, abonos realizados y bitácora histórica de estados. |
| **Diagnósticos y Cotización** | Registro de fallas encontradas, propuesta de solución, costo estimado de mano de obra y repuestos, y gestión de respuesta del cliente. |
| **Servicios** | Catálogo de servicios de mano de obra con tarifas base y registro de trabajos ejecutados en cada orden. |
| **Inventario y Repuestos** | Catálogo de repuestos con control de existencias en almacén, precios de costo y venta, número de parte y descargo transaccional al asociar piezas a una reparación. |
| **Pagos y Cobranzas** | Registro de abonos a órdenes, control de saldo pendiente, liquidación total, corrección de pagos con trazabilidad (reemplazo automático) y anulación con motivo obligatorio. |
| **Centro de Reportes** | Generación de reportes de Órdenes de Servicio, Pagos e Inventario por períodos de fecha. Incluye visor integrado con vista previa de documento, exportación nativa a **PDF** (`GeneradorPdfReporte`) e impresión física directa. |
| **Auditoría y Errores** | Trazabilidad completa de cambios de estado por orden con usuario responsable y fecha/hora. Registro automático de excepciones SQL en la tabla `LogErrores`. |

---

## Arquitectura del Software

El sistema implementa una arquitectura desacoplada en cuatro capas con orientación a objetos (POO), herencia, encapsulamiento, polimorfismo e inyección de dependencias mediante interfaces.

```text
┌────────────────────────────────────────────────────────┐
│               AlDia.UI (Windows Forms)                 │
│    Formularios Base, Listados, Registros, Reportes     │
└───────────────────────────┬────────────────────────────┘
                            │ Instancia AplicacionBll
                            ▼
┌────────────────────────────────────────────────────────┐
│               AlDia.BLL (Lógica de Negocio)            │
│   Validaciones, Control de Roles/Permisos, Sesión      │
└───────────────────────────┬────────────────────────────┘
                            │ Interfaces (IRolDal, IUsuarioDal, ...)
                            ▼
┌────────────────────────────────────────────────────────┐
│               AlDia.DAL (Acceso a Datos)               │
│  Comandos SQL Fuertemente Tipados, Paginación OFFSET   │
└───────────────────────────┬────────────────────────────┘
                            │ Parámetros y Procedimientos Almacenados
                            ▼
┌────────────────────────────────────────────────────────┐
│           SQL Server (AlDiaDB - 16 Tablas)             │
│   Transacciones ACID, Integridad Referencial y SPs     │
└────────────────────────────────────────────────────────┘
```

### Principios y Patrones Aplicados

1. **Herencia en Entidades:**
   - `PersonaEntity` (abstracta) → derivada en `ClienteEntity` y `UsuarioEntity`.
   - `CatalogoEntity` (abstracta) → derivada en `RolEntity`, `TipoEquipoEntity`, `ServicioEntity` y `RepuestoEntity`.
   - `DetalleOrdenEntity` (abstracta) → derivada en `DetalleServicioEntity` y `DetalleRepuestoEntity`.
2. **Herencia en Interfaz Gráfica (UI):**
   - `FrmListadoBase`: Base para todos los módulos de consulta con barra de búsqueda reactiva, filtros, cuadrícula de datos estilizada y paginación en SQL.
   - `FrmRegistroBase`: Base para formularios de captura con cabecera estandarizada, validaciones unificadas, eventos virtuales de guardado y estilo moderno sin bordes de ventana nativos.
3. **Inversión de Dependencias y Abstracción:**
   - La capa BLL depende exclusivamente de contratos/interfaces (`IClienteDal`, `IOrdenReparacionDal`, etc.), desacoplándola de la implementación concreta de base de datos.
4. **Seguridad y Criptografía:**
   - Hashing seguro de contraseñas con PBKDF2 (`Rfc2898DeriveBytes`), HMAC-SHA256 y sal aleatoria única por usuario.
   - No se almacenan contraseñas en texto plano ni se exponen hashes en consultas o modelos de datos.

---

## Flujo Operativo del Taller

```text
[Recepción del Equipo] ──────────► [En Revisión]
                                         │
                                         ▼ (Técnico asignado diagnostica)
[Pendiente de Confirmación] ◄─── [Registro de Diagnóstico y Costos]
         │
         ├───► (Cliente Rechaza) ──► [No Autorizada] ──► [Entrega al Cliente]
         │
         └───► (Cliente Aprueba) ──► [Aprobada]
                                         │
                                         ▼
                                  [En Reparación] (Consumo de repuestos y servicios)
                                         │
                                         ▼
                          ┌──────────────┴──────────────┐
                          ▼                             ▼
                    [Reparada]                   [No Reparada]
                          │                             │
                          └──────────────┬──────────────┘
                                         │
                                         ▼ (Registro de pagos hasta saldo 0)
                                    [Entregada]
```

1. **Recepción:** El recepcionista registra al cliente, ingresa el equipo con sus accesorios y motivo de ingreso, y asigna un técnico responsable. La orden pasa a **En Revisión**.
2. **Diagnóstico:** El técnico asignado evalúa el equipo, documenta la falla, la solución propuesta y el costo estimado. La orden pasa a **Pendiente de Confirmación**.
3. **Autorización:** Recepción consulta al cliente:
   - Si aprueba: se fija el monto máximo autorizado y pasa a **Aprobada**.
   - Si rechaza: la orden pasa a **No Autorizada** y puede devolverse de inmediato.
4. **Reparación:** El técnico pasa la orden a **En Reparación**, agrega los servicios realizados y los repuestos requeridos (validando existencias en stock y que el total no supere el presupuesto aprobado). Al finalizar, define el resultado: **Reparada** o **No Reparada**.
5. **Cobranza y Entrega:** Recepción registra abonos o liquidación total del saldo. Una vez el saldo llega a **$0.00**, se habilita la entrega formal del equipo (**Entregada**).

---

## Estructura del Repositorio

```text
Al-Dia/
├── AlDia.Solution/
│   ├── AlDia.Solution.slnx            # Solución principal de .NET 10
│   ├── AlDia.Entity/                  # Modelos de dominio, DTOs e interfaces
│   │   ├── Base/                      # Clases base abstractas (Persona, Catálogo, Detalle)
│   │   ├── Clientes/
│   │   ├── Equipos/
│   │   ├── Reparaciones/
│   │   ├── Inventario/
│   │   ├── Pagos/
│   │   └── Seguridad/
│   ├── AlDia.DAL/                     # Implementación de acceso a SQL Server
│   │   ├── Clientes/
│   │   ├── Equipos/
│   │   ├── Reparaciones/
│   │   ├── Inventario/
│   │   ├── Pagos/
│   │   └── Seguridad/
│   ├── AlDia.BLL/                     # Reglas del negocio, permisos y sesiones
│   │   ├── Common/                    # AplicacionBll, Sesión y validadores
│   │   └── [Módulos BLL]/
│   ├── AlDia.UI/                      # Presentación Windows Forms
│   │   ├── Controles/                 # Controles de usuario personalizados (PanelTarjeta, etc.)
│   │   ├── Helpers/                   # Asistentes de redondeo, moneda, validación
│   │   └── Formularios/
│   │       ├── Base/                  # FrmListadoBase, FrmRegistroBase, FrmOverlay
│   │       ├── Seguridad/             # FrmLogin, FrmAdministradorInicial, FrmConexion
│   │       └── Hijos/                 # Clientes, Equipos, Órdenes, Pagos, Inventario, Reportes, Usuarios
│   ├── AlDia.Verificacion/            # Suite de pruebas automatizadas y comprobación de integridad
│   ├── ARQUITECTURA.md                # Documento técnico de diseño y arquitectura
│   ├── DAL_BLL.md                     # Contratos detallados de DAL y BLL
│   └── METODOLOGIA_PROFESOR.md        # Correspondencia con lineamientos académicos
├── AlDia.DataBase/
│   ├── AlDia.DataBase.slnx
│   └── AlDia.DataBase/
│       ├── Base de datos.sql          # Creación del esquema completo (16 tablas, FKs, restricciones)
│       ├── Procedimientos_Almacenados.sql # 52 Procedimientos almacenados con lógica transaccional
│       ├── Datos_Prueba.sql           # Carga de datos de prueba y cuentas demo
│       └── LEEME.md                   # Documentación detallada de base de datos
└── README.md                          # Este archivo
```

---

## Tecnologías Utilizadas

- **Lenguaje:** C# 13.0
- **Plataforma:** .NET 10.0 (Windows Desktop SDK)
- **Framework de Presentación:** Windows Forms (.NET Windows Desktop)
- **Motor de Base de Datos:** Microsoft SQL Server 2019 / 2022 / LocalDB
- **Acceso a Datos:** `Microsoft.Data.SqlClient` (v7.1.1)
- **Librería de Iconografía:** `FontAwesome.Sharp` (v6.6.0)
- **Seguridad y Criptografía:** `System.Security.Cryptography.Pkcs` / PBKDF2 RFC 2898

---

## Guía de Instalación y Ejecución

### 1. Requisitos Previos

- Sistema Operativo: Windows 10 / Windows 11 (64-bit).
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) o superior instalado.
- Instancia activa de **Microsoft SQL Server** (LocalDB, Express o Enterprise).
- Herramienta para ejecutar scripts SQL: SQL Server Management Studio (SSMS), Azure Data Studio o extensión mssql en VS Code.

### 2. Configuración de la Base de Datos

Abra su gestor de SQL Server y ejecute los scripts ubicados en `AlDia.DataBase/AlDia.DataBase/` en el siguiente orden:

1. **`Base de datos.sql`**: Crea la base de datos `AlDiaDB`, las 16 tablas, relaciones, índices y catálogos iniciales.
   > [!WARNING]
   > `Base de datos.sql` elimina y recrea `AlDiaDB`. Ejecútelo únicamente para instalaciones nuevas.
2. **`Procedimientos_Almacenados.sql`**: Compila los 52 procedimientos almacenados requeridos por la capa DAL.
3. *(Opcional pero recomendado)* **`Datos_Prueba.sql`**: Carga registros de ejemplo (clientes, equipos, órdenes en diferentes estados del ciclo de vida, repuestos e historial) y genera las cuentas de prueba.

### 3. Cuentas de Acceso de Demostración

Si ejecutó `Datos_Prueba.sql`, el sistema cuenta con los siguientes usuarios listos para iniciar sesión:

| Rol | Usuario | Contraseña | Permisos Principales |
|---|---|---|---|
| **Administrador** | `admin.demo` | `Demo2026!` | Acceso total: Usuarios, Catálogos, Clientes, Equipos, Órdenes, Pagos, Inventario, Reportes y Auditoría. |
| **Recepcionista** | `recepcion.demo` | `Demo2026!` | Recepción de equipos, clientes, registro de decisiones del cliente, pagos, cobros y entrega de equipos. |
| **Técnico** | `tecnico.demo` | `Demo2026!` | Diagnóstico, cotización, registro de mano de obra/repuestos y resultado de reparaciones asignadas. |
| **Técnico 2** | `tecnico2.demo` | `Demo2026!` | Segundo técnico para pruebas de asignación cruzada y órdenes concurrentes. |

> [!NOTE]
> Si decide no ejecutar los datos de prueba, la primera vez que abra el sistema el formulario de inicio de sesión detectará que no existen usuarios y habilitará automáticamente el asistente para **Crear el Primer Administrador**.

### 4. Compilación y Ejecución

Desde la terminal (PowerShell o CMD) en la raíz del repositorio:

```powershell
# Compilar la solución completa
dotnet build AlDia.Solution/AlDia.Solution.slnx

# Iniciar la aplicación
dotnet run --project AlDia.Solution/AlDia.UI/AlDia.UI.csproj
```

También puede abrir el archivo de solución `AlDia.Solution/AlDia.Solution.slnx` directamente en **Visual Studio 2022 / 2025** y presionar **F5**.

### 5. Configuración de Conexión en el Primer Arranque

En la primera ejecución, la aplicación presentará la pantalla de **Configuración de Conexión a Base de Datos**:
- Ingrese el nombre de su servidor (ej. `localhost`, `.\SQLEXPRESS` o `(localdb)\MSSQLLocalDB`).
- Ingrese el nombre de la base de datos: `AlDiaDB`.
- Seleccione el tipo de autenticación (Autenticación de Windows o Autenticación de SQL Server con usuario y contraseña).
- Presione **Probar Conexión** y luego **Guardar**.

La configuración se almacenará de forma segura en `Config/configuracion.json` junto al ejecutable de la aplicación.

---

## Documentación Complementaria

Para obtener detalles técnicos en profundidad, consulte los documentos enlazados:

- 📐 [Documentación de Arquitectura y Patrones (ARQUITECTURA.md)](AlDia.Solution/ARQUITECTURA.md)
- 🔌 [Especificación de Operaciones en DAL y BLL (DAL_BLL.md)](AlDia.Solution/DAL_BLL.md)
- 🎓 [Alineación Metodológica y Verificación (METODOLOGIA_PROFESOR.md)](AlDia.Solution/METODOLOGIA_PROFESOR.md)
- 🗃️ [Manual del Esquema de Base de Datos y Procedimientos (LEEME.md)](AlDia.DataBase/AlDia.DataBase/LEEME.md)
- 📦 [Mapeo de Entidades y Modelos (AlDia.Entity/LEEME.md)](AlDia.Solution/AlDia.Entity/LEEME.md)

---

## Licencia y Créditos

Proyecto desarrollado como solución integral para la gestión de talleres de reparación electrónica, aplicando las mejores prácticas de la industria en arquitectura en capas, principios SOLID y patrones de diseño en el ecosistema .NET.
