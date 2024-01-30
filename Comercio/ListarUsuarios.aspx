<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ListarUsuarios.aspx.cs" Inherits="Comercio.ListarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


          <h1 class="Profesionales">Nuestros Usuarios</h1>
<asp:ScriptManager ID="ScriptManager1" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="dataGridViewUsuarios" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdUsuario"
            AllowPaging="true" PageSize="10" OnPageIndexChanging="dataGridViewUsuarios_PageIndexChanging" OnSelectedIndexChanged="dataGridViewUsuarios_SelectedIndexChanged" OnRowDeleting="dataGridViewUsuarios_RowDeleting">
            <RowStyle CssClass="gridview-row" />
            <HeaderStyle CssClass="gridview-header" />
            <Columns>
                
                <asp:BoundField DataField="Nombre" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="Contrasena" HeaderText="Contraseña" />
                <asp:BoundField DataField="TipoUsuario" HeaderText="Tipo de Usuario" />
                
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument='<%# Eval("IdUsuario") %>' OnClientClick="return confirm('¿Seguro que desea eliminar este registro?');" CssClass="eliminar-button" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="Modificar" ShowSelectButton="true" SelectText="Modificar" ControlStyle-CssClass="modificar-button" />
            </Columns>
        </asp:GridView>

    </ContentTemplate>
</asp:UpdatePanel>
<div class="login-container">
    <a href="AgregarUsuario.aspx" class="btn btn-primary">Agregar Usuario</a>
</div>


</asp:Content>
