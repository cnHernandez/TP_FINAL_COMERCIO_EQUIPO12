using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Comercio
{
    public partial class Venta : System.Web.UI.Page
    {
        
        public List<Productos> listaProductos { get; set; }
       
        public List <DetalleVenta> listaProductosSeleccionados { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NumeroCliente"] != null)
                {
                    string DniCliente = Session["DniCliente"].ToString();
                   
                }

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
                
                    // Lógica para manejar la actualización de la página, como cuando se selecciona una cantidad de productos
                
                


                // Inicializar el dgvProductos solo si la página no está en un postback
                ProductosNegocio negocio = new ProductosNegocio();
                string nombreProducto = txtNombre.Text.Trim();

                dgvProductosSeleccionados.DataSource = listaProductosSeleccionados;
                dgvProductosSeleccionados.DataBind();


                foreach (GridViewRow row in dgvProductosSeleccionados.Rows)
                {
                    CalcularYActualizarSubtotal(row);
                }
                CalcularTotalVenta();
            } 
                    
            
        }


        protected void dgvProductosSeleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument); // Obtener el índice de la fila
                List<DetalleVenta> listaDetallesVenta = (List<DetalleVenta>)Session["listaProductosSeleccionados"];

                if (listaDetallesVenta != null && index >= 0 && index < listaDetallesVenta.Count)
                {
                    listaDetallesVenta.RemoveAt(index); // Eliminar el detalle de venta de la lista en sesión
                    Session["listaProductosSeleccionados"] = listaDetallesVenta; // Actualizar la lista en sesión
                    ActualizarGridView(); // Método para actualizar el GridView
                }
            }
        }

        private void ActualizarGridView()
        {
            List<DetalleVenta> listaDetallesVenta = (List<DetalleVenta>)Session["listaProductosSeleccionados"];

            // Verifica si hay detalles de venta disponibles
            if (listaDetallesVenta != null)
            {
                dgvProductosSeleccionados.DataSource = listaDetallesVenta; // Enlaza los detalles de venta al GridView
                dgvProductosSeleccionados.DataBind(); // Actualiza visualmente el GridView
                CalcularTotalVenta();
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
            listaProductos = negocio.ObtenerProductosConPrecio();
        
        }

        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string nombreProducto = txtNombre.Text.Trim();

            if (!string.IsNullOrEmpty(nombreProducto))
            {
                // Utilizar la misma lista de productos para agregar resultados de búsqueda
                ProductosNegocio negocio = new ProductosNegocio();
                listaProductos = negocio.ObtenerProductosConPrecioYnombre(nombreProducto);

                dgvProductos.DataSource = listaProductos;
                dgvProductos.DataBind();
                btnAgregarSeleccionados.Visible=true;
                btnFinalizarCompra.Visible=true;
                lblTotalVenta.Visible=true;
                lblTotal.Visible=true;
            }
        }

        private bool ProductoYaSeleccionado(int idProducto)
        {
            listaProductosSeleccionados = Session["ListaProductosSeleccionados"] as List<DetalleVenta> ?? new List<DetalleVenta>();

            return listaProductosSeleccionados.Any(p => p.IdProducto == idProducto);
        }

        protected void dgvProductosSeleccionados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtén el objeto DetalleVenta asociado a la fila actual
                DetalleVenta detalle = (DetalleVenta)e.Row.DataItem;
                // Encuentra el TextBox de cantidad en la fila actual
                TextBox txtCantidad = (TextBox)e.Row.FindControl("txtCantidad");
                Label lblSubtotal = (Label)e.Row.FindControl("lblSubtotal");

                // Asigna la cantidad al TextBox
                if (txtCantidad != null && lblSubtotal != null)
                {
                    txtCantidad.Text = detalle.Cantidad.ToString();
                    lblSubtotal.Text = detalle.Subtotal.ToString();
                }
            }
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            List<Productos> productosEnSession = Session["ListaProductos"] as List<Productos>;
            List<DetalleVenta> detalleVentaEnSession = Session["ListaProductosSeleccionados"] as List<DetalleVenta>;

            if (detalleVentaEnSession == null || detalleVentaEnSession.Any(detalle => detalle.Cantidad == 0))
            {
                lblMensajeError.Text = "Debe especificar la cantidad para todos los productos seleccionados antes de finalizar la venta.";
                return;
            }

            VentasNegocio negocio = new VentasNegocio();
            DetalleVentaNegocio ventaNegocio = new DetalleVentaNegocio();
            ProductosNegocio productosNegocio = new ProductosNegocio();
            ClientesNegocio clientesNegocio = new ClientesNegocio();
            long idVenta;

            try
            {

                foreach (DetalleVenta detalle in detalleVentaEnSession)
                {
                    Productos producto = ObtenerProductoPorId(detalle.IdProducto);

                    if (producto.StockActual < detalle.Cantidad)
                    {
                        lblMensajeError.Text = "La cantidad seleccionada de "+ detalle.NombreProducto +" excede el stock disponible.";
                        return; // Detener el proceso si hay un problema
                    }
                // Verificar si la cantidad seleccionada supera el stock mínimo
                
                }

                if (detalleVentaEnSession.Count > 0)
                {
                    string dnicliente = Session["DniCliente"].ToString();
                    //insertar venta
                    Ventas nuevaVenta = new Ventas();
                    nuevaVenta.IdCliente = (int)clientesNegocio.buscarNroCliente(dnicliente);
                    nuevaVenta.FechaVenta = DateTime.Now;
                    nuevaVenta.Estado = true;
                    nuevaVenta.TotalVenta = getTotalVenta();
                    idVenta = negocio.AgregarVenta(nuevaVenta);
                    //insertar detalle
                    insertarDetalleVenta((int)idVenta, detalleVentaEnSession);

                    //modifico stock

                    foreach (DetalleVenta detalle in detalleVentaEnSession)
                    {
                        productosNegocio.ModificarStockVenta(detalle.IdProducto, detalle.Cantidad);
                    }

                    CalcularTotalVenta();
                    //Hacer el resumen de la venta . 
                    //limpiamos las listas 
                    for (int i = detalleVentaEnSession.Count - 1; i >= 0; i--)
                    {
                        if (detalleVentaEnSession[i].Cantidad == 0)
                        {
                            detalleVentaEnSession.RemoveAt(i);
                        }
                    }

                    Session["ListaProductosSeleccionados"] = detalleVentaEnSession;
                    Response.Redirect("ResumenVenta.aspx"); // Redirección sin abortar el subproceso
                    Session["ListaProductos"] = null;
                    Session["ListaProductosSeleccionados"] = null;
                }

            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

   
        
        public decimal getTotalVenta()
        {
            decimal totalVenta;
            totalVenta = Convert.ToDecimal(lblTotalVenta.Text);
            return totalVenta; 
        }

        private void insertarDetalleVenta(int idVenta , List<DetalleVenta> detalleVentas)
        {
            DetalleVentaNegocio negocio = new DetalleVentaNegocio();
            foreach (DetalleVenta detalles in detalleVentas)
            {
                Productos nuevo = ObtenerProductoPorId(detalles.IdProducto);
                detalles.IdVenta = idVenta;
                

                negocio.insertarDetalleVenta(detalles);
            }
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
                        Productos producto = ObtenerProductoPorId(idProducto);

                        // Validar si hay suficiente stock disponible
                        if (producto.StockActual > 0)
                        {
                            DetalleVenta aux = new DetalleVenta();
                            aux.IdProducto = idProducto;
                            aux.PrecioVenta = (producto.ProductosXProveedores[0].PrecioCompra) * (producto.PorcentajeGanancia / 100 + 1);
                            aux.Subtotal = 0;
                            aux.IdVenta = 0;
                            aux.NombreProducto = producto.Nombre;

                            // Agregar el producto y detalle de venta a las listas
                            detallesVentaSession.Add(aux);
                            productosSeleccionadosSession.Add(producto);

                            // Limpiar el mensaje de error
                            lblMensajeError.Text = string.Empty;
                        }
                        else
                        {
                            lblMensajeError.Text = "El producto "+ producto.Nombre + " no tiene suficiente stock disponible.";
                            return; // Sale del método si no hay suficiente stock
                        }
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

            // Calcular los subtotales después de agregar productos
            foreach (GridViewRow row in dgvProductosSeleccionados.Rows)
            {
                CalcularYActualizarSubtotal(row);
            }
            CalcularTotalVenta();
        }



        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtCantidad.NamingContainer;
            Label lblSubtotal = (Label)row.FindControl("lblSubtotal");

            if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad >= 0)
            {
                // Obtener el producto asociado a esta fila
                int idProducto = Convert.ToInt32(row.Cells[0].Text);
                Productos producto = ObtenerProductoPorId(idProducto);

                // Verificar el stock mínimo
                if (producto.StockActual - cantidad < producto.StockMinimo)
                {
                    // Mostrar advertencia sobre el stock mínimo
                    lblMensajeAdvertencia.Text = "Advertencia: La cantidad seleccionada para el producto '" + producto.Nombre + "' está cerca o por debajo del stock mínimo. Considere comprar más.";
                    lblMensajeAdvertencia.Visible = true;
                }
                else
                {
                    // Ocultar la advertencia si no es necesaria
                    lblMensajeAdvertencia.Visible = false;
                }

                // Calcular y actualizar subtotal y total de la venta
                CalcularYActualizarSubtotal(row);
                CalcularTotalVenta();

                // Actualizar la lista de detalles de venta con la nueva cantidad
                decimal subtotal = decimal.TryParse(lblSubtotal.Text, out decimal result) ? result : 0;
                ActualizarDetallesVenta(idProducto, cantidad, subtotal);
            }
            else
            {
                // Manejar el caso en que la entrada no sea válida, por ejemplo, mostrar un mensaje de error.
                lblMensajeError.Text = "La cantidad ingresada no es válida. Por favor, ingrese un número entero no negativo.";
            }
        }

        private void CalcularYActualizarSubtotal(GridViewRow row)
        {
            decimal subtotal = CalcularSubtotal(row);
            Label lblSubtotal = (Label)row.FindControl("lblSubtotal");

            if (lblSubtotal != null)
            {
                lblSubtotal.Text = subtotal.ToString();
            }
        }
        private void ActualizarDetallesVenta(int idProducto, int nuevaCantidad, decimal subtotal)
        {
            // Obtener la lista desde la sesión o inicializarla si aún no existe
            listaProductosSeleccionados = Session["ListaProductosSeleccionados"] as List<DetalleVenta> ?? new List<DetalleVenta>();

            // Verificar si el producto ya está en la lista de detalles
            var detalleExistente = listaProductosSeleccionados.FirstOrDefault(d => d.IdProducto == idProducto);
           
            if (detalleExistente != null)
            {
                // Actualizar la cantidad si el producto ya está en la lista
                detalleExistente.Cantidad = nuevaCantidad;
                // Obtener el subtotal actualizado de la etiqueta
                detalleExistente.Subtotal = subtotal;
            
            }
            
            else
            {
                // Agregar un nuevo detalle si el producto no está en la lista
                listaProductosSeleccionados.Add(new DetalleVenta
                {
                    IdProducto = idProducto,
                    Cantidad = nuevaCantidad,
                    Subtotal = subtotal

                }); ;
            }

            // Actualizar la sesión con la nueva lista de productos seleccionados
            Session["ListaProductosSeleccionados"] = listaProductosSeleccionados;
        }
        private decimal CalcularSubtotal(GridViewRow row)
        {
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            if (txtCantidad != null)
            {
                decimal PrecioVenta = Convert.ToDecimal(row.Cells[2].Text);
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                return PrecioVenta * cantidad;
            }
            return 0;
        }

        private void CalcularSubtotales(GridViewRow row)
        {
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            Label lblSubtotal = (Label)row.FindControl("lblSubtotal");

            if (txtCantidad != null && lblSubtotal != null)
            {
                
                decimal PrecioVenta = Convert.ToDecimal(row.Cells[2].Text); 
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                decimal subtotal = PrecioVenta * cantidad;

                lblSubtotal.Text = subtotal.ToString();
                CalcularTotalVenta();
            }
        }

        private void CalcularTotalVenta()
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
                        
                    }
                }
            }

            lblTotalVenta.Text = totalVenta.ToString();
        }
  
    }
}