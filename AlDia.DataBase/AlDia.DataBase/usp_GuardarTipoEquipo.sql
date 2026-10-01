USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GuardarTipoEquipo
 @IdUsuarioActor INT, @IdTipoEquipo INT OUTPUT, @Nombre VARCHAR(50), @Descripcion VARCHAR(200), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
IF @IdTipoEquipo IS NULL OR @IdTipoEquipo<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL THROW 51000,'El campo Nombre es obligatorio.',1;
SET @Nombre=LTRIM(RTRIM(@Nombre));
IF @IdTipoEquipo=0
BEGIN
 INSERT dbo.TiposEquipo(Nombre,Descripcion,Estado) VALUES(@Nombre,@Descripcion,@Estado);
 SET @IdTipoEquipo=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.TiposEquipo WITH(UPDLOCK,HOLDLOCK) WHERE IdTipoEquipo=@IdTipoEquipo) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.TiposEquipo SET Nombre=@Nombre,Descripcion=@Descripcion,Estado=@Estado WHERE IdTipoEquipo=@IdTipoEquipo;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO
