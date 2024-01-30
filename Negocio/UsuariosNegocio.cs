using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using static Dominio.Usuarios;

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

        public List<Usuarios> ListarUsuarios(string id = "")
        {
            List<Usuarios> Lista = new List<Usuarios>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select UsuarioID, NombreUsuario, Contraseña, TipoUsuario from Usuarios");
                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " where UsuarioID = @Id";
                    datos.setearParametros("@Id", Convert.ToInt32(id));
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Usuarios aux = new Usuarios("", "", false, false);

                    aux.IdUsuario = (int)datos.lector["UsuarioID"];
                    aux.Nombre = (string)datos.lector["NombreUsuario"];
                    aux.Contrasena = (string)datos.lector["Contraseña"];
                    aux.TipoUsuario = (TipoUsuarios)datos.lector["TipoUsuario"];

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

        public long AgregarUsuario(Usuarios nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("INSERT INTO Usuarios (NombreUsuario, Contraseña, TipoUsuario) VALUES (@Nombre, @Contraseña, @TipoUsuario); SELECT SCOPE_IDENTITY();");

                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@Contraseña", nuevo.Contrasena);
                    Datos.setearParametros("@TipoUsuario", nuevo.TipoUsuario);

                    long idUsuario = Convert.ToInt64(Datos.ejecutarScalar());

                    // Asignar el ID generado al objeto Usuario
                    nuevo.IdUsuario = (int)idUsuario;

                    return idUsuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarUsuario(Usuarios usuario)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("UPDATE Usuarios SET NombreUsuario = @Nombre, Contraseña = @Contraseña, TipoUsuario = @TipoUsuario WHERE UsuarioID = @Id");

                    Datos.setearParametros("@Nombre", usuario.Nombre);
                    Datos.setearParametros("@Contraseña", usuario.Contrasena);
                    Datos.setearParametros("@TipoUsuario", usuario.TipoUsuario);
                    Datos.setearParametros("@Id", usuario.IdUsuario);

                    Datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EliminarUsuario(int id)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("DELETE FROM Usuarios WHERE UsuarioID = @Id");

                    Datos.setearParametros("@Id", id);

                    Datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
