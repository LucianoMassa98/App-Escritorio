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
    public partial class modCapital : Form
    {
        double pn;
        Capitales anterior;
        public modCapital(ref Capitales anterior, object pn)
        {
            InitializeComponent();
            this.anterior = anterior;
            this.pn = (double)pn;
            loadData();
        }

        public void loadData() {

            Pago.CargarComboBox2(ref comboBox1, "1.1.1");
            Pago.CargarComboBox2(ref comboBox2, "1.1.1");
            Pago.CargarComboBox2(ref comboBox1, "5");
            Pago.CargarComboBox2(ref comboBox2, "5");
        }

        //cancelar
        private void button4_Click(object sender, EventArgs e)
        {

        }
        //guardar
        private void button3_Click(object sender, EventArgs e)
        {
            Pago desde = Pago.BuscarPorNombre(comboBox1.Text);
            if (desde!=null) {

                Pago hasta = Pago.BuscarPorNombre(comboBox2.Text);
                if (hasta!=null) {

                    if (desde.tipo()!=hasta.tipo()) {

                        if (desde.tipo()=="1") {
                            // restar cuenta desde y restar % cuenta hasta 

                            try {
                                double imp = double.Parse(textBox9.Text);
                                pn -= imp;
                                desde.Importe = imp;

                                hasta.Importe = (imp * 100) / pn;
                                Pago.RestarCuenta(desde);
                                Pago.RestarCuenta(hasta);
                                // sumar capitales restantes

                                double sumotrocap = hasta.Importe / 2;

                                Pago.recap(sumotrocap, desde.Codigo,true);

                                Form k = this;
                                anterior.loadData();
                                k.Close();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error en la carga del importe");
                            }

                            
                        }
                        else
                        {
                            // sumar cuenta desde y sumar % cuenta hasta 
                            try
                            {
                                
                                double imp = double.Parse(textBox9.Text);
                                pn += imp;
                                desde.Importe = (imp * 100) / pn;

                                hasta.Importe = imp;
                                Pago.SumarCuenta(desde);
                                Pago.SumarCuenta(hasta);

                                // restar capitales restantes

                                double sumotrocap = desde.Importe / 2;

                                Pago.recap(sumotrocap, desde.Codigo, false);

                                Form k = this;
                                anterior.loadData();
                                k.Close();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error en la carga del importe");
                            }
                           
                        }

                    }



                }
            
            }
        }
        //seleccionar desde
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //seleccionar hasta
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
