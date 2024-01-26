using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
//jovenes promesasas

namespace Comercio
{
    public partial class Compras : System.Web.UI.Page
    {
        private List<Productos> productosSeleccionados
        {
            get
            {
                if (ViewState["productosSeleccionados"] == null)
                    ViewState["productosSeleccionados"] = new List<Productos>();
                return (List<Productos>)ViewState["productosSeleccionados"];
            }
            set { ViewState["productosSeleccionados"] = value; }
        }

        private List<DetalleCompra> detallesCompra
        {
            get
            {
                if (ViewState["detallesCompra"] == null)
                    ViewState["detallesCompra"] = new List<DetalleCompra>();
                return (List<DetalleCompra>)ViewState["detallesCompra"];
            }
            set { ViewState["detallesCompra"] = value; }
        }
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
            CalcularTotalCompra();

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


        public decimal TotalDeCompra()
        {
            decimal totalCompra = 0;

            totalCompra = Convert.ToDecimal(lblTotalCompra.Text);


            return totalCompra;
        }




        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            ComprasNegocio compra = new ComprasNegocio();
            long idCompra;
            ProductosNegocio productos = new ProductosNegocio();

            if (productosSeleccionados.Count > 0)
            {
                Dominio.Compras nuevaCompra = new Dominio.Compras();
                nuevaCompra.IdProveedor = ProveedorSeleccionado();
                nuevaCompra.FechaCompra = DateTime.Now;
                nuevaCompra.Estado = true;
                nuevaCompra.TotalCompra = TotalDeCompra();

                idCompra = compra.AgregarCompra(nuevaCompra);

                InsertarDetalleCompra((int)idCompra, detallesCompra);

                foreach (DetalleCompra detalle in detallesCompra)
                {
                    productos.ModificarStock(detalle.IdProducto, detalle.Cantidad);
                }

                productosSeleccionados.Clear();
                detallesCompra.Clear();

                // Actualizar las etiquetas de subtotal y total de compra después de la inserción
                //UpdatePanel1.Update();
                lblMensajeError.Text = "";
            }
        }





        private void InsertarDetalleCompra(int idCompra, List<DetalleCompra> detallesCompra)
        {
            DetalleCompraNegocio negocio = new DetalleCompraNegocio();
            foreach (DetalleCompra detalles in detallesCompra)
            {

                detalles.IdCompra = idCompra;


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

                // Actualizar el UpdatePanel después de cada cambio para reflejar los subtotales y el total de la compra.
                //UpdatePanel1.Update();

                ActualizarProductosSeleccionados(row);
                lblMensajeError.Text = "";
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

            if (cantidad > 0)
            {
                if (!productosSeleccionados.Contains(producto))
                {
                    productosSeleccionados.Add(producto);
                    detallesCompra.Add(detalle);
                }
            }
            else
            {
                productosSeleccionados.Remove(producto);
                detallesCompra.Remove(detalle);
            }
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
                decimal precioCompra = Convert.ToDecimal(row.Cells[2].Text);
                decimal porcentajeVenta = Convert.ToDecimal(row.Cells[3].Text);
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                decimal subtotal = (precioCompra + (precioCompra * porcentajeVenta / 100)) * cantidad;

                lblSubtotal.Text = subtotal.ToString();

                // Actualizar el total de la compra después de calcular los subtotales
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
        }

    }
}

