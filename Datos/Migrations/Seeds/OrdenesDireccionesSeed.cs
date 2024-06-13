using System;
using Datos.EF;
using System.Collections.Generic;
using System.Linq;

public static class OrdenesDireccionesSeed
{

    public static List<ORDENDIRECCION> getDirecciones(Entities context)
    {
        List<ORDEN> ordenesContext = context.ORDENES.ToList();
        
        return new List<ORDENDIRECCION>
        {
            new ORDENDIRECCION()
            {
                calle_numero = "Av Triunvirato 3749",
                piso = "1",
                departamento = "A",
                localidad = "Villa Urquiza",
                provincia = "CABA",
                codigo_postal = "1431",
                id_orden_direccion = ordenesContext[0].id_orden,
            },
            new ORDENDIRECCION()
            {
                calle_numero = "Presidente Peron 1234",
                piso = "2",
                departamento = "B",
                localidad = "San Miguel",
                provincia = "Buenos Aires",
                codigo_postal = "1663",
                id_orden_direccion = ordenesContext[1].id_orden,
                
            },
            new ORDENDIRECCION()
            {
                calle_numero = "Av. Rivadavia 1234",
                piso = "3",
                departamento = "C",
                localidad = "Caballito",
                provincia = "CABA",
                codigo_postal = "1406",
                id_orden_direccion = ordenesContext[2].id_orden,
            },
        };
    }
}