using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    internal class RemitoGasto
    {
        string emisor;
        string codigo;
        string receptor;
        string fechaEmision;
        Pago fuente;
        Pago razon;
        public RemitoGasto()
        {
           fuente = new Pago();
            razon = new Pago();

        }
        public string FechaEmision { get { return fechaEmision; } set { fechaEmision = value; } }
        public string Receptor { get { return receptor; } set { receptor = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Emisor { get { return emisor; } set { emisor = value; } }
        public Pago Fuente { get { return fuente; } set { fuente  = value; } }
        public Pago Razon { get { return razon; } set { razon = value; } }
        public string ListadoPagos()
        {

            string var = 
            this.Fuente.Codigo + "/" +
            this.Fuente.Nombre + "/" +
            this.Fuente.Importe + "/" +
            this.Razon.Codigo + "/" +
            this.Razon.Nombre + "/" +
            this.Razon.Importe;

            return var;
        }
        public void MostrarDataGrid(ref DataGridView y)
        {
            y.Rows.Clear();
           
                y.Rows.Add(
                    this.fuente.Codigo,
                    this.fuente.Nombre,
                    this.fuente.Importe
                    );
            y.Rows.Add(
                    this.razon.Codigo,
                    this.razon.Nombre,
                    this.razon.Importe
                    );

        }
        public void Imprimir()
        {

            try
            {


                CrearTicket ticket = new CrearTicket(32);
                ticket.AbreCajon();
                double suma = 0;
                ticket.TextoIzquierda("FECHA: " + DateTime.Now.ToShortDateString());
                ticket.TextoIzquierda("HORA: " + DateTime.Now.ToShortTimeString());
                ticket.lineasAsteriscos();

                /*ticket.AgregaArticulo(
                         this.fuente.Codigo,
                         this.fuente.Nombre,
                         this.fuente.Importe
                         );
                 ticket.AgregaArticulo(
                       this.razon.Codigo,
                       this.razon.Nombre,
                       this.razon.Importe
                       );*/

                ticket.lineasIgual();
                ticket.AgregarTotales("Total: $", 0);
                ticket.lineasAsteriscos();
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.CortaTicket();
                ticket.ImprimirTicket("POS-58");//Nombre de la impresora tick



            }
            catch (Exception) { }

        }
        static public bool Crear(RemitoGasto x)
        {

            if (RemitoGastoValidator.CrearRemitoGasto(ref x))
            {

                List<RemitoGasto> ListaRemitoGasto = RemitoGasto.Buscar();
                ListaRemitoGasto.Add(x);
                if (RemitoGasto.Guardar(ListaRemitoGasto))
                {
                    

                    
                     // cuenta fuente
                    Pago.RestarCuenta(x.Fuente);
                    //cuenta razon
                    Pago.SumarCuenta(x.Razon);

                    return true;
                }
                else {
                    new Alert("No se pudo Guardar").Show();

                }
                return false;

            }
            else {

                new Alert("Datos mal cargados").Show();

            }
            return false;
        }
        static public bool Borrar(string codigo)
        {

            if (RemitoGastoValidator.GetRemitoGasto(codigo))
            {
                List<RemitoGasto> ListaRemitoGasto = RemitoGasto.Buscar();
                int i = RemitoGasto.BuscarIndexPorCodigo(codigo, ListaRemitoGasto);
                if (i < ListaRemitoGasto.Count())
                {
                    ListaRemitoGasto.RemoveAt(i);
                    return RemitoGasto.Guardar(ListaRemitoGasto);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoGasto x)
        {

            if (RemitoGastoValidator.ActualizarRemitoGasto(codigo, x))
            {
                List<RemitoGasto> ListaRemitoGasto = RemitoGasto.Buscar();
                int i = RemitoGasto.BuscarIndexPorCodigo(codigo, ListaRemitoGasto);
                if (i < ListaRemitoGasto.Count())
                {
                    if (x.Emisor != "") { ListaRemitoGasto[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemitoGasto[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemitoGasto[i].FechaEmision = x.FechaEmision; }
                    ListaRemitoGasto[i].Fuente = x.Fuente;
                    ListaRemitoGasto[i].Razon = x.Razon;
                    return RemitoGasto.Guardar(ListaRemitoGasto);
                }

            }
            return false;
        }
        static public RemitoGasto BuscarPorCodigo(string codigo)
        {
            if (RemitoGastoValidator.GetRemitoGasto(codigo))
            {
                List<RemitoGasto> ListaRemitoGasto = RemitoGasto.Buscar();
                int i = RemitoGasto.BuscarIndexPorCodigo(codigo, ListaRemitoGasto);
                if (i < ListaRemitoGasto.Count())
                {
                    return ListaRemitoGasto[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoGasto> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public List<RemitoGasto> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.Gastos);
            string l = "";
            string[] dat;
            List<RemitoGasto> ListaDeRemitoGasto = new List<RemitoGasto>();

            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                RemitoGasto newRemitoGasto = new RemitoGasto();
                newRemitoGasto.Codigo = dat[0];
                newRemitoGasto.Emisor = dat[1];
                newRemitoGasto.Receptor = dat[2];

               
                string[] ListaPagos = dat[3].Split('/');
                if (ListaPagos.Length==6) {
                    try {
                        newRemitoGasto.Fuente.Codigo = ListaPagos[0];
                        newRemitoGasto.Fuente.Nombre = ListaPagos[1];
                        newRemitoGasto.Fuente.Importe = double.Parse(ListaPagos[2]);
                        newRemitoGasto.Razon.Codigo = ListaPagos[3];
                        newRemitoGasto.Razon.Nombre = ListaPagos[4];
                        newRemitoGasto.Razon.Importe = double.Parse(ListaPagos[5]);
                    } catch (Exception err) {
                        MessageBox.Show(err.ToString());
                    }
                
                }
                

                newRemitoGasto.FechaEmision = dat[4];
                ListaDeRemitoGasto.Add(newRemitoGasto);
            }
            p.Close(); p.Dispose();

            return ListaDeRemitoGasto;
        }
        static public bool Guardar(List<RemitoGasto> x)
        {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.Gastos);
            for (int i = 0; i < x.Count(); i++)
            {
                p.WriteLine(
                    x[i].Codigo + "|" +
                    x[i].Emisor + "|" +
                    x[i].Receptor + "|" +
                    x[i].ListadoPagos() + "|" +
                    x[i].FechaEmision
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public List<RemitoGasto> BuscarPorFecha(string fecheDesde, string fechaHasta)
        {
            List<RemitoGasto> x = RemitoGasto.Buscar();
            List<RemitoGasto> y = new List<RemitoGasto>();
            for (int i = 0; i < x.Count(); i++)
            {

                if ((DateTime.Parse(x[i].FechaEmision) > DateTime.Parse(fecheDesde))
                    || (DateTime.Parse(x[i].FechaEmision) == DateTime.Parse(fecheDesde)))
                {

                    if ((DateTime.Parse(x[i].FechaEmision) < DateTime.Parse(fechaHasta))
                         || (DateTime.Parse(x[i].FechaEmision) == DateTime.Parse(fechaHasta)))
                    {
                        y.Add(x[i]);
                    }

                }
            }
            return y;
        }
    }
}
