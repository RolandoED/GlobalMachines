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
            +"\n4.Búsqueda de productos\n5.Listado de productos\n6.Venta de productos\n7.Salir";

        static String SEPARADOR = "-------------------------------------------------";
        static String SEPARADOR2 = "=================================================";
        static int option = 0;
        static int size = 0;
        static int i = 0;
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
                Console.WriteLine("Ingrese la cantidad máxima de productos a registrar");

                size = int.Parse(Console.ReadLine());
                Productos = new Producto[size];
            }

            for (int i = 0; i < 4; i++)
            {
                Productos[i].codigo_articulo = i + 1;
                Productos[i].tipo = "confiteria";
                Productos[i].nombre_articulo = "producto"+i;
                Productos[i].fecha_vencimiento = "123/123/123";
                Productos[i].nombre_proveedor = "Sol";
                Productos[i].posicion_estante = 2;
                Productos[i].precio = 500;
                Productos[i].descuento = false;

            }

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
                            //ModifyBooks();
                            break;
                        }
                    case 3:
                        {
                            //DeleteBooks();
                            break;
                        }
                    case 4:
                        {
                            //SeekBooks();
                            break;
                        }
                    case 5:
                        {
                            MostrarProductos();
                            break;
                        }
                    case 6:
                        {
                            //MakeInvoice();
                            break;
                        }
                    case 9:
                        {
                            //TestAdd();
                            break;
                        }
                    case 7:
                        option = 7;
                        break;
                    default:
                        {
                            Console.WriteLine("Seleccione inválida..");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        }
                }
            } while (option != 7);
            Console.WriteLine("Saliendo del programa");
            //Console.ReadKey();

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
                    Productos[i].descuento = false;
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
                reg = i + 1;
                if (Productos[i].codigo_articulo != 0)
                {
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
            output2 += SEPARADOR2 + "\n";
            output2 += _titulo;
            output2 += "\n\tInforme de Inventario\n"+ SEPARADOR+"\n";
            output2 += SEPARADOR2;
            output2 += "\nClasificacion\tCantidad\n";
            output2 += "Frituras\t\t" + cat1 + "\n";
            output2 += "Reposteria\t\t" + cat2 + "\n";
            output2 += "Refrescos Gaseosos\t" + cat3 + "\n";
            output2 += "Refrescos Naturales\t" + cat4 + "\n";
            output2 += "Confiteria\t\t" + cat5 + "\n\n";
            output2 += "...\n";
            output2 += "Nivel 1\t\t" + niv1 + "\n";
            output2 += "Nivel 2\t\t" + niv2 + "\n";
            output2 += "Nivel 3\t\t" + niv3 + "\n";
            output2 += "Nivel 4\t\t" + niv4 + "\n";
            output2 += "Nivel 5\t\t" + niv5 + "\n";
            output2 += "...\n";
            output2 += SEPARADOR2+"\n";
            output2 += "Fin de listado";

            //ENDFOR

            guardarDoc("output2",output1);
                string currentPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName);
                StreamWriter sw = new StreamWriter(currentPath + "\\Output1.txt", true, Encoding.Unicode); //Ruta
                sw.Write(output1);
                sw.Close();
            output1 +="\n" +_sep + "\nFin de listado";

            Console.WriteLine(output1);
            Console.WriteLine(output2);
            Console.ReadKey();
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




        //Descuento
        //Console.WriteLine("Seleccione el descuento activo o no (si no):");    
        //Console.Write(ProductoDiscount.Frituras + ":" + (int)ProductoDiscount.Frituras + "\n" +
        //              ProductoDiscount.Repostería + ":" + (int)ProductoDiscount.Repostería + "\n" +
        //              ProductoDiscount.Refrescos_Gaseosos + ":" + (int)ProductoDiscount.Refrescos_Gaseosos+ "\n" +
        //              ProductoDiscount.Refrescos_Naturales + ":" + (int)ProductoDiscount.Refrescos_Naturales + "\n" +
        //              ProductoDiscount.Confitería + ":" + (int)ProductoDiscount.Confitería + "\n");
    }
}
