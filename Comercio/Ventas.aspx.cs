using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class Venta : System.Web.UI.Page
    {
        // Propiedades públicas para las listas de productos
        public List<Productos> listaProductos { get; set; }
        public List<Productos> listaProductosSeleccionados { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.vendedor))
            {
                Session.Add("Error", "No eres Vendedor");
                Response.Redirect("Login.aspx", false);
            }

            // Inicializar la lista de productos (agrega esta línea)
            listaProductos = new List<Productos>();

            // Si la lista de productos seleccionados no está inicializada, inicialízala.
            if (Session["ListaProductosSeleccionados"] == null)
            {
                listaProductosSeleccionados = new List<Productos>();
                Session["ListaProductosSeleccionados"] = listaProductosSeleccionados;
            }
            else
            {
                // Si ya existe, obtén la lista de la sesión
                listaProductosSeleccionados = (List<Productos>)Session["ListaProductosSeleccionados"];
            }

            // Inicializar el dgvProductos vacío
            dgvProductos.DataSource = listaProductos;
            dgvProductos.DataBind();

            dgvProductosSeleccionados.DataSource = listaProductosSeleccionados;
            dgvProductosSeleccionados.DataBind();
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

        protected void btnAgregarSeleccionados_Click(object sender, EventArgs e)
        {
            List<Productos> productosSeleccionados = new List<Productos>();

            foreach (GridViewRow row in dgvProductos.Rows)
            {
                CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkSeleccionar");

                if (chkSeleccionar != null && chkSeleccionar.Checked)
                {
                    // Agregar el producto seleccionado a la lista
                    int idProducto = Convert.ToInt32(DataBinder.Eval(row.DataItem, "IdProductos"));
                    // Ajusta el índice según la posición de la columna IdProductos en tu GridView
                    Productos producto = ObtenerProductoPorId(idProducto);
                    productosSeleccionados.Add(producto);
                }
            }

            // Agregar los productos seleccionados a la lista general
            listaProductosSeleccionados.AddRange(productosSeleccionados);

            // Actualizar la sesión con la nueva lista de productos seleccionados
            Session["ListaProductosSeleccionados"] = listaProductosSeleccionados;

            // Volver a cargar el dgvProductosSeleccionados con la nueva lista
            dgvProductosSeleccionados.DataSource = listaProductosSeleccionados;
            dgvProductosSeleccionados.DataBind();
        }

        private Productos ObtenerProductoPorId(int idProducto)
        {
            if (listaProductos == null)
            {
                // Si la lista de productos no está inicializada, intenta cargarla.
                CargarProductos();
            }

            if (listaProductos != null)
            {
                // Si la carga de productos fue exitosa, busca el producto por su ID.
                return listaProductos.FirstOrDefault(p => p.IdProductos == idProducto);
            }
            else
            {
                // Si la carga de productos falla, retorna null o toma alguna otra acción.
                return null;
            }
        }


        protected void dgvProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Añadir un atributo de cliente para el ID del producto a cada CheckBox
                CheckBox chkSeleccionar = (CheckBox)e.Row.FindControl("chkSeleccionar");
                int idProducto = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdProductos"));
                chkSeleccionar.Attributes.Add("data-idproducto", idProducto.ToString());
            }
        }
    }
}
