namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRODUCTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTO()
        {
            DETALLE_ORDENES = new HashSet<DETALLEORDEN>();
            IMAGENES = new HashSet<IMAGENPRODUCTO>();
            DETALLE_PRODUCTOS = new HashSet<DETALLEPRODUCTO>();
        }

        [Key]
        public int id_producto { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [StringLength(200)]
        public string descripcion { get; set; }

        public int porciones { get; set; }

        public decimal horas_trabajo { get; set; }

        [Required]
        [StringLength(50)]
        public string tipo_precio { get; set; }

        public decimal valor_precio { get; set; }

        public int id_categoria { get; set; }

        public virtual CATEGORIA CATEGORIA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLEORDEN> DETALLE_ORDENES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IMAGENPRODUCTO> IMAGENES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLEPRODUCTO> DETALLE_PRODUCTOS { get; set; }
    }
}
