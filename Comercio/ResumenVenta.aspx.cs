using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class ResumenVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarDetallesVenta();
            }
        }

        private void MostrarDetallesVenta()
        {
            // Obtén los detalles de la venta de la sesión
            List<DetalleVenta> detallesVenta = Session["listaProductosSeleccionados"] as List<DetalleVenta>;

            // Verifica si hay detalles de venta disponibles
            if (detallesVenta != null && detallesVenta.Count > 0)
            {
                // Obtener el ID de venta de la última instancia de DetalleVenta en la lista
                int idVenta = detallesVenta.Last().IdVenta;

                // Utilizar el ID de la venta para obtener el nombre del cliente
                VentasNegocio ventasNegocio = new VentasNegocio();
                string nombreCliente = ventasNegocio.ObtenerNombreClientePorIdVenta(idVenta);

                // Asigna el nombre del cliente al Label correspondiente
                lblNombreCliente.Text = nombreCliente;

                ProductosNegocio productosNegocio = new ProductosNegocio();

                // Iterar sobre cada detalle de venta y asignar el nombre del cliente
                foreach (DetalleVenta detalle in detallesVenta)
                {
                    string nombre = productosNegocio.ObtenerNombreProductoPorId(detalle.IdProducto);
                    detalle.NombreProducto = nombre;
                    detalle.NombreProveedor = "Mercado Util"; // Asignar el nombre del proveedor
                }

                // Enlaza los detalles de venta al GridView
                gvDetallesVenta.DataSource = detallesVenta;
                gvDetallesVenta.DataBind();
            }
        }


        protected void btnIrAPaginaPrincipal_Click(object sender, EventArgs e)
        {
            // Redirigir a la página principal (cambia la URL según tu estructura de proyecto)
            Response.Redirect("~/DefaultVendedor.aspx");
        }

        protected void gvDetallesVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                List<DetalleVenta> detallesVenta = Session["listaProductosSeleccionados"] as List<DetalleVenta>;

                if (detallesVenta != null && detallesVenta.Count > 0)
                {
                    decimal total = detallesVenta.Sum(detalle => detalle.Subtotal);

                    // Crear una nueva fila en el pie de página
                    GridViewRow footerRow = new GridViewRow(0, 0, DataControlRowType.Footer, DataControlRowState.Normal);

                    // Crear una nueva celda y establecer el texto
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = 4; // Ajusta el número de columnas según tus necesidades
                    cell.Text = "Total:";
                    cell.CssClass = "footer-label"; // Puedes agregar una clase CSS si es necesario
                    footerRow.Cells.Add(cell);

                    // Crear otra celda para mostrar el total
                    TableCell totalCell = new TableCell();
                    totalCell.Text = total.ToString("C"); // Formato de moneda
                    totalCell.CssClass = "footer-total"; // Puedes agregar una clase CSS si es necesario
                    footerRow.Cells.Add(totalCell);

                    // Agregar la fila al GridView
                    gvDetallesVenta.Controls[0].Controls.Add(footerRow);
                }
            }
        }

        protected void btnDescargarFactura_Click(object sender, EventArgs e)
        {
            List<DetalleVenta> detallesVenta = Session["listaProductosSeleccionados"] as List<DetalleVenta>;
            if (detallesVenta != null && detallesVenta.Count > 0)
            {
                decimal total = detallesVenta.Sum(detalle => detalle.Subtotal);

                string contenidoHTML = GenerarContenidoFactura(detallesVenta, total);

                // Configuración de la respuesta HTTP para descargar el archivo
                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment; filename=Factura.html");
                Response.Write(contenidoHTML);
                Response.End();
            }
        }

        protected string GenerarContenidoFactura(List<DetalleVenta> detallesVenta, decimal total)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<head><title>Factura</title></head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h1>Factura</h1>");

            // Agregar la fecha actual al encabezado
            sb.AppendLine($"<p>Fecha: {DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss")}</p>");

            sb.AppendLine("<h2>Detalles de la venta:</h2>");

            foreach (DetalleVenta detalle in detallesVenta)
            {
                // Supongamos que tienes una lista de productos con IdProducto y NombreProducto
                Productos producto = ObtenerProductoPorId(detalle.IdProducto);

                sb.AppendLine($"<p>ID Producto: {detalle.IdProducto}</p>");
                sb.AppendLine($"<p>Nombre Producto: {producto.Nombre}</p>");
                sb.AppendLine($"<p>Cantidad: {detalle.Cantidad}</p>");
                sb.AppendLine($"<p>Precio Unitario: {detalle.PrecioVenta.ToString("C")}</p>");
                sb.AppendLine($"<p>Subtotal: {detalle.Subtotal.ToString("C")}</p>");
                sb.AppendLine("<hr/>");
            }

            sb.AppendLine($"<p>Total: {total.ToString("C")}</p>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        private Productos ObtenerProductoPorId(int idProducto)
        {
            // Supongamos que tienes una lista de productos llamada 'listaProductos'
            // y que Producto tiene propiedades IdProducto y NombreProducto
            List<Productos> productosSeleccionados = Session["listaProductos"] as List<Productos>;
            // Asegúrate de tener la lógica adecuada para obtener el producto desde tu fuente de datos
            Productos productoEncontrado = productosSeleccionados.FirstOrDefault(p => p.IdProductos == idProducto);

            return productoEncontrado ?? new Productos(); // Manejo de caso cuando el producto no se encuentra
        }
    }
}