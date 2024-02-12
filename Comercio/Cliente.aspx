<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="Comercio.Cliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container" style="display: flex; align-items: center; max-width: 600px; margin-left: 320px; margin-top: 10px;">
        <label for="txtDni" style="margin-right: 10px; margin-bottom: 0;">Esta Registrado ? Por favor ingresa el DNI/Nro del cliente:  </label>
        <asp:TextBox runat="server" ID="txtDniCliente" CssClass="form-control" onkeypress="return soloNumeros(event);" />
        <asp:Button runat="server" ID="btnBuscarCliente" Text="Buscar Cliente" CssClass="btn btn-primary" Style="margin-top: 10px;" OnClick="btnBuscarCliente_Click" />
    </div>
    <asp:Label ID="lblMensajeClienteNoEncontrado" runat="server" ForeColor="Red" Text=""></asp:Label>
    <asp:Button ID="btnVolverAVenta" runat="server" Text="Volver a Venta" CssClass="btn btn-primary" OnClick="btnVolverAVenta_Click" Visible="false" />
<script type="text/javascript">
    function soloNumeros(e) {
        var key = e.charCode || e.keyCode || 0;
        
        return key >= 48 && key <= 57;
    }
</script>
</asp:Content>

