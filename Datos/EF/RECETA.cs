namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RECETA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RECETA()
        {
            DETALLE_RECETAS = new HashSet<DETALLERECETA>();
            DETALLE_PRODUCTOS = new HashSet<DETALLEPRODUCTO>();
        }

        [Key]
        public int id_receta { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(200)]
        public string descripcion { get; set; }

        public int id_categoria { get; set; }

        [Column(TypeName = "money")]
        public decimal? precio_personalizado { get; set; }

        public virtual CATEGORIA CATEGORIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLERECETA> DETALLE_RECETAS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLEPRODUCTO> DETALLE_PRODUCTOS { get; set; }
    }
}
