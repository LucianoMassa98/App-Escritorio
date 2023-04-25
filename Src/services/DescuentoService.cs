using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    class Descuento
    {
       double importeDescuento, cantidadRequerida;
        string codigo, nombre;
        public Descuento() { 
            importeDescuento = 0;
            cantidadRequerida = 0;
         }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Nombre{get{return nombre;}set{nombre = value;}}
        public double ImporteDescuento {get{return importeDescuento;}set{importeDescuento = value;}}
        public double CantidadRequerida {get{return cantidadRequerida;}set{cantidadRequerida = value;}}

       static public bool Crear(Descuento x){

             if(DescuentoValidador.CrearDescuento(ref x)){

               List<Descuento> ListaDescuentos = Descuento.Buscar();
               ListaDescuentos.Add(x);
               return Descuento.Guardar(ListaDescuentos);
                
            }return false;
       }

        static public bool Borrar(string codigo)
        {
            if (DescuentoValidador.GetDescuento(codigo))
            {
                List<Descuento> ListaDescuento  = Descuento.Buscar();
                int i = Descuento.BuscarIndexPorCodigo(codigo, ListaDescuento);
                if (i < ListaDescuento.Count())
                {
                    ListaDescuento.RemoveAt(i);
                    return Descuento.Guardar(ListaDescuento);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, Descuento x)
        {
            if (DescuentoValidador.ActualizarDescuento(codigo, x))
            {
                List<Descuento> ListaDescuento = Descuento.Buscar();
                int i = Descuento.BuscarIndexPorCodigo(codigo, ListaDescuento);
                if (i < ListaDescuento.Count())
                {
                    if (x.Nombre != "") { ListaDescuento[i].Nombre = x.Nombre; }
                    ListaDescuento[i].ImporteDescuento = x.ImporteDescuento;
                    ListaDescuento[i].CantidadRequerida = x.CantidadRequerida;

                    return Descuento.Guardar(ListaDescuento);
                }

            }
            return false;
        }
        static public List<Descuento> Buscar(){
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.Descuentos);
            string l=""; 
            string []dat;
            List<Descuento> ListaDeDescuentos = new List<Descuento>();

            while((l=p.ReadLine())!=null){
            dat = l.Split('|');
            Descuento newDescuento = new Descuento();
            newDescuento.Codigo = dat[0];
            newDescuento.Nombre = dat[1];
            newDescuento.ImporteDescuento = double.Parse(dat[2]);
            newDescuento.CantidadRequerida = double.Parse(dat[3]);
            ListaDeDescuentos.Add(newDescuento);
            }
            p.Close(); p.Dispose();

            return ListaDeDescuentos;
       }
        static public Descuento BuscarPorCodigo(string codigo)
        {
            if (DescuentoValidador.GetDescuento(codigo))
            {
                List<Descuento> ListaDescuento = Descuento.Buscar();
                int i = Descuento.BuscarIndexPorCodigo(codigo, ListaDescuento);
                if (i < ListaDescuento.Count())
                {
                    return ListaDescuento[i];
                }

            }
            return null;
        }
        static public Descuento BuscarPorNombre(string nombre)
        {
            if (DescuentoValidador.GetDescuento(nombre))
            {
                List<Descuento> ListaDescuento = Descuento.Buscar();
                int i = Descuento.BuscarIndexPorNombre(nombre, ListaDescuento);
                if (i < ListaDescuento.Count())
                {
                    return ListaDescuento[i];
                }

            }
            return null;
        }
        static public int BuscarIndexPorCodigo(string codigo, List<Descuento> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public int BuscarIndexPorNombre(string nombre, List<Descuento> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Nombre != nombre))
            {
                ind++;
            }
            return ind;
        }
        static public bool Guardar(List<Descuento> x){
             Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.Descuentos);
            for(int i =0; i<x.Count(); i++){
                p.WriteLine(
                    x[i].Codigo+'|'+
                    x[i].Nombre + '|' + 
                    x[i].ImporteDescuento + '|' + 
                    x[i].CantidadRequerida
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public void MostrarDataGrid(ref DataGridView y)
        {
            y.Rows.Clear();
            List<Descuento> x = Descuento.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                y.Rows.Add(
                    x[i].Codigo,
                    x[i].Nombre,
                    x[i].cantidadRequerida,
                    x[i].ImporteDescuento
                    );
            }

        }

        static public void CargarComboBox(ref ComboBox y)
        {

            y.Items.Clear();
            List<Descuento> x = Descuento.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                y.Items.Add(x[i].Nombre);
            }

        }
    }

}
