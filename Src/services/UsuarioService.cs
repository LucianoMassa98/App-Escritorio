using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    class Usuario
    {
        string codigo, nombre, tel, correo, direccion;
        int tipo;

        double saldo;


      
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public Usuario() { saldo = 0; tipo = 0; }
        public string Nombre { get { return nombre; }set { nombre = value; } }
        public string Tel { get { return tel; }set { tel = value; } }
        public string Correo { get { return correo; } set { correo = value; } }
        public string Direccion { get { return direccion; } set { direccion = value; } }
        public int Tipo { get { return tipo; } set { tipo = value; } }
        public double Saldo{get{return saldo;}set{saldo = value;}}

        static public bool Crear(Usuario x){

            if (UsuarioValidador.CrearUsuario(ref x)) { 
               List<Usuario> ListaUsuarios = Usuario.Buscar();
               ListaUsuarios.Add(x);
               return Usuario.Guardar(ListaUsuarios);
                
            }
            return false;

        }
        static public bool Borrar(string codigo){
            if (UsuarioValidador.GetUsuario(codigo)) {

                List<Usuario> ListaUsuario  = Usuario.Buscar();
                int i = Usuario.BuscarIndexPorCodigo(codigo, ListaUsuario);
                if (i < ListaUsuario.Count())
                {
                    ListaUsuario.RemoveAt(i);
                    return Usuario.Guardar(ListaUsuario);
                }
               
            }
            return false;
        
        }
        static public bool Actualizar(string codigo, Usuario x){
            if (UsuarioValidador.ActualizarUsuario(codigo, x))
            {
                List<Usuario> ListaUsuario = Usuario.Buscar();
                int i = Usuario.BuscarIndexPorCodigo(codigo, ListaUsuario);
                if (i < ListaUsuario.Count())
                {
                    if (x.Codigo != "") { ListaUsuario[i].Codigo = x.Codigo; }
                    if (x.Nombre != "") { ListaUsuario[i].Nombre = x.Nombre; }
                    if (x.Tel != "") { ListaUsuario[i].Tel = x.Tel; }
                    if (x.Correo != "") { ListaUsuario[i].Correo = x.Correo; }
                    if (x.Direccion != "") { ListaUsuario[i].Direccion = x.Direccion; }
                    if (x.Tipo != 0) { ListaUsuario[i].Tipo = x.Tipo; }
                    ListaUsuario[i].Saldo = x.Saldo;

                    return Usuario.Guardar(ListaUsuario);
                }

            }
            return false;
        }
        static public List<Usuario> Buscar(){
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.Usuarios);
            string l=""; 
            string []dat;
            List<Usuario> ListaDeUsuarios = new List<Usuario>();

            while((l=p.ReadLine())!=null){
            dat = l.Split('|');
            Usuario newUsuario = new Usuario();
            newUsuario.Codigo = dat[0];
            newUsuario.Nombre = dat[1];
            newUsuario.Tel = dat[2];
            newUsuario.Correo = dat[3];
            newUsuario.Direccion = dat[4];
            newUsuario.Tipo = int.Parse(dat[5]);
            newUsuario.Saldo = double.Parse(dat[6]);

            ListaDeUsuarios.Add(newUsuario);
            }
            p.Close(); p.Dispose();

            return ListaDeUsuarios;

        }
        static public Usuario BuscarPorCodigo(string codigo){
            if (UsuarioValidador.GetUsuario(codigo))
            {
                List<Usuario> ListaUsuario = Usuario.Buscar();
                int i = Usuario.BuscarIndexPorCodigo(codigo, ListaUsuario);
                if (i < ListaUsuario.Count())
                {
                    return ListaUsuario[i];
                }
            }
            return null;

        }
        static public Usuario BuscarPorNombre(string nombre){
            if (UsuarioValidador.GetUsuario(nombre))
            {
                List<Usuario> ListaUsuario = Usuario.Buscar();
                int i = Usuario.BuscarIndexPorNombre(nombre, ListaUsuario);
                if (i < ListaUsuario.Count())
                {
                    return ListaUsuario[i];
                }
            }
            return null;

        }
        static public int BuscarIndexPorCodigo(string codigo, List<Usuario> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public int BuscarIndexPorNombre(string nombre, List<Usuario> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Nombre != nombre))
            {
                ind++;
            }
            return ind;
        }
        static public bool Guardar(List<Usuario> x){
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.Usuarios);
            for(int i =0; i<x.Count(); i++){
                p.WriteLine(
                    x[i].Codigo + '|' +
                    x[i].Nombre + '|' + 
                    x[i].Tel + '|' + 
                    x[i].Correo + '|' +
                    x[i].Direccion + '|' +
                    x[i].Tipo + '|' +
                    x[i].Saldo
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }

        static public void MostrarDataGrid(ref DataGridView y)
        {
            y.Rows.Clear();
            List<Usuario> x = Usuario.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                y.Rows.Add(
                    x[i].Codigo,
                    x[i].Nombre,
                    x[i].Tel,
                    x[i].Correo,
                    x[i].Direccion,
                    x[i].Tipo,
                    x[i].Saldo
                    );
            }

        }
    }

    
   
}
