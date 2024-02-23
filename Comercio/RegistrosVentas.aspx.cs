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
    public partial class RegistrosVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.vendedor))
            {
                Session.Add("Error", "No eres vendedor");
                Response.Redirect("Login.aspx", false);
            }

            // Verificar si es la carga inicial de la página
            if (!IsPostBack)
            {
                ListarVentas();
            }
        }

        protected void ListarVentas()
        {
            try
            {           
                VentasNegocio negocio = new VentasNegocio();
                string idVenta = string.IsNullOrEmpty(txtIdVenta.Text) ? "" : txtIdVenta.Text.Trim();
                List<Dominio.Ventas> listaVentas = negocio.ListarVentas(idVenta);
                dataGridViewVentas.DataSource = listaVentas;
                dataGridViewVentas.DataBind();
            }
            catch (Exception ex)
            {
                // Manejar la excepción de acuerdo a tu lógica de negocio
                throw ex;
            }
        }

        protected void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            ListarVentas();
        }

        protected void dataGridViewVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewVentas.PageIndex = e.NewPageIndex;
            ListarVentas();
        }

        protected void dataGridViewVentas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dataGridViewVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener el ID del proveedor para la fila actual
                int idCliente = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdCliente"));
                ClientesNegocio negocio = new ClientesNegocio();
                // Obtener el nombre del proveedor usando el ID
                string nombreProveedor = negocio.ObtenerNombreClientePorId(idCliente);

                // Encontrar el Label dentro de la fila actual
                Label lblCliente = (Label)e.Row.FindControl("lblCliente");

                // Asignar el nombre del proveedor al Label
                lblCliente.Text = nombreProveedor;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Calcular el total facturado utilizando LINQ
                decimal totalFacturado = ((List<Dominio.Ventas>)dataGridViewVentas.DataSource)
                    .Sum(Venta => Venta.TotalVenta);

                // Mostrar el total facturado en el Label
                lblTotalFacturado.Text = $"Total Facturado: {totalFacturado.ToString("C")}";
            }
        }

        protected void dataGridViewVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detallesVenta")
            {
                // Obtener el ID de la compra seleccionada
                int idVenta = Convert.ToInt32(e.CommandArgument);

                DetalleVentaNegocio detalleVentaNegocio = new DetalleVentaNegocio();
                
                // Obtener el índice de la fila seleccionada
                int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
                GridViewRow selectedRow = dataGridViewVentas.Rows[rowIndex];

                // Buscar el control Label dentro de la fila para obtener el nombre del cliente
                Label lblCliente = (Label)selectedRow.FindControl("lblCliente");
                string nombreCliente = lblCliente.Text;

                // Obtener la lista de detalles de compra por ID de venta
                List<DetalleVenta> detallesVenta = detalleVentaNegocio.ObtenerDetallesPorIdVenta(idVenta, nombreCliente);


                // Crear una lista para almacenar los productos
                List<Productos> productosSeleccionados = new List<Productos>();
                ProductosNegocio negocioProducto = new ProductosNegocio();

                // Obtener los productos asociados a cada detalle de venta
                foreach (var detalle in detallesVenta)
                {
                    // Obtener el producto asociado al detalle de venta
                    Productos producto = negocioProducto.ObtenerProductoPorId(detalle.IdProducto);

                    // Verificar si se encontró el producto
                    if (producto != null)
                    {
                        // Agregar el producto a la lista de productos seleccionados
                        productosSeleccionados.Add(producto);
                    }
                }

                // Almacenar la lista de productos en la sesión
                Session["productosSeleccionados"] = productosSeleccionados;

                // Almacenar la lista de detalles de venta en la sesión
                Session["detallesVenta"] = detallesVenta;

                // Redireccionar a la página de resumen de la compra
                Response.Redirect($"ResumenVenta.aspx?id={idVenta}");
            }
        }
    }
}