namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RECETA
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_receta { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(200)]
        public string descripcion { get; set; }

        public Guid id_categoria { get; set; }

        [Column(TypeName = "money")]
        public decimal? precio_personalizado { get; set; }

        [ForeignKey("id_categoria")]
        public virtual CATEGORIA CATEGORIA { get; set; }

        [ForeignKey("id_receta")]
        public virtual ICollection<DETALLERECETA> DETALLE_RECETAS { get; set; }

        [ForeignKey("id_receta")]
        public virtual ICollection<DETALLEPRODUCTO> DETALLE_PRODUCTOS { get; set; }
    }
}
