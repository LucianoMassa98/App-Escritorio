using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class PagoValidador
    {
        static public bool CrearPago(ref Pago x)
        {
            if (x.Nombre == "") { return false; }
            if (x.Codigo=="") { return false; }
            List<Pago> Lista = Pago.Buscar();
            Pago y = Pago.BuscarPorNombre(x.Nombre);
            if (y != null) { return false; }
            y = Pago.BuscarPorCodigo(x.Codigo);
            if (y != null) { return false; }

            return true;
        }
        static public bool ActualizarPago(string codigo, Pago x)
        {
            return PagoValidador.GetPago(codigo);

        }
        static public bool GetPago(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
    }
}

