using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class RemitoGastoValidator

    {
        static public bool CrearRemitoGasto(ref RemitoGasto x)
        {
           
            if (x.Emisor == "") { new Alert("Emisor mal cargado").Show(); return false; }
            if (x.Receptor == "") { new Alert("Receptor mal cargado").Show(); return false;}
            if (x.FechaEmision == "") { new Alert("Fecha mal cargada").Show(); return false;  }
            if (x.Fuente == null) { new Alert("No hay ninguna fuente").Show();  return false;  }
            if (x.Razon == null) { new Alert("No hay ninguna razon").Show(); return false; }

            List<RemitoGasto> Lista = RemitoGasto.Buscar();
            x.Codigo = (Lista.Count() + 1).ToString();
            


            return true;
        }
       
        static public bool GetRemitoGasto(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoGasto(string codigo, RemitoGasto x)
        {

            return GetRemitoGasto(codigo);

        }

    }
}
