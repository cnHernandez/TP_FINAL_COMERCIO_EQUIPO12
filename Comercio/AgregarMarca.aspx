<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="AgregarMarca.aspx.cs" Inherits="Comercio.AgregarMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <section class="card-login">
        <div class="login-container">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre de la Marca</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" name="txtNombre" />
                <div id="NombreHelp" class="form-text">Ingrese el nombre de la Marca.</div>
                <asp:Label runat="server" ID="lblNombre" CssClass="text-danger" />
            </div>

            <div class="mb-3">
                <label for="txtUrl" class="form-label">Url de imagen</label>
                <asp:TextBox runat="server" ID="txtUrl" CssClass="form-control" name="txtUrl" AutoPostBack="true" OnTextChanged="txtURLImagen_TextChanged" />
                <div id="ApellidoHelp" class="form-text">Ingrese el Url del logo.</div>
                <asp:Label runat="server" ID="lblUrl" CssClass="text-danger" />
            </div>
            <div class="mb-3">
                <asp:Image runat="server" ID="imgMarca" CssClass="img-fluid" />
            </div>
            <div class="mb-3">
                <asp:Button runat="server" ID="btnAgregar" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />                
            </div>
        </div>
    </section>

    <div class="row">
        <div class="col-6">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
