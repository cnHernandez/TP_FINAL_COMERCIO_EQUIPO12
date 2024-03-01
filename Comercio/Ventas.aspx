<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="Comercio.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="login-container" style="display: flex; flex-direction: column; align-items: center; max-width: 600px; margin: 0 auto; margin-top: 10px;">
        <label for="txtNombre" style="margin-bottom: 10px;">Nombre del Producto: </label>
        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" Style="margin-bottom: 10px;"></asp:TextBox>
        <asp:Button runat="server" ID="btnBuscarProducto" Text="Buscar Producto" CssClass="btn btn-primary" OnClick="btnBuscarProducto_Click" />
    </div>


    <div class="container" style="margin-top: 30px; margin-bottom: 100px;">
        <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
            OnRowDataBound="dgvProductos_RowDataBound">
            <Columns>
                <asp:BoundField DataField="IdProductos" HeaderText="ID" SortExpression="IdProductos" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:TemplateField HeaderText="Precio Compra">
                    <ItemTemplate>
                        <asp:Label ID="lblPrecioCompra" runat="server" Text='<%# Eval("ProductosXProveedores[0].PrecioCompra") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Porcentaje Ganancia" SortExpression="PorcentajeGanancia" />
                <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" SortExpression="StockActual" />
                <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" SortExpression="StockMinimo" />
                <asp:TemplateField HeaderText="Seleccionar">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>


       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
         <div class="login-container">
             <asp:Button runat="server" ID="btnAgregarSeleccionados" Text="Agregar Seleccionados" CssClass="btn btn-primary" OnClick="btnAgregarSeleccionados_Click" Visible="false" />
         </div>
         <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red" Text=""></asp:Label>
         <asp:Label ID="lblMensajeAdvertencia" runat="server" ForeColor="Red" Visible="false"></asp:Label>
         <asp:GridView ID="dgvProductosSeleccionados" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" OnRowDataBound="dgvProductosSeleccionados_RowDataBound" OnRowCommand="dgvProductosSeleccionados_RowCommand">
             <Columns>
                 <asp:BoundField DataField="IdProducto" HeaderText="ID" SortExpression="IdProducto" />
                 <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" SortExpression="NombreProducto" />
                 <asp:BoundField DataField="PrecioVenta" HeaderText="Precio Venta" SortExpression="PrecioVenta" />
                 <asp:TemplateField HeaderText="Cantidad">
                     <ItemTemplate>
                         <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="0" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged" Enabled="true" />
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Subtotal">
                     <ItemTemplate>
                         <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Eliminar">
                     <ItemTemplate>
                         <asp:Button ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-danger btn-sm" ImageUrl="~/Images/delete_icon.png" Text="Eliminar" />
                     </ItemTemplate>
                 </asp:TemplateField>                
             </Columns>
         </asp:GridView>
         <div class="login-container">
             <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" OnClick="btnFinalizarCompra_Click" CssClass="btn btn-primary" Visible="false" />
         <div class="total-container">
             <asp:Label class="total-label" runat="server" Visible="false" ID="lblTotal">Total de la Venta:</asp:Label>
             <asp:Label ID="lblTotalVenta" Visible="false" runat="server" CssClass="font-weight-bold total-value"></asp:Label>                  
         </div>                   
          </div>
     </ContentTemplate>
 </asp:UpdatePanel>

</asp:Content>
