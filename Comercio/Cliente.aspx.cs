using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{

    public partial class Cliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.vendedor))
                {
                    Session.Add("Error", "No eres Vendedor");
                    Response.Redirect("Login.aspx", false);
                }
            }
            }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {

            string dniCliente = txtDniCliente.Text.Trim();

            if (!string.IsNullOrEmpty(dniCliente))
            {

                ClientesNegocio negocioClientes = new ClientesNegocio();
                negocioClientes.buscarCliente(dniCliente);

                if (negocioClientes.buscarCliente(dniCliente))
                {
                    Session["DniCliente"] = dniCliente;
                    Response.Redirect("Ventas.aspx");
                }
                else
                {

                    lblMensajeClienteNoEncontrado.Text = "Cliente no registrado, por favor registre al cliente.";
                    btnVolverAVenta.Visible = true;
                }
            }

        }

        protected void btnVolverAVenta_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("DefaultVendedor.aspx");
        }
    }
}