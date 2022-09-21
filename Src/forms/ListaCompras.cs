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
    public partial class ListaCompras : Form
    {
        Usuario User;
        List<RemitoCompra> RemitosX;
        int indice;
        public ListaCompras(object user)
        {
            InitializeComponent();
            User = (Usuario)user;
            RemitosX = new List<RemitoCompra>();
            indice = 0;
            LoadComboBox();
        }
        public void BorraRemito()
        {
            if (RemitosX.Count() > 0)
            {
                if (RemitoCompra.Borrar(RemitosX[indice].Codigo))
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
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                }
                this.Close();

            }
        }
        public void LoadComboBox()
        {
            List<Proveedor> lista = Proveedor.Buscar();
            comboBox3.Items.Clear();
            foreach(Proveedor prv in lista)
            {
                comboBox3.Items.Add(prv.Nombre);
            }

        }
        //Buscar remitos
        private void button1_Click(object sender, EventArgs e)
        {

            string fechaA = dateTimePicker1.Value.Date.ToShortDateString();
            Proveedor prv = Proveedor.BuscarPorNombre(comboBox3.Text);
            if (prv!=null) {
                RemitosX = RemitoCompra.BuscarPorFecha(fechaA, fechaA,prv);
                if (RemitosX.Count() > 0)
                {
                    RemitosX[0].MostrarDataGrid(ref dataGridView1);
                    indice = 0;
                    MostraRemito();
                }
                else { dataGridView1.Rows.Add("No Hay Productos"); }
                dateTimePicker1.Enabled = false;
                comboBox3.Enabled = false;
            }
        }
        // siguiente compra
        private void button2_Click(object sender, EventArgs e)
        {
            if ((RemitosX.Count()>0)&&((indice+1<RemitosX.Count()))) {
                indice++;
                RemitosX[indice].MostrarDataGrid(ref dataGridView1);
                MostraRemito();

            }
        }
        // compra anterior
        private void button3_Click(object sender, EventArgs e)
        {
            if ((RemitosX.Count() > 0) && ((indice - 1) > -1))
            {
                indice--;
                RemitosX[indice].MostrarDataGrid(ref dataGridView1);
                MostraRemito();

            }
        }


        //Mostrar Datos por Remito
        public void MostraRemito() {

            if (RemitosX.Count() > 0)
            {
                textBox1.Text = RemitosX.Count().ToString();
                textBox2.Text = (indice+1).ToString();
                textBox10.Text = RemitosX[indice].Codigo;
                textBox5.Text = RemitosX[indice].Emisor;
                listBox1.Items.Clear();
                foreach (Pago p in RemitosX[indice].Pagos) {
                    listBox1.Items.Add(p.Nombre);
                    listBox1.Items.Add(p.Importe);
                    listBox1.Items.Add("--------");
                }
                textBox7.Text = RemitosX[indice].TotalCosto().ToString();
            }

        
        }

        private void ListaCompras_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        // click borrar
        private void button6_Click_1(object sender, EventArgs e)
        {
            BorraRemito();
        }
        // imprimir remito
        private void button5_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {
                RemitosX[indice].Imprimir();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {
                CrearPdf n = new CrearPdf();
                //n.GenerarPdfRemitoVenta(RemitosX[indice]);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (RemitosX.Count() > 0)
            {

                Form k = this;
                CargarRemitoCompra y = new CargarRemitoCompra(User, ref k, RemitosX[indice]);
                y.Show();
                y.LoadRemitoX();
                k.Visible = false;

            }
        }
    }
}
