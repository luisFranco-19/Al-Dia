# Contexto de trabajo de Al Día

El usuario pidió desarrollar el proyecto siguiendo el método de su maestro Mario Garcia, de Desarrollo de Aplicaciones III: arquitectura en capas y POO, según los materiales de Classroom «1. Crear proyecto y configuraciones iniciales» y «2. Gestión de roles».

Antes de tareas de implementación, consultar `METODO_TRABAJO_MAESTRO.md` para el análisis y las fuentes, y `AlDia.Solution/ARQUITECTURA.md` para los contratos y el estado documentado del proyecto. Verificar el código actual cuando el estado sea relevante.

- Mantener C# Windows Forms y los cuatro proyectos UI, BLL, DAL y Entity.
- El usuario aclaró que primero debemos completar Entity de todo el sistema, cubriendo todas las tablas, antes de avanzar a DAL y BLL. Respetar ese alcance por capa; la secuencia de cada módulo sigue siendo tabla/procedimientos → Entity → DAL → BLL → listado → registro/edición → integración y comprobación.
- Aplicar encapsulamiento en entidades, constructores para creación/reconstrucción y métodos que validen los cambios.
- Al implementar listados, seguir el patrón del maestro: formulario base reutilizable, herencia, métodos virtuales y overrides; búsqueda, paginación y cambio de estado.
- Reutilizar el diseño y los helpers; mantener los formularios editables en el diseñador de Visual Studio y tomar FrmConfiguracionConexion como referencia visual de Al Día.
- Delegar las operaciones de los formularios a BLL y ejecutar la persistencia del negocio en DAL mediante procedimientos almacenados parametrizados.
- Después de guardar o cambiar estado, actualizar el listado; los modales de guardado exitoso devuelven DialogResult.OK.
- Conservar las reglas y los contratos actuales de Al Día, incluidas la autenticación, los permisos, la sesión y la base de datos. HelpDesk enseña el patrón; sus tablas, roles y credenciales de demostración no son requisitos de Al Día.
- Priorizar código claro y explicable en clase; reutilizar las interfaces y la composición existentes y evitar introducir arquitecturas o dependencias innecesarias.
- Explicar las decisiones de implementación por capa y los principios de POO utilizados.

El estudio inicial de los videos solo agregó documentación; no autoriza por sí mismo a ejecutar scripts de recreación de la base ni a reestructurar todos los módulos.
