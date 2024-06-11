namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class ORDENESTADO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_orden_estado { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [ForeignKey("id_orden_estado")]
        public virtual ICollection<ORDEN> ORDENES { get; set; }
    }
}
