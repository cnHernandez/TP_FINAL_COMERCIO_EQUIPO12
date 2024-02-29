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
    public partial class AgregarMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Id = Request.QueryString["IdMarcas"] != null ? Request.QueryString["IdMarcas"].ToString() : "";
            if (Id != "" && !IsPostBack)
            {
                MarcasNegocio negocio = new MarcasNegocio();
                Dominio.Marcas seleccionado = (negocio.ListarMarcas(Id))[0];

                ///precargamos
                txtNombre.Text = seleccionado.Nombre;
                txtUrl.Text = seleccionado.UrlImagen;
                imgMarca.ImageUrl = seleccionado.UrlImagen;
                imgMarca.Visible = !string.IsNullOrEmpty(seleccionado.UrlImagen);
            }
            else
            {
                imgMarca.ImageUrl = txtUrl.Text;
                imgMarca.Visible = !string.IsNullOrEmpty(txtUrl.Text);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder errores = new StringBuilder();
                errores.Clear();


                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    errores.AppendLine("Complete el Nombre...");
                    lblNombre.Text = "Complete el Nombre...";
                    lblNombre.Visible = true;
                }
                else
                {
                    lblNombre.Visible = false;
                }

                if (string.IsNullOrWhiteSpace(txtUrl.Text))
                {
                    errores.AppendLine("Complete la Url...");
                    lblUrl.Text = "Complete la Url...";
                    lblUrl.Visible = true;
                }
                else
                {
                    lblUrl.Visible = false;
                }

                if (errores.Length > 0)
                {
                    return; // Detener el proceso ya que hay errores
                }
                Dominio.Marcas Marca = new Dominio.Marcas();
                MarcasNegocio nuevo = new MarcasNegocio();

                Marca.Nombre = txtNombre.Text;
                Marca.UrlImagen = txtUrl.Text;

                if (Request.QueryString["IdMarcas"] != null)
                 {
                    string legajo = Request.QueryString["IdMarcas"];
                    Marca.IdMarcas = (int)(long.TryParse(legajo, out long legajoComoLong) ? legajoComoLong : 0);
                    nuevo.Modificar(Marca);
                    Response.Redirect("ListarMarcas.aspx", false);
                }
               else
                {
                    nuevo.AgregarMarca(Marca);
                    Response.Redirect("ListarMarcas.aspx", false);
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
            Response.Redirect("ListarMarcas.aspx", false);
        }

        protected void txtURLImagen_TextChanged(object sender, EventArgs e)
        {
            imgMarca.ImageUrl = txtUrl.Text;
            imgMarca.Visible = !string.IsNullOrEmpty(txtUrl.Text);
        }
    }
}