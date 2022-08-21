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
            double[] res = RemitoVenta.Egreso(lventas);
            //efectivo
            label89.Text = "$"+res[3].ToString();
            //mercado pago
            label72.Text = "$"+res[5].ToString();
            //credito
            label69.Text = "$" + res[7].ToString();
            //bonificado
            label66.Text = "$" + res[1].ToString();
            //Registradora
            label60.Text = "$" + lregistradoras;
            //total
            label63.Text = "$" + (res[7] + res[3] + res[5]).ToString();

            //COMPRAS
            double[] res2 = RemitoCompra.Ingreso(lcompras);

            label17.Text = "$" + res2[3].ToString();

            label14.Text = "$" + res2[5].ToString();

            label12.Text = "$" + res2[7].ToString();

            label10.Text = "$" + res2[1].ToString();

            label7.Text = "$" + (res2[7]+ res2[3] + res2[5]).ToString();


            //consolidado de ventas y registradora
             RemitoVenta.ConsolidarMostrar(lventas, ref dataGridView1);
            RemitoCompra.ConsolidarMostrar(lcompras,ref dataGridView2);
            //Producto.Consolidar(ref dataGridView1,lventas,lregistradoras);
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
                ticket.TextoExtremos("Efectivo:", label17.Text);
                ticket.TextoExtremos("Mercado Pago:", label14.Text);
                ticket.TextoExtremos("Credito:", label12.Text);
                ticket.TextoExtremos("Bonificado:", label10.Text);
                ticket.TextoExtremos("Total:", label7.Text);
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
                ticket.TextoExtremos("Efectivo:", label89.Text);
                ticket.TextoExtremos("Mercado Pago:", label72.Text);
                ticket.TextoExtremos("Credito:", label69.Text);
                ticket.TextoExtremos("Bonificado:", label66.Text);
                ticket.TextoExtremos("Registradora:", label60.Text);
                ticket.TextoExtremos("Total:", label63.Text);
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
            doc.Add(new Phrase("Efectivo: " + label89.Text, StandarFont));
            doc.Add(new Phrase("\nMercado Pago: " + label72.Text, StandarFont));
            doc.Add(new Phrase("\nCredito: " + label69.Text, StandarFont));
            doc.Add(new Phrase("\nBonificado: " + label66.Text, StandarFont));
            doc.Add(new Phrase("\nRegistradora: " + label60.Text, StandarFont));
            doc.Add(new Phrase("\nTotal: " + label63.Text, StandarFont));
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
            doc.Add(new Phrase("Efectivo: " + label17.Text, StandarFont));
            doc.Add(new Phrase("\nMercado Pago: " + label14.Text, StandarFont));
            doc.Add(new Phrase("\nCredito: " + label10.Text, StandarFont));
            doc.Add(new Phrase("\nBonificado: " + label7.Text, StandarFont));
            doc.Add(new Phrase("\nTotal: " + label7.Text, StandarFont));

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
