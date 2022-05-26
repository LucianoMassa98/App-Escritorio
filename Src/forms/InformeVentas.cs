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
    public partial class InformeVentas : Form
    {
        public InformeVentas()
        {
            InitializeComponent();
        }

        private void InformeVentas_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }

        // buscar Ventas
        private void button1_Click(object sender, EventArgs e)
        {
            string fechaA = dateTimePicker1.Value.Date.ToShortDateString();
            string fechaB = dateTimePicker2.Value.Date.ToShortDateString();

          List<RemitoVenta> Remitos =  RemitoVenta.BuscarPorFecha(fechaA,fechaB);
          RemitoVenta.ConsolidarMostrar(Remitos, ref dataGridView1,ref listBox1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
