using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Comercio
{
    public partial class AgregarClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string legajo = Request.QueryString["IdCliente"] != null ? Request.QueryString["IdCliente"].ToString() : "";
            if (legajo != "" && !IsPostBack)
            {
                ClientesNegocio negocio = new ClientesNegocio();
                // List<Medico> lista = negocio.ListarMedicos(legajo);
                // Medico seleccionado = lista[0];

                Dominio.Clientes seleccionado = (negocio.ListarClientes(legajo))[0];

                ///precargamos
                txtNombre.Text = seleccionado.Nombre;
                txtApellido.Text = seleccionado.Apellido;
                txtMail.Text = seleccionado.Mail;
                txtTelefono.Text = seleccionado.Telefono.ToString();
                txtDni.Text= seleccionado.Dni.ToString();

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Dominio.Clientes cliente = new Dominio.Clientes();
                ClientesNegocio nuevo = new ClientesNegocio();

                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Mail = txtMail.Text;

                // Validar que el DNI sea un número válido antes de intentar convertirlo
                if (long.TryParse(txtDni.Text, out long dni))
                {
                    cliente.Dni = dni;

                    // Validar que el DNI no exista ya en la base de datos
                    if (!DNIExiste(cliente.Dni, cliente.IdCliente))
                    {
                        // Continuar con la asignación de otros campos
                        cliente.Telefono = long.Parse(txtTelefono.Text);

                        if (Request.QueryString["IdCliente"] != null)
                        {
                            string legajo = Request.QueryString["IdCliente"];
                            cliente.IdCliente = (int)(long.TryParse(legajo, out long legajoComoLong) ? legajoComoLong : 0);
                            nuevo.ModificarCliente(cliente);
                        }
                        else
                        {
                            nuevo.AgregarClientes(cliente);
                        }

                        Response.Redirect("Clientes.aspx", false);
                    }
                    else
                    {
                        // El DNI ya existe en la base de datos, mostrar mensaje al usuario
                        lblMensaje.Text = "El DNI ya ha sido utilizado. Por favor, ingrese un DNI diferente.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    // El DNI no es un número válido, mostrar mensaje de error
                    lblMensaje.Text = "Ingrese un valor válido para el DNI.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }


    protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx", false);
        }

        private bool DNIExiste(long dni, int idCliente)
        {
            // Lógica para verificar si el DNI ya existe en la base de datos
            // Excluye el DNI del cliente actual durante la verificación
            // Retorna true si el DNI existe, false si no existe

            // Ejemplo (ajusta la consulta y la conexión a tu base de datos):
            using (AccesoDatos Datos = new AccesoDatos())
            {
                Datos.SetearQuery("SELECT COUNT(*) FROM Clientes WHERE Dni = @Dni AND ClienteID = @IdCliente");
                Datos.setearParametros("@Dni", dni);
                Datos.setearParametros("@IdCliente", idCliente);
                int count = Convert.ToInt32(Datos.ejecutarScalar());
                return count > 0;
            }
        }
    }
}