using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class BuscarProductos : System.Web.UI.Page
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
                // Solo cargar los productos si no es un PostBack
                CargarProductos();
            }
        }

        protected decimal ObtenerPrecioCompra(object productosXProveedores)
        {
            if (productosXProveedores is List<Producto_x_Proveedor> lista && lista.Any())
            {
                return lista[0].PrecioCompra;
            }
            return 0; // O cualquier otro valor predeterminado que desees
        }
        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string nombreProducto = txtNombre.Text.Trim();

            if (!string.IsNullOrEmpty(nombreProducto))
            {
                // Utilizar la misma lista de productos para agregar resultados de búsqueda
                ProductosNegocio negocio = new ProductosNegocio();
                listaProductos = negocio.ObtenerProductosConPrecioYnombre(nombreProducto);

                reRepeater.DataSource = listaProductos;
                reRepeater.DataBind();
            }
        }

        private void CargarProductos()
        {
            ProductosNegocio negocio = new ProductosNegocio();
            listaProductos = negocio.ObtenerProductosConPrecio();
            reRepeater.DataSource = listaProductos;
            reRepeater.DataBind();
        }
    }
}