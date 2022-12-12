using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

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

        public void GenerarPdfRemitoCompra(RemitoCompra x)
        {
            FileStream p = new FileStream(new Direcciones().ArchivoPdf + x.Emisor + ".pdf", FileMode.Create);
            Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
            PdfWriter pw = PdfWriter.GetInstance(doc, p);
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
            doc.Add(new Phrase("Proveedor: " + x.Emisor, StandarFont));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Phrase("Encargado: " + x.Receptor, StandarFont));
            doc.Add(Chunk.NEWLINE);


            //encabezado columnas

            PdfPTable tableEjemplo = new PdfPTable(4);
            tableEjemplo.WidthPercentage = 100;

            //configurando el titulo de las comunas
            PdfPCell clProducto = new PdfPCell(new Phrase("Producto", StandarFont));
            clProducto.BorderWidth = 0;
            clProducto.BorderWidthBottom = 0.75f;

            PdfPCell clCantidad = new PdfPCell(new Phrase("Cantidad", StandarFont));
            clCantidad.BorderWidth = 0;
            clCantidad.BorderWidthBottom = 0.75f;

            PdfPCell clPrecio = new PdfPCell(new Phrase("Costo", StandarFont));
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

            for (int i = 0; i < x.ListaProdutos.Count(); i++)
            {
                clProducto = new PdfPCell(new Phrase(x.ListaProdutos[i].Nombre, StandarFont));
                clProducto.BorderWidth = 0;

                clCantidad = new PdfPCell(new Phrase(x.ListaProdutos[i].Cantidad.ToString(), StandarFont));
                clCantidad.BorderWidth = 0;

                clPrecio = new PdfPCell(new Phrase(x.ListaProdutos[i].Costo.ToString(), StandarFont));
                clPrecio.BorderWidth = 0;

                clImporte = new PdfPCell(new Phrase(x.ListaProdutos[i].ImporteCosto().ToString(), StandarFont));
                clPrecio.BorderWidth = 0;

                tableEjemplo.AddCell(clProducto);
                tableEjemplo.AddCell(clCantidad);
                tableEjemplo.AddCell(clPrecio);
                tableEjemplo.AddCell(clImporte);

            }
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Phrase("Total: $" + x.TotalCosto(), StandarFont));

            doc.Add(tableEjemplo);
            doc.Close();
            pw.Close();

            MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        public void GenerarPdfProductos(DataGridView x)
        {
            FileStream p = new FileStream(new Direcciones().ArchivoPdf +"ListaProductos"+ ".pdf", FileMode.Create);
            Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
            PdfWriter pw = PdfWriter.GetInstance(doc, p);
            doc.Open();
            //titulo y autor
            doc.AddTitle("Productos");
            doc.AddAuthor("Setigex.sj@hotmail.com");
            // define tipo de fuente (tipo,tamaño,forma,color)

            iTextSharp.text.Font StandarFont = new iTextSharp.text.Font(
                iTextSharp.text.Font.FontFamily.HELVETICA,
                8,
                iTextSharp.text.Font.NORMAL,
                BaseColor.BLACK
                );

            //escribir encabezado


            doc.Add(new Phrase("Fecha: " + DateTime.Now.ToString("dd/MM/yy"), StandarFont)); ;
            doc.Add(Chunk.NEWLINE);


            //encabezado columnas

            PdfPTable tableEjemplo = new PdfPTable(9);
            tableEjemplo.WidthPercentage = 100;

            //configurando el titulo de las comunas
            PdfPCell codigo = new PdfPCell(new Phrase("Código", StandarFont));
            codigo.BorderWidth = 0;
            codigo.BorderWidthBottom = 0.75f;

            //configurando el titulo de las comunas
            PdfPCell producto = new PdfPCell(new Phrase("Producto", StandarFont));
            producto.BorderWidth = 0;
            producto.BorderWidthBottom = 0.75f;

            //configurando el titulo de las comunas
            PdfPCell descri = new PdfPCell(new Phrase("Costo", StandarFont));
            descri.BorderWidth = 0;
            descri.BorderWidthBottom = 0.75f;


            //configurando el titulo de las comunas
            PdfPCell bulto = new PdfPCell(new Phrase("Bulto", StandarFont));
            bulto.BorderWidth = 0;
            bulto.BorderWidthBottom = 0.75f;

            //configurando el titulo de las comunas
            PdfPCell cnt = new PdfPCell(new Phrase("Cantidad", StandarFont));
            cnt.BorderWidth = 0;
            cnt.BorderWidthBottom = 0.75f;



            //configurando el titulo de las comunas
            PdfPCell precio1 = new PdfPCell(new Phrase("Precio 1", StandarFont));
            precio1.BorderWidth = 0;
            precio1.BorderWidthBottom = 0.75f;

            PdfPCell precio2 = new PdfPCell(new Phrase("Precio 2", StandarFont));
            precio2.BorderWidth = 0;
            precio2.BorderWidthBottom = 0.75f;

            PdfPCell precio3  = new PdfPCell(new Phrase("Precio 3", StandarFont));
            precio3.BorderWidth = 0;
            precio3.BorderWidthBottom = 0.75f;


            PdfPCell importe = new PdfPCell(new Phrase("Importe", StandarFont));
            importe.BorderWidth = 0;
            importe.BorderWidthBottom = 0.75f;



            //añadir las columnas a la tabla

            tableEjemplo.AddCell(codigo);
            tableEjemplo.AddCell(producto);
            tableEjemplo.AddCell(descri);
            tableEjemplo.AddCell(bulto);
            tableEjemplo.AddCell(cnt);
            tableEjemplo.AddCell(precio1);
            tableEjemplo.AddCell(precio2);
            tableEjemplo.AddCell(precio3);
            tableEjemplo.AddCell(importe);
            //agregando datos

            for (int i = 0; i < x.RowCount; i++)
            {
                codigo = new PdfPCell(new Phrase("", StandarFont));
                codigo.BorderWidth = 0;
                codigo.BorderWidthBottom = 0.75f;
                producto = new PdfPCell(new Phrase(x.Rows[i].Cells[1].Value.ToString(), StandarFont));
                producto.BorderWidth = 0;
                producto.BorderWidthBottom = 0.75f;

                descri = new PdfPCell(new Phrase(x.Rows[i].Cells[6].Value.ToString(), StandarFont));
                descri.BorderWidth = 0;
                descri.BorderWidthBottom = 0.75f;

                bulto = new PdfPCell(new Phrase(x.Rows[i].Cells[3].Value.ToString(), StandarFont));
                bulto.BorderWidth = 0;
                bulto.BorderWidthBottom = 0.75f;

                cnt = new PdfPCell(new Phrase(x.Rows[i].Cells[4].Value.ToString(), StandarFont));
                cnt.BorderWidth = 0;
                cnt.BorderWidthBottom = 0.75f;

                precio1 = new PdfPCell(new Phrase(x.Rows[i].Cells[7].Value.ToString(), StandarFont));
                precio1.BorderWidth = 0;
                precio1.BorderWidthBottom = 0.75f;
                precio2 = new PdfPCell(new Phrase(x.Rows[i].Cells[8].Value.ToString(), StandarFont));
                precio2.BorderWidth = 0;
                precio2.BorderWidthBottom = 0.75f;
                precio3 = new PdfPCell(new Phrase(x.Rows[i].Cells[9].Value.ToString(), StandarFont));
                precio3.BorderWidth = 0;
                precio3.BorderWidthBottom = 0.75f;

                importe = new PdfPCell(new Phrase(x.Rows[i].Cells[10].Value.ToString(), StandarFont));
                importe.BorderWidth = 0;
                importe.BorderWidthBottom = 0.75f;

                tableEjemplo.AddCell(codigo);
                tableEjemplo.AddCell(producto);
                tableEjemplo.AddCell(descri);
                tableEjemplo.AddCell(bulto);
                tableEjemplo.AddCell(cnt);
                tableEjemplo.AddCell(precio1);
                tableEjemplo.AddCell(precio2);
                tableEjemplo.AddCell(precio3);
                tableEjemplo.AddCell(importe);
            }
            doc.Add(Chunk.NEWLINE);
            doc.Add(tableEjemplo);
            doc.Close();
            pw.Close();

            MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Process n = new Process();
            string file = new Direcciones().ArchivoPdf + "ListaProductos.pdf";
            n.StartInfo.FileName = file;
            n.Start();

        }

    }



}
