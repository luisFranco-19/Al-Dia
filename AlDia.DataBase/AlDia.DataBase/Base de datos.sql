USE [master];
GO


--> 1. Validar si la base de datos existe 
IF DB_ID('AlDiaDB') IS NOT NULL
BEGIN
	ALTER DATABASE AlDiaDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE AlDiaDB
END
GO


CREATE DATABASE [AlDiaDB];
GO
USE [AlDiaDB];
GO


/* =========================================================
   1. ROLES
   ========================================================= */

CREATE TABLE dbo.Roles
(
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(200),
    Estado BIT NOT NULL DEFAULT 1
);

/* =========================================================
   2. USUARIOS
   ========================================================= */

CREATE TABLE dbo.Usuarios
(
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    IdRol INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Cedula VARCHAR(20) NOT NULL UNIQUE,
    Telefono VARCHAR(20),
    Correo VARCHAR(100),
    Usuario VARCHAR(50) NOT NULL UNIQUE,
    ContrasenaHash VARCHAR(255) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Usuarios_Roles
        FOREIGN KEY (IdRol)
        REFERENCES dbo.Roles(IdRol)
);

/* =========================================================
   2. CLIENTES
   ========================================================= */

CREATE TABLE dbo.Clientes
(
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20) NOT NULL,
    Cedula VARCHAR(20) NOT NULL UNIQUE,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT CK_Clientes_CedulaNoVacia CHECK (LEN(LTRIM(RTRIM(Cedula))) > 0)
);

/* =========================================================
   3. TIPOS DE EQUIPO
   ========================================================= */

CREATE TABLE dbo.TiposEquipo
(
    IdTipoEquipo INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(200),
    Estado BIT NOT NULL DEFAULT 1
);

/* =========================================================
   4. EQUIPOS
   ========================================================= */

CREATE TABLE dbo.Equipos
(
    IdEquipo INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL,
    IdTipoEquipo INT NOT NULL,

    Marca VARCHAR(50) NOT NULL,
    Modelo VARCHAR(100),
    NumeroSerie VARCHAR(100),
    Color VARCHAR(50),
    Observaciones VARCHAR(500),
    Estado BIT NOT NULL DEFAULT 1,

    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Equipos_Clientes
        FOREIGN KEY (IdCliente)
        REFERENCES Clientes(IdCliente),

    CONSTRAINT FK_Equipos_TiposEquipo
        FOREIGN KEY (IdTipoEquipo)
        REFERENCES TiposEquipo(IdTipoEquipo)
);

/* =========================================================
   5. ESTADOS DE REPARACIÓN
   ========================================================= */

CREATE TABLE dbo.EstadosReparacion
(
    IdEstado INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(200)
);

/* =========================================================
   6. ORDENES DE REPARACIÓN
   ========================================================= */

CREATE TABLE dbo.OrdenesReparacion
(
    IdOrden INT IDENTITY(1,1) PRIMARY KEY,
    NumeroOrden VARCHAR(20) NOT NULL UNIQUE,

    IdEquipo INT NOT NULL,
    IdRecepcionista INT NOT NULL,
    IdEstado INT NOT NULL,
    IdTecnicoResponsable INT NULL,

    FechaRecepcion DATETIME NOT NULL DEFAULT GETDATE(),

    ProblemaReportado VARCHAR(500) NOT NULL,
    ObservacionesRecepcion VARCHAR(500),
    AccesoriosRecepcion VARCHAR(300),

    FechaEntrega DATETIME NULL,
    Anulada BIT NOT NULL DEFAULT 0,

    Total DECIMAL(10,2) NOT NULL DEFAULT 0,

    CONSTRAINT FK_Ordenes_Equipos
        FOREIGN KEY (IdEquipo)
        REFERENCES Equipos(IdEquipo),

    CONSTRAINT FK_Ordenes_Recepcionista
        FOREIGN KEY (IdRecepcionista)
        REFERENCES Usuarios(IdUsuario),

    CONSTRAINT FK_Ordenes_Estados
        FOREIGN KEY (IdEstado)
        REFERENCES EstadosReparacion(IdEstado),

    CONSTRAINT FK_Ordenes_TecnicoResponsable
        FOREIGN KEY (IdTecnicoResponsable)
        REFERENCES Usuarios(IdUsuario),

    CONSTRAINT CK_Ordenes_Total
        CHECK (Total >= 0)
);

/* =========================================================
   7. DIAGNÓSTICOS
   ========================================================= */

CREATE TABLE dbo.Diagnosticos
(
    IdDiagnostico INT IDENTITY(1,1) PRIMARY KEY,

    IdOrden INT NOT NULL UNIQUE,
    IdTecnico INT NOT NULL,

    FechaDiagnostico DATETIME NOT NULL DEFAULT GETDATE(),

    ProblemaEncontrado VARCHAR(1000) NOT NULL,
    ReparacionPropuesta VARCHAR(1000) NOT NULL,

    CostoEstimado DECIMAL(10,2) NOT NULL DEFAULT 0,

    Observaciones VARCHAR(1000),

    CONSTRAINT FK_Diagnosticos_Ordenes
        FOREIGN KEY (IdOrden)
        REFERENCES OrdenesReparacion(IdOrden),

    CONSTRAINT FK_Diagnosticos_Tecnicos
        FOREIGN KEY (IdTecnico)
        REFERENCES Usuarios(IdUsuario),

    CONSTRAINT CK_Diagnosticos_Costo
        CHECK (CostoEstimado >= 0)
);

