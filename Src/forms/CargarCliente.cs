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
    public partial class CargarCliente : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        public CargarCliente(object x,ref Form y)
        {
            InitializeComponent();
            Cliente.MostrarDataGrid(ref dataGridView1);
            xUsuario = (Usuario)x;
            FormularioAnterior = y;

            if (xUsuario.Tipo != 1) { panel1.Visible = false; }

            //Rojo: 255;99;125
            //amarillo claro : 244;241;187
            // celeste: 102;216;209
            //blanco : 234;242;227
            // amarillo: 255;248;127

        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { textBox2.Focus(); }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { textBox3.Focus(); }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13) { textBox4.Focus(); }
        }

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }

        }
        // añadir cliente 
        private void button1_Click(object sender, EventArgs e)
        {
            Cliente c = new Cliente();
            c.Nombre = textBox1.Text;
            c.Tel = textBox2.Text;
            c.Correo = textBox3.Text;
            c.Direccion = textBox4.Text;

            if (Cliente.Crear(c))
            {
                // se creo correctamente y agregar al datagrid

                Cliente.MostrarDataGrid(ref dataGridView1);

                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
                textBox1.Focus();
            }
            else { 
            
            // no se agrego corretamente el cliente
            }
        }
        // borrar cliente
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                string codigo = dataGridView1.Rows[i].Cells[0].Value.ToString();
                if (Cliente.Borrar(codigo))
                {
                    Cliente.MostrarDataGrid(ref dataGridView1);
                }
                else { 
                // algo salio mal
                }
            }
            catch (Exception) { }
        }
        //seleccion de cliente
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
               
                // codigo
                textBox10.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //nombre
                textBox5.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                //telefono
                textBox6.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                //correo
                textBox7.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                //direccion
                textBox8.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                //tipo
                numericUpDown1.Value = decimal.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()); ;
                //saldo
                textBox11.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();

            }
            catch (Exception) { }
        }
        // actualziar cliente
        private void button3_Click(object sender, EventArgs e)
        {
            Cliente upCliente = new Cliente();
            // codigo
            //upCliente.Codigo = textBox10.Text;
            //nombre
            upCliente.Nombre = textBox5.Text;
            //telefono
            upCliente.Tel = textBox6.Text;
            //correo
            upCliente.Correo = textBox7.Text;
            //direccion
            upCliente.Direccion = textBox8.Text;
            //saldo
            try
            {
                upCliente.Tipo = double.Parse(numericUpDown1.Value.ToString());
                upCliente.Saldo = double.Parse(textBox11.Text);
               
                Cliente.Actualizar(textBox10.Text,upCliente);
                Cliente.MostrarDataGrid(ref dataGridView1);
            }
            catch (Exception) {  //Error
                                 }
        }

        private void CargarCliente_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            new ProductosCliente(textBox10.Text).Show();
        }
    }
}
