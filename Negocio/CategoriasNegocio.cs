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
                datos.SetearQuery("SELECT TipoID, Nombre FROM Tipos");

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
