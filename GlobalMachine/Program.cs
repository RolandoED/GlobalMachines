using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace GlobalMachine
{
    class Program
    {
        public static string _titulo = "*-*\tGlobal Machines\t  *-*";
        public static string  _menu  = 
            "*-*\tGlobal Machines\t*-*\n1.Ingreso productos\n2.Modificación de productos\n3.Eliminacion de progductos"
            + "\n4.Búsqueda de productos\n5.Listado de productos\n6.Venta de productos\n7.Lectura de Archivos\n8.Salir";

        static String SEPARADOR = "---------------------------------------------";
        static String SEPARADOR2 = "=============================================";
        static int option = 0;
        static int size = 0;
        static int i = 0;
        static int factnum = 1;
        static Producto[] Productos;

        struct Producto
        {
            public int codigo_articulo { get; set; }
            public string nombre_articulo { get; set; }
            public string tipo { get; set; }
            public string fecha_vencimiento { get; set; }
            public string nombre_proveedor { get; set; }
            public int posicion_estante { get; set; }
            public int precio { get; set; }
            public bool descuento { get; set; }
        };

        [Flags]
        enum ProductoDiscount
        {
            Frituras = 15,
            Repostería = 20,
            Refrescos_Gaseosos = 05,
            Refrescos_Naturales = 25,
            Confitería = 10,
        };

        static void Main(string[] args)
        {
            if (size <= 0)
            {
                Console.WriteLine("Ingrese la cantidad máxima de productos a registrar\nIngresar 5 o más de 5 para que los objetos default carguen");
                size = int.Parse(Console.ReadLine());
                Productos = new Producto[size];
            }

            //llena array de dummy data
            for (int e = 0; e < 4; e++)
            {
                Productos[e].codigo_articulo = e + 1;
                Productos[e].tipo = "reposteria";
                Productos[e].nombre_articulo = "Galletas"+e;
                Productos[e].fecha_vencimiento = "12/12/16";
                Productos[e].nombre_proveedor = "Poz";
                Productos[e].posicion_estante = 3;
                Productos[e].precio = 500;
                Productos[e].descuento = true;
                i++;
            }
            //Main Menu
            do
            {
                Console.Clear();
                Console.WriteLine(_menu);
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        {
                            AgregarProducto();
                            break;
                        }
                    case 2:
                        {
                            ModificarProductos();
                            break;
                        }
                    case 3:
                        {
                            BorrarProductos();
                            break;
                        }
                    case 4:
                        {
                            EncontrarProductos();
                            break;
                        }
                    case 5:
                        {
                            MostrarProductos();
                            break;
                        }
                    case 6:
                        {
                            VentadeProductos();
                            break;
                        }
                    case 7:
                        {
                            LecturaArchivoPlano();
                            break;
                        }
                    case 8:
                        option = 8;
                        break;
                    default:
                        {
                            Console.WriteLine("Seleccione inválida..");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                }
            } while (option != 8);
            Console.WriteLine("Saliendo del programa");
            //Console.ReadKey();

        }

        private static void LecturaArchivoPlano()
        {
            try
            {


            string currentPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
            string line;
            string filename = "";
            Console.WriteLine("Ingrese el nombre del archivo a leer en el root del proyecto");
            filename = Console.ReadLine();
            StreamReader sr = new StreamReader(currentPath+"\\"+filename);
            line = sr.ReadLine();
            while (line != null)
            {
                Console.WriteLine(line);
                line = sr.ReadLine();
            }            
            sr.Close();
            Console.ReadLine();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("ERROR" + e.Message);
                Console.ReadLine();
            }
        }

        private static void VentadeProductos()
        {
            List<ProductoInvoice> arrayfactura = new List<ProductoInvoice>();
            int ID = 0;
            int r = 0;
            string decide = "";
            bool found = false;
            string nombrecliente = "";
            string telefonocliente = "";
            Console.Clear();
            Console.WriteLine("Ingrese el nombre del cliente: ");
            nombrecliente = Console.ReadLine();
            Console.WriteLine("Ingrese el teléfono del cliente: ");
            telefonocliente = Console.ReadLine();
            while (r != 1)
            {
                Console.Clear();
                //Console.WriteLine(_titulo);
                Console.WriteLine(ListarCompraProductos());                
                Console.WriteLine("Ingrese el ID del producto: ");
                ID = int.Parse(Console.ReadLine());
                for (i = 0; i < Productos.Length; i++)
                {
                    if (ID == Productos[i].codigo_articulo && ID != 0)
                    {
                        ProductoInvoice agregar = new ProductoInvoice();
                        //reg = i + 1;
                        Console.WriteLine("Ingrese la cantidad de {0} ", Productos[i].nombre_articulo);
                        agregar.cantidad = int.Parse(Console.ReadLine());
                        agregar.codigo_articulo = Productos[i].codigo_articulo;
                        agregar.nombre_articulo = Productos[i].nombre_articulo;
                        agregar.tipo = Productos[i].tipo;
                        agregar.posicion_estante = Productos[i].posicion_estante;
                        agregar.precio = Productos[i].precio;
                        agregar.descuento = Productos[i].descuento;
                        arrayfactura.Add(agregar);                        
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No se encontro ese ID");
                    found = false;
                }
                Console.WriteLine("Desea facturar otro Producto 0-Sí 1-No");
                r = int.Parse(Console.ReadLine());
            }
            //obtener total
            double total = 0;
            double descuento = 0;

            foreach (ProductoInvoice item in arrayfactura)
            {
                if (item.descuento == true)
                {
                    if (item.posicion_estante !=1 || item.posicion_estante !=2)
                    {
                        total += item.precio * item.cantidad;
                        if (item.tipo.Equals("fritura"))
                        {
                            descuento += (item.precio * item.cantidad) * 0.15;
                        }
                        else if (item.tipo.Equals("reposteria"))
                        {
                            descuento += (item.precio * item.cantidad) * 0.20;                        
                        }
                        else if (item.tipo.Equals("refrescos_gaseosos"))
                        {
                            descuento += (item.precio * item.cantidad) * 0.05;
                        }
                        else if (item.tipo.Equals("refrescos_naturales"))
                        {
                            descuento += (item.precio * item.cantidad) * 0.25;
                        }
                        // defecto confiteria
                        else if (item.tipo.Equals("confiteria"))
                            descuento += (item.precio * item.cantidad) * 0.10;
                    }                    
                }
                else
                    total += item.precio * item.cantidad;
            }
            retry:
            Console.Clear();
            Console.WriteLine("El total es {0}", (total - descuento)*1.13);
            Console.WriteLine("Ingrese con cuanto paga: ");
            int pagacon = int.Parse(Console.ReadLine());
            if (pagacon < ((total-descuento)*1.13))
            {
                Console.WriteLine("Dinero insuficiente ");
                Console.ReadKey();
                goto retry;
            }

            Console.Clear();
            string factura = "";
            factura += SEPARADOR2 + "\n" + _titulo + "\n" + SEPARADOR2+"\n";
            factura += "Factura Proforma N° " + factnum + "\n";
            factnum++;
            factura += "Cliente: " + nombrecliente + "\n";
            factura += "Teléfono: " + telefonocliente + "\n\n";
            factura += SEPARADOR + "\n";
            factura += "Item\tctd\tprecio\tsubtotal\t\n";
            foreach (ProductoInvoice item in arrayfactura)
            {
                factura += item.nombre_articulo + "\t" + item.cantidad + "\t" + item.precio + "\t" + (item.precio * item.cantidad) + "\n";
            }
            factura += SEPARADOR+"\n";
            factura += "Total Orden: ¢ " + (total - descuento) + "\n";
            factura += "Descuento: ¢ " + descuento + "\n";
            factura += "Impuesto Venta: 13% \n";
            factura += "Impuesto Total: ¢ " + (total - descuento) * 0.13 + "\n";
            factura += "Precio Neto: ¢ " + (total - descuento) * 1.13 + "\n";
            factura += SEPARADOR + "\n";
            factura += "Monto Recibido: ¢" + pagacon + " \n";
            factura += "Monto Cambio: ¢ " + (pagacon - ((total - descuento) * 1.13))+"\n";
            factura += SEPARADOR2 + "\n";
            factura += "Gracias por su compra...\n";
            Console.WriteLine(factura);

            Console.WriteLine("Desea guardar la factura (SI) ?");
            decide = Console.ReadLine();
            if (decide.Equals("si"))
            {
                guardarDoc("factura"+factnum+".txt", factura);
            }
            Console.ReadLine();
        }

        private static void BorrarProductos()
        {
            //Eliminar por ID
            int ID = 0;
            int r = 0;
            bool found = false;
            while (r != 1)
            {
                Console.Clear();
                Console.WriteLine(_titulo);
                Console.WriteLine("Búsquedad de Productos para ELIMINAR");
                Console.WriteLine("Digite el ID del Producto a ELIMINAR: ");
                ID = int.Parse(Console.ReadLine());
                for (i = 0; i < Productos.Length; i++)
                {
                    if (ID == Productos[i].codigo_articulo && ID != 0)
                    {
                        //reg = i + 1;
                        Console.WriteLine("Eliminado\n");
                        Console.WriteLine("{0}:   ", Productos[i].codigo_articulo);
                        Console.WriteLine("{0}:    ", Productos[i].nombre_articulo);
                        Console.WriteLine("{0}:     ", Productos[i].nombre_proveedor);
                        Console.WriteLine("{0} (1-5):", Productos[i].tipo);
                        Console.WriteLine("{0}:   ", Productos[i].descuento);
                        Console.WriteLine("{0}:     ", Productos[i].fecha_vencimiento);
                        Console.WriteLine("{0}:  ", Productos[i].posicion_estante);
                        Console.WriteLine("{0} Colones : ", Productos[i].precio);
                        Console.WriteLine("{0} Colones : ", Productos[i].tipo);
                        Productos[i].codigo_articulo = 0;
                        Productos[i].nombre_articulo = "";
                        Productos[i].nombre_proveedor = "";
                        Productos[i].tipo = "";
                        Productos[i].descuento = false;
                        Productos[i].fecha_vencimiento = "";
                        Productos[i].posicion_estante = 0;
                        Productos[i].precio = 0;
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No se encontro ese ID");
                    found = false;
                }
                Console.WriteLine("Desea eliminar otro Producto 0-Sí 1-No");
                r = int.Parse(Console.ReadLine());
            }
        }

        private static void AgregarProducto()
        {
            //Producto nuevo = new Producto();
            int r = 0;
            while (r != 1)
            {
                Console.Clear();
                if (i < Productos.Length)
                {
                    Console.WriteLine(_titulo);
                    Console.WriteLine("Ingreso de Productos.             ");
                    Console.Write("Digite el ID:                  ");
                    Productos[i].codigo_articulo = int.Parse(Console.ReadLine());
                    Console.Write("Digite el nombre del producto:    ");
                    Productos[i].nombre_articulo = Console.ReadLine();
                    Console.Write("Digite el Proveedor del producto:     ");
                    Productos[i].nombre_proveedor = Console.ReadLine();
                    Console.WriteLine("Ingrese la categoría del producto:");
                    Console.Write("1. "+ProductoDiscount.Frituras + "\n" +
                                 "2. " + ProductoDiscount.Repostería + "\n" +
                                 "3. " + ProductoDiscount.Refrescos_Gaseosos + "\n" +
                                 "4. " + ProductoDiscount.Refrescos_Naturales + "\n" +
                                 "5. " + ProductoDiscount.Confitería + "\n");
                    Productos[i].tipo = selectorCategoria(Console.ReadLine());
                    //descuento desactivado por defecto
                    Productos[i].descuento = false;
                    Console.Write("Ingrese la fecha de vencimiento del producto:     ");
                    Productos[i].fecha_vencimiento = Console.ReadLine();
                    Console.WriteLine("Ingrese la posicion del producto en la máquina:");
                    Productos[i].posicion_estante = int.Parse(Console.ReadLine());
                    Console.Write("Digite el precio:                  ");
                    Productos[i].precio = int.Parse(Console.ReadLine());
                    Console.WriteLine("Desea agregar otro Producto 0-Sí 1-No");
                    r = int.Parse(Console.ReadLine());
                    i++;
                }
                else
                {
                    Console.WriteLine("Registro de productos Lleno..");
                    r = 1;
                    Console.ReadKey();
                }
            }
        }

        static void EncontrarProductos()
        {
            //Buscar por registro
            int ID = 0;
            //int reg = 0;
            int r = 0;
            bool found = false;
            while (r != 1)
            {
                Console.Clear();
                Console.WriteLine(_titulo);
                Console.WriteLine("Búsquedad de Productos para buscar");
                Console.WriteLine("Digite el ID del Producto a buscar: ");
                ID = int.Parse(Console.ReadLine());
                for (i = 0; i < Productos.Length; i++)
                {
                    if (ID == Productos[i].codigo_articulo && ID != 0)
                    {
                        //reg = i + 1;
                        Console.WriteLine("ID : {0} ", Productos[i].codigo_articulo);
                        Console.WriteLine("Nmbre Producto  : {0}", Productos[i].nombre_articulo);
                        Console.WriteLine("Proveedor  : {0}", Productos[i].nombre_proveedor);
                        Console.WriteLine("Tipo : {0} ", Productos[i].tipo);
                        Console.WriteLine("Descuento activo  :   {0}  ", Productos[i].descuento);
                        Console.WriteLine("Fecha venc  : {0} ", Productos[i].fecha_vencimiento);
                        Console.WriteLine("Posicion : {0} ", Productos[i].posicion_estante);
                        Console.WriteLine("Precio  Colones : {0} ", Productos[i].precio);
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No se encontro ese ID");
                    found = false;
                }
                Console.WriteLine("Desea modificar otro Producto 0-Sí 1-No");
                r = int.Parse(Console.ReadLine());
            }
        }

        static void ModificarProductos()
        {
            //Buscar por registro
            int ID = 0;
            //int reg = 0;
            int r = 0;
            bool found = false;
            while (r != 1)
            {
                Console.Clear();
                Console.WriteLine(_titulo);
                Console.WriteLine("Búsquedad de Productos para modificar");
                Console.WriteLine("Digite el ID del Producto a modificar: ");
                ID = int.Parse(Console.ReadLine());
                for (i = 0; i < Productos.Length; i++)
                {
                    if (ID == Productos[i].codigo_articulo && ID != 0)
                    {
                        //reg = i + 1;
                        Console.Write("Digite el nuevo ID, antes({0}):   ", Productos[i].codigo_articulo);
                        Productos[i].codigo_articulo = int.Parse(Console.ReadLine());
                        Console.Write("Digite el nombre del producto, antes({0}):    ", Productos[i].nombre_articulo);
                        Productos[i].nombre_articulo = Console.ReadLine();
                        Console.Write("Digite el Proveedor del producto, antes({0}):     ",Productos[i].nombre_proveedor);
                        Productos[i].nombre_proveedor = Console.ReadLine();
                        Console.WriteLine("Ingrese la categoría del producto, antes({0}) (1-5):", Productos[i].tipo);
                        Console.Write("1. " + ProductoDiscount.Frituras + "\n" +
                                     "2. " + ProductoDiscount.Repostería + "\n" +
                                     "3. " + ProductoDiscount.Refrescos_Gaseosos + "\n" +
                                     "4. " + ProductoDiscount.Refrescos_Naturales + "\n" +
                                     "5. " + ProductoDiscount.Confitería + "\n");
                        Productos[i].tipo = selectorCategoria(Console.ReadLine());
                        //descuento desactivado por defecto
                        Console.Write("Desea activar el descuento? Si No, antes ({0}):     ", Productos[i].descuento);
                        string decidir = Console.ReadLine();
                        decidir = decidir.ToLower();
                        if (decidir.Equals("si"))
                            Productos[i].descuento = true;
                        if (decidir.Equals("no"))
                            Productos[i].descuento = false;
                        
                        Console.Write("Ingrese la fecha de vencimiento del producto, antes ({0}):     ", Productos[i].fecha_vencimiento);
                        Productos[i].fecha_vencimiento = Console.ReadLine();
                        Console.WriteLine("Ingrese la posicion del producto en la máquina, antes ({0}):  ", Productos[i].posicion_estante);
                        Productos[i].posicion_estante = int.Parse(Console.ReadLine());
                        Console.Write("Digite el precio, antes ({0}) Colones : ", Productos[i].precio);
                        Productos[i].precio = int.Parse(Console.ReadLine());
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("No se encontro ese ID");
                    found = false;
                }
                Console.WriteLine("Desea modificar otro Producto 0-Sí 1-No");
                r = int.Parse(Console.ReadLine());
            }
        }

        static void MostrarProductos()
        {
            string _sep = "==========================================================================================================";
            int reg = 0;
            Console.Clear();

            int niv1 = 0, niv2 = 0, niv3 = 0, niv4 = 0, niv5 = 0;
            int cat1 = 0, cat2 = 0, cat3 = 0, cat4 = 0, cat5 = 0;           
            string output2 = "", output1 = "";
            output1 += "\t\t\t" + _titulo + "\n" + _sep+"\n";
            output1 += "\t\t\tListado de Productos\n";
            output1 += _sep +"\n";
            output1 += "Reg\tCodigo\tNombre\t\tTipo\tVence\t\tProveedor\tPosición\tPrecio\tDescuento";


            for (int i = 0; i < Productos.Length; i++)
            {
                if (Productos[i].codigo_articulo != 0)
                {
                    reg ++;
                    output1 += "\n";
                    output1 += reg+"\t";
                    output1 += Productos[i].codigo_articulo+"\t";
                    output1 += Productos[i].nombre_articulo + "\t";
                    output1 += Productos[i].tipo.Substring(0,4)+ ".."+ "\t";
                    output1 += Productos[i].fecha_vencimiento + "\t";
                    output1 += Productos[i].nombre_proveedor + "\t\t";
                    output1 += Productos[i].posicion_estante + "\t";
                    output1 += "\tCOl " + Productos[i].precio + "\t";
                    output1 += Productos[i].descuento + "\t";
                    //Checkeos para el output2;
                    // por posicion del estante
                    if (Productos[i].posicion_estante == 1)                    
                        niv1++;                    
                    else if (Productos[i].posicion_estante == 2)                    
                      niv2++;                    
                    else if (Productos[i].posicion_estante == 3)                    
                        niv3++;                    
                    else if (Productos[i].posicion_estante == 4)                    
                        niv4++;
                        //5
                    else                     
                        niv5++;
                    //categoria de producto
                    if (Productos[i].tipo.Equals("frituras"))
                        cat1++;
                    else if (Productos[i].tipo.Equals("reposteria"))
                        cat2++;
                    else if (Productos[i].tipo.Equals("refrescos_gaseosos"))
                        cat3++;
                    else if (Productos[i].tipo.Equals("refrescos_naturales"))
                        cat4++;
                        //confiteria
                    else
                        cat5++;

                    //Creando output 2
                }
            }
            output1 += "\n";
            output2 += SEPARADOR2 + "\n";
            output2 += _titulo+ "\n";
            output2 += SEPARADOR + "\n\tInforme de Inventario\n";
            output2 += SEPARADOR2;
            output2 += "\nClasificacion\t\t|| Cantidad\n";
            output2 += "Frituras\t\t|| \t" + cat1 + "\n";
            output2 += "Reposteria\t\t|| \t" + cat2 + "\n";
            output2 += "Refrescos Gaseosos\t|| \t" + cat3 + "\n";
            output2 += "Refrescos Naturales\t|| \t" + cat4 + "\n";
            output2 += "Confiteria\t\t|| \t" + cat5 + "\n\n";
            output2 += "...\n";
            output2 += "   Nivel\t|| Cantidad\n";
            output2 += "      1\t\t|| \t" + niv1 + "\n";
            output2 += "      2\t\t|| \t" + niv2 + "\n";
            output2 += "      3\t\t|| \t" + niv3 + "\n";
            output2 += "      4\t\t|| \t" + niv4 + "\n";
            output2 += "      5\t\t|| \t" + niv5 + "\n";
            output2 += "...\n";
            output2 += SEPARADOR2+"\n";
            output2 += "Fin de listado\n";

            //ENDFOR
            output1 +="\n" +_sep + "\nFin de listado";

            Console.WriteLine(output1);
            Console.WriteLine("");
            Console.WriteLine(output2);

            string decidir = "";
            Console.WriteLine("\nDesea guardar el Listado de productos? si/no ");
            decidir = Console.ReadLine();
            if (decidir.Equals("si"))
            {
                guardarDoc("Listado.txt", output1);
            }
            Console.WriteLine("Desea guardar el Informe de Inventario? si/no ");
            decidir = Console.ReadLine();
            if (decidir.Equals("si"))
            {
                guardarDoc("InformeInventario.txt", output2);
            }
        }


        static string ListarCompraProductos()
        {
            string _sep = "===========================================";
            string output1 = "";
            output1 += "\t" + _titulo + "\n" + _sep + "\n";
            output1 += "\t--Selección de Productos--\n";

            for (int i = 0; i < Productos.Length; i++)
            {
                if (Productos[i].codigo_articulo != 0)
                {
                    output1 += " -" + Productos[i].codigo_articulo + "\t";
                    output1 += Productos[i].nombre_articulo + "\t";
                    output1 += "¢ " + Productos[i].precio + "\t\n";
                }
            }
            return output1;
        }

        static void guardarDoc(string filename , string send)
        {
            string currentPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
            StreamWriter sw = new StreamWriter(currentPath + "\\"+filename, true, Encoding.Unicode); //Ruta
            sw.Write(send);
            sw.Close();
        }

        public static string selectorCategoria(string selec){
            selec = selec.ToLower();
            if (selec.Equals("1"))
	        {
                return "frituras";
	        }
            else if (selec.Equals("2"))
            {
                return "reposteria";
            }
            else if (selec.Equals("3"))
            {
                return "refrescos_gaseosos";
            }
            else if (selec.Equals("4"))
            {
                return "refrescos_naturales";
            }
            else if (selec.Equals("5"))
            {
                return "confiteria";
            }
            return "confiteria";
        }

    }
}
