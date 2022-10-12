using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace E_Shop
{
    static class RemitoVentaValidador
    {
        static public bool CrearRemitoVenta(RemitoVenta x)
        {
            if (x.Emisor == "") {
                MessageBox.Show("Falta Emisor");
                return false; }
            if (x.Receptor == "") {
                MessageBox.Show("Falta Cliente");
                return false; }
            if (x.FechaEmision == "") {
                MessageBox.Show("Falta fecha");
                return false; }
            if (x.Pagos.Count() == 0) {
                MessageBox.Show("Falta medios de cobro");
                return false; }
            if (x.ListaProdutos.Count() == 0) {
                MessageBox.Show("Falta productos");
                return false; }
            if(Math.Round(x.TotalVenta()) !=Math.Round(x.TotalPago()))
            {
                MessageBox.Show("Falta cargar cobros "+ x.TotalVenta()+" - "+x.TotalPago());
                return false;
            }

            List<RemitoVenta> Lista = RemitoVenta.Buscar();
            x.Codigo = (Lista.Count() + 1).ToString();
            return true;
        }
        static public bool GetRemitoVenta(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoVenta(string codigo, RemitoVenta x)
        {
            if (x.Emisor == "")
            {
                MessageBox.Show("Falta Emisor");
                return false;
            }
            if (x.Receptor == "")
            {
                MessageBox.Show("Falta Cliente");
                return false;
            }
            if (x.FechaEmision == "")
            {
                MessageBox.Show("Falta fecha");
                return false;
            }
            if (x.Pagos.Count() == 0)
            {
                MessageBox.Show("Falta medios de cobro");
                return false;
            }
            if (x.ListaProdutos.Count() == 0)
            {
                MessageBox.Show("Falta productos");
                return false;
            }
            if (Math.Round(x.TotalVenta()) != Math.Round(x.TotalPago()))
            {
                MessageBox.Show("Falta cargar cobros");
                return false;
            }

            return RemitoVentaValidador.GetRemitoVenta(codigo);

        }
        static public bool AgregarProducto(ref Producto x,ref Descuento y) {
            // Verificar si el producto viene con codigo o nombre
            // completar segun corresponda
            // Colocar importe, aplicando descuento de ser necesario
           
            
            List<Producto> Stock = Producto.Buscar();
            Descuento dsc = Descuento.BuscarPorNombre(y.Nombre);
            if (dsc == null) { return false; }


            double ImporteDescontar = dsc.ImporteDescuento;

            if (x.Cantidad == 0) { return false; }
            if (x.Cantidad < dsc.CantidadRequerida) { return false; }

            int i;
            if ((i = Producto.BuscarIndexPorCodigo(x.Codigo, Stock)) < Stock.Count()) {

                x.Nombre = Stock[i].Nombre;
                x.Descripcion = Stock[i].Descripcion;
                if (dsc.Nombre == "Bonificado") {
                    x.Precio = 0;
                }
                else
                {
                    x.Precio = Stock[i].Precio - ImporteDescontar;
                }
                return true;
            }

            if ((i = Producto.BuscarIndexPorNombre(x.Nombre, Stock)) < Stock.Count()) {

                x.Codigo = Stock[i].Codigo;
                x.Descripcion = Stock[i].Descripcion;
                if (dsc.Nombre == "Bonificado")
                {
                    x.Precio = 0;
                }
                else
                {
                    x.Precio = Stock[i].Precio - ImporteDescontar;
                }
                return true;

            }
            return false; 
        }
        static public bool AgregarProducto(ref Producto x)
        {
            

            List<Producto> Stock = Producto.Buscar();
          
            if (x.Bulto == 0) { return false; }
            if (x.Cantidad == 0) { return false; }

            int i;
            if ((i = Producto.BuscarIndexPorCodigo(x.Codigo, Stock)) < Stock.Count())
            {

                x.Nombre = Stock[i].Nombre;
                x.Descripcion = Stock[i].Descripcion;
                
                return true;
            }

            if ((i = Producto.BuscarIndexPorNombre(x.Nombre, Stock)) < Stock.Count())
            {

                x.Codigo = Stock[i].Codigo;
                x.Descripcion = Stock[i].Descripcion;
             
                return true;

            }
            return false;
        }
    }
}
