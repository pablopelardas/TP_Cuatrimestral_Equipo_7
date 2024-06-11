namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DETALLERECETA
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid id_receta { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid id_ingrediente { get; set; }

        public double cantidad { get; set; }

        [ForeignKey("id_ingrediente")]
        public virtual INGREDIENTE INGREDIENTE { get; set; }

        [ForeignKey("id_receta")]
        public virtual RECETA RECETA { get; set; }
    }
}
