<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ResumenCompra.aspx.cs" Inherits="Comercio.ResumenCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
             background-image: url('https://www.solidbackgrounds.com/images/1920x1080/1920x1080-bottle-green-solid-color-background.jpg');
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container-custom">
        <h1 class="custom-heading">Resumen de Compra</h1>
        <asp:GridView ID="gvDetallesCompra" CssClass="table-custom" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDetallesCompra_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IdProducto" HeaderText="ID" SortExpression="IdProducto" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" SortExpression="NombreProducto" />
                <asp:BoundField DataField="IdCompra" HeaderText="IDCompra" SortExpression="IDCompra" />
                <asp:BoundField DataField="NombreProveedor" HeaderText="Proveedor" SortExpression="Nombre" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
                <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Unitario" SortExpression="PrecioCompra" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" SortExpression="Subtotal" />
            </Columns>
        </asp:GridView>
    <div class="login-container">
        <asp:Button ID="btnDescargarFactura" CssClass="btn btn-primary" runat="server" Text="Descargar Factura" OnClick="btnDescargarFactura_Click"/>
        <asp:Button ID="btnIrAPaginaPrincipal" CssClass="btn btn-primary" runat="server" Text="Volver" OnClick="btnIrAPaginaPrincipal_Click" />

        </div>
    </div>
</asp:Content>
