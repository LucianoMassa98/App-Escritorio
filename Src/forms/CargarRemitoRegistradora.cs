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
    public partial class CargarRemitoRegistradora : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        RemitoRegistradora RemitoX;
        int inicio;
        public CargarRemitoRegistradora(object x, ref Form y)
        {
            InitializeComponent();
            xUsuario = (Usuario)x;
            FormularioAnterior = y;
            RemitoX = new RemitoRegistradora();
            textBox4.Text = RemitoX.Emisor = xUsuario.Nombre;
            textBox3.Text = RemitoX.FechaEmision = DateTime.Now.Date.ToShortDateString();
            inicio = 0;
            LoadCombox();
        }
       //seleccionar por codigo
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }
        //seleccionar por nombre
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }
        //cantidad
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { textBox1.Focus(); }
        }
        //precio
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button1.Focus(); }
        }
        //cancelar remito
        private void button4_Click(object sender, EventArgs e)
        {
            NuevoRemito();
        }
       //eliminar producto
        private void button2_Click(object sender, EventArgs e)
        {
            ElimnarProducto();
        }
        // agregar producto
        private void button1_Click(object sender, EventArgs e)
        {
            AgregarProducto();
        }
        //guardar remito
        private void button3_Click(object sender, EventArgs e)
        {
            GuardarRemito();
        }
        private void CargarRemitoRegistradora_Load(object sender, EventArgs e)
        {
            Direcciones dir = new Direcciones();
            panel3.BackgroundImage = Image.FromFile(dir.Logo);
        }
        public void LoadCombox()
        { 
            // cargr codigos barra y nombres de productos
            Producto.CargarComboBox( ref comboBox4,"Huevo");
            //comboBox1.Items.Add("aaa");

        }
        public void NuevoRemito()
        {
            RemitoX.ListaProdutos.Clear();
            RemitoX.MostrarDataGrid(ref dataGridView1);
            label1.Text = "Total: $0000";
            comboBox4.Text =
            textBox2.Text =
            textBox1.Text="";
            

        }
        public void ElimnarProducto() {
            try
            {
                int i = dataGridView1.CurrentRow.Index;
                RemitoX.EliminarProducto(dataGridView1.Rows[i].Cells[0].Value.ToString());
                RemitoX.MostrarDataGrid(ref dataGridView1);
                label1.Text = "Total: $" + RemitoX.Total();

            }
            catch (Exception) { }
        }
        public void AgregarProducto() {
            Producto p =Producto.BuscarPorCodigo(comboBox4.Text);
            if (p == null) { p = Producto.BuscarPorNombre(comboBox4.Text); }
            if (p!=null) {
                try
                {
                    p.Cantidad = double.Parse(textBox2.Text);
                    p.Precio = double.Parse(textBox1.Text);
                    if (RemitoX.AgregarProducto(p))
                    {
                        RemitoX.MostrarDataGrid(ref dataGridView1);
                        label1.Text = "Total: $" + RemitoX.Total();
                        comboBox4.Focus(); 
                        comboBox4.Text = textBox2.Text = textBox1.Text = "";
                    }
                    else { }
                }
                catch (Exception) { }
            }
               
            
                    
        }
        public void GuardarRemito() {

            RemitoX.Xpago= Pago.BuscarPorNombre("Efectivo");

            if (RemitoRegistradora.Crear(RemitoX))
            {
                NuevoRemito();
                comboBox4.Focus();
            }
            else { new Alert("Error, no se pudo guardar tu venta!!").Show(); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
