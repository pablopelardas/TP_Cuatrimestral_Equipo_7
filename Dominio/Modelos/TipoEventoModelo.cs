using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    [Serializable()]
    public class TipoEventoModelo
    {
        public Guid IdTipoEvento { get; set; }
        public string Nombre { get; set; }

        public TipoEventoModelo() { }
    }
}
