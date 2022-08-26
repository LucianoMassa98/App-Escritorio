using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    class Cliente
    {
        string codigo, nombre, tel, correo, direccion;
        double saldo;
        List<Producto> listaX;

        public Cliente(){saldo =0; listaX = new List<Producto>(); }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Nombre { get { return nombre; }set { nombre = value; } }
        public string Tel { get { return tel; }set { tel = value; } }
        public string Correo { get { return correo; } set { correo = value; } }
        public string Direccion { get { return direccion; } set { direccion = value; } }
        public double Saldo{get{return saldo;}set{saldo = value;}}
        public List<Producto> ListaX { get { return listaX; } set { listaX= value; } }


        static public bool Crear(Cliente x){
            
            if(ClienteValidador.CrearCliente(ref x)){
               List<Cliente> ListaClientes = Cliente.Buscar();
               ListaClientes.Add(x);
               return Cliente.Guardar(ListaClientes);
                
            }return false;
        }
        static public List<Cliente> Buscar()
        {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.Clientes);
            string l = "";
            string[] dat;
            List<Cliente> ListaDeClientes = new List<Cliente>();

            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                Cliente newCliente = new Cliente();
                newCliente.Codigo = dat[0];
                newCliente.Nombre = dat[1];
                newCliente.Tel = dat[2];
                newCliente.Correo = dat[3];
                newCliente.Direccion = dat[4];
                newCliente.Saldo = double.Parse(dat[5]);
            
                ListaDeClientes.Add(newCliente);
            }
            p.Close(); p.Dispose();

            return ListaDeClientes;
        }
        

        static public bool Borrar(string codigo){

            if (ClienteValidador.GetCliente(codigo))
            {
                List<Cliente> ListaClientes = Cliente.Buscar();
                int i = Cliente.BuscarIndexPorCodigo(codigo, ListaClientes);
                if (i < ListaClientes.Count())
                {
                    ListaClientes.RemoveAt(i);
                    return Cliente.Guardar(ListaClientes);
                }
               
            }
            return false;
        }
        static public bool Actualizar(string codigo, Cliente x)
        {

            if (ClienteValidador.ActualizarCliente(codigo, x)) { 
            List<Cliente> ListaCliente = Cliente.Buscar();
            int i = Cliente.BuscarIndexPorCodigo(codigo, ListaCliente);
            if (i < ListaCliente.Count())
            {
                    if (x.Nombre!="") { ListaCliente[i].Nombre = x.Nombre; }
                    if (x.Tel != "") { ListaCliente[i].Tel = x.Tel; }
                    if (x.Correo != "") { ListaCliente[i].Correo = x.Correo; }
                    if (x.Direccion != "") { ListaCliente[i].Direccion = x.Direccion; }
                    ListaCliente[i].Saldo = x.Saldo; 
                return Cliente.Guardar(ListaCliente);
            }
              
             }
             return false;
        }
        static public Cliente BuscarPorCodigo(string codigo){
            if (ClienteValidador.GetCliente(codigo))
            {
                List<Cliente> ListaClientes = Cliente.Buscar();
                int i = Cliente.BuscarIndexPorCodigo(codigo, ListaClientes);
                if (i < ListaClientes.Count())
                {
                    return ListaClientes[i];
                }
                
            }
            return null;

        }
        static public Cliente BuscarPorNombre(string nombre)
        {
            if (ClienteValidador.GetCliente(nombre)) { 
                List<Cliente> ListaClientes = Cliente.Buscar();
            int i = Cliente.BuscarIndexPorNombre(nombre, ListaClientes);
            if (i < ListaClientes.Count())
            {
                return ListaClientes[i];
            }
            }
            return null;
        }
        static public int BuscarIndexPorCodigo(string codigo,List<Cliente> x)
        {
            int ind = 0;
            while ((ind<x.Count())&&(x[ind].Codigo!=codigo))
            {
                ind++;
            }
            return ind;
        }
        static public int BuscarIndexPorNombre(string nombre, List<Cliente> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Nombre != nombre))
            {
                ind++;
            }
            return ind;
        }
        static public bool Guardar(List<Cliente> x){
              Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.Clientes);
            for(int i =0; i<x.Count(); i++){
                p.WriteLine(
                    x[i].Codigo+'|'+
                    x[i].Nombre + '|' +
                    x[i].Tel + '|' + 
                    x[i].Correo + '|' +
                    x[i].Direccion + '|' +
                    x[i].Saldo
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public void MostrarDataGrid(ref DataGridView y) {
            y.Rows.Clear();
            List<Cliente> x = Cliente.Buscar();
            for (int i = 0; i< x.Count(); i++) {
                y.Rows.Add(
                    x[i].Codigo,
                    x[i].Nombre,
                    x[i].Tel,
                    x[i].Correo,
                    x[i].Direccion,
                    x[i].Saldo
                    );
            }

        }
        static public void CargarComboBox(ref ComboBox y) {

            y.Items.Clear();
            List<Cliente> x = Cliente.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                y.Items.Add(x[i].Nombre);
            }

        }

        static public void CargarComboBox(ref ComboBox y, ref ComboBox z)
        {

            y.Items.Clear(); z.Items.Clear();
            List<Cliente> x = Cliente.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                y.Items.Add(x[i].Nombre);
                z.Items.Add(x[i].Codigo);
            }

        }
        static public void SumarSaldo(string codCliente, double saldox) {

            List<Cliente> Lista = Cliente.Buscar();
            int ind = Cliente.BuscarIndexPorCodigo(codCliente,Lista);
            if (ind < Lista.Count()) {
                Lista[ind].Saldo += saldox;
                Cliente.Guardar(Lista);
            }
        }
        static public void RestarSaldo(string codCliente, double saldox)
        {

            List<Cliente> Lista = Cliente.Buscar();
            int ind = Cliente.BuscarIndexPorCodigo(codCliente, Lista);
            if (ind < Lista.Count())
            {
                Lista[ind].Saldo -= saldox;
                Cliente.Guardar(Lista);
            }
        }
    }

   
}
