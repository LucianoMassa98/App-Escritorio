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
    class RemitoEgreso
    {
        string emisor;
        string codigo;
        string receptor;
        string fechaEmision;
        List<Producto> ListaDeProductos;
        bool directo,cobrado;

        public RemitoEgreso() {
            ListaDeProductos = new List<Producto>();
            directo = true;
        }
        public string FechaEmision { get { return fechaEmision; } set { fechaEmision= value; } }
        public string Receptor { get { return receptor; }set { receptor = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Emisor { get { return emisor;  }set { emisor = value; } }
        public List<Producto> ListaProdutos{ get { return ListaDeProductos; } set { ListaDeProductos = value; } }
        public bool Directo { get{ return directo; }set { directo = value; }}
        public bool Cobrado { get { return cobrado; } set { cobrado = value; } }
        public bool EnviarFacturaAfip() {
            return true;
        }
        public double TotalVenta() { 
            return Producto.SumaVentas(this.ListaDeProductos); }
        
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
     
        public bool AgregarProducto(Producto x)
        {
            if (RemitoEgresoValidador.AgregarProducto(ref x))
            {
                ListaDeProductos.Add(x);
                return true;
            }
            return false;
        }
        public bool EliminarProducto(string codigo)
        {
            if (RemitoEgresoValidador.GetRemitoEgreso(codigo))
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
        static public bool Crear(RemitoEgreso x)
        {

            RemitoEgreso remitoX = RemitoEgreso.BuscarPorCodigo(x.Codigo);
            if (remitoX==null) {
                if (RemitoEgresoValidador.CrearRemitoEgreso(x))
                {

                    List<RemitoEgreso> ListaRemitoVenta = RemitoEgreso.Buscar();
                    ListaRemitoVenta.Add(x);
                    if (RemitoEgreso.Guardar(ListaRemitoVenta))
                    {
                        MessageBox.Show("Remito Creado");
                        return true;
                    }

                    return false;
                }
            }
            else
            {
              
             return RemitoEgreso.Actualizar(x.Codigo,x);
            }

            
            return false;
        }
        static public bool Borrar(string codigo)
        {
            if (RemitoEgresoValidador.GetRemitoEgreso(codigo))
            {
                List<RemitoEgreso> ListaRemitoVenta = RemitoEgreso.Buscar();
                int i = RemitoEgreso.BuscarIndexPorCodigo(codigo, ListaRemitoVenta);
                if (i < ListaRemitoVenta.Count())
                {
                    ListaRemitoVenta.RemoveAt(i);
                    return RemitoEgreso.Guardar(ListaRemitoVenta);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoEgreso x)
        {

            if (RemitoEgresoValidador.ActualizarRemitoEgreso(codigo, x))
            {
                List<RemitoEgreso> ListaRemitoVenta = RemitoEgreso.Buscar();
                int i = RemitoEgreso.BuscarIndexPorCodigo(codigo, ListaRemitoVenta);
                if (i < ListaRemitoVenta.Count())
                {
                    
                    if (x.Emisor != "") { ListaRemitoVenta[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemitoVenta[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemitoVenta[i].FechaEmision = x.FechaEmision; }

                 
                    ListaRemitoVenta[i].ListaProdutos = x.ListaProdutos;

                   
                    if (RemitoEgreso.Guardar(ListaRemitoVenta))
                    {
                        MessageBox.Show("Remito Actualizado!!");
                        return true;
                    }
                }

            }
            return false;
        }
        static public RemitoEgreso BuscarPorCodigo(string codigo)
        {
            if (RemitoEgresoValidador.GetRemitoEgreso(codigo))
            {
                List<RemitoEgreso> ListaRemitoVenta  = RemitoEgreso.Buscar();
                int i = RemitoEgreso.BuscarIndexPorCodigo(codigo, ListaRemitoVenta);
                if (i < ListaRemitoVenta.Count())
                {
                    return ListaRemitoVenta[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoEgreso> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public List<RemitoEgreso> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.RemitoEgresos);
            string l = "";
            string[] dat;
            List<RemitoEgreso> ListaDeRemitoVenta  = new List<RemitoEgreso>();
            int lErr = 0;
            while ((l = p.ReadLine()) != null)
            {
                lErr++;
                dat = l.Split('|');
                RemitoEgreso newRemito  = new RemitoEgreso();
                newRemito.Codigo = dat[0];
                newRemito.Emisor = dat[1];
                newRemito.Receptor = dat[2];

                // leer lista de productos por cada boleta 
                string[] ListaProductos = dat[3].Split('*');

                for (int i = 0; i < ListaProductos.Count(); i = i + 5)
                {

                    Producto pr = new Producto();
                    pr.Codigo = ListaProductos[i];
                    pr.Nombre = ListaProductos[i + 1];
                    pr.Bulto= double.Parse(ListaProductos[i + 2]);
                    pr.Cantidad = double.Parse(ListaProductos[i + 3]);
                    pr.Precio = double.Parse(ListaProductos[i + 4]);
                    newRemito.ListaDeProductos.Add(pr);
                    
                }
                newRemito.FechaEmision = dat[4];
                try {
                    newRemito.Directo = bool.Parse(dat[5]);
                    newRemito.Cobrado = bool.Parse(dat[6]);
                } catch (Exception) { }
                ListaDeRemitoVenta.Add(newRemito);
            }
            p.Close(); p.Dispose();

            return ListaDeRemitoVenta;
        }
        static public bool Guardar(List<RemitoEgreso> x)
        {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.RemitoEgresos);
            for (int i = 0; i < x.Count(); i++)
            {
                p.WriteLine(
                    x[i].Codigo + "|" +
                    x[i].Emisor + "|" +
                    x[i].Receptor + "|" +
                    x[i].ListadoProductos() + "|" +
                    x[i].FechaEmision + "|" +
                    x[i].Directo.ToString() +"|"+
                    x[i].Cobrado.ToString()
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public List<RemitoEgreso> BuscarPorFecha(string fecheDesde, string fechaHasta) {
            List<RemitoEgreso> x = RemitoEgreso.Buscar();
            List<RemitoEgreso> y = new List<RemitoEgreso>();
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
        static public List<RemitoEgreso> BuscarPorCliente(Cliente cli)
        {
            List<RemitoEgreso> x = RemitoEgreso.Buscar();
            List<RemitoEgreso> y = new List<RemitoEgreso>();
            for (int i = 0; i < x.Count(); i++)
            {

                if (x[i].Receptor==cli.Nombre) {
                    y.Add(x[i]);
                }
            }
            return y;
        }
        static public List<RemitoEgreso> BuscarPorFecha(string fecheDesde, string fechaHasta,Cliente cli)
        {
            List<RemitoEgreso> x = RemitoEgreso.Buscar();
            List<RemitoEgreso> y = new List<RemitoEgreso>();
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
        static public void ConsolidarMostrar(List<RemitoEgreso> x, ref DataGridView y,ref ListBox z) {

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
        static public void ConsolidarMostrar(List<RemitoEgreso> x, ref DataGridView y)
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
        static public double[] Egreso(List<RemitoEgreso> x, string descripcion) {
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
                        }
                    }

                }

            } 
            return res;
        }
    
       
        static public double CostoTotal(List<RemitoEgreso> x) {
            double sum = 0;
            foreach (RemitoEgreso rv in x) {
                sum += rv.TotalCosto();
                
            }
            return sum;
        }
        static public double VentaTotal(List<RemitoEgreso> x)
        {
            double sum = 0;
            foreach (RemitoEgreso rv in x)
            {
                sum += rv.TotalVenta();
            }
            return sum;
        }

    }
}
