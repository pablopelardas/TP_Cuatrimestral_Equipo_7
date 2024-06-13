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
                calle_numero = "Av Triunvirato 3749",
                piso = "1",
                departamento = "A",
                localidad = "Villa Urquiza",
                provincia = "CABA",
                codigo_postal = "1431"
            },
            new DIRECCION()
            {
                calle_numero = "Presidente Peron 1234",
                piso = "2",
                departamento = "B",
                localidad = "San Miguel",
                provincia = "Buenos Aires",
            },
            new DIRECCION()
            {
                calle_numero = "Av. Rivadavia 1234",
                piso = "3",
                departamento = "C",
                localidad = "Caballito",
                provincia = "CABA",
                codigo_postal = "1406"
            },
        };
    }
}