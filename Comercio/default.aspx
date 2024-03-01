<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Comercio._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="cards-container">

        <!-- Card 5 -->
        <a href="Compra.aspx" class="custom-card">
            <i class="fas fa-shopping-cart fa-3x"></i>
            <h2>Compra de stock</h2>
        </a>

        <!-- Card 1 -->
        <a href="ListarProductos.aspx" class="custom-card">
            <i class="fas fa-list-ul fa-3x"></i>
            <h2>Productos</h2>
        </a>

        <!-- Card 2 -->
        <a href="ListarProveedores.aspx" class="custom-card">
            <i class="fas fa-truck fa-3x"></i>
            <h2>Proveedores</h2>
        </a>

        <!-- Card 3 -->
        <a href="ListarMarcas.aspx" class="custom-card">
            <i class="fas fa-industry fa-3x"></i>
            <h2>Marcas</h2>
        </a>

        <!-- Card 4 -->
        <a href="Clientes.aspx" class="custom-card">
            <i class="fas fa-users fa-3x"></i>
            <h2>Clientes</h2>
        </a>

        <!-- Card 6 -->
        <a href="ListarCategorias.aspx" class="custom-card">
            <i class="fas fa-tags fa-3x"></i>
            <h2>Categorias</h2>
        </a>

        <a href="CambioDePrecio.aspx" class="custom-card">
           <i class="fas fa-dollar-sign fa-3x"></i>
            <h2>Cambio De Precio</h2>
        </a>

        <a href="RegistrosCompras.aspx" class="custom-card">
             <i class="fas fa-file-invoice-dollar fa-3x"></i>
            <h2>Registro de compra de stock</h2>
        </a>
    </div>


</asp:Content>
