using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriasNegocio
    {

        public List<Categorias> ListarCategorias(string id = "")
        {
            List<Categorias> Lista = new List<Categorias>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("SELECT TipoID, Nombre, UrlImagen FROM Tipos WHERE Estado = 0");

                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " AND TipoID = @Id";
                    datos.setearParametros("@Id", id);
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Categorias aux = new Categorias();

                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];

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

        public List<Categorias> ObtenerCategoriasPorNombre(string nombreCategoria)
        {
            List<Categorias> listaCategorias = new List<Categorias>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT TipoID, Nombre, UrlImagen, Estado FROM Tipos WHERE Estado = 0 and Nombre LIKE @nombreCategoria";
                datos.SetearQuery(query);
                datos.setearParametros("@nombreCategoria", "%" + nombreCategoria + "%");
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Categorias aux = new Categorias();
                    aux.IdCategoria = (int)datos.lector["TipoID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.UrlImagen = (string)datos.lector["UrlImagen"];
                    aux.Estado = (bool)datos.lector["Estado"];

                    listaCategorias.Add(aux);
                }

                return listaCategorias;
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


        public void AgregarCategoria(Categorias nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {

                    Datos.SetearQuery("INSERT INTO Tipos  (Nombre, UrlImagen, Estado) VALUES ( @Nombre, @UrlImagen, @Estado ); SELECT SCOPE_IDENTITY();");


                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@UrlImagen", nuevo.UrlImagen);
                    Datos.setearParametros("@Estado", 0);
                    long Id = Convert.ToInt64(Datos.ejecutarScalar());

                    // Asignar el ID generado al objeto Medico
                    nuevo.IdCategoria = (int)Id;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public void Modificar(Categorias nuevo)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetearQuery("UPDATE Tipos SET Nombre = @nombre, UrlImagen = @UrlImagen WHERE TipoID = @TipoID");

                Datos.setearParametros("@nombre", nuevo.Nombre);
                Datos.setearParametros("@UrlImagen", nuevo.UrlImagen);
                Datos.setearParametros("@TipoID", nuevo.IdCategoria);

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

        public void EliminarCategoria(int IdCat)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Tipos set estado=1 where TipoID =@TipoID");
                datos.setearParametros("@TipoID", IdCat);
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
