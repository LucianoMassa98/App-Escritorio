using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class UsuarioValidador
    {
        static public bool CrearCliente(Usuario x){

            if(x.Codigo==""){ return false; }
            if(x.Nombre==""){ return false;}
            if(x.Tel==""){ return false;}
            if(x.Correo==""){ return false;}
            if(x.Direccion==""){ return false;}
            if(x.Tipo==""){ return false;}

            return true;
        }

        static public bool BorrarCliente(string codigo){
            if(codigo==""){return false;}
            return true;
        }
    
        static public bool ActualizarCliente(){}    
    }
}