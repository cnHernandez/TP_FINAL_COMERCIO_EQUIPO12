using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private bool ContieneNumeros(string texto)
        {
            foreach (char c in texto)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
        private bool ContieneLetras(string texto)
        {
            foreach (char c in texto)
            {
                if (char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder errores = new StringBuilder();
                errores.Clear();


                if (string.IsNullOrWhiteSpace(txtNombre.Text) || ContieneNumeros(txtNombre.Text))
                {
                    errores.AppendLine("Nombre Invalido...");
                    lblNombre.Text = "Nombre Invalido...";
                    lblNombre.Visible = true;
                }
                else
                {
                    lblNombre.Visible = false;
                }
                if (string.IsNullOrWhiteSpace(txtApellido.Text) || ContieneNumeros(txtApellido.Text))
                {
                    errores.AppendLine("Apellido Invalido...");
                    lblApellido.Text = "Apellido Invalido...";
                    lblApellido.Visible = true;
                }
                else
                {
                    lblApellido.Visible = false;
                }
                if (string.IsNullOrWhiteSpace(txtDni.Text) || ContieneLetras(txtDni.Text))
                {
                    errores.AppendLine("Dni Invalido...");
                    lblDniError.Text = "Dni Invalido...";
                    lblDniError.Visible = true;
                }
                else
                {
                    lblDniError.Visible = false;
                }
                if (string.IsNullOrWhiteSpace(txtTelefono.Text) || ContieneLetras(txtTelefono.Text))
                {
                    errores.AppendLine("Telefono Invalido...");
                    lblTelefono.Text = "Telefono Invalido...";
                    lblTelefono.Visible = true;
                }
                else
                {
                    lblTelefono.Visible = false;
                }
                if (string.IsNullOrWhiteSpace(txtMail.Text))
                {
                    errores.AppendLine("Mail Invalido...");
                    lblMail.Text = "Mail Invalido...";
                    lblMail.Visible = true;
                }
                else
                {
                    lblMail.Visible = false;
                }

                if (errores.Length > 0)
                {
                    return; // Detener el proceso ya que hay errores
                }

                Dominio.Clientes cliente = new Dominio.Clientes();
                ClientesNegocio nuevo = new ClientesNegocio();

                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Mail = txtMail.Text;
                cliente.Telefono = long.Parse(txtTelefono.Text);
                if (Request.QueryString["IdCliente"] != null)
                {
                    string legajo = Request.QueryString["IdCliente"];
                    cliente.IdCliente = (int)(long.TryParse(legajo, out long legajoComoLong) ? legajoComoLong : 0);                  
                }
                // Validar que el DNI sea un número válido antes de intentar convertirlo
                if (long.TryParse(txtDni.Text, out long dni))
                {
                    cliente.Dni = dni;

                    // Validar que el DNI no exista ya en la base de datos
                        // Continuar con la asignación de otros campos
                    if (!nuevo.DNIExiste(cliente.Dni, cliente.IdCliente))
                    {
                        if(!nuevo.MailExiste(cliente.Mail, cliente.IdCliente)) {

                            if (!nuevo.TelefonoExiste(cliente.Telefono, cliente.IdCliente))
                            {

                            if (Request.QueryString["IdCliente"] != null)
                            {
                                nuevo.ModificarCliente(cliente);
                            }
                            else
                            {
                                nuevo.AgregarClientes(cliente);
                            }
                            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.vendedor))
                            {
                                Response.Redirect("default.aspx", false);
                            }
                            else
                            {
                                Response.Redirect("DefaultVendedor.aspx", false);
                             }
                            }
                            else
                            {
                                lblTelefono.Text = "El telefono ya esta en uso";
                                lblTelefono.Visible = true;
                            }
                        

                        }
                        else
                        {
                            lblMail.Text = "El mail ya existe. O la persona ya existe";
                            lblMail.Visible = true;
                        }
                        
                    }
                    else
                    {                      
                        // El DNI ya existe en la base de datos, mostrar mensaje al usuario
                        lblDniError.Text = "El DNI ya ha sido utilizado. O la persona ya existe";
                        lblDniError.ForeColor = System.Drawing.Color.Red;
                        lblDniError.Visible = true;
                    }
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

      
    }
}