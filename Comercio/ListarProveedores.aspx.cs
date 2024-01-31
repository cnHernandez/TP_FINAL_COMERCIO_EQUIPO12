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
    public partial class ListarProveedores : System.Web.UI.Page
    {
        public List<Proveedores> listaProveedores{ get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "no eres administrador");
                Response.Redirect("Login.aspx", false);
            }

            if (!IsPostBack)
            {
                ProveedoresNegocio negocio = new ProveedoresNegocio();
                listaProveedores = negocio.ListarProveedores();
                repRepeater.DataSource = listaProveedores;
                repRepeater.DataBind();
            }

            if (Request.QueryString["IdProveedor"] != null && listaProveedores.Count > 0)
            {
                string MarcaID = Request.QueryString["IdProveedor"];
            }
        }

        protected void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            string nombreProveedor = txtNombre.Text.Trim();

            if (!string.IsNullOrEmpty(nombreProveedor))
            {
                // Utilizar la misma lista de productos para agregar resultados de búsqueda
                ProveedoresNegocio negocio = new ProveedoresNegocio();
                listaProveedores = negocio.ObtenerProveedoresPorNombre(nombreProveedor);

                repRepeater.DataSource = listaProveedores;
                repRepeater.DataBind();
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Obtener el IdMarcas del control CommandArgument del botón
            Button btnEliminar = (Button)sender;
            int idProveedor = Convert.ToInt32(btnEliminar.CommandArgument);

            ProveedoresNegocio proveedor = new ProveedoresNegocio();
            proveedor.EliminarProveedor(idProveedor);

            Response.Redirect("ListarProveedores.aspx", false);
        }
    }
}