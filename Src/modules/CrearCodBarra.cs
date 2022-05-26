using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using BarcodeLib;

namespace E_Shop
{
    static class CodigoBarra
    { 
      static public Image Generar(string codigo) {

            BarcodeLib.Barcode Codigox = new BarcodeLib.Barcode();
            Codigox.IncludeLabel = true;
            Image x = Codigox.Encode(
                        BarcodeLib.TYPE.CODE128, 
                        codigo, 
                        Color.Black, 
                        Color.White, 
                        400, 
                        100
                        );
            return x;
            
        }

    }
}
