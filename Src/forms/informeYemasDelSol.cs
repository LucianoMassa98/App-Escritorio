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
            lproducciones = RemitoProduccion.BuscarPorFecha(desde, hasta);
            label71.Text = "Producciones: " + lproducciones.Count();
            lregistradoras = RemitoRegistradora.BuscarPorFecha(desde, hasta);
            label74.Text = "Registradora: " + lregistradoras.Count();
            listBox1.Items.Clear();
            // estado cliente
            for (int i =0; i<lventas.Count();i++) {
                if (listBox2.Items.Contains(lventas[i].Receptor) == false) { 
                    listBox2.Items.Add(lventas[i].Receptor + "\t $"+ lventas[i].TotalVenta()); }
                if (lventas[i].Pagos[0].Codigo=="1.1.3") {

                    listBox1.Items.Add(
                        lventas[i].Receptor+" \t "+
                        lventas[i].CantidadHuevos() + " -" +
                        lventas[i].CantidadMercaderia() + " - " +
                        "$"+lventas[i].Pagos[0].Importe);

                    
                }
            }

            // infrome huevos
            //ingreso
            RemitoProduccion.Ingreso(lproducciones,ref label12,ref label13);
            //egreso
            double[] res = RemitoVenta.Egreso(lventas,"Huevo");
            label29.Text = res[0].ToString(); 
            label27.Text = "$"+res[1].ToString();

            label20.Text = res[2].ToString();
            label18.Text = "$"+res[3].ToString();

            label23.Text = res[4].ToString();
            label21.Text = "$" + res[5].ToString();

            label26.Text = res[6].ToString();
            label24.Text = "$" + res[7].ToString();

            RemitoRegistradora.Egreso(lregistradoras,ref label88,ref label86,"Huevo");

            label32.Text = (res[0] + res[2] + res[4] + res[6]+double.Parse(label88.Text)).ToString();
            label30.Text = "$"+(res[1] + res[3] + res[5] + res[7]+double.Parse(label86.Text.Split('$')[1])).ToString();
            //informe mercaderia
            //ingreso
            RemitoCompra.Ingreso(lcompras, ref label59, ref label56);
            //egreso
             res = RemitoVenta.Egreso(lventas, "Mercaderia");
            label39.Text = res[0].ToString();
            label37.Text = "$" + res[1].ToString();

            label48.Text = res[2].ToString();
            label46.Text = "$" + res[3].ToString();

            label45.Text = res[4].ToString();
            label43.Text = "$" + res[5].ToString();

            label42.Text = res[6].ToString();
            label40.Text = "$" + res[7].ToString();

            RemitoRegistradora.Egreso(lregistradoras, ref label85, ref label76, "Mercaderia");
            label36.Text = (res[0] + res[2] + res[4] + res[6] + double.Parse(label85.Text)).ToString();
            label34.Text = "$" + (res[1] + res[3] + res[5] + res[7] + double.Parse(label76.Text.Split('$')[1])).ToString();

            //-----TOTAL------

            label82.Text = label13.Text;
            label92.Text = label56.Text;


            label89.Text = "$"+(double.Parse(label18.Text.Split('$')[1]) + double.Parse(label46.Text.Split('$')[1])).ToString();
            label72.Text = "$" + (double.Parse(label21.Text.Split('$')[1]) + double.Parse(label43.Text.Split('$')[1])).ToString();
            label69.Text = "$" + (double.Parse(label24.Text.Split('$')[1]) + double.Parse(label40.Text.Split('$')[1])).ToString();
            label60.Text = "$" + (double.Parse(label86.Text.Split('$')[1]) + double.Parse(label76.Text.Split('$')[1])).ToString();
            label63.Text = "$" + (double.Parse(label30.Text.Split('$')[1]) + double.Parse(label34.Text.Split('$')[1])).ToString();



            //consolidado de ventas y registradora
            //RemitoVenta.ConsolidarMostrar(lventas, ref dataGridView1, ref listBox1);

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
                ticket.TextoCentro("Ventas");
                ticket.lineasAsteriscos();
                ticket.EncabezadoHuevos();
                ticket.TextoIzquierda("");
                for (int i =0; i<dataGridView1.RowCount;i++) {

                    if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "Huevo"){
                        ticket.AgregaArticulo(
                            dataGridView1.Rows[i].Cells[1].Value.ToString(),
                            double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                            double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())
                            );
                    }
                }
                ticket.lineasIgual();
                ticket.EncabezadoMercaderia();
                ticket.TextoIzquierda("");
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "Mercaderia")
                    {
                        ticket.AgregaArticulo(
                            dataGridView1.Rows[i].Cells[1].Value.ToString(),
                            double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()),
                            double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())
                            );
                    }
                }
                ticket.TextoIzquierda("");
                ticket.lineasBarra();
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("Informe Cartones");
                ticket.lineasAsteriscos();
                ticket.TextoExtremos("Ingresaron:",label12.Text);
                ticket.TextoExtremos("Egresaron:", label32.Text);
                ticket.lineasGuio();
                ticket.TextoIzquierda("EFECTIVO");
                ticket.TextoExtremos("Egresaron:", label20.Text);
                ticket.TextoExtremos("Importe:", label18.Text);
                ticket.lineasGuio();
                ticket.TextoIzquierda("REGISTRADORA");
                ticket.TextoExtremos("Egresaron:", label88.Text);
                ticket.TextoExtremos("Importe:", label86.Text);
                ticket.lineasGuio();
                ticket.TextoIzquierda("CUENTA CORRIENTE");
                ticket.TextoExtremos("Egresaron:", label26.Text);
                ticket.TextoExtremos("Importe:", label24.Text);
                ticket.lineasGuio();
                ticket.TextoIzquierda("BONIFICADOS");
                ticket.TextoExtremos("Egresaron:", label29.Text);
                ticket.TextoIzquierda("");
                ticket.lineasBarra();
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("Informe Mercaderia");
                ticket.lineasAsteriscos();
                ticket.TextoExtremos("Cantidad:", label36.Text);
                ticket.TextoExtremos("Importe:", label34.Text);
                ticket.TextoIzquierda("");
                ticket.lineasBarra();
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("Cuentas Corrientes");
                ticket.lineasAsteriscos();
                ticket.EncabezadoClientes();
                ticket.TextoIzquierda("");
                for (int i =0; i<lventas.Count();i++) {
                   
                    if (lventas[i].Pagos[0].Codigo == "1.1.3")
                    {
                        ticket.AgregaArticulo(lventas[i].Receptor,
                            lventas[i].CantidadHuevos(),
                            lventas[i].CantidadMercaderia(),
                            lventas[i].Pagos[0].Importe);

                        for (int j = 0;j<lventas[i].ListaProdutos.Count();j++) {
                            ticket.TextoExtremos(lventas[i].ListaProdutos[j].Nombre, lventas[i].ListaProdutos[j].Cantidad.ToString());
                        }

                        ticket.lineasGuio();
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

        private void label94_Click(object sender, EventArgs e)
        {

        }

        private void label115_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            GenerarPdfInormeYemas();

        }
        public void GenerarPdfInormeYemas()
        {
            
            string FechaActual = DateTime.Now.Date.ToString();
            FileStream p = new FileStream(new Direcciones().ArchivoPdf + "fecha actual" + ".pdf", FileMode.Create);
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

            doc.Add(new Paragraph("Informe Yemas del sol"));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Phrase("Fecha: " + FechaActual, StandarFont));
            doc.Add(Chunk.NEWLINE);
            doc.Add(new Paragraph("INFORME HUEVOS"));
            
            doc.Add(new Paragraph("Cantidad Ingreso: "+label12.Text));
         
            //encabezado columnas
            doc.Add(new Paragraph("tabla de Egresos"));
          

            PdfPTable tablePagos = new PdfPTable(3);
            tablePagos.WidthPercentage = 100;

            //configurando el titulo de las comunas
            PdfPCell medio = new PdfPCell(new Phrase("Medio", StandarFont));
            medio.BorderWidth = 0;
            medio.BorderWidthBottom = 0.75f;

            PdfPCell egreso = new PdfPCell(new Phrase("Egreso", StandarFont));
            egreso.BorderWidth = 0;
            egreso.BorderWidthBottom = 0.75f;

            PdfPCell monto = new PdfPCell(new Phrase("Monto", StandarFont));
            monto.BorderWidth = 0;
            monto.BorderWidthBottom = 0.75f;

            tablePagos.AddCell(medio);
            tablePagos.AddCell(egreso);
            tablePagos.AddCell(monto);


            medio = new PdfPCell(new Phrase("Efectivo", StandarFont));
            medio.BorderWidth = 0;
            egreso = new PdfPCell(new Phrase(label20.Text, StandarFont));
            egreso.BorderWidth = 0;
            monto = new PdfPCell(new Phrase(label18.Text, StandarFont));
            monto.BorderWidth = 0;



            tablePagos.AddCell(medio);
            tablePagos.AddCell(egreso);
            tablePagos.AddCell(monto);
            doc.Add(Chunk.NEWLINE);
            doc.Add(tablePagos);
            doc.Add(new Phrase("Total: " + label30.Text, StandarFont));

            
            doc.Close();
            pw.Close();

            MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);



            /*
            PdfPTable tableEjemplo = new PdfPTable(4);
            tableEjemplo.WidthPercentage = 100;

            //configurando el titulo de las comunas
            PdfPCell clProducto = new PdfPCell(new Phrase("Producto", StandarFont));
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

            for (int i = 0; i < x.ListaProdutos.Count(); i++)
            {
                clProducto = new PdfPCell(new Phrase(x.ListaProdutos[i].Nombre, StandarFont));
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
            doc.Add(new Phrase("Total: $" + x.TotalVenta(), StandarFont));

            doc.Add(tableEjemplo);
            doc.Close();
            pw.Close();

            MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            */
        }
    }
}
