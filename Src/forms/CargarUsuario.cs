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
    public partial class CargarUsuario : Form
    {

        Usuario xUsuario;
        Form FormularioAnterior;
        public CargarUsuario(object x, ref Form y)
        {
            InitializeComponent();
            Usuario.MostrarDataGrid(ref dataGridView1);
            xUsuario = (Usuario)x;
            FormularioAnterior = y;

            if (xUsuario.Tipo != 1) { panel1.Visible = false; }
        }

        //crear usuario
        private void button1_Click(object sender, EventArgs e)
        {
            Usuario c = new Usuario();
            c.Nombre = textBox1.Text;
            c.Tel = textBox2.Text;
            c.Correo = textBox3.Text;
            c.Direccion = textBox4.Text;
            c.Codigo = textBox10.Text;
            try
            {
                

                if (Usuario.Crear(c))
                {
                    // se creo correctamente y agregar al datagrid

                    Usuario.MostrarDataGrid(ref dataGridView1);

                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox10.Text = "";
                }
                else
                {

                    // no se agrego corretamente el cliente
                }
            }
            catch (Exception) { }

        }
        //seleccionar usuario
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;

                // codigo
                textBox12.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //nombre
                textBox5.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                //telefono
                textBox6.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                //correo
                textBox7.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                //direccion
                textBox8.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                //tipo
                textBox9.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                //saldo
                textBox11.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();

            }
            catch (Exception) { }
        }
        // actualizar cliente
        private void button3_Click(object sender, EventArgs e)
        {

            Usuario upUsuario = new Usuario();

            //Codigo
            upUsuario.Codigo= textBox12.Text;
            //nombre
            upUsuario.Nombre = textBox5.Text;
            //telefono
            upUsuario.Tel = textBox6.Text;
            //correo
            upUsuario.Correo = textBox7.Text;
            //direccion
            upUsuario.Direccion = textBox8.Text;
            try
            {   //tipo
                upUsuario.Tipo = int.Parse(textBox9.Text);
                //saldo
                upUsuario.Saldo = double.Parse(textBox11.Text);
                if (Usuario.Actualizar(textBox12.Text, upUsuario)) {

                    Usuario.MostrarDataGrid(ref dataGridView1);
                } else { 
                
                //Error
                }
            
            }
            catch (Exception)
            {  //Error
            }
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
            if (e.KeyChar == 13) { textBox4.Focus(); }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { textBox1.Focus(); }
        }

        private void CargarUsuario_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }
    }
}
