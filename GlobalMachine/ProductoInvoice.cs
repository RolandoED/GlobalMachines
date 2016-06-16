using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalMachine
{
    class ProductoInvoice
    {
        public int codigo_articulo { get; set; }
        public int cantidad { get; set; }
        public string nombre_articulo { get; set; }
        public string tipo { get; set; }
        public string fecha_vencimiento { get; set; }

        public int posicion_estante { get; set; }
        public int precio { get; set; }
        public bool descuento { get; set; }

        public ProductoInvoice()
        {

        }

        public ProductoInvoice(int cod,int cantidad, string nom, string tip, int pos, int prec, bool d)
        {
            codigo_articulo = cod;
            nombre_articulo = nom;
            tipo = tip;
            posicion_estante = pos;
            precio = prec;
            descuento = d;

        }
    }
}
