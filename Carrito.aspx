<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="FerroApp.Carrito" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Carrito de Compras - Ferro Oriente</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container py-4">
        <h2 class="mb-4">Carrito de Compras</h2>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
        <asp:GridView ID="gvCarrito" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" DataKeyNames="IdProducto" OnRowCommand="gvCarrito_RowCommand">
            <Columns>
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C2}" />
                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' CssClass="form-control" Width="80"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C2}" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnActualizar" runat="server" CommandName="Actualizar" CommandArgument='<%# Eval("IdProducto") %>' CssClass="btn btn-outline-primary btn-sm">Actualizar</asp:LinkButton>
                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("IdProducto") %>' CssClass="btn btn-outline-danger btn-sm">Eliminar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <asp:Label ID="lblTotal" runat="server" CssClass="h5"></asp:Label>
            <div>
                <asp:Button ID="btnVaciar" runat="server" Text="Vaciar Carrito" CssClass="btn btn-secondary" OnClick="btnVaciar_Click" />
                <asp:Button ID="btnPagar" runat="server" Text="Proceder al Pago" CssClass="btn btn-success" OnClick="btnPagar_Click" />
            </div>
        </div>
        <asp:HyperLink ID="lnkSeguir" runat="server" NavigateUrl="~/Inventario.aspx">Seguir comprando</asp:HyperLink>
    </form>
</body>
</html>
