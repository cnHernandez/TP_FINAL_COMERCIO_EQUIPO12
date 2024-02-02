<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="Comercio.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container" style="display: flex; align-items: center; max-width: 600px; margin-left: 320px; margin-top:10px;" >
        <label for="txtNombre" style="margin-right: 10px; margin-bottom: 0;">Nombre del Producto: </label>
        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" style="margin-right: 10px; margin-bottom: 0;"></asp:TextBox>
       <asp:Button runat="server" ID="btnBuscarProducto" Text="Buscar Producto" CssClass="btn btn-primary" style="margin-top:10px;" OnClick="btnBuscarProducto_Click" />

    </div>

    <div class="container" style="margin-top: 30px; margin-bottom: 100px;">
       <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
    OnRowDataBound="dgvProductos_RowDataBound">
    <Columns>
        <asp:BoundField DataField="IdProductos" HeaderText="ID" SortExpression="IdProductos" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
        <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" SortExpression="PrecioCompra" />
        <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Porcentaje Ganancia" SortExpression="PorcentajeGanancia" />
        <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" SortExpression="StockActual" />
        <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" SortExpression="StockMinimo" />
        <asp:TemplateField HeaderText="Seleccionar">
            <ItemTemplate>
                <asp:CheckBox ID="chkSeleccionar" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        <asp:Button runat="server" ID="btnAgregarSeleccionados" Text="Agregar Seleccionados" CssClass="btn btn-primary" OnClick="btnAgregarSeleccionados_Click" />

        <asp:GridView ID="dgvProductosSeleccionados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
   
             <Columns>
                <asp:BoundField DataField="IdProductos" HeaderText="ID" SortExpression="IdProductos" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" SortExpression="PrecioCompra" />
                <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Porcentaje Ganancia" SortExpression="PorcentajeGanancia" />
                <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" SortExpression="StockActual" />
                <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" SortExpression="StockMinimo" />
                
            </Columns>
</asp:GridView>

    </div>
</asp:Content>
