<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ResumenCompra.aspx.cs" Inherits="Comercio.ResumenCompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div>
        <h1>Resumen de Compra</h1>
        <asp:GridView ID="gvDetallesCompra" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDetallesCompra_RowDataBound">
            <Columns>
              <asp:BoundField DataField="IdProducto" HeaderText="ID" SortExpression="IdProducto" />
              <asp:BoundField DataField="NombreProducto" HeaderText="Producto" SortExpression="NombreProducto" />
                <asp:BoundField DataField="IdCompra" HeaderText="IDCompra" SortExpression="IDCompra" />
                <asp:BoundField DataField="NombreProveedor" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
                <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Unitario" SortExpression="PrecioCompra" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" SortExpression="Subtotal" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:Button ID="btnDescargarFactura" runat="server" Text="Descargar Factura" OnClick="btnDescargarFactura_Click"/>

</asp:Content>
