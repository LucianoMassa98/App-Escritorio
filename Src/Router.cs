using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop
{
    
    class  Direcciones
    {

        string user, space;

        public Direcciones() {
            user = Environment.UserName;
            space = "MyG";
           
          
        }
        public string Space { get { return space; }set { space = value; } }
        public string Usuarios { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\"+ space +"\\usrs.txt"; ; } }
        public string Productos { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\prds.txt"; } }
        public string Pagos { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\pgs.txt"; } }
        public string RemitoCompras { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\cmprs.txt"; } }
        public string RemitoProducciones { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\prcns.txt"; } }

        public string RemitoVentas { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\vnts.txt"; } }
        public string Clientes { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\clnts.txt"; } }
        public string Proveedores { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\pvds.txt"; } }
        public string Descuentos { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\dcts.txt"; } }
        public string RemitoRegistradoras { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\rgts.txt"; } }
        public string CobroClientes { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\"+ space +"\\cbocl.txt"; } }
        public string PagoProveedores { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\pgpv.txt"; } }
        public string ArchivoPdf { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\Pdfs\\Pdf-"; } }
        public string Logo { get { return @"C:\\Users\\" + user + "\\OneDrive\\SetData\\" + space + "\\Images\\logo.png"; } }
        //"TP806L   |     POS-58"
        public string Impresora { get { return "POS-58"; } }

    }
}
 