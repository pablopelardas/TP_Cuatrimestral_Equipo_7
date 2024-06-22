using System;
using Datos.EF;
using System.Collections.Generic;
using System.Linq;

public static class DireccionesSeed
{

    public static List<DIRECCION> getDirecciones(Entities context)
    {
        List<CONTACTO> clientes = context.CONTACTOS.ToList();
        
        return new List<DIRECCION>
        {
            new DIRECCION()
            {
                calle_numero = "Av Triunvirato 3749",
                piso = "1",
                departamento = "A",
                localidad = "Villa Urquiza",
                provincia = "CABA",
                codigo_postal = "1431",
                id_cliente = clientes[0].id_contacto,
            },
            new DIRECCION()
            {
                calle_numero = "Presidente Peron 1234",
                piso = "2",
                departamento = "B",
                localidad = "San Miguel",
                provincia = "Buenos Aires",
                codigo_postal = "1663",
                id_cliente = clientes[1].id_contacto,
                
            },
            new DIRECCION()
            {
                calle_numero = "Av. Rivadavia 1234",
                piso = "3",
                departamento = "C",
                localidad = "Caballito",
                provincia = "CABA",
                codigo_postal = "1406",
                id_cliente = clientes[2].id_contacto,
            },
        };
    }
}