using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;

namespace E_Shop
{
    class ProductoProveedor
    {
        string codigoProveedor;
        List<Producto> listaX;
     

        public ProductoProveedor(){listaX = new List<Producto>(); }
        public string CodigoProveedor { get { return codigoProveedor; } set { codigoProveedor = value; } }
        public List<Producto> ListaX { get { return listaX; } set { listaX= value; } }


        public Producto FindOneProduct(string id)
        {

            for (int i = 0; i < listaX.Count(); i++)
            {
                if (listaX[i].Codigo == id)
                {
                    return listaX[i];
                }
                if (listaX[i].Nombre == id)
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

        public string ListaProductos()
        {
            string l = "";


            for (int j = 0; j < listaX.Count(); j++)
            {
                if (j != 0) { l = l + "/"; }
                l = l + listaX[j].Codigo + "/" + listaX[j].Costo;

            }
            return l;
        }

        static public void create(ProductoProveedor x) {

            List<ProductoProveedor> lista = findAll();
            lista.Add(x);
            save(lista);
        }
        static public List<ProductoProveedor> findAll() {

            List<ProductoProveedor> lista= new List<ProductoProveedor>();
            StreamReader h = new StreamReader(new Direcciones().ProductosProveedor);
            string l; string[] dat;

            while ((l = h.ReadLine()) != null)
            {
                dat = l.Split('|');
                ProductoProveedor x = new ProductoProveedor();
                x.CodigoProveedor = dat[0];

                dat = dat[1].Split('/');


                try {
                    if (dat.Length>1) {
                        for (int j = 0; j < dat.Length; j = j + 2)
                        {
                            Producto n = new Producto();
                            n.Codigo = dat[j];
                            n.Costo = double.Parse(dat[j + 1]);

                            x.ListaX.Add(n);

                        }
                    }
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("Problema con la linea: " + l);
                }

                lista.Add(x);
            }
            h.Close(); h.Dispose();

            return lista;
        }
        static public ProductoProveedor findOne(string id) {
            List<ProductoProveedor> lista = findAll();
            for (int i =0; i<lista.Count();i++) {
                if (lista[i].CodigoProveedor == id) {


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
            List<ProductoProveedor> lista = findAll();
            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista[i].CodigoProveedor == id) { return i; }
            }
            return lista.Count();
        }
        static public bool update(string id, ProductoProveedor nuevo) {

            List<ProductoProveedor> lista = findAll();
            int index = findOneIndex(id);
            lista[index] = nuevo;
            save(lista);
            return true;
        }
        static public bool delete(string id) {
            List<ProductoProveedor> lista = findAll();
            int index = findOneIndex(id);
            lista.RemoveAt(index);
            save(lista);
            return true;
        }
        static public bool save(List<ProductoProveedor> x) {

            StreamWriter h = new StreamWriter(new Direcciones().ProductosProveedor);

            for (int i = 0; i < x.Count(); i++)
            {
                

                h.WriteLine(x[i].CodigoProveedor + "|" + x[i].ListaProductos());

            }

            h.Close(); h.Dispose();
            return true;
        }
      
    }

   
}
