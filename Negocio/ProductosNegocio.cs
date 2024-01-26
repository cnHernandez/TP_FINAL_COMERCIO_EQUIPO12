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
        public List<Productos> ListarProductos(string id = "")
        {
            List<Productos> Lista = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select p.ProductoID, p.Nombre, p.PrecioCompra, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.UrlImagen, p.TipoID, p.MarcaID, p.ProveedorID, p.Estado from Productos p where Estado = 0");
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
                    aux.PrecioCompra = (decimal)datos.lector["PrecioCompra"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.IdProveedor = (int)datos.lector["ProveedorID"];
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

        public List<Productos> ListarProductosPorProveedor(int Id)
        {
            List<Productos> Lista = new List<Productos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Usar un marcador de posición para el parámetro
                string query = "select p.ProductoID, p.Nombre, p.PrecioCompra, p.PorcentajeGanancia, p.StockActual, p.StockMinimo, p.UrlImagen, p.TipoID, p.MarcaID, p.ProveedorID, p.Estado from Productos p where Estado = 0 and p.ProveedorID = @Id";
                datos.SetearQuery(query);

                // Establecer el valor del parámetro
                datos.setearParametros("@Id", Id);

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Productos aux = new Productos();

                    aux.IdProductos = (int)datos.lector["ProductoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.PrecioCompra = (decimal)datos.lector["PrecioCompra"];
                    aux.PorcentajeGanancia = (decimal)datos.lector["PorcentajeGanancia"];
                    aux.StockActual = (int)datos.lector["StockActual"];
                    aux.StockMinimo = (int)datos.lector["StockMinimo"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.IdMarca = (int)datos.lector["MarcaID"];
                    aux.IdProveedor = (int)datos.lector["ProveedorID"];
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
                    Datos.SetearQuery("insert into Productos (Nombre, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, TipoID, MarcaID, ProveedorID, UrlImagen, Estado) values (@Nombre, @PrecioCompra, @PorcentajeGanancia, @StockActual,@StockMinimo, @TipoID, @MarcaID, @ProveedorID, @UrlImagen, @Estado); SELECT SCOPE_IDENTITY();");

                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@PrecioCompra", nuevo.PrecioCompra);
                    Datos.setearParametros("@PorcentajeGanancia", nuevo.PorcentajeGanancia);
                    Datos.setearParametros("@StockActual", nuevo.StockActual);
                    Datos.setearParametros("@StockMinimo", nuevo.StockMinimo);
                    Datos.setearParametros("@TipoID", nuevo.IdCategoria);
                    Datos.setearParametros("@MarcaID", nuevo.IdMarca);
                    Datos.setearParametros("@ProveedorID", nuevo.IdProveedor);
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
                Datos.SetearQuery("UPDATE Productos SET Nombre = @nombre,PrecioCompra=@precioCompra, PorcentajeGanancia=@PorcentajeGanancia,StockActual=@StockActual,StockMinimo=@stockMinimo,MarcaID=@IdMarca,TipoID=@idCategoria, ProveedorID=@IdProveedor, Estado=@Estado,UrlImagen=@urlImagen WHERE ProductoID=@IdProductos");
                Datos.setearParametros("@nombre", nuevo.Nombre);
                Datos.setearParametros("@IdProductos", nuevo.IdProductos);
                Datos.setearParametros("@precioCompra",nuevo.PrecioCompra);
                Datos.setearParametros("@PorcentajeGanancia",nuevo.PorcentajeGanancia);
                Datos.setearParametros("@StockActual",nuevo.StockActual);
                Datos.setearParametros("@stockMinimo",nuevo.StockMinimo);
                Datos.setearParametros("@IdMarca",nuevo.IdMarca);
                Datos.setearParametros("@IdCategoria",nuevo.IdCategoria);
                Datos.setearParametros("@IdProveedor", nuevo.IdProveedor);
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
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetearQuery("update Productos set StockActual += @cantidad where ProductoID = @id");
                datos.setearParametros("@id", id);
                datos.setearParametros("@cantidad", cantidad);
                datos.ejecutarAccion();
            }
            catch (Exception Ex)
            {
                throw Ex;
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

