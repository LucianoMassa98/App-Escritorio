using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace E_Shop
{
    static class AfipServices
    {
        static public string generarTra() {
            UInt32 _globalUniqueID = 0;
            _globalUniqueID += 1;
            XmlWriter p = XmlWriter.Create(new Direcciones().Tra);
            p.WriteStartDocument();
            p.WriteStartElement("loginTicketRequest");

                p.WriteStartElement("header");

                   /* p.WriteStartElement("source");
                    p.WriteString("cn=demo1");
                    p.WriteString(",ou=facturacion");
                    p.WriteString(",o=destored");
                    p.WriteString(",c=ar");
                    p.WriteString(",serialNumber=CUIT 20408237743");
                    p.WriteEndElement();

                    p.WriteStartElement("destination");
                    p.WriteString("cn=wsaahomo");
                    p.WriteString(",o=afip");
                    p.WriteString(",c=ar");
                    p.WriteString(",serialNumber=CUIT 33693450239");
                    p.WriteEndElement();*/

                    p.WriteStartElement("uniqueId");
                    p.WriteString(Convert.ToString(_globalUniqueID));
                    p.WriteEndElement();

                    p.WriteStartElement("generationTime");
                    p.WriteString(DateTime.Now.AddMinutes(-10).ToString("s"));
                    p.WriteEndElement();

                    p.WriteStartElement("expirationTime");
                    p.WriteString(DateTime.Now.AddMinutes(+10).ToString("s"));
                    p.WriteEndElement();

                p.WriteEndElement();

                p.WriteStartElement("service");
                p.WriteString("wsfe");
                
            p.WriteEndDocument();
            p.Close();

            return p.ToString();
           
        }


    }
}
