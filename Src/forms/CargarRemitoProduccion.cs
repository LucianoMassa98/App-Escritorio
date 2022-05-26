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
    public partial class CargarRemitoProduccion : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        RemitoProduccion RemitoX;
        int inicio;
        public CargarRemitoProduccion(object x, ref Form y)
        {
            InitializeComponent();
            xUsuario = (Usuario)x;
            FormularioAnterior = y;
            RemitoX = new RemitoProduccion();
            textBox4.Text = RemitoX.Receptor = xUsuario.Nombre;
            textBox3.Text = RemitoX.FechaEmision = DateTime.Now.Date.ToShortDateString();
            inicio = 0;
            LoadCombox();
        }
        public void LoadCombox()
        {
            // cargr codigos barra y nombres de productos
            Producto.CargarComboBox(ref comboBox1, ref comboBox4,"Huevo");
           
            // cargar lista de choferes

        }
        public void NuevoRemito()
        {
            RemitoX.ListaProdutos.Clear();
            RemitoX.Emisor= null;
            RemitoX.MostrarDataGrid(ref dataGridView1);
            label1.Text = "Total: $0000";
            comboBox1.Text =
            comboBox4.Text =
            textBox2.Text = "";


        }
        public void ElimnarProducto()
        {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                RemitoX.EliminarProducto(dataGridView1.Rows[i].Cells[0].Value.ToString());
                RemitoX.MostrarDataGrid(ref dataGridView1);
                label1.Text = "Total: $" + RemitoX.TotalCosto();

            }
            catch (Exception) { }
        }
        public void AgregarProducto()
        {
            Producto p = new Producto();
            p.Codigo = comboBox1.Text;
            p.Nombre = comboBox4.Text;
           
                try
                {
                    p.Cantidad = double.Parse(textBox2.Text);
                    if (RemitoX.AgregarProducto(p))
                    {
                        RemitoX.MostrarDataGrid(ref dataGridView1);
                        label1.Text = "Total: $" + RemitoX.TotalCosto();
                        if (comboBox1.Text != "") { comboBox1.Focus(); }
                        else { comboBox4.Focus(); }
                        comboBox1.Text = comboBox4.Text = textBox2.Text = "";
                    }
                }
                catch (Exception) { }
            

        }
        public void GuardarRemito()
        {
            if (RemitoProduccion.Crear(RemitoX))
            {
                NuevoRemito();
                comboBox1.Focus();
            }
            else { new Alert("Error, no se pudo guardar tu venta!!").Show(); }
        }
        private void CargarRemitoProduccion_Load(object sender, EventArgs e)
        {
            Direcciones dir = new Direcciones();
            panel3.BackgroundImage = Image.FromFile(dir.Logo);
        }
        
        //eliminar producto
        private void button2_Click(object sender, EventArgs e)
        {
            ElimnarProducto();
        }
        //agregar producto
        private void button1_Click(object sender, EventArgs e)
        {
            AgregarProducto();
        }
        //nuevo remito
        private void button4_Click(object sender, EventArgs e)
        {
            NuevoRemito();
        }
        //guardar remito
        private void button3_Click(object sender, EventArgs e)
        {
            GuardarRemito();
        }
        // seleccionar chofer
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemitoX.Emisor = comboBox3.Text;
            

        }
        // busca producto por codigo
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }
        // coloca cantidad y presiona enter
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { AgregarProducto(); }
        }
        // busca producto por nombre
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }
    }
}
