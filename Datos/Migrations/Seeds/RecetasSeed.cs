using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using Datos.Migrations;

public static class RecetasSeed
{

    public static List<RECETA> getRecetas(Datos.EF.Entities context)
    {
        List<CATEGORIA> categorias = context.CATEGORIAS.Where(c => c.tipo == "Receta").ToList();

        Guid randomCategoryGuid()
        {
            int index = Configuration.GlobalRandom.Next(0, categorias.Count);
            return categorias[index].id_categoria;
        }

        return new List<RECETA>
        {
            new RECETA
            {
                nombre = "Alfajores de maicena",
                id_categoria = categorias.Find(c => c.nombre == "Alfajores").id_categoria,
                descripcion = "Alfajores de maicena",
                rendimiento = "10 unidades",
            },
            new RECETA
            {
                nombre = "Chocotorta",
                id_categoria = categorias.Find(c => c.nombre == "Tortas").id_categoria,
                descripcion = "chocolate y galletitas",
                rendimiento = "1 torta",
            },
        };
    }
}
