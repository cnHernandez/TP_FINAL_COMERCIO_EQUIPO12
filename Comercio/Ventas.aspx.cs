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
            if (!IsPostBack)
            {
                if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.vendedor))
                {
                    Session.Add("Error", "No eres Vendedor");
                    Response.Redirect("Login.aspx", false);
                }

                // Inicializar la lista de productos 
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

                // Inicializar el dgvProductos solo si la página no está en un postback
                //CargarProductos();

               dgvProductos.DataSource = listaProductos;
               dgvProductos.DataBind();

                dgvProductosSeleccionados.DataSource = listaProductosSeleccionados;
                dgvProductosSeleccionados.DataBind();
            }
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
            // Asegúrate de que la lista de productos seleccionados esté inicializada
            if (Session["ListaProductosSeleccionados"] == null)
            {
                listaProductosSeleccionados = new List<Productos>();
                Session["ListaProductosSeleccionados"] = listaProductosSeleccionados;
            }
            else
            {
                listaProductosSeleccionados = (List<Productos>)Session["ListaProductosSeleccionados"];
            }

            List<Productos> productosSeleccionados = new List<Productos>();

            foreach (GridViewRow row in dgvProductos.Rows)
            {
                CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkSeleccionar");

                if (chkSeleccionar != null && chkSeleccionar.Checked)
                {
                    // Asegúrate de que la lista de productos también esté inicializada
                    if (listaProductosSeleccionados == null)
                    {
                        listaProductosSeleccionados = new List<Productos>();
                    }

                    // Agregar el producto seleccionado a la lista
                    int idProducto = Convert.ToInt32(row.Cells[0].Text); // Ajusta según tu estructura
                    Productos producto = ObtenerProductoPorId(idProducto);
                    productosSeleccionados.Add(producto);
                }
            }

            // Asegúrate nuevamente de que la lista de productos seleccionados esté inicializada
            if (listaProductosSeleccionados == null)
            {
                listaProductosSeleccionados = new List<Productos>();
            }

            // Agregar los productos seleccionados a la lista general
            listaProductosSeleccionados.AddRange(productosSeleccionados);

            // Actualizar la sesión con la nueva lista de productos seleccionados
            Session["ListaProductosSeleccionados"] = listaProductosSeleccionados;

            // Volver a cargar el dgvProductosSeleccionados con la nueva lista
            dgvProductosSeleccionados.DataSource = listaProductosSeleccionados;
            dgvProductosSeleccionados.DataBind();

        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtCantidad.NamingContainer;

            if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad >= 0)
            {
                CalcularSubtotales(row);
                CalcularTotalCompra();

              
            }
            else
            {
                /* // Manejar el caso en que la entrada no sea válida, por ejemplo, mostrar un mensaje de error.
                 lblMensajeError.Text = "La cantidad ingresada no es válida. Por favor, ingrese un número entero no negativo.";*/
            }
        }

        private void CalcularSubtotales(GridViewRow row)
        {
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            Label lblSubtotal = (Label)row.FindControl("lblSubtotal");

            if (txtCantidad != null && lblSubtotal != null)
            {
                // Acceder directamente a las celdas de GridView para obtener los valores
                decimal precioCompra = Convert.ToDecimal(row.Cells[2].Text); // Cambia el índice según la posición de la columna PrecioCompra en tu GridView
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                decimal ganancia = Convert.ToDecimal(row.Cells[3].Text);
                ganancia = (ganancia / 100) + 1;
                decimal subtotal = (precioCompra*ganancia) * cantidad;

                lblSubtotal.Text = subtotal.ToString();
                CalcularTotalCompra();
            }
        }

        private void CalcularTotalCompra()
        {
            decimal totalVenta = 0;

            foreach (GridViewRow row in dgvProductosSeleccionados.Rows)
            {
                Label lblSubtotal = (Label)row.FindControl("lblSubtotal");

                if (lblSubtotal != null)
                {
                    decimal subtotal;

                    if (decimal.TryParse(lblSubtotal.Text, out subtotal))
                    {
                        totalVenta += subtotal;
                    }
                    else
                    {
                        // Manejo de error: Puedes mostrar un mensaje o tomar otra acción en caso de que haya un problema con el formato.
                    }
                }
            }

            lblTotalVenta.Text = totalVenta.ToString();





        }
    }
}