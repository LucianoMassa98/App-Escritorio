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
    public partial class CobroCliente : Form
    {
        Usuario xUsuario;
        Form FormularioAnterior;
        RemitoCobroCliente RemitoX;
        
        public CobroCliente(object x, ref Form y)
        {
            InitializeComponent();
            xUsuario = (Usuario)x;
            FormularioAnterior = y;
            LoadComboBox();
            RemitoX = new RemitoCobroCliente();
            RemitoX.Emisor = xUsuario.Nombre;
            RemitoX.FechaEmision = DateTime.Now.Date.ToShortDateString();

        }
        public void LoadComboBox() {

            // cargar nom y cod de clientes
            Cliente.CargarComboBox(ref comboBox2, ref comboBox1);

            //cargar formas de pago
            Pago.CargarComboBox(ref comboBox3, "1.1.1");
        }

        //guardar cobro
        private void button3_Click(object sender, EventArgs e)
        {
            if ((double.Parse(textBox9.Text) > double.Parse(textBox1.Text))
                || (double.Parse(textBox9.Text) == double.Parse(textBox1.Text)))
            {


               
                RemitoX.Pagos[0].Importe = double.Parse(textBox1.Text);

                if (RemitoCobroCliente.Crear(RemitoX,"1.1.3"))
                {
                    NuevoCobro();
                }
                else {
                    new Alert("No se pudo 'crear' nuevo pago").Show();

                }
            }
            else {
                new Alert("Importes mal cargados").Show();
            }
            
        }
        //selecionar cliente por codigo
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cliente c = Cliente.BuscarPorCodigo(comboBox1.Text);
           RemitoX.Receptor =  comboBox2.Text = c.Nombre;
            textBox9.Text = c.Saldo.ToString();
            label5.Text = "Ultimo saldo: " + Cliente.ultimaSaldo(c);
            comboBox1.Enabled = comboBox2.Enabled = textBox9.Enabled = false;
            comboBox3.Focus();

        }
        //seleccionar cliente por nombre
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cliente c = Cliente.BuscarPorNombre(comboBox2.Text);
            RemitoX.Receptor = comboBox2.Text;

            label5.Text = "Ultimo saldo: "+Cliente.ultimaSaldo(c);
            
            comboBox1.Text = c.Codigo;
            textBox9.Text = c.Saldo.ToString();
            comboBox1.Enabled = comboBox2.Enabled = textBox9.Enabled = false;
            comboBox3.Focus();

        }
       
        public void NuevoCobro() {

            RemitoX.Pagos.Clear();
            comboBox1.Enabled = comboBox2.Enabled = textBox9.Enabled = true;
            comboBox1.Text = comboBox2.Text = comboBox3.Text = textBox9.Text = textBox1.Text = "";
            comboBox1.Focus();
        }
        //cancelar cobro
        private void button4_Click(object sender, EventArgs e)
        {
            NuevoCobro();
        }
        //seleccionar tipo de pago
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemitoX.Pagos.Add(Pago.BuscarPorNombre(comboBox3.Text));
            textBox1.Focus();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) { button3.Focus(); }
        }

        private void CobroCliente_Load(object sender, EventArgs e)
        {
            panel3.BackgroundImage = Image.FromFile(new Direcciones().Logo);
        }
    }
}
