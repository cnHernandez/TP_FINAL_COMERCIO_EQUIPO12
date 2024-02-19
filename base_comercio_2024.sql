use master
go
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
    Rubro NVARCHAR(50) NOT NULL, 
    Estado bit not null,
	UrlImagen  NVARCHAR(MAX) NULL
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
    Nombre NVARCHAR(50) NOT NULL,
	UrlImagen NVARCHAR(1000) NULL,
	Estado BIT NOT NULL
);
GO

-- Crear tabla de Productos
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    PorcentajeGanancia DECIMAL(5, 2) NOT NULL,
    StockActual INT NOT NULL,
    StockMinimo INT NOT NULL,
    MarcaID INT FOREIGN KEY REFERENCES Marcas(MarcaID) NOT NULL,
    TipoID INT FOREIGN KEY REFERENCES Tipos(TipoID) NOT NULL,
	UrlImagen NVARCHAR(MAX) NULL,
	Estado BIT NOT NULL
);
GO

-- Crear tabla de Compras
CREATE TABLE Compras (
    CompraID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ProveedorID INT FOREIGN KEY REFERENCES Proveedores(ProveedorID) NOT NULL,
    FechaCompra DATE NOT NULL,
    TotalCompra DECIMAL(10, 2) NOT NULL,
	Estado BIT NOT NULL
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
    TotalVenta DECIMAL(10, 2) NOT NULL,
	Estado BIT NOT NULL
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
--Para los distintos proveedores.

CREATE TABLE Producto_x_Proveedor(
	ProductoID INT NOT NULL,
	ProveedorID INT NOT NULL,
	PrecioCompra decimal (10,2) NOT NULL,
	Estado bit default(0) NOT NULL  ,
	PRIMARY KEY (ProductoID,ProveedorID ),
	FOREIGN KEY (ProveedorID ) REFERENCES Proveedores(ProveedorID),
	FOREIGN KEY (ProductoID) REFERENCES Productos (ProductoID)

);
GO
delete from Tipos
select * from Marcas
select * from Clientes
select * from Usuarios
select * from Ventas
select * from DetalleVenta
select * from Proveedores
select * from Producto_x_Proveedor


SELECT p.ProductoID, p.Nombre, p.PrecioCompra, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.UrlImagen, p.TipoID, p.MarcaID, p.ProveedorID, p.Estado FROM Productos p WHERE p.Estado = 0 AND p.ProveedorID = 1 AND p.TipoID = 2

INSERT INTO Tipos(Nombre, Estado, UrlImagen)VALUES('Gaseosas', 0, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHZz3m2YejL4st8KxTfODtwhZPhdlPkhyBhaHkIddYeA&s')
INSERT INTO Tipos(Nombre)VALUES('Snacks')
insert into Usuarios(NombreUsuario, Contraseña, TipoUsuario) VALUES('Nico', 'Hernandez', 1)
insert into Usuarios(NombreUsuario, Contraseña, TipoUsuario) VALUES('Luca', 'diba', 2)
insert into Marcas(Nombre, UrlImagen, Estado) VALUES('Coca Cola', 'https://lh3.googleusercontent.com/proxy/8sEMW-B5DXw5h0wcVv8CXteQqWb9GV2jY9gbp_gRDr5QQmEvsuI9gpGX8fHy42rap3QDa6SydoP7LYwMS7aolJIX9a8HfG2rGhVDRXPUUZbZve9kJ2sDlw0STdg', 0)
insert into Marcas(Nombre, UrlImagen, Estado) VALUES('Ferrero', 'https://sgfm.elcorteingles.es/SGFM/dctm/MEDIA03/202211/18/00120602500447____6__600x600.jpg', 0)
insert into Productos(Nombre, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, UrlImagen, Estado)VALUES('Coca', 50.50, 1000, 500, 1, 5, 'https://lh3.googleusercontent.com/proxy/8sEMW-B5DXw5h0wcVv8CXteQqWb9GV2jY9gbp_gRDr5QQmEvsuI9gpGX8fHy42rap3QDa6SydoP7LYwMS7aolJIX9a8HfG2rGhVDRXPUUZbZve9kJ2sDlw0STdg', 0)
insert into Productos(Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, UrlImagen, Estado)VALUES('Huevo Kinder', 100, 50.50, 1000, 500, 1, 2, 'https://camoga.ar/media/catalog/product/cache/17183a23c5d57b885c9e1f3d66234d68/5/0/50081000_huevo_kinder_con_leche_sorpresa_x20_gramos.jpg', 0)
-- Insertar productos de ejemplo con ProveedorID = 1 y Estado = 0
INSERT INTO Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, ProveedorID, UrlImagen, Estado)
VALUES ('Pizza Margarita', 9.99, 30.0, 50, 5, 2, 2, 1, 'pizza_margarita.jpg', 0);

INSERT INTO Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, ProveedorID, UrlImagen, Estado)
VALUES ('Hamburguesa Clásica', 5.99, 25.0, 80, 10, 2, 2, 1, 'hamburguesa_clasica.jpg', 0);

