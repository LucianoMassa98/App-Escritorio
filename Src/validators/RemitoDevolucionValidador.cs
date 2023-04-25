using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace E_Shop
{
    static class RemitoDevolucionValidador
    {
        static public bool CrearRemitoDevolucion(RemitoDevolucion x)
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

            List<RemitoDevolucion> Lista = RemitoDevolucion.Buscar();
            x.Codigo = (Lista.Count() + 1).ToString();
            return true;
        }
        static public bool GetRemitoDevolucion(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoDevolucion(string codigo, RemitoDevolucion x)
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
