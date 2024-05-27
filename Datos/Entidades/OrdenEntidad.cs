using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class OrdenEntidad
    {
        public int id_orden { get; set; }
        public int id_cliente { get; set; }
        public DateTime fecha { get; set; }
        public string tipo_evento { get; set; }
        public string tipo_entrega { get; set; }
        public decimal total { get; set; }
        public decimal descuento { get; set; }
        public decimal incremento { get; set; }
        public string descripcion { get; set; }
        public OrdenEntidad() { };
    }
}
