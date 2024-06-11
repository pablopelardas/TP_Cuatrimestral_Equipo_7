namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DETALLEORDEN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid id_orden { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid id_producto { get; set; }

        public int cantidad { get; set; }

        public int producto_porciones { get; set; }

        [Column(TypeName = "money")]
        public decimal producto_costo { get; set; }

        [Column(TypeName = "money")]
        public decimal producto_precio { get; set; }

        [ForeignKey("id_orden")]
        public virtual ORDEN ORDEN { get; set; }

        [ForeignKey("id_producto")]
        public virtual PRODUCTO PRODUCTO { get; set; }
    }
}
