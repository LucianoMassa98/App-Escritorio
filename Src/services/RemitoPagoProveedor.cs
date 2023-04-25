using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    internal class RemitoPagoProveedor
    {
        string emisor;
        string codigo;
        string receptor;
        string fechaEmision;
        List<Pago> ListaDePagos;
        public RemitoPagoProveedor()
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
                    this.Pagos[i].Codigo,
                    this.Pagos[i].Nombre,
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
                        this.Pagos[i].Codigo,
                        this.Pagos[i].Importe,
                        this.Pagos[i].Importe,
                        0
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
        static public bool Crear(RemitoPagoProveedor x)
        {

            if (RemitoPagoProveedorValidador.CrearRemitoPagoProveedor(ref x))
            {

                List<RemitoPagoProveedor> ListaRemitoPagoProveedor = RemitoPagoProveedor.Buscar();
                ListaRemitoPagoProveedor.Add(x);
                if (RemitoPagoProveedor.Guardar(ListaRemitoPagoProveedor))
                {
                    //sumar saldo a proveedor global
                    Pago newP = new Pago();
                    newP.Codigo = "2.1.1";
                    newP.Importe = x.Pagos[0].Importe;
                    List<Pago> xy = new List<Pago>();
                    xy.Add(newP);
                    Pago.SumarCuenta(xy);
                    //sumar saldo a proveedor individual
                    Proveedor.SumarSaldo(Proveedor.BuscarPorNombre(x.Receptor).Codigo, x.Pagos[0].Importe);

                    //restar cuenta con la que se realizo el pago
                    Pago.RestarCuenta(x.Pagos);
                    return true;
                }
                return false;

            }
            return false;
        }
        static public bool Borrar(string codigo)
        {

            if (RemitoPagoProveedorValidador.GetRemitoPagoProveedor(codigo))
            {
                List<RemitoPagoProveedor> ListaRemitoPagoProveedor = RemitoPagoProveedor.Buscar();
                int i = RemitoPagoProveedor.BuscarIndexPorCodigo(codigo, ListaRemitoPagoProveedor);
                if (i < ListaRemitoPagoProveedor.Count())
                {
                    ListaRemitoPagoProveedor.RemoveAt(i);
                    return RemitoPagoProveedor.Guardar(ListaRemitoPagoProveedor);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoPagoProveedor x)
        {

            if (RemitoPagoProveedorValidador.ActualizarRemitoPagoProveedor(codigo, x))
            {
                List<RemitoPagoProveedor> ListaRemitoPagoProveedor = RemitoPagoProveedor.Buscar();
                int i = RemitoPagoProveedor.BuscarIndexPorCodigo(codigo, ListaRemitoPagoProveedor);
                if (i < ListaRemitoPagoProveedor.Count())
                {
                    if (x.Emisor != "") { ListaRemitoPagoProveedor[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemitoPagoProveedor[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemitoPagoProveedor[i].FechaEmision = x.FechaEmision; }
                    ListaRemitoPagoProveedor[i].Pagos = x.Pagos;

                    return RemitoPagoProveedor.Guardar(ListaRemitoPagoProveedor);
                }

            }
            return false;
        }
        static public RemitoPagoProveedor BuscarPorCodigo(string codigo)
        {
            if (RemitoPagoProveedorValidador.GetRemitoPagoProveedor(codigo))
            {
                List<RemitoPagoProveedor> ListaRemitoPagoProveedor = RemitoPagoProveedor.Buscar();
                int i = RemitoPagoProveedor.BuscarIndexPorCodigo(codigo, ListaRemitoPagoProveedor);
                if (i < ListaRemitoPagoProveedor.Count())
                {
                    return ListaRemitoPagoProveedor[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoPagoProveedor> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }

        static public List<RemitoPagoProveedor> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.PagoProveedores);
            string l = "";
            string[] dat;
            List<RemitoPagoProveedor> ListaDeRemitoPagoProveedor = new List<RemitoPagoProveedor>();

            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                RemitoPagoProveedor newRemitoPagoProveedor = new RemitoPagoProveedor();
                newRemitoPagoProveedor.Codigo = dat[0];
                newRemitoPagoProveedor.Emisor = dat[1];
                newRemitoPagoProveedor.Receptor = dat[2];

                // leer pago por cada boleta 
                string[] ListaPagos = dat[3].Split('/');
                for (int i = 0; i < ListaPagos.Length; i = i + 3)
                {
                    Pago n = new Pago();
                    n.Codigo = ListaPagos[i];
                    n.Nombre = ListaPagos[i + 1];
                    n.Importe = double.Parse(ListaPagos[i + 2]);
                    newRemitoPagoProveedor.Pagos.Add(n);
                }


                newRemitoPagoProveedor.FechaEmision = dat[4];
                ListaDeRemitoPagoProveedor.Add(newRemitoPagoProveedor);
            }
            p.Close(); p.Dispose();

            return ListaDeRemitoPagoProveedor;
        }
        static public bool Guardar(List<RemitoPagoProveedor> x)
        {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.PagoProveedores);
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

        static public List<RemitoPagoProveedor> BuscarPorFecha(string fecheDesde, string fechaHasta)
        {
            List<RemitoPagoProveedor> x = RemitoPagoProveedor.Buscar();
            List<RemitoPagoProveedor> y = new List<RemitoPagoProveedor>();
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
