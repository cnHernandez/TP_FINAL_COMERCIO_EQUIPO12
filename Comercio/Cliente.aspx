<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="Comercio.Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: url('https://images.unsplash.com/photo-1556740772-1a741367b93e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D');
        }

        /* Nueva clase para la tarjeta */
        .new-card {
            background-color: rgba(255, 255, 255, 0.8);
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.3);
            max-width: 600px;
            margin: 130px auto 20px; /* Margen superior de 50px y margen inferior de 20px, centrado horizontalmente */
            padding: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="new-card"> <!-- Cambio de clase a "new-card" -->
        <div class="container-custom">
            <h1 class="custom-heading">Cliente</h1>
            <div class="login-container" style="display: flex; align-items: center;">
                <label for="txtDni" style="margin-right: 10px; margin-bottom: 0;">DNI/Nro del cliente:  </label>
                <asp:TextBox runat="server" ID="txtDniCliente" CssClass="form-control" onkeypress="return soloNumeros(event);" />
                <asp:Button runat="server" ID="btnBuscarCliente" Text="Buscar Cliente" CssClass="btn btn-primary" Style="margin-top: 10px;" OnClick="btnBuscarCliente_Click" />
            </div>
            <asp:Label ID="lblMensajeClienteNoEncontrado" runat="server" ForeColor="Red" Text=""></asp:Label>
            <asp:Button ID="btnVolverAVenta" runat="server" Text="Volver a Venta" CssClass="btn btn-primary" OnClick="btnVolverAVenta_Click" Visible="false" />
        </div>
    </div>

    <script type="text/javascript">
        function soloNumeros(e) {
            var key = e.charCode || e.keyCode || 0;
            return key >= 48 && key <= 57;
        }
    </script>
</asp:Content>


