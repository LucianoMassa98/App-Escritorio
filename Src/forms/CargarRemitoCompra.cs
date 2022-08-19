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
    public partial class CargarRemitoCompra : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        RemitoCompra RemitoX;
        public CargarRemitoCompra(object x, ref Form y)
        {
            InitializeComponent();
            xUsuario = (Usuario)x;
            FormularioAnterior = y;
            RemitoX = new RemitoCompra();
            textBox4.Text =   RemitoX.Receptor = xUsuario.Nombre;
            textBox3.Text = RemitoX.FechaEmision = DateTime.Now.Date.ToShortDateString();
            LoadCombox();
        }

        // cargar base de  datos en los comboBoxs
        public void LoadCombox() {

            //cargar proveedores
            Proveedor.CargarComboBox(ref comboBox3);
            // cargar codigos barra y nombres de productos

            Producto.CargarComboBox(ref comboBox4);

            //cargar formas de pago

            Pago.CargarComboBox(ref comboBox2, "1.1.1");
            comboBox2.Items.Add(Pago.BuscarPorCodigo("2.1.1").Nombre);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { textBox1.Focus(); }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }
        }

        //agregar producto al remito
        private void button1_Click(object sender, EventArgs e)
        {
            Producto p = Producto.BuscarPorCodigo(comboBox4.Text);
            if (p==null) { p = Producto.BuscarPorNombre(comboBox4.Text); }
            if (p!=null) {
                try
                {
                    p.Cantidad = double.Parse(textBox2.Text);
                    p.Costo = double.Parse(textBox1.Text);

                    if (RemitoX.AgregarProducto(p))
                    {

                        RemitoX.MostrarDataGrid(ref dataGridView1);
                        label1.Text = "Total: $" + RemitoX.TotalCosto();
                        comboBox4.Text = textBox2.Text = textBox1.Text = "";
                        comboBox4.Focus();
                    }

                }
                catch (Exception) { }
            }

           
            
        }
        //seleccionar proveedor
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            RemitoX.Emisor = comboBox3.Text;
            comboBox2.Focus();
        }
        //seleccionar tipo de pago
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pago x = new Pago();
            x.Codigo =  Pago.BuscarPorNombre(comboBox2.Text).Codigo;
            x.Nombre = comboBox2.Text;
            RemitoX.Pagos.Add(x);
            button3.Focus();
            comboBox2.Enabled = false;
        }
        // eliminar producto
        private void button2_Click(object sender, EventArgs e)
        {

            try {
                int i = dataGridView1.CurrentRow.Index;

                RemitoX.EliminarProducto(dataGridView1.Rows[i].Cells[0].Value.ToString());
                RemitoX.MostrarDataGrid(ref dataGridView1);
                label1.Text = "Total: $" + RemitoX.TotalCosto();

            } catch (Exception) { }
        }
        // iniciar nuevo remito

        public void NuevoRemito() {
            RemitoX.ListaProdutos.Clear();
            RemitoX.MostrarDataGrid(ref dataGridView1);
            label1.Text = "Total: $0000";
            comboBox2.Text =
            comboBox3.Text =
            comboBox4.Text =
            textBox1.Text =
            textBox2.Text = "";
            comboBox2.Enabled = true;
        }
        //boton cancelar remito
        private void button4_Click(object sender, EventArgs e)
        {
            NuevoRemito();


        }
        // guardarRemito
        private void button3_Click(object sender, EventArgs e)
        {
            if (RemitoCompra.Crear(RemitoX)) {
                NuevoRemito();
            
            } else { }
        }

        private void CargarRemitoCompra_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }
    }
}
