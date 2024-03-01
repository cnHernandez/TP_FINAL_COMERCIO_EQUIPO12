using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClientesNegocio
    {

        public List<Clientes> ListarClientes(string id = "")
        {
            List<Clientes> Lista = new List<Clientes>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("select ClienteID, Nombre, Apellido, Mail, Dni, Telefono, Estado from Clientes where Estado = 0");
                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " and ClienteID = @Id";
                    datos.setearParametros("@Id", Convert.ToInt32(id));
                }

                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Clientes aux = new Clientes();

                    aux.IdCliente = (int)datos.lector["CLienteID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.Apellido = (string)datos.lector["Apellido"];
                    aux.Mail = (string)datos.lector["Mail"];
                    aux.Dni = (long)datos.lector["Dni"];
                    aux.Telefono = (long)datos.lector["Telefono"];
                    aux.Estado = (bool)datos.lector["Estado"];
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
        public int? buscarNroCliente(string dni)
        {
            int nroCliente;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string query = "SELECT ClienteID FROM Clientes WHERE  Dni LIKE @dni";
                datos.SetearQuery(query);
                datos.setearParametros("@dni", dni);
                datos.EjecutarLectura();

                if (datos.lector.Read())
                {
                    nroCliente = (int)datos.lector["ClienteID"];
                    return nroCliente;
                }
                else
                {
                    
                    return null;
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
        public bool buscarCliente (string filtro)
            {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string query = "SELECT Estado FROM Clientes WHERE  Dni LIKE @filtro";
                datos.SetearQuery(query);
                datos.setearParametros("@filtro", filtro);
                datos.EjecutarLectura();

                if (datos.lector.Read() == true)
                {
                    return true;
                }
                else
                {
                    return false; 
                }

            }
            catch (Exception ex)
            {

                throw ex ;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public List<Clientes> ObtenerClientesPorNombreApellido(string filtro)
        {
            List<Clientes> listaClientes = new List<Clientes>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT ClienteID, Nombre, Apellido, Mail, Dni, Telefono, Estado FROM Clientes WHERE Estado = 0 AND (Nombre LIKE @filtro OR Apellido LIKE @filtro)";
                datos.SetearQuery(query);
                datos.setearParametros("@filtro", "%" + filtro + "%");
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Clientes aux = new Clientes();
                    aux.IdCliente = (int)datos.lector["ClienteID"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.Apellido = (string)datos.lector["Apellido"];
                    aux.Mail = (string)datos.lector["Mail"];
                    aux.Dni = (long)datos.lector["Dni"];
                    aux.Telefono = (long)datos.lector["Telefono"];
                    aux.Estado = (bool)datos.lector["Estado"];

                    listaClientes.Add(aux);
                }

                return listaClientes;
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

        public long AgregarClientes(Clientes nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("INSERT INTO Clientes (Nombre, Apellido, Mail, Dni, Telefono, Estado) VALUES (@Nombre, @Apellido, @Mail, @Dni, @Telefono, @Estado); SELECT SCOPE_IDENTITY();");

                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@Apellido", nuevo.Apellido);
                    Datos.setearParametros("@Mail", nuevo.Mail);
                    Datos.setearParametros("@Dni", nuevo.Dni);
                    Datos.setearParametros("@Telefono", nuevo.Telefono);
                    Datos.setearParametros("@Estado", 0);

                    long legajo = Convert.ToInt64(Datos.ejecutarScalar());

                    // Asignar el ID generado al objeto Medico
                    nuevo.IdCliente = (int)legajo;

                    return legajo;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void ModificarCliente(Clientes nuevo)
        {
            AccesoDatos Datos = new AccesoDatos();

            try
            {
                Datos.SetearQuery("UPDATE Clientes\r\nSET Nombre = @Nombre,\r\n    Apellido = @Apellido,\r\n    Mail = @Mail,\r\n    Dni = @Dni,\r\n    Telefono = @Telefono,\r\n    Estado = @Estado\r\nWHERE ClienteID = @IdCliente;");

                Datos.setearParametros("@Nombre", nuevo.Nombre);
                Datos.setearParametros("@Apellido", nuevo.Apellido);
                Datos.setearParametros("@Mail", nuevo.Mail);
                Datos.setearParametros("@Dni", nuevo.Dni);
                Datos.setearParametros("@Telefono", nuevo.Telefono);
                Datos.setearParametros("@Estado", 0);
                Datos.setearParametros("@IdCliente", nuevo.IdCliente);


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

        public void EliminarCliente(int IdCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearQuery("update Clientes set estado=1 where ClienteID =@IdCliente");
                datos.setearParametros("@IdCliente", IdCliente);
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

            public string ObtenerNombreClientePorId(int idCliente)
            {
                AccesoDatos datos = new AccesoDatos();

                try
                {
                    datos.SetearQuery("SELECT Nombre FROM Clientes WHERE ClienteID = @IdCliente AND Estado = 0");
                    datos.setearParametros("@IdCliente", idCliente);
                    datos.EjecutarLectura();

                    if (datos.lector.Read())
                    {
                        return datos.lector["Nombre"].ToString();
                    }
                    else
                    {
                        // Si el cliente no se encuentra o está marcado como inactivo, retornar un valor por defecto o lanzar una excepción según el caso.
                        return "Cliente Desconocido";
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

        public bool MailExiste(string mail, int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT COUNT(*) FROM Clientes WHERE Mail = @Mail AND ClienteID != @IdCliente";
                datos.SetearQuery(query);
                datos.setearParametros("@Mail", mail);
                datos.setearParametros("@IdCliente", idCliente);
                int count = Convert.ToInt32(datos.ejecutarScalar());
                return count > 0;
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

        public bool DNIExiste(long dni, int idCliente)
        {
            try
            {
                using (AccesoDatos datos = new AccesoDatos())
                {
                    datos.SetearQuery("SELECT COUNT(*) FROM Clientes WHERE Dni = @Dni AND ClienteID <> @IdCliente");
                    datos.setearParametros("@Dni", dni);
                    datos.setearParametros("@IdCliente", idCliente);
                    int count = Convert.ToInt32(datos.ejecutarScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tu lógica de aplicación
                throw ex;
            }
        }

        public bool TelefonoExiste(long telefono, int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string query = "SELECT COUNT(*) FROM Clientes WHERE Telefono = @Telefono AND ClienteID != @IdCliente";
                datos.SetearQuery(query);
                datos.setearParametros("@Telefono", telefono);
                datos.setearParametros("@IdCliente", idCliente);
                int count = Convert.ToInt32(datos.ejecutarScalar());
                return count > 0;
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
