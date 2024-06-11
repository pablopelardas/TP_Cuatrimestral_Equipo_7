using System;
using Datos.EF;
using System.Collections.Generic;

public static class EventosSeed
{
    public static List<EVENTO> getEventos()
    {
        return new List<EVENTO>
        {
            new EVENTO
            {
                id_evento = 1,
                id_cliente = 1,
                fecha = new DateTime(2021, 12, 24),
                id_tipo_evento = 1
            },
            new EVENTO
            {
                id_evento = 2,
                id_cliente = 2,
                fecha = new DateTime(2021, 12, 24),
                id_tipo_evento = 2
            },
            new EVENTO
            {
                id_evento = 3,
                id_cliente = 1,
                fecha = new DateTime(2021, 12, 24),
                id_tipo_evento = 3
            },
            new EVENTO
            {
                id_evento = 4,
                id_cliente = 2,
                fecha = new DateTime(2021, 12, 24),
                id_tipo_evento = 4
            },
            new EVENTO
            {id_evento = 5,
                id_cliente = 1,
                fecha = new DateTime(2021, 12, 24),
                id_tipo_evento = 5
            },
            new EVENTO
            {
                id_evento = 6,
                id_cliente = 2,
                fecha = new DateTime(2021, 12, 24),
                id_tipo_evento = 6
            },
            new EVENTO
            {id_evento = 7,
                id_cliente = 1,
                fecha = new DateTime(2021, 12, 24),
                id_tipo_evento = 7
            },
            new EVENTO
            {
                id_evento = 8,
                id_cliente = 2,
                fecha = new DateTime(2021, 12, 24),
                id_tipo_evento = 8
            }
        };
    }

}