using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            panel3.BackgroundImage = Image.FromFile(dir.Logo);
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
                if (listBox2.Items.Contains(lventas[i].Receptor) == false) { listBox2.Items.Add(lventas[i].Receptor); }
                if (lventas[i].Pagos[0].Codigo=="1.1.3") {

                    listBox1.Items.Add(lventas[i].Receptor+" \t "+lventas[i].Pagos[0].Importe);

                    
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
                ticket.TextoCentro(dir.Space);
                ticket.TextoCentro("Informe");
                ticket.TextoIzquierda("FECHA: " + DateTime.Now.ToShortDateString());
                ticket.TextoIzquierda("HORA: " + DateTime.Now.ToShortTimeString());
                ticket.lineasAsteriscos();
                ticket.TextoCentro("Clientes\tCreditos");
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    string linea = listBox1.Items[i].ToString();
                    ticket.TextoCentro(linea);
                }
                ticket.lineasAsteriscos();
                ticket.TextoCentro("Resumen Huevos");
                ticket.TextoIzquierda("Ingreso");
                ticket.lineasGuio();
                ticket.TextoIzquierda("Produccion");
                ticket.TextoIzquierda(label12.Text + "\t" + label13.Text);
                ticket.TextoIzquierda("Egreso");
                ticket.lineasGuio();
                ticket.TextoIzquierda("Efectivo");
                ticket.TextoIzquierda(label20.Text + "\t" + label18.Text);
                ticket.TextoIzquierda("Mercado Pago");
                ticket.TextoIzquierda(label23.Text + "\t" + label21.Text);
                ticket.TextoIzquierda("Credito");
                ticket.TextoIzquierda(label26.Text + "\t" + label24.Text);
                ticket.TextoIzquierda("Bonificados");
                ticket.TextoIzquierda(label29.Text + "\t" + label27.Text);
                ticket.TextoIzquierda("Registradora");
                ticket.TextoIzquierda(label88.Text + "\t" + label86.Text);
                ticket.TextoIzquierda("Total");
                ticket.TextoIzquierda(label32.Text + "\t" + label30.Text);

                ticket.lineasAsteriscos();
                ticket.TextoCentro("Resumen Mercaderia");
                ticket.TextoIzquierda("Ingreso");
                ticket.lineasGuio();
                ticket.TextoIzquierda("Compras");
                ticket.TextoIzquierda(label59.Text + "\t" + label56.Text);
                ticket.TextoIzquierda("Egreso");
                ticket.lineasGuio();
                ticket.TextoIzquierda("Efectivo");
                ticket.TextoIzquierda(label48.Text + "\t" + label46.Text);
                ticket.TextoIzquierda("Mercado Pago");
                ticket.TextoIzquierda(label45.Text + "\t" + label43.Text);
                ticket.TextoIzquierda("Credito");
                ticket.TextoIzquierda(label42.Text + "\t" + label40.Text);
                ticket.TextoIzquierda("Bonificados");
                ticket.TextoIzquierda(label39.Text + "\t" + label37.Text);
                ticket.TextoIzquierda("Registradora");
                ticket.TextoIzquierda(label85.Text + "\t" + label76.Text);
                ticket.TextoIzquierda("Total");
                ticket.TextoIzquierda(label36.Text + "\t" + label34.Text);



                ticket.lineasAsteriscos();
                ticket.TextoCentro("Resumen Total");
                ticket.TextoIzquierda("Ingreso");
                ticket.lineasGuio();
                ticket.TextoIzquierda(label83.Text + "\t" + label82.Text);
                ticket.TextoIzquierda(label93.Text + "\t" + label92.Text);
                ticket.TextoIzquierda("Egreso");
                ticket.lineasGuio();
                ticket.TextoIzquierda(label90.Text + "\t" + label89.Text);
                ticket.TextoIzquierda(label73.Text + "\t" + label72.Text);
                ticket.TextoIzquierda(label70.Text + "\t" + label69.Text);
                ticket.TextoIzquierda(label67.Text + "\t" + label66.Text);
                ticket.TextoIzquierda(label61.Text + "\t" + label60.Text);
                ticket.TextoIzquierda(label64.Text + "\t" + label63.Text);

                ticket.lineasAsteriscos();
                ticket.TextoCentro("Ventas y Registradora");
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    // articulos
                    ticket.TextoCentro(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    ticket.TextoCentro(
                        dataGridView1.Rows[i].Cells[3].Value.ToString() + "\t - $" +
                        dataGridView1.Rows[i].Cells[4].Value.ToString()


                        );
                }

                ticket.lineasAsteriscos();
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
    }
}
