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
                nombre = "Torta",
                tipo = "Receta"
            },
            new CATEGORIA
            {
                nombre = "Galleta",
                tipo = "Receta"
            },
            new CATEGORIA
            {
                nombre = "Pan",
                tipo = "Receta"
            },
            new CATEGORIA
            {
                nombre = "Tarta",
                tipo = "Producto"
            },
            new CATEGORIA
            {
                nombre = "Insumos",
                tipo = "Suministro"
            },
            new CATEGORIA
            {
                nombre = "Decoracion",
                tipo = "Suministro"
            },
            new CATEGORIA
            {
                nombre = "Cajas y Bolsas",
                tipo = "Suministro"
            },
            new CATEGORIA
            {
                nombre = "Tarjetas y Etiquetas",
                tipo = "Suministro"
            },
            new CATEGORIA
            {
                nombre = "Otros",
                tipo = "Suministro"
            }
        };
    }

}