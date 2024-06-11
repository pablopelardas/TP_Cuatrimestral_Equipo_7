using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.EF
{
    public class PAGO
    {

        [Key]
        public int id_pago { get; set; }
        public int id_cliente { get; set; }
        public int id_orden { get; set; }
        public DateTime fecha { get; set; }
        public decimal monto { get; set; }
        public string tipo_pago { get; set; }
        public virtual CONTACTO CLIENTE { get; set; }

        public virtual ORDEN ORDEN { get; set; }

    }
}
