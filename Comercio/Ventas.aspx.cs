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
       
        public List <DetalleVenta> listaProductosSeleccionados { get; set; }

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
                //ListaProductos
                if (Session["ListaProductos"] == null)
                {
                    listaProductos = new List<Productos>();
                    Session["ListaProductos"] = listaProductos;
                }
                else
                {
                    // Si ya existe, obtén la lista de la sesión
                    listaProductos = (List<Productos>)Session["ListaProductos"];
                }

                if (Session["ListaProductosSeleccionados"] == null)
                {
                    listaProductosSeleccionados = new List<DetalleVenta>();
                    Session["ListaProductosSeleccionados"] = listaProductosSeleccionados;
                }
                else
                {
                    // Si ya existe, obtén la lista de la sesión
                    listaProductosSeleccionados = (List<DetalleVenta>)Session["ListaProductosSeleccionados"];
                }

              

                // Inicializar el dgvProductos solo si la página no está en un postback
                ProductosNegocio negocio = new ProductosNegocio();
                string nombreProducto = txtNombre.Text.Trim();

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

        private bool ProductoYaSeleccionado(int idProducto)
        {
            if (listaProductosSeleccionados == null)
            {
                listaProductosSeleccionados = new List<DetalleVenta>();
            }

            return listaProductosSeleccionados.Any(p => p.IdProducto == idProducto);
        }



        protected void btnAgregarSeleccionados_Click(object sender, EventArgs e)
        {
     



            List<DetalleVenta> detallesVentaSession = Session["ListaProductosSeleccionados"] as List<DetalleVenta> ?? new List<DetalleVenta>();
            List<Productos> productosSeleccionadosSession = Session["ListaProductos"] as List<Productos> ?? new List<Productos>();

            foreach (GridViewRow row in dgvProductos.Rows)
            {
                CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkSeleccionar");

                if (chkSeleccionar != null && chkSeleccionar.Checked)
                {
                    int idProducto = Convert.ToInt32(row.Cells[0].Text);

                    if (!ProductoYaSeleccionado(idProducto))
                    {
                        DetalleVenta aux = new DetalleVenta();
                        Productos producto = ObtenerProductoPorId(idProducto);

                        productosSeleccionadosSession.Add(producto);
                        aux.IdProducto = idProducto;
                        aux.PrecioVenta = ((producto.PorcentajeGanancia / 100) + 1) * producto.PrecioCompra;
                        
                        aux.Subtotal = 0;
                        aux.IdVenta = 0;
                        detallesVentaSession.Add(aux);

                        // Limpiar el mensaje de error
                        lblMensajeError.Text = string.Empty;
                    }
                    else
                    {
                        lblMensajeError.Text = "El producto ya está seleccionado.";
                    }
                }
            }

            // Actualizar la sesión con la nueva lista de productos seleccionados
            Session["ListaProductosSeleccionados"] = detallesVentaSession;
            Session["ListaProductos"] = productosSeleccionadosSession;

            // Recargar las listas locales
            listaProductos = productosSeleccionadosSession;
            listaProductosSeleccionados = detallesVentaSession;//falta actualizar la cantidad en la session . 

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

                // Actualizar la lista de detalles de venta con la nueva cantidad
                int idProducto = Convert.ToInt32(row.Cells[0].Text);
                ActualizarDetallesVenta(idProducto, cantidad);
            }
            else
            {
                // Manejar el caso en que la entrada no sea válida, por ejemplo, mostrar un mensaje de error.
                 lblMensajeError.Text = "La cantidad ingresada no es válida. Por favor, ingrese un número entero no negativo.";
            }
        }
        private void ActualizarDetallesVenta(int idProducto, int nuevaCantidad)
        {
            // Obtener la lista desde la sesión o inicializarla si aún no existe
            listaProductosSeleccionados = Session["ListaProductosSeleccionados"] as List<DetalleVenta> ?? new List<DetalleVenta>();

            // Verificar si el producto ya está en la lista de detalles
            var detalleExistente = listaProductosSeleccionados.FirstOrDefault(d => d.IdProducto == idProducto);

            if (detalleExistente != null)
            {
                // Actualizar la cantidad si el producto ya está en la lista
                detalleExistente.Cantidad = nuevaCantidad;
            }
            else
            {
                // Agregar un nuevo detalle si el producto no está en la lista
                listaProductosSeleccionados.Add(new DetalleVenta
                {
                    IdProducto = idProducto,
                    Cantidad = nuevaCantidad,
                   
                });
            }

            // Actualizar la sesión con la nueva lista de productos seleccionados
            Session["ListaProductosSeleccionados"] = listaProductosSeleccionados;
        }

        private void CalcularSubtotales(GridViewRow row)
        {
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            Label lblSubtotal = (Label)row.FindControl("lblSubtotal");

            if (txtCantidad != null && lblSubtotal != null)
            {
                // Acceder directamente a las celdas de GridView para obtener los valores
                decimal PrecioVenta = Convert.ToDecimal(row.Cells[1].Text); // Cambia el índice según la posición de la columna PrecioCompra en tu GridView
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                decimal subtotal = PrecioVenta * cantidad;

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