namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class EVENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENTO()
        {
            ORDENES = new HashSet<ORDEN>();
        }

        [Key]
        public int id_evento { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha { get; set; }

        public int id_cliente { get; set; }

        public int id_tipo_evento { get; set; }

        public virtual CONTACTO CLIENTE { get; set; }

        public virtual TIPO_EVENTO TIPO_EVENTO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEN> ORDENES { get; set; }
    }
}
