using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private bool ContieneNumeros(string texto)
        {
            foreach (char c in texto)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
        private bool ContieneLetras(string texto)
        {
            foreach (char c in texto)
            {
                if (char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContieneCaracterEspecial(string texto)
        {
            foreach (char c in texto)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    // Si el carácter no es letra, dígito ni espacio en blanco, consideramos que es un carácter especial
                    return true;
                }
            }
            return false;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder errores = new StringBuilder();
                errores.Clear();


                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    errores.AppendLine("Complete el Nombre...");
                    lblNombre.Text = "Complete el Nombre...";
                    lblNombre.Visible = true;
                }
                else
                {
                    lblNombre.Visible = false;
                }

                if (string.IsNullOrWhiteSpace(txtContraseña.Text))
                {
                    errores.AppendLine("Complete la contraseña...");
                    lblPass.Text = "Complete la contraseña...";
                    lblPass.Visible = true;
                }
                else if (txtContraseña.Text.Length < 8)
                {
                    errores.AppendLine("La contraseña debe tener al menos 8 caracteres.");
                    lblPass.Text = "La contraseña debe tener al menos 8 caracteres.";
                    lblPass.Visible = true;
                }
                else if (!ContieneLetras(txtContraseña.Text) || !ContieneNumeros(txtContraseña.Text) || !ContieneCaracterEspecial(txtContraseña.Text))
                {
                    errores.AppendLine("La contraseña debe contener al menos una letra, un número y un carácter especial.");
                    lblPass.Text = "La contraseña debe contener al menos una letra, un número y un carácter especial.";
                    lblPass.Visible = true;
                }
                else
                {
                    lblPass.Visible = false;
                }


                if (errores.Length > 0)
                {
                    return; // Detener el proceso ya que hay errores
                }
                Dominio.Usuarios prod = new Dominio.Usuarios("","",false,false);
                UsuariosNegocio nuevo = new UsuariosNegocio();

                prod.Nombre = txtNombre.Text;             
                prod.Contrasena = txtContraseña.Text;
                prod.TipoUsuario = (Dominio.Usuarios.TipoUsuarios)Enum.Parse(typeof(Dominio.Usuarios.TipoUsuarios), ddlTipo.SelectedValue);

                if (Request.QueryString["IdUsuario"] != null)
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