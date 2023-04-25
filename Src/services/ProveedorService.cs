using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
namespace E_Shop
{
    class Proveedor
    {
        string codigo, nombre, tel, correo, direccion;
        double saldo;


       public Proveedor() { saldo =0; }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Nombre { get { return nombre; }set { nombre = value; } }
        public string Tel { get { return tel; }set { tel = value; } }
        public string Correo { get { return correo; } set { correo = value; } }
        public string Direccion { get { return direccion; } set { direccion = value; } }
        public double Saldo{get{return saldo;}set{saldo = value;}}
        static public bool Crear(Proveedor x){
            if(ProveedorValidador.CrearProveedor(ref x))
            { 
               List<Proveedor> ListaProveedors = Proveedor.Buscar();
               ListaProveedors.Add(x);
               return Proveedor.Guardar(ListaProveedors);
                
            }
            return false;
        }
        static public bool Borrar(string codigo){
            if (ProveedorValidador.GetProveedor(codigo))
            {
                List<Proveedor> ListaProveedor  = Proveedor.Buscar();
                int i = Proveedor.BuscarIndexPorCodigo(codigo, ListaProveedor);
                if (i < ListaProveedor.Count())
                {
                    ListaProveedor.RemoveAt(i);
                    return Proveedor.Guardar(ListaProveedor);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, Proveedor x){

            if (ProveedorValidador.ActualizarProveedor(codigo, x))
            {
                List<Proveedor> ListaProveedor = Proveedor.Buscar();
                int i = Proveedor.BuscarIndexPorCodigo(codigo, ListaProveedor);
                if (i < ListaProveedor.Count())
                {


                    if (x.Nombre != "") { ListaProveedor[i].Nombre = x.Nombre; }
                    if (x.Tel != "") { ListaProveedor[i].Tel = x.Tel; }
                    if (x.Correo != "") { ListaProveedor[i].Correo = x.Correo; }
                    if (x.Direccion != "") { ListaProveedor[i].Direccion = x.Direccion; }
                    ListaProveedor[i].Saldo = x.Saldo;

                    return Proveedor.Guardar(ListaProveedor);
                }

            }
            return false;

        }
        static public List<Proveedor> Buscar(){
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.Proveedores);
            string l=""; 
            string []dat;
            List<Proveedor> ListaDeProveedores = new List<Proveedor>();

            while((l=p.ReadLine())!=null){
            dat = l.Split('|');
            Proveedor newProveedor = new Proveedor();
            newProveedor.Codigo = dat[0];
            newProveedor.Nombre = dat[1];
            newProveedor.Tel = dat[2];
            newProveedor.Correo = dat[3];
            newProveedor.Direccion = dat[4];
            newProveedor.Saldo = double.Parse(dat[5]);

            ListaDeProveedores.Add(newProveedor);
            }
            p.Close(); p.Dispose();

            return ListaDeProveedores;
        }
        static public Proveedor BuscarPorCodigo(string codigo){
            if (ProveedorValidador.GetProveedor(codigo))
            {
                List<Proveedor> ListaProveedor = Proveedor.Buscar();
                int i = Proveedor.BuscarIndexPorCodigo(codigo, ListaProveedor);
                if (i < ListaProveedor.Count())
                {
                    return ListaProveedor[i];
                }

            }
            return null;

        }
        static public Proveedor BuscarPorNombre(string nombre){
            if (ProveedorValidador.GetProveedor(nombre))
            {
                List<Proveedor> ListaProveedor = Proveedor.Buscar();
                int i = Proveedor.BuscarIndexPorNombre(nombre, ListaProveedor);
                if (i < ListaProveedor.Count())
                {
                    return ListaProveedor[i];
                }

            }
            return null;
        }
        static public int BuscarIndexPorCodigo(string codigo, List<Proveedor> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public int BuscarIndexPorNombre(string nombre, List<Proveedor> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Nombre != nombre))
            {
                ind++;
            }
            return ind;
        }
        static public bool Guardar(List<Proveedor> x){
              Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.Proveedores);
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
        static public void MostrarDataGrid(ref DataGridView y)
        {
            y.Rows.Clear();
            List<Proveedor> x = Proveedor.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
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
        static public void CargarComboBox(ref ComboBox y)
        {

         //   y.Items.Clear();
            List<Proveedor> x = Proveedor.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                y.Items.Add(x[i].Nombre);
            }

        }
        static public void CargarComboBox(ref ComboBox y,ref ComboBox z)
        {

            y.Items.Clear(); z.Items.Clear();
            List<Proveedor> x = Proveedor.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                z.Items.Add(x[i].Codigo);
                y.Items.Add(x[i].Nombre);
            }

        }
        static public void SumarSaldo(string codCliente, double saldox)
        {

            List<Proveedor> Lista = Proveedor.Buscar();
            int ind = Proveedor.BuscarIndexPorCodigo(codCliente, Lista);
            if (ind < Lista.Count())
            {
                Lista[ind].Saldo += saldox;
                Proveedor.Guardar(Lista);
            }
        }
        static public void RestarSaldo(string codCliente, double saldox)
        {

            List<Proveedor> Lista = Proveedor.Buscar();
            int ind = Proveedor.BuscarIndexPorCodigo(codCliente, Lista);
            if (ind < Lista.Count())
            {
                Lista[ind].Saldo -= saldox;
                Proveedor.Guardar(Lista);
            }
        }
    }

   
}
