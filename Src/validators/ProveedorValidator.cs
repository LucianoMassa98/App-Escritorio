using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_Shop
{
    static class ProveedorValidador
    {
        static public bool CrearProveedor(ref Proveedor x)
        {
            if (x.Nombre == "") { return false; }
            if (x.Tel == "") { return false; }
            if (x.Correo == "") { return false; }
            if (x.Direccion == "") { return false; }
            List<Proveedor> Lista = Proveedor.Buscar();
            Proveedor y = Proveedor.BuscarPorNombre(x.Nombre);
            if (y != null) { return false; }
            x.Codigo = (Lista.Count() + 1).ToString();
            return true;
        }
        static public bool ActualizarProveedor(string codigo, Proveedor x)
        {
            if (codigo == "") { return false; }
            return true;

        }
        static public bool GetProveedor(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
    }
}
