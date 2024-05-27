﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Entidades
{
    public class ContactoEntidad
    {
        public int Id { get; set; }
        public string NombreApellido { get; set; }

        public string Tipo { get; set; }

        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

        public string Fuente { get; set; }
        public string ProductoQueProvee { get; set; }
        public bool DeseaRecibirCorreos { get; set; }
        public bool DeseaRecibirWhatsapp { get; set; }

        public Contacto()
        {
        }

    }
}