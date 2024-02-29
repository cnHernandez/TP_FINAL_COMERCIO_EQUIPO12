<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ListarProductos.aspx.cs" Inherits="Comercio.ListarProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="Profesionales">Nuestros Productos</h1>

    <div class="login-container" style="display: flex; flex-direction: column; align-items: center; max-width: 600px; margin: 0 auto; margin-top: 10px;">
        <label for="txtNombre" style="margin-bottom: 10px;">Nombre del Producto: </label>
        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" Style="margin-bottom: 10px;"></asp:TextBox>
        <asp:Button runat="server" ID="btnBuscarProducto" Text="Buscar Producto" CssClass="btn btn-primary" OnClick="btnBuscarProducto_Click" />
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="dataGridViewProductos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos"
                AllowPaging="true" PageSize="5" OnPageIndexChanging="dataGridViewProductos_PageIndexChanging" OnSelectedIndexChanged="dataGridViewProductos_SelectedIndexChanged" OnRowDeleting="dataGridViewProductos_RowDeleting" OnRowCommand="dataGridViewProductos_RowCommand">
                <RowStyle CssClass="gridview-row" />
                <HeaderStyle CssClass="gridview-header" />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="PorcentajeGanancia" HeaderText="PorcentajeGanancia" />
                    <asp:BoundField DataField="StockActual" HeaderText="StockActual" />
                    <asp:BoundField DataField="StockMinimo" HeaderText="StockMinimo" />
                    <asp:BoundField DataField="IdMarca" HeaderText="IdMarca" />
                    <asp:BoundField DataField="IdCategoria" HeaderText="IdCategoria" />
                    <asp:TemplateField HeaderText="Imagen">
                        <ItemTemplate>
                            <asp:Image ID="Image" runat="server" ImageUrl='<%# Eval("UrlImagen") %>' Height="50" Width="50" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument='<%# Eval("IdProductos") %>' OnClientClick="return confirm('¿Seguro que desea eliminar este registro?');" CssClass="eliminar-button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Modificar" ShowSelectButton="true" SelectText="Modificar" ControlStyle-CssClass="modificar-button" />
                    <asp:TemplateField HeaderText="Agregar Proveedor y precio">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAgregarPxP" runat="server" Text="Agregar Proveedor" CssClass="eliminar-button" CommandName="AgregarProveedor" CommandArgument='<%# Eval("IdProductos") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>


        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="login-container" style="margin-bottom:100px;">
        <a href="AgregarProducto.aspx" class="btn btn-primary">Agregar Producto</a>
    </div>




</asp:Content>
