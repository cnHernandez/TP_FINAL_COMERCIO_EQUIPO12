<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="AgregarProveedor.aspx.cs" Inherits="Comercio.AgregarProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <section class="card-login">
        <div class="login-container">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre del Proveedor</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" name="txtNombre" />
                <div id="NombreHelp" class="form-text">Ingrese el nombre de la Marca.</div>
                <asp:Label runat="server" ID="lblErrorNombre" CssClass="text-danger" Visible="false"/>
            </div>

            <div class="mb-3">
                <label for="txtUrl" class="form-label">Url de imagen</label>
                <asp:TextBox runat="server" ID="txtUrl" CssClass="form-control" name="txtUrl" AutoPostBack="true" OnTextChanged="txtUrl_TextChanged" />
                <div id="UrlHelp" class="form-text">Ingrese el Url del logo.</div>
                <asp:Label runat="server" ID="lblErrorUrl" CssClass="text-danger" Visible="false"/>
            </div>

            <div class="mb-3">
                <label for="txtCategoria" class="form-label">Categoria o Rubro del Proveedor</label>
                <asp:TextBox runat="server" ID="txtCategoria" CssClass="form-control" name="txtCategoria" AutoPostBack="true" />
                <div id="CategoriaHelp" class="form-text">Ingrese la categoria o rubro del Proveedor.</div>
                <asp:Label runat="server" ID="lblErrorCat" CssClass="text-danger" Visible="false"/>
            </div>

            <div class="mb-3">
                <asp:Image runat="server" ID="imgProveedor" CssClass="img-fluid" />
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
