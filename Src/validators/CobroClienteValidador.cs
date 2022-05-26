using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class RemitoCobroClienteValidador
    {
        static public bool CrearRemitoCobroCliente(ref RemitoCobroCliente x)
        {
            
            if (x.Emisor == "") { return false; }
            if (x.Receptor == "") { return false; }
            if (x.FechaEmision == "") { return false; }
            if (x.Pagos.Count()==0) { return false; }
            try { x.Pagos[0].Importe = x.TotalCosto();
                List<RemitoCobroCliente> Lista = RemitoCobroCliente.Buscar();
                x.Codigo = (Lista.Count() + 1).ToString();
            } catch (Exception) { return false; }

           
            return true;
        }
        static public bool AgregarPago(ref Pago x)
        {
            if (x.Importe == 0) { return false; }
            
            return true;
        }
        static public bool GetRemitoCobroCliente(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoCobroCliente(string codigo, RemitoCompra x)
        {

            return RemitoCobroClienteValidador.GetRemitoCobroCliente(codigo);

        }

    }
}
