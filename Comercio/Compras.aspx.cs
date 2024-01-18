using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class Compras : System.Web.UI.Page
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
                CargarProveedor();

            }
        }
        private void CargarProveedor()
        {
            ProveedoresNegocio pro = new ProveedoresNegocio();
            ddlProveedor.DataSource = pro.ListarProveedores();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataBind();

            ddlProveedor.Items.Insert(0, new ListItem("-- Seleccione un proveedor --", ""));
        }
        private void BindGridViewDataProveedor()
        {
            ProductosNegocio negocio = new ProductosNegocio();
            int idProveedor = Convert.ToInt32(Request.QueryString["IdProveedor"]);
            List<Dominio.Productos> listaProductos = negocio.ListarProductosPorProveedor(idProveedor);

            dataGridViewProductos.DataSource = listaProductos;
            dataGridViewProductos.DataBind();
        }
        private void BindGridViewData()
        {
            ProductosNegocio negocio = new ProductosNegocio();
            List<Dominio.Productos> listaProductos = negocio.ListarProductos();

            dataGridViewProductos.DataSource = listaProductos;
            dataGridViewProductos.DataBind();
        }

        protected void dataGridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewProductos.PageIndex = e.NewPageIndex;
            BindGridViewData();
        }

        protected void dataGridViewProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = dataGridViewProductos.SelectedRow;
            string IdCliente = dataGridViewProductos.DataKeys[selectedRow.RowIndex].Value.ToString();
            Response.Redirect("AgregarProducto.aspx?IdProductos=" + IdCliente);
        }

        protected void dataGridViewProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = dataGridViewProductos.DataKeys[e.RowIndex].Value.ToString();
            ProductosNegocio negocio = new ProductosNegocio();
            negocio.EliminarProducto(int.Parse(ID));
            BindGridViewData();
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlProveedor.SelectedValue))
            {
                int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

                // Llama al método para cargar los productos asociados al proveedor
                BindGridViewDataProveedor(idProveedor);
                ddlProveedor.Enabled = false;
            }
        }

        private void BindGridViewDataProveedor(int idProveedor)
        {
        ProductosNegocio negocio = new ProductosNegocio();
        List<Dominio.Productos> listaProductos = negocio.ListarProductosPorProveedor(idProveedor);

        dataGridViewProductos.DataSource = listaProductos;
        dataGridViewProductos.DataBind();
}
    }
}