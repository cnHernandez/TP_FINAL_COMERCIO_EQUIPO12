<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ListarCategorias.aspx.cs" Inherits="Comercio.ListarCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="login-container">
      <a href="AgregarCategoria.aspx" class="btn btn-primary">Agregar Categoria</a>
 </div>

 <div class="container" style="margin-top: 30px">
     <div class="row">
         <asp:Repeater ID="repRepeater" runat="server">
             <ItemTemplate>
                 <div class="col-md-4">
                     <div class="card mb-3">
                         <img src="<%#Eval("UrlImagen") %>" class="card-image card-img-top" alt="..." onerror="this.src='https://camarasal.com/wp-content/uploads/2020/08/default-image-5-1.jpg'">
                         <div class="card-body">
                             <h5 class="card-title"><%#Eval("Nombre") %></h5>
                             <div class="login-container">
                                 <a href='<%# "AgregarCategoria.aspx?IdCategoria=" + Eval("IdCategoria") %>' class="btn btn-primary btn-block">Modificar</a>
                                 <asp:Button runat="server" ID="btnEliminar" CssClass="btn btn-danger btn-block eliminar-btn" Text="Eliminar" OnClientClick='<%# "return confirmarEliminacion(" + Eval("IdCategoria") + ");" %>' OnClick="btnEliminar_Click" CommandArgument='<%# Eval("IdCategoria") %>' />
                             </div>

                         </div>
                     </div>
                 </div>
             </ItemTemplate>
         </asp:Repeater>

     </div>

 </div>

 <script>
     function confirmarEliminacion(idMarcas) {
         // Muestra un modal de Bootstrap para confirmar la eliminación
         return confirm("¿Estás seguro de que quieres eliminar esta categoria?");
     }
 </script>

</asp:Content>
