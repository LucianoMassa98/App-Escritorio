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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<RemitoCompra> compras = RemitoCompra.Buscar();


            foreach (RemitoCompra compra in compras)
            {
               
                switch (compra.Emisor) {
                    case "Primo": { Agregar(ref listBox1, compra); break; }
                    case "Bustos y Beltran": { Agregar(ref listBox2, compra); break; }
                    case "General Pico": { Agregar(ref listBox3, compra); break; }
                    case "GOCHICOA": { Agregar(ref listBox4, compra); break; }
                    case "PLATERO ": { Agregar(ref listBox5, compra); break; }
                    default: { MessageBox.Show("otro na que ver"); break; }
                }
            }
            label1.Text = listBox1.Items.Count.ToString();
            label2.Text = listBox2.Items.Count.ToString();
            label3.Text = listBox3.Items.Count.ToString();
            label4.Text = listBox4.Items.Count.ToString();
            label5.Text = listBox5.Items.Count.ToString();
        }

        public void Agregar(ref ListBox x, object y)
        {
            RemitoCompra compra = (RemitoCompra)y;
            
            foreach (Producto prd in compra.ListaProdutos) {
                
                if (x.Items.Contains(prd.Nombre)==false) { x.Items.Add(prd.Nombre); }
            }

        }

    }
}
