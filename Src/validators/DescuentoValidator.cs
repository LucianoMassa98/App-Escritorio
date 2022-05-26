using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class DescuentoValidador
    {
        static public bool CrearDescuento(ref Descuento x)
        {
            if (x.Nombre == "") { return false; }
            if (x.ImporteDescuento < -1) { return false; }
            if (x.CantidadRequerida < -1) { return false; }
            List<Descuento> Lista = Descuento.Buscar();
            Descuento y = Descuento.BuscarPorNombre(x.Nombre);
            if (y != null) { return false; }
            x.Codigo = (Lista.Count() + 1).ToString();
            return true;
        }
        static public bool ActualizarDescuento(string codigo, Descuento x)
        {
            return DescuentoValidador.GetDescuento(codigo);

        }
        static public bool GetDescuento(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
    }
}