INSERT INTO Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, ProveedorID, UrlImagen, Estado)
VALUES ('Ensalada César', 7.99, 20.0, 30, 5,2, 2, 1, 'ensalada_cesar.jpg', 0);

INSERT INTO Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, ProveedorID, UrlImagen, Estado)
VALUES ('Ensalada rusa', 7.99, 20.0, 30, 5,2, 2, 1, 'ensalada_cesar.jpg', 0);

INSERT INTO Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, ProveedorID, UrlImagen, Estado)
VALUES ('Milanesa con pure', 10.99, 20.0, 30, 5,2, 2, 1, 'ensalada_cesar.jpg', 0);

INSERT INTO Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, ProveedorID, UrlImagen, Estado)
VALUES ('Milanesa napolitana', 10.99, 20.0, 30, 5,2, 2, 1, 'ensalada_cesar.jpg', 0);

INSERT INTO Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, ProveedorID, UrlImagen, Estado)
VALUES ('Milanesa a caballo', 10.99, 20.0, 30, 5,2, 2, 1, 'ensalada_cesar.jpg', 0);
-- Agrega más productos según sea necesario

/*
  public int IdProductos {  get; set; }
        public string Nombre {  get; set; }
        public decimal PrecioCompra {  get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public int StockActual {  get; set; }
        public int StockMinimo { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
        public bool Estado {  get; set; }
        public string UrlImagen { get; set; }
*/


select p.ProveedorID,p.Nombre,p.Rubro,
from Proveedores p where 
select p.Nombre, p.PrecioCompra, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.TipoID, p.MarcaID from Productos p where Estado = 0
UPDATE Productos SET Nombre = @nombre,PrecioCompra=@precioCompra, PorcentajeGanancia=@PorcentajeGanancia,StockActual=@StockActual,StockMinimo=@stockMinimo,MarcaID=@IdMarca,TipoID=@idCategoria,Estado=@Estado,UrlImagen=@urlImagen WHERE ProductoID=@idProductos

select VentaID,ClienteID,FechaVenta,TotalVenta from Ventas 

select CompraID,ProveedorID,FechaCompra,TotalCompra,Estado from Compras where Estado=0
INSERT INTO Ventas(ClienteID,FechaVenta,TotalVenta,Estado) VALUES (@ClienteID,@FechaVenta,@TotalVenta,@Estado)
insert into Compras (ProveedorID,FechaCompra,TotalCompra,Estado) values (@ProveedorID,@FechaCompra,@TotalCompra,@Estado)
UPDATE Compras SET proveedorid=@ProveedorID,FechaCompra=@FechaCompra,TotalCompra=@TotalCompra where CompraID=@CompraID

select p.ProductoID,p.Nombre, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.MarcaID, p.TipoID, p.Estado,p.UrlImagen, pp.PrecioCompra from Productos p 
inner join Producto_x_Proveedor pp on pp.ProductoID = p.ProductoID
where pp.ProveedorID = 1 and p.Estado = 0


SELECT p., p.Nombre, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.IdMarca, p.IdCategoria, p.Estado, p.UrlImagen, pxp.PrecioCompra 
                 FROM Productos p 
                 INNER JOIN Producto_x_Proveedor pxp ON p.IdProductos = pxp.IdProducto 
                 WHERE pxp.IdProveedor = 1 AND p.Estado = 0


