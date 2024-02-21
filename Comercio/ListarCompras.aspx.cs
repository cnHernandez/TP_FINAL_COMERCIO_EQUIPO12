using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class ListarCompras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "no eres administrador");
                Response.Redirect("Login.aspx", false);
            }
            if (!IsPostBack)
            {
                BindGridViewData();
            }
        }

        private void BindGridViewData()
        {
            DetalleCompraNegocio negocio = new DetalleCompraNegocio();
            List<DetalleCompra> listaCompras = negocio.ListarCompras();

            dataGridViewCompras.DataSource = listaCompras;
            dataGridViewCompras.DataBind();

            foreach (GridViewRow row in dataGridViewCompras.Rows)
            {
                // Obtener el valor de la fecha de la fila actual
                DateTime fechaCompra = Convert.ToDateTime(row.Cells[6].Text); // Suponiendo que la fecha está en la sexta columna del GridView

                // Formatear la fecha como "dd/MM/yyyy"
                string fechaFormateada = fechaCompra.ToString("dd/MM/yyyy");

                // Actualizar el valor de la fecha en la celda correspondiente
                row.Cells[6].Text = fechaFormateada; // Suponiendo que la fecha está en la sexta columna del GridView
            }
        }

        protected void dataGridViewCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewCompras.PageIndex = e.NewPageIndex;
            BindGridViewData();
        }

        protected void dataGridViewCompras_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dataGridViewCompras_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = dataGridViewCompras.DataKeys[e.RowIndex].Value.ToString();
            UsuariosNegocio negocio = new UsuariosNegocio();
            negocio.EliminarUsuario(int.Parse(ID));
            BindGridViewData();
        }

        protected void dataGridViewCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal totalFacturado = 0;
                foreach (GridViewRow row in dataGridViewCompras.Rows)
                {
                    decimal subtotal;
                    if (decimal.TryParse(row.Cells[5].Text, out subtotal)) // El índice 5 corresponde a la columna del subtotal en tu GridView
                    {
                        totalFacturado += subtotal;
                    }
                }
                lblTotalFacturado.Text = $"Total Facturado: {totalFacturado.ToString("C")}";
            }
        }

        protected void dataGridViewCompras_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalle")
            {
                // Obtener el IdCompra del argumento de comando
                string idCompra = e.CommandArgument.ToString();

                // Redirigir a la página ResumenCompra.aspx con el IdCompra en la URL
                Response.Redirect("ResumenCompra.aspx?IdCompra=" + idCompra);
            }
        }
    }
}