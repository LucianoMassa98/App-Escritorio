using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace E_Shop
{
    public partial class ImpirmirCodigoBarra : Form
    {
        string producto;
        Image codigo;
        public ImpirmirCodigoBarra(string nombre, Image barra)
        {
            InitializeComponent();
            codigo = panel1.BackgroundImage = barra; 
            producto = label1.Text = nombre;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0;i<numericUpDown1.Value; i++) {
                printDocument1 = new PrintDocument();
                PrinterSettings ps = new PrinterSettings();
                printDocument1.PrinterSettings = ps;
                printDocument1.PrintPage += imprimir;
                printDocument1.Print();
            }
           

            this.Close();
        }

        private void imprimir(object sender, PrintPageEventArgs e) {

            Font font = new Font("Arial",8, FontStyle.Regular, GraphicsUnit.Point);
            int x = 130;
            int y = 20;

            
            if (producto.Length > 18) {
                producto = producto.Substring(0,19);
            }
            e.Graphics.DrawString(producto, font, Brushes.Black, new Rectangle(0,y+=20,x,20));
            e.Graphics.DrawImage(codigo, new Rectangle(0, y+=20, x,50));
            CrearTicket ticket = new CrearTicket(32);
            ticket.AbreCajon();
            ticket.TextoIzquierda("");
            ticket.CortaTicket();
            ticket.ImprimirTicket(new Direcciones().Impresora);//Nombre de la impresora tick

        }

        private void ImpirmirCodigoBarra_Load(object sender, EventArgs e)
        {

        }
    }
}
