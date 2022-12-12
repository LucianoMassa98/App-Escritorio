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
    class RemitoIngreso
    {
        string emisor;
        string codigo;
        string receptor;
        string fechaEmision;
        List<Producto> ListaDeProductos;
        bool directo,pagado;

        public RemitoIngreso() {
            ListaDeProductos = new List<Producto>();
            directo = true;
        }
        public string FechaEmision { get { return fechaEmision; } set { fechaEmision= value; } }
        public string Receptor { get { return receptor; }set { receptor = value; } }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Emisor { get { return emisor;  }set { emisor = value; } }
        public List<Producto> ListaProdutos{ get { return ListaDeProductos; } set { ListaDeProductos = value; } }
        public bool Directo { get{ return directo; }set { directo = value; }}
        public bool Pagado { get { return pagado; } set { pagado  = value; } }
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
                       ListaDeProductos[i].Costo

                       );
                if (i + 1 < ListaDeProductos.Count()) { var = var + "*"; }

            }

            return var;
        }
     
        public bool AgregarProducto(Producto x)
        {
            if (RemitoIngresoValidador.AgregarProducto(ref x))
            {
                ListaDeProductos.Add(x);
                return true;
            }
            return false;
        }
        public bool EliminarProducto(string codigo)
        {
            if (RemitoIngresoValidador.GetRemitoIngreso(codigo))
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
                    this.ListaProdutos[i].Costo,
                    this.ListaProdutos[i].ImporteCosto()
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
        static public bool Crear(RemitoIngreso x)
        {

            RemitoIngreso remitoX = RemitoIngreso.BuscarPorCodigo(x.Codigo);
            if (remitoX==null) {
                if (RemitoIngresoValidador.CrearRemitoIngreso(x))
                {

                    List<RemitoIngreso> ListaRemito= RemitoIngreso.Buscar();
                    ListaRemito.Add(x);
                    if (RemitoIngreso.Guardar(ListaRemito))
                    {
                        MessageBox.Show("Remito Creado");
                        return true;
                    }

                    return false;
                }
            }
            else
            {
              
             return RemitoIngreso.Actualizar(x.Codigo,x);
            }

            
            return false;
        }
        static public bool Borrar(string codigo)
        {
            if (RemitoIngresoValidador.GetRemitoIngreso(codigo))
            {
                List<RemitoIngreso> ListaRemito= RemitoIngreso.Buscar();
                int i = RemitoIngreso.BuscarIndexPorCodigo(codigo, ListaRemito);
                if (i < ListaRemito.Count())
                {
                    ListaRemito.RemoveAt(i);
                    return RemitoIngreso.Guardar(ListaRemito);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoIngreso x)
        {

            if (RemitoIngresoValidador.ActualizarRemitoIngreso(codigo, x))
            {
                List<RemitoIngreso> ListaRemito = RemitoIngreso.Buscar();
                int i = RemitoIngreso.BuscarIndexPorCodigo(codigo, ListaRemito);
                if (i < ListaRemito.Count())
                {
                    
                    if (x.Emisor != "") { ListaRemito[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemito[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemito[i].FechaEmision = x.FechaEmision; }

                 
                    ListaRemito[i].ListaProdutos = x.ListaProdutos;

                   
                    if (RemitoIngreso.Guardar(ListaRemito))
                    {
                        MessageBox.Show("Remito Actualizado!!");
                        return true;
                    }
                }

            }
            return false;
        }
        static public RemitoIngreso BuscarPorCodigo(string codigo)
        {
            if (RemitoIngresoValidador.GetRemitoIngreso(codigo))
            {
                List<RemitoIngreso> ListaRemito  = RemitoIngreso.Buscar();
                int i = RemitoIngreso.BuscarIndexPorCodigo(codigo, ListaRemito);
                if (i < ListaRemito.Count())
                {
                    return ListaRemito[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoIngreso> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public List<RemitoIngreso> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.RemitoIngresos);
            string l = "";
            string[] dat;
            List<RemitoIngreso> ListaDeRemito  = new List<RemitoIngreso>();
            int lErr = 0;
            while ((l = p.ReadLine()) != null)
            {
                lErr++;
                dat = l.Split('|');
                RemitoIngreso newRemito  = new RemitoIngreso();
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
                    pr.Costo = double.Parse(ListaProductos[i + 4]);
                    newRemito.ListaDeProductos.Add(pr);
                    
                }
                newRemito.FechaEmision = dat[4];
                try {
                    newRemito.Directo = bool.Parse(dat[5]);
                    newRemito.Pagado = bool.Parse(dat[6]);
                } catch (Exception) { }
                ListaDeRemito.Add(newRemito);
            }
            p.Close(); p.Dispose();

            return ListaDeRemito;
        }
        static public bool Guardar(List<RemitoIngreso> x)
        {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.RemitoIngresos);
            for (int i = 0; i < x.Count(); i++)
            {
                p.WriteLine(
                    x[i].Codigo + "|" +
                    x[i].Emisor + "|" +
                    x[i].Receptor + "|" +
                    x[i].ListadoProductos() + "|" +
                    x[i].FechaEmision + "|" +
                    x[i].Directo.ToString() +"|"+
                    x[i].Pagado.ToString()
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public List<RemitoIngreso> BuscarPorFecha(string fecheDesde, string fechaHasta) {
            List<RemitoIngreso> x = RemitoIngreso.Buscar();
            List<RemitoIngreso> y = new List<RemitoIngreso>();
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
        static public List<RemitoIngreso> BuscarPorCliente(Cliente cli)
        {
            List<RemitoIngreso> x = RemitoIngreso.Buscar();
            List<RemitoIngreso> y = new List<RemitoIngreso>();
            for (int i = 0; i < x.Count(); i++)
            {

                if (x[i].Receptor==cli.Nombre) {
                    y.Add(x[i]);
                }
            }
            return y;
        }
        static public List<RemitoIngreso> BuscarPorFecha(string fecheDesde, string fechaHasta,Cliente cli)
        {
            List<RemitoIngreso> x = RemitoIngreso.Buscar();
            List<RemitoIngreso> y = new List<RemitoIngreso>();
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
        static public void ConsolidarMostrar(List<RemitoIngreso> x, ref DataGridView y,ref ListBox z) {

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
        static public void ConsolidarMostrar(List<RemitoIngreso> x, ref DataGridView y)
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
        static public double[] Egreso(List<RemitoIngreso> x, string descripcion) {
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
    
       
        static public double CostoTotal(List<RemitoIngreso> x) {
            double sum = 0;
            foreach (RemitoIngreso rv in x) {
                sum += rv.TotalCosto();
                
            }
            return sum;
        }
        static public double VentaTotal(List<RemitoIngreso> x)
        {
            double sum = 0;
            foreach (RemitoIngreso rv in x)
            {
                sum += rv.TotalVenta();
            }
            return sum;
        }

    }
}
