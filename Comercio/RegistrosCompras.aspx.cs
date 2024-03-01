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
    public partial class RegistrosCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "No eres administrador");
                Response.Redirect("Login.aspx", false);
            }

            // Verificar si es la carga inicial de la página
            if (!IsPostBack)
            {
                ListarCompras();
            }
        }

        protected void ListarCompras()
        {
            try
            {
                ComprasNegocio negocio = new ComprasNegocio();
                int idCompra = string.IsNullOrEmpty(txtIdCompra.Text) ? 0 : int.Parse(txtIdCompra.Text.Trim());
                List<Dominio.Compras> listaCompras = negocio.ListarVentas(idCompra);
                dataGridViewCompras.DataSource = listaCompras;
                dataGridViewCompras.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void dataGridViewCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Obtener el ID del proveedor para la fila actual
                int idProveedor = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdProveedor"));
                ProveedoresNegocio negocio = new ProveedoresNegocio();
                // Obtener el nombre del proveedor usando el ID
                string nombreProveedor = negocio.ObtenerNombreProveedorPorId(idProveedor);

                // Encontrar el Label dentro de la fila actual
                Label lblProveedor = (Label)e.Row.FindControl("lblProveedor");

                // Asignar el nombre del proveedor al Label
                lblProveedor.Text = nombreProveedor;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                // Calcular el total facturado utilizando LINQ
                decimal totalFacturado = ((List<Dominio.Compras>)dataGridViewCompras.DataSource)
                    .Sum(compra => compra.TotalCompra);

                // Mostrar el total facturado en el Label
                lblTotalFacturado.Text = $"Total Facturado: {totalFacturado.ToString("C")}";
            }
        }

        protected void btnBuscarCompra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdCompra.Text))
            {
            ListarCompras();
            lblTotalFacturado.Visible = true;

            } else
            {
                ListarCompras();
                lblTotalFacturado.Visible = false;
            }
        }

        protected void dataGridViewCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewCompras.PageIndex = e.NewPageIndex;
            ListarCompras();
            lblTotalFacturado.Visible = false;
        }
       
        protected void dataGridViewCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DetallesCompra")
            {
                // Obtener el ID de la compra seleccionada
                int idCompra = Convert.ToInt32(e.CommandArgument);

                DetalleCompraNegocio detalleCompraNegocio = new DetalleCompraNegocio();
                ProveedoresNegocio negocio = new ProveedoresNegocio();

                // Obtener el índice de la fila seleccionada
                int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
                GridViewRow selectedRow = dataGridViewCompras.Rows[rowIndex];

                // Buscar el control Label dentro de la fila para obtener el nombre del proveedor
                Label lblProveedor = (Label)selectedRow.FindControl("lblProveedor");
                string nombreProveedor = lblProveedor.Text;

                // Obtener la lista de detalles de compra por ID de compra
                List<DetalleCompra> detallesCompra = detalleCompraNegocio.ObtenerDetallesPorIdCompra(idCompra, nombreProveedor);

                // Crear una lista para almacenar los productos
                List<Productos> productosSeleccionados = new List<Productos>();
                ProductosNegocio negocioProducto = new ProductosNegocio();

                // Obtener los productos asociados a cada detalle de compra
                foreach (var detalle in detallesCompra)
                {
                    // Obtener el producto asociado al detalle de compra
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

                // Almacenar la lista de detalles de compra en la sesión
                Session["detallesCompra"] = detallesCompra;

                // Redireccionar a la página de resumen de la compra
                Response.Redirect($"ResumenCompra.aspx?id={idCompra}");
                
            }
        }





        protected void dataGridViewCompras_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tu lógica de manejo de evento aquí
        }

        protected void dataGridViewCompras_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Tu lógica de manejo de evento aquí
        }


    }
}