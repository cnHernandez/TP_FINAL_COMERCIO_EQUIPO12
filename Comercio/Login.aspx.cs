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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuarios usuario;// creo una instancia de usuario
            UsuariosNegocio negocio = new UsuariosNegocio(); // y de negiocio
            
            try
            {
                usuario = new Usuarios(txtUser.Text, txtPass.Text, false, false);//guardo los datos en la instancia de usuario

                if (negocio.loguear(usuario))// con el metodo vemos si es correcto lo ingresado y si es correcto en este caso lo mando a default
                {
                    Session.Add("Usuario", usuario);
                    //Response.Redirect("Logeado.aspx", false);
                    if (EsAdministrador())
                    {
                        Response.Redirect("Default.aspx", false);
                    }
                    else if (EsVendedor())
                    {
                        Response.Redirect("DefaultVendedor.aspx", false);
                    }
                   
                }
                else
                {
                    Session.Add("error", "user o pass incorrectas"); // y si no a una pantalla de error
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error ", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        private bool EsAdministrador()
        {
            // Verifica si la sesión contiene un usuario y si ese usuario es un administrador
            return Session["Usuario"] != null && ((Dominio.Usuarios)Session["Usuario"]).TipoUsuario == Usuarios.TipoUsuarios.administrador;
        }

        private bool EsVendedor()
        {
            // Verifica si la sesión contiene un usuario y si ese usuario es un usuario regular
            return Session["Usuario"] != null && ((Dominio.Usuarios)Session["Usuario"]).TipoUsuario == Usuarios.TipoUsuarios.vendedor;
        }
    }
}