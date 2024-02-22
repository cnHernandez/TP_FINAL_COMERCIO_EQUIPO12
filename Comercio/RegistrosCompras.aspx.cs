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
                // Manejar la excepción de acuerdo a tu lógica de negocio
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
            ListarCompras();
        }

        protected void dataGridViewCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewCompras.PageIndex = e.NewPageIndex;
            ListarCompras();
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

                // Almacenar la lista en la sesión
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