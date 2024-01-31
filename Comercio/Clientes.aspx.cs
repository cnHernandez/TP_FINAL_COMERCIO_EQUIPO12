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

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            // Obtener el nombre del producto ingresado por el usuario
            string nombreCliente = txtNombre.Text.Trim();

            // Verificar si se proporcionó un nombre de producto
            if (!string.IsNullOrEmpty(nombreCliente))
            {
                // Llamar al método ObtenerProductosPorNombre para obtener la lista filtrada
                ClientesNegocio negocio = new ClientesNegocio();
                List<Dominio.Clientes> listaClientes = negocio.ObtenerClientesPorNombreApellido(nombreCliente);

                // Actualizar el origen de datos del GridView
                dataGridViewClientes.DataSource = listaClientes;
                dataGridViewClientes.DataBind();
            }
            else
            {
                // Si no se proporcionó un nombre de producto, mostrar todos los productos
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
            string ID= dataGridViewClientes.DataKeys[e.RowIndex].Value.ToString();
            ClientesNegocio negocio = new ClientesNegocio();
            negocio.EliminarCliente(int.Parse(ID));
            BindGridViewData();
        }
    }
}