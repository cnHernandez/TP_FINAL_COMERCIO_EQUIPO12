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
            DetalleCompraNegocio negocio = new DetalleCompraNegocio();
            if (!IsPostBack)
            {

                if (Request.QueryString["IdCompra"] != null)
                {
                    // Obtener el valor del parámetro IdCompra de la URL
                    string idCompra = Request.QueryString["IdCompra"];
                    
                        // Llamar al método para mostrar los detalles de la compra por IdCompra
                        
                    gvDetallesCompra.DataSource = negocio.ListarCompras(idCompra);
                    gvDetallesCompra.DataBind();
                }
                    else
                    {
                    MostrarDetallesCompra();
                    }
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
            // Redirigir a la página principal (cambia la URL según tu estructura de proyecto)
            Response.Redirect("~/default.aspx");
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
           
                List<DetalleCompra> detallesCompra = null;
                decimal total = 0;

                // Verificar si hay un IdCompra en la URL
                if (Request.QueryString["IdCompra"] != null)
                {
                    // Obtener el IdCompra de la URL
                    string idCompra = Request.QueryString["IdCompra"];

                    // Llamar al método para obtener los detalles de la compra por IdCompra
                    detallesCompra = ObtenerDetallesCompraPorId(idCompra);

                    // Calcular el total de la compra
                    total = detallesCompra.Sum(detalle => detalle.Subtotal);
                }
                else
                {
                    // Si no hay un IdCompra en la URL, verificar si hay detalles de compra en la sesión
                    if (Session["detallesCompra"] != null)
                    {
                        // Obtener los detalles de la compra de la sesión
                        detallesCompra = Session["detallesCompra"] as List<DetalleCompra>;

                        // Calcular el total de la compra
                        total = detallesCompra.Sum(detalle => detalle.Subtotal);
                    }
                }

                // Verificar si se obtuvieron detalles de compra
                if (detallesCompra != null && detallesCompra.Count > 0)
                {
                    // Generar el contenido HTML de la factura
                    string contenidoHTML = GenerarContenidoFactura(detallesCompra, total);

                    // Configuración de la respuesta HTTP para descargar el archivo
                    Response.Clear();
                    Response.ContentType = "application/force-download";
                    Response.AddHeader("content-disposition", "attachment; filename=Factura.html");
                    Response.Write(contenidoHTML);
                    Response.End();
                }
            }

            private List<DetalleCompra> ObtenerDetallesCompraPorId(string idCompra)
            {
                // Llamar al método para obtener los detalles de la compra por IdCompra
                DetalleCompraNegocio negocio = new DetalleCompraNegocio();
                return negocio.ListarCompras(idCompra);
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
               // Productos producto = ObtenerProductoPorId(detalle.IdProducto);

                sb.AppendLine($"<p>ID Producto: {detalle.IdProducto}</p>");
                sb.AppendLine($"<p>Nombre Producto: {detalle.NombreProducto}</p>");
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
            List<Productos> productosSeleccionados = Session["productosSeleccionados"] as List<Productos>;

            // Verificar si la lista de productos seleccionados no es nula y contiene elementos
            if (productosSeleccionados != null && productosSeleccionados.Count > 0)
            {
                // Intentar encontrar el producto con el IdProducto especificado
                Productos productoEncontrado = productosSeleccionados.FirstOrDefault(p => p.IdProductos == idProducto);

                // Verificar si se encontró un producto con el IdProducto especificado
                if (productoEncontrado != null)
                {
                    return productoEncontrado; // Devolver el producto encontrado
                }
            }

            // Si no se encuentra el producto o la lista de productos es nula o está vacía, devolver un nuevo objeto Productos
            return new Productos();
        }
    }

}