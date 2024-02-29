<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="AgregarUsuario.aspx.cs" Inherits="Comercio.AgregarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <asp:ScriptManager ID="ScriptManager1" runat="server" />

   <section class="card-login">
       <div class="login-container">
           <div class="mb-3">
               <label for="txtNombre" class="form-label">Nombre del Usuario</label>
               <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" name="txtNombre" />
               <div id="NombreHelp" class="form-text">Ingrese el nombre del Usuario.</div>
               <asp:Label runat="server" ID="lblNombre" CssClass="text-danger" Visible="false" />
           </div>

           <div class="mb-3">
               <label for="txtContraseña" class="form-label">Contraseña</label>
               <asp:TextBox runat="server" ID="txtContraseña" CssClass="form-control" name="txtContraseña" />
               <div id="ContraseñaHelp" class="form-text">Ingrese la contraseña del usuario.</div>
               <asp:Label runat="server" ID="lblPass" CssClass="text-danger" Visible="false" />
           </div>

     <div class="mb-3">
    <label for="ddlTipoUsuario" class="form-label">Tipo</label>
    <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" />
    <div id="TipoHelp" class="form-text">Seleccione el tipo de Usuario</div>
</div>

    
           <div class="mb-3">
               <asp:Button runat="server" ID="btnAgregar" Text="Aceptar" CssClass="btn btn-primary" OnClientClick="return validarFormulario();" OnClick="btnAgregar_Click" />
               <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
               <asp:Label runat="server" ID="lblMensaje" CssClass="error-message" />
           </div>

       </div>
   </section>

</asp:Content>
