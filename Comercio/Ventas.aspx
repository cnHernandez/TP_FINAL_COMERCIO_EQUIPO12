<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Comercio.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container" style="display: flex; align-items: center; max-width: 600px; margin-left: 320px; margin-top: 10px;">
        <label for="txtNombre" style="margin-right: 10px; margin-bottom: 0;">Nombre del Producto: </label>
        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" Style="margin-right: 10px; margin-bottom: 0;"></asp:TextBox>
        <asp:Button runat="server" ID="btnBuscarProducto" Text="Buscar Producto" CssClass="btn btn-primary" Style="margin-top: 10px;" OnClick="btnBuscarProducto_Click" />

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
        <div class="login-container">
            <asp:Button runat="server" ID="btnAgregarSeleccionados" Text="Agregar Seleccionados" CssClass="btn btn-primary" OnClick="btnAgregarSeleccionados_Click" />
        </div>

        <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red" Text=""></asp:Label>


        <asp:GridView ID="dgvProductosSeleccionados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowDataBound="dgvProductosSeleccionados_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IdProducto" HeaderText="ID" SortExpression="IdProducto" />

                <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta" SortExpression="PrecioVenta" />


                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>

                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="0" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>



            </Columns>
        </asp:GridView>



    </div>
    <div class="login-container">
        <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" OnClick="btnFinalizarCompra_Click" CssClass="btn btn-primary" />
    </div>
    <div class="purchase-info-container" style="margin-bottom: 150px;">
        <label class="total-label">Total de la Venta:</label>
        <asp:Label ID="lblTotalVenta" runat="server" CssClass="font-weight-bold total-value"></asp:Label>
    </div>

</asp:Content>
