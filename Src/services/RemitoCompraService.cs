using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace E_Shop
{
    class RemitoCompra
    {
        string emisor;
        string codigo;
        string receptor;
        string fechaEmision;
        List<Producto> ListaDeProductos;
        List<Pago> ListaDePagos;

        public RemitoCompra() {
            ListaDeProductos = new List<Producto>();
            ListaDePagos = new List<Pago>();

        }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Emisor { get { return emisor;  }set { emisor = value; } }
        public string Receptor { get { return receptor; }set { receptor = value; } }
        public string FechaEmision { get { return fechaEmision; } set { fechaEmision = value; } }
        public List<Producto> ListaProdutos{ get { return ListaDeProductos; } set { ListaDeProductos = value; } }
        public List<Pago> Pagos{ get { return ListaDePagos; } set { ListaDePagos = value; } }
        public double TotalVenta() { return Producto.SumaVentas(this.ListaDeProductos); }
        public double TotalCosto() { return Producto.SumaCostos(this.ListaDeProductos); }
        public double TotalPago() {
            double sum = 0;
            foreach (Pago x in Pagos) {
                sum += x.Importe;
            }
            return sum;
        }
        public double TotalProductos() {
            double sum = 0;
            for (int i =0; i<ListaDeProductos.Count();i++) {
                sum += ListaDeProductos[i].Cantidad;
            }
            return sum;
        }
        public string ListadoProductos() {

            string var = "";
            for (int i = 0; i<ListaDeProductos.Count();i++) {

                var = var +(

                    ListaDeProductos[i].Codigo + "*" +
                     ListaDeProductos[i].Nombre + "*" +
                     ListaDeProductos[i].Bulto + "*" +
                      ListaDeProductos[i].Cantidad + "*" +
                       ListaDeProductos[i].Costo
                       
                       );
                if (i+1<ListaDeProductos.Count()) { var = var + "*";  }

            }

            return var;
        }
        public string ListadoPagos() {

            string var = "";
            for (int i = 0; i < this.Pagos.Count(); i++)
            {

                var = var + (

                    this.Pagos[i].Codigo + "*" +
                     this.Pagos[i].Nombre + "*" +
                      this.Pagos[i].Importe 
                       );
                if (i + 1 < this.Pagos.Count()) { var = var + "*"; }

            }

            return var;
        }
        public bool AgregarProducto(Producto x){
           
            if (RemitoCompraValidador.AgregarProducto(ref x))
            {
                ListaDeProductos.Add(x);
                return true;
            }return false;
        }
        public bool EliminarProducto(string codigo)
        {
            if (RemitoCompraValidador.GetRemitoCompra(codigo))
            {
                int inde = Producto.BuscarIndexPorCodigo(codigo, this.ListaDeProductos);
                if (inde < this.ListaDeProductos.Count())
                {
                    ListaDeProductos.RemoveAt(inde);
                    return true;
                }
            }  return false;
        }
        public void MostrarDataGrid(ref DataGridView y) {
            y.Rows.Clear();
            for (int i =0; i < this.ListaProdutos.Count();i++) {
                y.Rows.Add(
                    this.ListaProdutos[i].Codigo, 
                    this.ListaProdutos[i].Nombre,
                    this.ListaProdutos[i].Descripcion,
                     this.ListaProdutos[i].Bulto,
                    this.ListaProdutos[i].Cantidad, 
                    this.ListaProdutos[i].Costo,
                    (this.ListaProdutos[i].Cantidad*
                    this.ListaProdutos[i].Costo)
                    );
            }
        }
        static public bool Crear(RemitoCompra x){

            RemitoCompra remitoX = RemitoCompra.BuscarPorCodigo(x.Codigo);
            if (remitoX==null) {
                if (RemitoCompraValidador.CrearRemitoCompra(ref x))
                {

                    List<RemitoCompra> ListaRemitoCompras = RemitoCompra.Buscar();
                    ListaRemitoCompras.Add(x);
                    if (RemitoCompra.Guardar(ListaRemitoCompras))
                    {

                        Producto.SumarStock(x.ListaDeProductos);
                        Pago.RestarCuenta(x.Pagos);

                        foreach (Pago p in x.Pagos)
                        {
                            if (p.Codigo == "2.1.1")
                            {
                                Proveedor pr = Proveedor.BuscarPorNombre(x.Emisor);

                                
                                Proveedor.RestarSaldo(pr.Codigo, p.Importe);

                            }
                            else
                            {
                                if (p.Codigo=="aaaa") {
                                    Proveedor ps = Proveedor.BuscarPorNombre(x.Emisor);
                                    if (ps != null)
                                    {
                                        Proveedor.RestarSaldo(ps.Codigo, p.Importe);
                                        Pago nP = Pago.BuscarPorCodigo("2.1.1");
                                        nP.Importe = p.Importe;
                                        Pago.RestarCuenta(nP);

                                    }
                                }

                            }

                        }
                        MessageBox.Show("Remito Creado");

                        return true;
                    }
                    return false;

                }
            }
            else
            {
                return RemitoCompra.Actualizar(x.Codigo, x);
            }
           return false;
        }
        static public bool Borrar(string codigo)
        {
            if (RemitoCompraValidador.GetRemitoCompra(codigo))
            {
                List<RemitoCompra> ListaRemitoCompra = RemitoCompra.Buscar();
                int i = RemitoCompra.BuscarIndexPorCodigo(codigo, ListaRemitoCompra);
                if (i < ListaRemitoCompra.Count())
                {
                    Producto.RestarStock(ListaRemitoCompra[i].ListaDeProductos);
                    Pago.SumarCuenta(ListaRemitoCompra[i].Pagos);

                    foreach (Pago x in ListaRemitoCompra[i].Pagos)
                    {
                        if (x.Codigo == "2.1.1")
                        {
                            Proveedor.SumarSaldo(
                                Proveedor.BuscarPorNombre(ListaRemitoCompra[i].Emisor).Codigo,
                                x.Importe
                                );
                        }
                    }
                    ListaRemitoCompra.RemoveAt(i);
                    return RemitoCompra.Guardar(ListaRemitoCompra);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoCompra x)
        {

            if (RemitoCompraValidador.ActualizarRemitoCompra(codigo, x))
            {
                List<RemitoCompra> ListaRemitoCompra = RemitoCompra.Buscar();
                int i = RemitoCompra.BuscarIndexPorCodigo(codigo, ListaRemitoCompra);
                if (i < ListaRemitoCompra.Count())
                {
            
                    if (x.Emisor != "") { ListaRemitoCompra[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemitoCompra[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemitoCompra[i].FechaEmision = x.FechaEmision; }

                    Producto.RestarStock(ListaRemitoCompra[i].ListaProdutos);
                    Pago.SumarCuenta(ListaRemitoCompra[i].Pagos);
                    foreach (Pago p in ListaRemitoCompra[i].Pagos)
                    {
                        if (p.Codigo == "2.1.1")
                        {

                            Proveedor.SumarSaldo(Proveedor.BuscarPorNombre(x.Emisor).Codigo, p.Importe);

                        }
                    }
                    ListaRemitoCompra[i].Pagos = x.Pagos;
                    ListaRemitoCompra[i].ListaProdutos = x.ListaProdutos;
                    if (RemitoCompra.Guardar(ListaRemitoCompra))
                    {

                        Producto.SumarStock(x.ListaDeProductos);
                        Pago.RestarCuenta(x.Pagos);

                        foreach (Pago p in x.Pagos)
                        {
                            if (p.Codigo == "2.1.1")
                            {

                                Proveedor.RestarSaldo(Proveedor.BuscarPorNombre(x.Emisor).Codigo, p.Importe);

                            }
                        }
                        MessageBox.Show("Remito Actualizado!!");

                        return true;
                    }





                }

            }
            return false;
        }
        static public List<RemitoCompra> BuscarPorFecha(string fecheDesde, string fechaHasta)
        {
            List<RemitoCompra> x = RemitoCompra.Buscar();
            List<RemitoCompra> y = new List<RemitoCompra>();
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
        static public List<RemitoCompra> BuscarPorFecha(string fecheDesde, string fechaHasta, Proveedor prv)
        {
            List<RemitoCompra> x = RemitoCompra.Buscar();
            List<RemitoCompra> y = new List<RemitoCompra>();
            for (int i = 0; i < x.Count(); i++)
            {

                if ((DateTime.Parse(x[i].FechaEmision) > DateTime.Parse(fecheDesde))
                    || (DateTime.Parse(x[i].FechaEmision) == DateTime.Parse(fecheDesde)))
                {

                    if ((DateTime.Parse(x[i].FechaEmision) < DateTime.Parse(fechaHasta))
                         || (DateTime.Parse(x[i].FechaEmision) == DateTime.Parse(fechaHasta)))
                    {
                        if (prv.Nombre==x[i].Emisor) { y.Add(x[i]); }
                    }

                }
            }
            return y;
        }

        public void Imprimir()
        {

            Direcciones dir = new Direcciones();
            CrearTicket ticket = new CrearTicket(32);
            ticket.AbreCajon();
            ticket.TextoCentro(dir.Space);
            ticket.TextoIzquierda("FECHA: " + DateTime.Now.ToShortDateString());
            ticket.TextoIzquierda("HORA: " + DateTime.Now.ToShortTimeString());
            ticket.lineasAsteriscos();

            for (int i = 0; i < this.ListaDeProductos.Count(); i++)
            {

                ticket.TextoCentro(this.ListaDeProductos[i].Nombre);
                ticket.TextoCentro(
                    this.ListaDeProductos[i].Cantidad + " - $" +
                    this.ListaDeProductos[i].Costo + " - $" +
                    this.ListaDeProductos[i].ImporteCosto()
                    );
                ticket.lineasGuio();
            }
            ticket.lineasIgual();
            ticket.AgregarTotales("Total: $", this.TotalCosto());
            ticket.lineasAsteriscos();
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.CortaTicket();
            ticket.ImprimirTicket(dir.Impresora);//Nombre de la impresora tick
            MessageBox.Show("Documento generado existosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);




        }
        static public RemitoCompra BuscarPorCodigo(string codigo)
        {
            if (RemitoCompraValidador.GetRemitoCompra(codigo))
            {
                List<RemitoCompra> ListaRemitoCompra = RemitoCompra.Buscar();
                int i = RemitoCompra.BuscarIndexPorCodigo(codigo, ListaRemitoCompra);
                if (i < ListaRemitoCompra.Count())
                {
                    return ListaRemitoCompra[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoCompra> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public List<RemitoCompra> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.RemitoCompras);
            string l = "";
            string[] dat;
            int lErr = 0;
            List<RemitoCompra> ListaDeRemitoCompras = new List<RemitoCompra>();

            while ((l = p.ReadLine()) != null)
            {
                lErr++;
                dat = l.Split('|');
                RemitoCompra newRemitoCompra = new RemitoCompra();
                newRemitoCompra.Codigo = dat[0];
                newRemitoCompra.Emisor = dat[1];
                newRemitoCompra.Receptor = dat[2];

                // leer pago por cada boleta (falta desarrollar)
                string []ListaPagos = dat[3].Split('*');
                for (int i =0; i<ListaPagos.Length;i=i+3) {
                    try {
                        Pago n = new Pago();
                        n.Codigo = ListaPagos[i];
                        n.Nombre = ListaPagos[i + 1];
                        n.Importe = double.Parse(ListaPagos[i + 2]);
                        newRemitoCompra.Pagos.Add(n);
                    } catch (Exception err) { MessageBox.Show("Error en la linea " + lErr + ". Consultar Servicio"); }
                }
                // leer lista de productos por cada boleta (falta desarrollar)
                string[]ListaProductos = dat[4].Split('*');

                for (int i = 0; i<ListaProductos.Count(); i = i +5) {

                    Producto pr = new Producto();
                    pr.Codigo = ListaProductos[i];
                    pr.Nombre = ListaProductos[i + 1];
                    pr.Bulto = double.Parse(ListaProductos[i + 2]);
                    pr.Cantidad = double.Parse(ListaProductos[i+3]);
                    pr.Costo = double.Parse(ListaProductos[i + 4]);
                    newRemitoCompra.ListaDeProductos.Add(pr);
                }
                newRemitoCompra.FechaEmision = dat[5];
                ListaDeRemitoCompras.Add(newRemitoCompra);

            }
            p.Close(); p.Dispose();

            return ListaDeRemitoCompras;
        }
        static public bool Guardar(List<RemitoCompra> x){
              Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.RemitoCompras);
            for(int i =0; i<x.Count(); i++){
                p.WriteLine(
                    x[i].Codigo+"|"+
                    x[i].Emisor + "|" + 
                    x[i].Receptor + "|" +
                    x[i].ListadoPagos() +"|"+
                    x[i].ListadoProductos() + "|" +
                    x[i].FechaEmision
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public List<Pago> Ingreso(List<RemitoCompra> x)
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

        static public void ConsolidarMostrar(List<RemitoCompra> x, ref DataGridView y)
        {

            List<Producto> Lista = new List<Producto>();
            for (int i = 0; i < x.Count(); i++)
            {

                for (int j = 0; j < x[i].ListaProdutos.Count(); j++)
                {
                    Producto.AgregarProductosCompra(ref Lista, x[i].ListaProdutos[j]);
                }

            }
            Producto.MostrarCompras(ref y, Lista);
        }

    }
}
