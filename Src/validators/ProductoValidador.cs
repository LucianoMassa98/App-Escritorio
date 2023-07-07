using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace E_Shop{
    static  class ProductoValidador
    {
        static public bool CrearProducto(Producto x)
        {
            if (x.Codigo == "") {
                MessageBox.Show("Ingresar código");
                return false; }
            if (x.Nombre == "") {
                MessageBox.Show("Ingresar nombre");
                return false; }
            if (x.Descripcion == "") {
                MessageBox.Show("Ingresar descripción");
                return false; }
            if (x.Cantidad < -1) { 
                return false; }
            if (x.CantidadEstandar < -1) {
                MessageBox.Show("Ingresar nuevamente cantidad estandar");
                return false; }
            if (x.Costo < -1) {
                MessageBox.Show("Ingresar nuevamente costo");
                return false; }
            if (x.Precio < -1) {
                MessageBox.Show("Ingresar nuevamente precio");
                return false; }
            /*if (x.Costo > x.Precio) {
                MessageBox.Show("Costo mayor"); 
                return false; }
            if (x.Costo == x.Precio) {
                MessageBox.Show("Costo igua"); 
                return false; }*/
            if (Producto.BuscarPorCodigo(x.Codigo)!=null) {
                MessageBox.Show("Código de producto ya existente");
                return false; }
            return true;
        }
        static public bool GetProducto(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarProducto(string codigo, Producto x)
        {
            return ProductoValidador.GetProducto(codigo);

        }
       
    }
}
