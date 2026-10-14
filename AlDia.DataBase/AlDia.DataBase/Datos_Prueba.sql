/*
DATOS DE PRUEBA PARA AL DIA
Ejecutar completo en SSMS despues de instalar el esquema y los procedimientos.
No ejecutar de nuevo "Base de datos.sql" sobre una base que ya contiene datos:
ese archivo elimina y recrea AlDiaDB.

Contenido: 4 usuarios, 12 clientes, 8 tipos, 12 equipos, 8 servicios,
10 repuestos y 12 ordenes con diagnosticos, decisiones, detalles, pagos e historial.
Las cedulas, series y numeros DEMO son ficticios.

Usuarios: admin.demo / recepcion.demo / tecnico.demo / tecnico2.demo
Clave de las cuatro cuentas: Demo2026!
Cada hash usa PBKDF2-SHA256, 600000 iteraciones y una sal individual,
con el formato que verifica HashContrasena.cs. Son cuentas solo para pruebas.

Reejecucion: inserta solamente registros ausentes; no reinicia contrasenas,
stock, pagos ni el avance de ordenes existentes. Las ordenes se reconocen
por NumeroOrden y se omiten completas si ya existen.
Los catalogos y cuentas se insertan para preparar el escenario; el flujo
de las ordenes se genera con los procedimientos publicos del proyecto.
Si una operacion falla, se revierte toda la carga de esta ejecucion.
*/
USE [AlDiaDB];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

IF @@TRANCOUNT <> 0
    THROW 51000, 'Ejecute el script sin una transaccion previa abierta.', 1;

