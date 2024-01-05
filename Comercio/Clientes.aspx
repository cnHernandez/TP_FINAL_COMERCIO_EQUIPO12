<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Comercio.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <h1 class="Profesionales">Nuestros Clientes</h1>
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="dataGridViewClientes" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdCliente"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="dataGridViewClientes_PageIndexChanging" OnSelectedIndexChanged="dataGridViewClientes_SelectedIndexChanged" OnRowDeleting="dataGridViewClientes_RowDeleting">
            <RowStyle CssClass="gridview-row" />
            <HeaderStyle CssClass="gridview-header" />
            <Columns>
                
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="Dni" HeaderText="Dni" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                <asp:BoundField DataField="Mail" HeaderText="Mail" />
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument='<%# Eval("IdCliente") %>' OnClientClick="return confirm('¿Seguro que desea eliminar este registro?');" CssClass="eliminar-button" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="Modificar" ShowSelectButton="true" SelectText="Modificar" ControlStyle-CssClass="modificar-button" />
            </Columns>
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>
<div class="login-container">
    <a href="AgregarClientes.aspx" class="btn btn-primary">Agregar Cliente</a>
</div>
</asp:Content>
