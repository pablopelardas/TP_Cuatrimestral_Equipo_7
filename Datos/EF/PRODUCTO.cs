namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRODUCTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_producto { get; set; }

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

        public Guid id_categoria { get; set; }

        [ForeignKey("id_categoria")]
        public virtual CATEGORIA CATEGORIA { get; set; }

        [ForeignKey("id_producto")]
        public virtual ICollection<DETALLEORDEN> DETALLE_ORDENES { get; set; }

        [ForeignKey("id_producto")]
        public virtual ICollection<IMAGENPRODUCTO> IMAGENES { get; set; }

        [ForeignKey("id_producto")]
        public virtual ICollection<DETALLEPRODUCTO> DETALLE_PRODUCTOS { get; set; }
    }
}
