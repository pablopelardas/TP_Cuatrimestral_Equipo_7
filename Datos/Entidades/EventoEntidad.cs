using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class EventoEntidad
    {
        public int id_evento { get; set; }
        public DateTime fecha { get; set; }
        public Entidades.TipoEventoEntidad tipo_evento { get; set; }
        public Entidades.ContactoEntidad cliente { get; set; }
        public Entidades.OrdenEntidad orden { get; set; }
    }
}
