using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;

namespace E_Shop
{
    public partial class ConvertidorBD : Form
    {
        public ConvertidorBD()
        {
            InitializeComponent();
        }
       /* DataView importDatos(string nomArchivo)
        {
            string conexion = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties = 'Excel 12.0';",nomArchivo);
            OleDbConnection conector = new OleDbConnection
            {
            }
            ;
            conector.Open();

            OleDbCommand consulta = new OleDbCommand("select * from [Hoja1$]",conector);

            OleDbDataAdapter adaptador = new OleDbDataAdapter { SelectCommand = consulta };

            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            conector.Close();
            return ds.Tables[0].DefaultView;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel | *.xls;*.xlsx",
                Title = "Seleccionar Archivo",

            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = importDatos(openFileDialog.FileName);
            }
        }
       */
        private void button4_Click(object sender, EventArgs e)
        {
            List<Producto> lista = new List<Producto>();
            StreamReader prd = new StreamReader(@"C:\Users\Luciano\OneDrive\SetData\MyG\Debug\productos-nombre.txt");
            StreamReader blt = new StreamReader(@"C:\Users\Luciano\OneDrive\SetData\MyG\Debug\productos-bulto.txt");
            StreamReader cnt = new StreamReader(@"C:\Users\Luciano\OneDrive\SetData\MyG\Debug\productos-cnt.txt");
            StreamReader cost = new StreamReader(@"C:\Users\Luciano\OneDrive\SetData\MyG\Debug\productos-costo.txt");
            int i = 1;
            string lcost = "";
            string lcnt = "";
            string lblt = "";
            string lprd = "";
            while ((lprd=prd.ReadLine())!=null) {

                lblt = blt.ReadLine();
                lcnt = cnt.ReadLine();
                lcost = cost.ReadLine();



                Producto newProducto = new Producto();
                newProducto.Codigo = i.ToString();
                i++;
                newProducto.Nombre=lprd;
                newProducto.Descripcion = "RubroX";
                newProducto.Bulto = double.Parse(lblt);
                newProducto.Cantidad = double.Parse(lcnt);
                newProducto.Costo = double.Parse(lcost);

                lista.Add(newProducto);

            }

            Producto.Guardar(lista);



            cost.Close(); cost.Dispose();
            cnt.Close(); cnt.Dispose();
            blt.Close(); blt.Dispose();
            prd.Close(); prd.Dispose();

        }
    }
}
