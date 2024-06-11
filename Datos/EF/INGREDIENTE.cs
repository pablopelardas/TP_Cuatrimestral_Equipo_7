namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class INGREDIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INGREDIENTE()
        {
            DETALLE_RECETAS = new HashSet<DETALLERECETA>();
        }

        [Key]
        public int id_ingrediente { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        public double cantidad { get; set; }

        public int id_unidad { get; set; }

        [Column(TypeName = "money")]
        public decimal costo { get; set; }

        [StringLength(50)]
        public string proveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLERECETA> DETALLE_RECETAS { get; set; }

        public virtual UNIDAD_MEDIDA UNIDAD_MEDIDA { get; set; }
    }
}
