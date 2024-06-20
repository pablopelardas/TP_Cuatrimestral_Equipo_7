using Datos.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using Datos.Migrations;

public static class ProductosSeed
{
    public static List<PRODUCTO> getProductos(Datos.EF.Entities context)
    {
        List<CATEGORIA> categorias = context.CATEGORIAS.Where(c => c.tipo == "Receta").ToList();

        Guid randomCategoryGuid()
        {
            int index = Configuration.GlobalRandom.Next(0, categorias.Count);
            return categorias[index].id_categoria;
        }
        return new List<PRODUCTO>
        {
            new PRODUCTO
            {
                nombre = "Tarta de Frutillas",
                id_categoria = randomCategoryGuid(),
                descripcion = "Tarta de frutillas con crema",
                horas_trabajo = 3,
                porciones = 8,
                tipo_precio = "Margen",
                valor_precio = 30,
            },
            new PRODUCTO
            {
                nombre = "Tarta de Chocolate",
                id_categoria = randomCategoryGuid(),
                descripcion = "Tarta de chocolate con crema",
                horas_trabajo = 3,
                porciones = 8,
                tipo_precio = "Margen",
                valor_precio = 30,
            },
            new PRODUCTO
            {
                nombre = "Tarta de Dulce de Leche",
                id_categoria = randomCategoryGuid(),
                descripcion = "Tarta de dulce de leche con crema",
                horas_trabajo = 3,
                porciones = 8,
                tipo_precio = "Margen",
                valor_precio = 30,
            },
            new PRODUCTO
            {
                nombre = "Galletas de Vainilla",
                id_categoria = randomCategoryGuid(),
                descripcion = "Galletas de vainilla con azucar",
                horas_trabajo = 1,
                porciones = 12,
                tipo_precio = "Margen",
                valor_precio = 20,
            },
            new PRODUCTO
            {
                nombre = "Galletas de Chocolate",
                id_categoria = randomCategoryGuid(),
                descripcion = "Galletas de chocolate con azucar",
                horas_trabajo = 1,
                porciones = 12,
                tipo_precio = "Margen",
                valor_precio = 20,
            },
            new PRODUCTO
            {
                nombre = "Galletas de Limon",
                id_categoria = randomCategoryGuid(),
                descripcion = "Galletas de limon con azucar",
                horas_trabajo = 1,
                porciones = 12,
                tipo_precio = "Margen",
                valor_precio = 20,
            }
        };
    }
}