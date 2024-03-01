using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductosNegocio
    {
       
        public List<Productos> ListarProductosLimpio(string id = "")
        {
            List<Productos> Lista = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("SELECT  p.ProductoID, p.Nombre, p.PorcentajeGanancia,p.StockActual, p.StockMinimo, p.UrlImagen, p.TipoID, p.MarcaID, p.Estado FROM Productos p WHERE p.Estado = 0");
                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " and ProductoID = @Id";
                    datos.setearParametros("@Id", Convert.ToInt32(id));
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();

                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.Estado = (bool)datos.lector["Estado"];
                  
                    Lista.Add(aux);
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public List<Productos> ObtenerProductosConPrecio()
        {
            List<Productos> listaProductos = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT ProductoID, Nombre, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, UrlImagen, Estado FROM Productos WHERE Estado = 0";
                datos.SetearQuery(query);
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();
                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.ProductosXProveedores = new List<Producto_x_Proveedor>();
                    aux.ProductosXProveedores = MaxPrecio(aux.IdProductos);
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.Estado = (bool)datos.lector["Estado"];

                    listaProductos.Add(aux);
                }

                return listaProductos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        
        public Productos ObtenerProductoPorId(int idProducto)
        {
            Productos aux = new Productos();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "select ProductoID,Nombre,PorcentajeGanancia,StockActual,StockMinimo,MarcaID,TipoID,UrlImagen,Estado from Productos where ProductoID = @idProducto";
                datos.SetearQuery(query);
                datos.setearParametros("@idProducto", idProducto);
                datos.EjecutarLectura();
               if(datos.lector.Read())
                {
                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.Estado = (bool)datos.lector["Estado"];
                }
                    return aux;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
          
        }
        

        public List<Productos> ObtenerProductosPorNombre(string nombreProducto)
        {
            List<Productos> listaProductos = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT ProductoID, Nombre, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, UrlImagen, Estado FROM Productos WHERE Estado = 0 and Nombre LIKE @nombreProducto";
                datos.SetearQuery(query);
                datos.setearParametros("@nombreProducto", "%" + nombreProducto + "%");
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();
                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.Estado = (bool)datos.lector["Estado"];


                    listaProductos.Add(aux);
                }

                return listaProductos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Productos> ObtenerProductosConPrecioYnombre(string nombreProducto)
        {
            List<Productos> listaProductos = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT ProductoID, Nombre, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, UrlImagen, Estado FROM Productos WHERE Estado = 0 and Nombre LIKE @nombreProducto";
                datos.SetearQuery(query);
                datos.setearParametros("@nombreProducto", "%" + nombreProducto + "%");
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();
                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.ProductosXProveedores = new List<Producto_x_Proveedor>();
                    aux.ProductosXProveedores = MaxPrecio(aux.IdProductos);
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.Estado = (bool)datos.lector["Estado"];

                    listaProductos.Add(aux);
                }

                return listaProductos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Producto_x_Proveedor> MaxPrecio (int idPro)
        {

            AccesoDatos datos = new AccesoDatos();
            List<Producto_x_Proveedor> list = new List<Producto_x_Proveedor>();
            List<Producto_x_Proveedor> listaPrecio = new List<Producto_x_Proveedor>();

            try
            {
                datos.SetearQuery("SELECT ProductoID, ProveedorID, PrecioCompra, Estado FROM Producto_x_Proveedor WHERE @idPro = ProductoID");
                datos.setearParametros("@idPro", idPro);
                datos.EjecutarLectura();

                while(datos.lector.Read()){
                    Producto_x_Proveedor aux = new Producto_x_Proveedor();

                    aux.ProductoID = (int)datos.lector["ProductoID"];
                    aux.ProveedorID = (int)datos.lector["ProveedorID"];
                    aux.PrecioCompra = (decimal)datos.lector["PrecioCompra"];

                    list.Add(aux);
                }

                Producto_x_Proveedor Mayor = new Producto_x_Proveedor();
                Mayor.PrecioCompra = 0;
                foreach (Producto_x_Proveedor productos in list)
                {
                    if(productos.PrecioCompra > Mayor.PrecioCompra)
                    {
                        Mayor.PrecioCompra = productos.PrecioCompra;
                        Mayor.ProductoID = productos.ProductoID;    
                        Mayor.ProveedorID = productos.ProveedorID;                      
                    }
                }

                listaPrecio.Add( Mayor );

                return listaPrecio;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Productos> ObtenerProductosParaventa(string nombreProducto)
        {
            List<Productos> listaProductos = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT ProductoID, Nombre, PorcentajeGanancia, StockActual, StockMinimo, MarcaID, TipoID, UrlImagen, Estado FROM Productos WHERE Nombre LIKE @nombreProducto";
                datos.SetearQuery(query);
                datos.setearParametros("@nombreProducto", "%" + nombreProducto + "%");
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();
                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.ProductosXProveedores = new List<Producto_x_Proveedor>();
                    aux.ProductosXProveedores = MaxPrecio(aux.IdProductos);
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.Estado = (bool)datos.lector["Estado"];

                    listaProductos.Add(aux);
                }

                return listaProductos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public string ObtenerNombreProductoPorId(int idProducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT Nombre FROM Productos WHERE ProductoID = @idProducto";
                datos.SetearQuery(query);
                datos.setearParametros("@idProducto", idProducto);
                datos.EjecutarLectura();

                if (datos.lector.Read())
                {
                    return (string)datos.lector["Nombre"];
                }
                else
                {
                    // Manejo del caso en que no se encuentra el producto
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public List<Productos> ListarProductosPorProveedor(int IdProveedor, int? IdCategoria = null)
        {
            List<Productos> Lista = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Usar un marcador de posición para el parámetro

                string query = "select p.ProductoID,p.Nombre, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.MarcaID, p.TipoID, p.Estado,p.UrlImagen, pp.PrecioCompra from Productos p \r\ninner join Producto_x_Proveedor pp on pp.ProductoID = p.ProductoID\r\nwhere pp.ProveedorID = @IdProveedor and p.Estado = 0";

                // Verifica si se proporciona un ID de categoría y ajusta la consulta en consecuencia
                if (IdCategoria.HasValue)
                {
                    query += " AND p.TipoID = @IdCategoria";
                }

                datos.SetearQuery(query);

                // Establecer los valores de los parámetros
                datos.setearParametros("@IdProveedor", IdProveedor);
                if (IdCategoria.HasValue)
                {
                    datos.setearParametros("@IdCategoria", IdCategoria.Value);
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();

                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    if (aux.ProductosXProveedores == null)
                    {
                        // Si no ha sido inicializado, inicializar la lista
                        aux.ProductosXProveedores = new List<Producto_x_Proveedor>();
                    }

                    // Crear una instancia de Producto_x_Proveedor para cada registro en la tabla Producto_x_Proveedor
                    Producto_x_Proveedor productoXProveedor = new Producto_x_Proveedor();
                    productoXProveedor.PrecioCompra = Convert.ToDecimal(datos.lector["PrecioCompra"]);
                    productoXProveedor.ProveedorID = IdProveedor;
                    productoXProveedor.ProductoID = (int)datos.lector["ProductoID"];
                    aux.ProductosXProveedores.Add(productoXProveedor);
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.Estado = (bool)datos.lector["Estado"];
                    Lista.Add(aux);
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Productos> TodoslosProductosPorProveedor(int Id)
        {
            List<Productos> Lista = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Usar un marcador de posición para el parámetro

                datos.SetearQuery("SELECT p.ProductoID, p.Nombre, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.MarcaID, p.TipoID, p.Estado, p.UrlImagen, pxp.PrecioCompra, pxp.ProveedorID " +
                                 "FROM Productos p " +
                                 "INNER JOIN Producto_x_Proveedor pxp ON p.ProductoID = pxp.ProductoID " +
                                 "WHERE pxp.ProveedorID = @IdProveedor AND p.Estado = 0");

                datos.setearParametros("@IdProveedor", Id);
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();

                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    if (aux.ProductosXProveedores == null)
                    {
                        // Si no ha sido inicializado, inicializar la lista
                        aux.ProductosXProveedores = new List<Producto_x_Proveedor>();
                    }

                    // Crear una instancia de Producto_x_Proveedor para cada registro en la tabla Producto_x_Proveedor
                    Producto_x_Proveedor productoXProveedor = new Producto_x_Proveedor();
                    productoXProveedor.PrecioCompra = Convert.ToDecimal(datos.lector["PrecioCompra"]);
                    productoXProveedor.ProveedorID = Id;
                    productoXProveedor.ProductoID = (int)datos.lector["ProductoID"];
                    aux.ProductosXProveedores.Add(productoXProveedor);
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.Estado = (bool)datos.lector["Estado"];
                    Lista.Add(aux);
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public long AgregarProducto(Productos nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("insert into Productos (Nombre, PorcentajeGanancia, StockActual, StockMinimo, TipoID, MarcaID, UrlImagen, Estado) values (@Nombre, @PorcentajeGanancia, @StockActual,@StockMinimo, @TipoID, @MarcaID, @UrlImagen, @Estado); SELECT SCOPE_IDENTITY();");

                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@PorcentajeGanancia", nuevo.PorcentajeGanancia);
                    Datos.setearParametros("@StockActual", 0);
                    Datos.setearParametros("@StockMinimo", nuevo.StockMinimo);
                    Datos.setearParametros("@TipoID", nuevo.IdCategoria);
                    Datos.setearParametros("@MarcaID", nuevo.IdMarca);
                    Datos.setearParametros("@UrlImagen", nuevo.UrlImagen);
                    Datos.setearParametros("@Estado", 0);
                   
                 

                    long ID = Convert.ToInt64(Datos.ejecutarScalar());

                    // Asignar el ID generado al objeto 
                    nuevo.IdProductos = (int)ID;

                    return ID;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void Modificar(Productos nuevo)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetearQuery("UPDATE Productos SET Nombre = @nombre, PorcentajeGanancia=@PorcentajeGanancia,StockMinimo=@stockMinimo,MarcaID=@IdMarca,TipoID=@idCategoria, Estado=@Estado,UrlImagen=@urlImagen WHERE ProductoID=@IdProductos");
                Datos.setearParametros("@nombre", nuevo.Nombre);
                Datos.setearParametros("@IdProductos", nuevo.IdProductos);
                Datos.setearParametros("@PorcentajeGanancia",nuevo.PorcentajeGanancia);
                
                Datos.setearParametros("@stockMinimo",nuevo.StockMinimo);
                Datos.setearParametros("@IdMarca",nuevo.IdMarca);
                Datos.setearParametros("@IdCategoria",nuevo.IdCategoria);
                Datos.setearParametros("@Estado",nuevo.Estado);
                Datos.setearParametros("@urlImagen",nuevo.UrlImagen);
                Datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Datos.CerrarConexion();
            }
        }

        public void ModificarStock(int id, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearQuery("update Productos set StockActual += @cantidad where ProductoID = @id");
                datos.setearParametros("@id", id);
                datos.setearParametros("@cantidad", cantidad);
                datos.ejecutarAccion();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void ModificarStockVenta(int id, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearQuery("update Productos set StockActual -= @cantidad where ProductoID = @id");
                datos.setearParametros("@id", id);
                datos.setearParametros("@cantidad", cantidad);
                datos.ejecutarAccion();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }




        public void EliminarProducto(int IdProducto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Productos set estado=1 where ProductoID =@IdProducto");
                datos.setearParametros("@IdProducto", IdProducto);
                datos.ejecutarAccion();
                datos.SetearQuery("update Producto_x_Proveedor set estado=1 where ProductoID =@IdProducto");
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}

