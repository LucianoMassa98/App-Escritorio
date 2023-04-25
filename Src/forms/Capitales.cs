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
    public partial class Capitales : Form
    {
        List<Pago> cuentaActivos,cuentaPasivos;
        // modificar cap
        private void buttonPersonalizado1_Click(object sender, EventArgs e)
        {
            Capitales k = this;
            double sumActivo = Pago.SumarImportes(cuentaActivos);
            double sumPasivo = Pago.SumarImportes(cuentaPasivos);
            double pn = sumActivo + sumPasivo;
            new modCapital(ref k, pn).Show();
        }

        public Capitales()
        {
            InitializeComponent();

            loadData();
        }

        public void  loadData() {
            cuentaActivos = Pago.Buscar("1");
            cuentaPasivos = Pago.Buscar("2");

            double sumActivo = Pago.SumarImportes(cuentaActivos);
            double sumPasivo = Pago.SumarImportes(cuentaPasivos);


            double total = sumActivo + sumPasivo;
            label1.Text = "Patrimonio Neto: $" + total;
            label2.Text = "Activos: $" + sumActivo;
            label3.Text = "Pasivos: $" + sumPasivo;

            double por1 = Pago.BuscarPorCodigo("5.1").Importe;
            label5.Text = "Ruly: %"+ por1;
            label10.Text = "$"+((por1*total)/100);
            double por2 = Pago.BuscarPorCodigo("5.2").Importe;
            label6.Text = "Marco: %" + por2;
            label9.Text = "$" + ((por2 * total) / 100);
            double por3 = Pago.BuscarPorCodigo("5.3").Importe;
            label7.Text = "Gonzalo: %" + por3;
            label8.Text = "$" + ((por3 * total) / 100);



        }

    }
}
