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
        public DateTime fecha { get; set; }
        public string tipo_evento { get; set; }
        public string tipo_entrega { get; set; }
        public string descripcion { get; set; }
        public decimal descuento_porcentaje { get; set; }
        public decimal costo_envio { get; set; }
        public string hora_entrega { get; set; }
        public string direccion_entrega { get; set; }

        public decimal subtotal { get; set; }

        public Entidades.ContactoEntidad cliente { get; set; }
        public Entidades.OrdenEstadoEntidad estado { get; set; }
        public Entidades.OrdenEstadoPagoEntidad estado_pago { get; set; }

        public OrdenEntidad() { }
    }
}
