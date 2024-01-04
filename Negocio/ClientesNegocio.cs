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
                datos.SetearQuery("select ClienteID, Nombre, Apellido, Mail, Dni, Telefono from Clientes");
                if (!string.IsNullOrEmpty(id))
                {
                    datos.Comando.CommandText += " Where ClienteID = @Id";
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

        public long AgregarClientes(Clientes nuevo)
        {
            try
            {
                using (AccesoDatos Datos = new AccesoDatos())
                {
                    Datos.SetearQuery("INSERT INTO Clientes (Nombre, Apellido, Mail, Dni, Telefono) VALUES (@Nombre, @Apellido, @Mail, @Dni, @Telefono); SELECT SCOPE_IDENTITY();");

                    Datos.setearParametros("@Nombre", nuevo.Nombre);
                    Datos.setearParametros("@Apellido", nuevo.Apellido);
                    Datos.setearParametros("@Mail", nuevo.Mail);
                    Datos.setearParametros("@Dni", nuevo.Dni);
                    Datos.setearParametros("@Telefono", nuevo.Telefono);

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
    }
}
