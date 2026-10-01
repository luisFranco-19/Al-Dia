USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_RegistrarError
 @MensajeError NVARCHAR(MAX), @NumeroError INT, @Procedimiento NVARCHAR(100), @LineaError INT, @UsuarioApp NVARCHAR(25)
AS
BEGIN
 SET NOCOUNT ON;
INSERT dbo.tblLogErrores(mensajeError,numeroError,procedimiento,lineaError,usuarioApp) VALUES(@MensajeError,@NumeroError,@Procedimiento,@LineaError,@UsuarioApp);
END;
GO
