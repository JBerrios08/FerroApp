<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="FerroApp.Ticket" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ticket de Compra - Ferro Oriente</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container py-4">
        <h2 class="mb-4">Ferro Oriente - Ticket de Compra</h2>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
        <div class="mb-3">
            <asp:Label ID="lblFecha" runat="server" CssClass="d-block"></asp:Label>
            <asp:Label ID="lblNumeroCompra" runat="server" CssClass="d-block"></asp:Label>
            <asp:Label ID="lblUsuario" runat="server" CssClass="d-block"></asp:Label>
        </div>
        <asp:GridView ID="gvTicket" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C2}" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblTotal" runat="server" CssClass="h5"></asp:Label>
        <div class="mt-3">
            <asp:Button ID="btnGenerar" runat="server" Text="Generar Ticket PDF y Enviar" CssClass="btn btn-primary" OnClick="btnGenerar_Click" />
            <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="~/Carrito.aspx" CssClass="btn btn-link">Volver al carrito</asp:HyperLink>
        </div>
    </form>
</body>
</html>
