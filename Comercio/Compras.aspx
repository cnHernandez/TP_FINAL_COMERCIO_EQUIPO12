<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Comercio.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="Profesionales">Nuestros Productos</h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="mb-3" style="max-width: 300px; margin-left: 190px;">
                <label for="ddlProveedor" class="form-label">Proveedor</label>
                <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" DataTextField="Proveedor" DataValueField="IdProveedor" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" AutoPostBack="True"/>
                <div id="ProveedorHelp" class="form-text">Seleccione el proveedor del producto</div>
            </div>

            <asp:GridView ID="dataGridViewProductos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="dataGridViewProductos_PageIndexChanging" OnRowDeleting="dataGridViewProductos_RowDeleting">
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

                    <asp:TemplateField HeaderText="Cantidad a Comprar">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="0" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subtotal">
                        <ItemTemplate>
                            <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div style="margin-top: 20px;">
                <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" OnClick="btnFinalizarCompra_Click" CssClass="btn btn-primary" />
            </div>
            <div style="margin-top: 10px;">
                <label>Total de la Compra: </label>
                <asp:Label ID="lblTotalCompra" runat="server" CssClass="font-weight-bold"></asp:Label>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>