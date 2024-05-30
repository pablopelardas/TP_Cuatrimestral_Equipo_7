using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class ProductoDetalleOrdenEntidad
    {
        public Entidades.ProductoEntidad producto { get; set; }

        public int cantidad { get; set; }
        public int producto_porciones { get; set; }
        public decimal producto_costo { get; set; }
        public decimal producto_precio { get; set; }

    }
}
