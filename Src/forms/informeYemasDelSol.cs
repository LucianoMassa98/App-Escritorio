using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace E_Shop
{
    public partial class informeYemasDelSol : Form
    {
        List<RemitoVenta> lventas;
        List<RemitoPagoProveedor> lpagos;
        List<RemitoCobroCliente> lcobros;
        List<RemitoCompra> lcompras;
        List<RemitoProduccion> lproducciones;
        List<RemitoRegistradora> lregistradoras;
        public informeYemasDelSol()
        {
            InitializeComponent();
        }
        private void informeYemasDelSol_Load(object sender, EventArgs e)
        {
            Direcciones dir = new Direcciones();
            panel3.BackgroundImage = System.Drawing.Image.FromFile(dir.Logo);
            
        }
        public void Filtrar(string desde, string hasta) { 
            lventas = RemitoVenta.BuscarPorFecha(desde, hasta);
            label65.Text = "Ventas: " + lventas.Count();
            lcompras = RemitoCompra.BuscarPorFecha(desde,hasta);
            label68.Text = "Compras: " + lcompras.Count();
            lregistradoras = RemitoRegistradora.BuscarPorFecha(desde,hasta);


            listBox1.Items.Clear();

            // estado cliente
            for (int i =0; i<lventas.Count();i++) {
                if (listBox2.Items.Contains(lventas[i].Receptor) == false) { 
                    listBox2.Items.Add(lventas[i].Receptor + "\t $"+ lventas[i].TotalVenta()); }
               
                
                if (lventas[i].Pagos[0].Codigo=="1.1.3") {

                    listBox1.Items.Add(
                        lventas[i].Receptor+" \t "+
                        "$"+lventas[i].Pagos[0].Importe);

                    
                }
            }


            //VENTAS
            List<Pago> lista  = RemitoVenta.Egreso(lventas);
            dataGridView3.Rows.Clear();
            for (int i =0; i<lista.Count();i++) {
                dataGridView3.Rows.Add(lista[i].Nombre,lista[i].Importe);
            }
            //Registradora
            double impRegistradora = RemitoRegistradora.Egreso(lregistradoras);
            dataGridView3.Rows.Add("Registradora", impRegistradora);



            //COMPRAS
            List<Pago> lista2 = RemitoCompra.Ingreso(lcompras);
            dataGridView4.Rows.Clear();
            for (int i = 0; i < lista2.Count(); i++)
            {
                dataGridView4.Rows.Add(lista2[i].Nombre, lista2[i].Importe);
            }



            //consolidado de ventas y registradora
            // RemitoVenta.ConsolidarMostrar(lventas, ref dataGridView1);
            RemitoCompra.ConsolidarMostrar(lcompras,ref dataGridView2);
            Producto.Consolidar(ref dataGridView1,lventas,lregistradoras);
        }
        public void BorrarTodo() {

            lventas = new List<RemitoVenta>();
            lcompras = new List<RemitoCompra>();
            lproducciones = new List<RemitoProduccion>();
            lregistradoras = new List<RemitoRegistradora>();
        
            RemitoVenta.Guardar(lventas);
            RemitoCompra.Guardar(lcompras);
            RemitoProduccion.Guardar(lproducciones);
            RemitoRegistradora.Guardar(lregistradoras);
        
        }
        // buscar remitos por fecha
        private void button1_Click(object sender, EventArgs e)
        {
            Filtrar(
                dateTimePicker1.Value.Date.ToShortDateString(),
                dateTimePicker2.Value.Date.ToShortDateString()
                );
        }
      // click borrar
        private void button4_Click(object sender, EventArgs e)
        {
            BorrarTodo();
        }
        // click imprimir
        private void button5_Click(object sender, EventArgs e)
        {
               if (dataGridView1.RowCount > 0)
               {
                   Direcciones dir = new Direcciones();
                   CrearTicket ticket = new CrearTicket(32);

                   ticket.AbreCajon();
                   ticket.TextoCentro("Informe "+new Direcciones().Space);
                   ticket.TextoIzquierda("FECHA: " + DateTime.Now.ToShortDateString());
                   ticket.TextoIzquierda("HORA: " + DateTime.Now.ToShortTimeString());
                   ticket.TextoIzquierda("");

                   ticket.lineasAsteriscos();
                   ticket.TextoCentro("Compras");
                ticket.TextoIzquierda("");

                for (int i =0; i<dataGridView4.RowCount; i++) {
                    ticket.TextoExtremos(dataGridView4.Rows[i].Cells[0].Value.ToString(), dataGridView4.Rows[i].Cells[1].Value.ToString()) ;
                }



                ticket.TextoIzquierda("");
                ticket.EncabezadoCompra();
                   for (int i =0; i<dataGridView2.RowCount;i++) {
                           ticket.AgregaArticulo(
                               dataGridView2.Rows[i].Cells[1].Value.ToString(),
                               double.Parse(dataGridView2.Rows[i].Cells[3].Value.ToString()),
                               double.Parse(dataGridView2.Rows[i].Cells[4].Value.ToString())
                               );
                   }
                   ticket.lineasIgual();
                ticket.TextoCentro("Ventas");
                ticket.TextoIzquierda("");
                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    ticket.TextoExtremos(dataGridView3.Rows[i].Cells[0].Value.ToString(), dataGridView3.Rows[i].Cells[1].Value.ToString());
                }
                ticket.TextoIzquierda("");
                ticket.EncabezadoVenta();
                  for (int i = 0; i < dataGridView1.RowCount; i++)
                   {
                           ticket.AgregaArticulo(
                               dataGridView1.Rows[i].Cells[1].Value.ToString(),
                               double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                               double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())
                               );
                   }
                ticket.TextoIzquierda("");

                   ticket.lineasBarra();
                
                // Detalle Cuenta Corriente
                if (dateTimePicker1.Value.Date.ToShortDateString()== dateTimePicker2.Value.Date.ToShortDateString())
                   {
                    lventas = RemitoVenta.BuscarPorFecha(
                    dateTimePicker1.Value.Date.ToShortDateString(),
                    dateTimePicker2.Value.Date.ToShortDateString());

                    ticket.lineasAsteriscos();
                    ticket.TextoIzquierda("Cuentas Corrientes");
                    ticket.TextoIzquierda("");
                       for (int i = 0; i < lventas.Count(); i++)
                       {

                           if (lventas[i].Pagos[0].Codigo == "1.1.3")
                           {
                               ticket.AgregaArticulo(lventas[i].Receptor,
                                   lventas[i].Pagos[0].Importe);

                               for (int j = 0; j < lventas[i].ListaProdutos.Count(); j++)
                               {
                                   ticket.TextoExtremos(lventas[i].ListaProdutos[j].Nombre, lventas[i].ListaProdutos[j].Cantidad.ToString());
                               }

                               ticket.lineasGuio();
                           }
                       }
                   }
                   ticket.TextoIzquierda("");
                   ticket.TextoIzquierda("");
                   ticket.TextoIzquierda("");
                   ticket.TextoIzquierda("");
                   ticket.TextoIzquierda("");
                   ticket.CortaTicket();
                   ticket.ImprimirTicket(dir.Impresora);//Nombre de la impresora tick
                   MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);


               }
        }


        private void button2_Click(object sender, EventArgs e)
        {


            GenerarPdfInforme();


        }

        public void pdfInformeVenta(ref Document doc, iTextSharp.text.Font StandarFont)
        {
           

            // tabla de ventas
            doc.Add(new Paragraph("VENTAS"));


           
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                doc.Add(new Phrase(dataGridView4.Rows[i].Cells[0].Value.ToString()+": "+
                    dataGridView4.Rows[i].Cells[1].Value.ToString(), StandarFont));
            }
            PdfPTable tableVentas = new PdfPTable(3);
            tableVentas.WidthPercentage = 80;

            PdfPCell art = new PdfPCell(new Phrase("Articulo", StandarFont));
            art.BorderWidth = 0;
            art.BorderWidthBottom = 0.75f;

            PdfPCell cnt = new PdfPCell(new Phrase("Cantidad", StandarFont));
            cnt.BorderWidth = 0;
            cnt.BorderWidthBottom = 0.75f;

            PdfPCell imp = new PdfPCell(new Phrase("Importe", StandarFont));
            imp.BorderWidth = 0;
            imp.BorderWidthBottom = 0.75f;

            tableVentas.AddCell(art);
            tableVentas.AddCell(cnt);
            tableVentas.AddCell(imp);

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                art = new PdfPCell(new Phrase(dataGridView1.Rows[i].Cells[1].Value.ToString(), StandarFont));
                art.BorderWidth = 0;
                cnt = new PdfPCell(new Phrase(dataGridView1.Rows[i].Cells[3].Value.ToString(), StandarFont));
                cnt.BorderWidth = 0;
                imp = new PdfPCell(new Phrase("$" + dataGridView1.Rows[i].Cells[4].Value.ToString(), StandarFont));
                imp.BorderWidth = 0;
                tableVentas.AddCell(art);
                tableVentas.AddCell(cnt);
                tableVentas.AddCell(imp);

            }
            doc.Add(tableVentas);

        }
        public void pdfInformeCompra(ref Document doc, iTextSharp.text.Font StandarFont)
        {

            doc.Add(new Paragraph("COMPRAS"));

            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                doc.Add(new Phrase(dataGridView3.Rows[i].Cells[0].Value.ToString() + ": " +
                    dataGridView3.Rows[i].Cells[1].Value.ToString(), StandarFont));
            }

            PdfPTable tableCompras= new PdfPTable(3);
            tableCompras.WidthPercentage = 80;

            PdfPCell articulo = new PdfPCell(new Phrase("Articulo", StandarFont));
            articulo.BorderWidth = 0;
            articulo.BorderWidthBottom = 0.75f;

            PdfPCell cantidad = new PdfPCell(new Phrase("Cantidad", StandarFont));
            cantidad.BorderWidth = 0;
            cantidad.BorderWidthBottom = 0.75f;

            PdfPCell importe = new PdfPCell(new Phrase("Importe", StandarFont));
            importe.BorderWidth = 0;
            importe.BorderWidthBottom = 0.75f;

            tableCompras.AddCell(articulo);
            tableCompras.AddCell(cantidad);
            tableCompras.AddCell(importe);



            for (int i = 0; i < dataGridView2.RowCount; i++)
            {

                articulo = new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells[1].Value.ToString(), StandarFont));
                articulo.BorderWidth = 0;
                cantidad = new PdfPCell(new Phrase(dataGridView2.Rows[i].Cells[3].Value.ToString(), StandarFont));
                cantidad.BorderWidth = 0;
                importe = new PdfPCell(new Phrase("$" + dataGridView2.Rows[i].Cells[4].Value.ToString(), StandarFont));
                importe.BorderWidth = 0;
                tableCompras.AddCell(articulo);
                tableCompras.AddCell(cantidad);
                tableCompras.AddCell(importe);

            }
            doc.Add(tableCompras);
        }
    
        public void pdfCuentaCorriente(ref Document doc, iTextSharp.text.Font StandarFont)
        {
            lventas = RemitoVenta.BuscarPorFecha(
                 dateTimePicker1.Value.Date.ToShortDateString(),
                dateTimePicker2.Value.Date.ToShortDateString());

            doc.Add(new Paragraph("INFORME CUENTA CORRIENTE"));

            PdfPTable tableCliente = new PdfPTable(2);
            tableCliente.WidthPercentage = 70;

            PdfPCell cliente = new PdfPCell(new Phrase("Cliente", StandarFont));
            cliente.BorderWidth = 0;
            cliente.BorderWidthBottom = 0.75f;

            PdfPCell importe = new PdfPCell(new Phrase("Importe", StandarFont));
            importe.BorderWidth = 0;
            importe.BorderWidthBottom = 0.75f;

            tableCliente.AddCell(cliente);
            tableCliente.AddCell(importe);

            for (int i =0; i< lventas.Count(); i++) {

                if (lventas[i].Pagos[0].Codigo == "1.1.3")
                {
                    cliente = new PdfPCell(new Phrase(lventas[i].Receptor, StandarFont));
                    cliente.BorderWidth = 0;

                    importe = new PdfPCell(new Phrase("$" + lventas[i].Pagos[0].Importe, StandarFont));
                    importe.BorderWidth = 0;

                    tableCliente.AddCell(cliente);
                    tableCliente.AddCell(importe);
                }
          
            }

            doc.Add(tableCliente);


        }
        public void pdfProveedores(ref Document doc, iTextSharp.text.Font StandarFont)
        {
            lcompras = RemitoCompra.BuscarPorFecha(
                 dateTimePicker1.Value.Date.ToShortDateString(),
                dateTimePicker2.Value.Date.ToShortDateString());

            doc.Add(new Paragraph("INFORME PROVEEDORES"));

            PdfPTable tableProveedor= new PdfPTable(2);
            tableProveedor.WidthPercentage = 70;

            PdfPCell proveedor = new PdfPCell(new Phrase("Proveedor", StandarFont));
            proveedor.BorderWidth = 0;
            proveedor.BorderWidthBottom = 0.75f;

            PdfPCell importe = new PdfPCell(new Phrase("Importe", StandarFont));
            importe.BorderWidth = 0;
            importe.BorderWidthBottom = 0.75f;

            tableProveedor.AddCell(proveedor);
            tableProveedor.AddCell(importe);

            for (int i = 0; i < lcompras.Count(); i++)
            {

                if (lcompras[i].Pagos[0].Codigo == "2.1.1")
                {
                    proveedor = new PdfPCell(new Phrase(lcompras[i].Emisor, StandarFont));
                    proveedor.BorderWidth = 0;

                    importe = new PdfPCell(new Phrase("$" + lcompras[i].Pagos[0].Importe, StandarFont));
                    importe.BorderWidth = 0;

                    tableProveedor.AddCell(proveedor);
                    tableProveedor.AddCell(importe);
                }

            }

            doc.Add(tableProveedor);


        }
        public void pdfPagos(ref Document doc, iTextSharp.text.Font StandarFont)
        {
            lpagos = RemitoPagoProveedor.BuscarPorFecha(
                 dateTimePicker1.Value.Date.ToShortDateString(),
                dateTimePicker2.Value.Date.ToShortDateString());

            doc.Add(new Paragraph("INFORME PAGOS"));

            PdfPTable tableProveedor = new PdfPTable(3);
            tableProveedor.WidthPercentage = 70;

            PdfPCell proveedor = new PdfPCell(new Phrase("Proveedor", StandarFont));
            proveedor.BorderWidth = 0;
            proveedor.BorderWidthBottom = 0.75f;
            PdfPCell medio = new PdfPCell(new Phrase("Medio", StandarFont));
            medio.BorderWidth = 0;
            medio.BorderWidthBottom = 0.75f;

            PdfPCell importe = new PdfPCell(new Phrase("Importe", StandarFont));
            importe.BorderWidth = 0;
            importe.BorderWidthBottom = 0.75f;

            tableProveedor.AddCell(proveedor);
            tableProveedor.AddCell(medio);
            tableProveedor.AddCell(importe);

            for (int i = 0; i < lpagos.Count(); i++)
            {

                
                    proveedor = new PdfPCell(new Phrase(lpagos[i].Emisor, StandarFont));
                    proveedor.BorderWidth = 0;
                    medio = new PdfPCell(new Phrase(lpagos[i].Pagos[0].Nombre, StandarFont));
                    medio.BorderWidth = 0;
                importe = new PdfPCell(new Phrase("$" + lpagos[i].Pagos[0].Importe, StandarFont));
                    importe.BorderWidth = 0;

                    tableProveedor.AddCell(proveedor);
                tableProveedor.AddCell(medio);
                tableProveedor.AddCell(importe);
                

            }

            doc.Add(tableProveedor);


        }
        public void pdfCobros(ref Document doc, iTextSharp.text.Font StandarFont)
        {
            lcobros = RemitoCobroCliente.BuscarPorFecha(
                 dateTimePicker1.Value.Date.ToShortDateString(),
                dateTimePicker2.Value.Date.ToShortDateString());

            doc.Add(new Paragraph("INFORME COBROS"));

            PdfPTable tableCliente= new PdfPTable(3);
            tableCliente.WidthPercentage = 70;

            PdfPCell cliente = new PdfPCell(new Phrase("Cliente", StandarFont));
            cliente.BorderWidth = 0;
            cliente.BorderWidthBottom = 0.75f;
            PdfPCell medio = new PdfPCell(new Phrase("Medio", StandarFont));
            medio.BorderWidth = 0;
            medio.BorderWidthBottom = 0.75f;

            PdfPCell importe = new PdfPCell(new Phrase("Importe", StandarFont));
            importe.BorderWidth = 0;
            importe.BorderWidthBottom = 0.75f;

            tableCliente.AddCell(cliente);
            tableCliente.AddCell(medio);
            tableCliente.AddCell(importe);

            for (int i = 0; i < lcobros.Count(); i++)
            {

                
                    cliente = new PdfPCell(new Phrase(lcobros[i].Emisor, StandarFont));
                    cliente.BorderWidth = 0;
                medio = new PdfPCell(new Phrase(lcobros[i].Pagos[0].Nombre, StandarFont));
                medio.BorderWidth = 0;

                importe = new PdfPCell(new Phrase("$" + lcobros[i].Pagos[0].Importe, StandarFont));
                    importe.BorderWidth = 0;

                    tableCliente.AddCell(cliente);
                tableCliente.AddCell(medio);
                tableCliente.AddCell(importe);
                

            }

            doc.Add(tableCliente);


        }
        public void GenerarPdfInforme() {

            string FechaActual = DateTime.Now.Date.ToLongDateString();
            FileStream p = new FileStream(new Direcciones().ArchivoPdf + "Informe -" + FechaActual + ".pdf", FileMode.Create);
            Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
            PdfWriter pw = PdfWriter.GetInstance(doc, p);
            doc.Open();
            //titulo y autor
            doc.AddTitle("Informe");
            doc.AddAuthor("lgmassa98@gmail.com");
            // define tipo de fuente (tipo,tamaño,forma,color)

            iTextSharp.text.Font StandarFont = new iTextSharp.text.Font(
                iTextSharp.text.Font.FontFamily.HELVETICA,
                8,
                iTextSharp.text.Font.NORMAL,
                BaseColor.BLACK
                );

            //escribir encabezado

            doc.Add(new Paragraph("Informe General"));
            doc.Add(new Phrase("\nFecha emision: " + FechaActual, StandarFont));
            doc.Add(new Phrase("\nHora  emision: " + DateTime.Now.ToShortTimeString(), StandarFont));
            doc.Add(new Phrase("\nPeriodo desde: " + dateTimePicker1.Value.ToShortDateString(), StandarFont));
            doc.Add(new Phrase("\nPeriodo Hasta: " + dateTimePicker2.Value.ToShortDateString(), StandarFont));
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            pdfInformeCompra(ref doc, StandarFont);
            doc.Add(Chunk.NEWLINE);
            pdfInformeVenta(ref doc, StandarFont);
            doc.Add(Chunk.NEWLINE);
            pdfCuentaCorriente(ref doc, StandarFont);
            doc.Add(Chunk.NEWLINE);
            pdfCobros(ref doc, StandarFont);
            doc.Add(Chunk.NEWLINE);
            pdfProveedores(ref doc, StandarFont);
            doc.Add(Chunk.NEWLINE);
            pdfPagos(ref doc, StandarFont);
            doc.Close();
            pw.Close();

            MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process n = new Process();
            string file = new Direcciones().ArchivoPdf + "Informe -" + FechaActual + ".pdf";
            n.StartInfo.FileName = file;
            n.Start();
            
        }
    }
}
