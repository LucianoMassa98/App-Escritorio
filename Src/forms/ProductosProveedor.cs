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
    public partial class ProductosProveedor : Form
    {
        ProductoProveedor x;
        List<Producto> ListaProductos;
        public ProductosProveedor(string codProve)
        {
            InitializeComponent();
            x = ProductoProveedor.findOne(codProve);
            if (x == null)
            {
                x = new ProductoProveedor();
                x.CodigoProveedor = codProve;

                ProductoProveedor.create(x);

            }
            ListaProductos = Producto.Buscar();
            loadComboBox();
            loadDataGrid();
            label9.Text = "Lista de Costos \n" + Proveedor.BuscarPorCodigo(codProve).Nombre;
        }

        public void clearForms()
        {
            comboBox1.Text = "";
            textBox2.Text = "";
            comboBox1.Focus();
        }
        public void loadComboBox()
        {
            comboBox1.Items.Clear();

            for (int i = 0; i < ListaProductos.Count(); i++)
            {
                comboBox1.Items.Add(ListaProductos[i].Codigo);
                comboBox1.Items.Add(ListaProductos[i].Nombre);
            }
        }
        public void loadDataGrid()
        {
            if (x != null)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < x.ListaX.Count(); i++)
                {

                    dataGridView1.Rows.Add(x.ListaX[i].Codigo, x.ListaX[i].Nombre, x.ListaX[i].Descripcion, x.ListaX[i].Costo);
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Producto prodX;

            if ((prodX = x.FindOneProduct(comboBox1.Text)) == null)
            {
                prodX = x.FindOneProduct(comboBox1.Text);
            }

            if (prodX != null)
            {

                prodX.Costo = double.Parse(textBox2.Text);
                x.AddProductosCliente(prodX);
                ProductoProveedor.update(x.CodigoProveedor, x);
                loadDataGrid();
                clearForms();
            }
            else
            {
                Producto n;
                if ((n = Producto.BuscarPorNombre(comboBox1.Text)) == null)
                {
                    n = Producto.BuscarPorCodigo(comboBox1.Text);
                }

                if (n != null)
                {
                    n.Costo = double.Parse(textBox2.Text);
                    x.AddProductosCliente(n);
                    ProductoProveedor.update(x.CodigoProveedor, x);

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
            if (e.KeyChar==13) { button1.Focus(); }
        }
  
    
    
    
    }
}
