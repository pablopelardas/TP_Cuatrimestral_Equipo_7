using Datos.EF;
using System;
using System.Collections.Generic;
using Datos.Migrations;

public static class ContactosSeed
{
    public static List<CONTACTO> GetContactos()
    {
        return new List<CONTACTO>
        {
            new CONTACTO
            {
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
                nombre_apellido = "Maria Lopez",
                telefono = "87654321",
                correo = "maria_lopez@gmail.com",
                direccion = "Calle Falsa 123",
                desea_recibir_correos = true,
                desea_recibir_whatsapp = true,
                fuente = "Instagram",
                informacion_personal = "<div><p>Informacion personal de Maria Lopez</p></div>",
                tipo = "Cliente"
            },
            new CONTACTO
            {
                nombre_apellido = "Pedro Rodriguez",
                telefono = "12345678",
                correo = "pedro_rodriguez@gmail.com",
                direccion = "Av. Siempre Viva 123",
                desea_recibir_correos = true,
                desea_recibir_whatsapp = true,
                fuente = "Facebook",
                informacion_personal = "<div><p>Informacion personal de Pedro Rodriguez</p></div>",
                tipo = "Cliente"
                }
        };
    }
}