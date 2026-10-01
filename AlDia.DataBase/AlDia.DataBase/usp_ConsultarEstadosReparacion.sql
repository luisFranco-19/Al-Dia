USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarEstadosReparacion
 @IdUsuarioActor INT
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT IdEstado,Nombre,Descripcion FROM dbo.EstadosReparacion ORDER BY IdEstado;
END;
GO
