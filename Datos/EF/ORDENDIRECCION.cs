﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.EF
{
    public class ORDENDIRECCION
    {
        [Key]
        [ForeignKey("ORDEN")]
        public Guid id_orden_direccion { get; set; }
        public string calle_numero { get; set; }
        public string localidad { get; set; }
        public string codigo_postal { get; set; }
        public string piso { get; set; }
        public string departamento { get; set; }

        public string provincia { get; set; }

        public virtual ORDEN ORDEN { get; set; }
        
    }
}
