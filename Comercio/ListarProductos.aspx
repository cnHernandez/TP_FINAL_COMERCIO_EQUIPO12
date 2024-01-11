﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ListarProductos.aspx.cs" Inherits="Comercio.ListarProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="Profesionales">Nuestros Productos</h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="dataGridViewProductos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="dataGridViewProductos_PageIndexChanging" OnSelectedIndexChanged="dataGridViewProductos_SelectedIndexChanged" OnRowDeleting="dataGridViewProductos_RowDeleting">
                <RowStyle CssClass="gridview-row" />
                <HeaderStyle CssClass="gridview-header" />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="PrecioCompra" HeaderText="PrecioCompra" />
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
                </Columns>
            </asp:GridView>


        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="login-container">
        <a href="AgregarProducto.aspx" class="btn btn-primary">Agregar Producto</a>
    </div>




</asp:Content>