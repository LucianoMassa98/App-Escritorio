using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace E_Shop
{
    public partial class Index : Form
    {
        Usuario Xusuario;
        Form y;
        public Index()
        {
            InitializeComponent();
            Usuario x = new Usuario();
            x.Tipo = 1;
            x.Nombre = "Generics";
            Xusuario = x;
            y = this;

            
        }
        // cargar venta
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            switch (Xusuario.Tipo.ToString()) {
                case "1": { new CargarRemitoVenta(Xusuario, ref y, new RemitoVenta()).Show(); break; }
                case "2": { break; }
                case "3": { new CargarRemitoEgreso(Xusuario, ref y, new RemitoEgreso()).Show(); break; }
            }

        }
        //cargar compra
        private void button2_Click(object sender, EventArgs e)
        {
           

            switch (Xusuario.Tipo.ToString())
            {
                case "1": { new CargarRemitoCompra(Xusuario, ref y, new RemitoCompra()).Show(); break; }
                case "2": { break; }
                case "3": { 
                        new CargarRemitoIngreso(Xusuario, ref y, new RemitoIngreso()).Show();
                        break; }
            }
        }
        // cargar cliente
        private void button6_Click(object sender, EventArgs e)
        {
            CargarCliente n = new CargarCliente(Xusuario, ref y);
            n.Show();
        }
        //cargar proveedores
        private void button7_Click(object sender, EventArgs e)
        {
            CargarProveedor n = new CargarProveedor(Xusuario, ref y);
            n.Show();
        }
        //carga pagos
        private void button3_Click(object sender, EventArgs e)
        {
            CargarPago n = new CargarPago(Xusuario, ref y);
            n.Show();
        }
        //cargarDescuento
        private void button4_Click(object sender, EventArgs e)
        {
            /* CargarDescuento n = new CargarDescuento(Xusuario, ref y);
             n.Show();*/
            new ConvertidorBD().Show();

        }
        //cargar usuario
        private void button8_Click(object sender, EventArgs e)
        {
            CargarUsuario n = new CargarUsuario(Xusuario, ref y);
            n.Show();
        }
        //consolidado ventas
        private void button5_Click(object sender, EventArgs e)
        {
            InformeVentas n = new InformeVentas();
            n.Show();
        }
        //lista de Ventas
        private void button9_Click(object sender, EventArgs e)
        {
            ListaVentas n = new ListaVentas(Xusuario);
           n.Show();
        }
        // lista compras
        private void button10_Click(object sender, EventArgs e)
        {
            ListaCompras n = new ListaCompras(Xusuario);
            n.Show();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            CargarProducto n = new CargarProducto(Xusuario, ref y);
            n.Show();
        }
        private void button12_Click(object sender, EventArgs e)
        {
             Usuario d = Usuario.BuscarPorNombre(textBox1.Text);
             Usuario c = Usuario.BuscarPorCodigo(textBox2.Text);
             if ((d!=null)&&(c != null)&&(d.Nombre == c.Nombre)) {

                    switch (c.Tipo) {
                        case 1: {
                            button2.Visible = button10.Visible = button19.Visible =
                               button3.Visible = button13.Visible = button7.Visible = button14.Visible = true;
                            panel1.Visible = panel4.Visible = panel5.Visible = true;
                                button3.Visible = button8.Visible = true;
                                break;
                            }
                        case 2:
                            {
                                panel1.Visible = panel4.Visible = panel5.Visible = true;
                            button2.Visible = button10.Visible = button19.Visible =
                                button3.Visible = button13.Visible = button7.Visible = button14.Visible = false;
                            button3.Visible = button8.Visible = false;

                                break;
                            }
                        case 3:
                        {
                            button9.Visible = button10.Visible = button19.Visible =
                                button3.Visible = button13.Visible = button7.Visible = button14.Visible = false;
                              panel1.Visible = panel4.Visible  = true;
                                break;
                            }


                    }


                    Xusuario = c;
                    textBox1.Enabled = textBox2.Enabled = false;
                    button12.Visible = false;
                    button15.Visible = true;

                }   
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { textBox2.Focus(); }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button12.Focus(); }
        }
        // abrir cobro clientes
        private void button13_Click(object sender, EventArgs e)
        {
            CobroCliente x = new CobroCliente(Xusuario,ref y);
            x.Show();

        }
        //abrir pago proveedores
        private void button14_Click(object sender, EventArgs e)
        {
            PagoProveedor x = new PagoProveedor(Xusuario, ref y);
            x.Show();
        }
        //cerrar sesion
        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = textBox2.Enabled = true;
            button12.Visible = true;
            button15.Visible = false;
            panel1.Visible = panel4.Visible = panel5.Visible = false;
            textBox1.Text = textBox2.Text = "";


        }

        private void button16_Click(object sender, EventArgs e)
        {
            new informeYemasDelSol().Show();

        }

        private void Index_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);

             if (DateTime.Now.DayOfWeek==DayOfWeek.Monday) {

                // cerear cuentas de gastos
                if (Pago.LeerBorrado()==false) { 
                    
                    Pago.CerearCuentas("3.3.3");
                    Pago.EscribirBorrado(true);
                }
                
            }
            else
            {
                // bandera de borrado false
                Pago.EscribirBorrado(false);
            }

            
          //  new Form1().Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
             new CargarRemitoRegistradora(Xusuario,ref y).Show();

        }
        //abrir carga produccion
        private void button18_Click(object sender, EventArgs e)
        {
            new CargarRemitoProduccion(Xusuario, ref y).Show();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            CargarRemitoRegistradora n = new CargarRemitoRegistradora(Xusuario, ref y);
            n.Show();
        }

        //generar tra
        private void button4_Click_1(object sender, EventArgs e)
        {
             string s=   AfipServices.generarTra();
             MessageBox.Show(s);
        }

        // enviar TraFirmado en base 64
        private void button5_Click_2(object sender, EventArgs e)
        {
            /*

        ServiceReferenceWSAA.LoginCMSChannel();
        ServiceReferenceWSAA.LoginCMSClient();
        ServiceReferenceWSAA.loginCmsRequest();
        ServiceReferenceWSAA.loginCmsResponse();
        ServiceReferenceWSAA.LoginFault();
        */
            try
            {
                StreamReader p = new StreamReader(new Direcciones().TraCms);
                ServiceReferenceWSAA.LoginCMSClient oCliente = new ServiceReferenceWSAA.LoginCMSClient();
                string res = oCliente.loginCms(p.ReadToEnd());
                p.Close(); p.Dispose();
                MessageBox.Show(res);



            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            new CargarGatos(Xusuario).Show();
            
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            new Capitales().Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            new ListaDevolucion(Xusuario).Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            new ListaIngresos(Xusuario).Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            new ListaEgresos(Xusuario).Show();
        }
    }
}
