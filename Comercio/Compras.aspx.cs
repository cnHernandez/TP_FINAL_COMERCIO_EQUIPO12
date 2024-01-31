using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using System.Linq;
//jovenes promesasas

namespace Comercio
{
    public partial class Compras : System.Web.UI.Page
    {
        private List<Productos> productosSeleccionados=new List<Productos>();

        private List<DetalleCompra> detallesCompra=new List<DetalleCompra>(); 
       
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario es un administrador
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "No eres administrador");
                Response.Redirect("Login.aspx", false);
            }

            // Verificar si es la carga inicial de la página
            if (!IsPostBack)
            {
                // Cargar proveedores solo en la carga inicial
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

        private void BindGridViewDataProveedor(int idProveedor)
        {
            if (idProveedor > 0)
            {
                ProductosNegocio negocio = new ProductosNegocio();
                List<Dominio.Productos> listaProductos = negocio.ListarProductosPorProveedor(idProveedor);

                dataGridViewProductos.DataSource = listaProductos;
                dataGridViewProductos.DataBind();
            }
            else
            {
                dataGridViewProductos.DataSource = null;
                dataGridViewProductos.DataBind();
            }
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

                // Deshabilita el DropDownList después de seleccionar un proveedor
                ddlProveedor.Enabled = false;
            }
        }


        private int ProveedorSeleccionado()
        {
            int idProveedor = 0;
            if (!string.IsNullOrEmpty(ddlProveedor.SelectedValue))
            {
                idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);
            }
            return idProveedor;
        }

        private string NombreProveedorSeleccionado()
        {
            string nombreProveedor = "";
            if (!string.IsNullOrEmpty(ddlProveedor.SelectedValue))
            {
                nombreProveedor = ddlProveedor.SelectedItem.Text;
            }
            return nombreProveedor;
        }


        public decimal TotalDeCompra()
        {
            decimal totalCompra = 0;

            totalCompra = Convert.ToDecimal(lblTotalCompra.Text);
                

            return totalCompra;
        }




        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            List<Productos> productosSeleccionadosSession = Session["productosSeleccionados"] as List<Productos>;
            List<DetalleCompra> detalleComprasSession = Session["detallesCompra"] as List<DetalleCompra>;
            // CalcularTotalCompra(); // Esto no es necesario ya que se vuelve a calcular en el Page_Load
            ComprasNegocio compra = new ComprasNegocio();
            ProductosNegocio productos = new ProductosNegocio();
            long idCompra;
            // agrego a la tabla compra 



            if (productosSeleccionadosSession.Count > 0)
            {
                Dominio.Compras nuevaCompra = new Dominio.Compras();
                nuevaCompra.IdProveedor = ProveedorSeleccionado();               
                nuevaCompra.FechaCompra = DateTime.Now;
                nuevaCompra.Estado = true;
                nuevaCompra.TotalCompra = TotalDeCompra();

                idCompra = compra.AgregarCompra(nuevaCompra);

                // Insertar en la tabla DetalleCompra
                InsertarDetalleCompra((int)idCompra, detalleComprasSession);
                foreach (DetalleCompra  detalle in detalleComprasSession)
                {
                    
                    productos.ModificarStock(detalle.IdProducto, detalle.Cantidad);
                }


               

                // Actualizar las etiquetas de subtotal y total de compra después de la inserción
                CalcularTotalCompra();


                // Recargar la página después de la inserción
                //Response.Redirect(Request.RawUrl);
                Response.Redirect("ResumenCompra.aspx");
            }
                // Limpiar la lista de productos seleccionados en la sesión
                Session["productosSeleccionados"] = null;
                // Limpiar la lista de detalles de compra en la sesión
                Session["detallesCompra"] = null;
            // No es necesario repetir Response.Redirect aquí
        }

        private Productos ObtenerProductoPorId(int idProducto)
        {
            // Supongamos que tienes una lista de productos llamada 'listaProductos'
            // y que Producto tiene propiedades IdProducto y NombreProducto
            List<Productos> productosSeleccionados = Session["productosSeleccionados"] as List<Productos>;
            // Asegúrate de tener la lógica adecuada para obtener el producto desde tu fuente de datos
            Productos productoEncontrado = productosSeleccionados.FirstOrDefault(p => p.IdProductos == idProducto);

            return productoEncontrado ?? new Productos(); // Manejo de caso cuando el producto no se encuentra
        }



        private void InsertarDetalleCompra(int idCompra, List<DetalleCompra> detallesCompra)
        {
            DetalleCompraNegocio negocio = new DetalleCompraNegocio();
            foreach (DetalleCompra detalles in detallesCompra)
            {
            Productos producto = ObtenerProductoPorId(detalles.IdProducto);
                
                detalles.IdCompra = idCompra;
                detalles.NombreProveedor = NombreProveedorSeleccionado();
                detalles.NombreProducto = producto.Nombre;
               
               

                negocio.InsertarDetalleCompra(detalles);

              
            }

        }

      

        protected void dataGridViewProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtCantidad = (TextBox)e.Row.FindControl("txtCantidad");
                txtCantidad.TextChanged += new EventHandler(txtCantidad_TextChanged);
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtCantidad.NamingContainer;

            if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad >= 0)
            {
                CalcularSubtotales(row);
                CalcularTotalCompra();

                ActualizarProductosSeleccionados(row);
                lblMensajeError.Text = "";
                int cant = productosSeleccionados.Count;
            }
            else
            {
                // Manejar el caso en que la entrada no sea válida, por ejemplo, mostrar un mensaje de error.
                lblMensajeError.Text = "La cantidad ingresada no es válida. Por favor, ingrese un número entero no negativo.";
            }
        }


        private void ActualizarProductosSeleccionados(GridViewRow row)
        {
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            Productos producto = ObtenerProductoDesdeGridViewRow(row);
            DetalleCompra detalle = ObtenerDetalleCompraDesdeGridViewRow(row);

            // Obtener las listas desde la sesión o inicializarlas si aún no existen
            List<Productos> productosSeleccionadosSession = Session["productosSeleccionados"] as List<Productos>;
            List<DetalleCompra> detalleComprasSession = Session["detallesCompra"] as List<DetalleCompra>;

            if (productosSeleccionadosSession == null)
            {
                productosSeleccionadosSession = new List<Productos>();
            }

            if (detalleComprasSession == null)
            {
                detalleComprasSession = new List<DetalleCompra>();
            }

            // Verificar si el producto ya está en la lista
            int index = productosSeleccionadosSession.FindIndex(p => p.IdProductos == producto.IdProductos);

            if (index != -1)
            {
                // Si el producto ya existe, actualizar la cantidad
                detalleComprasSession[index] = detalle;
                productosSeleccionadosSession[index] = producto;
            }
            else
            {
                // Si el producto no existe, agregarlo a la lista
                productosSeleccionadosSession.Add(producto);
                detalleComprasSession.Add(detalle);
            }

            // Guardar las listas actualizadas en la sesión
            Session["productosSeleccionados"] = productosSeleccionadosSession;
            Session["detallesCompra"] = detalleComprasSession;

        }





        ///Este es para crear en objetos (producto) de  la lista que tenemos en el gridview
        private Productos ObtenerProductoDesdeGridViewRow(GridViewRow row)
        {
            // Implementa el código necesario para crear un objeto Dominio.Productos
            Productos producto = new Productos();

            // a partir de los valores en la GridViewRow.
            // Puedes acceder a los valores mediante los índices de las celdas.
            int idProducto = Convert.ToInt32(row.Cells[0].Text);  // Ajusta el índice según la posición de la columna IdProducto en tu GridView
                                                                  
            string nombre = row.Cells[1].Text;
            decimal precioCompra = Convert.ToDecimal(row.Cells[2].Text);
            decimal porcentaje = Convert.ToDecimal(row.Cells[3].Text);
            int stockActual = Convert.ToInt32(row.Cells[4].Text);
            int stockMinimo = Convert.ToInt32(row.Cells[5].Text);
            int idMarca = Convert.ToInt32(row.Cells[6].Text);
            int idCategoria = Convert.ToInt32(row.Cells[7].Text);
            int IdProveedor = Convert.ToInt32(row.Cells[8].Text);
          


            // Crea el objeto Productos y devuelve
            producto.IdProductos = idProducto;
            producto.Nombre = nombre;
            producto.PrecioCompra = precioCompra;
            producto.PorcentajeGanancia = porcentaje;
            producto.StockActual = stockActual;
            producto.StockMinimo = stockMinimo;
            producto.IdMarca = idMarca;
            producto.IdCategoria = idCategoria;
            producto.IdProveedor = IdProveedor;
            producto.Estado = true;

            return producto;
        }

        //este es para que convierta en objetos de detalle compra lo que esta en el dgv
        private DetalleCompra ObtenerDetalleCompraDesdeGridViewRow(GridViewRow row)
        {
            DetalleCompra detalleCompra = new DetalleCompra();

            // Ajusta los índices según la posición de las columnas en tu GridView
            int idProducto = Convert.ToInt32(row.Cells[0].Text);
            int cantidad = Convert.ToInt32(((TextBox)row.FindControl("txtCantidad")).Text);
            decimal precioCompra = Convert.ToDecimal(row.Cells[2].Text);
            decimal subtotal = Convert.ToDecimal(((Label)row.FindControl("lblSubtotal")).Text);

            
            detalleCompra.IdProducto = idProducto;
            detalleCompra.Cantidad = cantidad;
            detalleCompra.PrecioCompra = precioCompra;
            detalleCompra.Subtotal = subtotal;

            return detalleCompra;
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
                decimal subtotal = precioCompra * cantidad;

                lblSubtotal.Text = subtotal.ToString();
                CalcularTotalCompra();
            }
        }
        private void CalcularTotalCompra()
        {
            decimal totalCompra = 0;

            foreach (GridViewRow row in dataGridViewProductos.Rows)
            {
                Label lblSubtotal = (Label)row.FindControl("lblSubtotal");

                if (lblSubtotal != null)
                {
                    decimal subtotal;

                    if (decimal.TryParse(lblSubtotal.Text, out subtotal))
                    {
                        totalCompra += subtotal;
                    }
                    else
                    {
                        // Manejo de error: Puedes mostrar un mensaje o tomar otra acción en caso de que haya un problema con el formato.
                    }
                }
            }

            lblTotalCompra.Text = totalCompra.ToString();
            UpdatePanel1.Update();
        }

    }
}

