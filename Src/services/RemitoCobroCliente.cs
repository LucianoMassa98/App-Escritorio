using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    internal class RemitoCobroCliente
    {
        string emisor;
        string codigo;
        string receptor;
        string fechaEmision;
        List<Pago> ListaDePagos;
        public RemitoCobroCliente()
        {
            ListaDePagos = new List<Pago>();

        }
        public string FechaEmision { get { return fechaEmision; } set { fechaEmision = value; } }
        public string Receptor { get { return receptor; } set { receptor = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Emisor { get { return emisor; } set { emisor = value; } }
        public List<Pago> Pagos { get { return ListaDePagos; } set { ListaDePagos = value; } }
        public string ListadoPagos()
        {

            string var = "";
            for (int i = 0; i < this.Pagos.Count(); i++)
            {

                var = var + (

                    this.Pagos[i].Codigo + "/" +
                     this.Pagos[i].Nombre + "/" +
                      this.Pagos[i].Importe
                       );
                if (i + 1 < this.Pagos.Count()) { var = var + "/"; }

            }

            return var;
        }
        public void MostrarDataGrid(ref DataGridView y)
        {
            y.Rows.Clear();
            for (int i = 0; i < this.ListaDePagos.Count(); i++)
            {
                y.Rows.Add(
                    this.ListaDePagos[i].Codigo,
                    this.ListaDePagos[i].Nombre,
                    this.Pagos[i].Importe
                    );
            }
        }
        public void Imprimir()
        {

            try
            {


                CrearTicket ticket = new CrearTicket(32);
                ticket.AbreCajon();
                double suma = 0;
                ticket.TextoCentro("Pintitas");
                ticket.TextoIzquierda("FECHA: " + DateTime.Now.ToShortDateString());
                ticket.TextoIzquierda("HORA: " + DateTime.Now.ToShortTimeString());
                ticket.lineasAsteriscos();
                for (int i = 0; i < this.Pagos.Count(); i++)
                {
                    ticket.AgregaArticulo(
                        this.Pagos[i].Nombre,
                        this.Pagos[i].Importe,
                        this.Pagos[i].Importe,
                        this.Pagos[i].Importe
                        );

                }
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
        static public bool Crear(RemitoCobroCliente x,string codPago)
        {

            if (RemitoCobroClienteValidador.CrearRemitoCobroCliente(ref x))
            {

                List<RemitoCobroCliente> ListaRemitoCobroCliente = RemitoCobroCliente.Buscar();
                ListaRemitoCobroCliente.Add(x);
                if (RemitoCobroCliente.Guardar(ListaRemitoCobroCliente))
                {
                    //RestarSaldoCliente
                    Cliente.RestarSaldo(Cliente.BuscarPorNombre(x.Receptor).Codigo,x.Pagos[0].Importe);

                    Cliente c = new Cliente();
                    //RestarSaldoaCreditoxVenta
                    Pago newP = new Pago();
                    newP.Codigo = codPago;
                    newP.Importe = x.Pagos[0].Importe;
                     List<Pago> xy = new List<Pago>();
                    xy.Add(newP);
                    Pago.RestarCuenta(xy);
                    //sumar la cuenta con la que se pago
                    Pago.SumarCuenta(x.Pagos);

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

            if (RemitoCobroClienteValidador.GetRemitoCobroCliente(codigo))
            {
                List<RemitoCobroCliente> ListaRemitoCobroCliente = RemitoCobroCliente.Buscar();
                int i = RemitoCobroCliente.BuscarIndexPorCodigo(codigo, ListaRemitoCobroCliente);
                if (i < ListaRemitoCobroCliente.Count())
                {
                    ListaRemitoCobroCliente.RemoveAt(i);
                    return RemitoCobroCliente.Guardar(ListaRemitoCobroCliente);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoCobroCliente x)
        {

            if (RemitoCobroClienteValidador.ActualizarRemitoCobroCliente(codigo, x))
            {
                List<RemitoCobroCliente> ListaRemitoCobroCliente = RemitoCobroCliente.Buscar();
                int i = RemitoCobroCliente.BuscarIndexPorCodigo(codigo, ListaRemitoCobroCliente);
                if (i < ListaRemitoCobroCliente.Count())
                {
                    if (x.Emisor != "") { ListaRemitoCobroCliente[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemitoCobroCliente[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemitoCobroCliente[i].FechaEmision = x.FechaEmision; }
                    ListaRemitoCobroCliente[i].Pagos = x.Pagos;

                    return RemitoCobroCliente.Guardar(ListaRemitoCobroCliente);
                }

            }
            return false;
        }
        static public RemitoCobroCliente BuscarPorCodigo(string codigo)
        {
            if (RemitoCobroClienteValidador.GetRemitoCobroCliente(codigo))
            {
                List<RemitoCobroCliente> ListaRemitoCobroCliente = RemitoCobroCliente.Buscar();
                int i = RemitoCobroCliente.BuscarIndexPorCodigo(codigo, ListaRemitoCobroCliente);
                if (i < ListaRemitoCobroCliente.Count())
                {
                    return ListaRemitoCobroCliente[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoCobroCliente> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }

        static public List<RemitoCobroCliente> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.CobroClientes);
            string l = "";
            string[] dat;
            List<RemitoCobroCliente> ListaDeRemitoCobroCliente = new List<RemitoCobroCliente>();

            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                RemitoCobroCliente newRemitoCobroCliente = new RemitoCobroCliente();
                newRemitoCobroCliente.Codigo = dat[0];
                newRemitoCobroCliente.Emisor = dat[1];
                newRemitoCobroCliente.Receptor = dat[2];

                // leer pago por cada boleta 
                string[] ListaPagos = dat[3].Split('/');
                for (int i = 0; i < ListaPagos.Length; i = i + 3)
                {
                    Pago n = new Pago();
                    n.Codigo = ListaPagos[i];
                    n.Nombre = ListaPagos[i + 1];
                    n.Importe = double.Parse(ListaPagos[i + 2]);
                    newRemitoCobroCliente.Pagos.Add(n);
                }


                newRemitoCobroCliente.FechaEmision = dat[4];
                ListaDeRemitoCobroCliente.Add(newRemitoCobroCliente);
            }
            p.Close(); p.Dispose();

            return ListaDeRemitoCobroCliente;
        }
        static public bool Guardar(List<RemitoCobroCliente> x)
        {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.CobroClientes);
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
        static public List<Pago> Ingreso(List<RemitoCobroCliente> x)
        {
            List<Pago> lista = new List<Pago>();
            double[] res = new double[8];
            for (int i = 0; i < x.Count(); i++)
            {
                for (int j = 0; j < x[i].Pagos.Count(); j++)
                {
                    Pago.AgregarCuenta(ref lista, x[i].Pagos[j]);
                }

            }
            return lista;
        }
        static public List<RemitoCobroCliente> BuscarPorFecha(string fecheDesde, string fechaHasta)
        {
            List<RemitoCobroCliente> x = RemitoCobroCliente.Buscar();
            List<RemitoCobroCliente> y = new List<RemitoCobroCliente>();
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
