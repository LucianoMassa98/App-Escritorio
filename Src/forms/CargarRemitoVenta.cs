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
    public partial class CargarRemitoVenta : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        RemitoVenta RemitoX;
        int inicio;
        public CargarRemitoVenta(object x, ref Form y,  object z)
        {
            InitializeComponent();
            xUsuario = (Usuario)x;
            FormularioAnterior = y;
            RemitoX = (RemitoVenta)z;
            textBox4.Text = RemitoX.Emisor = xUsuario.Nombre;
            textBox3.Text = RemitoX.FechaEmision = DateTime.Now.Date.ToShortDateString();
            inicio = 0;
            LoadCombox();
        }
        
        public void LoadCombox()
        {
            
            //cargar Clientes
            Cliente.CargarComboBox(ref comboBox3);
            // cargr codigos barra y nombres de productos
            Producto.CargarComboBox( ref comboBox4);
            comboBox4.Items.Add("aaa");
            comboBox3.Focus();
      
        }

        public void LoadRemitoX()
        {
            textBox3.Text = RemitoX.FechaEmision;
            textBox4.Text = RemitoX.Emisor;
            comboBox3.Text = RemitoX.Receptor;
            comboBox3.Enabled = false;
            button4.Visible = false;
            button5.Visible = true;
            RemitoX.MostrarDataGrid(ref dataGridView1);
            label1.Text = "Total: $" + RemitoX.TotalVenta();

        }
        // agregar Producto
        public void AgregarProducto() {
            Producto p = Producto.BuscarPorCodigo(comboBox4.Text);

            if (p == null)
            {
                p = Producto.BuscarPorNombre(comboBox4.Text);
            }

            if (p != null) {
                //buscar producto por cliente
                
                
                    try
                    {

                        

                        p.Precio = double.Parse(textBox5.Text);

                       if (p.Costo>=p.Precio) {

                        MessageBox.Show("Cuidado, el precio esta por de bajo o igual al costo.");
                    }

                        p.Bulto = double.Parse(textBox1.Text);
                        p.Cantidad = double.Parse(textBox2.Text);
                    

                        if (RemitoX.AgregarProducto(p))
                        {

                            RemitoX.MostrarDataGrid(ref dataGridView1);
                            label1.Text = "Total: $" + RemitoX.TotalVenta();
                            textBox5.Text=  comboBox4.Text = textBox2.Text = textBox1.Text="";
                        checkBox1.Checked = false;
                            comboBox4.Focus();
                        }

                    }
                    catch (Exception) { }
                
            }
            

        }
       
        // iniciar nuevo remito
        public void nuevoRemito(){      
            try {
                RemitoX.Codigo = "";
                RemitoX.ListaProdutos.Clear();
                comboBox3.Enabled = true;
                RemitoX.MostrarDataGrid(ref dataGridView1);
                label1.Text = "Total: $0000";
                checkBox1.Checked = false;
                comboBox3.Text =
                comboBox4.Text =
                textBox2.Text = "";
                comboBox3.Focus();
            }catch (Exception err) { MessageBox.Show(err.Message); }
            
        }

        //fin cargar
        public void FinCarga() {
            if (button5.Visible==true) {

                Form k = this;
                k.Close();
            }
        
        }
        
        //crear remito de venta
        public void CrearRemitoVenta() {
            //buscarpagoycolocarimporte
            if (RemitoVenta.Crear(RemitoX))
            {
                try
                {
                   // RemitoX.Imprimir();
                }
                catch (Exception) { new Alert("Error al imprimir ticket!!").Show(); }
                nuevoRemito();

            }
            else { new Alert("Error, no se pudo guardar tu venta!!").Show(); }
        }

        // -------------- EVENTOS -------------------
        // selecionar cliente
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemitoX.Receptor = comboBox3.Text;
            comboBox3.Enabled = false;
            comboBox4.Enabled = true;
            comboBox4.Focus();
        }
        // crear remito
        private void button3_Click(object sender, EventArgs e)
        {
            Form k = this;
            if ((comboBox3.Enabled == false) && (dataGridView1.RowCount > 0)) { new seleccionarCobro(RemitoX, ref k).Show(); }

        }
        //eliminar remito
        private void button4_Click(object sender, EventArgs e)
        {
            nuevoRemito();
            comboBox3.Enabled = true;
            comboBox4.Enabled = false;
        }
        private void CargarRemitoVenta_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
            checkBox1.Checked = false;
        }
        // eliminar producto
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                
                int i = dataGridView1.CurrentRow.Index;

                RemitoX.EliminarProducto(dataGridView1.Rows[i].Cells[0].Value.ToString());
                RemitoX.MostrarDataGrid(ref dataGridView1);
                label1.Text = "Total: $" + RemitoX.TotalCosto();

               
                // selecciona tipo de pago 1
                // if (comboBox2.Items.Count > 0) { comboBox2.SelectedIndex = 0; }

            }
            catch (Exception) { }
        }
        //agregar producto
        private void button1_Click(object sender, EventArgs e)
        {
            AgregarProducto();
        }
        // buscar producto por nombre
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Enabled == true)
            {
                MessageBox.Show("Es necesario colocar cliente para empezar a cargar productos");
            }
            else
            {
                switch (comboBox4.Text)
                {
                    case "aaa": { button3.Focus(); break; }
                    case "bbb": { break; }
                    default:
                        {

                            Cliente c = Cliente.BuscarPorNombre(comboBox3.Text);
                            if (c != null)
                            {

                                switch (c.Tipo.ToString())
                                {
                                    case "1":
                                        {
                                            Producto p = Producto.BuscarPorNombre(comboBox4.Text);
                                            if (p == null) { p = Producto.BuscarPorCodigo(comboBox4.Text); }

                                            textBox5.Text = p.Precio.ToString();
                                            break;
                                        }
                                    case "2":
                                        {
                                            Producto p = Producto.BuscarPorNombre(comboBox4.Text);
                                            if (p == null) { p = Producto.BuscarPorCodigo(comboBox4.Text); }

                                            textBox5.Text = p.Precio2.ToString();
                                            break;
                                        }
                                    case "3":
                                        {
                                            Producto p = Producto.BuscarPorNombre(comboBox4.Text);
                                            if (p == null) { p = Producto.BuscarPorCodigo(comboBox4.Text); }

                                            textBox5.Text = p.Precio3.ToString();
                                            break;
                                        }
                                }

                            }




                            textBox1.Focus(); break;
                        }
                }
            }
            }
        
        // selecciona descuento
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            AgregarProducto();
        }
       
        // seleciona cantidad
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) {

                if (checkBox1.Checked==true) {
                    textBox5.Focus();
                }
                else
                {
                    button1.Focus();
                }
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { textBox2.Focus(); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form k = this;
            k.Close();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = checkBox1.Checked;
        }
    }
}
