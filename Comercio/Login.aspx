<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Comercio.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="card-login">
        <div class="login-container">
            <div class="row">
                <div class="col-md-6 offset-md-3">
                    <div>
                        <h2 class="Profesionales">Iniciar Sesión</h2>
                        <div class="form-group">
                            <label for="txtUsername">Nombre de Usuario:<br />
                                <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="form-group">
                            <label for="txtPassword">Contraseña:</label>
                            <br />
                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" style="margin-left: 25px;"></asp:TextBox>
                            <span id="togglePassword" style="cursor: pointer;" onclick="togglePasswordVisibility()">
                                <img src="https://e7.pngegg.com/pngimages/946/952/png-clipart-workforce-planning-business-unicornio-angle-company.png" alt="Toggle Password Visibility" width="20" height="20" />
                            </span>
                        </div>
                        <asp:Button runat="server" ID="btnIngresar" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>

    <script>
        function togglePasswordVisibility() {
            var passwordInput = document.getElementById('<%= txtPass.ClientID %>');
            var toggleIcon = document.getElementById('togglePassword').getElementsByTagName('img')[0];
            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                toggleIcon.src = "https://img1.freepng.fr/20180723/hcy/kisspng-human-eye-computer-icons-symbol-eye-examination-eyelashes-icon-5b56a0c98daef2.9027171015324039135803.jpg";
            } else {
                passwordInput.type = "password";
                toggleIcon.src = "https://e7.pngegg.com/pngimages/946/952/png-clipart-workforce-planning-business-unicornio-angle-company.png";
            }
        }
    </script>
</asp:Content>