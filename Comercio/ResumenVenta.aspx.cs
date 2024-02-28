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
                if (Request.QueryString["id"] != null)
                {
                    int idVenta;
                    if (int.TryParse(Request.QueryString["id"], out idVenta))
                    {
                        MostrarDetallesVenta(idVenta);
                    }
                }
                else
                {
                    MostrarDetallesVenta(); // Mostrar la última venta si no se proporciona un ID en la URL
                }
            }
        }

        private void MostrarDetallesVenta(int? idVenta = null)
        {
            DetalleVentaNegocio detalleVentaNegocio = new DetalleVentaNegocio();
            VentasNegocio ventasNegocio = new VentasNegocio();
            ProductosNegocio productosNegocio = new ProductosNegocio();
            List<DetalleVenta> detallesVenta;

            int ventaId = idVenta.HasValue ? idVenta.Value : 0; // Obtener el valor entero de idVenta o 0 si es nulo

            if (ventaId != 0)
            {
                detallesVenta = detalleVentaNegocio.ObtenerDetallesPorIdVentaCompra(ventaId);

                // Asignar el nombre del producto a cada detalle de venta
                foreach (DetalleVenta detalle in detallesVenta)
                {
                    // Aquí obtienes el nombre del producto por su ID
                    detalle.NombreProducto = productosNegocio.ObtenerNombreProductoPorId(detalle.IdProducto);
                }
            }
            else
            {
                // Obtén los detalles de la última venta de la sesión
                detallesVenta = Session["listaProductosSeleccionados"] as List<DetalleVenta>;

                // Si no hay detalles de venta disponibles, muestra un mensaje de error o maneja el caso según sea necesario
                if (detallesVenta == null || detallesVenta.Count == 0)
                {
                    // Manejo de error si no hay detalles de venta disponibles
                    return;
                }

                // Obtener el ID de venta de la última instancia de DetalleVenta en la lista
                ventaId = detallesVenta.Last().IdVenta;

                // Asignar el nombre del producto a cada detalle de venta
                foreach (DetalleVenta detalle in detallesVenta)
                {
                    // Aquí obtienes el nombre del producto por su ID
                    detalle.NombreProducto = productosNegocio.ObtenerNombreProductoPorId(detalle.IdProducto);
                }
            }

            // Utilizar el ID de la venta para obtener el nombre del cliente
            string nombreCliente = ventasNegocio.ObtenerNombreClientePorIdVenta(ventaId);
            lblNombreCliente.Text = nombreCliente;

            // Enlazar los detalles de venta al GridView
            gvDetallesVenta.DataSource = detallesVenta;
            gvDetallesVenta.DataBind();
        }


        protected void btnIrAPaginaPrincipal_Click(object sender, EventArgs e)
        {
            // Redirigir a la página principal (cambia la URL según tu estructura de proyecto)
            Session["ListaProductos"] = null;
            Session["listaProductosSeleccionados"] = null;
            Session["DetallesVenta"] = null;

            if (Request.QueryString["id"] != null)
            {
                Response.Redirect("RegistrosVentas.aspx");
            }
            else { Response.Redirect("~/DefaultVendedor.aspx"); }
        }

        protected void gvDetallesVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<DetalleVenta> detallesVenta = null;
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (Request.QueryString["id"] != null)
                {
                    detallesVenta = Session["DetallesVenta"] as List<DetalleVenta>;
                }
                else
                {
                    detallesVenta = Session["listaProductosSeleccionados"] as List<DetalleVenta>;
                }

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
            List<DetalleVenta> detallesVenta = null;
            decimal total = 0;

            // Obtener el ID de venta de la URL
            if (Request.QueryString["id"] != null)
            {
                int idVenta;
                if (int.TryParse(Request.QueryString["id"], out idVenta))
                {
                    // Obtener los detalles de la venta por el ID de venta
                    DetalleVentaNegocio detalleVentaNegocio = new DetalleVentaNegocio();
                    detallesVenta = detalleVentaNegocio.ObtenerDetallesPorIdVentaCompra(idVenta);
                }
            }

            // Si no se encontraron detalles de venta por ID de venta, intenta obtenerlos de la lista de productos seleccionados
            if (detallesVenta == null || detallesVenta.Count == 0)
            {
                detallesVenta = Session["listaProductosSeleccionados"] as List<DetalleVenta>;
            }

            // Verificar si se encontraron detalles de la venta
            if (detallesVenta != null && detallesVenta.Count > 0)
            {
                // Calcular el total
                total = detallesVenta.Sum(detalle => detalle.Subtotal);
            }
            else
            {
                // Manejar el caso en el que no se encuentren detalles de la venta
                // Puedes mostrar un mensaje de error o redirigir a una página de error
                return;
            }

            // Generar el contenido HTML de la factura
            string contenidoHTML = GenerarContenidoFactura(detallesVenta, total);

            // Limpiar las sesiones
            Session["listaProductos"] = null;
            Session["listaProductosSeleccionados"] = null;

            // Configurar la respuesta HTTP para descargar el archivo
            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename=Factura.html");
            Response.Write(contenidoHTML);
            Response.End();
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
                //Productos producto = ObtenerProductoPorId(detalle.IdProducto);

                sb.AppendLine($"<p>ID Producto: {detalle.IdProducto}</p>");
                sb.AppendLine($"<p>Nombre Producto: {detalle.NombreProducto}</p>");
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