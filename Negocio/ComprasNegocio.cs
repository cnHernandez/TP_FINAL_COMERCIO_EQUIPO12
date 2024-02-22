using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ComprasNegocio
    {
        public List<Compras> ListarVentas(int id = 0)
        {
            List<Compras> Lista = new List<Compras>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select CompraID, ProveedorID, FechaCompra, TotalCompra, Estado from Compras where Estado = 0");
                if (id != 0)
                {
                    datos.Comando.CommandText += " and CompraID = @Id";
                    datos.setearParametros("@Id", id);
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Compras aux = new Compras();
                    aux.IdCompras = (int)datos.lector["CompraID"];
                    aux.IdProveedor = (int)datos.lector["ProveedorID"];
                    aux.FechaCompra = (DateTime)datos.lector["FechaCompra"];
                    aux.TotalCompra = (decimal)datos.lector["TotalCompra"];
                    aux.Estado = (bool)datos.lector["Estado"];

                    // Agregar el objeto Compras a la lista
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



        public long AgregarCompra(Compras nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("insert into Compras (ProveedorID,FechaCompra,TotalCompra,Estado) values (@ProveedorID,@FechaCompra,@TotalCompra,@Estado); SELECT SCOPE_IDENTITY();");

                    Datos.setearParametros("@Proveedorid", nuevo.IdProveedor);
                    Datos.setearParametros("@FechaCompra", nuevo.FechaCompra);
                    Datos.setearParametros("@TotalCompra", nuevo.TotalCompra);
                    Datos.setearParametros("@Estado", 0);

                    long idCompra = Convert.ToInt64(Datos.ejecutarScalar());

                    
                    nuevo.IdCompras = (int)idCompra;

                    return idCompra;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void ModificarCompra(Compras nuevo)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetearQuery("UPDATE Compras SET proveedorid=@ProveedorID,FechaCompra=@FechaCompra,TotalCompra=@TotalCompra,Estado=@Estado where CompraID=@CompraID");

                Datos.setearParametros("@ProveedorID", nuevo.IdProveedor);
                Datos.setearParametros("@FechaCompra", nuevo.FechaCompra);
                Datos.setearParametros("@TotalCompra", nuevo.TotalCompra);
                Datos.setearParametros("@Estado", 0);
               // Datos.setearParametros("@VentaID", nuevo.IdCompras);


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

        public void EliminarCompra(int CompraID)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Compras set Estado=1 where ComprasID=@ComprasID");
                datos.setearParametros("@ComprasID", CompraID);
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
