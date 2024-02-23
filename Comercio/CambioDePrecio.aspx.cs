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
    public partial class CambioDePrecio : System.Web.UI.Page
    {
        private List<Producto_x_Proveedor> ListaProductosXProveedor = new List<Producto_x_Proveedor>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Usuario"] is Dominio.Usuarios usuario && usuario.TipoUsuario == Dominio.Usuarios.TipoUsuarios.administrador))
            {
                Session.Add("Error", "No eres administrador");
                Response.Redirect("Login.aspx", false);
            }

            // Verificar si es la carga inicial de la página
            if (!IsPostBack)
            {
                // Cargar proveedores solo en la carga inicial
                Session["ListaProductosXProveedor"] = null;
                CargarProveedor();
                
            }
        }
        private void CargarProveedor()
        {
            ProveedoresNegocio pro = new ProveedoresNegocio();
            ddlProveedor.DataSource = pro.ListarProveedores();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "IdProveedor";
            ddlProveedor.DataBind();

            ddlProveedor.Items.Insert(0, new ListItem("-- Seleccione un proveedor --", ""));
        }
        private void BindGridViewDataProveedor(int idProveedor)
        {
            if (idProveedor > 0)
            {
             
                    BindGridViewData();
                
            }
            else
            {
                dataGridViewProductos1.DataSource = null;
                dataGridViewProductos1.DataBind();
            }
        }

        private void BindGridViewData()
        {
            ProductosNegocio negocio = new ProductosNegocio();
            int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);
            List<Dominio.Productos> listaProductos = negocio.TodoslosProductosPorProveedor(idProveedor);
            dataGridViewProductos1.DataSource = listaProductos;
            dataGridViewProductos1.DataBind();
        }
        protected void dataGridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dataGridViewProductos1.PageIndex = e.NewPageIndex;
            BindGridViewData();
        }
        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlProveedor.SelectedValue))
            {
                int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

                    // Llama al método para cargar los productos asociados al proveedor y la categoría seleccionados
                    BindGridViewDataProveedor(idProveedor);
              

                // Deshabilita el DropDownList después de seleccionar un proveedor
                ddlProveedor.Enabled = false;
            }
        }
        private List<Producto_x_Proveedor> ObtenerListaProductosXProveedorDesdeSesion()
        {
            if (Session["ListaProductosXProveedor"] == null)
            {
                Session["ListaProductosXProveedor"] = new List<Producto_x_Proveedor>();
            }

            return (List<Producto_x_Proveedor>)Session["ListaProductosXProveedor"];
        }
        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            List<Producto_x_Proveedor> listaProductosXProveedor = new List<Producto_x_Proveedor>();

            try
            {
                foreach (GridViewRow row in dataGridViewProductos1.Rows)
                {
                    TextBox txtPrecioCompra = (TextBox)row.FindControl("txtPrecioCompra");

                    if (txtPrecioCompra != null)
                    {
                        int productoID = Convert.ToInt32(row.Cells[0].Text);
                        int proveedorID = Convert.ToInt32(row.Cells[8].Text);

                        decimal nuevoPrecio;

                       
                        if (decimal.TryParse(txtPrecioCompra.Text, out nuevoPrecio) && nuevoPrecio >= 0)
                        {
                            Producto_x_Proveedor productoXProveedor = new Producto_x_Proveedor
                            {
                                ProductoID = productoID,
                                ProveedorID = proveedorID,
                                PrecioCompra = nuevoPrecio
                            };

                            listaProductosXProveedor.Add(productoXProveedor);
                        }
                        else
                        {
                            
                            lblMensajeError1.Text = "El precio debe ser un número mayor o igual a 0.";
                            return; 
                        }
                    }
                }

                
                lblMensajeError1.Text = "";

                Session["ListaProductosXProveedor"] = listaProductosXProveedor;

                Producto_x_ProveedorNegocio negocio = new Producto_x_ProveedorNegocio();
                negocio.updatearPrecios(listaProductosXProveedor);

                string script = "alert('Guardado correctamente.'); window.location.href = 'default.aspx';";
                ClientScript.RegisterStartupScript(this.GetType(), "guardadoCorrectamente", script, true);


                Response.Redirect("default.aspx");
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
        }



        protected void dataGridViewProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }






    }
}