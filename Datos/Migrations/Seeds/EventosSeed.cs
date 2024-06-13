using System;
using Datos.EF;
using System.Collections.Generic;
using System.Linq;
using Datos.Migrations;

public static class EventosSeed
{
    public static List<EVENTO> getEventos(Datos.EF.Entities context)
    {

        List<TIPO_EVENTO> tipoEventosContext = context.TIPOS_EVENTOS.ToList();
        List<CONTACTO> clientesContext = context.CONTACTOS.ToList();

        EVENTO getRandomEvento(DateTime fecha)
        {
            TIPO_EVENTO tipoEvento = tipoEventosContext[Configuration.GlobalRandom.Next(0, tipoEventosContext.Count)];
            CONTACTO cliente = clientesContext[Configuration.GlobalRandom.Next(0, clientesContext.Count)];
            return new EVENTO
            {
                id_cliente = cliente.id_contacto,
                fecha = fecha,
                id_tipo_evento = tipoEvento.id_tipo_evento
            };
        }

        return new List<EVENTO>
        {
            getRandomEvento(new DateTime(2024,06,15)),
            getRandomEvento(new DateTime(2024,06,16)),
            getRandomEvento(new DateTime(2024,06,17)),
        };
    }

}