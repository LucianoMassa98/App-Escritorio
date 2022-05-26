using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class UsuarioValidador
    {
        static public bool CrearUsuario(ref Usuario x)
        {
            if (x.Codigo == "") { return false; }
            if (x.Nombre == "") { return false; }
            if (x.Tel == "") { return false; }
            if (x.Correo == "") { return false; }
            if (x.Direccion == "") { return false; }
            if (x.Tipo < 0) { return false; }
            if (x.Tipo > 4) { return false; }
            if (Usuario.BuscarPorNombre(x.Nombre)!=null) { return false; }
            return true;
        }
        static public bool ActualizarUsuario(string codigo, Usuario x)
        {
            if (codigo == "") { return false; }
            if (x.Tipo < -1) { return false; }
            if (x.Tipo > 4) { return false; }
            return true;

        }
        static public bool GetUsuario(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
    }
}
