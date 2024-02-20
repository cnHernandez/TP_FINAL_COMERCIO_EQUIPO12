<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="ListarMarcas.aspx.cs" Inherits="Comercio.ListarMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="margin-top: 30px; margin-bottom: 80px;">

        <div class="login-container" style="display: flex; flex-direction: column; align-items: center; max-width: 600px; margin: 0 auto; margin-top: 10px;">
            <label for="txtNombre" style="margin-bottom: 10px;">Nombre de la Marca: </label>
            <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" Style="margin-bottom: 10px;"></asp:TextBox>
            <asp:Button runat="server" ID="btnBuscarMarca" Text="Buscar Marca" CssClass="btn btn-primary" OnClick="btnBuscarMarca_Click" />
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
                                    <div class="button-container">
                                        <a href='<%# "AgregarMarca.aspx?IdMarcas=" + Eval("IdMarcas") %>' class="btn btn-primary btn-block">Modificar</a>
                                        <asp:Button runat="server" ID="btnEliminar" CssClass="btn btn-danger btn-block eliminar-btn" Text="Eliminar" OnClientClick='<%# "return confirmarEliminacion(" + Eval("IdMarcas") + ");" %>' OnClick="btnEliminar_Click" CommandArgument='<%# Eval("IdMarcas") %>' />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>

        </div>
    </div>
    <div class="login-container" style="margin-bottom: 100px;">
        <a href="AgregarMarca.aspx" class="btn btn-primary">Agregar Marca</a>
    </div>
    <script>
        function confirmarEliminacion(idMarcas) {
            // Muestra un modal de Bootstrap para confirmar la eliminación
            return confirm("¿Estás seguro de que quieres eliminar esta marca?");
        }
    </script>

</asp:Content>
