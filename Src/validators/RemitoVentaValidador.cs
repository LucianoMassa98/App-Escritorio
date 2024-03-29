﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    static class RemitoVentaValidador
    {
        static public bool CrearRemitoVenta(RemitoVenta x)
        {
            if (x.Emisor == "") { return false; }
            if (x.Receptor == "") { return false; }
            if (x.FechaEmision == "") { return false; }
            if (x.Pagos.Count() == 0) { return false; }
            if (x.ListaProdutos.Count() == 0) { return false; }
            try { x.Pagos[0].Importe = x.TotalVenta(); } catch (Exception) { return false; }
            
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
    }
}
