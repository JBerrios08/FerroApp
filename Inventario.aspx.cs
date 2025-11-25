using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using FerroApp.App_Code;
using MySql.Data.MySqlClient;

namespace FerroApp
{
    public partial class Inventario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private void CargarProductos()
        {
            using (var conexion = ConexionBD.ObtenerConexion())
            using (var cmd = ConexionBD.CrearComando("SELECT id AS IdProducto, nombre AS NombreProducto, precio AS Precio, stock AS StockDisponible, imagen_url AS Imagen FROM productos", conexion))
            using (var adapter = new MySqlDataAdapter(cmd))
            {
                var tabla = new DataTable();
                adapter.Fill(tabla);
                gvInventario.DataSource = tabla;
                gvInventario.DataBind();
            }
        }

        protected void gvInventario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvInventario.Rows.Cast<GridViewRow>().First(r => r.RowIndex == index);
                int idProducto = Convert.ToInt32(gvInventario.DataKeys[index].Values["IdProducto"]);
                string nombre = gvInventario.DataKeys[index].Values["NombreProducto"].ToString();
                decimal precio = Convert.ToDecimal(gvInventario.DataKeys[index].Values["Precio"]);
                int stock = Convert.ToInt32(gvInventario.DataKeys[index].Values["StockDisponible"]);
                string imagen = gvInventario.DataKeys[index].Values["Imagen"].ToString();

                TextBox txtCantidad = row.FindControl("txtCantidad") as TextBox;
                int cantidad = int.TryParse(txtCantidad.Text, out int c) ? c : 1;

                try
                {
                    var carrito = ObtenerCarrito();
                    carrito.AgregarItem(new ItemCarrito
                    {
                        IdProducto = idProducto,
                        NombreProducto = nombre,
                        Precio = precio,
                        Cantidad = cantidad,
                        Imagen = imagen,
                        StockDisponible = stock
                    });

                    Session["Carrito"] = carrito;
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Text = "Producto agregado al carrito.";
                }
                catch (Exception ex)
                {
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Text = ex.Message;
                }
            }
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
