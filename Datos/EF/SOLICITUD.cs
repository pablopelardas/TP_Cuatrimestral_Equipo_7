using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.EF
{
    public class SOLICITUD
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_solicitud { get; set; }
        
        // datos evento
        public Guid tipo_evento { get; set; }
        public DateTime fecha_evento { get; set; }
        public string tipo_entrega { get; set; }
        public TimeSpan hora_entrega { get; set; }
        // direccion
        public string calle_numero { get; set; }
        public string localidad { get; set; }
        public string codigo_postal { get; set; }
        public string piso { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        
        public string json_productos { get; set; } // {id_producto: cantidad}
        
        // datos cliente
        [StringLength(100)]
        public string nombre_apellido { get; set; }
        [StringLength(100)]
        [EmailAddress]
        public string correo { get; set; }
        [StringLength(20)]
        public string telefono { get; set; }
        [StringLength(100)]
        public string fuente { get; set; }
        public bool desea_recibir_correos { get; set; }

        public bool desea_recibir_whatsapp { get; set; }
        
    }
}