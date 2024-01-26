<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Comercio.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script type="text/javascript">
     function calcularSubtotal(row) {
         var txtCantidad = row.querySelector('[id*="txtCantidad"]');
         var lblSubtotal = row.querySelector('[id*="lblSubtotal"]');

         if (txtCantidad && lblSubtotal) {
             var precioCompra = parseFloat(row.cells[2].innerText);
             var porcentajeVenta = parseFloat(row.cells[3].innerText);
             var cantidad = parseInt(txtCantidad.value);
             var subtotal = (precioCompra + (precioCompra * porcentajeVenta / 100)) * cantidad;

             lblSubtotal.innerText = subtotal.toFixed(2);

             // Llamar a la función que actualiza el total de la compra en el servidor
             __doPostBack('<%= btnFinalizarCompra.UniqueID %>', '');
             
         }
         
     }
 </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1 class="Profesionales">Compras.</h1>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">

        <ContentTemplate>

            <div class="mb-3" style="max-width: 300px; margin-left: 190px;">
                <label for="ddlProveedor" class="form-label">Proveedor</label>
                <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" DataTextField="Proveedor" DataValueField="IdProveedor" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged" AutoPostBack="True"/>
                <div id="ProveedorHelp" class="form-text">Seleccione el proveedor del producto</div>
            </div>

            <asp:GridView ID="dataGridViewProductos" runat="server" AutoGenerateColumns="False" CssClass="gridview-style" DataKeyNames="IdProductos"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="dataGridViewProductos_PageIndexChanging" OnRowDeleting="dataGridViewProductos_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="IdProductos" HeaderText="ID Producto" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="PrecioCompra" HeaderText="Precio Compra" />
                    <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Porcentaje Ganancia" />
                    <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                    <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" />
                    <asp:BoundField DataField="IdMarca" HeaderText="ID Marca" />
                    <asp:BoundField DataField="IdCategoria" HeaderText="ID Categoria" />
                    <asp:BoundField DataField="IdProveedor" HeaderText="ID Proveedor" />

                   <asp:TemplateField HeaderText="Cantidad a Comprar">
    <ItemTemplate>
        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="0" oninput="calcularSubtotal(this.parentNode.parentNode)" />
    </ItemTemplate>
</asp:TemplateField>
                    
 
                    <asp:TemplateField HeaderText="Subtotal">
                        <ItemTemplate>
                            <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMensajeError" runat="server" Text="" ForeColor="Red"></asp:Label>
            <div style="margin-top: 10px;">

                <label>Total de la Compra: </label>
                <asp:Label ID="lblTotalCompra" runat="server" CssClass="font-weight-bold"></asp:Label>
            </div>

            <div style="margin-top: 20px;">
                <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" OnClick="btnFinalizarCompra_Click" CssClass="btn btn-primary" />
            </div>
                

        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>