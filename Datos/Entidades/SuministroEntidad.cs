using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class SuministroEntidad
    {
        public int id_suministro { get; set; }
        public int id_categoria { get; set; }
        public string nombre { get; set; }
        public string proveedor { get; set; }
        public float cantidad { get; set; }
        public decimal costo { get; set; }
        public SuministroEntidad() { }
    }
}
