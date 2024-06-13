using System;
using Datos.EF;
using System.Collections.Generic;
using System.Linq;

public static class DireccionesSeed
{

    public static List<DIRECCION> getDirecciones()
    {
        return new List<DIRECCION>
        {
            new DIRECCION()
            {
                descripcion = "Depto 3 A",
                google_name = "Calle Siempre Viva",
                google_lat = "-31.0099311",
                google_lng = "-64.25451199999999",
                google_place_id =
                    "Ei9DLiBTaWVtcHJlIFZpdmEsIExhIEdyYW5qYSwgQ8OzcmRvYmEsIEFyZ2VudGluYSIuKiwKFAoSCWUIUgCwfjKUEVBsZlxpKpKPEhQKEgkTZjEhyn4ylBExz4EsGiYL8w",
                google_formatted_address = "Calle Siempre Viva 123, La Granja, Córdoba, Argentina",
                google_url =
                    "https://maps.google.com/?q=C.+Siempre+Viva,+La+Granja,+C%C3%B3rdoba,+Argentina&ftid=0x94327eb000520865:0x8f922a695c666c50",
            },
            new DIRECCION()
            {
                descripcion = "Casa de la esquina",
                google_name = "Triunvirato y Los Incas",
                google_lat = "-34.581314",
                google_lng = "-58.473913",
                google_place_id = "ChIJozb6eHK2vJURRXuq8H5gnsI",
                google_formatted_address = "Av Triunvirato 3749, C1427 Cdad. Autónoma de Buenos Aires, Argentina",
                google_url = "https://maps.google.com/?cid=14023752387998153541",
            }
        };
    }
}