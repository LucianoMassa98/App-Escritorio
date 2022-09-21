using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace E_Shop
{
    public partial class seleccionaPago : Form
    {
        RemitoCompra RemitoX;
        List<Pago> listaPagos;
        CargarRemitoCompra anterior;
        public seleccionaPago(object x,ref Form y)
        {
            InitializeComponent();
            anterior = (CargarRemitoCompra)y;
            anterior.Enabled = false;
            RemitoX = (RemitoCompra)x;
            listaPagos = new List<Pago>();
            LoadComboBox();
            LoadLabel();
        }

        public void LoadComboBox()
        {
            Pago.CargarComboBox(ref comboBox4, "1.1.1");
            comboBox4.Items.Add(Pago.BuscarPorCodigo("2.1.1").Nombre);
            comboBox4.Items.Add("aaa");
        }
        public void LoadLabel()
        {
            Proveedor x = Proveedor.BuscarPorNombre(RemitoX.Emisor);
            if (x!=null) {
                label4.Text = "Saldo: $" + x.Saldo.ToString();
                label1.Text = "Total Remito: $" + RemitoX.TotalCosto().ToString();
                label3.Text = "Proveedor: " + x.Nombre;
            }
            else
            {
                MessageBox.Show("Proveedor inexistente");
            }
            
        }
        public void mostrarGrid() {
            dataGridView1.Rows.Clear();
            
        foreach(Pago pago in listaPagos)
            {
                dataGridView1.Rows.Add(pago.Nombre, pago.Importe);
            }
            label5.Text = "SubTotal: $"+Pago.SumarImportes(listaPagos);

        }

        public void completeTextBox() {

            double imp = RemitoX.TotalCosto()- Pago.SumarImportes(listaPagos) ;
           if(imp < 1) { MessageBox.Show("El total de pagos ya esta cargado"); } else
            {
                textBox1.Text = imp.ToString();
            }


        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text=="aaa") {
                button3.Focus();
            } else
            {
                completeTextBox();
                textBox1.Focus();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pago newpago = Pago.BuscarPorNombre(comboBox4.Text);
            if (newpago!=null) {
                try {
                    double importe = double.Parse(textBox1.Text);

                    if ((importe + Pago.SumarImportes(listaPagos)) <= RemitoX.TotalCosto()) {

                        newpago.Importe = importe;
                      bool band =  Pago.AgregarPago(ref listaPagos, newpago);

                        if (band) {
                            mostrarGrid();
                            comboBox4.Text = textBox1.Text = "";
                            comboBox4.Focus();
                        }
                        else { MessageBox.Show("El pago ya se encuentra en la lista");
                            comboBox4.Text = textBox1.Text = "";
                            comboBox4.Focus();
                        }
                    } else
                    {
                        textBox1.Text = "";
                        textBox1.Focus();
                        MessageBox.Show("El importe ingresado supera el importe total de la compra ");
                    }
                }
                catch (Exception) { }
                
            } else { MessageBox.Show("El tipo de pago no existe"); }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { button1.Focus(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            anterior.Enabled = true;
            Form k = this;
            k.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount>0) {
                int i = dataGridView1.CurrentRow.Index;

             bool band =   Pago.RestarPago(ref listaPagos, dataGridView1.Rows[i].Cells[0].Value.ToString());

                if (band) { mostrarGrid(); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RemitoX.Pagos = listaPagos;

         
            if (RemitoCompra.Crear(RemitoX)) { 
                try
                {
                    if (Imprimir.Checked == true)
                    {
                        RemitoX.Imprimir();
                    }
                    if (pdfImprirmir.Checked==true) {

                        Form este = this;
                        este.Enabled = false;
                        CrearPdf n = new CrearPdf();
                        n.GenerarPdfRemitoCompra(RemitoX);
                        Process abrirpdf = new Process();
                        string file = new Direcciones().ArchivoPdf + RemitoX.Emisor + ".pdf";
                        abrirpdf.StartInfo.FileName = file;
                        abrirpdf.Start();
                    }
                }
                catch (Exception) { }
                anterior.Enabled = true;
                anterior.NuevoRemito();
                anterior.FinCarga();
                Form k = this;
                k.Close();  
            }
        }
    }
}
