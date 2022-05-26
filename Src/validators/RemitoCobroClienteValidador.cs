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
           
            if (x.Emisor == "") { new Alert("Emisor mal cargado").Show(); return false; }
                if (x.Receptor == "") { new Alert("Receptor mal cargado").Show(); return false;}
            if (x.FechaEmision == "") { new Alert("Fecha mal cargada").Show(); return false;  }
            if (x.Pagos.Count() == 0) { new Alert("No hay ningun pago").Show();  return false;  }
            
                for (int i = 0; i < x.Pagos.Count(); i++)
                {
                    if (x.Pagos[i].Importe==0) { new Alert("Importes de pagos mal cargados").Show();return false;  }

                }
            List<RemitoCobroCliente> Lista = RemitoCobroCliente.Buscar();
            x.Codigo = (Lista.Count() + 1).ToString();
            try
            {
                
            }
            catch (Exception) { new Alert("Error Critico").Show();  return false;  }


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
        static public bool ActualizarRemitoCobroCliente(string codigo, RemitoCobroCliente x)
        {

            return RemitoCobroClienteValidador.GetRemitoCobroCliente(codigo);

        }

    }
}