/* =========================================================
   8. CONFIRMACIONES DE REPARACIÓN
   ========================================================= */

CREATE TABLE dbo.ConfirmacionesReparacion
(
    IdConfirmacion INT IDENTITY(1,1) PRIMARY KEY,

    IdDiagnostico INT NOT NULL UNIQUE,
    IdUsuario INT NOT NULL,

    Decision VARCHAR(20) NOT NULL,
    CostoAprobado DECIMAL(10,2) NULL,

    FechaConfirmacion DATETIME NOT NULL DEFAULT GETDATE(),

    Observaciones VARCHAR(500),

    CONSTRAINT FK_Confirmaciones_Diagnosticos
        FOREIGN KEY (IdDiagnostico)
        REFERENCES Diagnosticos(IdDiagnostico),

    CONSTRAINT FK_Confirmaciones_Usuarios
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios(IdUsuario),

    CONSTRAINT CK_Confirmaciones_Decision
        CHECK (Decision IN ('APROBADA', 'RECHAZADA')),

    CONSTRAINT CK_Confirmaciones_CostoDecision
        CHECK ((Decision = 'APROBADA' AND CostoAprobado IS NOT NULL AND CostoAprobado >= 0)
            OR (Decision = 'RECHAZADA' AND CostoAprobado IS NULL))
);

/* =========================================================
   9. SERVICIOS
   ========================================================= */

CREATE TABLE dbo.Servicios
(
    IdServicio INT IDENTITY(1,1) PRIMARY KEY,

    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(300),

    PrecioBase DECIMAL(10,2) NOT NULL DEFAULT 0,

    Estado BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Servicios_Precio
        CHECK (PrecioBase >= 0)
);

/* =========================================================
   10. DETALLE DE SERVICIOS
   ========================================================= */

CREATE TABLE dbo.DetalleServicios
(
    IdDetalleServicio INT IDENTITY(1,1) PRIMARY KEY,

    IdOrden INT NOT NULL,
    IdServicio INT NOT NULL,

    Cantidad INT NOT NULL DEFAULT 1,
    Precio DECIMAL(10,2) NOT NULL,

    Observaciones VARCHAR(500),

    CONSTRAINT FK_DetalleServicios_Ordenes
        FOREIGN KEY (IdOrden)
        REFERENCES OrdenesReparacion(IdOrden),

    CONSTRAINT FK_DetalleServicios_Servicios
        FOREIGN KEY (IdServicio)
        REFERENCES Servicios(IdServicio),

    CONSTRAINT CK_DetalleServicios_Cantidad
        CHECK (Cantidad > 0),

    CONSTRAINT CK_DetalleServicios_Precio
        CHECK (Precio >= 0)
);

/* =========================================================
   11. REPUESTOS
   ========================================================= */

CREATE TABLE dbo.Repuestos
(
    IdRepuesto INT IDENTITY(1,1) PRIMARY KEY,

    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(300),
    Marca VARCHAR(50),
    NumeroParte VARCHAR(100),

    Stock INT NOT NULL DEFAULT 0,

    PrecioCompra DECIMAL(10,2) NOT NULL DEFAULT 0,
    PrecioVenta DECIMAL(10,2) NOT NULL DEFAULT 0,

    Estado BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Repuestos_Stock
        CHECK (Stock >= 0),

    CONSTRAINT CK_Repuestos_PrecioCompra
        CHECK (PrecioCompra >= 0),

    CONSTRAINT CK_Repuestos_PrecioVenta
        CHECK (PrecioVenta >= 0)
);

/* =========================================================
   12. DETALLE DE REPUESTOS
   ========================================================= */

CREATE TABLE dbo.DetalleRepuestos
(
    IdDetalleRepuesto INT IDENTITY(1,1) PRIMARY KEY,

    IdOrden INT NOT NULL,
    IdRepuesto INT NOT NULL,

    Cantidad INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_DetalleRepuestos_Ordenes
        FOREIGN KEY (IdOrden)
        REFERENCES OrdenesReparacion(IdOrden),

    CONSTRAINT FK_DetalleRepuestos_Repuestos
        FOREIGN KEY (IdRepuesto)
        REFERENCES Repuestos(IdRepuesto),

    CONSTRAINT CK_DetalleRepuestos_Cantidad
        CHECK (Cantidad > 0),

    CONSTRAINT CK_DetalleRepuestos_Precio
        CHECK (Precio >= 0)
);

/* =========================================================
   13. PAGOS
   ========================================================= */

