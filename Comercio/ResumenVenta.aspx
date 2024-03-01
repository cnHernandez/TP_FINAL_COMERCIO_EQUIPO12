<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ResumenVenta.aspx.cs" Inherits="Comercio.ResumenVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: url('https://www.solidbackgrounds.com/images/1920x1080/1920x1080-bottle-green-solid-color-background.jpg');
        }

        /* Nuevos estilos para la tabla */
        .table-custom {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px; /* Añadimos un margen superior */
        }

            .table-custom th,
            .table-custom td {
                padding: 8px;
                border: 1px solid #ddd;
                text-align: left;
            }

            .table-custom th {
                background-color: #f2f2f2;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-custom">

        <h1 class="custom-heading">Resumen de Venta</h1>
        <div>
            <h3>Cliente: <asp:Label ID="lblNombreCliente" runat="server" Text=""></asp:Label></h3>
            <h3>Proveedor: Mercado Util</h3>
        </div>        

        <asp:GridView ID="gvDetallesVenta" CssClass="table-custom" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvDetallesVenta_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IdProducto" HeaderText="ID" SortExpression="IdProducto" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" SortExpression="NombreProducto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />
                <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Unitario" SortExpression="PrecioVenta" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" SortExpression="Subtotal" />
            </Columns>
        </asp:GridView>
        <div class="login-container">
            <asp:Button ID="btnDescargarFactura" CssClass="btn btn-primary" runat="server" Text="Descargar Factura" OnClick="btnDescargarFactura_Click" />
            <asp:Button ID="btnIrAPaginaPrincipal" CssClass="btn btn-primary" runat="server" Text="Volver" OnClick="btnIrAPaginaPrincipal_Click" />
        </div>
    </div>

</asp:Content>