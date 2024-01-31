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
    public partial class WebForm1 : System.Web.UI.Page
    {
        private List<Productos> ProductosSeleccionados
        {
            get
            {
                if (Session["productosSeleccionados"] == null)
                    Session["productosSeleccionados"] = new List<Productos>();
                return (List<Productos>)Session["productosSeleccionados"];
            }
            set { Session["productosSeleccionados"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si el usuario es un Vendedor
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.vendedor))
            {
                Session.Add("Error", "No eres Vendedor");
                Response.Redirect("Login.aspx", false);
            }

            // Verificar si es la carga inicial de la página
            if (!IsPostBack)
            {
                // Cargar productos disponibles en la carga inicial
                CargarProductosDisponibles();
            }
        }

        protected void dataGridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);

                // Obtén el producto correspondiente al IdProducto desde tu fuente de datos
                ProductosNegocio negocio = new ProductosNegocio();
                Productos producto = negocio.ObtenerProductoPorId(idProducto);

                // Obtén la lista actual de productos seleccionados desde la sesión o crea una nueva si no existe
                List<Productos> productosSeleccionadosSession = Session["productosSeleccionados"] as List<Productos>;

                if (productosSeleccionadosSession == null)
                {
                    productosSeleccionadosSession = new List<Productos>();
                }

                // Agrega el producto a la lista de productos seleccionados
                productosSeleccionadosSession.Add(producto);

                // Guarda la lista actualizada en la sesión
                Session["productosSeleccionados"] = productosSeleccionadosSession;

                // Vuelve a cargar ambos GridViews para reflejar los cambios
                BindGridViewData();
                BindGridViewProductosSeleccionados();
            }
        }
        private void BindGridViewProductosSeleccionados()
        {
            List<Productos> productosSeleccionadosSession = Session["productosSeleccionados"] as List<Productos>;

            if (productosSeleccionadosSession != null)
            {
                dataGridViewProductosSeleccionados.DataSource = productosSeleccionadosSession;
                dataGridViewProductosSeleccionados.DataBind();
            }
            else
            {
                dataGridViewProductosSeleccionados.DataSource = null;
                dataGridViewProductosSeleccionados.DataBind();
            }
        }
     
            private void CargarProductosDisponibles()
            {
                // Lógica para cargar productos disponibles, similar a lo que ya has implementado
                // Puedes utilizar un objeto de la clase ProductosNegocio o similar
                // y luego asignar el resultado al GridView dataGridViewProductos
                ProductosNegocio negocio = new ProductosNegocio();
                List<Dominio.Productos> listaProductos = negocio.ListarProductos();

                dataGridViewProductos.DataSource = listaProductos;
                dataGridViewProductos.DataBind();
            }

           

           

            private void BindGridViewData()
            {
                // Lógica para cargar productos disponibles, similar a lo que ya has implementado
                // Puedes utilizar un objeto de la clase ProductosNegocio o similar
                // y luego asignar el resultado al GridView dataGridViewProductos
                ProductosNegocio negocio = new ProductosNegocio();
                List<Dominio.Productos> listaProductos = negocio.ListarProductos();

                dataGridViewProductos.DataSource = listaProductos;
                dataGridViewProductos.DataBind();
            }
        }



    }
