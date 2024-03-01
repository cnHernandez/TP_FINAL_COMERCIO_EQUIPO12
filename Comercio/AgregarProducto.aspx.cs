using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                CargarTipo();
                CargarMarcas();
            }

            string legajo = Request.QueryString["IdProductos"] != null ? Request.QueryString["IdProductos"].ToString() : "";
            if (legajo != "" && !IsPostBack)
            {
                ProductosNegocio negocio = new ProductosNegocio();
                Dominio.Productos seleccionado = (negocio.ListarProductosLimpio(legajo))[0];

                ///precargamos
                txtNombre.Text = seleccionado.Nombre;
                txtPorcentaje.Text = seleccionado.PorcentajeGanancia.ToString();
                
                txtMinimo.Text = seleccionado.StockMinimo.ToString();
                txtUrl.Text = seleccionado.UrlImagen.ToString();
                ddlTipo.SelectedValue = seleccionado.IdCategoria.ToString();
                ddlMarca.SelectedValue = seleccionado.IdMarca.ToString();
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

        private void CargarMarcas()
        {
            MarcasNegocio marca= new MarcasNegocio();
            ddlMarca.DataSource = marca.ListarMarcas();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "IdMarcas";
            ddlMarca.DataBind();
        }

        private bool EsNumeroConComa(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsDigit(c) && c != ',')
                {
                    return false;
                }
            }
            return true;
        }

        private bool NoContieneOtrosCaracteres(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Dominio.Productos prod = new Dominio.Productos();
                ProductosNegocio nuevo = new ProductosNegocio();
                StringBuilder errores = new StringBuilder();

                errores.Clear();

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    errores.AppendLine("Nombre inválido...");
                    lblNombreError.Text = "Nombre inválido...";
                    lblNombreError.Visible = true; // Asegúrate de que el label de error sea visible
                }
                else
                {
                    lblNombreError.Visible = false;
                }

                if (!EsNumeroConComa(txtPorcentaje.Text) || string.IsNullOrWhiteSpace(txtPorcentaje.Text))
                {
                    errores.AppendLine("Porcentaje de ganancia inválido. Ingrese solo números.");
                    lblPorcentajeError.Text = "Porcentaje de ganancia inválido. Ingrese solo números.";
                    lblPorcentajeError.Visible = true;
                }
                else
                {
                    lblPorcentajeError.Visible = false;
                }

               

                if (!NoContieneOtrosCaracteres(txtMinimo.Text) || string.IsNullOrWhiteSpace(txtMinimo.Text))
                {
                    errores.AppendLine("Stock mínimo inválido...");
                    lblMinimo.Text = "Stock minimo inválido...";
                    lblMinimo.Visible = true;
                }
                else
                {
                    lblMinimo.Visible = false;
                }

                if (string.IsNullOrWhiteSpace(txtUrl.Text))
                {
                    errores.AppendLine("Stock mínimo inválido...");
                    lblUrlError.Text = "Url vacia...";
                    lblUrlError.Visible = true;
                }
                else
                {
                    lblUrlError.Visible = false;
                }

                if (errores.Length > 0)
                {
                    return; // Detener el proceso ya que hay errores
                }


                prod.Nombre = txtNombre.Text;
                prod.PorcentajeGanancia = decimal.Parse(txtPorcentaje.Text);
                
                prod.StockMinimo = int.Parse(txtMinimo.Text);
                prod.UrlImagen = txtUrl.Text;
                prod.IdMarca = int.Parse(ddlMarca.SelectedValue);
                prod.IdCategoria = int.Parse(ddlTipo.SelectedValue);

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

                // Almacena el objeto Productos en la sesión
                Session["ProductoSeleccionado"] = prod;

                // Redirige a la página AgregarPxP.aspx
                Response.Redirect("AgregarPxP.aspx");
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