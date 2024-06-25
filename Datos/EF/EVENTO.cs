namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class EVENTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_evento { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public Guid id_cliente { get; set; }

        public Guid id_tipo_evento { get; set; }
        
        public string descripcion { get; set; }

        [ForeignKey("id_cliente")]
        public virtual CONTACTO CLIENTE { get; set; }

        [ForeignKey("id_tipo_evento")]
        public virtual TIPO_EVENTO TIPO_EVENTO { get; set; }

        [ForeignKey("id_evento")]
        public virtual ICollection<ORDEN> ORDENES { get; set; }
        
    }
}
