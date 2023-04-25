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
    public partial class ProductosCliente : Form
    {
       
         ProductoCliente x;
         List<Producto> ListaProductos;
        public ProductosCliente(string codClliente)
        {
            InitializeComponent();
            x = ProductoCliente.findOne(codClliente);
            if (x==null) {
                x = new ProductoCliente();
                x.CodigoCliente = codClliente;

                ProductoCliente.create(x);

            }
            ListaProductos = Producto.Buscar();
            loadComboBox();
            loadDataGrid();
            label9.Text = "Lista de Precios \n" + Cliente.BuscarPorCodigo(codClliente).Nombre;

        }
     
        public void clearForms()
        {
            comboBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Focus();
        }
        public void loadComboBox() {
            comboBox1.Items.Clear();

            for (int i = 0; i < ListaProductos.Count(); i++)
            {
                comboBox1.Items.Add(ListaProductos[i].Codigo);
                comboBox1.Items.Add(ListaProductos[i].Nombre);
            }
        }
        public void loadDataGrid() {
            if (x!=null)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < x.ListaX.Count(); i++)
                {

                    dataGridView1.Rows.Add(x.ListaX[i].Codigo, x.ListaX[i].Nombre, x.ListaX[i].Descripcion, x.ListaX[i].Precio);
                }
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Producto prodX;

            if((prodX = x.FindOneProduct(comboBox1.Text)) == null)
            {
                prodX = x.FindOneProduct(comboBox1.Text);
            }
           
            if (prodX != null) {

                    prodX.Precio = double.Parse(textBox2.Text);
                    x.AddProductosCliente(prodX);
                    ProductoCliente.update(x.CodigoCliente, x);
                loadDataGrid();
                clearForms();
            }
            else
            {
                Producto n;
                if ((n = Producto.BuscarPorNombre(comboBox1.Text)) ==null) {
                    n = Producto.BuscarPorCodigo(comboBox1.Text);
                }
                
                if (n != null)
                {
                    n.Precio = double.Parse(textBox2.Text);
                    x.AddProductosCliente(n);
                    ProductoCliente.update(x.CodigoCliente, x);

                    loadDataGrid();
                    clearForms();
                }
                else
                {
                    //Producto inexistente
                    MessageBox.Show("Producto no encontrado en la base de datos");
                }

            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }
        }

        private void ProductosCliente_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
