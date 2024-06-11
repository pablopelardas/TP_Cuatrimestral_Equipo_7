namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TIPO_EVENTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_tipo_evento { get; set; }

        [Required]
        [StringLength(50)]
        public string nombre { get; set; }

        [ForeignKey("id_tipo_evento")]
        public virtual ICollection<EVENTO> EVENTOS { get; set; }
    }
}
