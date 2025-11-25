using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FerroApp.App_Code
{
    public static class PdfHelper
    {
        public static string GenerarTicketPdf(string numeroCompra, string usuario, System.Collections.Generic.IEnumerable<ItemCarrito> items, decimal total)
        {
            var carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tickets");
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            var ruta = Path.Combine(carpeta, $"Ticket_{numeroCompra}.pdf");

            using (var fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var doc = new Document(PageSize.A4, 40, 40, 40, 40))
            {
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                var titulo = new Paragraph("Ferro Oriente - Ticket de Compra", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(titulo);
                doc.Add(new Paragraph($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}") { SpacingBefore = 10 });
                doc.Add(new Paragraph($"N° Compra: {numeroCompra}"));
                doc.Add(new Paragraph($"Cliente: {usuario}") { SpacingAfter = 10 });

                var tabla = new PdfPTable(4) { WidthPercentage = 100 };
                tabla.SetWidths(new float[] { 40, 20, 20, 20 });
                tabla.AddCell("Producto");
                tabla.AddCell("Precio");
                tabla.AddCell("Cantidad");
                tabla.AddCell("Subtotal");

                foreach (var item in items)
                {
                    tabla.AddCell(item.NombreProducto);
                    tabla.AddCell(item.Precio.ToString("C2"));
                    tabla.AddCell(item.Cantidad.ToString());
                    tabla.AddCell(item.Subtotal.ToString("C2"));
                }

                var celdaTotal = new PdfPCell(new Phrase($"Total: {total:C2}"))
                {
                    Colspan = 4,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    PaddingTop = 5,
                    Border = Rectangle.TOP_BORDER
                };
                tabla.AddCell(celdaTotal);

                doc.Add(tabla);
                doc.Close();
            }

            return ruta;
        }
    }
}
