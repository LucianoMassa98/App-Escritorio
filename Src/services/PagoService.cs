using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_Shop
{
    class Pago
    {

        string codigo, nombre;
        double debe, haber;
        double importe;

        public Pago() { importe = debe = haber = 0; }
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public double Importe { get { return importe; } set { importe = value; } }

        public double Debe { get { return debe; }set { debe = value; } }
        public double Haber { get { return haber; }set { haber = value; } }
        public double Saldo() { return debe - haber; }

        public string tipo() {

            string[] dat = codigo.Split('.');
            if (dat.Length>0) { return dat[0]; }
            return "";            
        
        }
        public void SumarImporte(double valor) { this.importe += valor; }
        public void RestarImporte(double valor) { this.importe -= valor; }


        static public bool Crear(Pago x){
            
            if(PagoValidador.CrearPago(ref x)){

               List<Pago> ListaPagos = Pago.Buscar();
               ListaPagos.Add(x);
               return Pago.Guardar(ListaPagos);
                
            }
            return false;
        }
        static public bool Borrar(string codigo){
            if (PagoValidador.GetPago(codigo))
            {
                List<Pago> ListaPago = Pago.Buscar();
                int i = Pago.BuscarIndexPorCodigo(codigo, ListaPago);
                if (i < ListaPago.Count())
                {
                    ListaPago.RemoveAt(i);
                    return Pago.Guardar(ListaPago);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, Pago x)
        {
            if (PagoValidador.ActualizarPago(codigo, x))
            {
                List<Pago> ListaPago = Pago.Buscar();
                int i = Pago.BuscarIndexPorCodigo(codigo, ListaPago);
                if (i < ListaPago.Count())
                {

                    if (x.Nombre != "") { ListaPago[i].Nombre = x.Nombre; }
                     ListaPago[i].Importe = x.Importe;

                    return Pago.Guardar(ListaPago);
                }

            }
            return false;
        }
        static public List<Pago> Buscar(){
             Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.Pagos);
            string l=""; 
            string []dat;
            List<Pago> ListaDePagos = new List<Pago>();

            while((l=p.ReadLine())!=null){
            dat = l.Split('|');
            Pago newPago = new Pago();
            newPago.Codigo = dat[0];
            newPago.Nombre = dat[1];
            newPago.Importe = double.Parse(dat[2]);

            ListaDePagos.Add(newPago);
            }
            p.Close(); p.Dispose();

            return ListaDePagos;
        }
        static public List<Pago> Buscar(string codigo)
        {
            string []cod = codigo.Split('.');
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.Pagos);
            string l = "";
            string[] dat;
            List<Pago> ListaDePagos = new List<Pago>();

            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                Pago newPago = new Pago();
                newPago.Codigo = dat[0];
                string[] cod2 = dat[0].Split('.');
                if ((cod.Length < cod2.Length) ||(cod.Length == cod2.Length)) {

                    int j = 0;
                    while ((j<cod.Length)&&(cod[j]==cod2[j])) { j++; }
                    if (j == cod.Length) {

                        newPago.Nombre = dat[1];
                        newPago.Importe = double.Parse(dat[2]);
                        ListaDePagos.Add(newPago);

                    } 
                }
           
            }
            p.Close(); p.Dispose();

            return ListaDePagos;
        }
        static public Pago BuscarPorCodigo(string codigo){
            if (PagoValidador.GetPago(codigo))
            {
                List<Pago> ListaPago = Pago.Buscar();
                int i = Pago.BuscarIndexPorCodigo(codigo, ListaPago);
                if (i < ListaPago.Count())
                {
                    return ListaPago[i];
                }

            }
            return null;
        }
        static public Pago BuscarPorNombre(string nombre){
            if (PagoValidador.GetPago(nombre))
            {
                List<Pago> ListaPago = Pago.Buscar();
                int i = Pago.BuscarIndexPorNombre(nombre, ListaPago);
                if (i < ListaPago.Count())
                {
                    return ListaPago[i];
                }

            }
            return null;
        }
        static public int BuscarIndexPorCodigo(string codigo, List<Pago> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public int BuscarIndexPorNombre(string nombre, List<Pago> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Nombre != nombre))
            {
                ind++;
            }
            return ind;
        }
        static public bool Guardar(List<Pago> x){
              Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.Pagos);
            for(int i =0; i<x.Count(); i++){
                p.WriteLine(
                    x[i].Codigo+'|'+
                    x[i].Nombre + '|' + 
                    x[i].Importe
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public void MostrarDataGrid(ref DataGridView y)
        {
            y.Rows.Clear();
            List<Pago> x = Pago.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i].Codigo=="1.1.6.1") {
                    y.Rows.Add(
                            x[i].Codigo,
                            x[i].Nombre,
                             Producto.SumaCostos(Producto.Buscar())
                            );
                } else {
                    y.Rows.Add(
                        x[i].Codigo,
                        x[i].Nombre,
                        x[i].Importe
                        );
                }
                
            }

        }
        static public void CargarComboBox(ref ComboBox y)
        {

            y.Items.Clear();
            List<Pago> x = Pago.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                y.Items.Add(x[i].Nombre);
            }

        }
        static public void CargarComboBox(ref ComboBox y,string codigo)
        {

            y.Items.Clear();
            List<Pago> x = Pago.Buscar(codigo);
            for (int i = 0; i < x.Count(); i++)
            {
                y.Items.Add(x[i].Nombre);
            }

        }
        static public void CargarComboBox2(ref ComboBox y, string codigo)
        {

          
            List<Pago> x = Pago.Buscar(codigo);
            for (int i = 0; i < x.Count(); i++)
            {
                y.Items.Add(x[i].Nombre);
            }

        }
        static public void RestarCuenta(List<Pago> x) {

            List<Pago> y = Pago.Buscar();

            for (int i = 0; i < x.Count(); i++)
            {
                int ind = Pago.BuscarIndexPorCodigo(x[i].Codigo, y);
                if (ind<y.Count()) {
                    if (x[i].Codigo!="aaaa") { y[ind].Importe -= x[i].Importe; }
                }
               

            }
            Pago.Guardar(y);



        }
        static public void SumarCuenta(List<Pago> x)
        {
            List<Pago> y = Pago.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                int ind = Pago.BuscarIndexPorCodigo(x[i].Codigo, y);
                if (ind < y.Count())
                {
                    if (x[i].Codigo!="aaaa") { y[ind].Importe += x[i].Importe; }
                }
            }
            Pago.Guardar(y);
        }
        static public void RestarCuenta(Pago x)
        {

            List<Pago> y = Pago.Buscar();

          
                int ind = Pago.BuscarIndexPorCodigo(x.Codigo, y);
                if (ind < y.Count())
                {
                    y[ind].Importe -= x.Importe;
                }


            Pago.Guardar(y);



        }
        static public void SumarCuenta(Pago x)
        {

            List<Pago> y = Pago.Buscar();

            
                int ind = Pago.BuscarIndexPorCodigo(x.Codigo, y);
                if (ind < y.Count())
                {
                    y[ind].Importe += x.Importe;
                }
            Pago.Guardar(y);



        }
        static public void CerearCuenta(List<Pago> x)
        {

            List<Pago> y = Pago.Buscar();

            for (int i = 0; i < x.Count(); i++)
            {
                int ind = Pago.BuscarIndexPorCodigo(x[i].Codigo, y);
                if (ind < y.Count())
                {
                    y[ind].Importe = 0;
                }


            }
            Pago.Guardar(y);



        }
        static public void AgregarCuenta(ref List<Pago> x, Pago y)
        {
               
            int ind = Pago.BuscarIndexPorCodigo(y.Codigo, x);
                if (ind < x.Count())
                {
                    x[ind].Importe += y.importe;
                }
            else
            {
                x.Add(y);
            }


        }
        static public bool AgregarPago(ref List<Pago> x, Pago y)
        {

            int ind = Pago.BuscarIndexPorCodigo(y.Codigo, x);
            if (ind < x.Count())
            {
                return false;
            }
           
                x.Add(y);
                return true;


        }
        static public bool RestarPago(ref List<Pago> x, string nombre)
        {

            for (int i =0; i<x.Count(); i++) {
                if (x[i].Nombre==nombre) {
                    x.RemoveAt(i);
                    return true;
                }
            }

            return false;


        }
        static public double SumarImportes(List<Pago> x) {
            double sum = 0;
            foreach(Pago p in x)
            {

                if (p.Codigo!="1.1.6.1") {
                    sum += p.Importe;
                }
                else
                {
                    sum += Producto.SumaCostos(Producto.Buscar());
                }
                
            }
        return sum;
        }
        static public void recap(double pn, string cod,bool sum)
        {
            List<Pago> lista =  Pago.Buscar("5");
            for (int i =0; i<lista.Count();i++) {
                if (lista[i].Codigo!=cod) {
                    if (sum) {
                        lista[i].Importe = pn;
                        Pago.SumarCuenta(lista[i]);
                    }
                    else
                    {
                        lista[i].Importe = pn;
                        Pago.RestarCuenta(lista[i]);
                    }
                }
            }
        }

        static public void CerearCuentas(string codigo) {
            string[] cod = codigo.Split('.');
            List<Pago> y = Pago.Buscar();
            for (int i=0; i<y.Count();i++) {

                string[] cod2 = y[i].Codigo.Split('.');
                if (cod.Length <= cod2.Length)
                {   int j = 0;
                    bool band = true;
                    while((band)&&(j<cod.Length))
                    {
                        if (cod[j] != cod2[j]) { band = false; }
                        j++;
                    }
                    if (band)
                    {
                        y[i].importe = 0;
                    }
                }
            }
            Pago.Guardar(y);
        }

        static public bool LeerBorrado() {

            StreamReader p = new StreamReader(new Direcciones().BandBorrado);
            string l = p.ReadLine();
            p.Close(); p.Dispose();
            return bool.Parse(l);
            
        }
        static public void EscribirBorrado(bool band)
        {

            StreamWriter p = new StreamWriter(new Direcciones().BandBorrado);
            p.WriteLine(band.ToString());
            p.Close(); p.Dispose();
           

        }
    }

}
