using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;

namespace Comercio
{
    public partial class Compras : System.Web.UI.Page
    {
        private List<Dominio.Productos> productosSeleccionados = new List<Dominio.Productos>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "No eres administrador");
                Response.Redirect("Login.aspx", false);
            }

            if (!IsPostBack)
            {
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



        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            CalcularTotalCompra();

            if (productosSeleccionados.Count > 0)
            {
                // Insertar en la tabla 'Compra'
                int idCompra = InsertarCompra();

                // Insertar en la tabla 'DetalleCompra'
                InsertarDetalleCompra(idCompra, productosSeleccionados);

                // Actualizar stock
                ActualizarStock(productosSeleccionados);

                // Otro código relacionado con finalizar la compra si es necesario
            }

        }

        private int InsertarCompra()
        {
            int id=0;
            // Implementa la lógica de inserción en la tabla 'Compra'
            // Devuelve el ID de la compra recién insertada
            return id; 
        }

        private void InsertarDetalleCompra(int idCompra, List<Dominio.Productos> productos)
        {
            // Implementa la lógica de inserción en la tabla 'DetalleCompra'
        }

        private void ActualizarStock(List<Dominio.Productos> productos)
        {
            // Implementa la lógica de actualización de stock
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

            CalcularSubtotales(row);
            CalcularTotalCompra();

            ActualizarProductosSeleccionados(row);
        }

        private void ActualizarProductosSeleccionados(GridViewRow row)
        {
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            Dominio.Productos producto = ObtenerProductoDesdeGridViewRow(row);

            if (cantidad > 0)
            {
                if (!productosSeleccionados.Contains(producto))
                {
                    productosSeleccionados.Add(producto);
                }
            }
            else
            {
                productosSeleccionados.Remove(producto);
            }
        }

        private Dominio.Productos ObtenerProductoDesdeGridViewRow(GridViewRow row)
        {
            // Implementa el código necesario para crear un objeto Dominio.Productos
            Productos producto = new Productos();

            // a partir de los valores en la GridViewRow.
            // Puedes acceder a los valores mediante los índices de las celdas.
            int idProducto = Convert.ToInt32(row.Cells[0].Text);  // Ajusta el índice según la posición de la columna IdProducto en tu GridView
                                                                  // Otros valores...
            string nombre = row.Cells[1].Text;
            decimal precioCompra = Convert.ToDecimal(row.Cells[2].Text);
            decimal porcentaje = Convert.ToDecimal(row.Cells[3].Text);
            int stockActual = Convert.ToInt32(row.Cells[4].Text);
            int stockMinimo = Convert.ToInt32(row.Cells[5].Text);
            int idMarca = Convert.ToInt32(row.Cells[6].Text);
            int  idCategoria = Convert.ToInt32(row.Cells[7].Text);
            int IdProveedor = Convert.ToInt32(row.Cells[8].Text);


            // Crea el objeto Dominio.Productos y devuélvelo
            producto.IdProductos = idProducto;
            producto.Nombre = nombre;
            producto.PrecioCompra = precioCompra;
            producto.PorcentajeGanancia = porcentaje;
            producto.StockActual = stockActual;
            producto.StockMinimo = stockMinimo;
            producto.IdMarca = idMarca;
            producto.IdCategoria = idCategoria;
            producto.IdProveedor = IdProveedor;
           
            return producto;
        }

        private void CalcularSubtotales(GridViewRow row)
        {
            TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");
            Label lblSubtotal = (Label)row.FindControl("lblSubtotal");

            if (txtCantidad != null && lblSubtotal != null)
            {
                // Acceder directamente a las celdas de GridView para obtener los valores
                decimal precioCompra = Convert.ToDecimal(row.Cells[1].Text); // Cambia el índice según la posición de la columna PrecioCompra en tu GridView
                int cantidad = Convert.ToInt32(txtCantidad.Text);
                decimal subtotal = precioCompra * cantidad;

                lblSubtotal.Text = subtotal.ToString();
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
