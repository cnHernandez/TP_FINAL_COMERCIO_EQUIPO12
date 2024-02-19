using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Comercio
{
    public partial class ListarProductos : System.Web.UI.Page
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
                if (Request.QueryString["IdProveedor"] != null)
                {
                    BindGridViewDataProveedor();
                }
                else
                {
                    BindGridViewData();
                }
                
            }
        }
        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            // Obtener el nombre del producto ingresado por el usuario
            string nombreProducto = txtNombre.Text.Trim();

            // Verificar si se proporcionó un nombre de producto
            if (!string.IsNullOrEmpty(nombreProducto))
            {
                // Llamar al método ObtenerProductosPorNombre para obtener la lista filtrada
                ProductosNegocio negocio = new ProductosNegocio();
                List<Dominio.Productos> listaProductos = negocio.ObtenerProductosPorNombre(nombreProducto);

                // Actualizar el origen de datos del GridView
                dataGridViewProductos.DataSource = listaProductos;
                dataGridViewProductos.DataBind();
            }
            else
            {
                // Si no se proporcionó un nombre de producto, mostrar todos los productos
                BindGridViewData();
            }
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
            List<Dominio.Productos> listaProductos = negocio.ListarProductosLimpio();

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

        protected void dataGridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgregarProveedor")
            {

                int idProducto = int.TryParse(e.CommandArgument.ToString(), out int result) ? result : -1;
                ProductosNegocio negocio = new ProductosNegocio();
                Productos producto = new Productos();

                producto=negocio.ObtenerProductoPorId(idProducto);

                Session["ProductoSeleccionado"] = producto;

                // Redirigir a la página AgregarPxP.aspx
                Response.Redirect("AgregarPxP.aspx");
            }
        }
    }
}
