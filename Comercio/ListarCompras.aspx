<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ListarCompras.aspx.cs" Inherits="Comercio.ListarCompras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

              <h1 class="Profesionales">Nuestras Compras</h1>
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="dataGridViewCompras" runat="server" OnRowDataBound="dataGridViewCompras_RowDataBound" OnRowCommand="dataGridViewCompras_RowCommand" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdCompra"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="dataGridViewCompras_PageIndexChanging" OnSelectedIndexChanged="dataGridViewCompras_SelectedIndexChanged" OnRowDeleting="dataGridViewCompras_RowDeleting">
            <RowStyle CssClass="gridview-row" />
            <HeaderStyle CssClass="gridview-header" />
            <Columns>
                
                <asp:BoundField DataField="IdCompra" HeaderText="Id de Compra" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Nombre Producto" />
                <asp:BoundField DataField="NombreProveedor" HeaderText="Nombre Proveedor" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha Compra" />
                
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument='<%# Eval("IdCompra") %>' OnClientClick="return confirm('¿Seguro que desea eliminar este registro?');" CssClass="eliminar-button" />
                    </ItemTemplate>
                </asp:TemplateField>
                
      <asp:TemplateField HeaderText="Ver Detalle">
    <ItemTemplate>
         <asp:Button ID="btnVerDetalle" runat="server" Text="Ver Detalle" CommandName="VerDetalle" CommandArgument='<%# Eval("IdCompra") %>' />
    </ItemTemplate>
</asp:TemplateField>

            </Columns>
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>

     <asp:Label ID="lblTotalFacturado" runat="server" CssClass="total-label" style="float: right; margin-right: 180px;"></asp:Label>

</asp:Content>
