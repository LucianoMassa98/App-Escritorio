using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class InformeService
    {
        // listado ventas , registro, produccion, compras y registradora
        static public List<RemitoVenta> FiltrarPorPago(List<RemitoVenta> x,string tipo) {

            List<RemitoVenta> list = new List<RemitoVenta>(); 
            for (int i = 0; i<x.Count();  i++) {

                if (x[i].Pagos[0].Nombre== tipo){ 
                list.Add(x[i]);
                }
            
            }

            return list;
        }
        static public List<RemitoVenta> FiltrarPorDescripcion(List<RemitoVenta> x, string descripcion) {
            
            List<RemitoVenta> list = new List<RemitoVenta>();
            for (int i = 0; i < x.Count(); i++)
            {
                int j = 0;
                
                while ((j< x[i].ListaProdutos.Count()) 
                    &&(Producto.BuscarPorCodigo(x[i].ListaProdutos[j].Codigo).Descripcion != descripcion)) { j++; }

                if (j<x[i].ListaProdutos.Count()) {
                    
                    RemitoVenta nuevoRemito = x[i];
                    for (int k =0; k< x[i].ListaProdutos.Count();k++) {

                        if (Producto.BuscarPorCodigo(x[i].ListaProdutos[k].Codigo).Descripcion != descripcion) 
                        {
                            nuevoRemito.EliminarProducto(x[i].ListaProdutos[k].Codigo); 
                        }
                    }
                    list.Add(nuevoRemito);
                }

            }

            return list;

        }
        static public List<RemitoCompra> FiltrarPorPago(List<RemitoCompra> x, string tipo)
        {
            List<RemitoCompra> list = new List<RemitoCompra>();
            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i].Pagos[0].Nombre == tipo)
                {
                    list.Add(x[i]);
                }

            }
            return list;
        }

        
        static public double CantidadBonificados(List<RemitoVenta> x)
        {
            double cnt = 0; 
            for (int i = 0; i < x.Count(); i++)
            {
                for (int j = 0; j < x[i].ListaProdutos.Count(); j++)
                {
                    if (x[i].ListaProdutos[j].Precio == 0)
                    {
                        cnt += x[i].ListaProdutos[j].Cantidad;
                    }

                }
            }
            return cnt;
        }

        static public double Cantidad(List<RemitoVenta> x)
        {
            double cnt = 0;
            for (int i = 0; i < x.Count(); i++)
            {
                for (int j = 0; j < x[i].ListaProdutos.Count(); j++)
                {
                    if (x[i].ListaProdutos[j].Precio != 0)
                    {
                        cnt += x[i].ListaProdutos[j].Cantidad;
                    }

                }
            }
            return cnt;
        }
        static public double Cantidad(List<RemitoRegistradora> x)
        {
            double cnt = 0;
            for (int i = 0; i < x.Count(); i++)
            {
                for (int j = 0; j < x[i].ListaProdutos.Count(); j++)
                {
                    if (x[i].ListaProdutos[j].Precio != 0)
                    {
                        cnt += x[i].ListaProdutos[j].Cantidad;
                    }

                }
            }
            return cnt;
        }

        static public double Monto(List<RemitoVenta> x) {
            double sum = 0;
            for (int i = 0; i < x.Count(); i++)
            {
                sum += x[i].TotalVenta();
            }
            return sum;
        }

        static public double Monto(List<RemitoCompra> x)
        {
            double sum = 0;
            for (int i = 0; i < x.Count(); i++)
            {
                sum += x[i].TotalCosto();
            }
            return sum;
        }

        static public double Monto(List<RemitoRegistradora> x)
        {
            double sum = 0;
            for (int i = 0; i < x.Count(); i++)
            {
                sum += x[i].Total();
            }
            return sum;
        }

    }
}
