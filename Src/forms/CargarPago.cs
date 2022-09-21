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
    public partial class CargarPago : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        public CargarPago(object x, ref Form y)
        {
            InitializeComponent();
            Pago.MostrarDataGrid(ref dataGridView1);
            xUsuario = (Usuario)x;
            FormularioAnterior = y;

            if (xUsuario.Tipo != 1) { panel1.Visible = false; }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { button1.Focus(); }
        }

        // cargar pago
        private void button1_Click(object sender, EventArgs e)
        {
            Pago p = new Pago();
            p.Nombre = textBox1.Text;
            p.Codigo = textBox2.Text;

            Pago.Crear(p);
            Pago.MostrarDataGrid(ref dataGridView1);
            textBox1.Text = textBox2.Text = "";
            textBox2.Focus();
        }

        //selecionar pago
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;

                // codigo
                textBox5.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //nombre
                textBox6.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                //importe
                textBox7.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();

            }
            catch (Exception) { }
        }
        // actualizar pago
        private void button3_Click(object sender, EventArgs e)
        {
            Pago upPago = new Pago();
            //nombre
            upPago.Nombre = textBox6.Text;

            try
            { //cantidad
                upPago.Importe = double.Parse(textBox7.Text);
                

                Pago.Actualizar(textBox5.Text, upPago);
                Pago.MostrarDataGrid(ref dataGridView1);
            }
            catch (Exception)
            {  //Error
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { textBox1.Focus(); }
        }

        private void CargarPago_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
