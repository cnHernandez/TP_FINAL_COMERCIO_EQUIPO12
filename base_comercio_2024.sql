-- Crear base de datos
CREATE DATABASE Base_Comercio_2024;
GO

-- Usar la base de datos
USE Base_Comercio_2024;
GO

-- Crear tabla de Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
	Apellido varchar(50),
	Mail varchar (100)
    -- Agrega otros campos según sea necesario
);
GO

-- Crear tabla de Proveedores
CREATE TABLE Proveedores (
    ProveedorID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Rubro NVARCHAR(50) -- Agregado el campo RUBRO
    -- Agrega otros campos según sea necesario
);
GO

-- Crear tabla de Marcas
CREATE TABLE Marcas (
    MarcaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50)
);
GO

-- Crear tabla de Tipos/Categorías
CREATE TABLE Tipos (
    TipoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50)
);
GO

-- Crear tabla de Productos
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    PrecioCompra DECIMAL(10, 2),
    PorcentajeGanancia DECIMAL(5, 2),
    StockActual INT,
    StockMinimo INT,
    MarcaID INT FOREIGN KEY REFERENCES Marcas(MarcaID),
    TipoID INT FOREIGN KEY REFERENCES Tipos(TipoID)
);
GO

-- Crear tabla de Asociación Producto-Proveedor
CREATE TABLE ProductoProveedor (
    ProductoProveedorID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT FOREIGN KEY REFERENCES Productos(ProductoID),
    ProveedorID INT FOREIGN KEY REFERENCES Proveedores(ProveedorID)
);
GO

-- Crear tabla de Compras
CREATE TABLE Compras (
    CompraID INT PRIMARY KEY IDENTITY(1,1),
    ProveedorID INT FOREIGN KEY REFERENCES Proveedores(ProveedorID),
    FechaCompra DATE,
    TotalCompra DECIMAL(10, 2)
);
GO

-- Crear tabla de Detalle de Compra
CREATE TABLE DetalleCompra (
    DetalleCompraID INT PRIMARY KEY IDENTITY(1,1),
    CompraID INT FOREIGN KEY REFERENCES Compras(CompraID),
    ProductoID INT FOREIGN KEY REFERENCES Productos(ProductoID),
    Cantidad INT,
    PrecioCompra DECIMAL(10, 2),
    Subtotal DECIMAL(10, 2)
);
GO

-- Crear tabla de Ventas
CREATE TABLE Ventas (
    VentaID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT FOREIGN KEY REFERENCES Clientes(ClienteID),
    FechaVenta DATE,
    TotalVenta DECIMAL(10, 2)
);
GO

-- Crear tabla de Detalle de Venta
CREATE TABLE DetalleVenta (
    DetalleVentaID INT PRIMARY KEY IDENTITY(1,1),
    VentaID INT FOREIGN KEY REFERENCES Ventas(VentaID),
    ProductoID INT FOREIGN KEY REFERENCES Productos(ProductoID),
    Cantidad INT,
    PrecioVenta DECIMAL(10, 2),
    Subtotal DECIMAL(10, 2)
);
GO

-- Crear tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(50),
    Contraseña NVARCHAR(50),
    TipoUsuario INT -- Cambiado a INT
);
GO