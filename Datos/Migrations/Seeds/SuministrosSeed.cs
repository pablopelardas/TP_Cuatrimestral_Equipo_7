using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using Datos.Migrations;

public static class SuministrosSeed
{
    public static List<SUMINISTRO> getSuministros(Datos.EF.Entities context)
    {
        List<CATEGORIA> categorias = context.CATEGORIAS.Where(c => c.tipo == "Suministro").ToList();
        
        return new List<SUMINISTRO>
        {
            new SUMINISTRO
            {
                id_categoria = categorias.Find(c => c.nombre == "Cajas y Bolsas").id_categoria,
                nombre = "Caja de alfajores",
                cantidad = 1,
                costo = 900,
            },
            new SUMINISTRO
            {
                id_categoria = categorias.Find(c => c.nombre == "Cajas y Bolsas").id_categoria,
                nombre = "Caja Torta 25x25x20 s/v",
                cantidad = 1,
                costo = 1485
            },
        };
    }

}