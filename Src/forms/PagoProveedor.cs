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
    public partial class PagoProveedor : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        RemitoPagoProveedor RemitoX;
        public PagoProveedor(object x, ref Form y)
        {
            InitializeComponent();
            xUsuario = (Usuario)x;
            FormularioAnterior = y;
            LoadComboBox();
            RemitoX = new RemitoPagoProveedor();
            RemitoX.Emisor = xUsuario.Nombre;
            RemitoX.FechaEmision = DateTime.Now.Date.ToShortDateString();
        }
        public void LoadComboBox()
        {

            // cargar nom y cod de Proveedores
            Proveedor.CargarComboBox(ref comboBox2, ref comboBox1);

            //cargar formas de pago
            Pago.CargarComboBox(ref comboBox3, "1.1.1");
        }
        //guardar pago
        private void button3_Click(object sender, EventArgs e)
        {
            try {
                double imp = double.Parse(textBox1.Text);
                if (imp>0)
                 {
              
                RemitoX.Pagos[0].Importe = imp;

                if (RemitoPagoProveedor.Crear(RemitoX))
                {
                    NuevoPago();
                }
                else
                {
                    new Alert("No se pudo 'crear' nuevo pago").Show();

                }
                }
                else
                {
                    new Alert("El importe debe ser mayor a cero").Show();
                }
            }
            catch (Exception) { new Alert("Hay Campos Vacios").Show(); }
        }
        //seleccionar tipo de pago
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemitoX.Pagos.Add(Pago.BuscarPorNombre(comboBox3.Text));
            textBox1.Focus();

        }
        // seleccionar Proveedor por codigo
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Proveedor c = Proveedor.BuscarPorCodigo(comboBox1.Text);
            RemitoX.Receptor = comboBox2.Text = c.Nombre;
            textBox9.Text = c.Saldo.ToString();
            comboBox1.Enabled = comboBox2.Enabled = textBox9.Enabled = false;
            comboBox3.Focus();

        }
        //seleccionar Proveedor por nombre
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Proveedor c = Proveedor.BuscarPorNombre(comboBox2.Text);
            RemitoX.Receptor = comboBox2.Text;
            comboBox1.Text = c.Codigo;
            textBox9.Text = c.Saldo.ToString();
            comboBox1.Enabled = comboBox2.Enabled = textBox9.Enabled = false;
            comboBox3.Focus();

        }
        // cancelar pago a proveedor
        private void button4_Click(object sender, EventArgs e)
        {
            NuevoPago();
        }
        public void NuevoPago()
        {

            RemitoX.Pagos.Clear();
            comboBox1.Enabled = comboBox2.Enabled = textBox9.Enabled = true;
            comboBox1.Text = comboBox2.Text = comboBox3.Text = textBox9.Text = textBox1.Text = "";
            comboBox1.Focus();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { button3.Focus(); }
        }

        private void PagoProveedor_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);

        }
    }
}
