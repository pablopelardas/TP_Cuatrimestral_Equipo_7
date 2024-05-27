﻿using System;
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

        public static Entidades.ContactoEntidad ModeloAEntidad(Dominio.Modelos.ContactoModelo contacto)
        {
            return new Entidades.ContactoEntidad
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