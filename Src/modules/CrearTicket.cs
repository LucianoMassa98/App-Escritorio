﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace E_Shop
{
    class CrearTicket
    {
        //Creamos un objeto de la clase StringBuilder, en este objeto agregaremos las lineas del ticket
        StringBuilder linea = new StringBuilder();
        //Creamos una variable para almacenar el numero maximo de caracteres que permitiremos en el ticket.
        int maxCar, cortar;//Para una impresora ticketera que imprime a 40 columnas. La variable cortar cortara el texto cuando rebase el limte.
        public CrearTicket(int car) { maxCar = car; }
        public void ImprimirLogo(string direccion) {

            
        
        }
        //Creamos el primer metodo, este dibujara lineas guion.
        public string lineasGuio()
        {
            string lineasGuion = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasGuion += "-";//Agregara un guio hasta llegar la numero maximo de caracteres.
            }
            return linea.AppendLine(lineasGuion).ToString(); //Devolvemos la lineaGuion
        }

        //Metodo para dibujar una linea con asteriscos
        public string lineasAsteriscos()
        {
            string lineasAsterisco = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasAsterisco += "*";//Agregara un asterisco hasta llegar la numero maximo de caracteres.
            }
            return linea.AppendLine(lineasAsterisco).ToString(); //Devolvemos la linea con asteriscos
        }
        public string lineasBarra()
        {
            string lineasAsterisco = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasAsterisco += "|";//Agregara un asterisco hasta llegar la numero maximo de caracteres.
            }
            return linea.AppendLine(lineasAsterisco).ToString(); //Devolvemos la linea con asteriscos
        }
        //Realizamos el mismo procedimiento para dibujar una lineas con el signo igual
        public string lineasIgual()
        {
            string lineasIgual = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasIgual += "=";//Agregara un igual hasta llegar la numero maximo de caracteres.
            }
            return linea.AppendLine(lineasIgual).ToString(); //Devolvemos la lienas con iguales
        }

        //Creamos el encabezado para los articulos
        public void EncabezadoVenta()
        {
            //Escribimos los espacios para mostrar el articulo. En total tienen que ser 40 caracteres
            if (maxCar == 32)
            {
                linea.AppendLine("ARTICULO |CANT|PRECIO|IMPORTE");
            }
            else {
                linea.AppendLine("ARTICULO                 |CANT|PRECIO|IMPORTE");
            } 
        }

        public void EncabezadoHuevos()
        {
            linea.AppendLine("HUEVOS      | CANTIDAD | IMPORTE");
        }
        public void EncabezadoMercaderia()
        {
            linea.AppendLine("MERCADERIA  | CANTIDAD | IMPORTE");
        }

        public void EncabezadoClientes()
        {
            linea.AppendLine("CLIENTE    |HVOS | MRIA| IMPORTE");
        }
        //Creamos un metodo para poner el texto a la izquierda
        public void TextoIzquierda(string texto)
        {
            //Si la longitud del texto es mayor al numero maximo de caracteres permitidos, realizar el siguiente procedimiento.
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    //Agregamos los fragmentos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                //agregamos el fragmento restante
                linea.AppendLine(texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                //Si no es mayor solo agregarlo.
                linea.AppendLine(texto);
            }
        }

        //Creamos un metodo para poner texto a la derecha.
        public void TextoDerecha(string texto)
        {
            //Si la longitud del texto es mayor al numero maximo de caracteres permitidos, realizar el siguiente procedimiento.
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    //Agregamos los fragmentos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                //Variable para poner espacios restntes
                string espacios = "";
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length); i++)
                {
                    espacios += " ";//Agrega espacios para alinear a la derecha
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < (maxCar - texto.Length); i++)
                {
                    espacios += " ";//Agrega espacios para alinear a la derecha
                }
                //Si no es mayor solo agregarlo.
                linea.AppendLine(espacios + texto);
            }
        }

        //Metodo para centrar el texto
        public void TextoCentro(string texto)
        {
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    //Agregamos los fragmentos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                //Variable para poner espacios restntes
                string espacios = "";
                //sacamos la cantidad de espacios libres y el resultado lo dividimos entre dos
                int centrar = (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length) / 2;
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";//Agrega espacios para centrar
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";
                //sacamos la cantidad de espacios libres y el resultado lo dividimos entre dos
                int centrar = (maxCar - texto.Length) / 2;
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";//Agrega espacios para centrar
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                linea.AppendLine(espacios + texto);

            }
        }

        //Metodo para poner texto a los extremos
        public void TextoExtremos(string textoIzquierdo, string textoDerecho)
        {
            // cantidad maxcar = 
            //variables que utilizaremos
            string textoIzq, textoDer, textoCompleto = "", espacios = "";

            //Si el texto que va a la izquierda es mayor a 18, cortamos el texto.
            int espacioIzquierdo = 18;
            if (textoIzquierdo.Length > espacioIzquierdo)
            {
                cortar = textoIzquierdo.Length - espacioIzquierdo;
                textoIzq = textoIzquierdo.Remove(espacioIzquierdo, cortar);
            }
            else
            { textoIzq = textoIzquierdo; }

            textoCompleto = textoIzq;//Agregamos el primer texto.

            int espacioDerecho = 14;
            if (textoDerecho.Length > espacioDerecho)//Si es mayor a 20 lo cortamos
            {
                cortar = textoDerecho.Length - espacioDerecho;
                textoDer = textoDerecho.Remove(espacioDerecho, cortar);
                
            }
            else
            { textoDer = textoDerecho; }

            //Obtenemos el numero de espacios restantes para poner textoDerecho al final
            int nroEspacios = maxCar - (textoIzq.Length + textoDer.Length);
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";//agrega los espacios para poner textoDerecho al final
            }
            textoCompleto += espacios + textoDerecho;//Agregamos el segundo texto con los espacios para alinearlo a la derecha.
            linea.AppendLine(textoCompleto);//agregamos la linea al ticket, al objeto en si.
        }

        //Metodo para agregar los totales d ela venta
        public void AgregarTotales(string texto, double total)
        {
            //Variables que usaremos
            string resumen, valor, textoCompleto, espacios = "";

            int espacioTexto = 18;
            if (texto.Length > espacioTexto)//Si es mayor a 25 lo cortamos
            {
                cortar = texto.Length - espacioTexto;
                resumen = texto.Remove(espacioTexto, cortar);
            }
            else
            { resumen = texto; }

            textoCompleto = resumen;
            valor = total.ToString("#,#.00");//Agregamos el total previo formateo.

            //Obtenemos el numero de espacios restantes para alinearlos a la derecha
            int nroEspacios = maxCar - (resumen.Length + valor.Length);
            //agregamos los espacios
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textoCompleto += espacios + valor;
            linea.AppendLine(textoCompleto);
        }

        //Metodo para agreagar articulos al ticket de venta
        public void AgregaArticulo(string articulo, double cant, double precio, double importe)
        {
            //Valida que cant precio e importe esten dentro del rango.
            int espacioArticulo = 11;
            int espacioCnt = 6;
            int espacioPrecio = 6;
            int espacioImporte = 9;
            if (cant.ToString().Length <= espacioCnt && 
                precio.ToString().Length <= espacioPrecio && 
                importe.ToString().Length <= espacioImporte)
            {
                string elemento = "", espacios = "";
                bool bandera = false;//Indicara si es la primera linea que se escribe cuando bajemos a la segunda si el nombre del articulo no entra en la primera linea
                int nroEspacios = 0;

                //Si el nombre o descripcion del articulo es mayor a 20, bajar a la siguiente linea
                   if (articulo.Length > espacioArticulo)
                {
                    articulo = articulo.Substring(0, espacioArticulo);

                }
                   for (int i = 0; i < (espacioArticulo - articulo.Length); i++)
                    {
                        espacios += " "; //Agrega espacios para completar los 20 caracteres
                    }
                    elemento = articulo + espacios;

                    //Colocar la cantidad a la derecha.
                    nroEspacios = (espacioCnt - cant.ToString().Length);// +(20 - elemento.Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();

                    //Colocar el precio a la derecha.
                    nroEspacios = (espacioPrecio - precio.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios+ precio.ToString();

                    //Colocar el importe a la derecha.
                    nroEspacios = (espacioImporte - importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + importe.ToString();

                    linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                
            }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }
      

        public void AgregaArticulo(string articulo, double cant)
        {
            //Valida que cant precio e importe esten dentro del rango.
            if (cant.ToString().Length <= 5)
            {
                string elemento = "", espacios = "";
                bool bandera = false;
                int nroEspacios = 0;
                if (articulo.Length > 35)
                {
                    nroEspacios = (5 - cant.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";//Generamos los espacios necesarios para alinear a la derecha
                    }
                    elemento += espacios + cant.ToString();//agregamos la cantidad con los espacios
                    int caracterActual = 0;
                    for (int longitudTexto = articulo.Length; longitudTexto > 35; longitudTexto -= 35)
                    {
                        if (bandera == false)//si es false o la primera linea en recorrerer, continuar...
                        {
                            //agregamos los primeros 20 caracteres del nombre del articulos, mas lo que ya tiene la variable elemento
                            linea.AppendLine(articulo.Substring(caracterActual, 35) + elemento);
                            bandera = true;//cambiamos su valor a verdadero
                        }
                        else
                            linea.AppendLine(articulo.Substring(caracterActual, 35));//Solo agrega el nombre del articulo

                        caracterActual += 35;//incrementa en 20 el valor de la variable caracterActual
                    }
                    //Agrega el resto del fragmento del  nombre del articulo
                    linea.AppendLine(articulo.Substring(caracterActual, articulo.Length - caracterActual));

                }
                else //Si no es mayor solo agregarlo, sin dar saltos de lineas
                {
                    for (int i = 0; i < (35 - articulo.Length); i++)
                    {
                        espacios += " "; //Agrega espacios para completar los 20 caracteres
                    }
                    elemento = articulo + espacios;

                    //Colocar la cantidad a la derecha.
                    nroEspacios = (5 - cant.ToString().Length);// +(20 - elemento.Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();



                    linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                }
            }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }

        public void AgregaArticulo(string articulo, double cant, double importe)
        {
            //Valida que cant precio e importe esten dentro del rango.
            
            
            int espacioArticulo = 17;
            int espacioCantidad = 6;
            int espacioImporte = 9;
            //Valida que cant precio e importe esten dentro del rango.
            if (cant.ToString().Length <= espacioCantidad && importe.ToString().Length <= espacioImporte)
            {
                string elemento = "", espacios = "";
               
                int nroEspacios = 0;

                //Si el nombre o descripcion del articulo es mayor a 20, bajar a la siguiente linea
                if (articulo.Length > espacioArticulo)
                {
                    cortar = espacioArticulo - articulo.Length;
                    articulo = articulo.Substring(0, 17);
                }
                for (int i = 0; i < (espacioArticulo - articulo.Length); i++)
                {
                    espacios += " ";
                }
                elemento = articulo + espacios;

                //Colocar la cantidad a la derecha.
                nroEspacios = (espacioImporte - cant.ToString().Length - 1);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += cant.ToString() + espacios;

                elemento += "$" + importe.ToString();

                linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
            }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }
        //Metodos para enviar secuencias de escape a la impresora
        //Para cortar el ticket
        public void CortaTicket()
        {
            linea.AppendLine("\x1B" + "m"); //Caracteres de corte. Estos comando varian segun el tipo de impresora
            // linea.AppendLine("\x1B" + "d" + "\x09"); //Avanza 9 renglones, Tambien varian
        }
        //Para abrir el cajon
        public void AbreCajon()
        {
            //Estos tambien varian, tienen que ever el manual de la impresora para poner los correctos.
            linea.AppendLine("\x1B" + "p" + "\x00" + "\x0F" + "\x96"); //Caracteres de apertura cajon 0
            //linea.AppendLine("\x1B" + "p" + "\x01" + "\x0F" + "\x96"); //Caracteres de apertura cajon 1
        }
        //Para mandara a imprimir el texto a la impresora que le indiquemos.
        public void ImprimirTicket(string impresora)
        {
            //Este metodo recibe el nombre de la impresora a la cual se mandara a imprimir y el texto que se imprimira.
            //Usaremos un código que nos proporciona Microsoft. https://support.microsoft.com/es-es/kb/322091

            RawPrinterHelper.SendStringToPrinter(impresora, linea.ToString()); //Imprime texto.
            linea.Clear();//Al cabar de imprimir limpia la linea de todo el texto agregado.
        }
    }


    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "Ticket de Venta";//Este es el nombre con el que guarda el archivo en caso de no imprimir a la impresora fisica.
            di.pDataType = "RAW";//de tipo texto plano

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}
