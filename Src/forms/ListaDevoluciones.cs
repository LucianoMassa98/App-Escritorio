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
    public partial class ListaDevolucion: Form
    {
        Usuario User;
        List<RemitoDevolucion> RemitosX;
        int indice;
        public ListaDevolucion(object user)
        {
            InitializeComponent();
            User = (Usuario)user;
            RemitosX = new List<RemitoDevolucion>();
            indice = 0;

            if (User.Tipo != 1)
            {
                button6.Visible = false;
            }
        }

      
        public void BorraRemito()
        {
            if (RemitosX.Count() > 0)
            {
                if (RemitoDevolucion.Borrar(RemitosX[indice].Codigo))
                {
                    MessageBox.Show(
                        "Remito Borrado",
                        "Exito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo realizar la operación",
                        "Falló",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning
                        );
                }
                this.Close();

            }
        } 
        //buscar ventas
        public void buscarBoletas()
        {
            string fechaA = dateTimePicker1.Value.Date.ToShortDateString();
            
                RemitosX = RemitoDevolucion.BuscarPorFecha(fechaA, fechaA);
                if (RemitosX.Count() > 0)
                {
                    RemitosX[0].MostrarDataGrid(ref dataGridView1);
                    indice = 0;
                    MostraRemito();
                }
                else { dataGridView1.Rows.Add("No hay resultados"); }
                dateTimePicker1.Enabled = false;
             
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            buscarBoletas();
            

        }
        //anterior venta
        private void button3_Click(object sender, EventArgs e)
        {
            if ((RemitosX.Count() > 0) && ((indice - 1) > -1))
            {
                indice--;
                RemitosX[indice].MostrarDataGrid(ref dataGridView1);
                MostraRemito();

            }
        }
        //siguiente venta
        private void button2_Click(object sender, EventArgs e)
        {
            if ((RemitosX.Count() > 0) && ((indice + 1 < RemitosX.Count())))
            {
                indice++;
                RemitosX[indice].MostrarDataGrid(ref dataGridView1);
                MostraRemito();

            }
        }
        //muestra datos de remito
        public void MostraRemito()
        {

            if (RemitosX.Count() > 0)
            {
                textBox1.Text = RemitosX.Count().ToString();
                textBox2.Text = (indice + 1).ToString();
                textBox10.Text = RemitosX[indice].Codigo;
                textBox3.Text = RemitosX[indice].Emisor;
                textBox5.Text = RemitosX[indice].Receptor;
                listBox1.Items.Clear();
                foreach(Pago p in RemitosX[indice].Pagos)
                {
                    listBox1.Items.Add(p.Nombre);
                    listBox1.Items.Add("$"+p.Importe);
                    listBox1.Items.Add("-----------");
                }


                textBox7.Text = RemitosX[indice].TotalVenta().ToString();
            }


        }
        // Editar Venta
        private void button4_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {
              
                CrearPdf n = new CrearPdf();
               // n.GenerarPdfRemitoVenta(RemitosX[indice]);
                


            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {
                RemitosX[indice].Imprimir();
            }
        }
        //borrar Remito De Venta
        private void button6_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {
                if (textBox3.Text == User.Nombre)
                {
                    BorraRemito();
                }
                else
                {
                    if (User.Tipo != 1)
                    {
                        MessageBox.Show("No puede modificar ventas cargadas por otro empleado");
                    }
                    else
                    {
                        BorraRemito();
                    }

                }



            }
        }
     
        private void ListaVentas_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        // enviar factura afip
        private void button8_Click(object sender, EventArgs e)
        {/* if (RemitosX.Count() > 0)
            {
                if (!RemitosX[indice].EnviarFacturaAfip()) {
                    MessageBox.Show("Error al Cargar");
                }
            }*/

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  buscarBoletas(comboBox3.Text);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // devolver
        private void button9_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {

                RemitoDevolucion newRemito = new RemitoDevolucion();
                newRemito.FechaEmision = DateTime.Now.ToString("dd/MM/yyyy");
                newRemito.Receptor = RemitosX[indice].Receptor;
                newRemito.Emisor = RemitosX[indice].Emisor;
                newRemito.Receptor = RemitosX[indice].Receptor;
                newRemito.Pagos = RemitosX[indice].Pagos;
                newRemito.ListaProdutos = RemitosX[indice].ListaProdutos;
                newRemito.Directo = RemitosX[indice].Directo;
                if (textBox3.Text == User.Nombre)
                {
                    if (RemitoDevolucion.Crear(newRemito)) {
                        BorraRemito();
                    }
                    else { MessageBox.Show("No se pudo crear devolucion"); }
                    
                }
                else
                {
                    if (User.Tipo == 1)
                    {
                        if (RemitoDevolucion.Crear(newRemito))
                        {
                            BorraRemito();
                        }
                        else { MessageBox.Show("No se pudo crear devolucion"); }
                    }
                    else
                    {
                        MessageBox.Show("No puede modificar ventas cargadas por otro empleado");

                    }

                }



            }
        }
    }
}
