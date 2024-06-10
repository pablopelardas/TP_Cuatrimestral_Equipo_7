using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class EventoModelo
    {
        public int IdEvento { get; set; }
        public DateTime Fecha { get; set; }


        public TipoEventoModelo TipoEvento { get; set; }
        public ContactoModelo Cliente { get; set; }

        public OrdenModelo Orden { get; set; }
        public string Descripcion
        {
            get
            {
                return $"{Cliente.NombreApellido} - {TipoEvento.Nombre} ";
            }
        }

    }
}
