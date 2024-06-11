namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SUMINISTRO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUMINISTRO()
        {
            DETALLE_PRODUCTOS = new HashSet<DETALLEPRODUCTO>();
        }

        [Key]
        public int id_suministro { get; set; }

        public int id_categoria { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [StringLength(50)]
        public string proveedor { get; set; }

        public double cantidad { get; set; }

        [Column(TypeName = "money")]
        public decimal costo { get; set; }

        public virtual CATEGORIA CATEGORIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLEPRODUCTO> DETALLE_PRODUCTOS { get; set; }
    }
}
