using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class AgregarCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Id = Request.QueryString["IdCategoria"] != null ? Request.QueryString["IdCategoria"].ToString() : "";
            if (Id != "" && !IsPostBack)
            {
                CategoriasNegocio negocio = new CategoriasNegocio();
                Dominio.Categorias seleccionado = (negocio.ListarCategorias(Id))[0];

                ///precargamos
                txtNombre.Text = seleccionado.Nombre;
                txtUrl.Text = seleccionado.UrlImagen;
                imgCat.ImageUrl = seleccionado.UrlImagen;
                imgCat.Visible = !string.IsNullOrEmpty(seleccionado.UrlImagen);
            }
            else
            {
                imgCat.ImageUrl = txtUrl.Text;
                imgCat.Visible = !string.IsNullOrEmpty(txtUrl.Text);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {

                // Verificar si algún campo está vacío
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtUrl.Text))
                {
                    lblMensaje.Text = "Todos los campos son obligatorios.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                Dominio.Categorias cat = new Dominio.Categorias();
                CategoriasNegocio nuevo = new CategoriasNegocio();

                cat.Nombre = txtNombre.Text;
                cat.UrlImagen = txtUrl.Text;

                if (Request.QueryString["IdCategoria"] != null)
                {
                    string legajo = Request.QueryString["IdCategoria"];
                    cat.IdCategoria = (int)(long.TryParse(legajo, out long legajoComoLong) ? legajoComoLong : 0);
                    nuevo.Modificar(cat);
                    Response.Redirect("ListarCategorias.aspx", false);
                }
                else
                {
                    nuevo.AgregarCategoria(cat);
                    Response.Redirect("ListarCategorias.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarCategorias.aspx", false);
        }

        protected void txtURLImagen_TextChanged(object sender, EventArgs e)
        {
            imgCat.ImageUrl = txtUrl.Text;
            imgCat.Visible = !string.IsNullOrEmpty(txtUrl.Text);
        }
    }
}