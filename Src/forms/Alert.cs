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
    public partial class Alert : Form
    {
        public Alert(string msj)
        {
            InitializeComponent();
            label1.Text = msj;
        }

    }
}
