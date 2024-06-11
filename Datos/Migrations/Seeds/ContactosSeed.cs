﻿using Datos.EF;
using System.Collections.Generic;

public static class ContactosSeed
{
    public static List<CONTACTO> GetContactos()
    {
        return new List<CONTACTO>
        {
            new CONTACTO
            {
                id_contacto = 1,
                nombre_apellido = "Juan Perez",
                telefono = "12345678",
                correo = "juan_perez@gmail.com",
                direccion = "Av. Siempre Viva 123",
                desea_recibir_correos = true,
                desea_recibir_whatsapp = true,
                fuente = "Facebook",
                informacion_personal = "<div><p>Informacion personal de Juan Perez</p></div>",
                tipo = "Cliente"
            },
            new CONTACTO
            {
                id_contacto = 2,
                nombre_apellido = "Maria Lopez",
                telefono = "87654321",
                correo = "maria_lopez@gmail.com",
                direccion = "Calle Falsa 123",
                desea_recibir_correos = true,
                desea_recibir_whatsapp = true,
                fuente = "Instagram",
                informacion_personal = "<div><p>Informacion personal de Maria Lopez</p></div>",
                tipo = "Cliente"
            }
        };
    }
}