namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUMINISTRO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_suministro { get; set; }

        public Guid id_categoria { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(50)]
        public string proveedor { get; set; }

        public double cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal costo { get; set; }

        [ForeignKey("id_categoria")]
        public virtual CATEGORIA CATEGORIA { get; set; }
    }
}
