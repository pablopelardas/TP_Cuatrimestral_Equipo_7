namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CATEGORIA
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_categoria { get; set; }

        [Required]
        [StringLength(50)]
        public string tipo { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        public string imagen { get; set; }

    }
}
