USE [AlDiaDB];
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO
CREATE OR ALTER PROCEDURE dbo.usp_GuardarRepuesto
 @IdUsuarioActor INT, @IdRepuesto INT OUTPUT, @Nombre VARCHAR(100), @Descripcion VARCHAR(300), @Marca VARCHAR(50), @NumeroParte VARCHAR(100), @PrecioCompra DECIMAL(10,2), @PrecioVenta DECIMAL(10,2), @Estado BIT
AS
BEGIN
 SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
EXEC dbo.usp_Interno_ValidarUsuario @IdUsuarioActor,'Administrador';
IF @IdRepuesto IS NULL OR @IdRepuesto<0 THROW 51000,'Identificador invalido.',1;
IF NULLIF(LTRIM(RTRIM(@Nombre)),'') IS NULL THROW 51000,'El campo Nombre es obligatorio.',1;
SET @Nombre=LTRIM(RTRIM(@Nombre));
IF @IdRepuesto=0
BEGIN
 INSERT dbo.Repuestos(Nombre,Descripcion,Marca,NumeroParte,PrecioCompra,PrecioVenta,Estado) VALUES(@Nombre,@Descripcion,@Marca,@NumeroParte,@PrecioCompra,@PrecioVenta,@Estado);
 SET @IdRepuesto=CONVERT(INT,SCOPE_IDENTITY());
END
ELSE
BEGIN
 IF NOT EXISTS(SELECT 1 FROM dbo.Repuestos WITH(UPDLOCK,HOLDLOCK) WHERE IdRepuesto=@IdRepuesto) THROW 51000,'Registro inexistente.',1;
 UPDATE dbo.Repuestos SET Nombre=@Nombre,Descripcion=@Descripcion,Marca=@Marca,NumeroParte=@NumeroParte,PrecioCompra=@PrecioCompra,PrecioVenta=@PrecioVenta,Estado=@Estado WHERE IdRepuesto=@IdRepuesto;
END
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 THROW;
END CATCH;
END;
GO
