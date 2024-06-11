using Datos.EF;
using System.Collections.Generic;

public static class TipoEventosSeed
{
    public static List<TIPO_EVENTO> getTipoEventos()
    {
        return new List<TIPO_EVENTO>
        {
            new TIPO_EVENTO
            {
                id_tipo_evento = 1,
                nombre = "Cumpleaños"
            },
            new TIPO_EVENTO
            {
                id_tipo_evento = 2,
                nombre = "Aniversario"
            },
            new TIPO_EVENTO
            {
                id_tipo_evento = 3,
                nombre = "Bautismo"
            },
            new TIPO_EVENTO
            {
                id_tipo_evento = 4,
                nombre = "Comunion"
            },
            new TIPO_EVENTO
            {
                id_tipo_evento = 5,
                nombre = "Casamiento"
            },
            new TIPO_EVENTO
            {
                id_tipo_evento = 6,
                nombre = "Baby Shower"
            },
            new TIPO_EVENTO
            {
                id_tipo_evento = 7,
                nombre = "Despedida"
            },
            new TIPO_EVENTO
            {
                id_tipo_evento = 8,
                nombre = "Otro"
            }
        };
    }

}