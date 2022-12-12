﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace E_Shop
{
    static class RemitoIngresoValidador
    {
        static public bool CrearRemitoIngreso(RemitoIngreso x)
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
            
            if (x.ListaProdutos.Count() == 0) {
                MessageBox.Show("Falta productos");
                return false; }
          

            List<RemitoIngreso> Lista = RemitoIngreso.Buscar();
            x.Codigo = (Lista.Count() + 1).ToString();
            return true;
        }
        static public bool GetRemitoIngreso(string CadenaTexto)
        {
            if (CadenaTexto == "") { return false; }
            return true;
        }
        static public bool ActualizarRemitoIngreso(string codigo, RemitoIngreso x)
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
            
            if (x.ListaProdutos.Count() == 0)
            {
                MessageBox.Show("Falta productos");
                return false;
            }
           

            return RemitoIngresoValidador.GetRemitoIngreso(codigo);

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
