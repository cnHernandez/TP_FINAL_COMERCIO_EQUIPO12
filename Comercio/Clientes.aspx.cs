using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "no eres administrador");
                Response.Redirect("Login.aspx", false);
            }
            if (!IsPostBack)
            {
                BindGridViewData();
            }
        }

        private void BindGridViewData()
        {
            ClientesNegocio negocio = new ClientesNegocio();
            List<Dominio.Clientes> listaClientes = negocio.ListarClientes();

            dataGridViewClientes.DataSource = listaClientes;
            dataGridViewClientes.DataBind();
        }

        protected void dataGridViewClientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewClientes.PageIndex = e.NewPageIndex;
            BindGridViewData();
        }

        protected void dataGridViewClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = dataGridViewClientes.SelectedRow;
            string IdCliente = dataGridViewClientes.DataKeys[selectedRow.RowIndex].Value.ToString();
            Response.Redirect("AgregarClientes.aspx?IdCliente=" + IdCliente);
        }

        protected void dataGridViewClientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}