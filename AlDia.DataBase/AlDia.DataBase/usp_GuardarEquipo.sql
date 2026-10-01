USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GuardarEquipo
 @IdUsuarioActor INT, @IdEquipo INT OUTPUT, @IdCliente INT, @IdTipoEquipo INT, @Marca VARCHAR(50), @Modelo VARCHAR(100), @NumeroSerie VARCHAR(100), @Color VARCHAR(50), @Observaciones VARCHAR(500), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador,Recepcionista';
IF @IdEquipo IS NULL OR @IdEquipo<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Marca)),'') IS NULL THROW 51000,'El campo Marca es obligatorio.',1;
SET @Marca=LTRIM(RTRIM(@Marca));
IF NOT EXISTS(SELECT 1 FROM dbo.Clientes WHERE IdCliente=@IdCliente AND Estado=1) OR NOT EXISTS(SELECT 1 FROM dbo.TiposEquipo WHERE IdTipoEquipo=@IdTipoEquipo AND Estado=1) THROW 51000,'Cliente o tipo de equipo inactivo o inexistente.',1;
IF EXISTS(SELECT 1 FROM dbo.Equipos e WHERE e.IdEquipo=@IdEquipo AND e.IdCliente<>@IdCliente) AND EXISTS(SELECT 1 FROM dbo.OrdenesReparacion WHERE IdEquipo=@IdEquipo) THROW 51000,'No puede cambiar el propietario de un equipo con ordenes registradas.',1;
IF @IdEquipo=0
BEGIN
 INSERT dbo.Equipos(IdCliente,IdTipoEquipo,Marca,Modelo,NumeroSerie,Color,Observaciones,Estado) VALUES(@IdCliente,@IdTipoEquipo,@Marca,@Modelo,@NumeroSerie,@Color,@Observaciones,@Estado);
 SET @IdEquipo=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.Equipos WITH(UPDLOCK,HOLDLOCK) WHERE IdEquipo=@IdEquipo) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.Equipos SET IdCliente=@IdCliente,IdTipoEquipo=@IdTipoEquipo,Marca=@Marca,Modelo=@Modelo,NumeroSerie=@NumeroSerie,Color=@Color,Observaciones=@Observaciones,Estado=@Estado WHERE IdEquipo=@IdEquipo;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO
