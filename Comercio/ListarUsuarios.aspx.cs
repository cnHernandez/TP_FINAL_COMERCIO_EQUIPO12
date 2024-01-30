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
    public partial class ListarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "no eres administrador");
                Response.Redirect("Login.aspx", false);
            }
            if (!IsPostBack)
            {
                BindGridViewData();
            }
        }

        private void BindGridViewData()
        {
            UsuariosNegocio negocio = new UsuariosNegocio();
            List<Dominio.Usuarios> listaUsuarios = negocio.ListarUsuarios();

            dataGridViewUsuarios.DataSource = listaUsuarios;
            dataGridViewUsuarios.DataBind();
        }

        protected void dataGridViewUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewUsuarios.PageIndex = e.NewPageIndex;
            BindGridViewData();
        }

        protected void dataGridViewUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = dataGridViewUsuarios.SelectedRow;
            string IdUsuario = dataGridViewUsuarios.DataKeys[selectedRow.RowIndex].Value.ToString();
            Response.Redirect("AgregarUsuario.aspx?IdUsuario=" + IdUsuario);
        }

        protected void dataGridViewUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = dataGridViewUsuarios.DataKeys[e.RowIndex].Value.ToString();
            UsuariosNegocio negocio = new UsuariosNegocio();
            negocio.EliminarUsuario(int.Parse(ID));
            BindGridViewData();
        }
    }
}