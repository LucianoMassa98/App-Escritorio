using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static  class RemitoProduccionValidator
    {
        static public bool CrearRemitoProduccion(ref RemitoProduccion x)
        {

            if (x.Emisor == null) { return false; }
            if (x.Receptor=="") { return false; }
            if (x.FechaEmision == "") { return false; }
            if (x.ListaProdutos.Count() == 0) { return false; }
            try
            {
                List<RemitoProduccion> Lista = RemitoProduccion.Buscar();
                x.Codigo = (Lista.Count() + 1).ToString();
            }
            catch (Exception) { return false; }


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
                x.Precio = Stock[i].Costo;
                x.Descripcion = Stock[i].Descripcion;
                return true;
            }

            if ((i = Producto.BuscarIndexPorNombre(x.Nombre, Stock)) < Stock.Count())
            {

                x.Codigo = Stock[i].Codigo;
                x.Descripcion = Stock[i].Descripcion;
                x.Costo = Stock[i].Costo;
                return true;

            }
            return false;
        }
        static public bool GetRemitoProduccion(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoProduccion(string codigo, RemitoProduccion x)
        {

            return RemitoProduccionValidator.GetRemitoProduccion(codigo);

        }

    }
}
