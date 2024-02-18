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



    }
}
