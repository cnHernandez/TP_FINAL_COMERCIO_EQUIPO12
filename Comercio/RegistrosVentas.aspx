<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="RegistrosVentas.aspx.cs" Inherits="Comercio.RegistrosVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="Profesionales">Registros de Ventas</h1>

    <div class="login-container" style="display: flex; flex-direction: column; align-items: center; max-width: 600px; margin: 0 auto; margin-top: 10px;">
        <label for="txtNombre" style="margin-bottom: 10px;">ID de Venta: </label>
        <asp:TextBox runat="server" ID="txtIdVenta" CssClass="form-control" Style="margin-bottom: 10px;"></asp:TextBox>
        <asp:Button runat="server" ID="btnBuscarVenta" Text="Buscar Venta" CssClass="btn btn-primary" OnClick="btnBuscarVenta_Click" />
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="dataGridViewVentas" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdVenta"
                AllowPaging="true" PageSize="5" OnPageIndexChanging="dataGridViewVentas_PageIndexChanging" OnRowCommand="dataGridViewVentas_RowCommand" OnRowDataBound="dataGridViewVentas_RowDataBound">
                <RowStyle CssClass="gridview-row" />
                <HeaderStyle CssClass="gridview-header" />
                <Columns>
                    <asp:BoundField DataField="IdVenta" HeaderText="ID Compra" />
                    <asp:BoundField DataField="IdCliente" HeaderText="ID Cliente" />  
                     <asp:TemplateField HeaderText="Cliente">
                     <ItemTemplate>
                     <asp:Label ID="lblCliente" runat="server"></asp:Label>
                     </ItemTemplate>
                     </asp:TemplateField>
                    <asp:BoundField DataField="FechaVenta" HeaderText="Fecha de Venta" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="TotalVenta" HeaderText="Total de Venta" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Ver Detalle">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDetallesVenta" runat="server" Text="Ver Detalle" CommandName="DetallesVenta" CommandArgument='<%# Eval("IdVenta") %>' CssClass="eliminar-button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Label ID="lblTotalFacturado" runat="server" CssClass="total-label" Style="float: right; margin-right: 180px;"></asp:Label>

</asp:Content>
