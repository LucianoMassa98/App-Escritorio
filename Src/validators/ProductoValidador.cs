using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop{
    static  class ProductoValidador
    {
        static public bool CrearProducto(Producto x)
        {
            if (x.Codigo == "") { return false; }
            if (x.Nombre == "") { return false; }
            if (x.Descripcion == "") { return false; }
            if (x.Cantidad < -1) { return false; }
            if (x.CantidadEstandar < -1) { return false; }
            if (x.Costo < -1) { return false; }
            if (x.Precio < -1) { return false; }
            if (x.Costo > x.Precio) { return false; }
            if (x.Costo == x.Precio) { return false; }
            if (Producto.BuscarPorCodigo(x.Codigo)!=null) { return false; }
            return true;
        }
        static public bool GetProducto(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarProducto(string codigo, Producto x)
        {
            return ProductoValidador.GetProducto(codigo);

        }
       
    }
}
