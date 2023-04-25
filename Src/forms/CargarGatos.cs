using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace E_Shop
{
    public partial class CargarGatos : Form
    {
        RemitoGasto x;
        Usuario user;
        public CargarGatos(object user)
        {
            InitializeComponent();
            loadData();
            this.user = (Usuario)user;

          
        }

        public void loadData() {
            x = new RemitoGasto();
            comboBox1.Items.Clear(); comboBox2.Items.Clear();
            Pago.CargarComboBox(ref comboBox1, "1.1.1");
            Pago.CargarComboBox(ref comboBox2, "3.3.3");
            

        }
        public void vaciar()
        {
            comboBox1.Text = comboBox2.Text = textBox9.Text = "";
        }


        //guardar Gasto
        private void button3_Click(object sender, EventArgs e)
        {
            if (x.Fuente!=null && x.Razon!=null) {
                try {
                    x.Emisor = x.Receptor = user.Codigo;
                    x.Fuente.Importe = double.Parse(textBox9.Text);
                    x.Razon.Importe = double.Parse(textBox9.Text);
                    x.FechaEmision = DateTime.Now.ToShortDateString();
                    if (RemitoGasto.Crear(x))
                    {
                        vaciar();
                    }
                    else {
                        MessageBox.Show("Ocurrio un problema para guardar el gasto");
                    }
                } catch (Exception err) { MessageBox.Show(err.ToString()); }
            }
            else
            {
                MessageBox.Show("Debe seleccionar Fuente y Razon");
            }
        }
        // seleccionar Fuente
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            x.Fuente = Pago.BuscarPorNombre(comboBox1.Text);
        }
        // seleccionar razon
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            x.Razon= Pago.BuscarPorNombre(comboBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            vaciar();
           
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
