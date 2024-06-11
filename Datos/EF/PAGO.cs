using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.EF
{
    public class PAGO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_pago { get; set; }
        public Guid id_cliente { get; set; }
        public Guid id_orden { get; set; }
        public DateTime fecha { get; set; }
        public decimal monto { get; set; }
        public string tipo_pago { get; set; }

        [ForeignKey("id_cliente")]
        public virtual CONTACTO CLIENTE { get; set; }

        [ForeignKey("id_orden")]
        public virtual ORDEN ORDEN { get; set; }

    }
}
