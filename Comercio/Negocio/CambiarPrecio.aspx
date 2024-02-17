<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="CambiarPrecio.aspx.cs" Inherits="Comercio.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="Profesionales">Compras.</h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <ContentTemplate>

            <div class="mb-3" style="max-width: 300px; margin-left: 190px;">
                <label for="ddlProveedor" class="form-label">Proveedor</label>
                <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" DataTextField="Proveedor" DataValueField="IdProveedor" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" AutoPostBack="True" />
                <div id="ProveedorHelp" class="form-text">Seleccione el proveedor del producto</div>
            </div>

            <div class="mb-3" style="max-width: 300px; margin-left: 190px;">
                <label for="ddlCat" class="form-label">Categorias</label>
                <asp:DropDownList runat="server" ID="ddlCat" CssClass="form-control" DataTextField="Categorias" DataValueField="IdCategoria" OnSelectedIndexChanged="ddlCat_SelectedIndexChanged" AutoPostBack="True" />
                <div id="CatHelp" class="form-text">Seleccione la categoria del producto</div>
            </div>
            <asp:GridView ID="dataGridViewProductos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos"
                AllowPaging="true" PageSize="5" OnPageIndexChanging="dataGridViewProductos_PageIndexChanging" OnRowDeleting="dataGridViewProductos_RowDeleting" OnRowDataBound="dataGridViewProductos_RowDataBound">
                <RowStyle CssClass="gridview-row" />
                <HeaderStyle CssClass="gridview-header" />
                <Columns>
                    <asp:BoundField DataField="IdProductos" HeaderText="ID Producto" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Porcentaje Ganancia" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                    <asp:BoundField DataField="IdMarca" HeaderText="ID Marca" />
                    <asp:BoundField DataField="IdCategoria" HeaderText="ID Categoria" />
                    <asp:BoundField DataField="IdProveedor" HeaderText="ID Proveedor" />
                    <asp:TemplateField HeaderText="Imagen">
                        <ItemTemplate>
                            <asp:Image ID="Image" runat="server" ImageUrl='<%# Eval("UrlImagen") %>' Height="50" Width="50" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nuevo Precio">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNuevoPrecio" runat="server" CssClass="form-control" Text="0" AutoPostBack="true" OnTextChanged="txtNuevoPrecio_TextChanged" Width="200px" Height="40px" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMensajeError" runat="server" Text="" ForeColor="Red"></asp:Label>

            <div class="login-container" style="margin-bottom: 150px">
                <asp:Button ID="btnFinalizarCambio" runat="server" Text="Guardar Cambios" OnClick="btnFinalizarCambio_Click" CssClass="btn btn-primary" AutoPostBack="false" />
               
            </div>




        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFinalizarCompra" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
