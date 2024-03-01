using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Comercio
{
    public partial class Compra : System.Web.UI.Page
    {
        private List<Productos> productosSeleccionados = new List<Productos>();

        private List<DetalleCompra> detallesCompra = new List<DetalleCompra>();

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
                Session["productosSeleccionados"] = null;
                Session["detallesCompra"] = null;
                CargarProveedor();
                CargarCategorias();
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

        private void CargarCategorias()
        {
            CategoriasNegocio categorias = new CategoriasNegocio();
            ddlCat1.DataSource = categorias.ListarCategorias();
            ddlCat1.DataTextField = "Nombre";
            ddlCat1.DataValueField = "IdCategoria";
            ddlCat1.DataBind();

            ddlCat1.Items.Insert(0, new ListItem("-- Seleccione una categoria --", ""));
            ddlCat1.Items.Insert(1, new ListItem("-- Todos --", "0"));
        }

        private void BindGridViewDataProveedor(int idProveedor, int idCat)
        {
            if (idProveedor > 0)
            {
                if (ddlCat1.SelectedValue == "0") // "0" es el valor asignado a la opción "Todos"
                {
                    // Lógica para cargar todos los productos
                    BindGridViewData();
                }
                else
                {
                    // Llama al método para cargar los productos asociados al proveedor y la categoría seleccionados
                    ProductosNegocio negocio = new ProductosNegocio();
                    List<Dominio.Productos> listaProductos = negocio.ListarProductosPorProveedor(idProveedor, idCat);

                    dataGridViewProductos1.DataSource = listaProductos;
                    dataGridViewProductos1.DataBind();
                }
            }
            else
            {
                dataGridViewProductos1.DataSource = null;
                dataGridViewProductos1.DataBind();
            }
        }

        private int ObtenerCantidadDesdeSesion(GridViewRow row)
        {
            // Obtener el Id del producto desde la GridViewRow
            int idProducto = Convert.ToInt32(row.Cells[0].Text);

            // Obtener la lista de detalles de compra desde la sesión o devolver 0 si no está presente
            List<DetalleCompra> detallesCompraEnSesion = Session["detallesCompra"] as List<DetalleCompra>;

            if (detallesCompraEnSesion != null)
            {
                // Buscar el detalle correspondiente al producto actual
                DetalleCompra detalleProducto = detallesCompraEnSesion.FirstOrDefault(detalle => detalle.IdProducto == idProducto);

                if (detalleProducto != null)
                {
                    // Devolver la cantidad del detalle encontrado
                    return detalleProducto.Cantidad;
                }
            }

            return 0;
        }


        private void BindGridViewData()
        {
            ProductosNegocio negocio = new ProductosNegocio();
            int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);
            List<Dominio.Productos> listaProductos = negocio.TodoslosProductosPorProveedor(idProveedor);
            dataGridViewProductos1.DataSource = listaProductos;
            dataGridViewProductos1.DataBind();
        }


        protected void dataGridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewProductos1.PageIndex = e.NewPageIndex;
            BindGridViewData();
        }


        protected void dataGridViewProductos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = dataGridViewProductos1.DataKeys[e.RowIndex].Value.ToString();
            ProductosNegocio negocio = new ProductosNegocio();
            negocio.EliminarProducto(int.Parse(ID));
            BindGridViewData();
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlProveedor.SelectedValue))
            {
                int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

                // Verifica si se ha seleccionado una categoría antes de obtener su valor
                if (!string.IsNullOrEmpty(ddlCat1.SelectedValue))
                {
                    int idCat = Convert.ToInt32(ddlCat1.SelectedValue);

                    // Llama al método para cargar los productos asociados al proveedor y la categoría seleccionados
                    BindGridViewDataProveedor(idProveedor, idCat);
                }
                else
                {

                }

                // Deshabilita el DropDownList después de seleccionar un proveedor
                ddlProveedor.Enabled = false;
                Session["CompraPage"] = true;
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
            // Verificar si hay algún detalle de compra en la sesión
            if (Session["detallesCompra"] != null)
            {
                
                // Verificar si la lista de detalles de compra no es nula y si tiene al menos un elemento
                if (detalleComprasSession != null || detalleComprasSession.Count > 0)
                {
                    Dominio.Compras nuevaCompra = new Dominio.Compras();
                    nuevaCompra.IdProveedor = ProveedorSeleccionado();
                    nuevaCompra.FechaCompra = DateTime.Now;
                    nuevaCompra.Estado = true;
                    nuevaCompra.TotalCompra = TotalDeCompra();

                    idCompra = compra.AgregarCompra(nuevaCompra);

                   

                    foreach (DetalleCompra detalle in detalleComprasSession)
                    {

                        productos.ModificarStock(detalle.IdProducto, detalle.Cantidad);
                        ActualizarProductos(productosSeleccionadosSession, detalle.IdProducto, detalle.Cantidad);
                    }

                    ActualizarDetalle(detalleComprasSession);

                    // Insertar en la tabla DetalleCompra
                    InsertarDetalleCompra((int)idCompra, detalleComprasSession);

                    // Actualizar las etiquetas de subtotal y total de compra después de la inserción
                    CalcularTotalCompra();

                    // Recargar la página después de la inserción
                    //Response.Redirect(Request.RawUrl);
                    Session["CompraPage"] = true; // Para la página de ventas
                    Response.Redirect("ResumenCompra.aspx");
                }
                else
                {
                    // Mostrar un mensaje de error si no hay detalles de compra en la sesión
                    lblError.Text = "No se puede finalizar una compra sin productos.";
                    lblError.Visible = true;
                }
            }
            else
            {
                // Mostrar un mensaje de error si no hay detalles de compra en la sesión
                lblError.Text = "No se puede finalizar una compra sin productos.";
                lblError.Visible = true;
            }
            Session["productosSeleccionados"] = null;
            // Limpiar la lista de detalles de compra en la sesión
            Session["detallesCompra"] = null;
        }

        private void ActualizarDetalle(List<DetalleCompra> detalleComprasSession)
        {
            // Iterar sobre la lista de detalleComprasSession usando un bucle for
            for (int i = 0; i < detalleComprasSession.Count; i++)
            {
                // Obtener el detalle de compra en la posición i
                DetalleCompra detalle = detalleComprasSession[i];

                // Verificar si la cantidad es igual a 0
                if (detalle.Cantidad == 0)
                {
                    // Si la cantidad es 0, eliminar el elemento de la lista
                    detalleComprasSession.RemoveAt(i);

                    // Decrementar el índice para ajustar el desplazamiento causado por la eliminación del elemento
                    i--;
                }
            }
        }

        private void ActualizarProductos(List<Productos> productosSeleccionadosSession, int id, int cantidad)
        {
            // Iterar sobre los productos en la lista productosSeleccionadosSession
            for (int i = 0; i < productosSeleccionadosSession.Count; i++)
            {
                // Obtener el producto actual
                Productos producto = productosSeleccionadosSession[i];

                // Verificar si el ID del producto coincide con el ID a eliminar
                if (producto.IdProductos == id && cantidad == 0)
                {
                    // Eliminar el producto de la lista
                    productosSeleccionadosSession.RemoveAt(i);
                    // Como ya encontramos y eliminamos el producto, no necesitamos seguir iterando
                    break;
                }
            }
        }




        private Productos ObtenerProductoPorId(int idProducto)
        {
           
            List<Productos> productosSeleccionados = Session["productosSeleccionados"] as List<Productos>;
           
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

                // Obtener la cantidad desde la sesión o establecerla en 0 si no está presente
                int cantidadDesdeSesion = ObtenerCantidadDesdeSesion(e.Row);

                // Establecer el valor del TextBox con la cantidad obtenida
                txtCantidad.Text = cantidadDesdeSesion.ToString();
            }

        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCantidad = (TextBox)sender;
            GridViewRow row = (GridViewRow)txtCantidad.NamingContainer;
            
            if (int.TryParse(txtCantidad.Text, out int cantidad) && cantidad >= 0)
            {
                CalcularSubtotales(row);
                ActualizarProductosSeleccionados(row);
                CalcularTotalCompra();  // Mover la llamada aquí

                lblMensajeError1.Text = "";
                int cant = productosSeleccionados.Count;
            }
            else
            {
                // Manejar el caso en que la entrada no sea válida, por ejemplo, mostrar un mensaje de error.
                lblMensajeError1.Text = "La cantidad ingresada no es válida. Por favor, ingrese un número entero no negativo.";
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
            decimal precioCompra = Convert.ToDecimal(row.Cells[7].Text);
            decimal porcentaje = Convert.ToDecimal(row.Cells[2].Text);
            int stockActual = Convert.ToInt32(row.Cells[3].Text);
            int stockMinimo = Convert.ToInt32(row.Cells[4].Text);
            int idMarca = Convert.ToInt32(row.Cells[5].Text);
            int idCategoria = Convert.ToInt32(row.Cells[6].Text);
            int IdProveedor = Convert.ToInt32(row.Cells[8].Text);



            // Crea el objeto Productos y devuelve
            producto.IdProductos = idProducto;
            producto.Nombre = nombre;
            producto.PorcentajeGanancia = porcentaje;
            producto.StockActual = stockActual;
            producto.StockMinimo = stockMinimo;
            producto.IdMarca = idMarca;
            producto.IdCategoria = idCategoria;
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
            decimal precioCompra = Convert.ToDecimal(row.Cells[7].Text);
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
                decimal precioCompra = Convert.ToDecimal(row.Cells[7].Text); // Cambia el índice según la posición de la columna PrecioCompra en tu GridView
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                decimal subtotal = precioCompra * cantidad;

                lblSubtotal.Text = subtotal.ToString();
            }
        }

        private void CalcularTotalCompra()
        {
            decimal totalCompra = 0;

            // Obtener la lista de detalles de compra desde la sesión
            List<DetalleCompra> detallesCompraSession = Session["detallesCompra"] as List<DetalleCompra>;

            if (detallesCompraSession != null)
            {
                // Calcular el total de compra sumando los subtotales de la lista en sesión
                totalCompra = detallesCompraSession.Sum(detalle => detalle.Subtotal);
            }

            lblTotalCompra.Text = totalCompra.ToString();
            UpdatePanel2.Update();
        }
        protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(ddlProveedor.SelectedValue) && !string.IsNullOrEmpty(ddlCat1.SelectedValue))
            {
                int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);
                int idCat = Convert.ToInt32(ddlCat1.SelectedValue);

                // Llama al método para cargar los productos asociados al proveedor y la categoría seleccionados
                BindGridViewDataProveedor(idProveedor, idCat);
            }
        }

    }
}