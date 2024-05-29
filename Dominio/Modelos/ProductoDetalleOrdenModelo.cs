using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ProductoDetalleOrdenModelo
    {
        public ProductoModelo Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitarioActual { get; set; }
        public decimal PrecioUnitarioActual { get; set; }
        public int Porciones { get; set; }

        public decimal Subtotal
        {
            get
            {
                return Cantidad * PrecioUnitarioActual;
            }
        }

        public ProductoDetalleOrdenModelo() { }

    }
}
