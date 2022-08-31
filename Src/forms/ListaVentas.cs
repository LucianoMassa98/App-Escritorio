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
    public partial class ListaVentas : Form
    {
        List<RemitoVenta> RemitosX;
        int indice;
        public ListaVentas()
        {
            InitializeComponent();
            RemitosX = new List<RemitoVenta>();
            indice = 0;
            LoadComboBox();
        }

        public void LoadComboBox()
        {
            comboBox3.Items.Clear();
            List<Cliente> lista = Cliente.Buscar();
            foreach (Cliente cliente in lista)
            {
                comboBox3.Items.Add(cliente.Nombre);
            }
        }
        //cargar ventas
        private void button1_Click(object sender, EventArgs e)
        {
            string fechaA = dateTimePicker1.Value.Date.ToShortDateString();
            Cliente cli = Cliente.BuscarPorNombre(comboBox3.Text);

            if (cli!=null) {
                RemitosX = RemitoVenta.BuscarPorFecha(fechaA, fechaA,cli);
                if (RemitosX.Count() > 0)
                {
                    RemitosX[0].MostrarDataGrid(ref dataGridView1);
                    indice = 0;
                    MostraRemito();
                }
                else { dataGridView1.Rows.Add("No Hay Productos"); }
                dateTimePicker1.Enabled = false;
            }
            else
            {
                MessageBox.Show("El cliente no existe!!");
            }
            

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
                textBox5.Text = RemitosX[indice].Receptor;
                textBox6.Text = RemitosX[indice].Pagos[0].Nombre;
                textBox7.Text = RemitosX[indice].TotalVenta().ToString();
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {
               
                CrearPdf n = new CrearPdf();
                n.GenerarPdfRemitoVenta(RemitosX[indice]);
                


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
            if  (RemitosX.Count() > 0)
            {
                if (RemitoVenta.Borrar(RemitosX[indice].Codigo))
                {
                    MessageBox.Show(
                        "Remito Borrado",
                        "Exito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                        );
                }
                else {
                    MessageBox.Show(
                        "No se pudo realizar la operación",
                        "Falló",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        ); 
                    }
                this.Close();
                    
            }
        }
        //eliminar producto x del remito y
        private void button7_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {
              
                string codProdu = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                double cantidad = double.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString());
              double importe = double.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString());
                RemitosX[indice].EliminarProducto(codProdu);
                if (RemitoVenta.Guardar(RemitosX)) {
                    RemitosX[indice].MostrarDataGrid(ref dataGridView1);
                    MostraRemito();

                    // sumar producto al stock y restar importe de la caja
                    /*
                    Producto.SumarStock(RemitosX[indice].ListaProdutos);
                    Pago.RestarCuenta(RemitosX[indice].Pagos);
                    if (RemitosX[indice].Pagos[0].Codigo == "1.1.3")
                    {
                        Cliente.RestarSaldo(
                        Cliente.BuscarPorNombre(RemitosX[indice].Receptor).Codigo,
                        RemitosX[indice].TotalVenta()
                                           );
                    }
                    */
                }

            }
        }

        private void ListaVentas_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }
    }
}
