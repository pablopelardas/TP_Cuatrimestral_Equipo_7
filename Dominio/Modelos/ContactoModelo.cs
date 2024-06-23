using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ContactoModelo
    {
        public Guid Id { get; set; }
        public string NombreApellido { get; set; }

        public string Rol { get; set; }

        public string Telefono { get; set; }
        public string Email { get; set; }
        public string InformacionPersonal { get; set; }

        public string Fuente { get; set; }
        public string ProductoQueProvee { get; set; }
        public bool DeseaRecibirCorreos { get; set; }
        public bool DeseaRecibirWhatsapp { get; set; }

        public string DatosDeContacto
        {
            get { return $"{NombreApellido} - {Telefono} - {Email}"; }
        }
        
        public string ShortId
        {
            get { return Id.ToString().Substring(0, 8); }
        }
        
        public List<DireccionModelo> Direcciones { get; set; }
        public List<EventoModelo> Eventos { get; set; }
    }
}
