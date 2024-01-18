<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Comercio.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="Profesionales">Nuestros Productos</h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="mb-3" style="max-width: 300px; margin-left: 190px;">
                <!-- Puedes ajustar el valor según tus necesidades -->
                <label for="ddlProveedor" class="form-label">Proveedor</label>
                <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" DataTextField="Proveedor" DataValueField="IdProveedor" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" AutoPostBack="True"/>
                <div id="ProveedorHelp" class="form-text">Seleccione el proveedor del producto</div>
            </div>

            <asp:GridView ID="dataGridViewProductos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="dataGridViewProductos_PageIndexChanging" OnSelectedIndexChanged="dataGridViewProductos_SelectedIndexChanged" OnRowDeleting="dataGridViewProductos_RowDeleting">
                <RowStyle CssClass="gridview-row" />
                <HeaderStyle CssClass="gridview-header" />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                    <asp:BoundField DataField="IdMarca" HeaderText="Id Marca" />
                    <asp:TemplateField HeaderText="Cantidad a agregar">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="0" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agregar a la compra">
                        <ItemTemplate>
                            <asp:Button ID="btnAgregarCompra" runat="server" Text="Agregar" CommandName="AgregarCompra" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary" OnClientClick="return confirm('¿Seguro que desea agregar este producto a la compra?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <h2 class="Profesionales">Productos en la Compra</h2>
            <asp:GridView ID="dataGridViewCompra" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos">
                <RowStyle CssClass="gridview-row" />
                <HeaderStyle CssClass="gridview-header" />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                    <asp:BoundField DataField="IdMarca" HeaderText="Id Marca" />
                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEliminarCompra" runat="server" Text="Eliminar" CommandName="EliminarCompra" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('¿Seguro que desea eliminar este producto de la compra?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
