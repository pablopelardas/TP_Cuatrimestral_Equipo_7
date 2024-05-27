using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class ContactoEntidad
    {
        public int id_contacto { get; set; }
        public string nombre_apellido { get; set; }

        public string tipo { get; set; }

        public string correo { get; set; }
        public string telefono { get; set; }
        public string fuente { get; set; }
        public string direccion { get; set; }
        public string producto_que_provee{ get; set; }
        public bool desea_recibir_correos { get; set; }
        public bool desea_recibir_whatsapp { get; set; }

        public string informacion_personal { get; set; }

        public ContactoEntidad()
        {
        }

    }
}
