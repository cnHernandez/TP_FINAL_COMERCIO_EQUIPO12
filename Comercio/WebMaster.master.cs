using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Turnos
{
    public partial class WebMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void Logout_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión y redirigir a la página de inicio o login
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Login.aspx"); // Cambia esto a la página que desees después del logout
        }
    }
}