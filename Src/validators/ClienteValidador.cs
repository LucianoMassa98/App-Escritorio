using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class ClienteValidador
    {
        static public bool CrearCliente(ref Cliente x) { 
            if (x.Nombre == "") { return false; }
            if (x.Tel == "") { return false; }
            if (x.Correo == "") { return false; }
            if (x.Direccion == "") { return false; }
            List<Cliente> Lista = Cliente.Buscar();
            Cliente y = Cliente.BuscarPorNombre(x.Nombre);
            if (y!=null) { return false; }
            x.Codigo = (Lista.Count() + 1).ToString();
            return true;
        }
        static public bool ActualizarCliente(string codigo, Cliente x){
            
            return ClienteValidador.GetCliente(codigo);

        }
        static public bool GetCliente(string CadenaTexto) {
            if (CadenaTexto == "") { return false; }
            return true;
        }
    }
}
