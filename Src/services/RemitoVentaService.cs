 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace E_Shop
{
    class RemitoVenta
    {
        string emisor;
        string codigo;
        string receptor;
        string fechaEmision;
        List<Producto> ListaDeProductos;
        List<Pago> ListaDePagos;
        bool directo;
        public RemitoVenta() {
            ListaDeProductos = new List<Producto>();
            ListaDePagos = new List<Pago>();
            directo = true;
        }
        public string FechaEmision { get { return fechaEmision; } set { fechaEmision= value; } }
        public string Receptor { get { return receptor; }set { receptor = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Emisor { get { return emisor;  }set { emisor = value; } }
        public List<Pago> Pagos{ get { return ListaDePagos; } set { ListaDePagos = value; } }
        public List<Producto> ListaProdutos{ get { return ListaDeProductos; } set { ListaDeProductos = value; } }
        public bool Directo { get{ return directo; }set { directo = value; }}
        public bool EnviarFacturaAfip() {
            return true;
        }
        public double TotalVenta() { 
            return Producto.SumaVentas(this.ListaDeProductos); }
        public double TotalPago()
        {
            double sum = 0;
            foreach(Pago x in Pagos)
            {
                sum += x.Importe;
            }
            return sum;
        }
        public double TotalCosto() {
            return Producto.SumaCostos2(this.ListaDeProductos); }
        public string ListadoProductos()
        {

            string var = "";
            for (int i = 0; i < ListaDeProductos.Count(); i++)
            {

                var = var + (

                    ListaDeProductos[i].Codigo + "*" +
                     ListaDeProductos[i].Nombre + "*" +
                      ListaDeProductos[i].Bulto + "*" +
                      ListaDeProductos[i].Cantidad + "*" +
                       ListaDeProductos[i].Precio

                       );
                if (i + 1 < ListaDeProductos.Count()) { var = var + "*"; }

            }

            return var;
        }
        public string ListadoPagos()
        {

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
        public bool AgregarProducto(Producto x,Descuento y)
        {
            if (RemitoVentaValidador.AgregarProducto(ref x,ref y))
            {
                ListaDeProductos.Add(x);
                return true;
            }
            return false;
        }
        public bool AgregarProducto(Producto x)
        {
            if (RemitoVentaValidador.AgregarProducto(ref x))
            {
                ListaDeProductos.Add(x);
                return true;
            }
            return false;
        }
        public bool EliminarProducto(string codigo)
        {
            if (RemitoVentaValidador.GetRemitoVenta(codigo))
            {
                int inde = Producto.BuscarIndexPorCodigo(codigo, this.ListaDeProductos);
                if (inde < this.ListaDeProductos.Count())
                {
                    ListaDeProductos.RemoveAt(inde);
                    return true;
                }
            }
            return false;
        }
        public bool Existe(string codigo)
        {
            for (int i = 0; i < ListaDeProductos.Count(); i++)
            {
                if (ListaDeProductos[i].Codigo == codigo) { return true; }
            }
            return false;
        }
        public void MostrarDataGrid(ref DataGridView y)
        {
            y.Rows.Clear();
            for (int i = 0; i < this.ListaProdutos.Count(); i++)
            {
                y.Rows.Add(
                    this.ListaProdutos[i].Codigo,
                    this.ListaProdutos[i].Nombre,
                    this.ListaProdutos[i].Descripcion,
                     this.ListaProdutos[i].Bulto,
                    this.ListaProdutos[i].Cantidad,
                    this.ListaProdutos[i].Precio,
                    this.ListaProdutos[i].ImportePrecio()
                    );
            }
        }
        public void Imprimir() {

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
                     this.ListaDeProductos[i].Bulto + " - " +
                     this.ListaDeProductos[i].Cantidad + " - $" +
                     this.ListaDeProductos[i].Precio + " - $" +
                     this.ListaDeProductos[i].ImportePrecio()
                     );
                ticket.lineasGuio();
            }
            ticket.lineasIgual();
                ticket.AgregarTotales("Total: $", this.TotalVenta());
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
        public double CantidadHuevos() {
            double sum = 0;

            for (int i =0; i<ListaDeProductos.Count();i++) {

                if (Producto.BuscarPorCodigo(ListaDeProductos[i].Codigo).Descripcion == "Huevo") { sum += ListaDeProductos[i].Cantidad; }
            }

            return sum;
        }
        public double CantidadMercaderia()
        {
            double sum = 0;

            for (int i = 0; i < ListaDeProductos.Count(); i++)
            {

                if (Producto.BuscarPorCodigo(ListaDeProductos[i].Codigo).Descripcion == "Mercaderia") { sum += ListaDeProductos[i].Cantidad; }
            }

            return sum;
        }
        static public bool Crear(RemitoVenta x)
        {
           
            RemitoVenta remitoX = RemitoVenta.BuscarPorCodigo(x.Codigo);
            if (remitoX==null) {
                if (RemitoVentaValidador.CrearRemitoVenta(x))
                {

                    List<RemitoVenta> ListaRemitoVenta = RemitoVenta.Buscar();
                    ListaRemitoVenta.Add(x);
                    if (RemitoVenta.Guardar(ListaRemitoVenta))
                    {
                        Producto.RestarStock(x.ListaDeProductos);
                        Pago.SumarCuenta(x.Pagos);
                        foreach (Pago p in x.Pagos)
                        {
                            if (p.Codigo == "1.1.3")
                            {
                                Cliente cs = Cliente.BuscarPorNombre(x.Receptor);

                                
                                if (cs!=null) {
                                    Cliente.SumarSaldo(cs.Codigo, p.Importe); } 
                                else
                                {
                                    Proveedor ps = Proveedor.BuscarPorNombre(x.Receptor);
                                    if (ps != null)
                                    {
                                        Proveedor.RestarSaldo(ps.Codigo, p.Importe);
                                    }

                                }
                            }
                            else
                            {
                                 if (p.Codigo == "aaaa")
                                 {

                                     Proveedor ps = Proveedor.BuscarPorNombre(x.Receptor);
                                     if (ps != null)
                                     {
                                         Proveedor.SumarSaldo(ps.Codigo, p.Importe);
                                         Pago nP = Pago.BuscarPorCodigo("2.1.1");
                                         nP.Importe = p.Importe;
                                         Pago.SumarCuenta(nP);

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
              
             return    RemitoVenta.Actualizar(x.Codigo,x);
            }

            
            return false;
        }
        static public bool Borrar(string codigo)
        {
            if (RemitoVentaValidador.GetRemitoVenta(codigo))
            {
                List<RemitoVenta> ListaRemitoVenta = RemitoVenta.Buscar();
                int i = RemitoVenta.BuscarIndexPorCodigo(codigo, ListaRemitoVenta);
                if (i < ListaRemitoVenta.Count())
                {
                             
                        Producto.SumarStock(ListaRemitoVenta[i].ListaDeProductos);
                        Pago.RestarCuenta(ListaRemitoVenta[i].Pagos);

                    foreach (Pago x in ListaRemitoVenta[i].Pagos) {

                        if ((x.Codigo == "1.1.3") || (x.Codigo == "1.1.4"))
                        {
                            Cliente.RestarSaldo(
                                Cliente.BuscarPorNombre(ListaRemitoVenta[i].Receptor).Codigo,
                                x.Importe
                                );

                        }

                    }
                       
                    ListaRemitoVenta.RemoveAt(i);
                    return RemitoVenta.Guardar(ListaRemitoVenta);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoVenta x)
        {

            if (RemitoVentaValidador.ActualizarRemitoVenta(codigo, x))
            {
                List<RemitoVenta> ListaRemitoVenta = RemitoVenta.Buscar();
                int i = RemitoVenta.BuscarIndexPorCodigo(codigo, ListaRemitoVenta);
                if (i < ListaRemitoVenta.Count())
                {
                    
                    if (x.Emisor != "") { ListaRemitoVenta[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemitoVenta[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemitoVenta[i].FechaEmision = x.FechaEmision; }

                    Producto.SumarStock(ListaRemitoVenta[i].ListaProdutos);
                    Pago.RestarCuenta(ListaRemitoVenta[i].Pagos);
                    foreach (Pago p in ListaRemitoVenta[i].Pagos)
                    {
                        if ((p.Codigo == "1.1.3") || (p.Codigo == "1.1.4"))
                        {
                            Cliente.RestarSaldo(Cliente.BuscarPorNombre(x.Receptor).Codigo, p.Importe);

                        }
                    }
                    ListaRemitoVenta[i].Pagos = x.Pagos;
                    ListaRemitoVenta[i].ListaProdutos = x.ListaProdutos;

                   
                    if (RemitoVenta.Guardar(ListaRemitoVenta))
                    {
                        Producto.RestarStock(x.ListaDeProductos);
                        Pago.SumarCuenta(x.Pagos);
                        foreach (Pago p in x.Pagos)
                        {
                            if (p.Codigo == "1.1.3")
                            {
                                Cliente.SumarSaldo(Cliente.BuscarPorNombre(x.Receptor).Codigo, p.Importe);

                            }
                        }


                        MessageBox.Show("Remito Actualizado!!");
                        return true;
                    }
                }

            }
            return false;
        }
        static public RemitoVenta BuscarPorCodigo(string codigo)
        {
            if (RemitoVentaValidador.GetRemitoVenta(codigo))
            {
                List<RemitoVenta> ListaRemitoVenta  = RemitoVenta.Buscar();
                int i = RemitoVenta.BuscarIndexPorCodigo(codigo, ListaRemitoVenta);
                if (i < ListaRemitoVenta.Count())
                {
                    return ListaRemitoVenta[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoVenta> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public List<RemitoVenta> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.RemitoVentas);
            string l = "";
            string[] dat;
            List<RemitoVenta> ListaDeRemitoVenta  = new List<RemitoVenta>();
            int lErr = 0;
            while ((l = p.ReadLine()) != null)
            {
                lErr++;
                dat = l.Split('|');
                RemitoVenta newRemitoVenta  = new RemitoVenta();
                newRemitoVenta.Codigo = dat[0];
                newRemitoVenta.Emisor = dat[1];
                newRemitoVenta.Receptor = dat[2];

                // leer pago por cada boleta 
                string[] ListaPagos = dat[3].Split('*');
                for (int i = 0; i < ListaPagos.Length; i = i + 3)
                {
                    try {
                        Pago n = new Pago();
                        n.Codigo = ListaPagos[i];
                        n.Nombre = ListaPagos[i + 1];
                        n.Importe = double.Parse(ListaPagos[i + 2]);
                        newRemitoVenta.Pagos.Add(n);
                    } catch (Exception) {

                        MessageBox.Show("Error en la linea "+lErr+". Consultar Servicio");
                    }
                }
                // leer lista de productos por cada boleta 
                string[] ListaProductos = dat[4].Split('*');

                for (int i = 0; i < ListaProductos.Count(); i = i + 5)
                {

                    Producto pr = new Producto();
                    pr.Codigo = ListaProductos[i];
                    pr.Nombre = ListaProductos[i + 1];
                    pr.Bulto= double.Parse(ListaProductos[i + 2]);
                    pr.Cantidad = double.Parse(ListaProductos[i + 3]);
                    pr.Precio = double.Parse(ListaProductos[i + 4]);
                    newRemitoVenta.ListaDeProductos.Add(pr);
                    
                }
                newRemitoVenta.FechaEmision = dat[5];
                try { newRemitoVenta.Directo = bool.Parse(dat[6]); } catch (Exception) { }
                ListaDeRemitoVenta.Add(newRemitoVenta);
            }
            p.Close(); p.Dispose();

            return ListaDeRemitoVenta;
        }
        static public bool Guardar(List<RemitoVenta> x)
        {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.RemitoVentas);
            for (int i = 0; i < x.Count(); i++)
            {
                p.WriteLine(
                    x[i].Codigo + "|" +
                    x[i].Emisor + "|" +
                    x[i].Receptor + "|" +
                    x[i].ListadoPagos() + "|" +
                    x[i].ListadoProductos() + "|" +
                    x[i].FechaEmision + "|" +
                    x[i].Directo.ToString()
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public List<RemitoVenta> BuscarPorFecha(string fecheDesde, string fechaHasta) {
            List<RemitoVenta> x = RemitoVenta.Buscar();
            List<RemitoVenta> y = new List<RemitoVenta>();
            for (int i =0; i<x.Count();i++) {

                if ((DateTime.Parse(x[i].FechaEmision) > DateTime.Parse(fecheDesde))
                    ||(DateTime.Parse(x[i].FechaEmision) == DateTime.Parse(fecheDesde))) {

                        if ((DateTime.Parse(x[i].FechaEmision) < DateTime.Parse(fechaHasta))
                             || (DateTime.Parse(x[i].FechaEmision) == DateTime.Parse(fechaHasta)))
                               {
                                y.Add(x[i]);
                                }

                    }
            }
            return y;
        }
        static public List<RemitoVenta> BuscarPorCliente(Cliente cli)
        {
            List<RemitoVenta> x = RemitoVenta.Buscar();
            List<RemitoVenta> y = new List<RemitoVenta>();
            for (int i = 0; i < x.Count(); i++)
            {

                if (x[i].Receptor==cli.Nombre) {
                    y.Add(x[i]);
                }
            }
            return y;
        }
        static public List<RemitoVenta> BuscarPorFecha(string fecheDesde, string fechaHasta,Cliente cli)
        {
            List<RemitoVenta> x = RemitoVenta.Buscar();
            List<RemitoVenta> y = new List<RemitoVenta>();
            for (int i = 0; i < x.Count(); i++)
            {

                if ((DateTime.Parse(x[i].FechaEmision) > DateTime.Parse(fecheDesde))
                    || (DateTime.Parse(x[i].FechaEmision) == DateTime.Parse(fecheDesde)))
                {

                    if ((DateTime.Parse(x[i].FechaEmision) < DateTime.Parse(fechaHasta))
                         || (DateTime.Parse(x[i].FechaEmision) == DateTime.Parse(fechaHasta)))
                    {
                        if (x[i].Receptor == cli.Nombre) { y.Add(x[i]); }
                    }

                }
            }
            return y;
        }
        static public void ConsolidarMostrar(List<RemitoVenta> x, ref DataGridView y,ref ListBox z) {

            List<Producto> Lista = new List<Producto>();
            for (int i = 0; i<x.Count();i++) {

                for (int j=0; j<x[i].ListaProdutos.Count();j++) {
                    Producto.AgregarProductosVenta(ref Lista, x[i].ListaProdutos[j]);
                }
            
            }
            z.Items.Clear();

            Producto.SumasDeConsolidados(ref z, Lista);
            Producto.MostrarVentas(ref y, Lista);
        }
        static public void ConsolidarMostrar(List<RemitoVenta> x, ref DataGridView y)
        {

            List<Producto> Lista = new List<Producto>();
            for (int i = 0; i < x.Count(); i++)
            {

                for (int j = 0; j < x[i].ListaProdutos.Count(); j++)
                {
                    Producto.AgregarProductosVenta(ref Lista, x[i].ListaProdutos[j]);
                }

            }
            Producto.MostrarVentas(ref y, Lista);
        }
        static public double[] Egreso(List<RemitoVenta> x, string descripcion) {
            double[] res =new double[8];
            for (int i =0; i<x.Count();i++) {

                for (int j = 0; j < x[i].ListaDeProductos.Count(); j++) {

                    if (Producto.BuscarPorCodigo(x[i].ListaDeProductos[j].Codigo).Descripcion == descripcion)
                    {
                        if (x[i].ListaDeProductos[j].Precio == 0)
                        {
                            // si es bonificado
                            res[0] += x[i].ListaDeProductos[j].Cantidad;
                            res[1] += x[i].ListaDeProductos[j].Precio;
                        }
                        else
                        {
                            switch (x[i].Pagos[0].Nombre)
                            {
                                case "Efectivo":
                                    {
                                        res[2] += x[i].ListaDeProductos[j].Cantidad;
                                        res[3] += x[i].ListaDeProductos[j].ImportePrecio();
                                        break;
                                    }
                                case "Mercado Pago":
                                    {
                                        res[4] += x[i].ListaDeProductos[j].Cantidad;
                                        res[5] += x[i].ListaDeProductos[j].ImportePrecio();
                                        break;
                                    }
                                case "Creditos x Venta":
                                    {
                                        res[6] += x[i].ListaDeProductos[j].Cantidad;
                                        res[7] += x[i].ListaDeProductos[j].ImportePrecio();
                                        break;
                                    }

                            }
                        }
                    }

                }

            } 
            return res;
        }
        static public List<Pago> Egreso(List<RemitoVenta> x)
        {
            List<Pago> lista = new List<Pago>();
            double[] res = new double[8];
            for (int i = 0; i < x.Count(); i++)
            {
                for (int j =0; j<x[i].Pagos.Count();j++) {
                    Pago.AgregarCuenta(ref lista, x[i].Pagos[j]);
                } 

            }
            return lista;
        }
        static public List<Pago> Egreso(List<RemitoVenta> x,bool directo)
        {
            List<Pago> lista = new List<Pago>();
            double[] res = new double[8];
            for (int i = 0; i < x.Count(); i++)
            {
                for (int j = 0; j < x[i].Pagos.Count(); j++)
                {
                    if (directo==x[i].Directo) { Pago.AgregarCuenta(ref lista, x[i].Pagos[j]); }
                    
                }

            }
            return lista;
        }
        static public double CostoTotal(List<RemitoVenta> x) {
            double sum = 0;
            foreach (RemitoVenta rv in x) {
                sum += rv.TotalCosto();
                
            }
            return sum;
        }
        static public double VentaTotal(List<RemitoVenta> x)
        {
            double sum = 0;
            foreach (RemitoVenta rv in x)
            {
                sum += rv.TotalVenta();
            }
            return sum;
        }

    }
}
