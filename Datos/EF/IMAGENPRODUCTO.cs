namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMAGENPRODUCTO
    {
        [Key]
        public int id_imagen { get; set; }

        public int id_producto { get; set; }

        [Required]
        [StringLength(200)]
        public string url { get; set; }

        public virtual PRODUCTO PRODUCTO { get; set; }
    }
}
