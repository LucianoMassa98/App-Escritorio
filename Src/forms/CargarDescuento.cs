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
    public partial class CargarDescuento : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        public CargarDescuento(object x, ref Form y)
        {
            InitializeComponent();
            Descuento.MostrarDataGrid(ref dataGridView1);
            xUsuario = (Usuario)x;
            FormularioAnterior = y;

            if (xUsuario.Tipo != 1) { panel1.Visible = false; }
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { textBox2.Focus(); }
          
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { textBox3.Focus(); }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }
        }
        // crear descuento
        private void button1_Click(object sender, EventArgs e)
        {
            Descuento p = new Descuento();
            p.Nombre = textBox1.Text;
            try {
                p.CantidadRequerida = double.Parse(textBox2.Text);
                p.ImporteDescuento = double.Parse(textBox3.Text);
                Descuento.Crear(p);
                Descuento.MostrarDataGrid(ref dataGridView1);

            } catch (Exception) { }

        }
        // selecion descuneto
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;

                // codigo
                textBox5.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //nombre
                textBox6.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                //cantidad requerida
                textBox7.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                //importedescontar
                textBox4.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();

            }
            catch (Exception) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Descuento upDescuento = new Descuento();
            //nombre
            upDescuento.Nombre = textBox6.Text;

            try
            { //cantidad
                upDescuento.CantidadRequerida = double.Parse(textBox7.Text);

                //precio
                upDescuento.ImporteDescuento = double.Parse(textBox4.Text);

                Descuento.Actualizar(textBox5.Text, upDescuento);
                Descuento.MostrarDataGrid(ref dataGridView1);
            }
            catch (Exception)
            {  //Error
            }
        }

        private void CargarDescuento_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }
    }
}
