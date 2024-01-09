using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
    class ProveedoresNegocio
    {
        public List<Proveedores> ListarProveedores(string id = "")
        {
            List<Proveedores> Lista = new List<Proveedores>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select p.ProveedorID,p.Nombre,p.Rubro,p.estado from Proveedores p where p.estado=0");
                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " and ProveedorID = @Id";
                    datos.setearParametros("@Id", Convert.ToInt32(id));
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Proveedores aux = new Proveedores();
                    aux.IdProveedor = (int)datos.lector["ProveedorID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.Categoria = (string)datos.lector["Rubro"];
                    aux.Estado = (bool)datos.lector["estado"];

                    
              
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

        public long AgregarProveedor(Proveedores nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("insert into Proveedores(ProveedorID,Nombre,Rubro,estado) values(@ProveedorID, @Nombre, @Rubro, @estado); SELECT SCOPE_IDENTITY(); ");
                    Datos.setearParametros("@proveedorID", nuevo.IdProveedor);
                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@Rubro", nuevo.Categoria);
                    Datos.setearParametros("@estado", nuevo.Estado);
                   

                    long ID = Convert.ToInt64(Datos.ejecutarScalar());

                    // Asignar el ID generado al objeto 
                    nuevo.IdProveedor = (int)ID;

                    return ID;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void Modificar(Proveedores nuevo)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetearQuery("UPDATE Proveedores SET Nombre = @nombre,Rubro=@Rubro ,estado=@estado where ProveedorID=@IdProveedor");

                Datos.setearParametros("@nombre", nuevo.Nombre);
                Datos.setearParametros("@Rubro", nuevo.Categoria);
                Datos.setearParametros("@estado", nuevo.Estado);
               
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
        public void EliminarProducto(int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Proveedores set estado=1 where ProveedorID=@IdProveedor");
                datos.setearParametros("@IdProvedor", idProveedor);
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