CREATE TABLE dbo.Pagos
(
    IdPago INT IDENTITY(1,1) PRIMARY KEY,

    IdOrden INT NOT NULL,
    IdUsuario INT NOT NULL,

    Monto DECIMAL(10,2) NOT NULL,
    MetodoPago VARCHAR(30) NOT NULL,

    FechaPago DATETIME NOT NULL DEFAULT GETDATE(),

    Observaciones VARCHAR(300),
    Anulado BIT NOT NULL DEFAULT 0,
    FechaAnulacion DATETIME NULL,
    IdUsuarioAnulacion INT NULL,
    MotivoAnulacion VARCHAR(300),

    CONSTRAINT FK_Pagos_Ordenes
        FOREIGN KEY (IdOrden)
        REFERENCES OrdenesReparacion(IdOrden),

    CONSTRAINT FK_Pagos_Usuarios
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios(IdUsuario),

    CONSTRAINT FK_Pagos_UsuarioAnulacion
        FOREIGN KEY (IdUsuarioAnulacion)
        REFERENCES Usuarios(IdUsuario),

    CONSTRAINT CK_Pagos_Monto
        CHECK (Monto > 0),

    CONSTRAINT CK_Pagos_Metodo
        CHECK (MetodoPago IN ('Efectivo', 'Tarjeta', 'Transferencia'))
);

/* =========================================================
   14. HISTORIAL DE ESTADOS
   ========================================================= */

CREATE TABLE dbo.HistorialEstados
(
    IdHistorial INT IDENTITY(1,1) PRIMARY KEY,

    IdOrden INT NOT NULL,

    IdEstadoAnterior INT NULL,
    IdEstadoNuevo INT NOT NULL,

    IdUsuario INT NOT NULL,

    FechaCambio DATETIME NOT NULL DEFAULT GETDATE(),

    Observacion VARCHAR(500),

    CONSTRAINT FK_Historial_Ordenes
        FOREIGN KEY (IdOrden)
        REFERENCES OrdenesReparacion(IdOrden),

    CONSTRAINT FK_Historial_EstadoAnterior
        FOREIGN KEY (IdEstadoAnterior)
        REFERENCES EstadosReparacion(IdEstado),

    CONSTRAINT FK_Historial_EstadoNuevo
        FOREIGN KEY (IdEstadoNuevo)
        REFERENCES EstadosReparacion(IdEstado),

    CONSTRAINT FK_Historial_Usuarios
        FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios(IdUsuario)
);
/* =========================================================
   15. Log de Errores
   ========================================================= */
CREATE TABLE tblLogErrores(
    idLog INT IDENTITY(1,1) NOT NULL,
    mensajeError NVARCHAR(MAX) NOT NULL,
    numeroError INT,
    procedimiento NVARCHAR(100),
    lineaError INT,
    usuarioApp NVARCHAR(25) NOT NULL,
    fechaError DATETIME DEFAULT GETDATE(),

    CONSTRAINT [PK_tblLogErrores] PRIMARY KEY(idLog)
);
GO

-- Roles y Estados iniciales, e indices de consulta.
INSERT dbo.Roles(Nombre, Descripcion) VALUES
 ('Administrador','Control total y administracion del sistema'),
 ('Recepcionista','Recepcion de equipos y atencion al cliente'),
 ('Tecnico','Diagnostico y reparacion de equipos');

INSERT dbo.EstadosReparacion(Nombre, Descripcion) VALUES
 ('En Revisión','Orden disponible para diagnostico'),
 ('Pendiente de Confirmación','Espera la decision del cliente'),
 ('Aprobada','Reparacion autorizada por el cliente'),
 ('En Reparación','Trabajo tecnico en curso'),
 ('Reparada','Reparacion finalizada satisfactoriamente'),
 ('No Reparada','No se pudo completar la reparacion'),
 ('Rechazada','El cliente rechazo la propuesta'),
 ('Entregada','Equipo devuelto al cliente');

CREATE INDEX IX_Usuarios_IdRol ON dbo.Usuarios(IdRol);
CREATE INDEX IX_Equipos_IdCliente ON dbo.Equipos(IdCliente);
CREATE INDEX IX_Equipos_IdTipoEquipo ON dbo.Equipos(IdTipoEquipo);
CREATE INDEX IX_OrdenesReparacion_IdEquipo ON dbo.OrdenesReparacion(IdEquipo);
CREATE INDEX IX_OrdenesReparacion_IdEstado ON dbo.OrdenesReparacion(IdEstado);
CREATE INDEX IX_OrdenesReparacion_IdTecnicoResponsable ON dbo.OrdenesReparacion(IdTecnicoResponsable);
CREATE INDEX IX_DetalleServicios_IdOrden ON dbo.DetalleServicios(IdOrden);
CREATE INDEX IX_DetalleRepuestos_IdOrden ON dbo.DetalleRepuestos(IdOrden);
CREATE INDEX IX_Pagos_IdOrden ON dbo.Pagos(IdOrden);
CREATE INDEX IX_HistorialEstados_IdOrden ON dbo.HistorialEstados(IdOrden);



/*
  REVISAR LA DB,
  TRAER AVANCES DEL PROYECTO EN C#

*/