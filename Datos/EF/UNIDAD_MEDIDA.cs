namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UNIDAD_MEDIDA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_unidad { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(10)]
        public string abreviatura { get; set; }

        [ForeignKey("id_unidad")]
        public virtual ICollection<INGREDIENTE> INGREDIENTES { get; set; }
    }
}
