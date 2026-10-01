USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_ConsultarClientes
 @IdUsuarioActor INT, @IdCliente INT=NULL, @SoloActivos BIT=1
AS
BEGIN
 SET NOCOUNT ON;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista,Tecnico';
SELECT IdCliente,Nombre,Apellido,Telefono,Cedula,Estado FROM dbo.Clientes WHERE (@IdCliente IS NULL OR IdCliente=@IdCliente) AND (@SoloActivos=0 OR Estado=1) ORDER BY IdCliente;
END;
GO
