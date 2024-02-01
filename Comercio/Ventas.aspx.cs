using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class Venta : System.Web.UI.Page
    {
        public List<Productos> listaProductos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.vendedor))
            {
                Session.Add("Error", "No eres Vendedor");
                Response.Redirect("Login.aspx", false);
            }

            if (!IsPostBack)
            {
                // Inicializar el dgvProductos vacío
                dgvProductos.DataSource = new List<Productos>();
                dgvProductos.DataBind();
            }
        }

        private void CargarProductos()
        {
            ProductosNegocio negocio = new ProductosNegocio();
            listaProductos = negocio.ListarProductos();
            dgvProductos.DataSource = listaProductos;
            dgvProductos.DataBind();
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string nombreProducto = txtNombre.Text.Trim();

            if (!string.IsNullOrEmpty(nombreProducto))
            {
                // Utilizar la misma lista de productos para agregar resultados de búsqueda
                ProductosNegocio negocio = new ProductosNegocio();
                listaProductos = negocio.ObtenerProductosPorNombre(nombreProducto);

                dgvProductos.DataSource = listaProductos;
                dgvProductos.DataBind();
            }
        }

    }
}
