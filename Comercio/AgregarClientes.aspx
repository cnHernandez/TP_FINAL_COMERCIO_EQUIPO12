<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="AgregarClientes.aspx.cs" Inherits="Comercio.AgregarClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <section class="card-login">
        <div class="login-container">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre del Cliente</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" name="txtNombre" />
                <div id="NombreHelp" class="form-text">Ingrese el nombre del cliente.</div>
                <asp:Label runat="server" ID="lblNombre" CssClass="text-danger" Visible="false" />
            </div>

            <div class="mb-3">
                <label for="txtApellido" class="form-label">Apellido del Cliente</label>
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" name="txtApellido" />
                <div id="ApellidoHelp" class="form-text">Ingrese el apellido del cliente.</div>
                <asp:Label runat="server" ID="lblApellido" CssClass="text-danger" Visible="false" />
            </div>

            <div class="mb-3">
                <label for="txtDni" class="form-label">DNI del Cliente</label>
                <asp:TextBox runat="server" ID="txtDni" CssClass="form-control" name="txtDni" />
                <div id="DniHelp" class="form-text">Ingrese el DNI del Cliente.</div>
                <asp:Label runat="server" ID="lblDniError" CssClass="text-danger" Visible="false" />
            </div>

            <div class="mb-3">
                <label for="txtTelefono" class="form-label">Telefono</label>
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" name="txtTelefono" />
                <div id="TelefonoHelp" class="form-text">Ingrese el telefono del Cliente.</div>
                <asp:Label runat="server" ID="lblTelefono" CssClass="text-danger" Visible="false" />
            </div>

            <div class="mb-3">
                <label for="txtMail" class="form-label">Mail del Cliente</label>
                <asp:TextBox runat="server" ID="txtMail" CssClass="form-control" name="txtMail" />
                <div id="MailHelp" class="form-text">Ingrese el correo electrónico del cliente.</div>
                <asp:Label runat="server" ID="lblMail" CssClass="text-danger" Visible="false" />
            </div>

            <div class="mb-3">
                <asp:Button runat="server" ID="btnAgregar" Text="Aceptar" CssClass="btn btn-primary" OnClientClick="return validarFormulario();" OnClick="btnAgregar_Click" />
                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
            </div>

            <div class="row">
                <div class="col-6">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
