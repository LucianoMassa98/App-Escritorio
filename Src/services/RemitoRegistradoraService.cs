using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace E_Shop
{
    class RemitoRegistradora
    {
        string codigo;
        string emisor;
        string receptor;
        string fechaEmision;
        Pago xpago;
        List<Producto> ListaDeProductos;

        public RemitoRegistradora() {
            ListaDeProductos = new List<Producto>();
        }

        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Emisor { get { return emisor; } set { emisor = value; } }
        public string Receptor { get { return receptor; } set { receptor = value; } }
        public string FechaEmision { get { return fechaEmision; } set { fechaEmision = value; } }
        public Pago Xpago { get { return xpago; } set { xpago = value; } }
        public List<Producto> ListaProdutos { get { return ListaDeProductos; } set { ListaDeProductos = value; } }
        public double Total() {
            double sum = 0;
            for (int i = 0; i < this.ListaDeProductos.Count(); i++) {
                sum += this.ListaDeProductos[i].Precio;
            }
            return sum;
        }

        public string ListadoProductos()
        {

            string var = "";
            for (int i = 0; i < ListaDeProductos.Count(); i++)
            {

                var = var + (

                    ListaDeProductos[i].Codigo + "/" +
                     ListaDeProductos[i].Nombre + "/" +
                      ListaDeProductos[i].Cantidad + "/" +
                       ListaDeProductos[i].Precio

                       );
                if (i + 1 < ListaDeProductos.Count()) { var = var + "/"; }

            }

            return var;
        }
        public bool AgregarProducto(Producto x)
        {
            if (RemitoRegistradoraValidador.AgregarProducto(ref x))
            {
                ListaDeProductos.Add(x);
                return true;
            }
            return false;
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
                    this.ListaProdutos[i].Cantidad,
                    this.ListaProdutos[i].Precio / this.ListaProdutos[i].Cantidad,
                    this.ListaProdutos[i].Precio
                    );
            }
        }
        public string ListadoPago() {

            return xpago.Codigo + "/" + xpago.Nombre + "/" + xpago.Importe;
        }

        static public bool Crear(RemitoRegistradora x)
        {

            if (RemitoRegistradoraValidador.CrearRemitoRegistradora(x))
            {

                List<RemitoRegistradora> ListaRemitoRegistradora = RemitoRegistradora.Buscar();
                ListaRemitoRegistradora.Add(x);
                RemitoRegistradora.Guardar(ListaRemitoRegistradora);
                Producto.RestarStock(x.ListaDeProductos);
                List<Pago> xpg = new List<Pago>();
                xpg.Add(x.Xpago);
                Pago.SumarCuenta(xpg);
                return true;
            }
            new Alert("no cumple con los parametros de entrada").Show();
            return false;
        }
        static public bool Borrar(string codigo)
        {

            if (RemitoRegistradoraValidador.GetRemitoRegistradora(codigo))
            {
                List<RemitoRegistradora> ListaRemitoRegistradora = RemitoRegistradora.Buscar();
                int i = RemitoRegistradora.BuscarIndexPorCodigo(codigo, ListaRemitoRegistradora);
                if (i < ListaRemitoRegistradora.Count())
                {
                    ListaRemitoRegistradora.RemoveAt(i);
                    return RemitoRegistradora.Guardar(ListaRemitoRegistradora);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoRegistradora x)
        {

            if (RemitoRegistradoraValidador.ActualizarRemitoRegistradora(codigo, x))
            {
                List<RemitoRegistradora> ListaRemitoRegistradora = RemitoRegistradora.Buscar();
                int i = RemitoRegistradora.BuscarIndexPorCodigo(codigo, ListaRemitoRegistradora);
                if (i < ListaRemitoRegistradora.Count())
                {
                    if (x.Emisor != "") { ListaRemitoRegistradora[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemitoRegistradora[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemitoRegistradora[i].FechaEmision = x.FechaEmision; }
                    ListaRemitoRegistradora[i].ListaProdutos = x.ListaProdutos;

                    return RemitoRegistradora.Guardar(ListaRemitoRegistradora);
                }

            }
            return false;
        }
        static public RemitoRegistradora BuscarPorCodigo(string codigo)
        {
            if (RemitoRegistradoraValidador.GetRemitoRegistradora(codigo))
            {
                List<RemitoRegistradora> ListaRemitoRegistradora = RemitoRegistradora.Buscar();
                int i = RemitoRegistradora.BuscarIndexPorCodigo(codigo, ListaRemitoRegistradora);
                if (i < ListaRemitoRegistradora.Count())
                {
                    return ListaRemitoRegistradora[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoRegistradora> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }



        //  FALTA GUARDAR Y BUSCAR


        static public List<RemitoRegistradora> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.RemitoRegistradoras);
            string l = "";
            string[] dat;
            List<RemitoRegistradora> ListaDeRemitoRegistradora = new List<RemitoRegistradora>();

            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                RemitoRegistradora newRemitoRegistradora = new RemitoRegistradora();
                newRemitoRegistradora.Codigo = dat[0];
                newRemitoRegistradora.Emisor = dat[1];
                string[] ListaPagos = dat[2].Split('/');
                Pago n = new Pago();
                n.Codigo = ListaPagos[0];
                n.Nombre = ListaPagos[1];
                n.Importe = double.Parse(ListaPagos[2]);
                newRemitoRegistradora.Xpago = n; ;
                // leer lista de productos por cada boleta (falta desarrollar)
                string[] ListaProductos = dat[3].Split('/');
                for (int i = 0; i < ListaProductos.Count(); i = i + 4)
                {

                    Producto pr = new Producto();
                    pr.Codigo = ListaProductos[i];
                    pr.Nombre = ListaProductos[i + 1];
                    pr.Cantidad = double.Parse(ListaProductos[i + 2]);
                    pr.Precio = double.Parse(ListaProductos[i + 3]);
                    newRemitoRegistradora.ListaDeProductos.Add(pr);

                }
                newRemitoRegistradora.fechaEmision = dat[4];
                ListaDeRemitoRegistradora.Add(newRemitoRegistradora);
            }
            p.Close(); p.Dispose();

            return ListaDeRemitoRegistradora;
        }
        static public bool Guardar(List<RemitoRegistradora> x)
        {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.RemitoRegistradoras);
            for (int i = 0; i < x.Count(); i++)
            {
                p.WriteLine(
                   x[i].Codigo + "|" +
                   x[i].Emisor + "|" +
                   x[i].ListadoPago() + "|" +
                   x[i].ListadoProductos() + "|" +
                   x[i].FechaEmision
                   );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public List<RemitoRegistradora> BuscarPorFecha(string fecheDesde, string fechaHasta)
        {
            List<RemitoRegistradora> x = RemitoRegistradora.Buscar();
            List<RemitoRegistradora> y = new List<RemitoRegistradora>();
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


        static public void Egreso(List<RemitoRegistradora> x, ref Label cnt, ref Label imp, string descripcion ) {

            double sumcnt = 0, sumimp = 0;
            for (int i =0; i<x.Count();i++) {

                for (int j =0; j<x[i].ListaDeProductos.Count();j++) {

                    if (descripcion == Producto.BuscarPorCodigo(x[i].ListaDeProductos[j].Codigo).Descripcion) {

                        sumcnt += x[i].ListaDeProductos[j].Cantidad;
                        sumimp += x[i].ListaDeProductos[j].Precio;
                    
                    }
                
                }
            
            
            }

            cnt.Text = sumcnt.ToString();
            imp.Text = "$" + sumimp.ToString();
        
        }
        static public double Egreso(List<RemitoRegistradora> x)
        {
            double sumimp = 0;
            for (int i = 0; i < x.Count(); i++)
            {

                for (int j = 0; j < x[i].ListaDeProductos.Count(); j++)
                {
                        sumimp += x[i].ListaDeProductos[j].Precio;
                }


            }
            return sumimp;

        }

    }
}
