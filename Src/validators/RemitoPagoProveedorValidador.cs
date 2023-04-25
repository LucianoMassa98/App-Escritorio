using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace E_Shop
{
    static class RemitoPagoProveedorValidador
    {
        static public bool CrearRemitoPagoProveedor(ref  RemitoPagoProveedor x)
        {
            try
            {
                if (x.Emisor == "") { return false; }
                if (x.Receptor == "") { return false; }
                if (x.FechaEmision == "") { return false; }
                if (x.Pagos.Count() == 0) { return false; }

                for (int i = 0; i < x.Pagos.Count(); i++)
                {
                    if (x.Pagos[i].Importe == 0) { return false; }

                }

                List<RemitoPagoProveedor> Lista = RemitoPagoProveedor.Buscar();
                x.Codigo = (Lista.Count() + 1).ToString();
            }
            catch (Exception) { return false; }


            return true;
        }
        static public bool AgregarPago(ref Pago x)
        {
            if (x.Importe == 0) { return false; }

            return true;
        }
        static public bool GetRemitoPagoProveedor(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoPagoProveedor(string codigo, RemitoPagoProveedor x)
        {

            return RemitoPagoProveedorValidador.GetRemitoPagoProveedor(codigo);

        }

    }
}
