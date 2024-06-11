using Datos.EF;
using System.Collections.Generic;

public static class CategoriasSeed
{
    public static List<CATEGORIA> getCategorias()
    {
        return new List<CATEGORIA>
        {
            new CATEGORIA
            {
                id_categoria = 1,
                nombre = "Torta",
                tipo = "Receta"
            },
            new CATEGORIA
            {
                id_categoria = 2,
                nombre = "Galleta",
                tipo = "Receta"
            },
            new CATEGORIA
            {
                id_categoria = 3,
                nombre = "Pan",
                tipo = "Receta"
            },
            new CATEGORIA
            {
                id_categoria = 4,
                nombre = "Tarta",
                tipo = "Producto"
            },
            new CATEGORIA
            {
                id_categoria = 5,
                nombre = "Insumos",
                tipo = "Suministro"
            },
            new CATEGORIA
            {
                id_categoria = 6,
                nombre = "Decoracion",
                tipo = "Suministro"
            },
            new CATEGORIA
            {
                id_categoria = 7,
                nombre = "Cajas y Bolsas",
                tipo = "Suministro"
            },
            new CATEGORIA
            {
                id_categoria = 8,
                nombre = "Tarjetas y Etiquetas",
                tipo = "Suministro"
            },
            new CATEGORIA
            {
                id_categoria = 9,
                nombre = "Otros",
                tipo = "Suministro"
            }
        };
    }

}