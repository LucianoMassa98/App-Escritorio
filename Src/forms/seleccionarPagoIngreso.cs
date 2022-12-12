using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace E_Shop
{
    public partial class seleccionarPagoIngreso : Form
    {
        RemitoIngreso RemitoY;
        List<Pago> listaCobros;
        ListaIngresos anterior;
        public seleccionarPagoIngreso(object x, ref Form y)
        {
            InitializeComponent();
            anterior = (ListaIngresos)y;
            anterior.Visible = false;
            RemitoY = (RemitoIngreso)x;
            listaCobros = new List<Pago>();
            LoadComboBox();
            LoadLabel();
            

        }

        public void LoadComboBox()
        {
            Pago.CargarComboBox(ref comboBox4, "1.1.1");
            comboBox4.Items.Add(Pago.BuscarPorCodigo("1.1.3").Nombre);
            comboBox4.Items.Add(Pago.BuscarPorCodigo("aaaa").Nombre);
            // comboBox4.Items.Add(Pago.BuscarPorCodigo("1.1.4").Nombre);
            comboBox4.Items.Add("aaa");
        }
        public void LoadLabel()
        {
           
                Proveedor y = Proveedor.BuscarPorNombre(RemitoY.Receptor);
                if (y!=null) {
                    label4.Text = "Saldo: $" + y.Saldo.ToString();
                    label1.Text = "Total Remito: $" + RemitoY.TotalCosto().ToString();
                    label3.Text = "Proveedor: " + y.Nombre;
                }
                else { MessageBox.Show("Error 402"); }
        }
        public void mostrarGrid()
        {
            dataGridView1.Rows.Clear();

            foreach (Pago pago in listaCobros)
            {
                dataGridView1.Rows.Add(pago.Nombre, pago.Importe);
            }
            label5.Text = "SubTotal: $" + Pago.SumarImportes(listaCobros);

        }
        public void completeTextBox()
        {

            double imp = RemitoY.TotalCosto() - Pago.SumarImportes(listaCobros);
            if (imp < 1) { MessageBox.Show("El total de cobros ya esta cargado"); }
            else
            {
                textBox1.Text = imp.ToString();
            }


        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "aaa")
            {
                button3.Focus();
            }
            else
            {
                completeTextBox();
                textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pago newpago = Pago.BuscarPorNombre(comboBox4.Text);
            if (newpago != null)
            {
                try
                {
                    double importe = double.Parse(textBox1.Text);

                    if (Math.Round(importe + Pago.SumarImportes(listaCobros)) <= Math.Round(RemitoY.TotalCosto()))
                    {

                        newpago.Importe = importe;
                        bool band = Pago.AgregarPago(ref listaCobros, newpago);

                        if (band)
                        {
                            mostrarGrid();
                            comboBox4.Text = textBox1.Text = "";
                            comboBox4.Focus();
                        }
                        else
                        {
                            MessageBox.Show("El pago ya se encuentra en la lista");
                            comboBox4.Text = textBox1.Text = "";
                            comboBox4.Focus();
                        }
                    }
                    else
                    {
                        textBox1.Text = "";
                        textBox1.Focus();
                        MessageBox.Show("El importe ingresado supera el importe total de la compra ");
                    }
                }
                catch (Exception) { }

            }
            else { MessageBox.Show("El tipo de pago no existe"); }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }

        }
      
        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.RowCount > 0)
            {
                int i = dataGridView1.CurrentRow.Index;

                bool band = Pago.RestarPago(ref listaCobros, dataGridView1.Rows[i].Cells[0].Value.ToString());

                if (band) { mostrarGrid(); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount>0)
            {
                RemitoCompra RemitoX = new RemitoCompra();
                RemitoX.Emisor = RemitoY.Emisor;
                RemitoX.Receptor = RemitoY.Receptor;
                RemitoX.ListaProdutos = RemitoY.ListaProdutos;
                RemitoX.FechaEmision = RemitoY.FechaEmision;
                RemitoX.Pagos = listaCobros;
                
               
                if (RemitoCompra.Crear(RemitoX))
                {
                    try
                    {
                        if (Imprimir.Checked == true)
                        {
                            RemitoX.Imprimir();
                        }
                        if (PdfImprimir.Checked == true)
                        {
                            Form este = this;
                            este.Enabled = false;
                            CrearPdf n = new CrearPdf();
                            n.GenerarPdfRemitoCompra(RemitoX);
                            Process abrirpdf = new Process();
                            string file = new Direcciones().ArchivoPdf + RemitoX.Receptor + ".pdf";
                            abrirpdf.StartInfo.FileName = file;
                            abrirpdf.Start();
                        }

                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }

                    anterior.BorraRemito();
                    
                 //   anterior.nuevoRemito();
                   // anterior.FinCarga();

                    Form k = this;
                    k.Close();
                }
                else
                {
                    MessageBox.Show("No se puedo generar Remito");
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            anterior.Visible = true;
            Form k = this;
           
            listaCobros.Clear();
            k.Close();
            
        }
    }
}
