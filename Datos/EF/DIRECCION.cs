using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.EF
{
    public class DIRECCION
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public Guid id_direccion { get; set; }
        public string descripcion { get; set; }
        
        public string google_name { get; set; }
        public string google_lat { get; set; }
        public string google_lng { get; set; }
        public string google_place_id { get; set; }
        public string google_formatted_address { get; set; }
        public string google_url { get; set; }
        
        [ForeignKey("id_direccion")]
        public virtual ICollection<ORDEN> ORDENES { get; set; }

    }
}
