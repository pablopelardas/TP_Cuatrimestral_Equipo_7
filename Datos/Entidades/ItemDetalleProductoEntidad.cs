using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class ItemDetalleProductoEntidad
    {
        public int cantidad { get; set; }
        public RecetaEntidad receta { get; set; }
        public SuministroEntidad suministro { get; set; }
        public ItemDetalleProductoEntidad() { }
    }
}
