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
        List<RemitoCompra> RemitosX;
        int indice;
        public ListaCompras()
        {
            InitializeComponent();
            RemitosX = new List<RemitoCompra>();
            indice = 0; 
        }

        //Buscar remitos
        private void button1_Click(object sender, EventArgs e)
        {

            string fechaA = dateTimePicker1.Value.Date.ToShortDateString();

            RemitosX = RemitoCompra.BuscarPorFecha(fechaA, fechaA);
            if (RemitosX.Count() > 0)
            {
                RemitosX[0].MostrarDataGrid(ref dataGridView1);
                indice = 0;
                MostraRemito();
            }
            else { dataGridView1.Rows.Add("No Hay Productos"); }
            dateTimePicker1.Enabled = false;
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
                textBox6.Text = RemitosX[indice].Pagos[0].Nombre;
                textBox7.Text = RemitosX[indice].TotalCosto().ToString();
            }

        
        }

        private void ListaCompras_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }
    }
}
