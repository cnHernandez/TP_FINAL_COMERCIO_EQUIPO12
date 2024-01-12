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
    public partial class AgregarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarProveedor();
                CargarTipo();
                CargarMarcas();
            }

            string legajo = Request.QueryString["IdProductos"] != null ? Request.QueryString["IdProductos"].ToString() : "";
            if (legajo != "" && !IsPostBack)
            {
                ProductosNegocio negocio = new ProductosNegocio();
                Dominio.Productos seleccionado = (negocio.ListarProductos(legajo))[0];

                ///precargamos
                txtNombre.Text = seleccionado.Nombre;
                txtPrecioCompra.Text = seleccionado.PrecioCompra.ToString();
                txtPorcentaje.Text = seleccionado.PorcentajeGanancia.ToString();
                txtStockActual.Text = seleccionado.StockActual.ToString();
                txtMinimo.Text = seleccionado.StockMinimo.ToString();
                txtUrl.Text = seleccionado.UrlImagen.ToString();
                ddlTipo.SelectedValue = seleccionado.IdCategoria.ToString();
                ddlMarca.SelectedValue = seleccionado.IdMarca.ToString();
                ddlProveedor.SelectedValue = seleccionado.IdProveedor.ToString();
                imgProducto.ImageUrl = txtUrl.Text;
                imgProducto.Visible = !string.IsNullOrEmpty(txtUrl.Text);
            }
        }

        private void CargarTipo()
        {
            CategoriasNegocio cat = new CategoriasNegocio();
            ddlTipo.DataSource = cat.ListarCategorias();
            ddlTipo.DataTextField = "Nombre";
            ddlTipo.DataValueField = "IdCategoria";
            ddlTipo.DataBind();
        }

        private void CargarProveedor()
        {
            ProveedoresNegocio pro = new ProveedoresNegocio();
            ddlProveedor.DataSource = pro.ListarProveedores();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataBind();
        }
        private void CargarMarcas()
        {
            MarcasNegocio marca= new MarcasNegocio();
            ddlMarca.DataSource = marca.ListarMarcas();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "IdMarcas";
            ddlMarca.DataBind();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Dominio.Productos prod = new Dominio.Productos();
                ProductosNegocio nuevo = new ProductosNegocio();

                prod.Nombre = txtNombre.Text;
                prod.PrecioCompra = decimal.Parse(txtPrecioCompra.Text);
                prod.PorcentajeGanancia = decimal.Parse(txtPorcentaje.Text);
                prod.StockActual = int.Parse(txtStockActual.Text);
                prod.StockMinimo = int.Parse(txtMinimo.Text);
                prod.UrlImagen = txtUrl.Text;
                prod.IdMarca = int.Parse(ddlMarca.SelectedValue);
                prod.IdCategoria = int.Parse(ddlTipo.SelectedValue);
                prod.IdProveedor = int.Parse(ddlProveedor.SelectedValue);

                if (Request.QueryString["IdProductos"] != null)
                {
                    string legajo = Request.QueryString["IdProductos"];
                    prod.IdProductos = (int)(long.TryParse(legajo, out long legajoComoLong) ? legajoComoLong : 0);
                    nuevo.Modificar(prod);
                }
                else
                {
                    nuevo.AgregarProducto(prod);
                }

                Response.Redirect("ListarProductos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void txtURLImagen_TextChanged(object sender, EventArgs e)
        {
            imgProducto.ImageUrl = txtUrl.Text;
            imgProducto.Visible = !string.IsNullOrEmpty(txtUrl.Text);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarProductos.aspx", false);
        }

   
    }
}