using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.EF
{
    public partial class HISTORICOENTIDAD
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id_historico { get; set; }
        public Guid id_entidad { get; set; }
        public DateTime fecha { get; set; }
        public string justificacion { get; set; }
        
    }
}