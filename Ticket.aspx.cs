using System;
using System.Linq;
using System.Web.UI;
using FerroApp.App_Code;
using MySql.Data.MySqlClient;

namespace FerroApp
{
    public partial class Ticket : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTicket();
            }
        }

        private void CargarTicket()
        {
            var carrito = ObtenerCarrito();
            gvTicket.DataSource = carrito.Items;
            gvTicket.DataBind();
            lblFecha.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}";
            lblNumeroCompra.Text = $"N° Compra: {ObtenerNumeroCompra()}";
            lblUsuario.Text = $"Usuario: {ObtenerUsuarioActual()}";
            lblTotal.Text = $"Total: {carrito.Total():C2}";
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            var carrito = ObtenerCarrito();
            if (!carrito.Items.Any())
            {
                lblMensaje.Text = "El carrito está vacío.";
                return;
            }

            try
            {
                int idVenta;
                using (var conexion = ConexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    using (var transaccion = conexion.BeginTransaction())
                    {
                        using (var cmdVenta = new MySqlCommand("INSERT INTO ventas (fecha, usuario, total) VALUES (@fecha, @usuario, @total); SELECT LAST_INSERT_ID();", conexion, transaccion))
                        {
                            cmdVenta.Parameters.AddWithValue("@fecha", DateTime.Now);
                            cmdVenta.Parameters.AddWithValue("@usuario", ObtenerUsuarioActual());
                            cmdVenta.Parameters.AddWithValue("@total", carrito.Total());
                            idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());
                        }

                        foreach (var item in carrito.Items)
                        {
                            using (var cmdDetalle = new MySqlCommand("INSERT INTO detalle_venta(id_venta, id_producto, cantidad, precio_unitario, subtotal) VALUES(@venta, @producto, @cantidad, @precio, @subtotal)", conexion, transaccion))
                            {
                                cmdDetalle.Parameters.AddWithValue("@venta", idVenta);
                                cmdDetalle.Parameters.AddWithValue("@producto", item.IdProducto);
                                cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                                cmdDetalle.Parameters.AddWithValue("@precio", item.Precio);
                                cmdDetalle.Parameters.AddWithValue("@subtotal", item.Subtotal);
                                cmdDetalle.ExecuteNonQuery();
                            }

                            using (var cmdStock = new MySqlCommand("UPDATE productos SET stock = stock - @cantidad WHERE id = @id", conexion, transaccion))
                            {
                                cmdStock.Parameters.AddWithValue("@cantidad", item.Cantidad);
                                cmdStock.Parameters.AddWithValue("@id", item.IdProducto);
                                cmdStock.ExecuteNonQuery();
                            }
                        }

                        transaccion.Commit();
                    }
                }

                string numeroCompra = ObtenerNumeroCompra();
                string rutaPdf = PdfHelper.GenerarTicketPdf(numeroCompra, ObtenerUsuarioActual(), carrito.Items, carrito.Total());
                CorreoHelper.EnviarTicket(ObtenerCorreoCliente(), rutaPdf);

                carrito.VaciarCarrito();
                Session["Carrito"] = carrito;
                CargarTicket();
                lblMensaje.CssClass = "text-success";
                lblMensaje.Text = "Compra confirmada, ticket enviado.";
            }
            catch (Exception ex)
            {
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Text = ex.Message;
            }
        }

        private string ObtenerUsuarioActual()
        {
            return Session["Usuario"] != null ? Session["Usuario"].ToString() : "Invitado";
        }

        private string ObtenerCorreoCliente()
        {
            return Session["Correo"] != null ? Session["Correo"].ToString() : "cliente@correo.com";
        }

        private string ObtenerNumeroCompra()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private CarritoCompras ObtenerCarrito()
        {
            if (Session["Carrito"] == null)
            {
                Session["Carrito"] = new CarritoCompras();
            }

            return (CarritoCompras)Session["Carrito"];
        }
    }
}
