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
                cliente.Dni = long.Parse(txtDni.Text);
                cliente.Telefono = long.Parse(txtTelefono.Text);


                if (Request.QueryString["IdCliente"] != null)
                {
                   /* string legajo = Request.QueryString["IdCliente"];
                    cliente.IdCliente= long.TryParse(legajo, out long legajoComoLong) ? legajoComoLong : 0;
                    nuevo.Modificar(sede);*/
                }

                else

                { nuevo.AgregarClientes(cliente); }

                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sedes.aspx", false);
        }
    }
}