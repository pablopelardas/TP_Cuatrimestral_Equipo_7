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
                nombre = "Cumpleaños"
            },
            new TIPO_EVENTO
            {
                nombre = "Aniversario"
            },
            new TIPO_EVENTO
            {
                nombre = "Bautismo"
            },
            new TIPO_EVENTO
            {
                nombre = "Comunion"
            },
            new TIPO_EVENTO
            {
                nombre = "Casamiento"
            },
            new TIPO_EVENTO
            {
                nombre = "Baby Shower"
            },
            new TIPO_EVENTO
            {
                nombre = "Despedida"
            },
            new TIPO_EVENTO
            {
                nombre = "Otro"
            }
        };
    }

}