BEGIN TRY
    BEGIN TRANSACTION;

    -- 1. Comprobar la instalacion necesaria.
    IF EXISTS
    (
        SELECT 1
        FROM (VALUES
            ('Roles'), ('Usuarios'), ('Clientes'), ('TiposEquipo'), ('Equipos'),
            ('EstadosReparacion'), ('Servicios'), ('Repuestos'), ('OrdenesReparacion'),
            ('Diagnosticos'), ('ConfirmacionesReparacion'), ('DetalleServicios'),
            ('DetalleRepuestos'), ('Pagos'), ('HistorialEstados')
        ) AS requeridas(Nombre)
        WHERE OBJECT_ID('dbo.' + requeridas.Nombre, 'U') IS NULL
    )
        THROW 51000, 'Faltan tablas. Instale el esquema del proyecto antes de cargar datos.', 1;

    IF EXISTS
    (
        SELECT 1
        FROM (VALUES
            ('usp_RegistrarRecepcion'), ('usp_TomarOrden'), ('usp_RegistrarDiagnostico'),
            ('usp_RegistrarDecisionCliente'), ('usp_IniciarReparacion'),
            ('usp_RegistrarServicio'), ('usp_ConsumirRepuesto'), ('usp_FinalizarReparacion'),
            ('usp_RegistrarPago'), ('usp_CorregirPago'), ('usp_EntregarEquipo'), ('usp_AnularOrden')
        ) AS requeridos(Nombre)
        WHERE OBJECT_ID('dbo.' + requeridos.Nombre, 'P') IS NULL
    )
        THROW 51000, 'Faltan procedimientos. Ejecute Procedimientos_Almacenados.sql.', 1;

    IF (SELECT COUNT(*) FROM dbo.Roles
        WHERE Nombre IN ('Administrador', 'Recepcionista', 'Tecnico') AND Estado = 1) <> 3
        THROW 51000, 'Deben existir los tres roles del sistema y estar activos.', 1;

    IF (SELECT COUNT(*) FROM dbo.EstadosReparacion
        WHERE Nombre IN (N'En Revisión', N'Pendiente de Confirmación', 'Aprobada',
            N'En Reparación', 'Reparada', 'No Reparada', 'Rechazada', 'Entregada')) <> 8
        THROW 51000, 'Faltan estados iniciales del flujo de reparacion.', 1;

    -- 2. Cuentas de prueba. Nunca sustituir cuentas existentes ni sus hashes.
    DECLARE @Usuarios TABLE
    (
        Usuario VARCHAR(50) PRIMARY KEY, Rol VARCHAR(50), Nombre VARCHAR(100),
        Apellido VARCHAR(100), Cedula VARCHAR(20), Telefono VARCHAR(20),
        Correo VARCHAR(100), ContrasenaHash VARCHAR(255)
    );

    INSERT INTO @Usuarios VALUES
        ('admin.demo', 'Administrador', 'Ana', 'Demo', 'DEMO-U001', '88880001',
         'admin.demo@example.com', 'PBKDF2-SHA256$600000$ymQtF246fiZ9F30rBbAOGQ==$wVyZ0yjzVXdfWz7g55kODTMe1p1kJYAAktKO+hRB3Jw='),
        ('recepcion.demo', 'Recepcionista', 'Laura', 'Demo', 'DEMO-U002', '88880002',
         'recepcion.demo@example.com', 'PBKDF2-SHA256$600000$nWobC7dKxIeaSF31qIbH8w==$MV0pO5QWhr1TEyuAUOJMOdK3y6E4xYUTwCnHCGHWQEQ='),
        ('tecnico.demo', 'Tecnico', 'Carlos', 'Demo', 'DEMO-U003', '88880003',
         'tecnico.demo@example.com', 'PBKDF2-SHA256$600000$5y+Iyr7M7OoefXPthbZTFQ==$U+yWRCDaSXK+YoPIglPuI+b0XnqZFLe4iYVclkun6RU='),
        ('tecnico2.demo', 'Tecnico', 'Luis', 'Demo', 'DEMO-U004', '88880004',
         'tecnico2.demo@example.com', 'PBKDF2-SHA256$600000$oSnlKyFihB2rxVayS0fLXg==$AMrCyI/KdTodKt/reIV7ybgMW9GPPh87wRfYBw1rMOc=');

    IF EXISTS
    (
        SELECT 1 FROM @Usuarios d JOIN dbo.Usuarios u
            ON u.Usuario = d.Usuario OR u.Cedula = d.Cedula
        JOIN dbo.Roles r ON r.IdRol = u.IdRol
        WHERE u.Usuario <> d.Usuario OR u.Cedula <> d.Cedula OR r.Nombre <> d.Rol
    )
        THROW 51000, 'Un usuario o cedula DEMO ya pertenece a otra cuenta. No se modifico.', 1;

    INSERT dbo.Usuarios
        (IdRol, Nombre, Apellido, Cedula, Telefono, Correo, Usuario, ContrasenaHash, Estado)
    SELECT r.IdRol, d.Nombre, d.Apellido, d.Cedula, d.Telefono, d.Correo,
        d.Usuario, d.ContrasenaHash, 1
    FROM @Usuarios d JOIN dbo.Roles r ON r.Nombre = d.Rol
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Usuarios u WHERE u.Usuario = d.Usuario);

    DECLARE @Recepcionista INT =
        (SELECT IdUsuario FROM dbo.Usuarios WHERE Usuario = 'recepcion.demo');

    -- 3. Clientes. Cedulas de prueba, sin usar identidades reales.
    DECLARE @Clientes TABLE
    (
        Cedula VARCHAR(20) PRIMARY KEY, Nombre VARCHAR(100),
        Apellido VARCHAR(100), Telefono VARCHAR(20)
    );
    INSERT INTO @Clientes VALUES
        ('DEMO-C001', 'Sofia', 'Lopez', '88881001'),
        ('DEMO-C002', 'Miguel', 'Hernandez', '88881002'),
        ('DEMO-C003', 'Valeria', 'Castro', '88881003'),
        ('DEMO-C004', 'Daniel', 'Martinez', '88881004'),
        ('DEMO-C005', 'Camila', 'Torres', '88881005'),
        ('DEMO-C006', 'Jose', 'Ramirez', '88881006'),
        ('DEMO-C007', 'Isabella', 'Morales', '88881007'),
        ('DEMO-C008', 'Andres', 'Flores', '88881008'),
        ('DEMO-C009', 'Gabriela', 'Perez', '88881009'),
        ('DEMO-C010', 'Fernando', 'Ruiz', '88881010'),
        ('DEMO-C011', 'Natalia', 'Gomez', '88881011'),
        ('DEMO-C012', 'Ricardo', 'Vargas', '88881012');

    INSERT dbo.Clientes (Nombre, Apellido, Telefono, Cedula, Estado)
    SELECT d.Nombre, d.Apellido, d.Telefono, d.Cedula, 1 FROM @Clientes d
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Clientes c WHERE c.Cedula = d.Cedula);

    -- 4. Tipos de equipo.
    DECLARE @Tipos TABLE (Nombre VARCHAR(50) PRIMARY KEY, Descripcion VARCHAR(200));
    INSERT INTO @Tipos VALUES
        ('Laptop (demo)', 'Computadora portatil'),
        ('Celular (demo)', 'Telefono inteligente'),
        ('Computadora (demo)', 'Computadora de escritorio'),
        ('Impresora (demo)', 'Impresora de tinta o laser'),
        ('Televisor (demo)', 'Televisor LED o LCD'),
        ('Audio (demo)', 'Parlante o equipo de sonido'),
        ('Router (demo)', 'Equipo de red inalambrica'),
        ('Monitor (demo)', 'Pantalla para computadora');

    INSERT dbo.TiposEquipo (Nombre, Descripcion, Estado)
    SELECT d.Nombre, d.Descripcion, 1 FROM @Tipos d
    WHERE NOT EXISTS (SELECT 1 FROM dbo.TiposEquipo t WHERE t.Nombre = d.Nombre);

    -- 5. Equipos. Cada numero de serie DEMO corresponde a un cliente.
    DECLARE @Equipos TABLE
    (
        NumeroSerie VARCHAR(100) PRIMARY KEY, CedulaCliente VARCHAR(20),
        Tipo VARCHAR(50), Marca VARCHAR(50), Modelo VARCHAR(100), Color VARCHAR(50)
    );
    INSERT INTO @Equipos VALUES
        ('DEMO-E001', 'DEMO-C001', 'Laptop (demo)', 'Lenovo', 'IdeaPad 3', 'Gris'),
        ('DEMO-E002', 'DEMO-C002', 'Celular (demo)', 'Samsung', 'Galaxy A52', 'Negro'),
        ('DEMO-E003', 'DEMO-C003', 'Laptop (demo)', 'HP', 'Pavilion 15', 'Plata'),
        ('DEMO-E004', 'DEMO-C004', 'Computadora (demo)', 'Dell', 'OptiPlex 7090', 'Negro'),
        ('DEMO-E005', 'DEMO-C005', 'Impresora (demo)', 'Epson', 'EcoTank L3250', 'Negro'),
        ('DEMO-E006', 'DEMO-C006', 'Televisor (demo)', 'LG', '43LM6300', 'Negro'),
        ('DEMO-E007', 'DEMO-C007', 'Celular (demo)', 'Xiaomi', 'Redmi Note 10', 'Azul'),
        ('DEMO-E008', 'DEMO-C008', 'Audio (demo)', 'Sony', 'MHC-V13', 'Negro'),
        ('DEMO-E009', 'DEMO-C009', 'Router (demo)', 'TP-Link', 'Archer C6', 'Negro'),
        ('DEMO-E010', 'DEMO-C010', 'Laptop (demo)', 'Acer', 'Aspire 5', 'Plata'),
        ('DEMO-E011', 'DEMO-C011', 'Impresora (demo)', 'Canon', 'PIXMA G3110', 'Negro'),
        ('DEMO-E012', 'DEMO-C012', 'Monitor (demo)', 'Dell', 'P2419H', 'Negro');

    -- NumeroSerie no tiene restriccion UNIQUE en el esquema; evitar ambiguedad.
    IF EXISTS
    (
        SELECT e.NumeroSerie FROM dbo.Equipos e JOIN @Equipos d
            ON d.NumeroSerie = e.NumeroSerie
        GROUP BY e.NumeroSerie HAVING COUNT(*) > 1
    )
        THROW 51000, 'Hay numeros de serie DEMO duplicados. Revise los equipos existentes.', 1;

    IF EXISTS
    (
        SELECT 1 FROM dbo.Equipos e JOIN @Equipos d ON d.NumeroSerie = e.NumeroSerie
        JOIN dbo.Clientes c ON c.IdCliente = e.IdCliente
        JOIN dbo.TiposEquipo t ON t.IdTipoEquipo = e.IdTipoEquipo
        WHERE c.Cedula <> d.CedulaCliente OR t.Nombre <> d.Tipo
    )
        THROW 51000, 'Una serie DEMO pertenece a otro cliente o tipo. No se modifico.', 1;

    INSERT dbo.Equipos
        (IdCliente, IdTipoEquipo, Marca, Modelo, NumeroSerie, Color, Observaciones, Estado)
    SELECT c.IdCliente, t.IdTipoEquipo, d.Marca, d.Modelo, d.NumeroSerie, d.Color,
        'Equipo ficticio para pruebas del sistema.', 1
    FROM @Equipos d
    JOIN dbo.Clientes c ON c.Cedula = d.CedulaCliente
    JOIN dbo.TiposEquipo t ON t.Nombre = d.Tipo
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Equipos e WHERE e.NumeroSerie = d.NumeroSerie);

    -- 6. Servicios. El ultimo queda inactivo para probar los filtros.
    DECLARE @Servicios TABLE
    (
        Nombre VARCHAR(100) PRIMARY KEY, Descripcion VARCHAR(300),
        PrecioBase DECIMAL(10,2), Estado BIT
    );
    INSERT INTO @Servicios VALUES
        ('Diagnostico (demo)', 'Revision y pruebas del equipo', 150.00, 1),
        ('Limpieza interna (demo)', 'Limpieza y cambio de pasta termica', 300.00, 1),
        ('Cambio de pantalla (demo)', 'Instalacion y prueba de pantalla', 400.00, 1),
        ('Cambio de bateria (demo)', 'Instalacion y calibracion de bateria', 250.00, 1),
        ('Reparacion electronica (demo)', 'Reparacion de placa y soldadura', 500.00, 1),
        ('Instalacion de SSD (demo)', 'Instalacion de unidad y sistema', 300.00, 1),
        ('Cambio de rodillos (demo)', 'Mantenimiento de alimentacion de papel', 150.00, 1),
        ('Servicio retirado (demo)', 'Ejemplo de servicio inactivo', 100.00, 0);

    INSERT dbo.Servicios (Nombre, Descripcion, PrecioBase, Estado)
    SELECT d.Nombre, d.Descripcion, d.PrecioBase, d.Estado FROM @Servicios d
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Servicios s WHERE s.Nombre = d.Nombre);

    -- 7. Inventario. Stock inicial solo para repuestos nuevos.
    DECLARE @Repuestos TABLE
    (
        NumeroParte VARCHAR(100) PRIMARY KEY, Nombre VARCHAR(100),
        Descripcion VARCHAR(300), Marca VARCHAR(50), Stock INT,
        PrecioCompra DECIMAL(10,2), PrecioVenta DECIMAL(10,2), Estado BIT
    );
    INSERT INTO @Repuestos VALUES
        ('DEMO-R001', 'SSD 480 GB (demo)', 'Unidad SATA de 2.5 pulgadas', 'Kingston', 15, 600.00, 850.00, 1),
        ('DEMO-R002', 'RAM 8 GB DDR4 (demo)', 'Memoria para laptop', 'Crucial', 12, 500.00, 750.00, 1),
        ('DEMO-R003', 'Bateria Redmi Note 10 (demo)', 'Bateria compatible', 'Generica', 10, 300.00, 450.00, 1),
        ('DEMO-R004', 'Pantalla laptop 15.6 (demo)', 'Panel de reemplazo', 'Generica', 6, 1200.00, 1800.00, 1),
        ('DEMO-R005', 'Capacitor 1000 uF (demo)', 'Capacitor para fuente de alimentacion', 'Generica', 50, 35.00, 80.00, 1),
        ('DEMO-R006', 'Rodillo Epson L3250 (demo)', 'Rodillo de alimentacion de papel', 'Generica', 8, 150.00, 250.00, 1),
        ('DEMO-R007', 'Ventilador laptop (demo)', 'Ejemplo con pocas existencias', 'Generica', 2, 120.00, 220.00, 1),
        ('DEMO-R008', 'Cable HDMI (demo)', 'Cable HDMI de 1.5 metros', 'Generica', 20, 80.00, 150.00, 1),
        ('DEMO-R009', 'Fusible 5 A (demo)', 'Ejemplo sin existencias', 'Generica', 0, 15.00, 40.00, 1),
        ('DEMO-R010', 'Cargador retirado (demo)', 'Ejemplo de repuesto inactivo', 'Generica', 1, 400.00, 650.00, 0);

    IF EXISTS
    (
        SELECT r.NumeroParte FROM dbo.Repuestos r JOIN @Repuestos d
            ON d.NumeroParte = r.NumeroParte
        GROUP BY r.NumeroParte HAVING COUNT(*) > 1
    )
        THROW 51000, 'Hay numeros de parte DEMO duplicados. Revise el inventario existente.', 1;

    INSERT dbo.Repuestos
        (Nombre, Descripcion, Marca, NumeroParte, Stock, PrecioCompra, PrecioVenta, Estado)
    SELECT d.Nombre, d.Descripcion, d.Marca, d.NumeroParte, d.Stock,
        d.PrecioCompra, d.PrecioVenta, d.Estado FROM @Repuestos d
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Repuestos r WHERE r.NumeroParte = d.NumeroParte);

    -- 8. Escenarios del taller. No dependen de valores IDENTITY fijos.
    -- En Revision sin tecnico: disponible; con tecnico: tomada sin diagnostico.
    -- ANULADA es un escenario: el estado sigue siendo En Revision y Anulada=1.
    -- ENTREGADA_RECHAZADA entrega un rechazo sin cobro.
    DECLARE @Ordenes TABLE
    (
        NumeroOrden VARCHAR(20) PRIMARY KEY, Serie VARCHAR(100), Destino VARCHAR(50),
        Tecnico VARCHAR(50), Problema VARCHAR(500), ProblemaEncontrado VARCHAR(1000),
        Propuesta VARCHAR(1000), Presupuesto DECIMAL(10,2),
        Servicio VARCHAR(100), Parte VARCHAR(100), CantidadRepuesto INT
    );
    INSERT INTO @Ordenes VALUES
        ('DEMO-001', 'DEMO-E001', N'En Revisión', NULL,
         'La laptop no enciende.', NULL, NULL, 0, NULL, NULL, 0),
        ('DEMO-002', 'DEMO-E002', N'En Revisión', 'tecnico.demo',
         'El celular tiene la pantalla rota.', NULL, NULL, 0, NULL, NULL, 0),
        ('DEMO-003', 'DEMO-E003', N'Pendiente de Confirmación', 'tecnico2.demo',
         'La pantalla muestra rayas.', 'Panel LCD deteriorado.',
         'Cambiar la pantalla y probar el equipo.', 2200.00, NULL, NULL, 0),
        ('DEMO-004', 'DEMO-E004', 'Aprobada', 'tecnico.demo',
         'Se apaga despues de varios minutos.', 'Acumulacion de polvo y temperatura elevada.',
         'Limpiar el interior y renovar la pasta termica.', 300.00, NULL, NULL, 0),
        ('DEMO-005', 'DEMO-E005', N'En Reparación', 'tecnico2.demo',
         'La impresora no toma el papel.', 'Rodillo de alimentacion desgastado.',
         'Cambiar el rodillo y hacer pruebas de impresion.', 450.00,
         'Cambio de rodillos (demo)', 'DEMO-R006', 1),
        ('DEMO-006', 'DEMO-E006', 'Reparada', 'tecnico.demo',
         'El televisor no enciende.', 'Dos capacitores danados en la fuente.',
         'Sustituir capacitores y verificar voltajes.', 800.00,
         'Reparacion electronica (demo)', 'DEMO-R005', 2),
        ('DEMO-007', 'DEMO-E007', 'Reparada', 'tecnico2.demo',
         'La bateria se descarga rapidamente.', 'Bateria con capacidad reducida.',
         'Cambiar la bateria y verificar la carga.', 800.00,
         'Cambio de bateria (demo)', 'DEMO-R003', 1),
        ('DEMO-008', 'DEMO-E008', 'No Reparada', 'tecnico.demo',
         'El equipo de sonido emite ruido.', 'Circuito amplificador averiado.',
         'Realizar pruebas; el repuesto principal no esta disponible.', 150.00,
         'Diagnostico (demo)', NULL, 0),
        ('DEMO-009', 'DEMO-E009', 'Rechazada', 'tecnico2.demo',
         'El router pierde la conexion WiFi.', 'Circuito inalambrico deteriorado.',
         'Reparar la etapa de radiofrecuencia.', 450.00, NULL, NULL, 0),
        ('DEMO-010', 'DEMO-E010', 'Entregada', 'tecnico.demo',
         'La laptop tarda mucho en iniciar.', 'Disco mecanico con sectores defectuosos.',
         'Instalar SSD y reinstalar el sistema.', 1200.00,
         'Instalacion de SSD (demo)', 'DEMO-R001', 1),
        ('DEMO-011', 'DEMO-E011', 'ANULADA', NULL,
         'Recepcion duplicada por error.', NULL, NULL, 0, NULL, NULL, 0),
        ('DEMO-012', 'DEMO-E012', 'ENTREGADA_RECHAZADA', 'tecnico2.demo',
         'El monitor no muestra imagen.', 'Panel danado; reparacion de alto costo.',
         'Reemplazar el panel completo.', 2500.00, NULL, NULL, 0);

    IF EXISTS
    (
        SELECT 1 FROM dbo.OrdenesReparacion o JOIN @Ordenes d
            ON d.NumeroOrden = o.NumeroOrden
        JOIN dbo.Equipos e ON e.IdEquipo = o.IdEquipo
        WHERE e.NumeroSerie <> d.Serie
    )
        THROW 51000, 'Un numero de orden DEMO pertenece a otro equipo. No se modifico.', 1;

    DECLARE @NumeroOrden VARCHAR(20), @Serie VARCHAR(100), @Destino VARCHAR(50),
        @UsuarioTecnico VARCHAR(50), @Problema VARCHAR(500), @Encontrado VARCHAR(1000),
        @Propuesta VARCHAR(1000), @Presupuesto DECIMAL(10,2),
        @NombreServicio VARCHAR(100), @NumeroParte VARCHAR(100), @Cantidad INT;

    DECLARE @IdOrden INT, @IdEquipo INT, @IdTecnico INT, @IdDiagnostico INT,
        @IdConfirmacion INT, @IdServicio INT, @IdRepuesto INT, @IdDetalle INT,
        @IdPago INT, @IdPagoNuevo INT, @Precio DECIMAL(10,2), @Total DECIMAL(10,2),
        @Reparada BIT, @Creadas INT = 0;

    DECLARE cursorOrdenes CURSOR LOCAL FAST_FORWARD FOR
        SELECT NumeroOrden, Serie, Destino, Tecnico, Problema,
            ProblemaEncontrado, Propuesta, Presupuesto, Servicio, Parte, CantidadRepuesto
        FROM @Ordenes ORDER BY NumeroOrden;
    OPEN cursorOrdenes;
    FETCH NEXT FROM cursorOrdenes INTO @NumeroOrden, @Serie, @Destino, @UsuarioTecnico,
        @Problema, @Encontrado, @Propuesta, @Presupuesto, @NombreServicio, @NumeroParte, @Cantidad;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM dbo.OrdenesReparacion WHERE NumeroOrden = @NumeroOrden)
        BEGIN
            SET @IdEquipo = (SELECT IdEquipo FROM dbo.Equipos WHERE NumeroSerie = @Serie);
            SET @IdTecnico = (SELECT IdUsuario FROM dbo.Usuarios WHERE Usuario = @UsuarioTecnico);

            EXEC dbo.usp_RegistrarRecepcion
                @IdUsuarioActor = @Recepcionista, @NumeroOrden = @NumeroOrden,
                @IdEquipo = @IdEquipo, @ProblemaReportado = @Problema,
                @ObservacionesRecepcion = 'Recepcion ficticia para pruebas.',
                @AccesoriosRecepcion = 'Sin accesorios adicionales.',
                @IdOrden = @IdOrden OUTPUT;

            IF @Destino = 'ANULADA'
                EXEC dbo.usp_AnularOrden @IdUsuarioActor = @Recepcionista,
                    @IdOrden = @IdOrden, @Motivo = 'Ejemplo de recepcion duplicada.';

            IF @UsuarioTecnico IS NOT NULL
                EXEC dbo.usp_TomarOrden @IdUsuarioActor = @IdTecnico, @IdOrden = @IdOrden;

            IF @UsuarioTecnico IS NOT NULL AND @Destino <> N'En Revisión'
            BEGIN
                EXEC dbo.usp_RegistrarDiagnostico
                    @IdUsuarioActor = @IdTecnico, @IdOrden = @IdOrden,
                    @ProblemaEncontrado = @Encontrado, @ReparacionPropuesta = @Propuesta,
                    @CostoEstimado = @Presupuesto, @Observaciones = 'Diagnostico de prueba.',
                    @IdDiagnostico = @IdDiagnostico OUTPUT;

                IF @Destino IN ('Rechazada', 'ENTREGADA_RECHAZADA')
                BEGIN
                    EXEC dbo.usp_RegistrarDecisionCliente
                        @IdUsuarioActor = @Recepcionista, @IdDiagnostico = @IdDiagnostico,
                        @Decision = 'RECHAZADA', @CostoAprobado = NULL,
                        @Observaciones = 'El cliente decide no reparar.',
                        @IdConfirmacion = @IdConfirmacion OUTPUT;

                    IF @Destino = 'ENTREGADA_RECHAZADA'
                        EXEC dbo.usp_EntregarEquipo @IdUsuarioActor = @Recepcionista,
                            @IdOrden = @IdOrden, @Observacion = 'Entrega sin reparacion ni cobro.';
                END
                ELSE IF @Destino <> N'Pendiente de Confirmación'
                BEGIN
                    EXEC dbo.usp_RegistrarDecisionCliente
                        @IdUsuarioActor = @Recepcionista, @IdDiagnostico = @IdDiagnostico,
                        @Decision = 'APROBADA', @CostoAprobado = @Presupuesto,
                        @Observaciones = 'El cliente autoriza el presupuesto de prueba.',
                        @IdConfirmacion = @IdConfirmacion OUTPUT;

                    IF @Destino <> 'Aprobada'
                    BEGIN
                        EXEC dbo.usp_IniciarReparacion @IdUsuarioActor = @IdTecnico, @IdOrden = @IdOrden;

                        SET @IdServicio = (SELECT IdServicio FROM dbo.Servicios WHERE Nombre = @NombreServicio);
                        SET @Precio = (SELECT PrecioBase FROM dbo.Servicios WHERE IdServicio = @IdServicio);
                        EXEC dbo.usp_RegistrarServicio
                            @IdUsuarioActor = @IdTecnico, @IdOrden = @IdOrden,
                            @IdServicio = @IdServicio, @Cantidad = 1, @Precio = @Precio,
                            @Observaciones = 'Mano de obra de prueba.',
                            @IdDetalleServicio = @IdDetalle OUTPUT;

                        IF @NumeroParte IS NOT NULL
                        BEGIN
                            SET @IdRepuesto = (SELECT IdRepuesto FROM dbo.Repuestos WHERE NumeroParte = @NumeroParte);
                            SET @Precio = (SELECT PrecioVenta FROM dbo.Repuestos WHERE IdRepuesto = @IdRepuesto);
                            EXEC dbo.usp_ConsumirRepuesto
                                @IdUsuarioActor = @IdTecnico, @IdOrden = @IdOrden,
                                @IdRepuesto = @IdRepuesto, @Cantidad = @Cantidad,
                                @Precio = @Precio, @IdDetalleRepuesto = @IdDetalle OUTPUT;
                        END

                        IF @Destino <> N'En Reparación'
                        BEGIN
                            SET @Reparada = CASE WHEN @Destino = 'No Reparada' THEN 0 ELSE 1 END;
                            EXEC dbo.usp_FinalizarReparacion
                                @IdUsuarioActor = @IdTecnico, @IdOrden = @IdOrden,
                                @Reparada = @Reparada, @Observaciones = 'Resultado tecnico de prueba.';
                            SET @Total = (SELECT Total FROM dbo.OrdenesReparacion WHERE IdOrden = @IdOrden);

                            -- DEMO-006 queda sin pagos: total 660, saldo 660.
                            -- DEMO-007 conserva un abono corregido: total 700, saldo 400.
                            IF @NumeroOrden = 'DEMO-007'
                            BEGIN
                                EXEC dbo.usp_RegistrarPago @IdUsuarioActor = @Recepcionista,
                                    @IdOrden = @IdOrden, @Monto = 250.00, @MetodoPago = 'Efectivo',
                                    @Observaciones = 'Abono con importe registrado por error.',
                                    @IdPago = @IdPago OUTPUT;
                                EXEC dbo.usp_CorregirPago @IdUsuarioActor = @Recepcionista,
                                    @IdPago = @IdPago, @Monto = 300.00, @MetodoPago = 'Efectivo',
                                    @Observaciones = 'Abono corregido.',
                                    @Motivo = 'El recibo de prueba indicaba 300.',
                                    @IdPagoNuevo = @IdPagoNuevo OUTPUT;
                            END

                            -- DEMO-008 queda lista para entrega: total 150, saldo 0.
                            IF @NumeroOrden = 'DEMO-008'
                                EXEC dbo.usp_RegistrarPago @IdUsuarioActor = @Recepcionista,
                                    @IdOrden = @IdOrden, @Monto = @Total, @MetodoPago = 'Transferencia',
                                    @Observaciones = 'Pago del diagnostico; reparacion no completada.',
                                    @IdPago = @IdPago OUTPUT;

                            -- DEMO-010 entregada: total 1150, dos pagos, saldo 0.
                            IF @Destino = 'Entregada'
                            BEGIN
                                EXEC dbo.usp_RegistrarPago @IdUsuarioActor = @Recepcionista,
                                    @IdOrden = @IdOrden, @Monto = 500.00, @MetodoPago = 'Efectivo',
                                    @Observaciones = 'Primer pago de prueba.',
                                    @IdPago = @IdPago OUTPUT;
                                SET @Precio = @Total - 500.00;
                                EXEC dbo.usp_RegistrarPago @IdUsuarioActor = @Recepcionista,
                                    @IdOrden = @IdOrden, @Monto = @Precio, @MetodoPago = 'Tarjeta',
                                    @Observaciones = 'Pago del saldo restante.',
                                    @IdPago = @IdPago OUTPUT;
                                EXEC dbo.usp_EntregarEquipo @IdUsuarioActor = @Recepcionista,
                                    @IdOrden = @IdOrden, @Observacion = 'Equipo probado y entregado al cliente.';
                            END
                        END
                    END
                END
            END

            SET @Creadas = @Creadas + 1;
        END

        FETCH NEXT FROM cursorOrdenes INTO @NumeroOrden, @Serie, @Destino, @UsuarioTecnico,
            @Problema, @Encontrado, @Propuesta, @Presupuesto, @NombreServicio, @NumeroParte, @Cantidad;
    END
    CLOSE cursorOrdenes;
    DEALLOCATE cursorOrdenes;

    COMMIT TRANSACTION;

    PRINT 'Carga completada. Ordenes nuevas: ' + CONVERT(VARCHAR(10), @Creadas);
    PRINT 'Clave inicial de las cuentas DEMO: Demo2026!';

    -- Resultado: cuentas disponibles y resumen para comparar con los escenarios.
    SELECT u.Usuario, r.Nombre AS Rol, u.Estado
    FROM dbo.Usuarios u JOIN dbo.Roles r ON r.IdRol = u.IdRol
    JOIN @Usuarios d ON d.Usuario = u.Usuario ORDER BY u.Usuario;

    SELECT o.NumeroOrden, c.Nombre + ' ' + c.Apellido AS Cliente,
        e.Marca + ' ' + COALESCE(e.Modelo, '') AS Equipo, er.Nombre AS Estado,
        o.Anulada, u.Usuario AS Tecnico, o.Total, COALESCE(p.Pagado, 0) AS Pagado,
        o.Total - COALESCE(p.Pagado, 0) AS Saldo, o.FechaEntrega
    FROM dbo.OrdenesReparacion o JOIN @Ordenes d ON d.NumeroOrden = o.NumeroOrden
    JOIN dbo.Equipos e ON e.IdEquipo = o.IdEquipo
    JOIN dbo.Clientes c ON c.IdCliente = e.IdCliente
    JOIN dbo.EstadosReparacion er ON er.IdEstado = o.IdEstado
    LEFT JOIN dbo.Usuarios u ON u.IdUsuario = o.IdTecnicoResponsable
    OUTER APPLY
        (SELECT SUM(Monto) AS Pagado FROM dbo.Pagos WHERE IdOrden = o.IdOrden AND Anulado = 0) p
    ORDER BY o.NumeroOrden;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    IF CURSOR_STATUS('local', 'cursorOrdenes') >= 0 CLOSE cursorOrdenes;
    IF CURSOR_STATUS('local', 'cursorOrdenes') >= -1 DEALLOCATE cursorOrdenes;
    THROW;
END CATCH;
GO

