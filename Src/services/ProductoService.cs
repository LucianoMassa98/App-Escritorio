using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace E_Shop
{
    class Producto
    {

        double cantidad, cantidaEstandar, precio,precio2,precio3, costo,bulto;
        string codigo, nombre, descripcion;
        public Producto() { cantidad = cantidaEstandar = costo = 0; precio = 1; precio2 = 1; precio3 = 1; }

        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public double Bulto{ get { return bulto; } set { bulto = value; } }
        public double Cantidad { get { return cantidad; } set { cantidad = value; } }
        public double CantidadEstandar { get { return cantidaEstandar; } set { cantidaEstandar = value; } }
        public double Costo { get { return costo; } set { costo = value; } }
        public double Precio { get { return precio; } set { precio = value; } }
        public double Precio2 { get { return precio2; } set { precio2 = value; } }
        public double Precio3 { get { return precio3; } set { precio3 = value; } }

        public double ImportePrecio() { return cantidad * precio; }
        public double ImportePrecio2() { return cantidad * precio2; }
        public double ImportePrecio3() { return cantidad * precio3; }
        public double ImporteCosto() { return cantidad * costo; }
        public void SumarCantidad(double cnt) { cantidad += cnt; }
        public void SumarBulto(double blt) { this.bulto  += blt; }
        public void SumarPrecio(double precio) { this.precio += precio; }
        public void SumarCosto(double costo) { this.costo += costo; }
        public void RestarCantidad(double cnt) { cantidad -= cnt; }
        public void RestarBulto(double blt) { bulto-= blt; }
        public void RestarPrecio(double precio) { this.precio -= precio; }
        public void RestarCosto(double costo) { this.costo -= costo; }



        static public bool Crear(Producto x) {

            if (ProductoValidador.CrearProducto(x)) {

                List<Producto> ListaProductos = Producto.Buscar();
                ListaProductos.Add(x);
                return Producto.Guardar(ListaProductos);

            } return false;
        }
        static public bool Borrar(string codigo)
        {

            if (ProductoValidador.GetProducto(codigo))
            {
                List<Producto> ListaProducto = Producto.Buscar();
                int i = Producto.BuscarIndexPorCodigo(codigo, ListaProducto);
                if (i < ListaProducto.Count())
                {
                    ListaProducto.RemoveAt(i);
                    return Producto.Guardar(ListaProducto);
                }

            }
            return false;
        }
        static public bool Actualizar(string codigo, Producto x)
        {

            if (ProductoValidador.ActualizarProducto(codigo, x))
            {
                List<Producto> ListaProducto = Producto.Buscar();
                int i = Producto.BuscarIndexPorCodigo(codigo, ListaProducto);
                if (i < ListaProducto.Count())
                {

                    if (x.Nombre != "") { ListaProducto[i].Nombre = x.Nombre; }
                    if (x.Descripcion != "") { ListaProducto[i].Descripcion = x.Descripcion; }
                    ListaProducto[i].Bulto = x.Bulto;
                    ListaProducto[i].Cantidad = x.Cantidad;
                    ListaProducto[i].CantidadEstandar = x.CantidadEstandar;
                    ListaProducto[i].Costo = x.Costo;
                    ListaProducto[i].Precio = x.Precio;
                    ListaProducto[i].Precio2 = x.Precio2;
                    ListaProducto[i].Precio3 = x.Precio3;
                  //  MessageBox.Show("precio 1: " + ListaProducto[i].Precio +" precio 2:"+ ListaProducto[i].Precio2 +" precio 3: "+ ListaProducto[i].Precio3);

                    return Producto.Guardar(ListaProducto);
                }

            }
            return false;
        }
        static public List<Producto> Buscar() {
            Direcciones dir = new Direcciones();
            StreamReader p = new StreamReader(dir.Productos);
            string l = "";
            string[] dat;
            List<Producto> ListaDeProductos = new List<Producto>();
            int i = 0;
            while ((l = p.ReadLine()) != null)
            {
                dat = l.Split('|');
                if (dat.Length == 10)
                {
                    Producto newProducto = new Producto();
                    newProducto.Codigo = dat[0];
                    newProducto.Nombre = dat[1];
                    newProducto.Descripcion = dat[2];
                    newProducto.Bulto= double.Parse(dat[3]);
                    newProducto.Cantidad = double.Parse(dat[4]);
                    newProducto.CantidadEstandar = double.Parse(dat[5]);
                    newProducto.Costo = double.Parse(dat[6]);
                    newProducto.Precio = double.Parse(dat[7]);
                    newProducto.Precio2 = double.Parse(dat[8]);
                    newProducto.Precio3 = double.Parse(dat[9]);
                    ListaDeProductos.Add(newProducto);
                }
                else {

                    Alert n = new Alert("Error \n En la lectura de la linea: "+i+ "\n Contactar Servicio Tecnico");
                    n.Show();
                }
                i++;
            }
            p.Close(); p.Dispose();

            return ListaDeProductos;
        }
        static public Producto BuscarPorCodigo(string codigo)
        {
            if (ProductoValidador.GetProducto(codigo))
            {
                List<Producto> ListaProducto = Producto.Buscar();
                int i = Producto.BuscarIndexPorCodigo(codigo, ListaProducto);
                if (i < ListaProducto.Count())
                {
                    return ListaProducto[i];
                }

            }
            return null;

        }
        static public Producto BuscarPorNombre(string nombre)
        {

            if (ProductoValidador.GetProducto(nombre))
            {
                List<Producto> ListaProducto = Producto.Buscar();
                int i = Producto.BuscarIndexPorNombre(nombre, ListaProducto);
                if (i < ListaProducto.Count())
                {
                    return ListaProducto[i];
                }

            }
            return null;
        }
        static public int BuscarIndexPorCodigo(string codigo, List<Producto> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Codigo != codigo))
            {
                ind++;
            }
            return ind;
        }
        static public int BuscarIndexPorNombre(string nombre, List<Producto> x)
        {
            int ind = 0;
            while ((ind < x.Count()) && (x[ind].Nombre != nombre))
            {
                ind++;
            }
            return ind;
        }
        static public bool Guardar(List<Producto> x) {
            Direcciones dir = new Direcciones();
            StreamWriter p = new StreamWriter(dir.Productos);
            for (int i = 0; i < x.Count(); i++) {
                p.WriteLine(
                    x[i].Codigo + '|' +
                    x[i].Nombre + '|' +
                    x[i].Descripcion + '|' +
                    x[i].Bulto+ '|' +
                    x[i].Cantidad + '|' +
                    x[i].CantidadEstandar + '|' +
                    x[i].Costo + '|' +
                    x[i].Precio + '|' +
                    x[i].Precio2 + '|' +
                    x[i].Precio3
                    );
            }
            p.Close(); p.Dispose();
            return true;
        }
        static public double SumaCostos(List<Producto> x) {
            double sum = 0;
            for (int i = 0; i < x.Count(); i++) { sum = sum + x[i].ImporteCosto(); }
            return sum;
        }
        static public double SumaVentas(List<Producto> x) {
            double sum = 0;
            for (int i = 0; i < x.Count(); i++) { sum = sum + x[i].ImportePrecio(); }
            return sum;
        }
        static public void MostrarDataGrid(ref DataGridView y)
        {
            y.Rows.Clear();
            List<Producto> x = Producto.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                y.Rows.Add(
                    x[i].Codigo,
                    x[i].Nombre,
                    x[i].Descripcion,
                      x[i].Bulto,
                    x[i].Cantidad,
                    x[i].CantidadEstandar,
                    x[i].Costo,
                    x[i].Precio,
                    x[i].Precio2,
                    x[i].Precio3
                    );
            }

        }
        static public void CargarComboBox( ref ComboBox yNom)
        {
            
            yNom.Items.Clear();
            List<Producto> x = Producto.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                yNom.Items.Add(x[i].Codigo);
                yNom.Items.Add(x[i].Nombre);
            }

        }

        static public void CargarComboBox(ref ComboBox yNom,string descripcion)
        {
            
            yNom.Items.Clear();
            List<Producto> x = Producto.Buscar();
            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i].descripcion==descripcion){

                    yNom.Items.Add(x[i].Codigo);
                    yNom.Items.Add(x[i].Nombre);
                }
            }

        }
        static public void AgregarProductosCompra(ref List<Producto> x, Producto y)
        {

            int ind = Producto.BuscarIndexPorCodigo(y.Codigo, x);
            y.precio = y.ImporteCosto();
            if (ind < x.Count())
            {
                x[ind].SumarBulto(y.Bulto);
                x[ind].SumarCantidad(y.Cantidad);
                x[ind].SumarCosto(y.precio);
            }
            else { x.Add(y); }

        }
        static public void AgregarProductosVenta(ref List<Producto> x, Producto y) {

            int ind = Producto.BuscarIndexPorCodigo(y.Codigo, x);
            y.precio = y.ImportePrecio();
            if (ind < x.Count())
            {
                x[ind].SumarCantidad(y.Cantidad);
                x[ind].SumarBulto(y.Bulto);
                x[ind].SumarPrecio(y.precio);
            }
            else { x.Add(y); }

        }
        static public void AgregarProductosRegistradora(ref List<Producto> x, Producto y)
        {

            int ind = Producto.BuscarIndexPorCodigo(y.Codigo, x);
            if (ind < x.Count())
            {
                x[ind].SumarCantidad(y.Cantidad);
                x[ind].SumarBulto(y.Bulto);
                x[ind].SumarPrecio(y.precio);
            }
            else { x.Add(y); }

        }
        static public void MostrarVentas(ref DataGridView x, List<Producto> y) {
            x.Rows.Clear();
            for (int i = 0; i < y.Count(); i++) {
                x.Rows.Add(
                    y[i].Nombre,
                    Producto.BuscarPorCodigo(y[i].Codigo).descripcion,
                      y[i].Bulto,
                    y[i].Cantidad,
                    y[i].Precio
                    );
            }
        }
        static public void MostrarCompras(ref DataGridView x, List<Producto> y)
        {
            x.Rows.Clear();
            for (int i = 0; i < y.Count(); i++)
            {
                x.Rows.Add(
                    y[i].Nombre,
                    Producto.BuscarPorCodigo(y[i].Codigo).descripcion,
                      y[i].Bulto,
                    y[i].Cantidad,
                    y[i].Costo
                    );
            }
        }

        static public void SumasDeConsolidados(ref ListBox x, List<Producto> y) {
            double sum = 0;
            for (int i = 0; i < y.Count(); i++) {
                sum = sum + y[i].Precio;
            }

            x.Items.Add("Total Venta: $"+sum);
        }

        static public void SumarStock(List<Producto> x) {

            List<Producto> y = Producto.Buscar();

            for (int i  =0; i< x.Count();i++) {

                int ind = Producto.BuscarIndexPorCodigo(x[i].Codigo,y);
                if (ind<y.Count()) {
                    y[ind].Cantidad += x[i].Cantidad;
                    y[ind].Bulto += x[i].Bulto;
                    y[ind].Costo = x[i].Costo;
                }

            }
            Producto.Guardar(y);
        }

        static public void RestarStock(List<Producto> x) {


            List<Producto> y = Producto.Buscar();

            for (int i = 0; i < x.Count(); i++)
            {
                int ind = Producto.BuscarIndexPorCodigo(x[i].Codigo, y);
                y[ind].Cantidad -= x[i].Cantidad;
                y[ind].Bulto-= x[i].Bulto;

            }
            Producto.Guardar(y);

        }


        static public List<Producto> Consolidar(ref DataGridView rs, List<RemitoVenta> x, List<RemitoRegistradora> y) {

            List<Producto> resultado = new List<Producto>();

            for (int i = 0; i < x.Count(); i++)
            {
                for (int j = 0; j < x[i].ListaProdutos.Count(); j++)
                {
                    Producto.AgregarProductosVenta(ref resultado, x[i].ListaProdutos[j]);
                }
            }
            for (int i = 0; i < y.Count(); i++)
            {
                for (int j = 0; j < y[i].ListaProdutos.Count(); j++)
                {
                    Producto.AgregarProductosRegistradora(ref resultado, y[i].ListaProdutos[j]);
                }
            }
            Producto.MostrarVentas(ref rs,resultado);
            return resultado;
        }
    }
}