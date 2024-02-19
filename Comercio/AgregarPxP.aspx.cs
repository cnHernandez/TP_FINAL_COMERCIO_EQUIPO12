﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Comercio
{
    public partial class AgregarPxP : System.Web.UI.Page
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
                // Solo cargar los productos si no es un PostBack
                CargarProductos();
            }
        }

        protected void reRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Encuentra el DropDownList en la fila actual del Repeater
                DropDownList ddlProveedor = (DropDownList)e.Item.FindControl("ddlProveedor");

                // Verifica que el DropDownList se haya encontrado correctamente
                if (ddlProveedor != null)
                {
                    // Carga los proveedores en el DropDownList
                    ProveedoresNegocio pro = new ProveedoresNegocio();
                    ddlProveedor.DataSource = pro.ListarProveedores();
                    ddlProveedor.DataTextField = "Nombre";
                    ddlProveedor.DataValueField = "IdProveedor";
                    ddlProveedor.DataBind();

                    // Inserta un elemento de "Seleccione un proveedor" en la primera posición
                    ddlProveedor.Items.Insert(0, new ListItem("-- Seleccione un proveedor --", ""));
                }
            }
        }

        private void CargarProductos()
        {
            Productos producto = (Productos)Session["ProductoSeleccionado"];
            if (producto != null)
            {
                List<Productos> listaProductos = new List<Productos>();
                listaProductos.Add(producto);
                reRepeater.DataSource = listaProductos;
                reRepeater.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in reRepeater.Items)
            {
                // Encuentra el DropDownList y el TextBox en la fila actual del Repeater
                DropDownList ddlProveedor = (DropDownList)item.FindControl("ddlProveedor");
                TextBox txtPrecio = (TextBox)item.FindControl("txtPrecio");

                // Verifica que tanto el DropDownList como el TextBox se hayan encontrado correctamente
                if (ddlProveedor != null && txtPrecio != null)
                {
                    // Obtén el ID del producto de la sesión si está disponible
                    int idProducto = -1; // Valor predeterminado en caso de que no se encuentre en la sesión
                    if (Session["ProductoSeleccionado"] != null)
                    {
                        Productos producto = (Productos)Session["ProductoSeleccionado"];
                        idProducto = producto.IdProductos;
                    }

                    // Obtén el ID del proveedor seleccionado del DropDownList
                    int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

                    // Obtén el precio del TextBox
                    decimal precioCompra;
                    if (decimal.TryParse(txtPrecio.Text, out precioCompra))
                    {
                        // Inserta el registro en la base de datos utilizando el método del negocio
                        Producto_x_ProveedorNegocio negocio = new Producto_x_ProveedorNegocio();
                        negocio.AgregarProducto_x_Proveedor(idProducto, idProveedor, precioCompra);
                    }
                }
            }

            // Redirige a la página ListarProductos.aspx después de la operación
            Response.Redirect("ListarProductos.aspx");
        }

    }
}