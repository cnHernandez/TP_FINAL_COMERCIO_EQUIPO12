using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
namespace Negocio
{
   public class  ProveedoresNegocio
    {
        public List<Proveedores> ListarProveedores(string id = "")
        {
            List<Proveedores> Lista = new List<Proveedores>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select p.ProveedorID,p.Nombre,p.Rubro, p.UrlImagen, p.estado from Proveedores p where p.estado=0");
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
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
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

        public List<Proveedores> ObtenerProveedoresPorNombre(string nombreProveedor)
        {
            List<Proveedores> listaProveedores = new List<Proveedores>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT ProveedorID, Nombre, Rubro, Estado, UrlImagen FROM Proveedores WHERE Estado = 0 and Nombre LIKE @nombreProveedor";
                datos.SetearQuery(query);
                datos.setearParametros("@nombreProveedor", "%" + nombreProveedor + "%");
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Proveedores aux = new Proveedores();
                    aux.IdProveedor = (int)datos.lector["ProveedorID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.Categoria = (string)datos.lector["Rubro"];
                    aux.Estado = (bool)datos.lector["Estado"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];

                    listaProveedores.Add(aux);
                }

                return listaProveedores;
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

        public string ObtenerNombreProveedorPorId(int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("SELECT Nombre FROM Proveedores WHERE ProveedorID = @IdProveedor AND Estado = 0");
                datos.setearParametros("@IdProveedor", idProveedor);
                datos.EjecutarLectura();

                if (datos.lector.Read())
                {
                    return datos.lector["Nombre"].ToString();
                }
                else
                {
                    // Manejar el caso en el que no se encuentre el proveedor con el ID dado
                    return "Proveedor no encontrado";
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
        public long AgregarProveedor(Proveedores nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("insert into Proveedores(Nombre,Rubro, UrlImagen, estado) values(@Nombre, @Rubro, @UrlImagen, @estado); SELECT SCOPE_IDENTITY(); ");
                   
                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@Rubro", nuevo.Categoria);
                    Datos.setearParametros("@estado", nuevo.Estado);
                    Datos.setearParametros("@UrlImagen", nuevo.UrlImagen);
                   

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
                Datos.SetearQuery("UPDATE Proveedores SET Nombre = @nombre,Rubro=@Rubro, UrlImagen= @UrlImagen ,estado=@estado where ProveedorID=@IdProveedor");

                Datos.setearParametros("@nombre", nuevo.Nombre);
                Datos.setearParametros("@Rubro", nuevo.Categoria);
                Datos.setearParametros("@estado", nuevo.Estado);
                Datos.setearParametros("@UrlImagen", nuevo.UrlImagen);
                Datos.setearParametros("@IdProveedor", nuevo.IdProveedor);
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
        public void EliminarProveedor(int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Proveedores set estado=1 where ProveedorID=@IdProveedor");
                datos.setearParametros("@IdProveedor", idProveedor);
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
