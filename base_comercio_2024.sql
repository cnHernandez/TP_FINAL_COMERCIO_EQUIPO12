drop database Base_Comercio_2024;
-- Crear base de datos
CREATE DATABASE Base_Comercio_2024;
GO

-- Usar la base de datos
USE Base_Comercio_2024;
GO

-- Crear tabla de Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
	Apellido varchar(50) NOT NULL,
	Mail varchar (100) NOT NULL,
	Dni BIGINT UNIQUE NOT NULL,
    Telefono BIGINT UNIQUE NOT NULL,
	Estado BIT NOT NULL
);
GO

-- Crear tabla de Proveedores
CREATE TABLE Proveedores (
    ProveedorID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Rubro NVARCHAR(50) NOT NULL -- Agregado el campo RUBRO
    -- Agrega otros campos según sea necesario
);
GO

-- Crear tabla de Marcas
CREATE TABLE Marcas (
    MarcaID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
	UrlImagen NVARCHAR(1000) NULL,
	Estado BIT NOT NULL
);
GO

-- Crear tabla de Tipos/Categorías
CREATE TABLE Tipos (
    TipoID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(50) NOT NULL
);
GO

-- Crear tabla de Productos
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    PrecioCompra DECIMAL(10, 2) NOT NULL,
    PorcentajeGanancia DECIMAL(5, 2) NOT NULL,
    StockActual INT NOT NULL,
    StockMinimo INT NOT NULL,
    MarcaID INT FOREIGN KEY REFERENCES Marcas(MarcaID) NOT NULL,
    TipoID INT FOREIGN KEY REFERENCES Tipos(TipoID) NOT NULL,
	UrlImagen NVARCHAR(1000) NULL,
	Estado BIT NOT NULL
);
GO


-- Crear tabla de Asociación Producto-Proveedor
CREATE TABLE ProductoProveedor (
    ProductoProveedorID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ProductoID INT FOREIGN KEY REFERENCES Productos(ProductoID) NOT NULL,
    ProveedorID INT FOREIGN KEY REFERENCES Proveedores(ProveedorID) NOT NULL
);
GO

-- Crear tabla de Compras
CREATE TABLE Compras (
    CompraID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ProveedorID INT FOREIGN KEY REFERENCES Proveedores(ProveedorID) NOT NULL,
    FechaCompra DATE NOT NULL,
    TotalCompra DECIMAL(10, 2) NOT NULL
);
GO

-- Crear tabla de Detalle de Compra
CREATE TABLE DetalleCompra (
    DetalleCompraID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CompraID INT FOREIGN KEY REFERENCES Compras(CompraID) NOT NULL,
    ProductoID INT FOREIGN KEY REFERENCES Productos(ProductoID) NOT NULL,
    Cantidad INT NOT NULL,
    PrecioCompra DECIMAL(10, 2) NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL
);
GO

-- Crear tabla de Ventas
CREATE TABLE Ventas (
    VentaID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ClienteID INT FOREIGN KEY REFERENCES Clientes(ClienteID) NOT NULL,
    FechaVenta DATE NOT NULL,
    TotalVenta DECIMAL(10, 2) NOT NULL
);
GO

-- Crear tabla de Detalle de Venta
CREATE TABLE DetalleVenta (
    DetalleVentaID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    VentaID INT FOREIGN KEY REFERENCES Ventas(VentaID) NOT NULL,
    ProductoID INT FOREIGN KEY REFERENCES Productos(ProductoID) NOT NULL,
    Cantidad INT NOT NULL,
    PrecioVenta DECIMAL(10, 2) NOT NULL,
    Subtotal DECIMAL(10, 2) NOT NULL
);
GO

-- Crear tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    NombreUsuario NVARCHAR(50) NOT NULL,
    Contraseña NVARCHAR(50) NOT NULL,
    TipoUsuario INT NOT NULL -- Cambiado a INT
);
GO


select * from Clientes
select * from Usuarios
select * from Marcas

insert into Usuarios(NombreUsuario, Contraseña, TipoUsuario) VALUES('Nico', 'Hernandez', 1)
insert into Marcas(Nombre, UrlImagen, Estado) VALUES('Arcor', 'https://upload.wikimedia.org/wikipedia/commons/9/9d/Arcor_textlogo.png', 0)
insert into Marcas(Nombre, UrlImagen, Estado) VALUES('Ferrero', 'https://sgfm.elcorteingles.es/SGFM/dctm/MEDIA03/202211/18/00120602500447____6__600x600.jpg', 0)

