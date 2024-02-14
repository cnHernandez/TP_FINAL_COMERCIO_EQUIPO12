using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class DefaultVendedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ListaProductos"] = null;
            Session["ListaProductosSeleccionados"] = null;
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.vendedor))
            {
                Session.Add("Error", "No eres Vendedor");
                Response.Redirect("Login.aspx", false);
            }
        }
    }
}