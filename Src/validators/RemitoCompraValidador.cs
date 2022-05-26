using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class RemitoCompraValidador
    {
        static public bool CrearRemitoCompra(ref RemitoCompra x)
        {
            
            if (x.Emisor == "") { return false; }
            if (x.Receptor == "") { return false; }
            if (x.FechaEmision == "") { return false; }
            if (x.Pagos.Count()==0) { return false; }
            if (x.ListaProdutos.Count()==0) { return false; }
            try { x.Pagos[0].Importe = x.TotalCosto();
                List<RemitoCompra> Lista = RemitoCompra.Buscar();
                x.Codigo = (Lista.Count() + 1).ToString();
            } catch (Exception) { return false; }

           
            return true;
        }
        static public bool AgregarProducto(ref Producto x)
        {
            if (x.Cantidad == 0) { return false; }
            List<Producto> Stock = Producto.Buscar();
            int i;
           

            if ((i = Producto.BuscarIndexPorCodigo(x.Codigo, Stock)) < Stock.Count())
            {

                x.Nombre = Stock[i].Nombre;
                x.Descripcion = Stock[i].Descripcion;
               // x.Costo = Stock[i].Costo;
                return true;
            }

            if ((i = Producto.BuscarIndexPorNombre(x.Nombre, Stock)) < Stock.Count())
            {

                x.Codigo = Stock[i].Codigo;
                x.Descripcion = Stock[i].Descripcion;   
                //x.Costo = Stock[i].Costo;
                return true;

            }
            return false;
        }
        static public bool GetRemitoCompra(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoCompra(string codigo, RemitoCompra x)
        {

            return RemitoCompraValidador.GetRemitoCompra(codigo);

        }

    }
}
