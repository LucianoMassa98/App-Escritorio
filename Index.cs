using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace E_Shop
{
    public partial class Index : Form
    {
        Usuario Xusuario;
        Form y,activeForm;
        public Index()
        {
            InitializeComponent();
            Usuario x = new Usuario();
            x.Tipo = 1;
            x.Nombre = "Generics";
            Xusuario = x;
            y = this;

            
        }

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel4.Controls.Add(childForm);
            panel4.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        // cargar venta
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            switch (Xusuario.Tipo.ToString()) {
                case "1": {
                        openChildForm(new CargarRemitoVenta(Xusuario, ref y, new RemitoVenta()));
                        break; }
                case "2": { break; }
                case "3": {
                        openChildForm(new CargarRemitoEgreso(Xusuario, ref y, new RemitoEgreso())); break; }
            }

        }
        //cargar compra
        private void button2_Click(object sender, EventArgs e)
        {
           

            switch (Xusuario.Tipo.ToString())
            {
                case "1": {
                        openChildForm(new CargarRemitoCompra(Xusuario, ref y, new RemitoCompra()));
                        break; }
                case "2": { break; }
                case "3": {
                        openChildForm(new CargarRemitoIngreso(Xusuario, ref y, new RemitoIngreso()));
                        break; }
            }
        }
        // cargar cliente
        private void button6_Click(object sender, EventArgs e)
        {
            openChildForm(new CargarCliente(Xusuario, ref y));
        }
        //cargar proveedores
        private void button7_Click(object sender, EventArgs e)
        {
           
        }
        
        //cargarDescuento
        private void button4_Click(object sender, EventArgs e)
        {
            /* CargarDescuento n = new CargarDescuento(Xusuario, ref y);
             n.Show();*/
            new ConvertidorBD().Show();

        }
        //cargar proveedor
        private void button8_Click(object sender, EventArgs e)
        {
            openChildForm(new CargarProveedor(Xusuario, ref y));
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
            
            openChildForm(new ListaVentas(Xusuario));
        }
        // lista compras
        private void button10_Click(object sender, EventArgs e)
        {
            openChildForm(new ListaCompras(Xusuario));
        }
        private void button11_Click(object sender, EventArgs e)
        {
            openChildForm(new CargarProducto(Xusuario, ref y));
        }
        private void button12_Click(object sender, EventArgs e)
        {
             Usuario d = Usuario.BuscarPorNombre(textBox1.Text);
             Usuario c = Usuario.BuscarPorCodigo(textBox2.Text);
             if ((d!=null)&&(c != null)&&(d.Nombre == c.Nombre)) {



                panel1.Visible = true;
                    Xusuario = c;
                   label2.Visible=textBox2.Visible= textBox1.Enabled = textBox2.Enabled = false;
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
       
        
       
        //cerrar sesion
        private void button15_Click(object sender, EventArgs e)
        {
            label2.Visible = textBox2.Visible=textBox1.Enabled = textBox2.Enabled = true;
            button12.Visible = true;
            button15.Visible = false;
            panel1.Visible  = false;
            textBox1.Text = textBox2.Text = "";
            activeForm.Close();

            this.Close();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            
            openChildForm(new informeYemasDelSol());

        }

        private void Index_Load(object sender, EventArgs e)
        {

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
                */
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

        private void panel3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void button5_Click_3(object sender, EventArgs e)
        {
            openChildForm(new CargarPago(Xusuario));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new CobroCliente(Xusuario));
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            openChildForm(new PagoProveedor(Xusuario));

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            openChildForm(new CargarGatos(Xusuario));
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
