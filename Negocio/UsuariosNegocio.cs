using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuariosNegocio
    {
        public bool loguear(Usuarios usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select UsuarioID,NombreUsuario, Contraseña, TipoUsuario from Usuarios where NombreUsuario = @User and Contraseña= @Pass");
                datos.setearParametros("@User", usuario.Nombre);
                datos.setearParametros("@Pass", usuario.Contrasena);




                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    usuario.IdUsuario = (int)datos.Lector["UsuarioID"];
                    usuario.Nombre = (string)datos.Lector["NombreUsuario"];
                    usuario.Contrasena = (string)datos.Lector["contraseña"];

                    usuario.TipoUsuario = (int)(datos.Lector["tipoUsuario"]) == 1
                     ? Usuarios.TipoUsuarios.administrador : Usuarios.TipoUsuarios.vendedor;
   
                    return true;

                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar loguear", ex);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
