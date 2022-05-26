using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class RemitoRegistradoraValidador
    {
        static public bool CrearRemitoRegistradora(RemitoRegistradora x)
        {
            if (x.Codigo=="") { return false; }
            if (x.Emisor == "") { return false; }
            if (x.FechaEmision == "") { return false; }
            if (x.ListaProdutos.Count()==0) { return false; }
            try { x.Xpago.Importe = x.Total(); } catch (Exception) { return false; }
            List<RemitoProduccion> Lista = RemitoProduccion.Buscar();
            x.Codigo = (Lista.Count() + 1).ToString();

            return true;
        }
        static public bool GetRemitoRegistradora(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoRegistradora(string codigo, RemitoRegistradora x)
        {

            return (RemitoRegistradoraValidador.CrearRemitoRegistradora(x) 
                && RemitoRegistradoraValidador.GetRemitoRegistradora(codigo));

        }
        static public bool AgregarProducto(ref Producto x)
        {
            if (x.Cantidad == 0) { return false; }
            if (x.Precio < 1) { return false; }
            
            List<Producto> Stock = Producto.Buscar();
            int i;

            if ((i = Producto.BuscarIndexPorCodigo(x.Codigo, Stock)) < Stock.Count())
            {
                x.Nombre = Stock[i].Nombre;
                x.Descripcion = Stock[i].Descripcion;
                return true;
            }
           
            if ((i = Producto.BuscarIndexPorNombre(x.Nombre, Stock)) < Stock.Count())
            {
                x.Codigo = Stock[i].Codigo;
                x.Descripcion = Stock[i].Descripcion;
                return true;

            }
            new Alert(i.ToString() + "-" + Stock.Count()).Show();
            return false;
        }

    }
}
