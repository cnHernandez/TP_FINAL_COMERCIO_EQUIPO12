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
    public partial class ListarMarcas : System.Web.UI.Page
    {
        public List<Marcas> listaMarcas { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "no eres administrador");
                Response.Redirect("Login.aspx", false);
            }

            if (!IsPostBack)
            {
                MarcasNegocio negocio = new MarcasNegocio();
                listaMarcas = negocio.ListarMarcas();
                repRepeater.DataSource = listaMarcas;
                repRepeater.DataBind();
            }

            if (Request.QueryString["IdMarcas"] != null && listaMarcas.Count > 0)
            {
                string MarcaID = Request.QueryString["IdMarcas"];
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el IdMarcas del control CommandArgument del botón
            Button btnEliminar = (Button)sender;
            int idMarcas = Convert.ToInt32(btnEliminar.CommandArgument);

            MarcasNegocio marca= new MarcasNegocio();
            marca.EliminarMarca(idMarcas);

            Response.Redirect("ListarMarcas.aspx", false);
        }

        protected void btnBuscarMarca_Click(object sender, EventArgs e)
        {
            string nombreMarca = txtNombre.Text.Trim();

            if (!string.IsNullOrEmpty(nombreMarca))
            {
                // Utilizar la misma lista de productos para agregar resultados de búsqueda
                MarcasNegocio negocio = new MarcasNegocio();
                listaMarcas = negocio.ObtenerMarcasPorNombre(nombreMarca);

                repRepeater.DataSource = listaMarcas;
                repRepeater.DataBind();
            }
        }
    }
}