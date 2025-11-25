using System;

namespace FerroApp.App_Code
{
    public class ItemCarrito
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }
        public int StockDisponible { get; set; }

        public decimal Subtotal
        {
            get { return Precio * Cantidad; }
        }
    }
}
