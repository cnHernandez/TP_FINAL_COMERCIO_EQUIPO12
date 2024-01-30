using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comercio
{
    public partial class AgregarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                CargarTipo();
                
            }
            string legajo = Request.QueryString["IdUsuario"] != null ? Request.QueryString["IdUsuario"].ToString() : "";
            if (legajo != "" && !IsPostBack)
            {
                UsuariosNegocio negocio = new UsuariosNegocio();
                Dominio.Usuarios seleccionado = (negocio.ListarUsuarios(legajo))[0];

                ///precargamos
                txtNombre.Text = seleccionado.Nombre;
                txtContraseña.Text = seleccionado.Contrasena;
                // Asegúrate de que el DropDownList tenga elementos antes de establecer el valor seleccionado
                if (ddlTipo.Items.FindByValue(seleccionado.TipoUsuario.ToString()) != null)
                {
                    ddlTipo.SelectedValue = seleccionado.TipoUsuario.ToString();
                }

            }
        }

        private void CargarTipo()
        {
            // Obtén los nombres de los elementos del enumerador TipoUsuarios
            string[] nombresTipoUsuarios = Enum.GetNames(typeof(Dominio.Usuarios.TipoUsuarios));

            // Asigna los nombres al DropDownList
            ddlTipo.DataSource = nombresTipoUsuarios;
            ddlTipo.DataBind();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Dominio.Usuarios prod = new Dominio.Usuarios("","",false,false);
                UsuariosNegocio nuevo = new UsuariosNegocio();

                prod.Nombre = txtNombre.Text;             
                prod.Contrasena = txtContraseña.Text;
                prod.TipoUsuario = (Dominio.Usuarios.TipoUsuarios)Enum.Parse(typeof(Dominio.Usuarios.TipoUsuarios), ddlTipo.SelectedValue);

                if (Request.QueryString["IdProductos"] != null)
                {
                    string legajo = Request.QueryString["IdUsuario"];
                    prod.IdUsuario = (int)(long.TryParse(legajo, out long legajoComoLong) ? legajoComoLong : 0);
                    nuevo.ModificarUsuario(prod);
                }
                else
                {
                    nuevo.AgregarUsuario(prod);
                }

                Response.Redirect("ListarUsuarios.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarUsuarios.aspx", false);
        }
    }
}