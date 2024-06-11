namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CONTACTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONTACTO()
        {
            EVENTOS = new HashSet<EVENTO>();
            ORDENES = new HashSet<ORDEN>();
        }

        [Key]
        public int id_contacto { get; set; }

        [Required]
        [StringLength(100)]
        public string nombre_apellido { get; set; }

        [Required]
        [StringLength(20)]
        public string tipo { get; set; }

        [Required]
        [StringLength(100)]
        public string correo { get; set; }

        [Required]
        [StringLength(20)]
        public string telefono { get; set; }

        [StringLength(100)]
        public string fuente { get; set; }

        [Required]
        [StringLength(200)]
        public string direccion { get; set; }

        [StringLength(100)]
        public string producto_que_provee { get; set; }

        public bool desea_recibir_correos { get; set; }

        public bool desea_recibir_whatsapp { get; set; }

        public string informacion_personal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EVENTO> EVENTOS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEN> ORDENES { get; set; }
    }
}
