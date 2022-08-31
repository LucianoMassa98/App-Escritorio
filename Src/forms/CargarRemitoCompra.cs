﻿using System;
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
            comboBox4.Items.Add("aaa");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "aaa") { button3.Focus(); } else {
                Producto n = Producto.BuscarPorNombre(comboBox4.Text);
                if (n==null) { n = Producto.BuscarPorCodigo(comboBox4.Text); }

                textBox1.Text = n.Costo.ToString();
                textBox5.Focus(); }
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
                    p.Bulto = double.Parse(textBox5.Text);
                    p.Cantidad = double.Parse(textBox2.Text);
                    p.Costo = double.Parse(textBox1.Text);

                    if (RemitoX.AgregarProducto(p))
                    {

                        RemitoX.MostrarDataGrid(ref dataGridView1);
                        label1.Text = "Total: $" + RemitoX.TotalCosto();
                        comboBox4.Text = textBox2.Text = textBox1.Text = textBox5.Text="";
                        comboBox4.Focus();
                    }

                }
                catch (Exception) { }
            }

           
            
        }
        //seleccionar proveedor
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = false;
            RemitoX.Emisor = comboBox3.Text;
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
            comboBox3.Enabled = true;
            comboBox3.Focus();
            comboBox3.Text=
            comboBox4.Text =
            textBox1.Text =
            textBox2.Text = "";
        }
        //boton cancelar remito
        private void button4_Click(object sender, EventArgs e)
        {
            NuevoRemito();


        }
        // guardarRemito
        private void button3_Click(object sender, EventArgs e)
        {
            Form k = this;
            if ((comboBox3.Enabled == false)&&(dataGridView1.RowCount>0)) { new seleccionaPago(RemitoX, ref k).Show(); }
        }

        private void CargarRemitoCompra_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { textBox2.Focus(); }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
