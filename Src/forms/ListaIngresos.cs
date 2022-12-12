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
    public partial class ListaIngresos : Form
    {
        Usuario User;
        List<RemitoIngreso> RemitosX;
        int indice;
        public ListaIngresos(object user)
        {
            InitializeComponent();
            User = (Usuario)user;
            RemitosX = new List<RemitoIngreso>();
            indice = 0;

            if (User.Tipo != 1)
            {
                button4.Visible = false;
            }
            else if (User.Tipo == 1)
            {
                button6.Visible = button7.Visible = false;
            }

        }

        
        public void BorraRemito()
        {
            if (RemitosX.Count() > 0)
            {
                if (RemitoIngreso.Borrar(RemitosX[indice].Codigo))
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
            string fechaA = dateTimePicker2.Value.Date.ToShortDateString();
            string fechaB = dateTimePicker1.Value.Date.ToShortDateString();
                RemitosX = RemitoIngreso.BuscarPorFecha(fechaA, fechaB);
                if (RemitosX.Count() > 0)
                {
                    RemitosX[0].MostrarDataGrid(ref dataGridView1);
                    indice = 0;
                    MostraRemito();
                }
                else { dataGridView1.Rows.Add("No Hay Productos"); }
               
               
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
                if (RemitosX[indice].Directo) {
                    label4.Text = "Galpon";
                }
                else
                {
                    label4.Text = "Reparto";
                }
                
             


                textBox7.Text = RemitosX[indice].TotalCosto().ToString();
            }


        }
        //  Venta
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
        //eliminar producto x del remito y
        private void button7_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {
                if (textBox3.Text == User.Nombre) {
                    Form k = this;
                    CargarRemitoIngreso y = new CargarRemitoIngreso(User, ref k, RemitosX[indice]);
                    y.Show();
                    y.LoadRemitoX();
                    k.Visible = false;

                } else
                {
                    if (User.Tipo!=1) {
                        MessageBox.Show("No puede modificar ventas cargadas por otro empleado");
                    }
                    else
                    {
                        Form k = this;
                        CargarRemitoIngreso y = new CargarRemitoIngreso(User, ref k, RemitosX[indice]);
                        y.Show();
                        y.LoadRemitoX();
                        k.Visible = false;
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
        {
            if (RemitosX.Count() > 0)
            {
                if (!RemitosX[indice].EnviarFacturaAfip()) {
                    MessageBox.Show("Error al Cargar");
                }
            }
        }
        // realizar cobro
        private void button4_Click_1(object sender, EventArgs e)
        {
            Form k = this;

                if (RemitosX.Count>0) {
                new seleccionarPagoIngreso(RemitosX[indice], ref k).Show();
            }
            else
            {
                MessageBox.Show("No hay remitos seleccionados");
            }
            
        }
    }
}
