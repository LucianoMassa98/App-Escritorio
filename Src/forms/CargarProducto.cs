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
    public partial class CargarProducto : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        public CargarProducto(object x, ref Form y)
        {
            InitializeComponent();
          
            label14.Text = "Stock: $" + Producto.SumaCostos(Producto.Buscar());
            xUsuario = (Usuario)x;
            FormularioAnterior = y;
            if (xUsuario.Tipo != 1) { 
                label14.Visible =
                panel1.Visible = false;
                Producto.MostrarDataGrid2(ref dataGridView1);
            }
            else
            {
                Producto.MostrarDataGrid(ref dataGridView1);
            }
            LoadCombobox();
        }
        public void LoadCombobox() {

            List<Producto> x = Producto.Buscar();
            for(int i= 0; i<x.Count();i++){

                comboBox1.Items.Add(x[i].Nombre);
            }
            

         }
      
        // crear PRoducto
        private void button1_Click(object sender, EventArgs e)
        {

            Producto c = new Producto();
            c.Nombre = textBox1.Text;
            c.Descripcion = textBox2.Text;
            c.Codigo = textBox3.Text;

            if (Producto.Crear(c))
            {
                // se creo correctamente y agregar al datagrid

                if (xUsuario.Tipo != 1)
                {
                    Producto.MostrarDataGrid2(ref dataGridView1);
                }
                else
                {
                    Producto.MostrarDataGrid(ref dataGridView1);
                }

                textBox1.Text = textBox2.Text = textBox3.Text  = "";
                textBox3.Focus();
            }
            else
            {

                // no se agrego corretamente el cliente
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { textBox1.Focus(); }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { textBox2.Focus(); }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }
        }

        // selecionar producto
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;

                // codigo
                textBox10.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                //nombre
                textBox5.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                //Descipcion
                textBox6.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                //bulto
                textBox11.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                //cantidad
                textBox7.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                //cantida estandar
                textBox8.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                //costo
                textBox9.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                //Venta
                textBox4.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                //Venta2
                textBox12.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
                //Venta3
                textBox13.Text = dataGridView1.Rows[i].Cells[9].Value.ToString();

            }
            catch (Exception) { }
        }
        // actualizar producto
        private void button3_Click(object sender, EventArgs e)
        {
            Producto upProducto= new Producto();
            //nombre
            upProducto.Nombre = textBox5.Text;
            //descipcion
            upProducto.Descripcion = textBox6.Text;
           
            try
            {
                //cantidad
                upProducto.Bulto = double.Parse(textBox11.Text);
                //cantidad
                upProducto.Cantidad = double.Parse(textBox7.Text);
                //cantidad estandar
                upProducto.CantidadEstandar = double.Parse(textBox8.Text);
                //costo
                upProducto.Costo = double.Parse(textBox9.Text);
                //venta
                upProducto.Precio = double.Parse(textBox4.Text);
                //venta
                upProducto.Precio2 = double.Parse(textBox12.Text);
                //venta
                upProducto.Precio3 = double.Parse(textBox13.Text);

                Producto.Actualizar(textBox10.Text, upProducto);
                if (xUsuario.Tipo != 1)
                {
                    Producto.MostrarDataGrid2(ref dataGridView1);
                }
                else
                {
                    Producto.MostrarDataGrid(ref dataGridView1);
                }
                label14.Text = "Stock: $"+Producto.SumaCostos(Producto.Buscar());
            }
            catch (Exception)
            {  //Error
                MessageBox.Show("Error en los campos del producto");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            while ((i < dataGridView1.RowCount)
                && (dataGridView1.Rows[i].Cells[1].Value.ToString() != comboBox1.Text)) { i++; }
            if (i < dataGridView1.RowCount)
            {

                dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
        //imprimir codigo barras
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox10.Text != "")
                new ImpirmirCodigoBarra(textBox5.Text, CodigoBarra.Generar(textBox10.Text)).Show();
        }

        private void CargarProducto_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CrearPdf n = new CrearPdf();
            n.GenerarPdfProductos(dataGridView1);
        }
    }
}
