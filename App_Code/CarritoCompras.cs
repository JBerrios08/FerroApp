using System;
using System.Collections.Generic;
using System.Linq;

namespace FerroApp.App_Code
{
    public class CarritoCompras
    {
        private readonly List<ItemCarrito> _items;

        public CarritoCompras()
        {
            _items = new List<ItemCarrito>();
        }

        public IReadOnlyCollection<ItemCarrito> Items => _items.AsReadOnly();

        public void AgregarItem(ItemCarrito nuevo)
        {
            var existente = _items.FirstOrDefault(x => x.IdProducto == nuevo.IdProducto);
            if (existente == null)
            {
                if (nuevo.Cantidad > nuevo.StockDisponible)
                {
                    throw new InvalidOperationException("Cantidad supera stock disponible");
                }
                _items.Add(nuevo);
            }
            else
            {
                var nuevaCantidad = existente.Cantidad + nuevo.Cantidad;
                if (nuevaCantidad > nuevo.StockDisponible)
                {
                    throw new InvalidOperationException("Cantidad supera stock disponible");
                }
                existente.Cantidad = nuevaCantidad;
                existente.Precio = nuevo.Precio;
                existente.NombreProducto = nuevo.NombreProducto;
                existente.Imagen = nuevo.Imagen;
                existente.StockDisponible = nuevo.StockDisponible;
            }
        }

        public void ActualizarCantidad(int idProducto, int cantidad, int stockDisponible)
        {
            var item = _items.FirstOrDefault(x => x.IdProducto == idProducto);
            if (item == null)
            {
                return;
            }

            if (cantidad < 1)
            {
                throw new InvalidOperationException("La cantidad debe ser mayor a cero");
            }

            if (cantidad > stockDisponible)
            {
                throw new InvalidOperationException("Cantidad supera stock disponible");
            }

            item.Cantidad = cantidad;
            item.StockDisponible = stockDisponible;
        }

        public void EliminarItem(int idProducto)
        {
            var item = _items.FirstOrDefault(x => x.IdProducto == idProducto);
            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public void VaciarCarrito()
        {
            _items.Clear();
        }

        public decimal Total()
        {
            return _items.Sum(x => x.Subtotal);
        }
    }
}
