using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    internal class CrearPdf
    {
        public CrearPdf() { }
        public void GenerarPdfRemitoVenta(RemitoVenta x)
        {
            FileStream p = new FileStream(new Direcciones().ArchivoPdf+x.Receptor+".pdf", FileMode.Create);
            Document doc = new Document(PageSize.LETTER, 5,5,7,7);
            PdfWriter pw = PdfWriter.GetInstance(doc,p);
            doc.Open();
            //titulo y autor
            doc.AddTitle("RemitoX");
            doc.AddAuthor("Setigex.sj@hotmail.com");
            // define tipo de fuente (tipo,tamaño,forma,color)
            
            iTextSharp.text.Font StandarFont = new iTextSharp.text.Font(
                iTextSharp.text.Font.FontFamily.HELVETICA,
                8,
                iTextSharp.text.Font.NORMAL,
                BaseColor.BLACK
                );
            
            //escribir encabezado

            doc.Add(new Paragraph("Documento [x] No válido como factura"));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Phrase("Fecha: " + x.FechaEmision, StandarFont));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Phrase("Vendedor: "+x.Emisor,StandarFont));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Phrase("Cliente: " + x.Receptor, StandarFont));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Phrase("Saldo: " + Cliente.BuscarPorNombre(x.Receptor).Saldo, StandarFont));
            doc.Add(Chunk.NEWLINE);

            
            //encabezado columnas

            PdfPTable tableEjemplo = new PdfPTable(4);
            tableEjemplo.WidthPercentage = 100;

            //configurando el titulo de las comunas
            PdfPCell clProducto= new PdfPCell(new Phrase("Producto",StandarFont));
            clProducto.BorderWidth = 0;
            clProducto.BorderWidthBottom = 0.75f;

            PdfPCell clCantidad = new PdfPCell(new Phrase("Cantidad", StandarFont));
            clCantidad.BorderWidth = 0;
            clCantidad.BorderWidthBottom = 0.75f;

            PdfPCell clPrecio = new PdfPCell(new Phrase("Precio", StandarFont));
            clPrecio.BorderWidth = 0;
            clPrecio.BorderWidthBottom = 0.75f;

            PdfPCell clImporte = new PdfPCell(new Phrase("Importe", StandarFont));
            clImporte.BorderWidth = 0;
            clImporte.BorderWidthBottom = 0.75f;

            //añadir las columnas a la tabla

            tableEjemplo.AddCell(clProducto);
            tableEjemplo.AddCell(clCantidad);
            tableEjemplo.AddCell(clPrecio);
            tableEjemplo.AddCell(clImporte);
            //agregando datos

            for (int i =0; i<x.ListaProdutos.Count();i++) {
                clProducto = new PdfPCell(new Phrase(x.ListaProdutos[i].Nombre,StandarFont));
                clProducto.BorderWidth = 0;

                clCantidad = new PdfPCell(new Phrase(x.ListaProdutos[i].Cantidad.ToString(), StandarFont));
                clCantidad.BorderWidth = 0;

                clPrecio = new PdfPCell(new Phrase(x.ListaProdutos[i].Precio.ToString(), StandarFont));
                clPrecio.BorderWidth = 0;

                clImporte = new PdfPCell(new Phrase(x.ListaProdutos[i].ImportePrecio().ToString(), StandarFont));
                clPrecio.BorderWidth = 0;

                tableEjemplo.AddCell(clProducto);
                tableEjemplo.AddCell(clCantidad);
                tableEjemplo.AddCell(clPrecio);
                tableEjemplo.AddCell(clImporte);

            }
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Phrase("Total: $"+x.TotalVenta(),StandarFont));

            doc.Add(tableEjemplo);
            doc.Close();
            pw.Close();

            MessageBox.Show("Documento generado existosamente","Exito",MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }

    

}
