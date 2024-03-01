<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="CambioDePrecio.aspx.cs" Inherits="Comercio.CambioDePrecio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="Profesionales">Cambio de Precio</h1>

    <div class="mb-3" style="max-width: 300px; margin-left: 190px;">
        <label for="ddlProveedor" class="form-label">Proveedor</label>
        <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" DataTextField="Proveedor" DataValueField="IdProveedor" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" AutoPostBack="True" />
        <div id="ProveedorHelp" class="form-text">Seleccione el proveedor del producto</div>
    </div>

    <asp:GridView ID="dataGridViewProductos1" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos"
    AllowPaging="true"  OnPageIndexChanging="dataGridViewProductos_PageIndexChanging" OnRowDataBound="dataGridViewProductos_RowDataBound">
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
        <asp:TemplateField HeaderText="Precio Compra">
            <ItemTemplate>
                <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="form-control" Text='<%# Eval("ProductosXProveedores[0].PrecioCompra") %>' Width="80px"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ProductosXProveedores[0].ProveedorID" HeaderText="ProveedorID" />
        <asp:TemplateField HeaderText="Imagen">
            <ItemTemplate>
                <asp:Image ID="Image" runat="server" ImageUrl='<%# Eval("UrlImagen") %>' Height="50" Width="50" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

    <asp:Label ID="lblMensajeError1" runat="server" Text="" ForeColor="Red"></asp:Label>

    <div class="login-container" style="margin-bottom: 150px">
        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" onclick="btnGuardarCambios_Click" OnClientClick="mostrarMensaje();"  CssClass="btn btn-primary" AutoPostBack="false" />
        <div class="total-container">
            
        </div>
    </div>

    <script type="text/javascript">
    function mostrarMensaje() {
        alert('Guardado correctamente.');
        window.location.href = 'default.aspx';
    }
    </script>t>
</asp:Content>
