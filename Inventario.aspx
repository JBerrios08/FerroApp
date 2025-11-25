<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="FerroApp.Inventario" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventario - Ferro Oriente</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container py-4">
        <h2 class="mb-4">Inventario de Productos</h2>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
        <asp:GridView ID="gvInventario" runat="server" CssClass="table table-striped" AutoGenerateColumns="False"
            DataKeyNames="IdProducto,NombreProducto,Precio,StockDisponible,Imagen" OnRowCommand="gvInventario_RowCommand">
            <Columns>
                <asp:BoundField DataField="IdProducto" HeaderText="ID" />
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="StockDisponible" HeaderText="Stock" />
                <asp:ImageField DataImageUrlField="Imagen" HeaderText="Imagen" ControlStyle-Width="80" ControlStyle-Height="80" />
                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCantidad" runat="server" Text="1" CssClass="form-control" Width="80"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnAgregar" runat="server" CommandName="Agregar" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' CssClass="btn btn-primary">Agregar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:HyperLink ID="lnkVerCarrito" runat="server" NavigateUrl="~/Carrito.aspx" CssClass="btn btn-success">Ver Carrito</asp:HyperLink>
    </form>
</body>
</html>
