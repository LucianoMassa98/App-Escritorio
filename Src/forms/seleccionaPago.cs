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
    public partial class seleccionaPago : Form
    {
        RemitoCompra RemitoX;
        List<Pago> listaPagos;
        public seleccionaPago(object x)
        {
            InitializeComponent();

            RemitoX = (RemitoCompra)x;
            listaPagos = new List<Pago>();
        }

        public void LoadComboBox()
        {
            Pago.CargarComboBox(ref comboBox4, "1.1.1");
            comboBox4.Items.Add(Pago.BuscarPorCodigo("2.1.1").Nombre);
        }

        public double totalMonto() { return 0; }

        public bool agregarPago(Pago x) { return true; }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = totalMonto().ToString();
            textBox1.Focus();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pago newpago = Pago.BuscarPorNombre(comboBox4.Text);
            if (newpago!=null) { 
            //newpago

            } else { MessageBox.Show("El tipo de pago no existe"); }
            
        }
    }
}
