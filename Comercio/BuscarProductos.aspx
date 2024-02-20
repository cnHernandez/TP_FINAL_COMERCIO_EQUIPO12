<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="BuscarProductos.aspx.cs" Inherits="Comercio.BuscarProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="login-container" style="display: flex; align-items: center; max-width: 600px; margin-left: 320px; margin-top:10px;" >
    <label for="txtNombre" style="margin-right: 10px; margin-bottom: 0;">Ingrese el Nombre del Producto: </label>
    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" style="margin-right: 10px; margin-bottom: 0;"></asp:TextBox>
    <asp:Button runat="server" ID="btnBuscarProducto" Text="Buscar Producto" CssClass="btn btn-primary" style="margin-top:10px;" OnClick="btnBuscarProducto_Click" />
</div>


    <div class="container" style="margin-top: 30px; margin-bottom: 100px;">
        <div class="row">
            <asp:Repeater ID="reRepeater" runat="server">
                <ItemTemplate>
                    <div class="col-md-4">
                        <div class="card mb-3">
                            <img src="<%#Eval("UrlImagen") %>" class="card-image card-img-top" alt="..." onerror="this.src='https://camarasal.com/wp-content/uploads/2020/08/default-image-5-1.jpg'">
                            <div class="card-body">                               
                                <h3 class="card-subtitle mb-2 text-muted"><%#Eval("Nombre") %></h3>
                                <h6 class="card-title">Id: <%#Eval("IdProductos") %></h6>
                                <p class="card-text">Precio Compra: $<%# ObtenerPrecioCompra(Eval("ProductosXProveedores")) %></p>
                                <p class="card-text">Porcentaje Ganancia: <%#Eval("PorcentajeGanancia") %>%</p>
                                <p class="card-text">Stock Actual: <%#Eval("StockActual") %></p>
                                <p class="card-text">Stock Mínimo: <%#Eval("StockMinimo") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>


</asp:Content>
