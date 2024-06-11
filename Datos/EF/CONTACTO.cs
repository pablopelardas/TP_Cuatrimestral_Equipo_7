namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CONTACTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_contacto { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre_apellido { get; set; }

        [Required]
        [StringLength(20)]
        public string tipo { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        [StringLength(20)]
        public string telefono { get; set; }

        [StringLength(100)]
        public string fuente { get; set; }

        [StringLength(200)]
        public string direccion { get; set; }

        [StringLength(100)]
        public string producto_que_provee { get; set; }

        public bool desea_recibir_correos { get; set; }

        public bool desea_recibir_whatsapp { get; set; }

        public string informacion_personal { get; set; }

        [ForeignKey("id_cliente")]
        public virtual ICollection<EVENTO> EVENTOS { get; set; }

        [ForeignKey("id_cliente")]
        public virtual ICollection<ORDEN> ORDENES { get; set; }
    }
}
