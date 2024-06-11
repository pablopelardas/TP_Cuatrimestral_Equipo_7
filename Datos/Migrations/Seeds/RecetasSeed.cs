using Datos.EF;
using System.Collections.Generic;

public static class RecetasSeed
{
    public static List<RECETA> getRecetas()
    {
        return new List<RECETA>
        {
            new RECETA
            {
                id_receta = 1,
                nombre = "Torta de Vainilla",
                id_categoria = 1,
                descripcion = "Torta de vainilla con crema y frutillas",
            },
            new RECETA
            {
                id_receta = 2,
                nombre = "Torta de Chocolate",
                id_categoria = 1,
                descripcion = "Torta de chocolate con crema y frutillas",
            },
            new RECETA
            {
                id_receta = 3,
                nombre = "Torta de Dulce de Leche",
                id_categoria = 1,
                descripcion = "Torta de dulce de leche con crema y frutillas",
            },
            new RECETA
            {
                id_receta = 4,
                nombre = "Galletas de Vainilla",
                id_categoria = 2,
                descripcion = "Galletas de vainilla con azucar",
            },
            new RECETA
            {
                id_receta = 5,
                nombre = "Galletas de Chocolate",
                id_categoria = 2,
                descripcion = "Galletas de chocolate con azucar",
            },
            new RECETA
            {
                id_receta = 6,
                nombre = "Galletas de Limon",
                id_categoria = 2,
                descripcion = "Galletas de limon con azucar",
            },
            new RECETA
            {
                id_receta = 7,
                nombre = "Pan de Vainilla",
                id_categoria = 3,
                descripcion = "Pan de vainilla con azucar",
            }
        };
    }

}