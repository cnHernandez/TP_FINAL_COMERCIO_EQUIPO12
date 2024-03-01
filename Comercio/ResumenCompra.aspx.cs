using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Text;

namespace Comercio
{
    public partial class ResumenCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarDetallesCompra();
            }

        }
        private void MostrarDetallesCompra()
        {
            // Obtén los detalles de la compra de la sesión
            List<DetalleCompra> detallesCompra = Session["detallesCompra"] as List<DetalleCompra>;

            // Verifica si hay detalles de compra disponibles
            if (detallesCompra != null && detallesCompra.Count > 0)
            {
                // Enlaza los detalles de compra al GridView
                gvDetallesCompra.DataSource = detallesCompra;
                gvDetallesCompra.DataBind();

            }
        }
        protected void btnIrAPaginaPrincipal_Click(object sender, EventArgs e)
        {

            // Redirigir a la página principal 
            
            Session["productosSeleccionados"] = null;
            Session["detallesCompra"] = null;

           
            if (Request.QueryString["id"] != null)
            {
                Response.Redirect("RegistrosCompras.aspx");
            }
            else {
                Response.Redirect("~/Default.aspx"); 
            }
        }
    

        protected void gvDetallesCompra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                List<DetalleCompra> detallesCompra = Session["detallesCompra"] as List<DetalleCompra>;

                if (detallesCompra != null && detallesCompra.Count > 0)
                {
                    decimal total = detallesCompra.Sum(detalle => detalle.Subtotal);

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
                    gvDetallesCompra.Controls[0].Controls.Add(footerRow);
                }
            }
        }

        protected void btnDescargarFactura_Click(object sender, EventArgs e)
        {
            List<DetalleCompra> detallesCompra = Session["detallesCompra"] as List<DetalleCompra>;
            if (detallesCompra != null && detallesCompra.Count > 0)
            {
                decimal total = detallesCompra.Sum(detalle => detalle.Subtotal);

                string contenidoHTML = GenerarContenidoFactura(detallesCompra, total);

                // Configuración de la respuesta HTTP para descargar el archivo
                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment; filename=Factura.html");
                Response.Write(contenidoHTML);
                Response.End();
            }
        }

        protected string GenerarContenidoFactura(List<DetalleCompra> detallesCompra, decimal total)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<head><title>Factura</title></head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h1>Factura</h1>");

            // Agregar la fecha actual al encabezado
            sb.AppendLine($"<p>Fecha: {DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss")}</p>");

            sb.AppendLine("<h2>Detalles de la compra:</h2>");

            foreach (DetalleCompra detalle in detallesCompra)
            {
                // Supongamos que tienes una lista de productos con IdProducto y NombreProducto
                Productos producto = ObtenerProductoPorId(detalle.IdProducto);

                sb.AppendLine($"<p>ID Producto: {detalle.IdProducto}</p>");
                sb.AppendLine($"<p>Nombre Producto: {producto.Nombre}</p>");
                sb.AppendLine($"<p>Nombre Proveedor: {detalle.NombreProveedor}</p>");
                sb.AppendLine($"<p>Cantidad: {detalle.Cantidad}</p>");
                sb.AppendLine($"<p>Precio Unitario: {detalle.PrecioCompra.ToString("C")}</p>");
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
            List<Productos> productosSeleccionados = Session["productosSeleccionados"] as List<Productos>;
            // Asegúrate de tener la lógica adecuada para obtener el producto desde tu fuente de datos
            Productos productoEncontrado = productosSeleccionados.FirstOrDefault(p => p.IdProductos == idProducto);

            return productoEncontrado ?? new Productos(); // Manejo de caso cuando el producto no se encuentra
        }
    }

}