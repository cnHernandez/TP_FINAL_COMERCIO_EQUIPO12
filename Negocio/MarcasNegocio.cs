using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcasNegocio
    {


        public List<Marcas> ListarMarcas(string id = "")
        {
            List<Marcas> Lista = new List<Marcas>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("SELECT MarcaID, Nombre, UrlImagen, Estado FROM Marcas WHERE Estado = 0");

                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " AND MarcaID = @Id";
                    datos.setearParametros("@Id", id);
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Marcas aux = new Marcas();

                    aux.IdMarcas = (int)datos.lector["MarcaID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.Estado = (bool)datos.lector["Estado"];
                    if (datos.lector["URLimagen"] != DBNull.Value)
                    {
                        aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    }
                    else
                    {
                        // Manejar el caso de valor nulo, por ejemplo, asignar un valor predeterminado
                        aux.UrlImagen = "No disponible";
                    }

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
        public List<Marcas> ObtenerMarcasPorNombre(string nombreMarca)
        {
            List<Marcas> listaMarcas = new List<Marcas>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT MarcaID, Nombre, UrlImagen, Estado FROM Marcas WHERE Estado = 0 and Nombre LIKE @nombreMarca";
                datos.SetearQuery(query);
                datos.setearParametros("@nombreMarca", "%" + nombreMarca + "%");
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Marcas aux = new Marcas();
                    aux.IdMarcas = (int)datos.lector["MarcaID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.Estado = (bool)datos.lector["Estado"];

                    listaMarcas.Add(aux);
                }

                return listaMarcas;
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

        public void AgregarMarca(Marcas nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {

                    Datos.SetearQuery("INSERT INTO Marcas  (Nombre,  UrlImagen, Estado) VALUES ( @Nombre, @Urlimagen, @Estado ); SELECT SCOPE_IDENTITY();");


                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@Urlimagen", nuevo.UrlImagen);
                    Datos.setearParametros("@Estado", 0);
                    long Id = Convert.ToInt64(Datos.ejecutarScalar());

                    // Asignar el ID generado al objeto Medico
                    nuevo.IdMarcas = (int)Id;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void Modificar(Marcas nuevo)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetearQuery("UPDATE Marcas SET Nombre = @nombre, UrlImagen = @UrlImagen WHERE MarcaID = @Marca");

                Datos.setearParametros("@nombre", nuevo.Nombre);
                Datos.setearParametros("@UrlImagen", nuevo.UrlImagen);
                Datos.setearParametros("@Marca", nuevo.IdMarcas);

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


        public void EliminarMarca(int IdMarca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Marcas set estado=1 where MarcaID =@IdMarca");
                datos.setearParametros("@IdMarca", IdMarca);
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
