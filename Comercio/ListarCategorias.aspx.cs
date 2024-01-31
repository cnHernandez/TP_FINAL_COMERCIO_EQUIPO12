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
    public partial class ListarCategorias : System.Web.UI.Page
    {
        public List<Categorias> listaCategorias{ get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "no eres administrador");
                Response.Redirect("Login.aspx", false);
            }

            if (!IsPostBack)
            {
                CategoriasNegocio negocio = new CategoriasNegocio();
                listaCategorias = negocio.ListarCategorias();
                repRepeater.DataSource = listaCategorias;
                repRepeater.DataBind();
            }

            if (Request.QueryString["IdCategoria"] != null && listaCategorias.Count > 0)
            {
                string MarcaID = Request.QueryString["IdCategoria"];
            }
        }

        protected void btnBuscarCat_Click(object sender, EventArgs e)
        {
            string nombreCat = txtNombre.Text.Trim();

            if (!string.IsNullOrEmpty(nombreCat))
            {
                // Utilizar la misma lista de productos para agregar resultados de búsqueda
                CategoriasNegocio negocio = new CategoriasNegocio();
                listaCategorias = negocio.ObtenerCategoriasPorNombre(nombreCat);

                repRepeater.DataSource = listaCategorias;
                repRepeater.DataBind();
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el IdCategoria del control CommandArgument del botón
            Button btnEliminar = (Button)sender;
            int idCat = Convert.ToInt32(btnEliminar.CommandArgument);

            CategoriasNegocio marca = new CategoriasNegocio();
            marca.EliminarCategoria(idCat);

            Response.Redirect("ListarCategorias.aspx", false);
        }
    }
}