using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;

public static class SuministrosSeed
{
    private static Random random = new Random(); 
    public static List<SUMINISTRO> getSuministros(Datos.EF.Entities context)
    {
        List<CATEGORIA> categorias = context.CATEGORIAS.ToList();

        Guid randomCategoryGuid()
        {
            int index = random.Next(0, categorias.Count);
            return categorias[index].id_categoria;
        }
        return new List<SUMINISTRO>
        {
            new SUMINISTRO
            {
                nombre = "Caja de Torta",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 200,
                proveedor = "Cajas SRL"
            },
            new SUMINISTRO
            {
                nombre = "Caja de Galletas",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 100,
                proveedor = "Cajas SRL"
            },
            new SUMINISTRO
            {
                nombre = "Caja de Pan",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 150,
                proveedor = "Cajas SRL"
            },
            new SUMINISTRO
            {
                nombre = "Bolsa de Torta",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 50,
                proveedor = "Bolsas SRL"
            },
            new SUMINISTRO
            {
                nombre = "Bolsa de Galletas",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 30,
                proveedor = "Bolsas SRL"
            },
            new SUMINISTRO
            {
                nombre = "Bolsa de Pan",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 40,
                proveedor = "Bolsas SRL"
            },
            new SUMINISTRO
            {
                nombre = "Tarjeta de Torta",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 10,
                proveedor = "Tarjetas SRL"
            },
            new SUMINISTRO
            {
                nombre = "Tarjeta de Galletas",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 5,
                proveedor = "Tarjetas SRL"
            },
            new SUMINISTRO
            {
                nombre = "Tarjeta de Pan",
                id_categoria = randomCategoryGuid(),
                cantidad = 1,
                costo = 7,
                proveedor = "Tarjetas SRL"
            },

        };
    }

}