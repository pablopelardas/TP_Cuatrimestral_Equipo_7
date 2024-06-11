namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DETALLEPRODUCTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_detalle_producto { get; set; }
        public Guid id_producto { get; set; }
        public Guid? id_suministro { get; set; }
        public Guid? id_receta { get; set; }

        public int cantidad { get; set; }


        [ForeignKey("id_producto")]
        public virtual PRODUCTO PRODUCTO { get; set; }

        [ForeignKey("id_receta")]
        public virtual RECETA RECETA { get; set; }
        [ForeignKey("id_suministro")]
        public virtual SUMINISTRO SUMINISTRO { get; set; }
    }
}
