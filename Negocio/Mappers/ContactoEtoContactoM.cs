using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Mappers
{
    public class Contacto
    {
        public static Dominio.Modelos.Contacto EntidadAModelo(Datos.Entidades.Contacto contactoEntidad)
        {
            return new Dominio.Modelos.Contacto
            {
                Id = contactoEntidad.Id,
                NombreApellido = contactoEntidad.NombreApellido,
                Rol = contactoEntidad.Tipo,
                Telefono = contactoEntidad.Telefono,
                Email = contactoEntidad.Correo,
                Direccion = contactoEntidad.Direccion,
                Fuente = contactoEntidad.Fuente,
                ProductoQueProvee = contactoEntidad.ProductoQueProvee,
                DeseaRecibirCorreos = contactoEntidad.DeseaRecibirCorreos,
                DeseaRecibirWhatsapp = contactoEntidad.DeseaRecibirWhatsapp
            };
        }

        public static Datos.Entidades.Contacto ModeloAEntidad(Dominio.Modelos.Contacto contacto)
        {
            return new Datos.Entidades.Contacto
            {
                Id = contacto.Id,
                NombreApellido = contacto.NombreApellido,
                Tipo = contacto.Rol,
                Telefono = contacto.Telefono,
                Correo = contacto.Email,
                Direccion = contacto.Direccion,
                Fuente = contacto.Fuente,
                ProductoQueProvee = contacto.ProductoQueProvee,
                DeseaRecibirCorreos = contacto.DeseaRecibirCorreos,
                DeseaRecibirWhatsapp = contacto.DeseaRecibirWhatsapp
            };
        }

    }
}
