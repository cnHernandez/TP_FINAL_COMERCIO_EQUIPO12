<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="AgregarProducto.aspx.cs" Inherits="Comercio.AgregarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <section class="card-login">
        <div class="login-container">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre del Producto</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" name="txtNombre" />
                <div id="NombreHelp" class="form-text">Ingrese el nombre del Producto.</div>
                <asp:Label runat="server" ID="lblNombreError" CssClass="text-danger" Visible="false" />
            </div>

            <div class="mb-3">
                <label for="txtPorcentaje" class="form-label">Porcentaje de ganancia</label>
                <asp:TextBox runat="server" ID="txtPorcentaje" CssClass="form-control" name="txtPorcentaje" />
                <div id="PorcentajeHelp" class="form-text">Ingrese el Porcentaje de ganancia.</div>
                <asp:Label runat="server" ID="lblPorcentajeError" CssClass="text-danger" Visible="false" />
            </div>

         

            <div class="mb-3">
                <label for="txtMinimo" class="form-label">Stock Minimo</label>
                <asp:TextBox runat="server" ID="txtMinimo" CssClass="form-control" name="txtMinimo" />
                <div id="MinimoHelp" class="form-text">Ingrese el stock minimo.</div>
                <asp:Label runat="server" ID="lblMinimo" CssClass="text-danger" Visible="false" />
            </div>

            <div class="mb-3">
                <label for="ddlTipo" class="form-label">Tipo</label>
                <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" DataTextField="Tipo" DataValueField="IdCategoria" />
                <div id="TipoHelp" class="form-text">Seleccione el tipo de producto</div>
            </div>

            <div class="mb-3">
                <label for="ddlMarca" class="form-label">Marca</label>
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-control" DataTextField="Marca" DataValueField="IdMarcas" />
                <div id="MarcaHelp" class="form-text">Seleccione la marca del producto</div>
            </div>
            <div class="mb-3">
                <label for="txtUrl" class="form-label">Url de imagen</label>
                <asp:TextBox runat="server" ID="txtUrl" CssClass="form-control" name="txtUrl" AutoPostBack="true" OnTextChanged="txtURLImagen_TextChanged" />
                <div id="ProductoHelp" class="form-text">Ingrese el Url del producto.</div>
                <asp:Label runat="server" ID="lblUrlError" CssClass="text-danger" Visible="false" />
            </div>

            <div class="mb-3">
                <asp:Image runat="server" ID="imgProducto" CssClass="img-fluid" />
            </div>
            <div class="mb-3">
                <asp:Button runat="server" ID="btnAgregar" Text="Aceptar" CssClass="btn btn-primary" OnClientClick="return validarFormulario();" OnClick="btnAgregar_Click" />
                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" />
                <asp:Label runat="server" ID="lblMensaje" CssClass="error-message" />
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
