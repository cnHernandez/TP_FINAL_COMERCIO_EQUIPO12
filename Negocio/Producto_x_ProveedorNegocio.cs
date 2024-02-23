using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class Producto_x_ProveedorNegocio
    {
        public List<Producto_x_Proveedor> ListarProductos_x_Proveedor(string id = "")
        {
            List<Producto_x_Proveedor> Lista = new List<Producto_x_Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select ProductoID,ProveedorID,PrecioCompra from Producto_x_Proveedor where @Id = ProveedorID");
               
                    
                    datos.setearParametros("@Id", Convert.ToInt32(id));
               

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Producto_x_Proveedor aux = new Producto_x_Proveedor();

                    aux.ProductoID = (int)datos.lector["ProductoID"];
                    aux.ProveedorID = (int)datos.lector["ProveedorID"];
                    aux.PrecioCompra = (decimal)datos.lector["PrecioCompra"];
                 
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

        public void AgregarProducto_x_Proveedor(int idProducto, int idProveedor, decimal precioCompra)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Define la consulta SQL para insertar un nuevo registro en la tabla Producto_x_Proveedor
                datos.SetearQuery("INSERT INTO Producto_x_Proveedor (ProductoID, ProveedorID, PrecioCompra) VALUES (@ProductoID, @ProveedorID, @PrecioCompra)");

                // Establece los parámetros de la consulta SQL
                datos.setearParametros("@ProductoID", idProducto);
                datos.setearParametros("@ProveedorID", idProveedor);
                datos.setearParametros("@PrecioCompra", precioCompra);

                // Ejecuta la consulta SQL
                datos.EjecutarLectura();
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

        public void updatearPrecios (List<Producto_x_Proveedor> lista)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                foreach (Producto_x_Proveedor producto in lista)
                {
                    datos.SetearQuery("update Producto_x_Proveedor set PrecioCompra=@precioCompra where ProductoID=@productoID and ProveedorID=@proveedorID");
                    datos.setearParametros("@precioCompra", producto.PrecioCompra);
                    datos.setearParametros("@productoID", producto.ProductoID);
                    datos.setearParametros("@proveedorID", producto.ProveedorID);
                    datos.ejecutarAccion();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }


    }
}
