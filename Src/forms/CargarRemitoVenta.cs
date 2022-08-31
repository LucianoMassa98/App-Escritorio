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
        public CargarRemitoVenta(object x, ref Form y)
        {
            InitializeComponent();
            xUsuario = (Usuario)x;
            FormularioAnterior = y;
            RemitoX = new RemitoVenta();
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
            comboBox4.Items.Add("bbb");

            //cargar formas de pago
            /*Pago.CargarComboBox(ref comboBox2,"1.1.1");
            comboBox2.Items.Add(Pago.BuscarPorCodigo("1.1.3").Nombre);*/



            //selecciona cliente x
            //  if (comboBox3.Items.Count > 0) { comboBox3.SelectedIndex = 0; }
            // seleccion Descuento nr1

            // selecciona tipo de pago 1
            // if (comboBox2.Items.Count>0) { comboBox2.SelectedIndex = 0;  }

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

                    switch (Cliente.BuscarPorNombre(RemitoX.Receptor).Tipo.ToString()) {
                        case "1":{ break; }
                        case "2": { p.Precio = p.Precio2; break; }
                        case "3": { p.Precio = p.Precio3; break; }
                    }    

                        p.Bulto = double.Parse(textBox1.Text);
                        p.Cantidad = double.Parse(textBox2.Text);
                    

                        if (RemitoX.AgregarProducto(p))
                        {

                            RemitoX.MostrarDataGrid(ref dataGridView1);
                            label1.Text = "Total: $" + RemitoX.TotalVenta();
                             comboBox4.Text = textBox2.Text = textBox1.Text="";
                            comboBox4.Focus();
                        }

                    }
                    catch (Exception) { }
                
            }
            

        }
       
        // iniciar nuevo remito
        public void NuevoRemito()
        {
           
            RemitoX.ListaProdutos.Clear();
            RemitoX.Receptor = "x";
            RemitoX.MostrarDataGrid(ref dataGridView1);
            label1.Text = "Total: $0000";
            comboBox4.Text =
            textBox2.Text = "";
            
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
                NuevoRemito();

            }
            else { new Alert("Error, no se pudo guardar tu venta!!").Show(); }
        }

        // -------------- EVENTOS -------------------
        // selecionar cliente
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemitoX.Receptor = comboBox3.Text;
            comboBox3.Enabled = false;
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
            NuevoRemito();
            comboBox3.Enabled = true;
        }
        private void CargarRemitoVenta_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
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
            switch (comboBox4.Text)
            {
                case "aaa": { break; }
                case "bbb": { break; }
                default: { textBox1.Focus(); break; }
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
            if (e.KeyChar == 13) { button1.Focus(); }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { textBox2.Focus(); }
        }
    }
}
