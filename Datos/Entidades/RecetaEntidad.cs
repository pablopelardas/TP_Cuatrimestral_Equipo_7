using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class RecetaEntidad
    {
        public int id_receta { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int id_categoria { get; set; }
        public decimal precio_personalizado { get; set; }
        public RecetaEntidad() { }
    }
}
