<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="RegistrosCompras.aspx.cs" Inherits="Comercio.RegistrosCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1 class="Profesionales">Registros de Compras</h1>

    <div class="login-container" style="display: flex; flex-direction: column; align-items: center; max-width: 600px; margin: 0 auto; margin-top: 10px;">
        <label for="txtNombre" style="margin-bottom: 10px;">ID de Compra: </label>
        <asp:TextBox runat="server" ID="txtIdCompra" CssClass="form-control" Style="margin-bottom: 10px;"></asp:TextBox>
        <asp:Button runat="server" ID="btnBuscarCompra" Text="Buscar Compra" CssClass="btn btn-primary" OnClick="btnBuscarCompra_Click" />
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="dataGridViewCompras" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdCompras"
                AllowPaging="true" PageSize="5" OnPageIndexChanging="dataGridViewCompras_PageIndexChanging" OnSelectedIndexChanged="dataGridViewCompras_SelectedIndexChanged" OnRowDeleting="dataGridViewCompras_RowDeleting" OnRowCommand="dataGridViewCompras_RowCommand" OnRowDataBound="dataGridViewCompras_RowDataBound">
                <RowStyle CssClass="gridview-row" />
                <HeaderStyle CssClass="gridview-header" />
                <Columns>
                    <asp:BoundField DataField="IdCompras" HeaderText="ID Compra" />
                    <asp:BoundField DataField="IdProveedor" HeaderText="ID Proveedor" />
                    <asp:TemplateField HeaderText="Proveedor">
                        <ItemTemplate>
                            <asp:Label ID="lblProveedor" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FechaCompra" HeaderText="Fecha de Compra" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="TotalCompra" HeaderText="Total de Compra" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Ver Detalle">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDetallesCompra" runat="server" Text="Ver Detalle" CommandName="DetallesCompra" CommandArgument='<%# Eval("IdCompras") %>' CssClass="eliminar-button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblTotalFacturado" runat="server" CssClass="total-label" Style="float: right; margin-right: 180px;"></asp:Label>

</asp:Content>
