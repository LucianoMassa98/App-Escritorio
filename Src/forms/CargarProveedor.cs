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
    public partial class CargarProveedor : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        public CargarProveedor(object x, ref Form y)
        {
            InitializeComponent();
            Proveedor.MostrarDataGrid(ref dataGridView1);
            xUsuario = (Usuario)x;
            FormularioAnterior = y;

            if (xUsuario.Tipo != 1) { panel1.Visible = false; }
        }

        // crear proveedor
        private void button1_Click(object sender, EventArgs e)
        {
            Proveedor c = new Proveedor();
            c.Nombre = textBox1.Text;
            c.Tel = textBox2.Text;
            c.Correo = textBox3.Text;
            c.Direccion = textBox4.Text;

            if (Proveedor.Crear(c))
            {
                // se creo correctamente y agregar al datagrid

                Proveedor.MostrarDataGrid(ref dataGridView1);
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
                textBox1.Focus();
            }
            else
            {

                // no se agrego corretamente el cliente
            }
        }
        //actualizar proveedor
        private void button3_Click(object sender, EventArgs e)
        {
            Proveedor upProveedor= new Proveedor();
            // codigo
            //upCliente.Codigo = textBox10.Text;
            //nombre
            upProveedor.Nombre = textBox5.Text;
            //telefono
            upProveedor.Tel = textBox6.Text;
            //correo
            upProveedor.Correo = textBox7.Text;
            //direccion
            upProveedor.Direccion = textBox8.Text;
            //saldo
            try
            {
                upProveedor.Saldo = double.Parse(textBox9.Text);
                Proveedor.Actualizar(textBox10.Text, upProveedor);
                Proveedor.MostrarDataGrid(ref dataGridView1);



            }
            catch (Exception)
            {

                //Error
            }

        }
        // seleccionar proveedor
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
                //saldo
                textBox9.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();

            }
            catch (Exception) { }
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

        private void CargarProveedor_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ProductosProveedor(textBox10.Text).Show();
        }
    }
}
