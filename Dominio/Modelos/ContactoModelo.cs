using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ContactoModelo
    {
        public int Id { get; set; }
        public string NombreApellido { get; set; }

        public string Rol { get; set; }

        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        public string Fuente { get; set; }
        public string ProductoQueProvee { get; set; }
        public bool DeseaRecibirCorreos { get; set; }
        public bool DeseaRecibirWhatsapp { get; set; }
    }
}
