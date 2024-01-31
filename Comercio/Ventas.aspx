<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Comercio.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:GridView ID="dataGridViewProductos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style"
    DataKeyNames="IdProductos" AllowPaging="true" PageSize="10" OnPageIndexChanging="dataGridViewProductos_PageIndexChanging"
    OnRowDeleting="dataGridViewProductos_RowDeleting" OnRowCommand="dataGridViewProductos_RowCommand">
    <Columns>
        <asp:BoundField DataField="IdProductos" HeaderText="ID Producto" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" />
                    <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Porcentaje Ganancia" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                    <asp:BoundField DataField="IdMarca" HeaderText="ID Marca" />
                    <asp:BoundField DataField="IdCategoria" HeaderText="ID Categoria" />
                    <asp:BoundField DataField="IdProveedor" HeaderText="ID Proveedor" />

        <asp:TemplateField HeaderText="Acciones">
            <ItemTemplate>
                <asp:Button runat="server" CommandName="Agregar" CommandArgument='<%# Eval("IdProductos") %>'
                    Text="Agregar" CssClass="btn btn-success" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


<!-- Área de búsqueda -->
<!-- ... -->

<!-- Área para mostrar productos seleccionados -->
<asp:GridView ID="dataGridViewProductosSeleccionados" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos">
    <!-- Definir las columnas aquí -->
</asp:GridView>

<!-- Área para mostrar el total de la venta -->
<asp:Label ID="lblTotalVenta" runat="server" CssClass="font-weight-bold"></asp:Label>

<!-- Botón para finalizar la venta -->
<asp:Button ID="btnFinalizarVenta" runat="server" Text="Finalizar Venta" OnClick="btnFinalizarVenta_Click" CssClass="btn btn-primary" />

