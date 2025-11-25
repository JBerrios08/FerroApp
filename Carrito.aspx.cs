using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using FerroApp.App_Code;

namespace FerroApp
{
    public partial class Carrito : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            var carrito = ObtenerCarrito();
            gvCarrito.DataSource = carrito.Items;
            gvCarrito.DataBind();
            lblTotal.Text = $"Total: {carrito.Total():C2}";
            lblMensaje.Text = string.Empty;
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var carrito = ObtenerCarrito();
            int idProducto = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Actualizar")
            {
                GridViewRow row = gvCarrito.Rows.Cast<GridViewRow>().First(r => Convert.ToInt32(gvCarrito.DataKeys[r.RowIndex].Value) == idProducto);
                TextBox txtCantidad = row.FindControl("txtCantidad") as TextBox;
                int cantidad = int.TryParse(txtCantidad.Text, out int c) ? c : 1;

                try
                {
                    var item = carrito.Items.First(i => i.IdProducto == idProducto);
                    carrito.ActualizarCantidad(idProducto, cantidad, item.StockDisponible);
                    Session["Carrito"] = carrito;
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Text = "Cantidad actualizada.";
                }
                catch (Exception ex)
                {
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Text = ex.Message;
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                carrito.EliminarItem(idProducto);
                Session["Carrito"] = carrito;
                lblMensaje.CssClass = "text-success";
                lblMensaje.Text = "Producto eliminado.";
            }

            CargarCarrito();
        }

        protected void btnVaciar_Click(object sender, EventArgs e)
        {
            ObtenerCarrito().VaciarCarrito();
            Session["Carrito"] = ObtenerCarrito();
            CargarCarrito();
        }

        protected void btnPagar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ticket.aspx");
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
