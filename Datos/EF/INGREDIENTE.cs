namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class INGREDIENTE
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_ingrediente { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        public double cantidad { get; set; }

        public Guid id_unidad { get; set; }

        [Column(TypeName = "money")]
        public decimal costo { get; set; }

        [StringLength(50)]
        public string proveedor { get; set; }

        [ForeignKey("id_ingrediente")]
        public virtual ICollection<DETALLERECETA> DETALLE_RECETAS { get; set; }

        [ForeignKey("id_unidad")]
        public virtual UNIDAD_MEDIDA UNIDAD_MEDIDA { get; set; }
    }
}
