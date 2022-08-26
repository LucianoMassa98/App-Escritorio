using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    class ProductoCliente
    {
        string codigoCliente;
        List<Producto> listaX;
     

        public ProductoCliente(){listaX = new List<Producto>(); }
        public string CodigoCliente{ get { return codigoCliente; } set { codigoCliente  = value; } }
        public List<Producto> ListaX { get { return listaX; } set { listaX= value; } }

        public Producto FindOneProduct(string id){

            for (int i =0; i<listaX.Count();i++) {
                if (listaX[i].Codigo==id) { 
                    return listaX[i];
                }
                if (listaX[i].Nombre== id)
                {
                    return listaX[i];
                }
            }
            return null;
        }

        public void AddProductosCliente(Producto n) {
            bool band = false;
                int i = 0;
                while ((i < listaX.Count()) &&(band==false)) {
                    if (listaX[i].Codigo == n.Codigo)
                    {
                        listaX[i] = n;
                    band = true;
                    }
                    i++; 
                }
            if (band == false) {
                listaX.Add(n);
            }
                
            
        }

        public string ListaProductos() {
            string l = "";


            for (int j = 0; j < listaX.Count(); j++)
            {
                if (j != 0) { l = l + "/"; }
                l = l + listaX[j].Codigo + "/" + listaX[j].Precio;

            }
            return l;
        }

        static public void create(ProductoCliente x) {

            List<ProductoCliente> lista = findAll();
            lista.Add(x);
            save(lista);
        }
        static public List<ProductoCliente> findAll() {

            List<ProductoCliente> ListaProductosClientes = new List<ProductoCliente>();
            StreamReader h = new StreamReader(new Direcciones().ProductosCliente);
            string l; string[] dat;

            while ((l = h.ReadLine()) != null)
            {
                dat = l.Split('|');
                ProductoCliente x = new ProductoCliente();
                x.CodigoCliente = dat[0];

                dat = dat[1].Split('/');


                try {
                    if (dat.Length>1) {
                        for (int j = 0; j < dat.Length; j = j + 2)
                        {
                            Producto n = new Producto();
                            n.Codigo = dat[j];
                            n.Precio = double.Parse(dat[j + 1]);

                            x.ListaX.Add(n);

                        }
                    }
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Problema con la linea: " + l);
                }

                ListaProductosClientes.Add(x);
            }
            h.Close(); h.Dispose();

            return ListaProductosClientes;
        }
        static public ProductoCliente findOne(string id) {
            List<ProductoCliente> lista = findAll();
            for (int i =0; i<lista.Count();i++) {
                if (lista[i].CodigoCliente==id) {


                    for (int j=0; j<lista[i].listaX.Count();j++) {

                        Producto n = Producto.BuscarPorCodigo(lista[i].listaX[j].Codigo);
                        lista[i].listaX[j].Nombre = n.Nombre;
                        lista[i].listaX[j].Descripcion = n.Descripcion;
                    }    
                    return lista[i]; 
                }
            }
            return null;
        }
        static public int findOneIndex(string id)
        {
            List<ProductoCliente> lista = findAll();
            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista[i].CodigoCliente == id) { return i; }
            }
            return lista.Count();
        }
        static public bool update(string id, ProductoCliente nuevo) {

            List<ProductoCliente> lista = findAll();
            int index = findOneIndex(id);

            lista[index] = nuevo;
            save(lista);
            return true;
        }
        static public bool delete(string id) {
            List<ProductoCliente> lista = findAll();
            int index = findOneIndex(id);
            lista.RemoveAt(index);
            save(lista);
            return true;
        }
        static public bool save(List<ProductoCliente> x) {

            StreamWriter h = new StreamWriter(new Direcciones().ProductosCliente);

            for (int i = 0; i < x.Count(); i++)
            {
                

                h.WriteLine(x[i].CodigoCliente + "|" + x[i].ListaProductos());

            }

            h.Close(); h.Dispose();
            return true;
        }
      
    }

   
}
