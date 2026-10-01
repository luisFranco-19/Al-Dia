USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GuardarServicio
 @IdUsuarioActor INT, @IdServicio INT OUTPUT, @Nombre VARCHAR(100), @Descripcion VARCHAR(300), @PrecioBase DECIMAL(10,2), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
IF @IdServicio IS NULL OR @IdServicio<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL THROW 51000,'El campo Nombre es obligatorio.',1;
SET @Nombre=LTRIM(RTRIM(@Nombre));
IF @IdServicio=0
BEGIN
 INSERT dbo.Servicios(Nombre,Descripcion,PrecioBase,Estado) VALUES(@Nombre,@Descripcion,@PrecioBase,@Estado);
 SET @IdServicio=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.Servicios WITH(UPDLOCK,HOLDLOCK) WHERE IdServicio=@IdServicio) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.Servicios SET Nombre=@Nombre,Descripcion=@Descripcion,PrecioBase=@PrecioBase,Estado=@Estado WHERE IdServicio=@IdServicio;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO
