using Negocio;
using Comercio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using System.Text;

namespace Comercio
{
    public partial class AgregarProveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Id = Request.QueryString["IdProveedor"] != null ? Request.QueryString["IdProveedor"].ToString() : "";
            if (Id != "" && !IsPostBack)
            {
                ProveedoresNegocio negocio = new ProveedoresNegocio();
                Proveedores seleccionado = (negocio.ListarProveedores(Id))[0];

                ///precargamos
                txtNombre.Text = seleccionado.Nombre;
                txtUrl.Text = seleccionado.UrlImagen;
                txtCategoria.Text = seleccionado.Categoria;
                imgProveedor.ImageUrl = seleccionado.UrlImagen;
                imgProveedor.Visible = !string.IsNullOrEmpty(seleccionado.UrlImagen);
            }
            else
            {
                imgProveedor.ImageUrl = txtUrl.Text;
                imgProveedor.Visible = !string.IsNullOrEmpty(txtUrl.Text);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si algún campo está vacío
                StringBuilder errores = new StringBuilder();
                errores.Clear();


                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    errores.AppendLine("Complete el Nombre...");
                    lblErrorNombre.Text = "Complete el Nombre...";
                    lblErrorNombre.Visible = true;
                }
                else
                {
                    lblErrorNombre.Visible = false;
                }

                if (string.IsNullOrWhiteSpace(txtUrl.Text))
                {
                    errores.AppendLine("Complete la Url...");
                    lblErrorUrl.Text = "Complete la Url...";
                    lblErrorUrl.Visible = true;
                }
                else
                {
                    lblErrorUrl.Visible = false;
                }

                if (string.IsNullOrWhiteSpace(txtCategoria.Text))
                {
                    errores.AppendLine("Complete la Categoria..");
                    lblErrorCat.Text = "Complete la Categoria...";
                    lblErrorCat.Visible = true;
                }
                else
                {
                    lblErrorCat.Visible = false;
                }
                if (errores.Length > 0)
                {
                    return; // Detener el proceso ya que hay errores
                }

                Proveedores Proveedor = new Proveedores();
                ProveedoresNegocio nuevo = new ProveedoresNegocio();

                Proveedor.Nombre = txtNombre.Text;
                Proveedor.UrlImagen = txtUrl.Text;
                Proveedor.Categoria = txtCategoria.Text;

                if (Request.QueryString["IdProveedor"] != null)
                {
                    string legajo = Request.QueryString["IdProveedor"];
                    Proveedor.IdProveedor = (int)(long.TryParse(legajo, out long legajoComoLong) ? legajoComoLong : 0);
                    nuevo.Modificar(Proveedor);
                    Response.Redirect("ListarProveedores.aspx", false);
                }
                else
                {
                    nuevo.AgregarProveedor(Proveedor);
                    Response.Redirect("ListarProveedores.aspx", false);
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
            Response.Redirect("ListarProveedores.aspx", false);
        }

        protected void txtUrl_TextChanged(object sender, EventArgs e)
        {
            imgProveedor.ImageUrl = txtUrl.Text;
            imgProveedor.Visible = !string.IsNullOrEmpty(txtUrl.Text);
        }
    }
}