﻿using System;
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

                    ListaDeProductos[i].Codigo + "/" +
                     ListaDeProductos[i].Nombre + "/" +
                      ListaDeProductos[i].Cantidad + "/" +
                       ListaDeProductos[i].Costo
                       
                       );
                if (i+1<ListaDeProductos.Count()) { var = var + "/";  }

            }

            return var;
        }
        public string ListadoPagos() {

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
                    this.ListaProdutos[i].Cantidad, 
                    this.ListaProdutos[i].Costo,
                    this.ListaProdutos[i].ImporteCosto()
                    );
            }
        }

        static public bool Crear(RemitoCompra x){
            
            if(RemitoCompraValidador.CrearRemitoCompra(ref x)){

               List<RemitoCompra> ListaRemitoCompras = RemitoCompra.Buscar();
               ListaRemitoCompras.Add(x);
                if (RemitoCompra.Guardar(ListaRemitoCompras)) {

                    Producto.SumarStock(x.ListaDeProductos);
                    Pago.RestarCuenta(x.Pagos);
                    if (x.Pagos[0].Codigo == "2.1.1")
                    {

                        Proveedor.RestarSaldo(Proveedor.BuscarPorNombre(x.Emisor).Codigo, x.TotalCosto());

                    }
                    return true;
                }
                return false;
                
            }return false;
        }
        static public bool Borrar(string codigo)
        {

            if (RemitoCompraValidador.GetRemitoCompra(codigo))
            {
                List<RemitoCompra> ListaRemitoCompra = RemitoCompra.Buscar();
                int i = RemitoCompra.BuscarIndexPorCodigo(codigo, ListaRemitoCompra);
                if (i < ListaRemitoCompra.Count())
                {
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
                    ListaRemitoCompra[i].Pagos = x.Pagos;
                    ListaRemitoCompra[i].ListaProdutos = x.ListaProdutos;
                    return RemitoCompra.Guardar(ListaRemitoCompra);
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
            List<RemitoCompra> ListaDeRemitoCompras = new List<RemitoCompra>();

            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                RemitoCompra newRemitoCompra = new RemitoCompra();
                newRemitoCompra.Codigo = dat[0];
                newRemitoCompra.Emisor = dat[1];
                newRemitoCompra.Receptor = dat[2];

                // leer pago por cada boleta (falta desarrollar)
                string []ListaPagos = dat[3].Split('/');
                for (int i =0; i<ListaPagos.Length;i=i+3) {
                    Pago n = new Pago();
                    n.Codigo = ListaPagos[i];
                    n.Nombre = ListaPagos[i+1];
                    n.Importe = double.Parse(ListaPagos[i+2]);
                    newRemitoCompra.Pagos.Add(n);
                }
                // leer lista de productos por cada boleta (falta desarrollar)
                string[]ListaProductos = dat[4].Split('/');

                for (int i = 0; i<ListaProductos.Count(); i = i +4) {

                    Producto pr = new Producto();
                    pr.Codigo = ListaProductos[i];
                    pr.Nombre = ListaProductos[i + 1];
                    pr.Cantidad = double.Parse(ListaProductos[i+2]);
                    pr.Costo = double.Parse(ListaProductos[i + 3]);
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
        static public void Ingreso(List<RemitoCompra> x, ref Label cnt, ref Label imp)
        {
            double sumcnt = 0, sumimp = 0;
            for (int i = 0; i < x.Count(); i++)
            {
                sumcnt += x[i].TotalProductos();
                sumimp += x[i].TotalCosto();
            }
            cnt.Text = sumcnt.ToString();
            imp.Text = "$" + sumimp;

        }

    }
}
