CREATE DATABASE SGInventarioDB;
GO

USE SGInventarioDB;
GO

--tabla usuarios--
CREATE TABLE Usuario (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);

--tabla productos--
CREATE TABLE Producto (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);

-- PA Listar Productos--
CREATE PROCEDURE sp_producto_listar
AS
BEGIN
    SELECT *
    FROM Producto
    WHERE Activo = 1
    ORDER BY FechaCreacion DESC;
END;
GO

--PA Obtener por ID--
CREATE PROCEDURE sp_producto_obtener_por_id
    @Id INT
AS
BEGIN
    SELECT *
    FROM Producto
    WHERE Id = @Id AND Activo = 1;
END;
GO
 
 --PA Crear producto--
CREATE PROCEDURE sp_producto_crear
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255),
    @Precio DECIMAL(18,2),
    @Stock INT
AS
BEGIN
    INSERT INTO Producto (Nombre, Descripcion, Precio, Stock)
    VALUES (@Nombre, @Descripcion, @Precio, @Stock);

    SELECT SCOPE_IDENTITY() AS Id;
END;
GO

--PA Actualizar producto--
CREATE PROCEDURE sp_producto_actualizar
    @Id INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(255),
    @Precio DECIMAL(18,2),
    @Stock INT
AS
BEGIN
    UPDATE Producto
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        Precio = @Precio,
        Stock = @Stock
    WHERE Id = @Id;
END;
GO

--PA Eliminar producto--
CREATE PROCEDURE sp_producto_eliminar
    @Id INT
AS
BEGIN
    UPDATE Producto
    SET Activo = 0
    WHERE Id = @Id;
END;
GO

--PA consultar usuario (login)--
CREATE PROCEDURE sp_usuario_obtener_por_username
    @Username NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM Usuario
    WHERE Username = @Username AND Activo = 1;
END;
GO

INSERT INTO Usuario (Username, PasswordHash, Role)
VALUES ('admin', '1234', 'Admin');

select * from Usuario

EXEC sp_producto_crear
    @Nombre = 'Laptop Dell',
    @Descripcion = 'Laptop empresarial 16GB RAM',
    @Precio = 850.00,
    @Stock = 10;

SELECT * FROM Producto;


