using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
namespace E_Shop
{
    internal class RemitoProduccion
    {
        string emisor;
        string codigo;
        string receptor;
        string fechaEmision;
        List<Producto> ListaDeProductos;

        public RemitoProduccion()
        {
            ListaDeProductos = new List<Producto>();
        }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Emisor { get { return emisor; } set { emisor = value; } }
        public string Receptor { get { return receptor; } set { receptor = value; } }
        public string FechaEmision { get { return fechaEmision; } set { fechaEmision = value; } }
        public List<Producto> ListaProdutos { get { return ListaDeProductos; } set { ListaDeProductos = value; } }
        public string ListadoProductos()
        {
            string var = "";
            for (int i = 0; i < ListaDeProductos.Count(); i++)
            {

                var = var + (

                    ListaDeProductos[i].Codigo + "/" +
                     ListaDeProductos[i].Nombre + "/" +
                      ListaDeProductos[i].Cantidad + "/" +
                       ListaDeProductos[i].Costo

                       );
                if (i + 1 < ListaDeProductos.Count()) { var = var + "/"; }

            }

            return var;
        }
        public bool AgregarProducto(Producto x)
        {
            if (RemitoProduccionValidator.AgregarProducto(ref x))
            {
                ListaDeProductos.Add(x);
                return true;
           }
            return false;
        }
        public bool EliminarProducto(string codigo)
        {
            if (RemitoProduccionValidator.GetRemitoProduccion(codigo))
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
        public double TotalCosto() { return Producto.SumaCostos(this.ListaDeProductos); }
        public double TotalProductos() {
            double sum = 0;
            for (int i =0; i<ListaDeProductos.Count();i++) { sum += ListaDeProductos[i].Cantidad; }

            return sum;
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
                    this.ListaProdutos[i].Costo,
                    this.ListaProdutos[i].ImporteCosto()
                    );
            }
        }
        static public bool Crear(RemitoProduccion x)
        {

            if (RemitoProduccionValidator.CrearRemitoProduccion(ref x))
            {

                List<RemitoProduccion> data = RemitoProduccion.Buscar();
                data.Add(x);
                if (RemitoProduccion.Guardar(data))
                {
                    Producto.SumarStock(x.ListaDeProductos);
                    return true;
                }
                return false;

            }
            return false;
        }
        static public bool Borrar(string codigo)
        {

            if (RemitoProduccionValidator.GetRemitoProduccion(codigo))
            {
                List<RemitoProduccion> ListaRemitoProduccion = RemitoProduccion.Buscar();
                int i = RemitoProduccion.BuscarIndexPorCodigo(codigo, ListaRemitoProduccion);
                if (i < ListaRemitoProduccion.Count())
                {
                    ListaRemitoProduccion.RemoveAt(i);
                    return RemitoProduccion.Guardar(ListaRemitoProduccion);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, RemitoProduccion x)
        {

            if (RemitoProduccionValidator.ActualizarRemitoProduccion(codigo, x))
            {
                List<RemitoProduccion> ListaRemitoProduccion = RemitoProduccion.Buscar();
                int i = RemitoProduccion.BuscarIndexPorCodigo(codigo, ListaRemitoProduccion);
                if (i < ListaRemitoProduccion.Count())
                {

                    if (x.Emisor != "") { ListaRemitoProduccion[i].Emisor = x.Emisor; }
                    if (x.Receptor != "") { ListaRemitoProduccion[i].Receptor = x.Receptor; }
                    if (x.FechaEmision != "") { ListaRemitoProduccion[i].FechaEmision = x.FechaEmision; }

                    ListaRemitoProduccion[i].ListaProdutos = x.ListaProdutos;
                    return RemitoProduccion.Guardar(ListaRemitoProduccion);
                }

            }
            return false;
        }
        static public List<RemitoProduccion> BuscarPorFecha(string fecheDesde, string fechaHasta)
        {
            List<RemitoProduccion> x = RemitoProduccion.Buscar();
            List<RemitoProduccion> y = new List<RemitoProduccion>();
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

        static public RemitoProduccion BuscarPorCodigo(string codigo)
        {
            if (RemitoProduccionValidator.GetRemitoProduccion(codigo))
            {
                List<RemitoProduccion> ListaRemitoProduccion = RemitoProduccion.Buscar();
                int i = RemitoProduccion.BuscarIndexPorCodigo(codigo, ListaRemitoProduccion);
                if (i < ListaRemitoProduccion.Count())
                {
                    return ListaRemitoProduccion[i];
                }

            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<RemitoProduccion> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public List<RemitoProduccion> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.RemitoProducciones);
            string l = "";
            string[] dat;
            List<RemitoProduccion> ListaDeRemitoProduccion = new List<RemitoProduccion>();

            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                RemitoProduccion newRemitoProduccion = new RemitoProduccion();
                newRemitoProduccion.Codigo = dat[0];
                newRemitoProduccion.Emisor = dat[1];
                newRemitoProduccion.Receptor = dat[2];
                // leer lista de productos por cada boleta (falta desarrollar)
                string[] ListaProductos = dat[3].Split('/');

                for (int i = 0; i < ListaProductos.Count(); i = i + 4)
                {

                    Producto pr = new Producto();
                    pr.Codigo = ListaProductos[i];
                    pr.Nombre = ListaProductos[i + 1];
                    pr.Cantidad = double.Parse(ListaProductos[i + 2]);
                    pr.Costo = double.Parse(ListaProductos[i + 3]);
                    newRemitoProduccion.ListaDeProductos.Add(pr);
                }
                newRemitoProduccion.FechaEmision = dat[4];
                ListaDeRemitoProduccion.Add(newRemitoProduccion);

            }
            p.Close(); p.Dispose();

            return ListaDeRemitoProduccion;
        }
        static public bool Guardar(List<RemitoProduccion> x)
        {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.RemitoProducciones);
            for (int i = 0; i < x.Count(); i++)
            {
                p.WriteLine(
                    x[i].Codigo + "|" +
                    x[i].Emisor + "|" +
                    x[i].Receptor + "|" +
                    x[i].ListadoProductos() + "|" +
                    x[i].FechaEmision
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }

        static public void Ingreso(List<RemitoProduccion> x, ref Label cnt, ref Label imp)
        {
            double sumcnt = 0, sumimp = 0;
            for (int i =0; i<x.Count();i++) {
                sumcnt += x[i].TotalProductos();
                sumimp += x[i].TotalCosto();
            }
            cnt.Text = sumcnt.ToString();
            imp.Text = "$" + sumimp;
        
        }
    }
}
