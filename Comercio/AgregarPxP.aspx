<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="AgregarPxP.aspx.cs" Inherits="Comercio.AgregarPxP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        body {
            background-image: url('https://www.solidbackgrounds.com/images/1920x1080/1920x1080-bottle-green-solid-color-background.jpg');
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-top: 30px; margin-bottom: 100px;">
        <div class="row">
            <asp:Repeater ID="reRepeater" runat="server" OnItemDataBound="reRepeater_ItemDataBound">
                <ItemTemplate>
                    <div class="col-md-4 mx-auto">
                        <div class="card mb-3">
                            <img src="<%#Eval("UrlImagen") %>" class="card-image card-img-top" alt="..." onerror="this.src='https://camarasal.com/wp-content/uploads/2020/08/default-image-5-1.jpg'">
                            <div class="card-body">
                                <h3 class="card-subtitle mb-2 text-muted"><%#Eval("Nombre") %></h3>
                                <h5 class="card-title">Id: <%#Eval("IdProductos") %></h5>
                                <p class="card-text">Porcentaje Ganancia: <%#Eval("PorcentajeGanancia") %>%</p>
                                <p class="card-text">Stock Actual: <%#Eval("StockActual") %></p>
                                <p class="card-text">Stock Mínimo: <%#Eval("StockMinimo") %></p>

                                <div>
                                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="Precio"></asp:TextBox>
                                </div>
                                <div class="login-container">
                                    <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-primary btn-block" Text="Agregar" OnClick="btnAgregar_Click" />
                                </div>
                                <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
