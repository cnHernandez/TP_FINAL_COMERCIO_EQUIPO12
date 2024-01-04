using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Comercio
{
    public partial class Web : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] is Dominio.Usuarios usuario)
            {

                // Mostrar el nombre de usuario en la barra de navegación
                MostrarNombreUsuario(usuario.Nombre);
            }
        }

        private void MostrarNombreUsuario(string nombre)
        {
            Admin.InnerText = "Hola, Administrador " + nombre;
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
