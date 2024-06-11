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
                tipo = "Receta",
                imagen = "torta.svg"
            },
            new CATEGORIA
            {
                nombre = "Galleta",
                tipo = "Receta",
                imagen = "galleta.svg"
            },
            new CATEGORIA
            {
                nombre = "Pan",
                tipo = "Receta",
                imagen = "pan.svg"
            },
            new CATEGORIA
            {
                nombre = "Tarta",
                tipo = "Producto",
                imagen = "tarta.svg"
            },
            new CATEGORIA
            {
                nombre = "Insumos",
                tipo = "Suministro",
                imagen = "insumos.svg"
            },
            new CATEGORIA
            {
                nombre = "Decoracion",
                tipo = "Suministro",
                imagen = "decoracion.svg"
            },
            new CATEGORIA
            {
                nombre = "Cajas y Bolsas",
                tipo = "Suministro",
                imagen = "cajasybolsas.svg"
            },
            new CATEGORIA
            {
                nombre = "Tarjetas y Etiquetas",
                tipo = "Suministro",
                imagen = "tarjetasyetiquetas.svg"
            },
            new CATEGORIA
            {
                nombre = "Otros",
                tipo = "Suministro",
                imagen = "otros.svg"
            }
        };
    }

}