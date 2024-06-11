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
        public int id_detalle_producto { get; set; }
        public int id_producto { get; set; }
        public int? id_suministro { get; set; }
        public int? id_receta { get; set; }

        public int cantidad { get; set; }

        public virtual PRODUCTO PRODUCTO { get; set; }

        public virtual RECETA RECETA { get; set; }

        public virtual SUMINISTRO SUMINISTRO { get; set; }
    }
}
