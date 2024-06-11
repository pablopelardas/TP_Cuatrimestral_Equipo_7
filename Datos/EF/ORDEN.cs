namespace Datos.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ORDEN
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_orden { get; set; }

        public Guid id_cliente { get; set; }

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

        public Guid? id_evento { get; set; }

        [ForeignKey("id_cliente")]
        public virtual CONTACTO CLIENTE { get; set; }

        [ForeignKey("id_orden")]
        public virtual ICollection<DETALLEORDEN> DETALLE_ORDENES { get; set; }

        [ForeignKey("id_evento")]
        public virtual EVENTO EVENTO { get; set; }

        [ForeignKey("id_orden_estado")]
        public virtual ORDENESTADO ESTADO { get; set; }

        [ForeignKey("id_orden_pago_estado")]
        public virtual ORDENPAGOESTADO ESTADO_PAGO { get; set; }

        [ForeignKey("id_orden")]
        public virtual ICollection<PAGO> PAGOS { get; set; }
    }
}
