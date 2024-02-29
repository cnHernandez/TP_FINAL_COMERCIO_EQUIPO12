<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="DefaultVendedor.aspx.cs" Inherits="Comercio.DefaultVendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="cards-container">

        <!-- Card 5 -->
        <a href="Cliente.aspx" class="custom-card">
            <i class="fas fa-shopping-cart fa-3x"></i>
            <h2>Ventas</h2>
        </a>

        <!-- Card 1 -->
        <a href="BuscarProductos.aspx" class="custom-card">
            <i class="fas fa-list-ul fa-3x"></i>
            <h2>Buscar Producto</h2>
        </a>

        <a href="AgregarClientes.aspx" class="custom-card">
            <i class="fas fa-list-ul fa-3x"></i>
            <h2>Registrar Cliente</h2>
        </a>

        <a href="RegistrosVentas.aspx" class="custom-card">
            <i class="fas fa-file-invoice-dollar fa-3x"></i>
            <h2>Registros de Ventas</h2>
        </a>


    </div>


</asp:Content>
