using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;

public static class RecetasSeed
{
    private static Random random = new Random(); // Move the random variable outside the method

    public static List<RECETA> getRecetas(Datos.EF.Entities context)
    {
        List<CATEGORIA> categorias = context.CATEGORIAS.ToList();

        Guid randomCategoryGuid()
        {
            int index = random.Next(0, categorias.Count);
            return categorias[index].id_categoria;
        }

        return new List<RECETA>
        {
            new RECETA
            {
                nombre = "Torta de Vainilla",
                id_categoria = randomCategoryGuid(),
                descripcion = "Torta de vainilla con crema y frutillas",
            },
            new RECETA
            {
                nombre = "Torta de Chocolate",
                id_categoria = randomCategoryGuid(),
                descripcion = "Torta de chocolate con crema y frutillas",
            },
            new RECETA
            {
                nombre = "Torta de Dulce de Leche",
                id_categoria = randomCategoryGuid(),
                descripcion = "Torta de dulce de leche con crema y frutillas",
            },
            new RECETA
            {
                nombre = "Galletas de Vainilla",
                id_categoria = randomCategoryGuid(),
                descripcion = "Galletas de vainilla con azucar",
            },
            new RECETA
            {
                nombre = "Galletas de Chocolate",
                id_categoria = randomCategoryGuid(),
                descripcion = "Galletas de chocolate con azucar",
            },
            new RECETA
            {
                nombre = "Galletas de Limon",
                id_categoria = randomCategoryGuid(),
                descripcion = "Galletas de limon con azucar",
            },
            new RECETA
            {
                nombre = "Pan de Vainilla",
                id_categoria = randomCategoryGuid(),
                descripcion = "Pan de vainilla con azucar",
            }
        };
    }
}
