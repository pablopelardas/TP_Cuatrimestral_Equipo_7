using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mappers
{
    public class ContactoMapper
    {
        public static Dominio.Modelos.ContactoModelo EntidadAModelo(Entidades.ContactoEntidad contactoEntidad)
        {
            return new Dominio.Modelos.ContactoModelo
            {
                Id = contactoEntidad.id_contacto,
                NombreApellido = contactoEntidad.nombre_apellido,
                Rol = contactoEntidad.tipo,
                Telefono = contactoEntidad.telefono,
                Email = contactoEntidad.correo,
                Direccion = contactoEntidad.direccion,
                Fuente = contactoEntidad.fuente,
                ProductoQueProvee = contactoEntidad.producto_que_provee,
                DeseaRecibirCorreos = contactoEntidad.desea_recibir_correos,
                DeseaRecibirWhatsapp = contactoEntidad.desea_recibir_whatsapp,
                InformacionPersonal = contactoEntidad.informacion_personal
            };
        }

        public static Entidades.ContactoEntidad ModeloAEntidad(Dominio.Modelos.ContactoModelo contacto)
        {
            return new Entidades.ContactoEntidad
            {
                id_contacto = contacto.Id,
                nombre_apellido = contacto.NombreApellido,
                tipo = contacto.Rol,
                telefono = contacto.Telefono,
                correo = contacto.Email,
                direccion = contacto.Direccion,
                fuente = contacto.Fuente,
                producto_que_provee = contacto.ProductoQueProvee,
                desea_recibir_correos = contacto.DeseaRecibirCorreos,
                desea_recibir_whatsapp = contacto.DeseaRecibirWhatsapp,
                informacion_personal = contacto.InformacionPersonal
            };
        }

    }
}
