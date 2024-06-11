namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ORDEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDEN()
        {
            DETALLE_ORDENES = new HashSet<DETALLEORDEN>();
        }

        [Key]
        public int id_orden { get; set; }

        public int id_cliente { get; set; }

        [Required]
        [StringLength(50)]
        public string tipo_entrega { get; set; }

        public string descripcion { get; set; }

        public decimal? descuento_porcentaje { get; set; }

        public decimal? costo_envio { get; set; }

        [StringLength(200)]
        public string direccion_entrega { get; set; }

        public TimeSpan hora_entrega { get; set; }

        public int? id_orden_estado { get; set; }

        public int? id_orden_pago_estado { get; set; }

        public int? id_evento { get; set; }

        public virtual CONTACTO CLIENTE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLEORDEN> DETALLE_ORDENES { get; set; }

        public virtual EVENTO EVENTO { get; set; }

        public virtual ORDENESTADO ESTADO { get; set; }

        public virtual ORDENPAGOESTADO ESTADO_PAGO { get; set; }
    }
}
