using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class ProductoDetalleOrdenEntidad
    {
        public decimal producto_costo { get; set; }
        public int producto_porciones { get; set; }
        public decimal producto_precio { get; set; }
        public int id_producto { get; set; }
        public string producto_nombre { get; set; }

        public string descripcion { get; set; }

        public decimal horas_trabajo { get; set; }

        public string tipo_precio { get; set; }

        public int id_categoria { get; set; }

        public string tipo { get; set; }

        public string nombre { get; set; }
        public int cantidad { get; set; }

    }
}
